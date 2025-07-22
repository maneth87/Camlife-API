using CamlifeAPI1.Class;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using NPOI.DDF;
using NPOI.SS.Formula.Functions;
using NPOI.Util;
using RestSharp.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using static CamlifeAPI1.Controllers.MicroProductController;

namespace CamlifeAPI1.Controllers
{
    //[BasicAuthentication]
    [Authorize]
    public class ApplicationConsumerController : ApiController
    {
        public enum LeadStatus { Approved, Reject, Delete };
        public string Get()
        {
            return "Welcome to Cambodia Life Micro Insurance \"CAMLIFE\" PLC.";
        }

        #region Application Consumer

        [Route("api/applicationconsumer")]
        [HttpPost]
        public object consumer(ParaApplicationConsumer para)
        {
            object myObj = new object();
            //if (BasicAuthenticationAttribute.ValidLogin)
            //{
            //string InsuranceApplicationId = "";
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var userName = HttpContext.Current.User.Identity.Name;
            string ch = "";
            //if (userName == "hatthabank@product.com")
            //{
            //    ch = "791D3296-82D0-4F07-AC62-B5C358742E2B";
            //}
            List<ValidateRequest> vaList = new List<ValidateRequest>();
            ChannelItemUserConfig chUser = new ChannelItemUserConfig();
            ChannelItemUserConfig chObj = chUser.GetByUserName(userName);
            if (chUser.Transection)
            {
                if (string.IsNullOrEmpty(chObj.ChannelItemId))// the specific user is not yet configure in table [CT_MICRO_CHANNEL_ITEM_LINK_USER_CONFIG]
                {
                   
                    vaList.Add(new ValidateRequest() { field = "Channel", message = "Channel is not configured in camlife." });
                   
                }
                else
                {
                    ch=chObj.ChannelItemId;
                    if (para.InsuranceApplicationId.Trim() == "")
                    {
                        vaList.Add(new ValidateRequest() { field = "InsuranceApplicationId", message = "Insurance application id cannot be blank." });
                    }
                    else if (para.InsuranceApplicationId.Trim().Length > 11 || para.InsuranceApplicationId.Trim().Length < 10)
                    {
                        vaList.Add(new ValidateRequest() { field = "InsuranceApplicationId", message = "Size must be 11 degits." });

                    }
                    else if (para.InsuranceApplicationId.Trim().Substring(0, 3) != "APP")
                    {
                        vaList.Add(new ValidateRequest() { field = "InsuranceApplicationId", message = "Insurance application id must be in format APP{number 8 degits}." });
                    }
                    if (para.InsuranceApplicationId.Trim().Length == 10)
                    {
                        if (!Helper.IsNumeric(para.InsuranceApplicationId.Trim().Substring(3, 7)))
                        {
                            vaList.Add(new ValidateRequest() { field = "InsuranceApplicationId", message = "Insurance application id must be in format APP{number 8 degits}." });
                        }
                    }
                    else if (para.InsuranceApplicationId.Trim().Length == 11)
                    {
                        if (!Helper.IsNumeric(para.InsuranceApplicationId.Trim().Substring(3, 8)))
                        {
                            vaList.Add(new ValidateRequest() { field = "InsuranceApplicationId", message = "Insurance application id must be in format APP{number 8 degits}." });
                        }
                    }
                }
            }
            else// get channel item user config error
            {
                vaList.Add(new ValidateRequest() { field = "Channel", message = "Channel is not configured in camlife." });

            }

            if (vaList.Count == 0)
            {
                da_banca.ApplicationConsumer obj = new da_banca.ApplicationConsumer();
                obj = da_banca.ApplicationConsumer.GetApplicationConsumer(para.InsuranceApplicationId, ch);
                List<da_banca.ApplicationConsumer> objList = new List<da_banca.ApplicationConsumer>();
                objList.Add(obj);
                ErrorCode myErr = new ErrorCode();
                if (da_banca.SUCCESS)
                {
                    if (obj.InsuranceApplicationId == null)
                    {
                        objList = new List<da_banca.ApplicationConsumer>();
                    }

                    myObj = (new ResponseExecuteSuccess() { message = da_banca.MESSAGE, detail = objList });

                }
                else
                {
                    myObj = (new ResponseExecuteError() { message = da_banca.CODE == "0" ? myErr.UnexpectedError + da_banca.MESSAGE : da_banca.MESSAGE, code = da_banca.CODE == "0" ? myErr.UnexpectedErrorCode : da_banca.CODE });
                }

            }
            else
            {
                myObj = (new ResponseValidateError() { message = "Validation error, The request has " + vaList.Count + " errors.", detail = vaList });

            }
         
            return myObj;

        }
        #endregion

        [Route("api/sendlead")]
        [HttpPost]
        public object SendLead(ParaSendLead para)
        {
            object myObj = new object();
            string LEAD_ID = "";
            ResponseExecuteSuccess response = new ResponseExecuteSuccess();
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var userName = HttpContext.Current.User.Identity.Name;
            ValidateRequest validate = new ValidateRequest();
            List<ValidateRequest> vaList = new List<ValidateRequest>();
            string channelItemId = "";
            #region Validate
            if (para.BranchCode.Trim() == "")
            {
                validate = new ValidateRequest();
                validate.field = "BranchCode";
                validate.message = "Banch code cannot be blank.";

                vaList.Add(validate);
            }
            if (para.BranchName.Trim() == "")
            {
                validate = new ValidateRequest();
                validate.field = "BranchName";
                validate.message = "Banch name cannot be blank.";

                vaList.Add(validate);
            }
            if (para.ApplicationID == "")
            {
                validate = new ValidateRequest();
                validate.field = "ApplicationId";
                validate.message = "Application id cannot be blank.";

                vaList.Add(validate);
            }
            if (para.ReferralStaffId.Trim() == "")
            {
                validate = new ValidateRequest();
                validate.field = "ReferralStaffId";
                validate.message = "Referral staff id cannot be blank.";

                vaList.Add(validate);
            }
            else
            {
                //check referral in white list

                //if (userName == "hatthabank@product.com")
                //{
                //    channelItemId = "791D3296-82D0-4F07-AC62-B5C358742E2B";
                //}
                //List<bl_referral> refList = da_referral.GetActiveReferral(channelItemId, para.ReferralStaffId);
                //if (refList.Count == 0)
                //{
                //    validate = new Validate();
                //    validate.field = "ReferralStaffId";
                //    validate.message = "Referral staff id [" + para.ReferralStaffId + "] is not in camlife white list.";

                //    vaList.Add(validate);
                //}
                ChannelItemUserConfig chUser = new ChannelItemUserConfig();
                ChannelItemUserConfig chObj = chUser.GetByUserName(userName);
                if (chUser.Transection)
                {
                    if (string.IsNullOrEmpty(chObj.ChannelItemId))
                    {
                        validate = new ValidateRequest();
                        validate.field = "Channel";
                        validate.message = "Channel is not configured in camlife.";

                        vaList.Add(validate);
                    }
                    else// the specific user is not yet configure in table [CT_MICRO_CHANNEL_ITEM_LINK_USER_CONFIG]
                    {
                        channelItemId = chObj.ChannelItemId;
                        List<bl_referral> refList = da_referral.GetActiveReferral(channelItemId, para.ReferralStaffId);
                        if (refList.Count == 0)
                        {
                            validate = new ValidateRequest();
                            validate.field = "ReferralStaffId";
                            validate.message = "Referral staff id [" + para.ReferralStaffId + "] is not in camlife white list.";

                            vaList.Add(validate);
                        }
                    }
                }
                else// get channel item user config error
                {
                    validate = new ValidateRequest();
                    validate.field = "Channel";
                    validate.message = "Channel is not configured in camlife.";

                    vaList.Add(validate);

                }
            }
            if (para.ReferralStaffName.Trim() == "")
            {
                validate = new ValidateRequest();
                validate.field = "ReferralStaffName";
                validate.message = "Referral staff name cannot be blank.";

                vaList.Add(validate);
            }

