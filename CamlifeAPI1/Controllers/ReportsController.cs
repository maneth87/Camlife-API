using CamlifeAPI1.Class;
using CamlifeAPI1.Class.Reports;
using Microsoft.Ajax.Utilities;
using Microsoft.CodeAnalysis.CSharp;
using NPOI.HSSF.UserModel;
using NPOI.OpenXmlFormats.Dml.Diagram;
using NPOI.POIFS.Crypt.Dsig;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CamlifeAPI1.Controllers
{
    [Authorize]
    public class ReportsController : ApiController
    {
        [Route("api/Reports/SaleMicroReports")]
        [HttpPost]
        public object GetSaleMicroReports(GetSaleMicroReportsParam objRequest)
        {
            object result = null;

            try
            {
                List<ValidateRequest> lValidate = new List<ValidateRequest>();
                if (objRequest == null)
                {
                    lValidate.Add(new ValidateRequest() { field = "Request Object", message = "Object reference not set to an instance of an object." });
                }
                else
                {
                    Reports.DateOption enumValue;
                    if (!Reports.DateOption.TryParse(objRequest.DateOptions, out enumValue))
                    {
                        result = new ResponseRequest() { Status = "Validate Error", StatusCode = (int)HttpStatusCode.BadRequest, Detail = null, Message = "Unexpected error." };

                        lValidate.Add(new ValidateRequest() { field = "DateOptions", message = " Date Options is not correct." });

                    }
                    else { 
                        if(objRequest.DateOptions == null) {
                            lValidate.Add(new ValidateRequest() { field = "DateOptions", message = " Date Options is required." });
                        }
                    }
                    if (!Helper.IsDate(objRequest.FromDate))
                    {
                        lValidate.Add(new ValidateRequest() { field = "FromDate", message = "From date is required as date [DD-MM-YYYY]." });

                    }
                    if (!Helper.IsDate(objRequest.ToDate))
                    {
                        lValidate.Add(new ValidateRequest() { field = "ToDate", message = "To date is required as date [DD-MM-YYYY]." });

                    }
                    if (objRequest.ChannelLocationId.Count == 0)
                    {
                        lValidate.Add(new ValidateRequest() { field = "ChannelLocationId", message = "Channel location id is required." });
                    }
                }
                if (lValidate.Count == 0)
                {
                    var obj = Reports.GetSaleMicroReport(objRequest.DateOptions.ToUpper() == "APP_DATE" ? Reports.DateOption.APP_DATE : Reports.DateOption.EFF_DATE,
                        Helper.FormatDateTime(objRequest.FromDate), Helper.FormatDateTime(objRequest.ToDate), objRequest.ChannelLocationId);
                   
                    result = new ResponseRequest()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = obj.Count + " Record(s) found.",
                        Detail = obj.Count==0? null:obj
                    };
                }
                else
                {
                    result = new ResponseRequest()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Status = "Validation",
                        Message = "Validation error, The request has " + lValidate.Count + " errors.",
                        Detail = lValidate
                    };
                }
            }
            catch (Exception ex)
            {
                result = new ResponseRequest() { Status = "ERROR", StatusCode = (int)HttpStatusCode.InternalServerError, Detail = null, Message = "Unexpected error." };
                Log.AddExceptionToLog("ReportsController", "GetSaleMicroReports( GetSaleMicroReportsParam objRequest)", ex);
            }

            return result;
        }


        [Route("api/Reports/AppPendingIssuePolicy")]
        [HttpPost]
        public object GetAppPendingIssuePolicyReports(GetAppPendingIssuePolicyReportsParam objRequest)
        {
            object result = null;

            try
            {
                List<ValidateRequest> lValidate = new List<ValidateRequest>();
                if (objRequest == null)
                {
                    lValidate.Add(new ValidateRequest() { field = "Request Object", message = "Object reference not set to an instance of an object." });
                }
                else
                {
                   
                    if (!Helper.IsDate(objRequest.FromDate))
                    {
                        lValidate.Add(new ValidateRequest() { field = "FromDate", message = "From date is required as date [DD-MM-YYYY]." });

                    }
                    if (!Helper.IsDate(objRequest.ToDate))
                    {
                        lValidate.Add(new ValidateRequest() { field = "ToDate", message = "To date is required as date [DD-MM-YYYY]." });

                    }
                    if (objRequest.ChannelLocationId.Count == 0)
                    {
                        lValidate.Add(new ValidateRequest() { field = "ChannelLocationId", message = "Channel location id is required." });
                    }
                }
                if (lValidate.Count == 0)
                {
                    var obj = Reports.GetAppPendingIssuePolicy(Helper.FormatDateTime(objRequest.FromDate), Helper.FormatDateTime(objRequest.ToDate), objRequest.ChannelLocationId);

                    result = new ResponseRequest()
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = HttpStatusCode.OK.ToString(),
                        Message = obj.Count + " Record(s) found.",
                        Detail = obj.Count == 0 ? null : obj
                    };
                }
                else
                {
                    result = new ResponseRequest()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Status = "Validation",
                        Message = "Validation error, The request has " + lValidate.Count + " errors.",
                        Detail = lValidate
                    };
                }
            }
            catch (Exception ex)
            {
                result = new ResponseRequest() { Status = "ERROR", StatusCode = (int)HttpStatusCode.InternalServerError, Detail = null, Message = "Unexpected error." };
                Log.AddExceptionToLog("ReportsController", "GetAppPendingIssuePolicyReports(GetAppPendingIssuePolicyReportsParam objRequest)", ex);
            }

            return result;
        }


        [Route("api/Reports/SaleMicroReportsExcel")]
        [HttpPost]
        public object GetSaleMicroReportsExcel(GetSaleMicroReportsParam objRequest)
        { 
            object result = null;
            try
            {
                var obj = (ResponseRequest) GetSaleMicroReports(objRequest);
                if (obj.StatusCode == 200 && obj.Detail!=null)
                {
                    var lObj =(List<Reports.SaleMicro>) obj.Detail;

                    HSSFWorkbook hssfworkbook = new HSSFWorkbook();
                  //  Response.Clear();
                    HSSFSheet sheet1 = (HSSFSheet)hssfworkbook.CreateSheet("Sheet 1");
                    Helper.excel.Sheet = sheet1;
                    Helper.excel.Title = new string[] { "Invalide Record" };
                    Helper.excel.HeaderText = new string[] {"NO.","ApplicationDate","EffectiveDate","ApplicationNo","CertificateNo","Package","Premium" }; 
                    Helper.excel.generateHeader();

                    int row_no = 0;
                    row_no = Helper.excel.NewRowIndex - 1;
                 
                    foreach (Reports.SaleMicro r in lObj)
                    {

                        row_no += 1;
                        HSSFRow rowCell = (HSSFRow)sheet1.CreateRow(row_no);
                        HSSFCell Cell1 = (HSSFCell)rowCell.CreateCell(1);
                        Cell1.SetCellValue(row_no);

                        HSSFCell Cell2 = (HSSFCell)rowCell.CreateCell(2);
                        Cell2.SetCellValue(r.ApplicationDate.ToString("dd-MM-yyyy"));

                        HSSFCell Cell3 = (HSSFCell)rowCell.CreateCell(3);
                        Cell3.SetCellValue(r.EffectiveDate.ToString("dd-MM-yyyy"));

                        HSSFCell Cell4 = (HSSFCell)rowCell.CreateCell(4);
                        Cell4.SetCellValue(r.ApplicationNumber);

                        HSSFCell Cell5 = (HSSFCell)rowCell.CreateCell(5);
                        Cell5.SetCellValue(r.PolicyNumber);

                        HSSFCell Cell6 = (HSSFCell)rowCell.CreateCell(6);
                        Cell6.SetCellValue(r.Package);

                        HSSFCell Cell7 = (HSSFCell)rowCell.CreateCell(7);
                        Cell7.SetCellValue(r.Premium);

                    }
                    string fName =Guid.NewGuid().ToString();
                    string filename = fName + ".xls";
                    // Response.Clear();
                    //Response.ContentType = "application/vnd.ms-excel";
                    //Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
                    MemoryStream file = new MemoryStream();
                    hssfworkbook.Write(file);

                    byte[] data = file.ToArray();

                    //Response.BinaryWrite(file.GetBuffer());

                    //Response.End();

                    result = new ResponseRequest() {
                        StatusCode = (int)HttpStatusCode.OK, Status = HttpStatusCode.OK.ToString(), Message = "Success", Detail = data
                    };

                }
            }
            catch(Exception ex)
            {
                Log.AddExceptionToLog("ReportsController", "GetSaleMicroReportsExcel(GetSaleMicroReportsParam objRequest)", ex);
                result = new ResponseRequest()
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Unexpected error.",
                    Detail = null
                };

            }
            return result;
        }

        public class GetSaleMicroReportsParam
        {
            public string DateOptions { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public List<string> ChannelLocationId { get; set; }
        }
        public class GetAppPendingIssuePolicyReportsParam
        {
          /// <summary>
          /// Application Date From
          /// </summary>
            public string FromDate { get; set; }
            /// <summary>
            /// Application Date To
            /// </summary>
            public string ToDate { get; set; }
            public List<string> ChannelLocationId { get; set; }
        }
        public class ResponseRequest
        {
            public string Status { get; set; }
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public object Detail { get; set; }
        }
    }


}
