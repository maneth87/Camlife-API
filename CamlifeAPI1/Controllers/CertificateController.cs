using CamlifeAPI1.Class;
using CamlifeAPI1.Class.Application;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using NPOI.DDF;
using NPOI.SS.Formula.Functions;
using NPOI.Util;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Crypto.Engines;
using RestSharp.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations.Model;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.UI.WebControls;
using WebGrease.Css.Visitor;
using static CamlifeAPI1.Controllers.ApplicationFormController;
using static System.Net.Mime.MediaTypeNames;

namespace CamlifeAPI1.Controllers
{
    [Authorize]
    public class CertificateController : ApiController
    {
        [Route("api/Certificate/GetCertificate")]
        [HttpGet]
        public  object  GetCertificate(string policyId, string policyType)
        {
           
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, AppConfiguration.GetCertificateGenerateUrl()+ "?policyId="+ policyId+ "&policyType="+ policyType);
                var response =   client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();
               var str = ( response.Content.ReadAsStringAsync().Result);
                ResponeCertificate obj  = JsonConvert.DeserializeObject<ResponeCertificate>(str);
               
                obj.Status = response.StatusCode.ToString(); //status string
                obj.StatusCode = (int)response.StatusCode;// status code as number
                return obj;
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("CertificateController", "GetCertificate(string policyId, string policyType)", ex);
                return new ResponeCertificate() { Status="ERROR", StatusCode=0, Message="Unexpected error.", Certificate=null };
            }
        }

        [Route("api/Certificate/GetCertificates")]
        [HttpPost]
        public object GetCertificates(RequestCertificates req)
        {

            try
            {
                string polId = string.Join(",", req.PolicyId);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, AppConfiguration.GetCertificateGenerateUrl() + "?policyId=" + polId + "&policyType=" + req.PolicyType + "&printPolInsurance="+ (req.PrintPolicyInsurance==true ? "Y":"N"));
                var response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();
                var str = (response.Content.ReadAsStringAsync().Result);
                ResponeCertificate obj = JsonConvert.DeserializeObject<ResponeCertificate>(str);

                obj.Status = response.StatusCode.ToString(); //status string
                obj.StatusCode = (int)response.StatusCode;// status code as number
                return obj;
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("CertificateController", "GetCertificates(RequestCertificates req)", ex);
                return new ResponeCertificate() { Status = "ERROR", StatusCode = 0, Message = "Unexpected error.", Certificate = null };
            }
        }
        [Route("api/Certificate/GetApplicationForm")]
        [HttpGet]
        public object GetApplicationForm(string applicationId, string applicationType)
        {

            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, AppConfiguration.GetApplicationFormGenerateUrl() + "?applicationId=" + applicationId + "&applicationType=" + applicationType);
                var response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();
                var str = (response.Content.ReadAsStringAsync().Result);
                ResponeApplicationForm obj = JsonConvert.DeserializeObject<ResponeApplicationForm>(str);

                obj.Status = response.StatusCode.ToString(); //status string
                obj.StatusCode = (int)response.StatusCode;// status code as number
                return obj;
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("CertificateController", "GetApplicationForm(string applicationId, string applicationType)", ex);
                return new ResponeApplicationForm() { Status = "ERROR", StatusCode = 0, Message = "Unexpected error.", ApplicationForm = null };
            }
        }


        [Route("api/Certificate/GetApplicationForms")]
        [HttpPost]
        public object GetApplicationForms(RequestApplicationForms req)
        {
            try
            {
                ResponeApplicationForm obj = null;
                List<byte[]> files = new List<byte[]>();

                if (req != null)
                {
                    string appNum = string.Join(",", req.ApplicationNumber);
                    var list = da_micro_application.GetApplicationFilter(appNum, req.OnlyFirstYear);
                    if (da_micro_application.SUCCESS)
                    {
                        string appId = string.Join(",", list.ConvertAll( _ => _.ApplicationId));
                        var client = new HttpClient();
                        var request = new HttpRequestMessage(HttpMethod.Get, AppConfiguration.GetApplicationFormGenerateUrl() + "?applicationId=" + appId + "&applicationType=" + req.ApplicationType);
                        var response = client.SendAsync(request).Result;
                        response.EnsureSuccessStatusCode();
                        var str = (response.Content.ReadAsStringAsync().Result);
                        obj = JsonConvert.DeserializeObject<ResponeApplicationForm>(str);

                        files.Add(obj.ApplicationForm);

                        obj.Status = response.StatusCode.ToString(); //status string
                        obj.StatusCode = (int)response.StatusCode;// status code as number
                        return obj;
                    }
                    else
                    {
                        return new ResponseExecuteError() { code = new ErrorCode().ParameterNotSuppliedCode, message = da_micro_application.MESSAGE };
                    }
                }
                else
                {
                    return new ResponseExecuteError() { code = new ErrorCode().ValidationErrorCode, message = "Record(s) found : " };
                }



                
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("CertificateController", "GetApplicationForms(RequestApplicationForms req)", ex);
                return new ResponeApplicationForm() { Status = "ERROR", StatusCode = 0, Message = "Unexpected error.", ApplicationForm = null };
            }
        }
        public class ResponeCertificate
        {
            public int StatusCode { get; set; }
            public string Status { get; set; }
            public byte[] Certificate { get; set; }
            public string Message { get; set; }
        }

        public class ResponeApplicationForm
        {
            public int StatusCode { get; set; }
            public string Status { get; set; }
            public byte[] ApplicationForm { get; set; }
            public string Message { get; set; }
        }

        public class RequestApplicationForms
        {
           
            public string ApplicationType { get; set; }
            public List<string> ApplicationNumber { get; set; }
            public bool OnlyFirstYear { get; set; }
        }
        public class RequestCertificates
        {

            public string PolicyType { get; set; }
            public List<string> PolicyId { get; set; }
            public bool PrintPolicyInsurance { get; set; }
        }

        public MemoryStream MergePdfForms(List<byte[]> files)
        {
            if (files.Count > 1)
            {
                PdfReader pdfFile;
                Document doc;
                PdfWriter pCopy;
                MemoryStream msOutput = new MemoryStream();

                pdfFile = new PdfReader(files[0]);

                doc = new Document();
                pCopy = new PdfSmartCopy(doc, msOutput);

                doc.Open();

                for (int k = 0; k < files.Count; k++)
                {
                    pdfFile = new PdfReader(files[k]);
                    for (int i = 1; i < pdfFile.NumberOfPages + 1; i++)
                    {
                        ((PdfSmartCopy)pCopy).AddPage(pCopy.GetImportedPage(pdfFile, i));
                    }
                    pCopy.FreeReader(pdfFile);
                }

                pdfFile.Close();
                pCopy.Close();
                doc.Close();

                return msOutput;
            }
            else if (files.Count == 1)
            {
                return new MemoryStream(files[0]);
            }

            return null;
        }

    }



}