            if (para.ReferralStaffPosition.Trim() == "")
            {
                validate = new ValidateRequest();
                validate.field = "ReferralStaffPosition";
                validate.message = "Referral staff position cannot be blank.";

                vaList.Add(validate);
            }

            if (para.ClientType == null || para.ClientType.Trim() == "")
            {
                validate = new ValidateRequest();
                validate.field = "ClientType";
                validate.message = "Client Type cannot be blank.";

                vaList.Add(validate);
            }
            else
            {
                if (para.ClientType.ToUpper().Trim() != "FAMILY")
                {
                    if (para.ClientCIF == null || para.ClientCIF == "")
                    {
                        validate = new ValidateRequest();
                        validate.field = "ClientCIF";
                        validate.message = "Client cif cannot be blank.";

                        vaList.Add(validate);
                    }

                }
            }
            if (para.ClientNameENG.Trim() == "")
            {
                validate = new ValidateRequest();
                validate.field = "ClientNameENG";
                validate.message = "Client name in english cannot be blank.";

                vaList.Add(validate);
            }
            if (para.ClientNameKHM.Trim() == "")
            {
                validate = new ValidateRequest();
                validate.field = "ClientNameKH";
                validate.message = "Client name in khmer cannot be blank.";

                vaList.Add(validate);
            }
            if (para.ClientGender.Trim() == "")
            {
                vaList.Add(new ValidateRequest() { field = "ClientGender", message = "Gender cannot be blank." });
            }
            else
            {
                if (para.ClientGender.Trim().ToUpper() != "MALE" && para.ClientGender.Trim().ToUpper() != "FEMALE")
                {
                    vaList.Add(new ValidateRequest() { field = "GENDER", message = "Gender must be Male or Female" });
                }
            }
            if (para.ClientNationality.Trim().ToUpper() == "")
            {
                vaList.Add(new ValidateRequest() { field = "ClientNationality", message = "Nationality cannot be blank." });
            }
            if (para.ClientDoB == "")
            {
                vaList.Add(new ValidateRequest() { field = "ClientDoB", message = "Date of birth cannot be blank." });
            }
            else
            {
                if (!Helper.IsDate(para.ClientDoB))
                {
                    vaList.Add(new ValidateRequest() { field = "ClientDoB", message = "Date of birth must be in format dd-MM-yyyy." });
                }
                else
                {
                    int age = Calculation.Culculate_Customer_Age(para.ClientDoB, DateTime.Now.Date);
                    if (age > 60)
                    {
                        vaList.Add(new ValidateRequest() { field = "ClientDoB", message = "age [" + age + "] is not allow." });
                    }
                }
            }

            if (para.ClientType.ToUpper().Trim() != "FAMILY" && para.ClientType.ToUpper().Trim() != "")
            {

                if (para.ClientVillage == null || para.ClientVillage.Trim().ToUpper() == "")
                {
                    vaList.Add(new ValidateRequest() { field = "ClientVillage", message = "Village cannot be blank." });
                }
                if (para.ClientCommune == null || para.ClientCommune.Trim().ToUpper() == "")
                {
                    vaList.Add(new ValidateRequest() { field = "ClientCommune", message = "Commune cannot be blank." });
                }
                if (para.ClientDistrict == null || para.ClientDistrict.Trim().ToUpper() == "")
                {
                    vaList.Add(new ValidateRequest() { field = "ClientDistrict", message = "District cannot be blank." });
                }
                if (para.ClientProvince == null || para.ClientProvince.Trim().ToUpper() == "")
                {
                    vaList.Add(new ValidateRequest() { field = "ClientProvince", message = "Province cannot be blank." });
                }
            }
            if (para.DocumentType.Trim().ToUpper() == "")
            {
                vaList.Add(new ValidateRequest() { field = "DocumentType", message = "Document type cannot be blank." });
            }
            if (para.DocumentId.Trim().ToUpper() == "")
            {
                vaList.Add(new ValidateRequest() { field = "DocumentType", message = "Document id number cannot be blank." });
            }
            if (para.ClientPhoneNumber.Trim().ToUpper() == "")
            {
                vaList.Add(new ValidateRequest() { field = "ClientPhoneNumber", message = "Phone number cannot be blank." });
            }

            //else
            //{
            //    int num;
            //    var isNumeric = int.TryParse(para.ClientPhoneNumber, out num);

            //    if (!isNumeric)
            //    {
            //        vaList.Add(new Validate() { field = "ClientPhoneNumber", message = "Phone number cannot be number only." });
            //    }
            //}

