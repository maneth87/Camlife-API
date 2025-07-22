using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Reflection;
using Org.BouncyCastle.Crypto.Engines;
using static System.Net.Mime.MediaTypeNames;
using iTextSharp.text.pdf.parser;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1;
using System.Web.Mvc;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Summary description for documents
/// </summary>
public class documents
{
    public documents()
    {
        //
        // TODO: Add constructor logic here
        //

    }
   
    public class PolicyContract
    {
        private static bool _createdFolder = false;
        private static string _errorMessage = "";
        private static string _path = "";
        private static string _fullPath = "";
        // private static string _mainPath = "";
        private static string _fileLocation = "";

        public static bool CreatedFolder { get { return _createdFolder; } }
        public static string ErrorMessage { get { return _errorMessage; } }
        /// <summary>
        /// Directory path , sub directory YYYY/MM/DD
        /// </summary>

        public static string Path { get { return _path; } }
        /// <summary>
        /// Full directory path, main folder combind with sub folder 
        /// </summary>
        public static string FullPath { get { return _fullPath; } }
        public static string MainPath
        {
            get
            {
                return GetMainPath();
            }
        }
        private static string GetMainPath()
        {
            //if (FileLocation.ToUpper() == DocumentLocation.LOCAL.ToString())
            if (FileLocation.ToUpper() == bl_system.SYSTEM_SETTING.DOCUMENT.LOCATION_OPTION.OPTION.LOCAL)
            {
                return HttpContext.Current.Server.MapPath(AppConfiguration.GetUploadDocumentPath());

            }
            //else if (FileLocation.ToUpper() == DocumentLocation.REMOTE.ToString())
            else if (FileLocation.ToUpper() == bl_system.SYSTEM_SETTING.DOCUMENT.LOCATION_OPTION.OPTION.REMOTE)
            {
                return AppConfiguration.GetUploadDocumentPath();
            }
            else
            {
                return string.Empty;
            }
        }
        public static string FileLocation { get { return AppConfiguration.GetDocumentLocation(); } }
       // public enum DocumentLocation { LOCAL, REMOTE }

