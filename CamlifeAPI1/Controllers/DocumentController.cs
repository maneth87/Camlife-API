
//using NPOI.OOXML.XWPF.Util;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Runtime.Remoting.Proxies;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Http;
//using System.Web.Http.Controllers;
//using System.Web.Http.Filters;
using CamlifeAPI1.Class;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using iTextSharp.xmp.impl.xpath;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using NPOI.DDF;
using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using NPOI.Util;
using Org.BouncyCastle.Bcpg.OpenPgp;
using RestSharp.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace CamlifeAPI1.Controllers
{

    [Authorize]

    public class DocumentController : ApiController
    {
        [Route("api/Document/UploadFiles")]
        [HttpPost]
        public object UploadFile(string userName, string applicationNumber)
        {
            ResponseRequest resRequest = new ResponseRequest();
            List<string> fileList = new List<string>();
            List<string> reqFileNameList = new List<string>();

            string reqFileName = "";
            string path = "";
            string newFileName = "";
            string ext = "";
            string savePart = "";
            double fileSize = 0;
            string docCode = "";
            int sequence = 0;
            List<string> docType = new List<string>();

            bool isError = false;

            List<string> existingFileToDelete = new List<string>();

            try
            {
                HttpFileCollection fileCollection = HttpContext.Current.Request.Files;
                reqFileName = HttpContext.Current.Request.Form["fileName"];
                if (!string.IsNullOrWhiteSpace(reqFileName))
                {

                    reqFileNameList = reqFileName.Split(',').ToList();
                    /*eligible file type*/
                    docType = AppConfiguration.GetDocumentType();

                    //Create the Directory.
                    documents.PolicyContract.CreateFolder();

                    //temp path
                    string tempPath = HttpContext.Current.Server.MapPath("~/UploadFiles/");
                    string tempFilePath = "";
                    if (documents.PolicyContract.CreatedFolder)/*create folder success*/
                    {
                        path = documents.PolicyContract.FullPath + "\\";
                        /*get application */
                        var app = da_micro_application.GetApplication(applicationNumber);
                        if (app.APPLICATION_NUMBER != null)
                        {
                            var existDoc = documents.PolicyContract.GetDocumentList(app.APPLICATION_ID);

                            for (int i = 0; i < fileCollection.Count; i++)
                            {

                                newFileName = reqFileNameList[i];
                                HttpPostedFile upload = fileCollection[i];
                                ext = System.IO.Path.GetExtension(upload.FileName);
                                /*new file name with extention*/
                                newFileName = newFileName + ext;
                                /*save part combine full part and file name*/
                                savePart = path + newFileName;// + ext;

                                fileSize = fileCollection[i].ContentLength;

                                /*check file setting*/
                                if (documents.PolicyContract.IsCorrectFileType(upload.FileName))
                                {

                                    if (documents.PolicyContract.IsCorrectFileSize(upload.ContentLength, false))
                                    {
                                        var fileSplit = newFileName.Split('-');

                                        switch (fileSplit[1].Substring(0, 2))
                                        {
                                            case "AP":
                                                docCode = "APP";
                                                sequence = 1;
                                                break;
                                            case "CE":
                                                docCode = "CERT";
                                                sequence = 2;
                                                break;
                                            case "ID":
                                                docCode = "ID_CARD";
                                                sequence = 3;
                                                break;
                                            case "PA":
                                                docCode = "PAY_SLIP";
                                                sequence = 4;
                                                break;
                                            default:
                                                docCode = "";
                                                sequence = 0;
                                                break;
                                        }
                                        /*save temp file*/
                                        tempFilePath = tempPath + newFileName;
                                        upload.SaveAs(tempFilePath);

                                        /*check pdf format*/
                                        if (ext != ".pdf")
                                        {
                                            /*convert to pdf*/
                                            Document document = new Document();
                                            document.SetPageSize(PageSize.A4.Rotate());

                                            byte[] newPdf = CreatedPdf(new string[] { tempFilePath });

                                            /*set new file name in pdf*/
                                            newFileName = newFileName.Replace(ext, ".pdf");
                                            /*save file*/
                                            File.WriteAllBytes(path + newFileName, newPdf);
                                            fileList.Add(newFileName);
                                        }
                                        else
                                        {
                                            /*is pdf file, save directly*/

                                            upload.SaveAs(savePart);
                                            fileList.Add(newFileName);
                                        }


                                        bool save = false;
                                        /*check existing code*/
                                        if (existDoc.Count > 0)
                                        {
                                            foreach (documents.PolicyContract doc in existDoc)
                                            {
                                                if (doc.DocCode == docCode)
                                                {
                                                    save = documents.PolicyContract.UpdateDocument(doc.DocID, newFileName, fileSize + "", documents.PolicyContract.Path + "\\" + newFileName, DateTime.Now, userName);

                                                    if (save)
                                                    {
                                                        /*update review status to emply after uploading*/
                                                        documents.PolicyContract.UpdateReviewedStatus(doc.DocID, "", doc.ReviewedBy, doc.ReviewedOn, "");
                                                        if (documents.PolicyContract.Path + "\\" + newFileName != doc.DocPath)
                                                        {
                                                            /*if old and new file store different folder, system will delete old file*/
                                                            existingFileToDelete.Add(documents.PolicyContract.MainPath + "\\" + doc.DocPath);
                                                        }

                                                        isError = false;
                                                    }
                                                    else
                                                    {
                                                        /*save file information fail*/
                                                        isError = true;
                                                    }
                                                    break;
                                                }
                                                else
                                                {
                                                    //add new doc which not exist doc code
                                                    save = documents.PolicyContract.InsertData(sequence, app.APPLICATION_ID, docCode, newFileName, ext.Replace(".", "").ToUpper(), fileSize + "", documents.PolicyContract.Path + "\\" + newFileName, DateTime.Now, userName);

                                                    if (save)
                                                    {

                                                        isError = false;
                                                    }
                                                    else
                                                    {
                                                        /*save file information fail*/
                                                        isError = true;
                                                    }
                                                    break;
                                                }

                                            }


                                        }
                                        else
                                        {
                                            /*new document*/

                                            /*save document information*/
                                            save = documents.PolicyContract.InsertData(sequence, app.APPLICATION_ID, docCode, newFileName, ext.Replace(".", "").ToUpper(), fileSize + "", documents.PolicyContract.Path + "\\" + newFileName, DateTime.Now, userName);

                                            if (save)
                                            {

                                                isError = false;
                                            }
                                            else
                                            {
                                                /*save file information fail*/
                                                isError = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        /*file size is over limitation*/
                                        resRequest = new ResponseRequest()
                                        {
                                            StatusCode = (int)HttpStatusCode.UnsupportedMediaType,
                                            Status = "ERROR",
                                            Message = "File size is over limit.",
                                            Detail = null
                                        };
                                        isError = true;
                                        /*exit loop*/
                                        break;
                                    }
                                }
                                else
                                {
                                    /*invalid file type*/
                                    resRequest = new ResponseRequest()
                                    {
                                        StatusCode = (int)HttpStatusCode.UnsupportedMediaType,
                                        Status = "ERROR",
                                        Message = "File type is not eligible.",
                                        Detail = null
                                    };
                                    isError = true;
                                    /*exit loop*/
                                    break;
                                }

                                /*delet file in temp folder*/
                                if (File.Exists(tempFilePath))
                                    File.Delete(tempFilePath);
                            }
                            /*reset value*/

                            newFileName = "";
                            ext = "";
                            savePart = "";
                            fileSize = 0;


                        }
                        else
                        {
                            /*application not found*/
                            resRequest = new ResponseRequest()
                            {
                                StatusCode = (int)HttpStatusCode.NoContent,
                                Status = "ERROR",
                                Message = "Application number is not found",
                                Detail = null
                            };
                            isError = true;
                        }
                    }
                    else
                    {
                        /*create folder error*/
                        resRequest = new ResponseRequest()
                        {
                            StatusCode = (int)HttpStatusCode.InternalServerError,
                            Status = "ERROR",
                            Message = documents.PolicyContract.ErrorMessage,
                            Detail = null
                        };
                        isError = true;
                    }
                }
                else
                {
                    /*file name not found*/
                    resRequest = new ResponseRequest()
                    {
                        StatusCode = (int)HttpStatusCode.NoContent,
                        Status = "ERROR",
                        Message = "file name not found",
                        Detail = null
                    };
                    isError = true;
                }


                //path = HttpContext.Current.Server.MapPath("~/UploadFiles/");
                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}

                //for (int i = 0; i < fileCollection.Count; i++)
                //{
                //    string newFileName = "";
                //    string ext = "";
                //    newFileName = reqFileNameList[i];
                //    HttpPostedFile upload = fileCollection[i];
                //    ext = Path.GetExtension(upload.FileName);
                //    int f = fileCollection[i].ContentLength;
                //    string filename = upload.FileName;
                //    /*new file name with extention*/
                //    newFileName = newFileName + ext;
                //    upload.SaveAs(path + newFileName + ext);

                //    fileList.Add(newFileName);

                //}

                if (!isError)/*all files saved successfully*/
                {
                    resRequest = new ResponseRequest()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "OK",
                        Detail = fileList
                    };

                    /*delete old file after update*/
                    foreach (string f in existingFileToDelete)
                    {
                        if (File.Exists(f))
                        { File.Delete(f); }
                    }
                }
                else
                {
                    /*delete file saved recently*/
                    foreach (string strName in fileList)
                    {
                        File.Delete(path + strName);
                    }
                }
            }
            catch (Exception ex)
            {
                foreach (string strName in fileList)
                {
                    File.Delete(path + strName);
                }

                Log.AddExceptionToLog("", "", ex);

                resRequest = new ResponseRequest()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Status = "ERROR",
                    Message = "Unexpected error.",
                    Detail = null
                };
            }

            return resRequest;
        }


        [Route("api/Document/UploadFileSingle")]
        [HttpPost]
        public object UploadFileSingle(string userName, string applicationNumber)
        {
            ResponseRequest resRequest = new ResponseRequest();
            List<string> fileList = new List<string>();
            List<string> reqFileNameList = new List<string>();

            string reqFileName = "";
            string path = "";
            string newFileName = "";
            string ext = "";
            string savePart = "";
            double fileSize = 0;
            string docCode = "";
            int sequence = 0;

            List<string> docType = new List<string>();

            bool isError = false;

            List<string> existingFileToDelete = new List<string>();

            try
            {
                HttpFileCollection fileCollection = HttpContext.Current.Request.Files;
                reqFileName = HttpContext.Current.Request.Form["fileName"];
                if (!string.IsNullOrWhiteSpace(reqFileName))
                {

                    reqFileNameList = reqFileName.Split(',').ToList();
                    /*eligible file type*/
                    docType = AppConfiguration.GetDocumentType();

                    List<Inquiries.ApplicationFilter> appList = da_micro_application.GetApplicationNumberMainSub(applicationNumber);

                    //Create the Directory.
                    documents.PolicyContract.CreateFolder();

                    //temp path
                    string tempPath = HttpContext.Current.Server.MapPath("~/UploadFiles/");
                    string tempFilePath = "";
                    string fileDesc = "";

                    /*build file description */
                    foreach (var obj in appList.Where(_ => _.NumbersApplication > 0 && _.NumbersPurchasingYear > 0))
                    {
                        fileDesc = string.Concat(obj.NumbersApplication + " policy/applications, ", obj.NumbersPurchasingYear, " Years");
                        break;
                    }


                    if (documents.PolicyContract.CreatedFolder)/*create folder success*/
                    {
                        path = documents.PolicyContract.FullPath + "\\";
                        /*get application */
                        var app = da_micro_application.GetApplication(applicationNumber);
                        if (app.APPLICATION_NUMBER != null)
                        {

                            for (int i = 0; i < fileCollection.Count; i++)
                            {

                                newFileName = reqFileNameList[i];
                                HttpPostedFile upload = fileCollection[i];
                                ext = System.IO.Path.GetExtension(upload.FileName);
                                /*new file name with extention*/
                                newFileName = newFileName + ext;
                                /*save part combine full part and file name*/
                                savePart = path + newFileName;// + ext;

                                fileSize = fileCollection[i].ContentLength;

                                /*check file setting*/
                                if (documents.PolicyContract.IsCorrectFileType(upload.FileName))
                                {

                                    if (documents.PolicyContract.IsCorrectFileSize(upload.ContentLength, false))
                                    {
                                        /*save temp file*/
                                        tempFilePath = tempPath + newFileName;
                                        upload.SaveAs(tempFilePath);

                                        upload.SaveAs(savePart);
                                        fileList.Add(newFileName);

                                        bool save = false;
                                        foreach (var obj in appList)
                                        {

                                            var existDoc = documents.PolicyContract.GetDocumentList(obj.ApplicationId);
                                            if (existDoc.Count > 0)
                                            {
                                                /*UPDATE EXISTING FILE*/
                                                foreach (documents.PolicyContract doc in existDoc)
                                                {
                                                    save = documents.PolicyContract.UpdateDocument(doc.DocID, newFileName, fileSize + "", documents.PolicyContract.Path + "\\" + newFileName, DateTime.Now, userName, fileDesc, "");
                                                    if (!save)
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                /*ADD NEW FILE*/
                                                /*loop application list*/
                                                foreach (var appObj in appList)
                                                {
                                                    /*loop for document type*/
                                                    for (i = 1; i <= 4; i++)
                                                    {
                                                        if (i == 1)
                                                        {
                                                            /*save document information*/
                                                            save = documents.PolicyContract.InsertData(i, appObj.ApplicationId, "APP", newFileName, ext.Replace(".", "").ToUpper(), fileSize + "", documents.PolicyContract.Path + "\\" + newFileName, DateTime.Now, userName);
                                                            if (!save)
                                                                break;

                                                        }
                                                        else if (i == 2)
                                                        {
                                                            /*save document information*/
                                                            save = documents.PolicyContract.InsertData(i, appObj.ApplicationId, "CER", newFileName, ext.Replace(".", "").ToUpper(), fileSize + "", documents.PolicyContract.Path + "\\" + newFileName, DateTime.Now, userName);
                                                            if (!save)
                                                                break;
                                                        }
                                                        else if (i == 3)
                                                        {
                                                            /*save document information*/
                                                            save = documents.PolicyContract.InsertData(i, appObj.ApplicationId, "ID_CARD", newFileName, ext.Replace(".", "").ToUpper(), fileSize + "", documents.PolicyContract.Path + "\\" + newFileName, DateTime.Now, userName);
                                                            if (!save)
                                                                break;
                                                        }

                                                        else if (i == 4)
                                                        {
                                                            /*save document information*/
                                                            save = documents.PolicyContract.InsertData(i, appObj.ApplicationId, "PAY_SLIP", newFileName, ext.Replace(".", "").ToUpper(), fileSize + "", documents.PolicyContract.Path + "\\" + newFileName, DateTime.Now, userName);
                                                            if (!save)
                                                                break;
                                                        }
                                                    }

                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        /*file size is over limitation*/
                                        resRequest = new ResponseRequest()
                                        {
                                            StatusCode = (int)HttpStatusCode.UnsupportedMediaType,
                                            Status = "ERROR",
                                            Message = "File size is over limit.",
                                            Detail = null
                                        };
                                        isError = true;
                                        /*exit loop*/
                                        break;
                                    }
                                }
                                else
                                {
                                    /*invalid file type*/
                                    resRequest = new ResponseRequest()
                                    {
                                        StatusCode = (int)HttpStatusCode.UnsupportedMediaType,
                                        Status = "ERROR",
                                        Message = "File type is not eligible.",
                                        Detail = null
                                    };
                                    isError = true;
                                    /*exit loop*/
                                    break;
                                }

                                /*delet file in temp folder*/
                                if (File.Exists(tempFilePath))
                                    File.Delete(tempFilePath);
                            }
                            /*reset value*/

                            newFileName = "";
                            ext = "";
                            savePart = "";
                            fileSize = 0;


                        }
                        else
                        {
                            /*application not found*/
                            resRequest = new ResponseRequest()
                            {
                                StatusCode = (int)HttpStatusCode.NoContent,
                                Status = "ERROR",
                                Message = "Application number is not found",
                                Detail = null
                            };
                            isError = true;
                        }
                    }
                    else
                    {
                        /*create folder error*/
                        resRequest = new ResponseRequest()
                        {
                            StatusCode = (int)HttpStatusCode.InternalServerError,
                            Status = "ERROR",
                            Message = documents.PolicyContract.ErrorMessage,
                            Detail = null
                        };
                        isError = true;
                    }
                }
                else
                {
                    /*file name not found*/
                    resRequest = new ResponseRequest()
                    {
                        StatusCode = (int)HttpStatusCode.NoContent,
                        Status = "ERROR",
                        Message = "file name not found",
                        Detail = null
                    };
                    isError = true;
                }

                if (!isError)/*all files saved successfully*/
                {
                    resRequest = new ResponseRequest()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "OK",
                        Message = "File uploaded successfully",
                        Detail = fileList
                    };

                    /*delete old file after update*/
                    foreach (string f in existingFileToDelete)
                    {
                        if (File.Exists(f))
                        { File.Delete(f); }
                    }
                }
                else
                {
                    /*delete file saved recently*/
                    foreach (string strName in fileList)
                    {
                        File.Delete(path + strName);
                    }
                }
            }
            catch (Exception ex)
            {
                foreach (string strName in fileList)
                {
                    File.Delete(path + strName);
                }

                Log.AddExceptionToLog("", "", ex);

                resRequest = new ResponseRequest()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Status = "ERROR",
                    Message = "Unexpected error.",
                    Detail = null
                };
            }

            return resRequest;
        }

        [Route("api/Document/UploadMultiFiles")]
        [HttpPost]
        public object UploadMultiFiles(string userName, string applicationNumber)
        {
            ResponseRequest responseRequest = new ResponseRequest();
            List<string> fileList = new List<string>();
            List<string> requestFileNameList = new List<string>();

            string path = "";/* path to save files*/

            double fileContent = 0.0;
         
            List<byte[]> savedFiles = new List<byte[]>();
            bool errorFlag = false;
            List<string> savedFileNames = new List<string>();
            List<string> tempfilePathList = new List<string>();/*full file path*/
            string tempPath = HttpContext.Current.Server.MapPath("~/UploadFiles/");
            try
            {
                HttpFileCollection fileCollection = HttpContext.Current.Request.Files;
                string reqFileName = HttpContext.Current.Request.Form["fileName"];
                if (!string.IsNullOrWhiteSpace(reqFileName))
                {
                    requestFileNameList = reqFileName.Split(',').ToList<string>();
                    AppConfiguration.GetDocumentType();
                    List<Inquiries.ApplicationFilter> appFilters = da_micro_application.GetApplicationNumberMainSub(applicationNumber);
                    documents.PolicyContract.CreateFolder();
                    
                    string fileDesc = "";
                    /*Build file description*/
                    foreach (var app in appFilters.Where(_ => _.NumbersApplication > 0 && _.NumbersPurchasingYear > 0))
                    {
                        fileDesc = $"{app.NumbersApplication.ToString() + " policies/applications, "}{app.NumbersPurchasingYear} Years";
                    }

                    if (documents.PolicyContract.CreatedFolder)/*create folder*/
                    {
                        path = documents.PolicyContract.FullPath + "\\";
                        if (da_micro_application.GetApplication(applicationNumber).APPLICATION_NUMBER != null)/*check application is exist or not*/
                        {
                            for (int index = 0; index < fileCollection.Count; ++index)/*Loop file collection*/
                            {
                                string fileName = requestFileNameList[index];/*no extension*/
                                HttpPostedFile httpPostedFile = fileCollection[index];
                                string extension = System.IO.Path.GetExtension(httpPostedFile.FileName);

                                string tempFilePath = tempPath + fileName + extension;
                                tempfilePathList.Add(tempFilePath);
                                fileContent = (double)httpPostedFile.ContentLength;

                                if (documents.PolicyContract.IsCorrectFileType(httpPostedFile.FileName))
                                {
                                    if (documents.PolicyContract.IsCorrectFileSize((object)httpPostedFile.ContentLength, false))
                                    {

                                        httpPostedFile.SaveAs(tempFilePath);
                                        if (extension.ToLower() != ".pdf")
                                        {
                                           
                                            byte[]  pdf= CreatedPdf(new string[] {tempFilePath });

                                            savedFiles.Add(pdf);
                                        }
                                        else/*file is pdf*/
                                        {
                                            byte[] pdf = System.IO.File.ReadAllBytes(tempFilePath);
                                            savedFiles.Add(pdf);
                                        }
                                        /*delete temp file*/
                                        if (System.IO.File.Exists(tempFilePath))
                                            System.IO.File.Delete(tempFilePath);
                                    }
                                    else
                                    {
                                        responseRequest = new ResponseRequest()
                                        {
                                            StatusCode = 415,
                                            Status = "ERROR",
                                            Message = "File size is over limit.",
                                            Detail = (object)null
                                        };
                                        errorFlag = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    responseRequest = new ResponseRequest()
                                    {
                                        StatusCode = 415,
                                        Status = "ERROR",
                                        Message = "File type is not eligible.",
                                        Detail = (object)null
                                    };
                                    errorFlag = true;
                                    break;
                                }
                            }
                           
                            if (!errorFlag)
                            {
                                if (savedFiles.Count > 0)
                                {
                                    byte[] files = this.MergePdfForms(savedFiles).ToArray();
                                    string newFileName = applicationNumber + ".pdf";
                                    System.IO.File.WriteAllBytes(path + newFileName, files);
                                    savedFileNames.Add(newFileName);
                                    double fileSize = files.Length;
                                   
                                    foreach (var app in appFilters)
                                    {
                                        List<documents.PolicyContract> documentList = documents.PolicyContract.GetDocumentList(app.ApplicationId);/*get existing document information*/
                                        if (documentList.Count > 0)/*document is exist*/
                                        {
                                            foreach (documents.PolicyContract policyContract in documentList)
                                            {
                                                if (!documents.PolicyContract.UpdateDocument(policyContract.DocID, newFileName, fileSize.ToString() ?? "", $"{documents.PolicyContract.Path}\\{newFileName}", DateTime.Now, userName, fileDesc, "reupload file"))
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            //foreach (var filter in appFilters)
                                            //{
                                                if (documents.PolicyContract.InsertData(1, app.ApplicationId, "APP",newFileName, "PDF", fileSize.ToString() ?? "", $"{documents.PolicyContract.Path}\\{newFileName}", DateTime.Now, userName))
                                                {
                                                    if (documents.PolicyContract.InsertData(2, app.ApplicationId, "CERT", newFileName, "PDF", fileSize.ToString() ?? "", $"{documents.PolicyContract.Path}\\{newFileName}", DateTime.Now, userName))
                                                    {
                                                        if (documents.PolicyContract.InsertData(3, app.ApplicationId, "ID_CARD", newFileName, "PDF", fileSize.ToString() ?? "", $"{documents.PolicyContract.Path}\\{newFileName}", DateTime.Now, userName))
                                                        {
                                                            if (!documents.PolicyContract.InsertData(4, app.ApplicationId, "PAY_SLIP", newFileName, "PDF", fileSize.ToString() ?? "", $"{documents.PolicyContract.Path}\\{newFileName}", DateTime.Now, userName))
                                                                break;
                                                        }
                                                        else
                                                            break;
                                                    }
                                                    else
                                                        break;
                                                }
                                                else
                                                    break;
                                            //}
                                        }
                                    }
                                }
                                else
                                {
                                    responseRequest = new ResponseRequest()
                                    {
                                        StatusCode = 204,
                                        Status = "ERROR",
                                        Message = "No file selected.",
                                        Detail = (object)null
                                    };
                                    errorFlag = true;
                                }
                            }
                        }
                        else
                        {
                            responseRequest = new ResponseRequest()
                            {
                                StatusCode = 204,
                                Status = "ERROR",
                                Message = "Application number is not found",
                                Detail = (object)null
                            };
                            errorFlag = true;
                        }
                    }
                    else
                    {
                        responseRequest = new ResponseRequest()
                        {
                            StatusCode = 500,
                            Status = "ERROR",
                            Message = documents.PolicyContract.ErrorMessage,
                            Detail = (object)null
                        };
                        errorFlag = true;
                    }
                }
                else
                {
                    responseRequest = new ResponseRequest()
                    {
                        StatusCode = 204,
                        Status = "ERROR",
                        Message = "file name not found",
                        Detail = (object)null
                    };
                    errorFlag = true;
                }
                if (!errorFlag)
                {
                    responseRequest = new ResponseRequest()
                    {
                        StatusCode = 200,
                        Status = "OK",
                        Message = "File uploaded successfully",
                        Detail = (object)savedFileNames
                    };
                    //foreach (string path in savedFileNames)
                    //{
                    //    if (System.IO.File.Exists(path))
                    //        System.IO.File.Delete(path);
                    //}
                }
                else
                {
                    foreach (string p in tempfilePathList)/*delete temp files*/
                        System.IO.File.Delete(p);
                }
            }
            catch (Exception ex)
            {
                foreach (string p in tempfilePathList)/*delete temp files*/
                    System.IO.File.Delete(p);
                Log.AddExceptionToLog(nameof(DocumentController), " UploadMultiFiles(string userName, string applicationNumber)", ex);
                responseRequest = new ResponseRequest()
                {
                    StatusCode = 500,
                    Status = "ERROR",
                    Message = "Unexpected error.",
                    Detail = (object)null
                };
            }
            return (object)responseRequest;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="folderName">We can refer to project or businuss remarks</param>
        /// <returns></returns>
        [Route("api/Document/UploadTransactionFiles")]
        [HttpPost]

        public object UploadTransactionFiles(string userName, string folderName)
        {
            ResponseRequest resRequest = new ResponseRequest();
            List<string> fileList = new List<string>();
            List<string> convertedFileList = new List<string>();
            List<string> reqFileNameList = new List<string>();
            string reqFileDescription = "";
            string path = "";
            string newFileName = "";
            string ext = "";
            string savePart = "";
            double fileSize = 0;
            string docCode = "";
            List<string> savedFileList = new List<string>();
            List<string> docType = new List<string>();

            bool isError = false;
            List<string> tempFiles = new List<string>();
            List<string> existingFileToDelete = new List<string>();
            string tempPath = "";
            try
            {
                HttpFileCollection fileCollection = HttpContext.Current.Request.Files;
                reqFileDescription = HttpContext.Current.Request.Form["fileDescription"];

                tempPath = HttpContext.Current.Server.MapPath("~/UploadFiles/");
                string tempFilePath = "";

                if (string.IsNullOrWhiteSpace(reqFileDescription))
                {
                    /*file name not found*/
                    resRequest = new ResponseRequest()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Status = "Validation Error",
                        Message = "File Description is required.",
                        Detail = null
                    };
                    isError = true;
                }
                else
                {
                    //Create the Directory.
                    path = documents.TrascationFiles.CreateSubFolder(folderName);
                    if (path == "")
                    {
                        /*create folder error*/
                        resRequest = new ResponseRequest()
                        {
                            StatusCode = (int)HttpStatusCode.InternalServerError,
                            Status = "ERROR",
                            Message = "Create folder is getting error.",
                            Detail = null
                        };
                        isError = true;
                    }
                    else
                    {
                        /*check file type*/
                        if (fileCollection.Count == 0)
                        {
                            resRequest = new ResponseRequest()
                            {
                                StatusCode = (int)HttpStatusCode.BadRequest,
                                Status = "Validation Error",
                                Message = "File is required.",
                                Detail = null
                            };
                            isError = true;
                        }
                        else
                        {
                            for (int i = 0; i < fileCollection.Count; i++)
                            {
                                HttpPostedFile upload = fileCollection[i];
                                if (!documents.PolicyContract.IsCorrectFileType(upload.FileName))
                                {

                                    resRequest = new ResponseRequest()
                                    {
                                        StatusCode = (int)HttpStatusCode.BadRequest,
                                        Status = "Validation Error",
                                        Message = "File type is not eligible.",
                                        Detail = null
                                    };
                                    isError = true;
                                    /*exit loop*/
                                    break;
                                }
                                else
                                {
                                    /*check file size*/
                                    if (!documents.TrascationFiles.IsValidDocSizes(upload.ContentLength))
                                    {
                                        resRequest = new ResponseRequest()
                                        {
                                            StatusCode = (int)HttpStatusCode.BadRequest,
                                            Status = "Validation Error",
                                            Message = "File size is not eligible.",
                                            Detail = null
                                        };
                                        isError = true;
                                        /*exit loop*/
                                        break;

                                    }
                                }

                            }
                        }
                    }
                }

                /*start saving files*/
                string generateFileName = "";
                List<byte[]> convertedFiles = new List<byte[]>();

                List<PdfDocument> pdfList = new List<PdfDocument>();
                if (!isError)
                {
                    if (path != "")/*create folder success*/
                    {
                        int fileIndex = 0;

                        //string[] arrName = reqFileDescription.Split(' ');
                        //foreach (string str in arrName)
                        //{
                        //    generateFileName += str.Trim().Substring(0, 1).ToUpper();
                        //}
                        generateFileName += userName.Split('.')[0];
                        for (int i = 0; i < fileCollection.Count; i++)
                        {
                            try
                            {
                                fileIndex = i + 1;
                                newFileName = DateTime.Now.ToString("yyMMddhhmmss") + generateFileName + fileIndex;// generateFileName + (fileCollection.Count == 1 ? "" : "_" + fileIndex);
                                HttpPostedFile upload = fileCollection[i];
                                ext = System.IO.Path.GetExtension(upload.FileName);
                                /*new file name with extention*/
                                newFileName = newFileName + ext;
                                tempFilePath = tempPath + newFileName;
                                tempFiles.Add(newFileName);

                                //if (ext != ".pdf")
                                //{
                                //    upload.SaveAs(tempFilePath);

                                //    /*convert to pdf*/
                                //    Document document = new Document();
                                //    document.SetPageSize(PageSize.A4.Rotate());

                                //    byte[] newPdf = CreatedPdf(new string[] { tempFilePath });

                                //    /*set new file name in pdf*/
                                //    newFileName = newFileName.Replace(ext, ".pdf");
                                //    tempFilePath = tempPath + newFileName;

                                //    /*save file*/
                                //    File.WriteAllBytes(tempFilePath, newPdf);
                                //    convertedFileList.Add(newFileName);
                                //    tempFiles.Add(newFileName);


                                //}
                                //else
                                //{/*PDF Files*/
                                //    /*save part combine full part and file name*/
                                //    tempFilePath = tempPath + newFileName;
                                //    // fileSize = fileCollection[i].ContentLength;
                                //    // upload.SaveAs(savePart);
                                //    upload.SaveAs(tempFilePath);
                                //    fileList.Add(newFileName);
                                //    tempFiles.Add(newFileName);
                                //}

                                /*save part combine full part and file name*/
                                tempFilePath = tempPath + newFileName;
                                // fileSize = fileCollection[i].ContentLength;
                                // upload.SaveAs(savePart);
                                upload.SaveAs(tempFilePath);
                                fileList.Add(newFileName);
                                tempFiles.Add(newFileName);
                            }
                            catch (Exception ex)
                            {
                                Log.AddExceptionToLog("DocumentController", "UploadTransactionFiles(string userName, string folderName)", ex);

                                resRequest = new ResponseRequest()
                                {
                                    StatusCode = (int)HttpStatusCode.InternalServerError,
                                    Status = "ERROR",
                                    Message = "Save file [" + fileIndex + "] error.",
                                    Detail = null
                                };
                                isError = true;
                                break;
                            }

                        }/*end loop*/

                        /*reset value*/

                        newFileName = "";
                        ext = "";
                        savePart = "";
                        fileSize = 0;

                        /*save file*/
                        try
                        {
                            Document doc = new Document();
                            string merchFile = "Merge" + DateTime.Now.ToString("yyMMddhhmmss") + generateFileName + ".pdf";

                            if (convertedFileList.Count > 0)
                            {
                                PdfCopy copy = new PdfCopy(doc, new FileStream(tempPath + merchFile, FileMode.Create));
                                doc.Open();

                                // List<string> newFileList = (List<string>)fileList.Concat(convertedFileList);

                                /*merch files*/
                                foreach (string f in convertedFileList)
                                {
                                    PdfReader reader = new PdfReader(tempPath + f);
                                    copy.AddDocument(reader);
                                    reader.Close();
                                }
                                doc.Close();

                                fileList.Add(merchFile);

                                /*store for deleting*/
                                tempFiles.Add(merchFile);
                            }

                            int fileOrder = 1;
                            foreach (string f in fileList)
                            {
                                try
                                {
                                    newFileName = f.Replace(f.Split('.')[0], DateTime.Now.ToString("yyMMddhhmmss") + generateFileName + (fileList.Count == 1 ? "" : "_" + fileOrder));

                                    File.Copy(tempPath + f, path + newFileName, true);

                                    documents.TrascationFiles saveObj = new documents.TrascationFiles()
                                    {
                                        //Id will generate in its function
                                        DocName = newFileName,
                                        DocPath = documents.TrascationFiles.Path + "\\" + newFileName,
                                        DocDescription = reqFileDescription,
                                        Remarks = "",
                                        UploadedBy = userName,
                                        UploadedOn = DateTime.Now,
                                    };

                                    if (!documents.TrascationFiles.SaveDoc(saveObj))
                                    {
                                        resRequest = new ResponseRequest()
                                        {
                                            StatusCode = (int)HttpStatusCode.InternalServerError,
                                            Status = "ERROR",
                                            Message = "Save Document [" + fileIndex + "] error.",
                                            Detail = null
                                        };
                                        isError = true;
                                        break;
                                    }
                                    savedFileList.Add(newFileName);
                                    fileOrder++;
                                }
                                catch (Exception ex)
                                {
                                    Log.AddExceptionToLog("DocumentController", "UploadTransactionFiles(string userName, string folderName)", ex);
                                    isError = true;
                                    break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            isError = true;
                            Log.AddExceptionToLog("DocumentController", "UploadTransactionFiles(string userName, string folderName)", ex);
                        }
                    }
                    else
                    {
                        /*create folder error*/
                        resRequest = new ResponseRequest()
                        {
                            StatusCode = (int)HttpStatusCode.InternalServerError,
                            Status = "ERROR",
                            Message = "Create folder is getting error.",
                            Detail = null
                        };
                        isError = true;
                    }
                }



                if (!isError)/*all files saved successfully*/
                {
                    /*SEND EMAIL NOTIFICATION*/
                    string body = "";
                    string fString = "";
                    int fNo = 0;
                    body += "<h2>Registration Document Upload</h2><br />";
                    body += "<p> <strong>" + userName.Split('.')[0].ToUpper() + "</strong> has uploaded documents as below:</p>";
                    body += "<p><strong>File Description: </strong>" + reqFileDescription + "</p>";
                    body += "<p><strong><u>File List:</u></strong></p>";
                    foreach (string fName in savedFileList)
                    {
                        fNo += 1;
                        fString += "<p>" + fNo + ". " + fName + "</p>";
                    }
                    body += fString;
                    body += "<br /><br /><br /><p><strong> *** PLEASE DO NOT REPLY ***</strong></p>";
                    bl_system.SYSTEM_PARAMATER sys = AppConfiguration.GetRegistrationDocSendEmail();
                    if (sys != null)
                    {
                        if (sys.ParamaterVal != null)
                        {
                            string[] receipt = sys.ParamaterVal.Split(',');
                            SendEmail mail = new SendEmail(AppConfiguration.GetEmailHost(), AppConfiguration.GetEmailPort(), AppConfiguration.GetEmailFrom(), AppConfiguration.GetEmailPassword());
                            mail.To = receipt;
                            mail.Subject = "NOTIFICATION - Document Upload";
                            mail.Message = body;

                            mail.Send();
                        }
                    }
                    resRequest = new ResponseRequest()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "OK",
                        Detail = savedFileList

                    };

                    /*delete temp files*/
                    foreach (string strName in tempFiles)
                    {
                        if (File.Exists(tempPath + strName))
                        {
                            File.Delete(tempPath + strName);
                        }
                    }

                }
                else
                {
                    /*delete file saved recently*/
                    foreach (string strName in savedFileList)
                    {
                        if (File.Exists(path + strName))
                        {
                            File.Delete(path + strName);
                        }
                    }

                    /*delete temp files*/
                    foreach (string strName in tempFiles)
                    {
                        if (File.Exists(tempPath + strName))
                        {
                            File.Delete(tempPath + strName);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                /*delete file saved recently*/
                foreach (string strName in savedFileList)
                {
                    if (File.Exists(path + strName))
                    {
                        File.Delete(path + strName);
                    }
                }
                /*delete temp files*/
                foreach (string strName in tempFiles)
                {
                    if (File.Exists(tempPath + strName))
                    {
                        File.Delete(tempPath + strName);
                    }
                }

                Log.AddExceptionToLog("DocumentController", "UploadTransactionFiles(string userName, string folderName)", ex);

                resRequest = new ResponseRequest()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Status = "ERROR",
                    Message = "Unexpected error.",
                    Detail = null
                };
            }

            return resRequest;
        }

        [Route("api/Document/GetFile")]
        [HttpGet]
        public Object GetFile(string filePath, string docType)
        {
            ResponseRequest resRequest = new ResponseRequest();
            try
            {
                string path = "";// documents.PolicyContract.MainPath + "\\";
                if (docType.ToLower() == "registation")
                {
                    path = documents.TrascationFiles.MainPath + "\\";
                }
                else if (docType.ToLower() == "policycontract")
                {
                    path = documents.PolicyContract.MainPath + "\\";
                }

                if (File.Exists(path + filePath))
                {
                    string fileExt = System.IO.Path.GetExtension(path + filePath);
                    byte[] file = File.ReadAllBytes(path + filePath);
                    ResponseFile resFile = new ResponseFile() { File = file, Ext = fileExt };
                    resRequest = new ResponseRequest() { Status = HttpStatusCode.OK.ToString(), StatusCode = (int)HttpStatusCode.OK, Message = "Success", Detail = resFile };
                }
                else
                {
                    resRequest = new ResponseRequest() { Status = "ERROR", StatusCode = (int)HttpStatusCode.InternalServerError, Message = "File is not exist or removed.", Detail = null };

                }
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("DocumentController", "", ex);
                resRequest = new ResponseRequest() { Status = "ERROR", StatusCode = (int)HttpStatusCode.InternalServerError, Message = "Unexpected error", Detail = null };
            }
            return resRequest;
        }

        [Route("api/Document/GetDocuments")]
        [HttpPost]
        public Object GetDoucumentList(GetDoucumentListPara reqObj)
        {
            ResponseRequest resRequest = new ResponseRequest();
            try
            {
                if (reqObj == null)
                {
                    resRequest = new ResponseRequest()
                    {
                        Status = HttpStatusCode.BadRequest.ToString(),
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = "Parameter is missing",
                        Detail = null
                    };
                    return resRequest;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(reqObj.ApplicationId))
                    {
                        resRequest = new ResponseRequest()
                        {
                            Status = HttpStatusCode.BadRequest.ToString(),
                            StatusCode = (int)HttpStatusCode.BadRequest,
                            Message = "Parameter is missing",
                            Detail = "Application id is required."
                        };
                        return resRequest;
                    }
                    else if (reqObj.ChannelLocationId == null)
                    {
                        resRequest = new ResponseRequest()
                        {
                            Status = HttpStatusCode.BadRequest.ToString(),
                            StatusCode = (int)HttpStatusCode.BadRequest,
                            Message = "Parameter is missing",
                            Detail = "Channel locaiton id is required."
                        };
                        return resRequest;
                    }
                }

                string chId = string.Join(",", reqObj.ChannelLocationId);
                List<documents.PolicyContract> listDoc = documents.PolicyContract.GetDocumentList(reqObj.ApplicationId, chId);
                if (string.IsNullOrEmpty(documents.PolicyContract.ErrorMessage))
                {
                    if (listDoc.Count > 0)
                    {
                        resRequest = new ResponseRequest()
                        {
                            Status = HttpStatusCode.OK.ToString(),
                            StatusCode = (int)HttpStatusCode.OK,
                            Message = "[ " + listDoc.Count + " ] recode(s) found.",
                            Detail = listDoc
                        };
                    }
                    else
                    {
                        /*record not found*/
                        resRequest = new ResponseRequest()
                        {
                            Status = HttpStatusCode.OK.ToString(),
                            StatusCode = (int)HttpStatusCode.OK,
                            Message = "Record not found",
                            Detail = null
                        };
                    }
                }
                else
                {
                    /*get data error*/
                    resRequest = new ResponseRequest() { Status = "ERROR", StatusCode = (int)HttpStatusCode.InternalServerError, Message = "Getting data error", Detail = null };

                }
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("DocumentController", "", ex);
                resRequest = new ResponseRequest() { Status = "ERROR", StatusCode = (int)HttpStatusCode.InternalServerError, Message = "Unexpected error", Detail = null };
            }
            return resRequest;
        }

        [Route("api/Document/GetRegistrationDoc")]
        [HttpPost]
        public Object GetRegistrationDoucumentList(GetRegistrationDoucumentListPara reqObj)
        {
            ResponseRequest resRequest = new ResponseRequest();
            try
            {
                if (reqObj == null)
                {
                    resRequest = new ResponseRequest()
                    {
                        Status = HttpStatusCode.BadRequest.ToString(),
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = "Parameter is missing",
                        Detail = null
                    };
                    return resRequest;
                }
                else
                {
                    if (!Helper.IsDate(reqObj.DateFrom) || reqObj.DateFrom == null)
                    {
                        resRequest = new ResponseRequest()
                        {
                            Status = HttpStatusCode.BadRequest.ToString(),
                            StatusCode = (int)HttpStatusCode.BadRequest,
                            Message = "Parameter is missing",
                            Detail = "Date From is required as date [DD-MM-YYYY]."
                        };
                        return resRequest;
                    }
                    else if (!Helper.IsDate(reqObj.DateTo) || reqObj.DateTo == null)
                    {
                        resRequest = new ResponseRequest()
                        {
                            Status = HttpStatusCode.BadRequest.ToString(),
                            StatusCode = (int)HttpStatusCode.BadRequest,
                            Message = "Parameter is missing",
                            Detail = "Date To is required as date [DD-MM-YYYY]."
                        };
                        return resRequest;
                    }
                }

                List<documents.TrascationFiles> listDoc = documents.TrascationFiles.GetDocList(Helper.FormatDateTime(reqObj.DateFrom), Helper.FormatDateTime(reqObj.DateTo), reqObj.FileDescription);
                if (string.IsNullOrEmpty(documents.TrascationFiles.ErrorMessage))
                {
                    if (listDoc.Count == 0)
                    {

                        /*record not found*/
                        resRequest = new ResponseRequest()
                        {
                            Status = HttpStatusCode.OK.ToString(),
                            StatusCode = (int)HttpStatusCode.OK,
                            Message = "Record not found",
                            Detail = null
                        };
                    }
                    else
                    {
                        resRequest = new ResponseRequest()
                        {
                            Status = HttpStatusCode.OK.ToString(),
                            StatusCode = (int)HttpStatusCode.OK,
                            Message = "[ " + listDoc.Count + " ] recode(s) found.",
                            Detail = listDoc
                        };
                    }
                }
                else
                {
                    /*get data error*/
                    resRequest = new ResponseRequest() { Status = "ERROR", StatusCode = (int)HttpStatusCode.InternalServerError, Message = "Getting data error", Detail = null };

                }
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("DocumentController", "", ex);
                resRequest = new ResponseRequest() { Status = "ERROR", StatusCode = (int)HttpStatusCode.InternalServerError, Message = "Unexpected error", Detail = null };
            }
            return resRequest;
        }

        private byte[] CreatedPdf(string[] bmpFilePaths)
        {
            using (var ms = new MemoryStream())
            {
                var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 0, 0, 0, 0);
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, ms).SetFullCompression();
                document.Open();
                foreach (var path in bmpFilePaths)
                {
                    var imgStream = GetImageStream1(path);
                    var image = iTextSharp.text.Image.GetInstance(imgStream);

                    image.ScaleToFit(document.PageSize.Width, document.PageSize.Height);


                    document.Add(image);
                }
                document.Close();
                return ms.ToArray();
            }
        }

        private const int ExifOrientationId = 0x112; // = 274
        /// <summary>
        /// Gets the image at the specified path, shrinks it, converts to JPG, and returns as a stream
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        private Stream GetImageStream(string imagePath)
        {
             Bitmap bitmap = new Bitmap(imagePath);

            if (bitmap.PropertyIdList.Contains(ExifOrientationId))
            {
                var prop = bitmap.GetPropertyItem(ExifOrientationId);
                int orientation = BitConverter.ToUInt16(prop.Value, 0);
                RotateFlipType transform = RotateFlipType.RotateNoneFlipNone;

                switch (orientation)
                {
                    case 2:
                        transform = RotateFlipType.RotateNoneFlipX;
                        break;
                    case 3:
                        transform = RotateFlipType.Rotate180FlipNone;
                        break;
                    case 4:
                        transform = RotateFlipType.Rotate180FlipX;
                        break;
                    case 5:
                        transform = RotateFlipType.Rotate90FlipX;
                        break;
                    case 6:
                        transform = RotateFlipType.Rotate90FlipNone;
                        break;
                    case 7:
                        transform = RotateFlipType.Rotate270FlipX;
                        break;
                    case 8:
                        transform = RotateFlipType.Rotate270FlipNone;
                        break;
                }

                if (transform != RotateFlipType.RotateNoneFlipNone)
                {
                    bitmap.RotateFlip(transform);
                }

                try
                {
                    bitmap.RemovePropertyItem(ExifOrientationId);
                }
                catch { /* Some formats may not support removing — skip if needed */ }
            }

            var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Jpeg);
            ms.Position = 0;
            return ms;
        }
        private Stream GetImageStream1(string path)
        {
            Bitmap bitmap = new Bitmap(path);
            if (((IEnumerable<int>)bitmap.PropertyIdList).Contains<int>(274))
            {
                int num = (int)bitmap.GetPropertyItem(274).Value[0];
                RotateFlipType rotateFlipType = RotateFlipType.RotateNoneFlipNone;
                switch (num)
                {
                    case 3:
                        rotateFlipType = RotateFlipType.Rotate180FlipNone;
                        break;
                    case 6:
                        rotateFlipType = RotateFlipType.Rotate90FlipNone;
                        break;
                    case 8:
                        rotateFlipType = RotateFlipType.Rotate270FlipNone;
                        break;
                }
                bitmap.RotateFlip(rotateFlipType);
            }
            MemoryStream imageStream1 = new MemoryStream();
            bitmap.Save((Stream)imageStream1, ImageFormat.Jpeg);
            imageStream1.Position = 0L;
            bitmap.Dispose();
            return (Stream)imageStream1;
        }
        private MemoryStream MergePdfForms(List<byte[]> files)
        {
            if (files.Count > 1)
            {
                MemoryStream os = new MemoryStream();
                PdfReader reader = new PdfReader(files[0]);
                Document document = new Document();
                PdfWriter pdfWriter = (PdfWriter)new PdfSmartCopy(document, (Stream)os);
                document.Open();
                for (int index = 0; index < files.Count; ++index)
                {
                    reader = new PdfReader(files[index]);
                    for (int pageNumber = 1; pageNumber < reader.NumberOfPages + 1; ++pageNumber)
                        ((PdfCopy)pdfWriter).AddPage(pdfWriter.GetImportedPage(reader, pageNumber));
                    pdfWriter.FreeReader(reader);
                }
                reader.Close();
                pdfWriter.Close();
                document.Close();
                return os;
            }
            return files.Count == 1 ? new MemoryStream(files[0]) : (MemoryStream)null;
        }
    }

    public class ResponseRequest
    {
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Detail { get; set; }
    }

    public class ResponseFile
    {
        public string Ext { get; set; }
        public byte[] File { get; set; }

    }

    public class GetDoucumentListPara
    {
        public string ApplicationId { get; set; }
        public List<string> ChannelLocationId { get; set; }
    }
    public class UploadTransactionFilesPara
    {
        public string UserName { get; set; }
        /// <summary>
        /// Folder Name refer to project or business remarks
        /// </summary>
        public string FolderName { get; set; }
    }
    public class GetRegistrationDoucumentListPara
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string FileDescription { get; set; }
    }


}