            if (para.Interest.Trim().ToUpper() == "")
            {
                vaList.Add(new ValidateRequest() { field = "Interest", message = "Interest cannot be blank." });
            }
            if (para.ReferredDate.Trim().ToUpper() == "")
            {
                vaList.Add(new ValidateRequest() { field = "ReferredDate", message = "Referred date cannot be blank." });
            }
            else
            {
                if (!Helper.IsDate(para.ReferredDate))
                {
                    vaList.Add(new ValidateRequest() { field = "ReferredDate", message = "Referred date must be in format dd-MM-yyyy." });
                }
            }
            if (para.CreatedBy.Trim().ToUpper() == "")
            {
                vaList.Add(new ValidateRequest() { field = "CreatedBy", message = "Created by cannot be blank." });
            }
            #endregion
            //Valid Data
            if (vaList.Count == 0)
            {
                ErrorCode myError = new ErrorCode();
                try
                {
                    string policyStatus = "";
                    string leadType = "";
                    string errorMessage = "";
                    #region Check multi policy
                    int remainExpiryDays = 0;
                    int policyExpirDays = 0;
                    int allowBeforeExpireDays = AppConfiguration.AllowMultiPolicyBeforeExpireDays();
                    int allowAfterExpireDays = AppConfiguration.AllowMRepaymentPolicyAfterExpireDays();
                    bool allowNewMultiPolicy = AppConfiguration.AllowMultiNewPolicyPerLife();
                    bool allowRenewMulitPolicy = AppConfiguration.AllowMultiPolicyPerLife();

                    DataTable tbl = new DataTable();
                    DataTable tblExpiring = new DataTable();

                    bl_customer_lead lead = new bl_customer_lead();
                    bool isNewApplication = true;// true save new, false update on old lead

                    //check existing lead

                    List<bl_customer_lead> LeadList = new List<bl_customer_lead>();
                    //bl_customer_lead xLead = new bl_customer_lead();
                    //Check existing lead
                    lead = da_customer_lead.GetCustomerLeadByApplicationID(para.ApplicationID);//recheck lead by appliction number
                    if (lead.ApplicationID != null)//existing lead found
                    {
                        #region existing lead
                        if (lead.InsuranceApplicationId == "" && lead.Status == "")//lead is not yet convert to appliction system is allow to update 
                        {
                            //Upate
                            isNewApplication = false;
                            myObj = new ResponseExecuteError();
                        }
                        else if (lead.InsuranceApplicationId == "" && (lead.Status == LeadStatus.Reject.ToString() || lead.Status == LeadStatus.Delete.ToString()))
                        {
                            myObj = new ResponseExecuteError() { message = "This lead is already exist with the status Reject, system is not allow to update.", code = myError.ExistingCode };

                        }
                        else// lead is already converted to application 
                        {
                            myObj = new ResponseExecuteError() { message = "This lead is already exist and convert to appliction [" + lead.InsuranceApplicationId + "], system is not allow to update.", code = myError.ExistingCode };
                        }
                        #endregion existing lead
                    }
                    else// new lead
                    {
                        #region New lead
                        string exBankApplicationId = "";
                        int newLeadCount = 0;
                        int policyInf = 0;
                        string exPolicy = "";
                        //check recheck lead by: CIF or Client Name, Gender, DOB, Id type &  ID number
                        if (para.ClientType.ToUpper().Trim() != "FAMILY" && para.ClientType.ToUpper().Trim() != "")
                        {
                            LeadList = da_customer_lead.GetCustomerLead(para.ClientCIF);
                            tbl = da_banca.GetLeadApplicationPolicy(para.ClientCIF);//count policies 
                        }
                        else
                        {
                            LeadList = da_customer_lead.GetCustomerLead(para.ClientNameENG, para.ClientGender, Helper.FormatDateTime(para.ClientDoB), para.DocumentType, para.DocumentId);
                            tbl = da_banca.GetLeadApplicationPolicy(para.ClientNameENG, para.ClientGender, Helper.FormatDateTime(para.ClientDoB), para.DocumentType, para.DocumentId);
                        }

                        if (da_banca.SUCCESS)// no error while geting inforce policy
                        {
                            var listLeadNew = LeadList.Where(_ => _.Status == "");//count number of new lead
                            newLeadCount = listLeadNew.Count();
                            foreach (bl_customer_lead l in listLeadNew)// loop to get bank application id of new lead
                            {
                                exBankApplicationId += exBankApplicationId == "" ? l.ApplicationID : "," + l.ApplicationID;
                            }

                            if (tbl.Rows.Count > 0)
                            {
                                List<DataRow> dataInforce = tbl.AsEnumerable().Where(_ => _["policy_status"].ToString() == "IF").ToList();
                                policyInf = dataInforce.Count();// count inforce policy

                                if (policyInf > 0)
                                {
                                    foreach (DataRow pol in dataInforce)
                                    {
                                        exPolicy += exPolicy == "" ? pol["policy_number"].ToString() : "," + pol["policy_number"].ToString();
                                    }
                                }
                            }

                            //get policy config by channel item it
                            Channel_Item_Config ch = new Channel_Item_Config();
                            List<Channel_Item_Config> lCh = ch.GetChannelItemConfig(channelItemId, 1);
                            if (Channel_Item_Config.Transection)
                            {
                                var chConfig = lCh[0];
                                //check multi policy type 
                                if (AppConfiguration.CheckMultiPolicy() == AppConfiguration.MultiPolicyType.REPAYMENT.ToString())
                                {
                                    if (LeadList.Count > 0)//lead found
                                    {

                                        #region Backup v1
                                        /*
                                          lead = LeadList[0];
                                        if (lead.InsuranceApplicationId == "")//lead existing but not yet convert to application 
                                        {
                                            //check lead status
                                            if (lead.Status == "")
                                            {
                                                myObj = new ResponseExecuteError() { message = "This lead is already exist with appliction id [" + lead.ApplicationID + "], system is not allow to add.", code = myError.ExistingCode };
                                            }
                                            else if (lead.Status == "Reject" || lead.Status == "Delete")
                                            {
                                                //new lead is ready to add
                                                leadType = "New";
                                                myObj = new ResponseExecuteError();
                                                isNewApplication = true;
                                            }
                                        }
                                        else// lead existing but already convert to application or policy
                                        {
                                            //check lead status
                                            if (lead.Status == "")
                                            {
                                                myObj = new ResponseExecuteError() { message = "This lead is already exist with appliction id [" + lead.ApplicationID + "], system is not allow to add.", code = myError.ExistingCode };
                                            }
                                            else if (lead.Status == "Reject" || lead.Status == "Delete")//application was cancel or delete
                                            {
                                                //new lead is ready to add
                                                leadType = "New";
                                                myObj = new ResponseExecuteError();
                                                isNewApplication = true;
                                            }
                                            else if (lead.Status == "Approved")//application is already convert to policy
                                            {
                                                isNewApplication = true;
                                                //check policy in expiring list
                                                if (para.ClientType.ToUpper().Trim() != "FAMILY" && para.ClientType.ToUpper().Trim() != "")
                                                {
                                                    tblExpiring = da_banca.GetLeadPolicyExpiring(para.ClientCIF);
                                                }
                                                else
                                                {
                                                    tblExpiring = da_banca.GetLeadPolicyExpiring(para.ClientNameENG, para.ClientGender, Helper.FormatDateTime(para.ClientDoB), para.DocumentType, para.DocumentId);
                                                }
                                                if (da_banca.SUCCESS)
                                                {
                                                    if (tblExpiring.Rows.Count > 0)// has policy in expiring list
                                                    {
                                                        var r = tblExpiring.Rows[0];
                                                        remainExpiryDays = Convert.ToDateTime(r["expiry_date"].ToString()).Date.Subtract(DateTime.Now.Date).Days;//calculate remain expire days
                                                        policyExpirDays = DateTime.Now.Date.Subtract(Convert.ToDateTime(r["expiry_date"].ToString()).Date).Days;
                                                        policyStatus = r["policy_status"].ToString();
                                                        //check policy status
                                                        if (policyStatus == "EXP")
                                                        {
                                                            // policy status is expired, system will check on expired period which can count as repayment or new
                                                            myObj = new ResponseExecuteError();
                                                            if (policyExpirDays <= allowAfterExpireDays)
                                                            {
                                                                //policy expired in 365 days system counts as repayment, this value can be changed in web config "ALLOW-REPAYMENT-POLICY-AFTER-EXPIRE-DAYS"
                                                                leadType = "Repayment";
                                                            }
                                                            else
                                                            {
                                                                //policy expired more than 10 days system counts as new policy
                                                                leadType = "New";

                                                            }
                                                        }
                                                        else if (policyStatus == "IF")
                                                        {
                                                            // policy status In-force system will check on multi policy setting is allowed or not, this setting can be changed in web config "ALLOW-MULTI-POLICY-PER-LIFE"

                                                            if (AppConfiguration.AllowMultiPolicyPerLife())
                                                            {
                                                                //system check on before expire period which can be did repayment or not
                                                                if (remainExpiryDays <= allowBeforeExpireDays)
                                                                {
                                                                    // system allow to do repayment in 30 days before policy expire, this value can be changed in web config "ALLOW-MULTI-POLICY-BEFORE-EXPIRE-DAYS"
                                                                    leadType = "Repayment";
                                                                    myObj = new ResponseExecuteError();
                                                                }
                                                                else
                                                                {
                                                                    //before expire period is not in 10 days system not allow to do repayment
                                                                    errorMessage = "This lead is already exist with policy number [" + r["policy_number"].ToString() + "] which will be expired in " + remainExpiryDays + " days. System is not allowed to post new lead.";
                                                                    myObj = new ResponseExecuteError() { message = string.Format(myError.Existing, r["policy_number"].ToString(), remainExpiryDays), code = myError.ExistingCode };
                                                                }
                                                            }
                                                            else
                                                            {
                                                                //system not allow to add multi policies
                                                                errorMessage = "This lead is already exist with policy number [" + r["policy_number"].ToString() + "], status In-force. System is not allowed to post new lead.";
                                                                myObj = new ResponseExecuteError() { message = errorMessage, code = myError.ExistingCode };

                                                            }
                                                        }
                                                    }
                                                    else
                                                    {//no policy in expiring list
                                                     //check policy
                                                        if (para.ClientType.ToUpper().Trim() != "FAMILY" && para.ClientType.ToUpper().Trim() != "")
                                                        {
                                                            tbl = da_banca.GetLeadApplicationPolicy(para.ClientCIF);//count policies 
                                                        }
                                                        else
                                                        {
                                                            tbl = da_banca.GetLeadApplicationPolicy(para.ClientNameENG, para.ClientGender, Helper.FormatDateTime(para.ClientDoB), para.DocumentType, para.DocumentId);
                                                        }
                                                        if (da_banca.SUCCESS)//get policy no error
                                                        {
                                                            if (tbl.Rows.Count > 0)//exist policy found
                                                            {
                                                                var r = tbl.Rows[0];
                                                                //remainExpiryDays = Convert.ToDateTime(r["expiry_date"].ToString()).Date.Subtract(DateTime.Now.Date).Days;//calculate remain expire days
                                                                policyExpirDays = DateTime.Now.Date.Subtract(Convert.ToDateTime(r["expiry_date"].ToString()).Date).Days;
                                                                policyStatus = r["policy_status"].ToString();
                                                                //check policy status

                                                                if (policyStatus == "IF")
                                                                {
                                                                    //policy status in-force system will check the multi policy is allow or not, this value can be changed in web config "ALLOW-MULTI-NEW_POLICY-PER-LIFE"
                                                                    if (AppConfiguration.AllowMultiNewPolicyPerLife())
                                                                    {
                                                                        leadType = "New";
                                                                        myObj = new ResponseExecuteError();

                                                                    }
                                                                    else
                                                                    {
                                                                        //system not allow multi policies
                                                                        errorMessage = "This lead is already exist with policy number [" + r["policy_number"].ToString() + "], status In-force. System is not allowed to POST/UPDATE lead.";
                                                                        myObj = new ResponseExecuteError() { message = errorMessage, code = myError.ExistingCode };
                                                                    }
                                                                }
                                                                else if (policyStatus == "EXP")
                                                                {
                                                                    myObj = new ResponseExecuteError();
                                                                    if (policyExpirDays <= allowAfterExpireDays)
                                                                    {
                                                                        //policy expired in 365 days system counts as repayment, this value can be changed in web config "ALLOW-REPAYMENT-POLICY-AFTER-EXPIRE-DAYS"
                                                                        leadType = "Repayment";
                                                                    }
                                                                    else
                                                                    {
                                                                        //policy expired more than 10 days system counts as new policy
                                                                        leadType = "New";

                                                                    }
                                                                }
                                                            }
                                                            else
                                                            { //no existing plicy found
                                                              //new lead is ready to add
                                                                leadType = "New";
                                                                myObj = new ResponseExecuteError();

                                                            }

                                                        }
                                                        else
                                                        { //get policy error
                                                            myObj = new ResponseExecuteError()
                                                            {
                                                                code = da_banca.CODE,
                                                                message = da_banca.MESSAGE
                                                            };
                                                        }
                                                    }
                                                }
                                                else
                                                {//get policy expiring error

                                                    myObj = new ResponseExecuteError()
                                                    {
                                                        code = da_banca.CODE,
                                                        message = da_banca.MESSAGE
                                                    };
                                                }
                                            }
                                        }

                                        */
                                        #endregion Bakup v1

                                        #region V2
                                        lead = LeadList[0];

                                        if (newLeadCount > 0)
                                        {
                                            myObj = new ResponseExecuteError() { message = "This lead is already exist with appliction id [" + exBankApplicationId + "], system is not allow to add.", code = myError.ExistingCode };

                                        }
                                        else
                                        {
                                            // check policy inforce
                                            if (policyInf > 0)
                                            {

                                                isNewApplication = true;
                                                //check policy in expiring list
                                                if (para.ClientType.ToUpper().Trim() != "FAMILY" && para.ClientType.ToUpper().Trim() != "")
                                                {
                                                    tblExpiring = da_banca.GetLeadPolicyExpiring(para.ClientCIF);
                                                }
                                                else
                                                {
                                                    tblExpiring = da_banca.GetLeadPolicyExpiring(para.ClientNameENG, para.ClientGender, Helper.FormatDateTime(para.ClientDoB), para.DocumentType, para.DocumentId);
                                                }
                                                if (da_banca.SUCCESS)
                                                {
                                                    if (tblExpiring.Rows.Count > 0)// has policy in expiring list
                                                    {
                                                        var r = tblExpiring.Rows[0];
                                                        remainExpiryDays = Convert.ToDateTime(r["expiry_date"].ToString()).Date.Subtract(DateTime.Now.Date).Days;//calculate remain expire days
                                                        policyExpirDays = DateTime.Now.Date.Subtract(Convert.ToDateTime(r["expiry_date"].ToString()).Date).Days;
                                                        policyStatus = r["policy_status"].ToString();
                                                        //check policy status
                                                        if (policyStatus == "EXP")
                                                        {
                                                            // policy status is expired, system will check on expired period which can count as repayment or new
                                                            myObj = new ResponseExecuteError();
                                                            if (policyExpirDays <= allowAfterExpireDays)
                                                            {
                                                                //policy expired in 365 days system counts as repayment, this value can be changed in web config "ALLOW-REPAYMENT-POLICY-AFTER-EXPIRE-DAYS"
                                                                leadType = "Repayment";
                                                                myObj = new ResponseExecuteError();
                                                            }
                                                            else
                                                            {
                                                                //policy expired more than 10 days system counts as new policy
                                                                leadType = "New";
                                                                myObj = new ResponseExecuteError();

                                                            }
                                                        }
                                                        else if (policyStatus == "IF")
                                                        {
                                                            // policy status In-force system will check on multi policy setting is allowed or not, this setting can be changed in web config "ALLOW-MULTI-POLICY-PER-LIFE"

                                                            if (AppConfiguration.AllowMultiPolicyPerLife())
                                                            {
                                                                //system check on before expire period which can be did repayment or not
                                                                if (remainExpiryDays <= allowBeforeExpireDays)
                                                                {
                                                                    // system allow to do repayment in 30 days before policy expire, this value can be changed in web config "ALLOW-MULTI-POLICY-BEFORE-EXPIRE-DAYS"
                                                                    leadType = "Repayment";
                                                                    myObj = new ResponseExecuteError();
                                                                }
                                                                else
                                                                {
                                                                    //before expire period is not in 10 days system not allow to do repayment
                                                                    errorMessage = "This lead is already exist with policy number [" + r["policy_number"].ToString() + "] which will be expired in " + remainExpiryDays + " days. System is not allowed to post new lead.";
                                                                    myObj = new ResponseExecuteError() { message = string.Format(myError.Existing, r["policy_number"].ToString(), remainExpiryDays), code = myError.ExistingCode };
                                                                }
                                                            }
                                                            else
                                                            {
                                                                //system not allow to add multi policies
                                                                errorMessage = "This lead is already exist with policy number [" + r["policy_number"].ToString() + "], status In-force. System is not allowed to post new lead.";
                                                                myObj = new ResponseExecuteError() { message = errorMessage, code = myError.ExistingCode };

                                                            }
                                                        }
                                                    }
                                                    else
                                                    {//no policy in expiring list
                                                     //check policy

                                                        var r = tbl.Rows[0];
                                                        //remainExpiryDays = Convert.ToDateTime(r["expiry_date"].ToString()).Date.Subtract(DateTime.Now.Date).Days;//calculate remain expire days
                                                        policyExpirDays = DateTime.Now.Date.Subtract(Convert.ToDateTime(r["expiry_date"].ToString()).Date).Days;
                                                        policyStatus = r["policy_status"].ToString();
                                                        //check policy status

                                                        if (policyStatus == "EXP")
                                                        {
                                                            // policy status is expired, system will check on expired period which can count as repayment or new
                                                            myObj = new ResponseExecuteError();
                                                            if (policyExpirDays <= allowAfterExpireDays)
                                                            {
                                                                //policy expired in 365 days system counts as repayment, this value can be changed in web config "ALLOW-REPAYMENT-POLICY-AFTER-EXPIRE-DAYS"
                                                                leadType = "Repayment";
                                                                myObj = new ResponseExecuteError();
                                                            }
                                                            else
                                                            {
                                                                //policy expired more than 10 days system counts as new policy
                                                                leadType = "New";
                                                                myObj = new ResponseExecuteError();

                                                            }
                                                        }
                                                        else if (policyStatus == "IF")
                                                        {
                                                            // policy status In-force system will check on multi policy setting is allowed or not, this setting can be changed in web config "ALLOW-MULTI-POLICY-PER-LIFE"

                                                            if (AppConfiguration.AllowMultiPolicyPerLife())
                                                            {
                                                                //system check on before expire period which can be did repayment or not
                                                                if (remainExpiryDays <= allowBeforeExpireDays)
                                                                {
                                                                    // system allow to do repayment in 30 days before policy expire, this value can be changed in web config "ALLOW-MULTI-POLICY-BEFORE-EXPIRE-DAYS"
                                                                    leadType = "Repayment";
                                                                    myObj = new ResponseExecuteError();
                                                                }
                                                                else
                                                                {
                                                                    //before expire period is not in 10 days system not allow to do repayment
                                                                    errorMessage = "This lead is already exist with policy number [" + r["policy_number"].ToString() + "] which will be expired in " + remainExpiryDays + " days. System is not allowed to post new lead.";
                                                                    myObj = new ResponseExecuteError() { message = string.Format(myError.Existing, r["policy_number"].ToString(), remainExpiryDays), code = myError.ExistingCode };
                                                                }
                                                            }
                                                            else
                                                            {
                                                                //system not allow to add multi policies
                                                                errorMessage = "This lead is already exist with policy number [" + r["policy_number"].ToString() + "], status In-force. System is not allowed to post new lead.";
                                                                myObj = new ResponseExecuteError() { message = errorMessage, code = myError.ExistingCode };

                                                            }
                                                        }

                                                    }
                                                }
                                                else
                                                {//get policy expiring error

                                                    myObj = new ResponseExecuteError()
                                                    {
                                                        code = da_banca.CODE,
                                                        message = da_banca.MESSAGE
                                                    };
                                                }


                                            }
                                            else// no policy inforce found
                                            {
                                                //new lead is ready to add
                                                leadType = "New";
                                                myObj = new ResponseExecuteError();
                                                isNewApplication = true;

                                            }

                                        }

                                        #endregion V2
                                    }
                                    else// no lead found
                                    {
                                        //new lead is ready to add
                                        leadType = "New";
                                        myObj = new ResponseExecuteError();
                                        isNewApplication = true;
                                    }
                                }
                                else if (AppConfiguration.CheckMultiPolicy() == AppConfiguration.MultiPolicyType.NEW.ToString())
                                {
                                    //check number of lead

                                    if (newLeadCount + policyInf > 0)
                                    {
                                        if (allowNewMultiPolicy)
                                        {
                                            #region 
                                            //if (newLeadCount >= chConfig.MaxPolicyPerLife)
                                            //{
                                            //    errorMessage = string.Format(myError.LeadCountReachToLimitedNumber, chConfig.MaxPolicyPerLife, exBankApplicationId);
                                            //    myObj = new ResponseExecuteError() { message = errorMessage, code = myError.ExistingCode };
                                            //}
                                            //else if (policyInf >= chConfig.MaxPolicyPerLife)
                                            //{
                                            //    errorMessage = string.Format(myError.PolicyCountReachToLimitedNumber, chConfig.MaxPolicyPerLife, exPolicy);
                                            //    myObj = new ResponseExecuteError() { message = errorMessage, code = myError.ExistingCode };
                                            //}
                                            //else if (newLeadCount + policyInf >= chConfig.MaxPolicyPerLife)
                                            //{
                                            //    errorMessage = string.Format(myError.LeadCountReachToLimitedNumber, chConfig.MaxPolicyPerLife, exBankApplicationId + "," + exPolicy);
                                            //    myObj = new ResponseExecuteError() { message = errorMessage, code = myError.ExistingCode };

                                            //}
                                            //else
                                            //{
                                            //    //new lead is ready to add
                                            //    leadType = "New";
                                            //    myObj = new ResponseExecuteError();
                                            //}
                                            #endregion
                                            if (newLeadCount + policyInf >= chConfig.MaxPolicyPerLife)
                                            {
                                                errorMessage = string.Format(myError.LeadCountReachToLimitedNumber, chConfig.MaxPolicyPerLife, exBankApplicationId + (exPolicy != "" ? (exBankApplicationId != "" ? "," : "") + exPolicy : ""));
                                                myObj = new ResponseExecuteError() { message = errorMessage, code = myError.ExistingCode };

                                            }
                                            else
                                            {
                                                //new lead is ready to add
                                                leadType = "New";
                                                myObj = new ResponseExecuteError();
                                            }
                                        }
                                        else
                                        {//multi policy is not allow


                                            if (newLeadCount > 0)
                                            {
                                                errorMessage = "This lead is already exist with appliction id [" + exBankApplicationId + "], system is not allow to add.";
                                                myObj = new ResponseExecuteError() { message = errorMessage, code = myError.ExistingCode };
                                            }
                                            else if (policyInf > 0)
                                            {
                                                errorMessage = "This lead is already exist with policy number [" + exPolicy + "], status In-force. System is not allowed to post new lead.";
                                                myObj = new ResponseExecuteError() { message = errorMessage, code = myError.ExistingCode };
                                            }

                                        }
                                    }
                                    else
                                    {
                                        //new lead is ready to add
                                        leadType = "New";
                                        myObj = new ResponseExecuteError();
                                    }


                                }

                            }
                            else
                            {
                                myObj = new ResponseExecuteError()
                                {
                                    code = "0",
                                    message = Channel_Item_Config.Message
                                };

                            }

                        }

                        else//Error while getting inforce policy
                        {
                            myObj = new ResponseExecuteError()
                            {
                                code = da_banca.CODE,
                                message = da_banca.MESSAGE
                            };
                        }
                        #endregion New lead

                    }

                    ResponseExecuteError objErro = (ResponseExecuteError)myObj;

                    if (objErro.code == null)//check error 
                    {
                        bl_customer_lead obj_lead = new bl_customer_lead();
                        if (!isNewApplication)
                        {
                            //Upate
                            obj_lead = new bl_customer_lead() { BranchCode = para.BranchCode, BranchName = para.BranchName, ApplicationID = para.ApplicationID, ReferralStaffId = para.ReferralStaffId, ReferralStaffName = para.ReferralStaffName, ReferralStaffPosition = para.ReferralStaffPosition, ClientType = para.ClientType, ClientCIF = para.ClientCIF == null ? "" : para.ClientCIF, ClientNameENG = para.ClientNameENG, ClientNameKHM = para.ClientNameKHM, ClientGender = para.ClientGender, ClientNationality = para.ClientNationality, ClientDoB = Helper.FormatDateTime(para.ClientDoB), ClientVillage = para.ClientVillage == null ? "" : para.ClientVillage, ClientCommune = para.ClientCommune == null ? "" : para.ClientCommune, ClientDistrict = para.ClientDistrict == null ? "" : para.ClientDistrict, ClientProvince = para.ClientProvince == null ? "" : para.ClientProvince, DocumentType = para.DocumentType, DocumentId = para.DocumentId, ClientPhoneNumber = para.ClientPhoneNumber, Interest = para.Interest, ReferredDate = Helper.FormatDateTime(para.ReferredDate), CreatedBy = para.CreatedBy, CreatedOn = DateTime.Now, Status = "", Remarks = "", StatusRemarks = "", ExternalUpdatedBy = para.CreatedBy, ExternalUpdatedOn = DateTime.Now, ID = lead.ID, APIUserID = userId };
                            LEAD_ID = da_customer_lead.UpdateCustomerLead(obj_lead);
                        }
                        else
                        {
                            //save
                            obj_lead = new bl_customer_lead() { BranchCode = para.BranchCode, BranchName = para.BranchName, ApplicationID = para.ApplicationID, ReferralStaffId = para.ReferralStaffId, ReferralStaffName = para.ReferralStaffName, ReferralStaffPosition = para.ReferralStaffPosition, ClientType = para.ClientType, ClientCIF = para.ClientCIF == null ? "" : para.ClientCIF, ClientNameENG = para.ClientNameENG, ClientNameKHM = para.ClientNameKHM, ClientGender = para.ClientGender, ClientNationality = para.ClientNationality, ClientDoB = Helper.FormatDateTime(para.ClientDoB), ClientVillage = para.ClientVillage == null ? "" : para.ClientVillage, ClientCommune = para.ClientCommune == null ? "" : para.ClientCommune, ClientDistrict = para.ClientDistrict == null ? "" : para.ClientDistrict, ClientProvince = para.ClientProvince == null ? "" : para.ClientProvince, DocumentType = para.DocumentType, DocumentId = para.DocumentId, ClientPhoneNumber = para.ClientPhoneNumber, Interest = para.Interest, ReferredDate = Helper.FormatDateTime(para.ReferredDate), CreatedBy = para.CreatedBy, CreatedOn = DateTime.Now, Status = "", Remarks = "", StatusRemarks = "", APIUserID = userId, LeadType = leadType };
                            LEAD_ID = da_customer_lead.InsertCustomerLead(obj_lead);
                        }

                        // string m = da_customer_lead.MESSAGE;

                        if (LEAD_ID != "")
                        {

                            response.message = "Success";
                            response.detail = new ResponseSaveLeadSuccess() { ApplicationID = obj_lead.ApplicationID };
                            myObj = response;

                        }
                        else
                        {

                            myObj = new ResponseExecuteError() { message = da_customer_lead.CODE == "0" ? myError.UnexpectedError + da_customer_lead.MESSAGE : da_customer_lead.MESSAGE, code = da_customer_lead.CODE == "0" ? myError.UnexpectedErrorCode : da_customer_lead.CODE };
                        }

                    }
                    #endregion


                }
                catch (FormatException fEX)
                {
                    myObj = new ResponseExecuteError() { message = myError.DataTypeError, code = myError.DatatypeErrorCode };
                    Log.AddExceptionToLog("Error function [SendLead(ParaSendLead para)] in call [ApplicationConsumerController], detail: " + fEX.Message + " ==> " + fEX.StackTrace);
                }
                catch (Exception ex)
                {
                    Log.AddExceptionToLog("Error function [SendLead(ParaSendLead para)] in call [ApplicationConsumerController], detail: " + ex.Message + " ==> " + ex.StackTrace);
                    myObj = new ResponseExecuteError() { message = ex.Message };
                }

            }
            else //Validate fail
            {

                myObj = new ResponseValidateError() { message = "Validation error, The request has " + vaList.Count + " errors.", detail = vaList };
            }
            return myObj;
        }