        public string DocID { get; set; }
        public int Seq { get; set; }
        public string ApplicationID { get; set; }
        public string DocCode { get; set; }
        public string DocName { get; set; }
        public string DocType { get; set; }
        public string DocSize { get; set; }
        public string DocPath { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedRemark { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public string UpdateadRemarks { get; set; }
        public string ReviewedStatus { get; set; }
        public DateTime ReviewedOn { get; set; }
        public string ReviewedBy { get; set; }
        public string ReviewedRamarks { get; set; }
        public string FileDesc { get; set; }

        public PolicyContract()
        {


        }

        public static void CreateFolder()
        {
            string yFolder = "";// DateTime.Now.Year.ToString();
            string mFolder = "";//DateTime.Now.Month.ToString("00");
            string dFolder = "";//DateTime.Now.Day.ToString("00");
            string path = "";
            string fullPath = "";
            try
            {
                
                _fileLocation = FileLocation;
                DateTime dt = DateTime.Now;
                yFolder = dt.Year.ToString();
                mFolder = dt.Month.ToString("00");
                dFolder = dt.Day.ToString("00");
                path = yFolder + "\\" + mFolder + "\\" + dFolder;

                fullPath = MainPath + path;
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                    _path = path;
                    _fullPath = fullPath;
                    _createdFolder = true;
                    _errorMessage = string.Empty;

                }
                else
                {
                    _path = path;
                    _fullPath = fullPath;
                    _createdFolder = true;
                    _errorMessage = string.Empty;
                }


            }
            catch (Exception ex)
            {
                _createdFolder = false;
                _errorMessage = ex.Message;
                Log.AddExceptionToLog("document", MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        /// <summary>
        /// Return full path of file
        /// </summary>
        /// <param name="file_path">path file</param>
        /// <returns></returns>
        public static string GetFullPathFile(string file_path)
        {
           // if (AppConfiguration.GetDocumentLocation().ToUpper() == DocumentLocation.LOCAL.ToString())
                if (AppConfiguration.GetDocumentLocation().ToUpper() == bl_system.SYSTEM_SETTING.DOCUMENT.LOCATION_OPTION.OPTION.LOCAL)
            {
                return HttpContext.Current.Server.MapPath(AppConfiguration.GetUploadDocumentPath() + file_path);
            }
           // else if (AppConfiguration.GetDocumentLocation().ToUpper() == DocumentLocation.REMOTE.ToString())
             else       if (AppConfiguration.GetDocumentLocation().ToUpper() == bl_system.SYSTEM_SETTING.DOCUMENT.LOCATION_OPTION.OPTION.REMOTE)
            {
                return AppConfiguration.GetUploadDocumentPath() + file_path;
            }
            else
            {
                return "";
            }
        }

        public static string GetExtension(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                // return System.IO.Path .GetExtension(filePath);
                System.IO.FileInfo fInfo = new FileInfo(filePath);
                return fInfo.Extension;
            }
            else
            {
                return string.Empty;
            }
        }
        public static double GetFileSizes(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                System.IO.FileInfo fInfo = new FileInfo(filePath);
                return fInfo.Length;
            }
            else
            {
                return 0;
            }
        }

        public static bool IsCorrectFileType(string filePath)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(filePath))
            {
                System.IO.FileInfo fInfo = new FileInfo(filePath);
             List<  string> fType = AppConfiguration.GetDocumentType();
               
                if (fType.Count>0)
                {
                    if (!fType.Contains(fInfo.Extension.ToUpper()))
                    {
                        _errorMessage = result == false ? "File type [" + fInfo.Extension + "] is not supported. Please select file type [" + fType + "]" : string.Empty;
                        return result;
                    }
                    else
                    {
                        return  true;

                    }
                }
                else
                {
                    _errorMessage = "File type configuration is missing.";
                    return false;
                }
            }
            else
            {
                _errorMessage = "System cannot find file path.";
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileInfo">full file path or size of file as byte</param>
        /// <param name="isFile">Set true if fileInof is the full path of file, set False if fileInfo is size of file, size is byte</param>
        /// <returns></returns>
        public static bool IsCorrectFileSize(object fileInfo, bool isFile)
        {
            double fSize = 0;
            try
            {
                double fConfigSize = AppConfiguration.GetDocumentSize();

                if (isFile)
                {
                    fSize = GetFileSizes(fileInfo.ToString());
                }
                else
                {
                    fSize = Convert.ToDouble(fileInfo);
                }


                if (fSize > 0 && fSize <= fConfigSize)
                {
                    _errorMessage = string.Empty;
                    return true;
                }
                else
                {
                    _errorMessage = "File size " + fSize + " bytes is over limited. File size is limited " + fConfigSize + " bytes.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

        }

        public static bool InsertData(Int32 SEQ, string app_id, string doc_code, string Doc_name, string Doc_type, string doc_size, string Doc_path, DateTime Created_on, string Created_by, string created_remarks = "")
        {
            bool result = false;
            try
            {

                DB db = new DB();
                result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POL_CONTRACT_DOC_INSERT", new string[,] { 
                {"@SEQ",SEQ+""},
                {"@APPLICATION_ID",app_id},
                {"@DOC_CODE", doc_code},
                {"@DOC_NAME", Doc_name},
                {"@DOC_SIZE", doc_size},
                {"@DOC_TYPE", Doc_type},
                {"@DOC_PATH", Doc_path},
                {"@CREATED_ON",Created_on +""},
                {"@CREATED_BY", Created_by},
               {"@CREATED_REMARKS",created_remarks},
               {"@FILE_DESC",""}
                }, "banca_document_form_upload=>InsertData(Int32 SEQ,string app_id, string doc_code, string Doc_name, string Doc_type, string doc_size, string Doc_path, DateTime Created_on, string Created_by, string created_remarks = [Optional])");
                result = true;
                if (result)
                {
                    _errorMessage = string.Empty;
                }
                else
                {
                    _errorMessage = db.Message;
                }

            }

            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }


            return result;
        }

        public static bool InsertData(Int32 SEQ, string app_id, string doc_code, string Doc_name, string Doc_type, string doc_size, string Doc_path, DateTime Created_on, string Created_by, string fileDesc, string created_remarks = "")
        {
            bool result = false;
            try
            {

                DB db = new DB();
                result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POL_CONTRACT_DOC_INSERT", new string[,] {
                {"@SEQ",SEQ+""},
                {"@APPLICATION_ID",app_id},
                {"@DOC_CODE", doc_code},
                {"@DOC_NAME", Doc_name},
                {"@DOC_SIZE", doc_size},
                {"@DOC_TYPE", Doc_type},
                {"@DOC_PATH", Doc_path},
                {"@CREATED_ON",Created_on +""},
                {"@CREATED_BY", Created_by},
               {"@CREATED_REMARKS",created_remarks},
                {"@FILE_DESC",fileDesc}
                }, "banca_document_form_upload=>InsertData(Int32 SEQ,string app_id, string doc_code, string Doc_name, string Doc_type, string doc_size, string Doc_path, DateTime Created_on, string Created_by, string fileDesc, string created_remarks = [Optional])");
                result = true;
                if (result)
                {
                    _errorMessage = string.Empty;
                }
                else
                {
                    _errorMessage = db.Message;
                }

            }

            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }


            return result;
        }


        public static bool InsertData(documents.PolicyContract doc)
        {
            bool result = false;
            try
            {

                DB db = new DB();
                result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POL_CONTRACT_DOC_INSERT", new string[,] { 
                {"@SEQ",doc.Seq+""},
                {"@APPLICATION_ID",doc.ApplicationID},
                {"@DOC_CODE", doc. DocCode},
                {"@DOC_NAME", doc.DocName},
                {"@DOC_SIZE", doc.DocSize},
                {"@DOC_TYPE", doc.DocType},
                {"@DOC_PATH", doc.DocPath},
                {"@CREATED_ON",doc.CreatedOn +""},
                {"@CREATED_BY", doc.CreatedBy},
               {"@CREATED_REMARKS",doc.CreatedRemark},
                {"@FILE_DESC",doc.FileDesc}
                }, "banca_document_form_upload=>InsertData(documents.PolicyContract doc)");
                result = true;
                if (result)
                {
                    _errorMessage = string.Empty;
                }
                else
                {
                    _errorMessage = db.Message;
                }

            }

            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }


            return result;
        }
        /// <summary>
        /// Get document list by application id
        /// </summary>
        /// <param name="Application_ID"></param>
        /// <returns></returns>
        public static List<documents.PolicyContract> GetDocumentList(string Application_ID)
        {
            List<documents.PolicyContract> ListDoc = new List<PolicyContract>();
            try
            {
                DataTable tbl = new DataTable();
                DB db = new DB();

                tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POL_CONTRACT_DOC_GET_BY_APPID", new string[,]{
        {"@APPLICATION_ID",Application_ID},    
        }, "banca_document_form_upload=>CheckExistFile(string application_number)");

                if (tbl.Rows.Count > 0)
                {
                    foreach (DataRow r in tbl.Rows)
                    {
                        ListDoc.Add(new documents.PolicyContract()
                        {

                            Seq = Convert.ToInt32(r["seq"].ToString()),
                            DocID = r["DOC_ID"].ToString(),
                            ApplicationID = r["Application_id"].ToString(),
                            DocCode = r["DOC_CODE"].ToString(),
                            DocName = r["DOC_NAME"].ToString(),
                            DocType = r["DOC_TYPE"].ToString(),
                            DocSize = r["DOC_SIZE"].ToString(),
                            DocPath = r["DOC_PATH"].ToString(),
                            CreatedBy = r["CREATED_BY"].ToString(),
                            CreatedOn = Convert.ToDateTime(r["CREATED_ON"].ToString()),
                            CreatedRemark = r["CREATED_REMARKS"].ToString(),
                            ReviewedStatus = r["REVIEWED_STATUS"].ToString(),
                            ReviewedBy = r["reviewed_by"].ToString(),
                            ReviewedOn = Convert.ToDateTime(r["reviewed_on"].ToString()),
                            ReviewedRamarks = r["REVIEWED_REMARK"].ToString()
                        });
                    }
                }
                _errorMessage = string.Empty;
            }

            catch (Exception ex)
            {
                ListDoc = new List<PolicyContract>();
                _errorMessage = ex.Message;
                Log.AddExceptionToLog("document", MethodBase.GetCurrentMethod().Name, ex);
            }
            return ListDoc;
        }
        /// <summary>
        /// Get Document list by application id and channel location id
        /// </summary>
        /// <param name="applicaitonId"></param>
        /// <param name="channelLocationId"></param>
        /// <returns></returns>
        public static List<documents.PolicyContract> GetDocumentList(string applicaitonId, string channelLocationId)
        {
            List<documents.PolicyContract> ListDoc = new List<PolicyContract>();
            try
            {
                DataTable tbl = new DataTable();
                DB db = new DB();

                tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POL_CONTRACT_DOC_GET_BY_APPID_CHANNEL_LOCATION_ID", new string[,]{
        {"@APPLICATION_ID",applicaitonId},{ "@channel_location_id", channelLocationId},
        }, "documents.PolicyContract=>GetDocumentList(string applicaitonId, string channelLocationId)");

                if (tbl.Rows.Count > 0)
                {
                    foreach (DataRow r in tbl.Rows)
                    {
                        ListDoc.Add(new documents.PolicyContract()
                        {

                            Seq = Convert.ToInt32(r["seq"].ToString()),
                            DocID = r["DOC_ID"].ToString(),
                            ApplicationID = r["Application_id"].ToString(),
                            DocCode = r["DOC_CODE"].ToString(),
                            DocName = r["DOC_NAME"].ToString(),
                            DocType = r["DOC_TYPE"].ToString(),
                            DocSize = r["DOC_SIZE"].ToString(),
                            DocPath = r["DOC_PATH"].ToString(),
                            CreatedBy = r["CREATED_BY"].ToString(),
                            CreatedOn = Convert.ToDateTime(r["CREATED_ON"].ToString()),
                            CreatedRemark = r["CREATED_REMARKS"].ToString(),
                            ReviewedStatus = r["REVIEWED_STATUS"].ToString(),
                            ReviewedBy = r["reviewed_by"].ToString(),
                            ReviewedOn = Convert.ToDateTime(r["reviewed_on"].ToString()),
                            ReviewedRamarks = r["REVIEWED_REMARK"].ToString()
                        });
                    }
                }
                _errorMessage = string.Empty;
            }

            catch (Exception ex)
            {
                ListDoc = new List<PolicyContract>();
                _errorMessage = ex.Message;
                Log.AddExceptionToLog("document.PolicyContract", MethodBase.GetCurrentMethod().Name, ex);
            }
            return ListDoc;
        }
        //BIND REVIEWED STATUS
        public static List<documents.PolicyContract> Bindreviewstatus(string doc_id)
        {
            List<documents.PolicyContract> list = new List<PolicyContract>();
            try
            {
                DataTable tbl = new DataTable();
                DB db = new DB();

                tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POL_CONTRACT_DO_BIND_STATUS", new string[,]{
        {"@DOC_ID",doc_id},    
        }, "banca_view_upload_document_form=>Bindreviewstatus(string doc_id)");

                if (tbl.Rows.Count > 0)
                {
                    _errorMessage = string.Empty;
                }
                else
                {
                    _errorMessage = db.Message;
                }

            }

            catch (Exception ex)
            {
                list = new List<PolicyContract>();
                _errorMessage = ex.Message;
                Log.AddExceptionToLog("document", MethodBase.GetCurrentMethod().Name, ex);
            }
            return list;
        }
        public static DataTable GetDocName(string App_ID)
        {
            DataTable tbl = new DataTable();

            try
            {

                DB db = new DB();
                tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POL_CONTRACT_DOC_GET_DOC_NAME", new string[,] { 
                
                {"@APPLICATION_ID",App_ID}               
                }, "banca_view_upload_document_form=>GetDocName(string App_ID)");

                if (tbl.Rows.Count > 0)
                {
                    _errorMessage = string.Empty;
                }


            }

            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }


            return tbl;
        }
        //Block update review status by kehong
        public static bool UpdateReviewedStatus(string doc_id, string reviewed_status, string reviewed_by, DateTime reviewed_on, string reviewed_remarks = "")
        {
            bool result = false;
            try
            {

                DB db = new DB();
                result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POL_CONTRACT_DOC_UPDATE_STATUS", new string[,]{
            {"@REVIEWED_STATUS",reviewed_status},
            {"@REVIEWED_ON",reviewed_on+""},
            {"@REVIEWED_BY",reviewed_by},
            {"@REVIEWED_REMARK",reviewed_remarks},
            {"@DOC_ID",doc_id}
           }, "banca_view_upload_document_form=>UpdateReviewedStatus(string doc_id,string reviewed_status,string reviewed_by,string reviewed_on,string reviewed_remarks='')");

                if (result)
                {
                    _errorMessage = string.Empty;
                }
                else
                {
                    _errorMessage = db.Message;
                }

            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                Log.AddExceptionToLog("document", MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;

        }
        public static bool UpdateDocument(string doc_id, string doc_name, string doc_size, string doc_path, DateTime updated_on, string updated_by, string updated_remarks = "")
        {
            bool result = false;
            try
            {

                DB db = new DB();
                result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POL_CONTRACT_UPDATE_DOC", new string[,] { 
                {"@DOC_ID",doc_id},
                {"@DOC_NAME", doc_name},
                {"@DOC_SIZE", doc_size},
                {"@DOC_PATH", doc_path},
                {"@UPDATED_ON",updated_on +""},
                {"@UPDATED_BY", updated_by},
               {"@UPDATED_REMARKS",updated_remarks}, {"@FILE_DESC",""}
                }, "banca_document_form_upload=>UpdateDocument(string doc_id, string doc_name, string doc_size, string doc_path, DateTime updated_on, string updated_by, string updated_remarks = '')");
                result = true;
                if (result)
                {
                    _errorMessage = string.Empty;
                }
                else
                {
                    _errorMessage = db.Message;
                }

            }

            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                Log.AddExceptionToLog("document", MethodBase.GetCurrentMethod().Name, ex);
            }


            return result;
        }
        public static bool UpdateDocument(string doc_id, string doc_name, string doc_size, string doc_path, DateTime updated_on, string updated_by, string fileDesc, string updated_remarks = "")
        {
            bool result = false;
            try
            {

                DB db = new DB();
                result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POL_CONTRACT_UPDATE_DOC", new string[,] {
                {"@DOC_ID",doc_id},
                {"@DOC_NAME", doc_name},
                {"@DOC_SIZE", doc_size},
                {"@DOC_PATH", doc_path},
                {"@UPDATED_ON",updated_on +""},
                {"@UPDATED_BY", updated_by},
               {"@UPDATED_REMARKS",updated_remarks},
                {"@FILE_DESC",fileDesc}
                }, "banca_document_form_upload=>UpdateDocument(string doc_id, string doc_name, string doc_size, string doc_path, DateTime updated_on, string updated_by, string fileDesc, string updated_remarks = '')");
                result = true;
                if (result)
                {
                    _errorMessage = string.Empty;
                }
                else
                {
                    _errorMessage = db.Message;
                }

            }

            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                Log.AddExceptionToLog("document", MethodBase.GetCurrentMethod().Name, ex);
            }


            return result;
        }
        [Serializable]
        public class Preview
        {
            public Preview() { }
            public Preview(Int32 seq, string docCode, string docType, string docSize, string originalDocname, string docName, string docPath)
            {
                Seq = seq;
                DocumentCode = docCode;
                DocumentType = docType;
                DocumentSize = docSize;
                DocumentName = docName;
                OriginalDocumentName = originalDocname;
                DocumentPath = docPath;
            }
            public Int32 Seq { get; set; }
            public string DocumentCode { get; set; }
            public string DocumentType { get; set; }
            public string DocumentSize { get; set; }

            /// <summary>
            /// Original file name which selected by user
            /// </summary>
            public string OriginalDocumentName { get; set; }
            /// <summary>
            /// New file name which renamed by system
            /// </summary>
            public string DocumentName { get; set; }
            public string DocumentPath { get; set; }
        }
    }

    /// <summary>
    /// Any file upload in to system
    /// </summary>
    public class TrascationFiles
    {
        private static readonly string ApplicationID = "5302F619-CB71-4C30-A244-E3EE152975C6";/*this application id is camlife application name*/
      

        private static string _errorMessage = "";
        public static string ErrorMessage { get { return _errorMessage; } }

        public string Id { get; set; }
        public string DocName { get; set; }
        public string DocPath { get; set; }
        public string DocDescription { get; set; }
        public string Remarks { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadedOn { get; set; }

        /// <summary>
        /// return Path to store file
        /// </summary>
        public  static string Path { get { return _path; } }
        public static string MainPath { get { return GenerateMainPath(); } }

        private static string _path = "";

        private static string GenerateMainPath()
        {
            if (documents.PolicyContract.FileLocation.ToUpper() == bl_system.SYSTEM_SETTING.DOCUMENT.LOCATION_OPTION.OPTION.LOCAL)
            {
                return HttpContext.Current.Server.MapPath(AppConfiguration.TransactionFilesPath());

            }
            else if (documents.PolicyContract.FileLocation.ToUpper() == bl_system.SYSTEM_SETTING.DOCUMENT.LOCATION_OPTION.OPTION.REMOTE)
            {
                return AppConfiguration.TransactionFilesPath();
            }
            else
            {
                return string.Empty;
            }
        }

       
        /// <summary>
        /// Return full path after create subfolder
        /// </summary>
        /// <param name="subFolderName"></param>
        /// <returns></returns>
        public static string CreateSubFolder(string subFolderName)
        {
            string mainFolder = MainPath;
            string fullPath = "";
            try
            {
                if (mainFolder != "")
                {
                    DateTime dt = DateTime.Now;
                    string yFolder = dt.Year.ToString();
                    string mFolder = dt.Month.ToString("00");
                    string dFolder = dt.Day.ToString("00");
                    string subPath = yFolder + "\\" + mFolder + "\\" + dFolder;
                    fullPath = mainFolder + subFolderName + "\\" + subPath;

                    _path= subFolderName + "\\" + subPath;

                    if (!Directory.Exists(fullPath))
                    {
                        Directory.CreateDirectory(fullPath);
                    }
                    fullPath += "/";
                }
            }
            catch (Exception ex)
            {
                fullPath = "";
                Log.AddExceptionToLog("document.TrascationFiles", "CreateSubFolder(string subFolderName)", ex);
            }
            return fullPath;
        }
        public static List<string> AllowDocumentType()
        {
            List<string> lType = new List<string>();
            try
            {
                bl_system.SYSTEM_PARAMATER sysObj = new bl_system.SYSTEM_PARAMATER();
                sysObj = sysObj.GetParamater(bl_system.SYSTEM_SETTING.DOCUMENT.REGISTER_DOCUMENT_TYPE, ApplicationID);
                lType = sysObj.ParamaterVal.ToUpper().Split(',').ToList();
                return lType;
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("TrascationFiles", MethodBase.GetCurrentMethod().Name, ex);
                return new List<string>();
            }
        }

        public static bool IsValidDocSizes(double docSize)
        {
            try
            {
                bl_system.SYSTEM_PARAMATER sysObj = new bl_system.SYSTEM_PARAMATER();
                sysObj = sysObj.GetParamater(bl_system.SYSTEM_SETTING.DOCUMENT.REGISTER_DOCUMENT_SIZE, ApplicationID);

                double allowSize = Convert.ToDouble( sysObj.ParamaterVal);
                if (docSize <= allowSize)
                {
                    return true;
                }
                else
                { 
                return false;
                }
              
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("TrascationFiles", MethodBase.GetCurrentMethod().Name, ex);
                return false;
            }
        }


        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPath">full parth include file name</param>
        /// <returns></returns>
        public static bool DeleteFile(string fullPath)
        {
            try
            {
                File.Delete(fullPath);
                return true;
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("document", MethodBase.GetCurrentMethod().Name, ex); return false;
            }
        }


        public static bool IsCorrectFileType(string filePath)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(filePath))
            {
                System.IO.FileInfo fInfo = new FileInfo(filePath);
                List<string> fType = AllowDocumentType();

                if (fType.Count > 0)
                {
                    if (!fType.Contains(fInfo.Extension.ToUpper()))
                    {
                        _errorMessage = result == false ? "File type [" + fInfo.Extension + "] is not supported. Please select file type [" + fType + "]" : string.Empty;
                        return result;
                    }
                    else
                    {
                        return true;

                    }
                }
                else
                {
                    _errorMessage = "File type configuration is missing.";
                    return false;
                }
            }
            else
            {
                _errorMessage = "System cannot find file path.";
                return false;
            }
        }

        public static bool SaveDoc(documents.TrascationFiles doc)
        {
            bool result = false;
            try
            {

                DB db = new DB();
                doc.Id = Helper.GetNewGuid(new string[,] { { "TABLE", "CT_DOCUMENTS_UPLOAD" }, { "FIELD", "ID" } });
                result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_DOCUMENTS_UPLOAD_INSERT", new string[,] {
               
                {"@ID",doc.Id},
                {"@DOC_NAME", doc.DocName},
                {"@DOC_PATH", doc.DocPath},
                {"@DOC_DESCRIPTION",doc.DocDescription},
                {"@REMARKS", doc.Remarks},
               {"@UPLOADED_BY",doc.UploadedBy},
               {"@UPLOADED_ON", doc.UploadedOn+"" }
                }, "documents.TrascationFiles=>SaveDoc(documents.TrascationFiles doc)") ; 
                result = true;
                if (result)
                {
                    _errorMessage = string.Empty;
                }
                else
                {
                    _errorMessage = db.Message;
                }

            }

            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                Log.AddExceptionToLog("document", MethodBase.GetCurrentMethod().Name, ex); 
                result= false;
            }


            return result;
        }

        public static List<documents.TrascationFiles> GetDocList(DateTime fDate, DateTime tDate, string FileDescription)
        {
            List<documents.TrascationFiles> lObj = new List<TrascationFiles>();
            try
            {
              
                DB db = new DB();
                DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_DOCUMENTS_UPLOAD_GET", new string[,] {

                {"@f_date", fDate+"" },
                { "@t_date",tDate+""},
                {"@DOC_DESCRIPTION",FileDescription}
                }, "documents.TrascationFiles=>GetDocList(DateTime fDate, DateTime tDate, string FileDescription)");
               
               foreach(DataRow r in tbl.Rows)
                {
                    lObj.Add(new TrascationFiles()
                    {
                        Id = r["id"].ToString(),
                        DocName = r["DOC_NAME"].ToString(),
                        DocDescription = r["DOC_DESCRIPTION"].ToString(),
                        DocPath = r["DOC_PATH"].ToString(),
                        UploadedOn = Convert.ToDateTime(r["UPLOADED_ON"].ToString()),
                        UploadedBy = r["UPLOADED_BY"].ToString()
                    }) ;
                }

            }

            catch (Exception ex)
            {
                _errorMessage = "Error";
                Log.AddExceptionToLog("document", MethodBase.GetCurrentMethod().Name, ex);
                lObj = null;
            }


            return lObj;
        }
    }
}