        #region Daily Booking
        [Route("api/dailyinsurancebooking")]
        [HttpPost]
        public object InsuranceBooking(ParaDailyBooking para)
        {
            ValidateRequest v = new ValidateRequest();
            ResponseValidateError error = new ResponseValidateError() { };
            List<ValidateRequest> listValidate = new List<ValidateRequest>();

            var userId = HttpContext.Current.User.Identity.GetUserId();
            var userName = HttpContext.Current.User.Identity.Name;

            object myObj = new object();
            if (para.StartDate.Trim() == "")
            {
                listValidate.Add(new ValidateRequest() { field = "StartDate", message = "Start date cannot be blank." });

            }
            else
            {
                if (!Helper.IsDate(para.StartDate))
                {
                    listValidate.Add(new ValidateRequest() { field = "StartDate", message = "Start date must be in format dd-MM-yyyy." });

                }
            }
            if (para.ToDate.Trim() == "")
            {
                listValidate.Add(new ValidateRequest() { field = "ToDate", message = "To date cannot be blank." });

            }
            else
            {
                if (!Helper.IsDate(para.ToDate))
                {
                    listValidate.Add(new ValidateRequest() { field = "ToDate", message = "To date must be in format dd-MM-yyyy." });

                }
            }
            if (listValidate.Count == 0)
            {
                DateTime f = Helper.FormatDateTime(para.StartDate);
                DateTime t = Helper.FormatDateTime(para.ToDate);

                List<bl_daily_insurance_booking_htb> obj = new List<bl_daily_insurance_booking_htb>();
                obj = da_banca.GetDailyInsuranceBookingHTB(f, t, userId);


                ErrorCode myErr = new ErrorCode();
                if (da_banca.SUCCESS)
                {
                    if (obj.Count > 0)
                    {
                        myObj = (new ResponseExecuteSuccess() { message = da_banca.MESSAGE, detail = obj });
                    }
                    else
                    {
                        obj = new List<bl_daily_insurance_booking_htb>();
                        myObj = (new ResponseExecuteSuccess() { message = da_banca.MESSAGE, detail = obj });
                    }
                }
                else
                {
                    myObj = (new ResponseExecuteError() { message = da_banca.CODE == "0" ? myErr.UnexpectedError + da_banca.MESSAGE : da_banca.MESSAGE, code = da_banca.CODE == "0" ? myErr.UnexpectedErrorCode : da_banca.CODE });
                }


            }
            else
            {

                error.message = "Validation error, The request has " + listValidate.Count + " errors.";
                error.detail = listValidate;
                myObj = error;
            }

            return myObj;
        }

        #endregion

        #region Payment list
        [Route("api/sendpaymentlist")]
        [HttpPost]
        public object SendPaymentList([FromBody] List<da_banca.PaymentHTBObjectString> PaymentList)
        {
            object myObj = new object();
            try
            {


                List<da_banca.PaymentHTB> result = new List<da_banca.PaymentHTB>();
                List<ResponseExecuteError> listError = new List<ResponseExecuteError>();
                List<ResponseExecuteSuccess> listSuccess = new List<ResponseExecuteSuccess>();
                #region Validate
                List<ValidateRequest> vaList = new List<ValidateRequest>();
                int index = 0;
                foreach (da_banca.PaymentHTBObjectString obj in PaymentList)
                {


                    if (obj.BranchCode.Trim() == "")
                    {

                        vaList.Add(new ValidateRequest() { field = "BranchCode", message = "Banch code cannot be blank." + " [Row Index: " + index + "]." });
                    }
                    if (obj.BranchName.Trim() == "")
                    {
                        vaList.Add(new ValidateRequest() { field = "BranchName", message = "Banch name cannot be blank." + " [Row Index: " + index + "]." });
                    }
                    if (obj.PaymentReferenceNo.Trim() == "")
                    {
                        vaList.Add(new ValidateRequest() { field = "PaymentReferenceNo", message = "Payment reference no cannot be blank." + " [Row Index: " + index + "]." });
                    }
                    if (obj.TransactionType.Trim() == "")
                    {
                        vaList.Add(new ValidateRequest() { field = "TransactionType", message = "Transaction cannot be blank." + " [Row Index: " + index + "]." });
                    }
                    if (obj.InsuranceApplicationId.Trim() == "")
                    {
                        vaList.Add(new ValidateRequest() { field = "InsuranceApplicationId", message = "Insurance application id cannot be blank." + " [Row Index: " + index + "]." });
                    }

                    else if (obj.InsuranceApplicationId.Trim().Length != 10)
                    {
                        vaList.Add(new ValidateRequest() { field = "InsuranceApplicationId", message = "Size must be 10 degits." + " [Row Index: " + index + "]." });

                    }
                    else if (obj.InsuranceApplicationId.Trim().Substring(0, 3) != "APP")
                    {
                        vaList.Add(new ValidateRequest() { field = "InsuranceApplicationId", message = "Insurance application id must be in format APP{number 7 degits}." + " [Row Index: " + index + "]." });
                    }
                    if (obj.ClientNameENG.Trim() == "")
                    {
                        vaList.Add(new ValidateRequest() { field = "ClientNameENG", message = "Client name cannot be blank." + " [Row Index: " + index + "]." });
                    }
                    if (obj.Currency.Trim() == "")
                    {
                        vaList.Add(new ValidateRequest() { field = "Currency", message = "Currency cannot be blank." + " [Row Index: " + index + "]." });
                    }
                    if (obj.Premium.Trim() == "")
                    {
                        vaList.Add(new ValidateRequest() { field = "Premium", message = "Premium cannot be blank." + " [Row Index: " + index + "]." });
                    }
                    else if (!Helper.IsNumeric(obj.Premium.Trim()))
                    {
                        vaList.Add(new ValidateRequest() { field = "Premium", message = "Premium must be IsNumeric." + " [Row Index: " + index + "]." });
                    }
                    if (obj.PaymentDate.Trim() == "")
                    {
                        vaList.Add(new ValidateRequest() { field = "PaymentDate", message = "Payment date cannot be blank." + " [Row Index: " + index + "]." });
                    }
                    else if (!Helper.IsDate(obj.PaymentDate.Trim()))
                    {
                        vaList.Add(new ValidateRequest() { field = "PaymentDate", message = "Payment date must be in format dd-MM-yyyy." + " [Row Index: " + index + "]." });
                    }
                    index += 1;
                }
                #endregion
                //reset index
                int fail = 0; int success = 0;
                int total_record = PaymentList.Count;
                List<ResponsePaymentList> errList = new List<ResponsePaymentList>();

                if (vaList.Count == 0)
                {
                    foreach (da_banca.PaymentHTBObjectString obj in PaymentList)
                    {
                        try
                        {
                            DateTime payment_date_ = Helper.FormatDateTime(obj.PaymentDate);
                            DateTime created_on = DateTime.Now;
                            string created_by = "HTB";
                            double premium = Convert.ToDouble(obj.Premium);
                            da_banca.PaymentHTB.SavePaymentHTB(new da_banca.PaymentHTB()
                            {
                                BranchCode = obj.BranchCode,
                                BranchName = obj.BranchName,
                                PaymentReferenceNo = obj.PaymentReferenceNo,
                                TransactionType = obj.TransactionType,
                                InsuranceApplicationId = obj.InsuranceApplicationId,
                                ClientNameENG = obj.ClientNameENG,
                                Premium = premium,
                                CreatedBy = created_by,
                                CreatedOn = created_on,
                                Remarks = "",
                                Currency = obj.Currency,
                                PaymentDate = payment_date_

                            });
                            if (da_banca.SUCCESS)
                            {
                                success += 1;

                                // listSuccess.Add(new MyResponse() { MESSAGE = "Success", DETAIL = "PAYMENT_REFERENCE_NO:" + obj.PAYMENT_REFERENCE_NO });
                            }
                            else
                            {
                                fail += 1;
                                //listError.Add(new Error() { MESSAGE = da_banca.MESSAGE });
                                errList.Add(new ResponsePaymentList()
                                {
                                    BranchCode = obj.BranchCode,
                                    BranchName = obj.BranchName,
                                    PaymentReferenceNo = obj.PaymentReferenceNo,
                                    TransactionType = obj.TransactionType,
                                    InsuranceApplicationId = obj.InsuranceApplicationId,
                                    ClientNameENG = obj.ClientNameENG,
                                    Currency = obj.Currency,
                                    PaymentDate = obj.PaymentDate,
                                    Premium = obj.Premium,
                                    ErrorMessage = da_banca.MESSAGE
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            fail += 1;
                            //listError.Add(new Error() { MESSAGE = ex.Message });
                            errList.Add(new ResponsePaymentList()
                            {
                                BranchCode = obj.BranchCode,
                                BranchName = obj.BranchName,
                                PaymentReferenceNo = obj.PaymentReferenceNo,
                                TransactionType = obj.TransactionType,
                                InsuranceApplicationId = obj.InsuranceApplicationId,
                                ClientNameENG = obj.ClientNameENG,
                                Currency = obj.Currency,
                                PaymentDate = obj.PaymentDate,
                                Premium = obj.Premium,
                                ErrorMessage = ex.Message
                            });

                        }

                    }

                    if (success > 0 && fail == 0)//success all
                    {
                        ResponsePaymentListSuccessAll objSucc = new ResponsePaymentListSuccessAll()
                        {
                            status = "200",
                            code = "1000",
                            message = "Success",
                            TotalRecords = total_record,
                            TotalSuccessRecords = success,
                            TotalfailRecords = fail
                        };
                        myObj = objSucc;
                    }
                    else if (fail > 0) //fail all
                    {
                        ResponsePaymentListError objErr = new ResponsePaymentListError()
                        {
                            status = "400",
                            code = "1007",
                            message = "Fail",
                            TotalRecords = total_record,
                            TotalSuccessRecords = success,
                            TotalfailRecords = fail,
                            detail = errList
                        };
                        myObj = objErr;
                    }

                }
                else//validate error
                {
                    myObj = new ResponseValidateError() { message = "Validation error, The request has " + vaList.Count + " errors.", detail = vaList };
                }

            }
            catch (Exception ex)
            {
                myObj = new ResponseExecuteError() { message = ex.Message };

            }
            return myObj;
        }


        #endregion


        #region Post Payment
        /*
         
         */

        [Route("api/postpayment")]
        [HttpPost]
        public object PostPayment(ParaPostPayment para)
        {
            object myObj = new object();
            try
            {
                ChannelItemUserConfig chUser = new ChannelItemUserConfig();
                ChannelItemUserConfig chObj = new ChannelItemUserConfig();
                da_PostPayment postPaymentTran;
                string existTranNo = "";
                #region Validate
                List<ValidateRequest> vList = new List<ValidateRequest>();
                if (para == null)
                {
                    vList.Add(new ValidateRequest() { field = "PostPayment", message = "PostPayment object is required." });
                }
                else
                {
                    postPaymentTran = new da_PostPayment();
                   existTranNo= postPaymentTran.GetExistingTransactionNumber(para.TransactionReferenceNumber);
                    if (postPaymentTran.SUCCESS && existTranNo != "")
                    {
                        vList.Add(new ValidateRequest() { field = "TransactionReferenceNumber", message = string.Format("Transaction Reference Number [{0}] is already exist.", para.TransactionReferenceNumber) });
                    }
                    else if (postPaymentTran.SUCCESS && existTranNo == "")
                    {
                        if (string.IsNullOrWhiteSpace(para.PaymentCode))
                        {
                            vList.Add(new ValidateRequest() { field = "PaymentCode", message = "Payment Code is required." });
                        }
                        if (string.IsNullOrWhiteSpace(para.BillNo))
                        {
                            vList.Add(new ValidateRequest() { field = "BillNo", message = "Bill No is required." });
                        }
                        if (string.IsNullOrWhiteSpace(para.BillerId))
                        {
                            vList.Add(new ValidateRequest() { field = "BillerId", message = "Biller Id is required." });
                        }
                        if (string.IsNullOrWhiteSpace(para.BillerName))
                        {
                            vList.Add(new ValidateRequest() { field = "BillerName", message = "Biller Name is required." });
                        }

                        if (para.BillAmount <=0)
                        {
                            vList.Add(new ValidateRequest() { field = "BillAmount", message = "Bill Amount must be greater than 0." });
                        }
                       
                        if ((para.FeeCharge<0))
                        {
                            vList.Add(new ValidateRequest() { field = "FeeCharge", message = "Fee Charge must be grather or equal 0." });
                        }
                        if ( para.TotalAmount<=0)
                        {
                            vList.Add(new ValidateRequest() { field = "TotalAmount", message = "Total Amount must be greater than 0." });

                        }
                        if (string.IsNullOrWhiteSpace(para.TransactionType))
                        {
                            vList.Add(new ValidateRequest() { field = "TransactionType", message = "Transaction Type is required." });

                        }
                        if (string.IsNullOrWhiteSpace(para.TransactionReferenceNumber))
                        {
                            vList.Add(new ValidateRequest() { field = "TransactionReferenceNumber", message = "Transaction Reference Number is required." });
                        }
                        if (!Helper.IsDate(para.TransactionDate))
                        {
                            vList.Add(new ValidateRequest() { field = "TransactionDate", message = "Transaction Date is required with format [DD-MM-YYYY]." });


                        }
                        if (string.IsNullOrWhiteSpace(para.BankName))
                        {
                            vList.Add(new ValidateRequest() { field = "BankName", message = "Bank Name is required." });

                        }

                        if(para.TransactionAmount>0 && para.FeeCharge>=0 && para.TotalAmount>0 && para.BillAmount>0)
                        {
                            da_banca.ApplicationConsumer applicationConsumer = new da_banca.ApplicationConsumer();
                            chObj = chUser.GetByUserName(User.Identity.Name);
                            if (!string.IsNullOrEmpty(chObj.ChannelItemId))
                            {
                                applicationConsumer = da_banca.ApplicationConsumer.GetApplicationConsumer(para.PaymentCode, chObj.ChannelItemId);
                                if (para.PaymentCode == applicationConsumer.InsuranceApplicationId)
                                {
                                    if (para.BillAmount < applicationConsumer.Premium)
                                    {
                                        vList.Add(new ValidateRequest() { field = "BillAmount", message = "Bill Amount is not enough to pay the premium." });
                                    }
                                    else if (para.BillAmount > applicationConsumer.Premium)
                                    {
                                        vList.Add(new ValidateRequest() { field = "BillAmount", message = "Bill Amount is exceeded the premium." });

                                    }
                                    else//Bill amount is accepted
                                    {
                                        if (para.TotalAmount != para.BillAmount + para.FeeCharge)
                                        {
                                            vList.Add(new ValidateRequest() { field = "TotalAmount", message = "Total Amount must be equal to (Bill amount + Fee charge)." });
                                        }
                                        else
                                        {
                                            if (para.TransactionAmount != para.TotalAmount)
                                            {
                                                vList.Add(new ValidateRequest() { field = "TransactionAmount", message = "Transaction Amount must be equal to Total Amount." });

                                            }
                                        }
                                       
                                    }
                                }
                                else
                                {
                                    vList.Add(new ValidateRequest() { field = "PaymentCode", message = string.Format("PaymentCode [{0}] is not match with Application Consumer.", para.PaymentCode) });
                                }
                            }
                            else
                            {
                                vList.Add(new ValidateRequest() { field = "Channel", message = "Channel is not configured in camlife." });

                            }

                        }
                    }
                    else //get existing transaction number error
                    {
                        vList.Add(new ValidateRequest() { field = "TransactionReferenceNumber", message ="System is getting error on validation." });

                    }


                }
                

                #endregion Validate
                if (vList.Count == 0)
                {
                    ErrorCode myErr = new ErrorCode();
                     postPaymentTran = new da_PostPayment();
                    List<ResponsePostPaymentSuccess> successResponse = new List<ResponsePostPaymentSuccess>();
                    bl_PostPayment postPayment = new bl_PostPayment()
                    {
                        PaymentCode = para.PaymentCode,
                        BillNo=para.BillNo,
                        BillerId = para.BillerId,
                        BillerName = para.BillerName,
                        BillAmount = para.BillAmount,
                        FeeCharge = para.FeeCharge,
                        TotalAmount = para.TotalAmount,
                        TransactionAmount = para.TransactionAmount,
                        TransactionType = para.TransactionType,
                        TransactionReferenceNumber = para.TransactionReferenceNumber,
                        TransactionDate = Helper.FormatDateTime(para.TransactionDate),
                        BankName = para.BankName,
                        CreatedBy = User.Identity.Name,
                        CreatedOn = DateTime.Now,
                        SysRemarks = string.Empty
                    };
                    postPaymentTran.SavePostPayment(postPayment);
                    if (postPaymentTran.SUCCESS == true)
                    {
                        successResponse.Add(new ResponsePostPaymentSuccess() { TransactionReferenceNumber=para.TransactionReferenceNumber, PaymentCode = para.PaymentCode, ReceivedAmount = para.TransactionAmount, ReceivedDate= Helper.FormatDateTime( para.TransactionDate) });
                        myObj = (new ResponseExecuteSuccess() { message = "Success", detail = successResponse });
                    }
                    else
                    {

                        myObj = (new ResponseExecuteError() { message = postPaymentTran.CODE == "0" ? myErr.UnexpectedError + postPaymentTran.MESSAGE : postPaymentTran.MESSAGE, code = postPaymentTran.CODE == "0" ? myErr.UnexpectedErrorCode : postPaymentTran.CODE });

                    }
                }
                else
                {
                    myObj = new ResponseValidateError() { message = "Validation error, The request has " + vList.Count + " errors.", detail = vList };


                }
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("Error function [PostPayment(ParaPostPayment para)] in call [ApplicationConsumerController], detail: " + ex.Message + " ==> " + ex.StackTrace);
                myObj = new ResponseExecuteError() { message = ex.Message };
            }

            return myObj;

        }
        #endregion Post Payment

      
    }

   
    #region Paramenter for url
   
    public class ParaDailyBooking
    {
        public String StartDate { get; set; }
        public string ToDate { get; set; }
    }
    public class ParaApplicationConsumer
    {
        public string InsuranceApplicationId { get; set; }
    }
    public class ParaSendLead
    {
        #region Properties

        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string ApplicationID { get; set; }
        public string ReferralStaffId { get; set; }
        public string ReferralStaffName { get; set; }
        public string ReferralStaffPosition { get; set; }
        public string ClientType { get; set; }
        public string ClientCIF { get; set; }
        public string ClientNameENG { get; set; }
        public string ClientNameKHM { get; set; }
        public string ClientGender { get; set; }
        public string ClientNationality { get; set; }
        public string ClientDoB { get; set; }
        public string ClientVillage { get; set; }
        public string ClientCommune { get; set; }
        public string ClientDistrict { get; set; }
        public string ClientProvince { get; set; }
        /// <summary>
        /// ID type
        /// </summary>
        public string DocumentType { get; set; }
        /// <summary>
        /// ID Number
        /// </summary>
        public string DocumentId { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string Interest { get; set; }
        public string ReferredDate { get; set; }
        /// <summary>
        /// Is an insurance application number
        /// </summary>
        public string InsuranceApplicationId { get; set; }

        public string CreatedBy { get; set; }


        #endregion

    }

    public class ParaPostPayment
    {
        public string PaymentCode { get; set; }
        public string BillNo { get; set; }
        public string BillerId { get; set; }
        public string BillerName { get; set; }
        public double BillAmount { get; set; }
        public double FeeCharge { get; set; }
        public double TotalAmount { get; set; }
        public double TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public string TransactionReferenceNumber { get; set; }
        /// <summary>
        /// Format in DD/MM/YYYY
        /// </summary>
        public string TransactionDate { get; set; }
        public string BankName { get; set; }
    }
    #endregion
}
