using Antlr.Runtime;
using CamlifeAPI1.Class;
using CamlifeAPI1.Class.Application;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using NPOI.DDF;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.Converter;
using NPOI.SS.Formula.Functions;
using NPOI.Util;
using Org.BouncyCastle.Asn1.Mozilla;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Crypto.Engines;
using RestSharp.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations.Model;
using System.Data.SqlTypes;
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
using static CamlifeAPI1.Class.Master.da_master;
using static da_micro_product_config;

namespace CamlifeAPI1.Controllers
{
    [Authorize]
    public class PolicyController : ApiController
    {

        [Route("api/Policy/IssueMultiPolicy1")]
        [HttpPost]
        public object IssueMultiPolicy1(RequestParam.IssueMultiPolicy obj)
        {
            var validate = ValidatIssueMultiPolicy(obj);
            var err = new ErrorCode();
            var tranDate = DateTime.Now;
            try
            {
                if (validate.detail != null)
                {
                    return validate;
                }
                else
                {
                    bl_application_for_issue app = new bl_application_for_issue();
                    object response = new object();
                    int step = 0;

                    DateTime effectiveDate = new DateTime(1900, 1, 1);
                    DateTime issuedDate;
                    DateTime expiryDate;
                    DateTime maturityDate;
                    bool result = false;
                    bool isExistingCustomer = false;
                    bl_micro_customer cus = new bl_micro_customer();
                    //ResponseParam.IssueMultiPolicy IssuePolicies = new ResponseParam.IssueMultiPolicy();
                    List<ResponseParam.SavedIssuePolicy> IssuePolicies = new List<ResponseParam.SavedIssuePolicy>();
                    /*Loop application*/
                    foreach (var item in obj.Applications)
                    {
                        app = da_micro_application.GetApplicationForIssuePolicy(item.ApplicationNumber);
                        response = new object();
                        if (app != null)
                        {
                            string cusNumber = "";
                            //save customer 
                            if (step == 0)/*save customer only one time*/
                            {
                                /*check existing customer*/
                                var existCust = da_micro_customer.GetCustomerByIdNumber(app.Customer.ID_TYPE, app.Customer.ID_NUMBER);

                                if (existCust.CUSTOMER_NUMBER != null)
                                {
                                    isExistingCustomer = true;
                                    cus = existCust;
                                    result = true;//mark as transection success
                                }
                                else
                                {
                                    isExistingCustomer = false;
                                    //get customer prefix
                                    var cusPrefix = da_micro_customer_prefix.GetLastCustomerPrefix();
                                    cus = new bl_micro_customer();

                                    if (cusPrefix.PREFIX2 == cus.LAST_PREFIX)//in the same year
                                    {
                                        cus.SEQ = cus.LAST_SEQ + 1;
                                    }
                                    else
                                    {
                                        cus.SEQ = 1;
                                    }
                                    cusNumber = cusPrefix.PREFIX1 + cusPrefix.PREFIX2 + cus.SEQ.ToString(cusPrefix.DIGITS);
                                    cus.CUSTOMER_NUMBER = cusNumber;
                                    cus.CUSTOMER_TYPE = "INDIVIDUAL";
                                    cus.ID_TYPE = app.Customer.ID_TYPE + "";
                                    cus.ID_NUMBER = app.Customer.ID_NUMBER;
                                    cus.FIRST_NAME_IN_ENGLISH = app.Customer.FIRST_NAME_IN_ENGLISH;
                                    cus.LAST_NAME_IN_ENGLISH = app.Customer.LAST_NAME_IN_ENGLISH;
                                    cus.FIRST_NAME_IN_KHMER = app.Customer.FIRST_NAME_IN_KHMER;
                                    cus.LAST_NAME_IN_KHMER = app.Customer.LAST_NAME_IN_KHMER;
                                    cus.GENDER = app.Customer.GENDER + "";
                                    cus.DATE_OF_BIRTH = app.Customer.DATE_OF_BIRTH;
                                    cus.NATIONALITY = app.Customer.NATIONALITY;
                                    cus.MARITAL_STATUS = app.Customer.MARITAL_STATUS;
                                    cus.OCCUPATION = app.Customer.OCCUPATION;
                                    cus.HOUSE_NO_KH = app.Customer.HOUSE_NO_EN;
                                    cus.STREET_NO_KH = app.Customer.STREET_NO_EN;
                                    cus.VILLAGE_KH = app.Customer.VILLAGE_EN;
                                    cus.COMMUNE_KH = app.Customer.COMMUNE_EN;
                                    cus.DISTRICT_KH = app.Customer.DISTRICT_EN;
                                    cus.PROVINCE_KH = app.Customer.PROVINCE_EN;
                                    cus.HOUSE_NO_EN = app.Customer.HOUSE_NO_EN;
                                    cus.STREET_NO_EN = app.Customer.STREET_NO_EN;
                                    cus.VILLAGE_EN = app.Customer.VILLAGE_EN;
                                    cus.COMMUNE_EN = app.Customer.COMMUNE_EN;
                                    cus.DISTRICT_EN = app.Customer.DISTRICT_EN;
                                    cus.PROVINCE_EN = app.Customer.PROVINCE_EN;
                                    cus.PHONE_NUMBER1 = app.Customer.PHONE_NUMBER1;
                                    cus.EMAIL1 = app.Customer.EMAIL1;
                                    cus.CREATED_BY = obj.CreatedBy;
                                    cus.CREATED_ON = tranDate;
                                    cus.STATUS = 1;
                                    result = da_micro_customer.SaveCustomer(cus);
                                }
                            }
                            else
                            {
                                result = true;
                            }

                            if (string.IsNullOrWhiteSpace(app.PolicyNumber))
                            {

                                string polNumber = "";

                                var pol = new bl_micro_policy();
                                var polDetail = new bl_micro_policy_detail();
                                var polRider = new bl_micro_policy_rider();
                                var polpolPayment = new bl_micro_policy_payment();
                                var polBen = new List<bl_micro_policy_beneficiary>();
                                var polAddress = new bl_micro_policy_address();
                                var polPrefix = da_micro_policy_prefix.GetLastPolicyPrefix();

                                if (result)/*after save customer success*/
                                {
                                    if (pol.LAST_PREFIX == polPrefix.PREFIX2)//in the same year
                                    {
                                        pol.SEQ = pol.LAST_SEQ + 1;
                                    }
                                    else
                                    {
                                        pol.SEQ = 1;
                                    }
                                    polNumber = polPrefix.PREFIX1 + polPrefix.PREFIX2 + pol.SEQ.ToString(polPrefix.DIGITS);
                                    pol.POLICY_NUMBER = polNumber;
                                    pol.POLICY_TYPE = "COR";
                                    pol.APPLICATION_ID = app.Application.APPLICATION_ID;
                                    pol.CUSTOMER_ID = cus.ID;
                                    pol.PRODUCT_ID = app.Insurance.PRODUCT_ID;
                                    pol.CHANNEL_ID = app.Application.CHANNEL_ID;
                                    pol.CHANNEL_ITEM_ID = app.Application.CHANNEL_ITEM_ID;
                                    pol.CHANNEL_LOCATION_ID = app.Application.CHANNEL_LOCATION_ID;
                                    pol.AGENT_CODE = app.Application.SALE_AGENT_ID;
                                    pol.CREATED_ON = tranDate;
                                    pol.CREATED_BY = obj.CreatedBy;
                                    pol.POLICY_STATUS = "IF";
                                    pol.RenewFromPolicy = app.Application.RENEW_FROM_POLICY;
                                    result = da_micro_policy.SavePolicy(pol);
                                    if (result)
                                    {
                                        if (effectiveDate.Year == 1900)
                                        {
                                            effectiveDate = item.ClientType == Helper.ClientType.REPAYMENT.ToString() ? Helper.FormatDateTime(obj.EffectiveDate).AddYears(1) : Helper.FormatDateTime(obj.EffectiveDate);
                                        }
                                        else
                                        {
                                            effectiveDate = item.ClientType == Helper.ClientType.REPAYMENT.ToString() ? effectiveDate.AddYears(1) : Helper.FormatDateTime(obj.EffectiveDate); effectiveDate.AddYears(1);
                                        }

                                        maturityDate = effectiveDate.AddYears(1);
                                        expiryDate = maturityDate.AddDays(-1);
                                        issuedDate = Helper.FormatDateTime(obj.IssueDate);
                                        //save policy detail
                                        int age = Calculation.Culculate_Customer_Age(cus.DATE_OF_BIRTH.ToString("dd/MM/yyyy"), app.Application.APPLICATION_DATE);
                                        polDetail = new bl_micro_policy_detail()
                                        {
                                            POLICY_ID = pol.POLICY_ID,
                                            EFFECTIVE_DATE = effectiveDate,// Helper.FormatDateTime(obj.EffectiveDate),
                                            ISSUED_DATE = issuedDate,// Helper.FormatDateTime(obj.IssueDate),
                                            MATURITY_DATE = maturityDate,// Helper.FormatDateTime(obj.EffectiveDate).AddYears(1),
                                            EXPIRY_DATE = expiryDate,// Helper.FormatDateTime(obj.EffectiveDate).AddYears(1).AddDays(-1),
                                            PREMIUM = app.Insurance.PREMIUM,
                                            ANNUAL_PREMIUM = app.Insurance.ANNUAL_PREMIUM,
                                            DISCOUNT_AMOUNT = app.Insurance.DISCOUNT_AMOUNT,
                                            TOTAL_AMOUNT = app.Insurance.TOTAL_AMOUNT,
                                            COVER_YEAR = app.Insurance.TERME_OF_COVER,
                                            PAY_YEAR = app.Insurance.PAYMENT_PERIOD,
                                            PAYMENT_CODE = app.Insurance.PAYMENT_CODE,
                                            PAY_MODE = app.Insurance.PAY_MODE,
                                            AGE = age,
                                            COVER_UP_TO_AGE = age + app.Insurance.TERME_OF_COVER,
                                            PAY_UP_TO_AGE = age + app.Insurance.PAYMENT_PERIOD,
                                            CURRANCY = "USD",
                                            REFERRAL_FEE = 0,
                                            REFERRAL_INCENTIVE = 0,
                                            SUM_ASSURE = app.Insurance.SUM_ASSURE,
                                            POLICY_STATUS_REMARKS = app.Application.CLIENT_TYPE == Helper.ClientType.REPAYMENT.ToString() ? "REPAYMENT" : "NEW",
                                            CREATED_BY = obj.CreatedBy,
                                            CREATED_ON = tranDate,
                                        };

                                        result = da_micro_policy_detail.SavePolicyDetail(polDetail);
                                        if (result)
                                        {
                                            //save policy rider
                                            if (string.IsNullOrWhiteSpace(app.Rider.PRODUCT_ID))
                                            {
                                                result = true;
                                            }
                                            else
                                            {
                                                polRider = new bl_micro_policy_rider()
                                                {
                                                    POLICY_ID = pol.POLICY_ID,
                                                    PRODUCT_ID = app.Rider.PRODUCT_ID,
                                                    SUM_ASSURE = app.Rider.SUM_ASSURE,
                                                    PREMIUM = app.Rider.PREMIUM,
                                                    ANNUAL_PREMIUM = app.Rider.ANNUAL_PREMIUM,
                                                    DISCOUNT_AMOUNT = app.Rider.DISCOUNT_AMOUNT,
                                                    TOTAL_AMOUNT = app.Rider.TOTAL_AMOUNT,
                                                    CREATED_BY = obj.CreatedBy,
                                                    CREATED_ON = tranDate,
                                                };
                                                result = da_micro_policy_rider.SaveRider(polRider);
                                            }
                                            if (result)
                                            {
                                                //save polPayment
                                                double referralFee = 0;
                                                double incentive = 0;
                                                #region calculate commission
                                                da_micro_production_commission_config commConfig = new da_micro_production_commission_config();
                                                bl_micro_product_commission_config commObj = commConfig.GetProductionCommConfig(pol.CHANNEL_ITEM_ID, pol.PRODUCT_ID, bl_micro_product_commission_config.CommissionTypeOption.ReferralFee, app.Application.CLIENT_TYPE);
                                                if (!String.IsNullOrWhiteSpace(commObj.ProductId))
                                                {
                                                    if (commObj.Status == 1 && app.Application.APPLICATION_DATE <= commObj.EffectiveTo)
                                                    {
                                                        if (commObj.ValueType == bl_micro_product_commission_config.ValueTypeOption.Fix)
                                                        {
                                                            referralFee = commObj.Value;
                                                        }
                                                        else if (commObj.ValueType == bl_micro_product_commission_config.ValueTypeOption.Percentage)
                                                        {
                                                            referralFee = ((polDetail.TOTAL_AMOUNT + polRider.TOTAL_AMOUNT) * commObj.Value) / 100;
                                                        }
                                                    }
                                                }
                                                commObj = new bl_micro_product_commission_config();
                                                commObj = commConfig.GetProductionCommConfig(pol.CHANNEL_ITEM_ID, pol.PRODUCT_ID, bl_micro_product_commission_config.CommissionTypeOption.Incentive, app.Application.CLIENT_TYPE);
                                                if (!String.IsNullOrWhiteSpace(commObj.ProductId))
                                                {
                                                    if (commObj.ValueType == bl_micro_product_commission_config.ValueTypeOption.Fix)
                                                    {
                                                        incentive = commObj.Value;
                                                    }
                                                    else if (commObj.ValueType == bl_micro_product_commission_config.ValueTypeOption.Percentage)
                                                    {
                                                        incentive = ((polDetail.TOTAL_AMOUNT + polRider.TOTAL_AMOUNT) * commObj.Value) / 100;
                                                    }
                                                }

                                                #endregion calculate commission

                                                polpolPayment = new bl_micro_policy_payment()
                                                {
                                                    POLICY_DETAIL_ID = polDetail.POLICY_DETAIL_ID,
                                                    USER_PREMIUM = item.IsMainApplication == true ? obj.CollectedPremium : 0,
                                                    AMOUNT = polDetail.PREMIUM + polRider.PREMIUM,
                                                    DISCOUNT_AMOUNT = polDetail.DISCOUNT_AMOUNT + polRider.DISCOUNT_AMOUNT,
                                                    TOTAL_AMOUNT = polDetail.TOTAL_AMOUNT + polRider.TOTAL_AMOUNT,
                                                    DUE_DATE = polDetail.EFFECTIVE_DATE,
                                                    PAY_DATE = polDetail.ISSUED_DATE,
                                                    NEXT_DUE = Calculation.GetNext_Due(polDetail.EFFECTIVE_DATE.AddYears(1), polDetail.EFFECTIVE_DATE, polDetail.EFFECTIVE_DATE),
                                                    PREMIUM_YEAR = 1,
                                                    PREMIUM_LOT = 1,
                                                    OFFICE_ID = "Head Office",
                                                    PAY_MODE = app.Insurance.PAY_MODE,
                                                    POLICY_STATUS = "IF",
                                                    REFERANCE_TRANSACTION_CODE = obj.ReferenceNo,
                                                    TRANSACTION_TYPE = "",
                                                    REFERRAL_FEE = referralFee,
                                                    REFERRAL_INCENTIVE = incentive,
                                                    CREATED_BY = obj.CreatedBy,
                                                    CREATED_ON = tranDate
                                                };
                                                result = da_micro_policy_payment.SavePayment(polpolPayment);
                                                if (result)
                                                {
                                                    //save beneficiary
                                                    if (app.Beneficiaries.Count > 0)
                                                    {
                                                        foreach (bl_micro_application_beneficiary ben in app.Beneficiaries)
                                                        {
                                                            result = da_micro_policy_beneficiary.SaveBeneficiary(new bl_micro_policy_beneficiary()
                                                            {
                                                                POLICY_ID = pol.POLICY_ID,
                                                                FULL_NAME = ben.FULL_NAME,
                                                                AGE = ben.AGE,
                                                                ADDRESS = ben.ADDRESS,
                                                                RELATION = ben.RELATION,
                                                                PERCENTAGE_OF_SHARE = ben.PERCENTAGE_OF_SHARE,
                                                                CREATED_BY = obj.CreatedBy,
                                                                CREATED_ON = tranDate.AddSeconds(1)
                                                            });
                                                            if (!result)
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        result = false;
                                                    }
                                                    if (result)
                                                    {
                                                        //save address
                                                        polAddress = new bl_micro_policy_address()
                                                        {
                                                            HouseNoKh = cus.HOUSE_NO_KH,
                                                            HouseNoEn = cus.HOUSE_NO_EN,
                                                            StreetNoEn = cus.STREET_NO_EN,
                                                            StreetNoKh = cus.STREET_NO_KH,
                                                            PolicyID = pol.POLICY_ID,
                                                            ProvinceCode = cus.PROVINCE_EN,
                                                            DistrictCode = cus.DISTRICT_EN,
                                                            CommuneCode = cus.COMMUNE_EN,
                                                            VillageCode = cus.VILLAGE_EN,
                                                            CreatedOn = tranDate,
                                                            CreatedBy = obj.CreatedBy
                                                        };

                                                        result = da_micro_policy_address.SaveAddress(polAddress);
                                                        if (result)
                                                        {
                                                            //save approver 
                                                            var approver = AppConfiguration.GetApplicationApprover();
                                                            if (approver != "")
                                                            {
                                                                //save approver
                                                                List<da_report_approver.bl_report_approver> appList = new List<da_report_approver.bl_report_approver>();
                                                                appList = da_report_approver.GetApproverList();
                                                                var myApprove = new da_report_approver.bl_report_approver();

                                                                foreach (da_report_approver.bl_report_approver ap in appList.Where(_ => _.NameEn == approver))
                                                                {
                                                                    myApprove = ap;
                                                                }

                                                                if (!string.IsNullOrWhiteSpace(myApprove.NameKh))
                                                                {
                                                                    result = da_report_approver.InsertApproverPolicy(new da_report_approver.bl_report_approver_policy()
                                                                    {
                                                                        Approver_ID = myApprove.ID,
                                                                        Policy_ID = pol.POLICY_ID,
                                                                        Created_By = obj.CreatedBy,
                                                                        Created_On = tranDate
                                                                    });
                                                                    if (result)
                                                                    {
                                                                        //response = new ResponseExecuteSuccess() { message = "Success", detail = new ResponseParam.IssuePolicy() { PolicyId = pol.POLICY_ID, PolicyNumber = pol.POLICY_NUMBER } };

                                                                        var objSave = new ResponseParam.SavedIssuePolicy()
                                                                        {
                                                                            PolicyId = pol.POLICY_ID,
                                                                            PolicyNumber = pol.POLICY_NUMBER,
                                                                            ApplicationNumber = app.Application.APPLICATION_NUMBER,
                                                                            CustomerId = cus.ID,
                                                                            IsExistingCustomer = isExistingCustomer
                                                                        };
                                                                        IssuePolicies.Add(objSave);

                                                                    }
                                                                    else
                                                                    {
                                                                        //save approver fail
                                                                        response = RoleBackPolicy(isExistingCustomer == true ? "" : cus.ID, pol.POLICY_ID);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    //approver not found
                                                                    response = RoleBackPolicy(isExistingCustomer == true ? "" : cus.ID, pol.POLICY_ID);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                //get approver error
                                                                response = RoleBackPolicy(isExistingCustomer == true ? "" : cus.ID, pol.POLICY_ID);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //save address fail
                                                            response = RoleBackPolicy(isExistingCustomer == true ? "" : cus.ID, pol.POLICY_ID);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //save beneficiary fail
                                                        response = RoleBackPolicy(isExistingCustomer == true ? "" : cus.ID, pol.POLICY_ID);
                                                    }
                                                }
                                                else
                                                {
                                                    //save policy polPayment fail
                                                    response = RoleBackPolicy(isExistingCustomer == true ? "" : cus.ID, pol.POLICY_ID);
                                                }
                                            }
                                            else
                                            {
                                                //save rider fail
                                                response = RoleBackPolicy(isExistingCustomer == true ? "" : cus.ID, pol.POLICY_ID);
                                            }
                                        }
                                        else
                                        {
                                            //save policy detail fail
                                            response = RoleBackPolicy(isExistingCustomer == true ? "" : cus.ID, pol.POLICY_ID);
                                        }
                                    }
                                    else
                                    {
                                        //save policy fail
                                        response = RoleBackPolicy(isExistingCustomer == true ? "" : cus.ID, pol.POLICY_ID);
                                    }
                                }
                                else
                                {
                                    //save customer fail
                                    response = new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = "Save customer fail." };
                                }
                            }
                            else
                            {
                                //application is already issue 
                                response = new ResponseExecuteError() { code = err.ExistingCode, message = "Application :" + item.ApplicationNumber + " was already issued." };
                            }
                        }
                        else
                        {
                            //get data error or not found
                            response = new ResponseExecuteSuccess() { message = "Application is not found.", detail = "" };
                        }

                        step += 1;
                    }

                    if (result)
                    {
                        response = new ResponseExecuteSuccess() { message = "Success", detail = IssuePolicies };
                    }
                    else
                    {
                        foreach (var savedPol in IssuePolicies)
                        {
                            response = RoleBackPolicy(savedPol.IsExistingCustomer == true ? "" : savedPol.CustomerId, savedPol.PolicyId);
                        }

                    }

                    return response;
                }
            }
            catch (Exception ex)
            {

                Log.AddExceptionToLog("PolicyController", "IssueMultiPolicy(RequestParam.IssueMultiPolicy obj)", ex);
                return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
            }

        }
        [Route("api/Policy/IssueMultiPolicy")]
        [HttpPost]
        public object IssueMultiPolicy(PolicyController.RequestParam.IssueMultiPolicy obj)
        {
            ResponseValidateError responseValidateError = this.ValidatIssueMultiPolicy(obj);
            ErrorCode errorCode = new ErrorCode();
            DateTime now = DateTime.Now;
            List<PolicyController.ResponseParam.SavedIssuePolicy> savedIssuePolicyList = new List<PolicyController.ResponseParam.SavedIssuePolicy>();
            try
            {
                if (responseValidateError.detail != null)
                    return (object)responseValidateError;

                bl_application_for_issue applicationForIssue = new bl_application_for_issue();

                int step = 1;
                DateTime effectiveDate = new DateTime(1900, 1, 1);
                bool result = false;
                bool existingCus = false;
                object response = new object();
                bl_micro_customer CUSTOMER = new bl_micro_customer();
                bl_micro_product_config proConfig = new bl_micro_product_config();
                bool breakAll = false;/*tracking error in loop*/
                foreach (PolicyController.RequestParam.ApplicationToBeIssued application in obj.Applications)
                {
                    bl_application_for_issue applicationForIssuePolicy = da_micro_application.GetApplicationForIssuePolicy(application.ApplicationNumber);

                    if (applicationForIssuePolicy != null)
                    {
                        string cusNumber = "";
                        if (step == 1)
                        {
                            proConfig = da_micro_product_config.ProductConfig.GetProductMicroProduct(applicationForIssuePolicy.Insurance.PRODUCT_ID);
                            bl_micro_customer customerByIdNumber = da_micro_customer.GetCustomerByIdNumber(applicationForIssuePolicy.Customer.ID_TYPE, applicationForIssuePolicy.Customer.ID_NUMBER);
                            if (customerByIdNumber.CUSTOMER_NUMBER != null)
                            {
                                existingCus = true;
                                CUSTOMER = customerByIdNumber;
                                result = true;
                            }
                            else
                            {
                                existingCus = false;
                                var cusPrefix = da_micro_customer_prefix.GetLastCustomerPrefix();
                                CUSTOMER = new bl_micro_customer();

                                if (cusPrefix.PREFIX2 == CUSTOMER.LAST_PREFIX)//in the same year
                                {
                                    CUSTOMER.SEQ = CUSTOMER.LAST_SEQ + 1;
                                }
                                else
                                {
                                    CUSTOMER.SEQ = 1;
                                }
                                cusNumber = cusPrefix.PREFIX1 + cusPrefix.PREFIX2 + CUSTOMER.SEQ.ToString(cusPrefix.DIGITS);
                                CUSTOMER.CUSTOMER_NUMBER = cusNumber;
                                CUSTOMER.CUSTOMER_TYPE = "INDIVIDUAL";
                                CUSTOMER.ID_TYPE = applicationForIssuePolicy.Customer.ID_TYPE + "";
                                CUSTOMER.ID_NUMBER = applicationForIssuePolicy.Customer.ID_NUMBER;
                                CUSTOMER.FIRST_NAME_IN_ENGLISH = applicationForIssuePolicy.Customer.FIRST_NAME_IN_ENGLISH;
                                CUSTOMER.LAST_NAME_IN_ENGLISH = applicationForIssuePolicy.Customer.LAST_NAME_IN_ENGLISH;
                                CUSTOMER.FIRST_NAME_IN_KHMER = applicationForIssuePolicy.Customer.FIRST_NAME_IN_KHMER;
                                CUSTOMER.LAST_NAME_IN_KHMER = applicationForIssuePolicy.Customer.LAST_NAME_IN_KHMER;
                                CUSTOMER.GENDER = applicationForIssuePolicy.Customer.GENDER + "";
                                CUSTOMER.DATE_OF_BIRTH = applicationForIssuePolicy.Customer.DATE_OF_BIRTH;
                                CUSTOMER.NATIONALITY = applicationForIssuePolicy.Customer.NATIONALITY;
                                CUSTOMER.MARITAL_STATUS = applicationForIssuePolicy.Customer.MARITAL_STATUS;
                                CUSTOMER.OCCUPATION = applicationForIssuePolicy.Customer.OCCUPATION;
                                CUSTOMER.HOUSE_NO_KH = applicationForIssuePolicy.Customer.HOUSE_NO_EN;
                                CUSTOMER.STREET_NO_KH = applicationForIssuePolicy.Customer.STREET_NO_EN;
                                CUSTOMER.VILLAGE_KH = applicationForIssuePolicy.Customer.VILLAGE_EN;
                                CUSTOMER.COMMUNE_KH = applicationForIssuePolicy.Customer.COMMUNE_EN;
                                CUSTOMER.DISTRICT_KH = applicationForIssuePolicy.Customer.DISTRICT_EN;
                                CUSTOMER.PROVINCE_KH = applicationForIssuePolicy.Customer.PROVINCE_EN;
                                CUSTOMER.HOUSE_NO_EN = applicationForIssuePolicy.Customer.HOUSE_NO_EN;
                                CUSTOMER.STREET_NO_EN = applicationForIssuePolicy.Customer.STREET_NO_EN;
                                CUSTOMER.VILLAGE_EN = applicationForIssuePolicy.Customer.VILLAGE_EN;
                                CUSTOMER.COMMUNE_EN = applicationForIssuePolicy.Customer.COMMUNE_EN;
                                CUSTOMER.DISTRICT_EN = applicationForIssuePolicy.Customer.DISTRICT_EN;
                                CUSTOMER.PROVINCE_EN = applicationForIssuePolicy.Customer.PROVINCE_EN;
                                CUSTOMER.PHONE_NUMBER1 = applicationForIssuePolicy.Customer.PHONE_NUMBER1;
                                CUSTOMER.EMAIL1 = applicationForIssuePolicy.Customer.EMAIL1;
                                CUSTOMER.CREATED_BY = obj.CreatedBy;
                                CUSTOMER.CREATED_ON = now;
                                CUSTOMER.STATUS = 1;
                                result = da_micro_customer.SaveCustomer(CUSTOMER);
                            }
                        }
                        else
                            result = true;
                        if (string.IsNullOrWhiteSpace(applicationForIssuePolicy.PolicyNumber))
                        {
                          
                            bl_micro_policy POLICY = new bl_micro_policy(applicationForIssuePolicy.Insurance.PRODUCT_ID);
                            bl_micro_policy_detail polDetail = new bl_micro_policy_detail();
                            bl_micro_policy_rider polRider = new bl_micro_policy_rider();
                            bl_micro_policy_payment polPayment = new bl_micro_policy_payment();
                            bl_micro_policy_address polAddress = new bl_micro_policy_address();
                            bl_micro_policy_prefix polPrefix = da_micro_policy_prefix.GetLastPolicyPrefix(applicationForIssuePolicy.Insurance.PRODUCT_ID);
                            bl_micro_application_beneficiary.PrimaryBeneciary primaryBeneciary = applicationForIssuePolicy.PrimaryBeneciary;
                            string polNumber = "";

                            bool isMatched = polPrefix.PRODUCT_ID.Contains(POLICY.PRODUCT_ID);
                            if (!isMatched)
                            {
                                result = false;
                                breakAll = true;
                                response = (object)new ResponseExecuteSuccess()
                                {
                                    message = "Policy Prefix Number Is Not Matched.",
                                    detail = ""
                                };
                            }

                            if (result)
                            {
                                if (POLICY.LAST_PREFIX == polPrefix.PREFIX2 && POLICY.LAST_PREFIX1==polPrefix.PREFIX1)//in the same year
                                {
                                    POLICY.SEQ = POLICY.LAST_SEQ + 1;
                                }
                                else
                                {
                                    POLICY.SEQ = 1;
                                }
                                polNumber = polPrefix.PREFIX1 + polPrefix.PREFIX2 + POLICY.SEQ.ToString(polPrefix.DIGITS);
                                POLICY.POLICY_NUMBER = polNumber;
                                POLICY.POLICY_TYPE = "COR";
                                POLICY.APPLICATION_ID = applicationForIssuePolicy.Application.APPLICATION_ID;
                                POLICY.CUSTOMER_ID = CUSTOMER.ID;
                                POLICY.PRODUCT_ID = applicationForIssuePolicy.Insurance.PRODUCT_ID;
                                POLICY.CHANNEL_ID = applicationForIssuePolicy.Application.CHANNEL_ID;
                                POLICY.CHANNEL_ITEM_ID = applicationForIssuePolicy.Application.CHANNEL_ITEM_ID;
                                POLICY.CHANNEL_LOCATION_ID = applicationForIssuePolicy.Application.CHANNEL_LOCATION_ID;
                                POLICY.AGENT_CODE = applicationForIssuePolicy.Application.SALE_AGENT_ID;
                                POLICY.CREATED_ON = now;
                                POLICY.CREATED_BY = obj.CreatedBy;
                                POLICY.POLICY_STATUS = "IF";
                                POLICY.RenewFromPolicy = applicationForIssuePolicy.Application.RENEW_FROM_POLICY;
                                result = da_micro_policy.SavePolicy(POLICY);
                                if (result)
                                {
                                    PolicyController.ResponseParam.SavedIssuePolicy savedIssuePolicy = new PolicyController.ResponseParam.SavedIssuePolicy()
                                    {
                                        PolicyId = POLICY.POLICY_ID,
                                        PolicyNumber = POLICY.POLICY_NUMBER,
                                        ApplicationNumber = applicationForIssuePolicy.Application.APPLICATION_NUMBER,
                                        CustomerId = CUSTOMER.ID,
                                        IsExistingCustomer = existingCus
                                    };
                                    savedIssuePolicyList.Add(savedIssuePolicy);

                                    if (effectiveDate.Year == 1900)
                                    {
                                        if (application.ClientType != Helper.ClientType.REPAYMENT.ToString())
                                        {
                                            effectiveDate = Helper.FormatDateTime(obj.EffectiveDate);
                                        }
                                        else
                                        {
                                            effectiveDate = Helper.FormatDateTime(obj.EffectiveDate).AddYears(1);

                                        }
                                    }
                                    else
                                    {
                                        effectiveDate = application.ClientType == Helper.ClientType.REPAYMENT.ToString() ? effectiveDate.AddYears(1) : Helper.FormatDateTime(obj.EffectiveDate);
                                    }

                                    int cusAge = Calculation.Culculate_Customer_Age(CUSTOMER.DATE_OF_BIRTH.ToString("dd/MM/yyyy"), applicationForIssuePolicy.Application.APPLICATION_DATE);

                                    DateTime maturityDate = applicationForIssuePolicy.Insurance.COVER_TYPE == "Y" ? effectiveDate.AddYears(1) : effectiveDate.AddMonths(applicationForIssuePolicy.Insurance.TERME_OF_COVER);

                                    DateTime issueDate = Helper.FormatDateTime(obj.IssueDate);
                                    polDetail.POLICY_ID = POLICY.POLICY_ID;
                                    polDetail.EFFECTIVE_DATE = effectiveDate;
                                    polDetail.ISSUED_DATE = issueDate;
                                    polDetail.MATURITY_DATE = maturityDate;
                                    polDetail.EXPIRY_DATE = maturityDate.AddDays(-1);
                                    polDetail.PREMIUM = applicationForIssuePolicy.Insurance.PREMIUM;
                                    polDetail.ANNUAL_PREMIUM = applicationForIssuePolicy.Insurance.ANNUAL_PREMIUM;
                                    polDetail.DISCOUNT_AMOUNT = applicationForIssuePolicy.Insurance.DISCOUNT_AMOUNT;
                                    polDetail.TOTAL_AMOUNT = applicationForIssuePolicy.Insurance.TOTAL_AMOUNT;
                                    polDetail.COVER_YEAR = applicationForIssuePolicy.Insurance.TERME_OF_COVER;
                                    polDetail.PAY_YEAR = applicationForIssuePolicy.Insurance.PAYMENT_PERIOD;
                                    polDetail.PAYMENT_CODE = applicationForIssuePolicy.Insurance.PAYMENT_CODE;
                                    polDetail.PAY_MODE = applicationForIssuePolicy.Insurance.PAY_MODE;
                                    polDetail.AGE = cusAge;
                                    polDetail.COVER_UP_TO_AGE = cusAge + applicationForIssuePolicy.Insurance.TERME_OF_COVER;
                                    polDetail.PAY_UP_TO_AGE = cusAge + applicationForIssuePolicy.Insurance.PAYMENT_PERIOD;
                                    polDetail.CURRANCY = "USD";
                                    polDetail.REFERRAL_FEE = 0.0;
                                    polDetail.REFERRAL_INCENTIVE = 0.0;
                                    polDetail.SUM_ASSURE = applicationForIssuePolicy.Insurance.SUM_ASSURE;
                                    polDetail.POLICY_STATUS_REMARKS = applicationForIssuePolicy.Application.CLIENT_TYPE == Helper.ClientType.REPAYMENT.ToString() ? "REPAYMENT" : "NEW";
                                    polDetail.CREATED_BY = obj.CreatedBy;
                                    polDetail.CREATED_ON = now;
                                    polDetail.COVER_TYPE = (bl_micro_product_config.PERIOD_TYPE)Enum.Parse(typeof(bl_micro_product_config.PERIOD_TYPE), applicationForIssuePolicy.Insurance.COVER_TYPE);

                                    result = da_micro_policy_detail.SavePolicyDetail(polDetail);
                                    if (result)
                                    {
                                        if (string.IsNullOrWhiteSpace(applicationForIssuePolicy.Rider.PRODUCT_ID))
                                        {
                                            result = true;
                                        }
                                        else
                                        {
                                            polRider = new bl_micro_policy_rider()
                                            {
                                                POLICY_ID = POLICY.POLICY_ID,
                                                PRODUCT_ID = applicationForIssuePolicy.Rider.PRODUCT_ID,
                                                SUM_ASSURE = applicationForIssuePolicy.Rider.SUM_ASSURE,
                                                PREMIUM = applicationForIssuePolicy.Rider.PREMIUM,
                                                ANNUAL_PREMIUM = applicationForIssuePolicy.Rider.ANNUAL_PREMIUM,
                                                DISCOUNT_AMOUNT = applicationForIssuePolicy.Rider.DISCOUNT_AMOUNT,
                                                TOTAL_AMOUNT = applicationForIssuePolicy.Rider.TOTAL_AMOUNT,
                                                CREATED_BY = obj.CreatedBy,
                                                CREATED_ON = now
                                            };
                                            result = da_micro_policy_rider.SaveRider(polRider);
                                        }
                                        if (result)
                                        {
                                            double referralFee = 0.0;
                                            double incentive = 0.0;
                                            da_micro_production_commission_config daComm = new da_micro_production_commission_config();
                                            bl_micro_product_commission_config comm = daComm.GetProductionCommConfig(POLICY.CHANNEL_ITEM_ID, POLICY.PRODUCT_ID, bl_micro_product_commission_config.CommissionTypeOption.ReferralFee, applicationForIssuePolicy.Application.CLIENT_TYPE);
                                            if (!string.IsNullOrWhiteSpace(comm.ProductId) && comm.Status == 1 && applicationForIssuePolicy.Application.APPLICATION_DATE <= comm.EffectiveTo)
                                            {
                                                if (comm.ValueType == bl_micro_product_commission_config.ValueTypeOption.Fix)
                                                    referralFee = comm.Value;
                                                else if (comm.ValueType == bl_micro_product_commission_config.ValueTypeOption.Percentage)
                                                    referralFee = (polDetail.TOTAL_AMOUNT + polRider.TOTAL_AMOUNT) * comm.Value / 100.0;
                                            }
                                            bl_micro_product_commission_config commIncentive = daComm.GetProductionCommConfig(POLICY.CHANNEL_ITEM_ID, POLICY.PRODUCT_ID, bl_micro_product_commission_config.CommissionTypeOption.Incentive, applicationForIssuePolicy.Application.CLIENT_TYPE);
                                            if (!string.IsNullOrWhiteSpace(commIncentive.ProductId))
                                            {
                                                if (commIncentive.ValueType == bl_micro_product_commission_config.ValueTypeOption.Fix)
                                                    incentive = commIncentive.Value;
                                                else if (commIncentive.ValueType == bl_micro_product_commission_config.ValueTypeOption.Percentage)
                                                    incentive = (polDetail.TOTAL_AMOUNT + polRider.TOTAL_AMOUNT) * commIncentive.Value / 100.0;
                                            }
                                            polPayment.POLICY_DETAIL_ID = polDetail.POLICY_DETAIL_ID;
                                            polPayment.USER_PREMIUM = application.IsMainApplication ? obj.CollectedPremium : 0.0;
                                            polPayment.AMOUNT = polDetail.PREMIUM + polRider.PREMIUM;
                                            polPayment.DISCOUNT_AMOUNT = polDetail.DISCOUNT_AMOUNT + polRider.DISCOUNT_AMOUNT;
                                            polPayment.TOTAL_AMOUNT = polDetail.TOTAL_AMOUNT + polRider.TOTAL_AMOUNT;
                                            polPayment.DUE_DATE = polDetail.EFFECTIVE_DATE;
                                            polPayment.PAY_DATE = polDetail.ISSUED_DATE;
                                            polPayment.NEXT_DUE = Calculation.GetNext_Due(polDetail.EFFECTIVE_DATE.AddYears(1), polDetail.EFFECTIVE_DATE, polDetail.EFFECTIVE_DATE);
                                            polPayment.PREMIUM_YEAR = 1;
                                            polPayment.PREMIUM_LOT = 1;
                                            polPayment.OFFICE_ID = "Head Office";
                                            polPayment.PAY_MODE = applicationForIssuePolicy.Insurance.PAY_MODE;
                                            polPayment.POLICY_STATUS = "IF";
                                            polPayment.REFERANCE_TRANSACTION_CODE = obj.ReferenceNo;
                                            polPayment.TRANSACTION_TYPE = "";
                                            polPayment.REFERRAL_FEE = referralFee;
                                            polPayment.REFERRAL_INCENTIVE = incentive;
                                            polPayment.CREATED_BY = obj.CreatedBy;
                                            polPayment.CREATED_ON = now;
                                            result = da_micro_policy_payment.SavePayment(polPayment);
                                            if (result)
                                            {
                                                if (applicationForIssuePolicy.Beneficiaries.Count > 0)
                                                {
                                                    foreach (bl_micro_application_beneficiary beneficiary in applicationForIssuePolicy.Beneficiaries)
                                                    {
                                                        result = da_micro_policy_beneficiary.SaveBeneficiary(new bl_micro_policy_beneficiary()
                                                        {
                                                            POLICY_ID = POLICY.POLICY_ID,
                                                            FULL_NAME = beneficiary.FULL_NAME,
                                                            AGE = beneficiary.AGE,
                                                            ADDRESS = beneficiary.ADDRESS,
                                                            RELATION = beneficiary.RELATION,
                                                            PERCENTAGE_OF_SHARE = beneficiary.PERCENTAGE_OF_SHARE,
                                                            CREATED_BY = obj.CreatedBy,
                                                            CREATED_ON = now.AddSeconds(1.0),
                                                            BirthDate = beneficiary.DOB,
                                                            Gender = beneficiary.Gender,
                                                            IdType = beneficiary.IdType,
                                                            IdNo = beneficiary.IdNo
                                                        });
                                                        if (!result)
                                                        {
                                                            breakAll = true;
                                                            break;
                                                        }
                                                    }
                                                }
                                                if (proConfig.ProductType.ToUpper() == bl_micro_product_config.PRODUCT_TYPE.MICRO_LOAN.ToString() && result)
                                                    result = da_micro_policy_beneficiary.beneficiary_primary.Save(new bl_micro_policy_beneficiary.beneficiary_primary()
                                                    {
                                                        PolicyId = POLICY.POLICY_ID,
                                                        FullName = primaryBeneciary.FullName,
                                                        LoanNumber = primaryBeneciary.LoanNumber,
                                                        Address = primaryBeneciary.Address,
                                                        CreatedBy = obj.CreatedBy,
                                                        CreatedOn = now,
                                                        CreatedRemarks = ""
                                                    });
                                                if (result)
                                                {
                                                    result = da_micro_policy_address.SaveAddress(new bl_micro_policy_address()
                                                    {
                                                        HouseNoKh = CUSTOMER.HOUSE_NO_KH,
                                                        HouseNoEn = CUSTOMER.HOUSE_NO_EN,
                                                        StreetNoEn = CUSTOMER.STREET_NO_EN,
                                                        StreetNoKh = CUSTOMER.STREET_NO_KH,
                                                        PolicyID = POLICY.POLICY_ID,
                                                        ProvinceCode = CUSTOMER.PROVINCE_EN,
                                                        DistrictCode = CUSTOMER.DISTRICT_EN,
                                                        CommuneCode = CUSTOMER.COMMUNE_EN,
                                                        VillageCode = CUSTOMER.VILLAGE_EN,
                                                        CreatedOn = now,
                                                        CreatedBy = obj.CreatedBy
                                                    });
                                                    if (result)
                                                    {
                                                        string approver = AppConfiguration.GetApplicationApprover();
                                                        if (approver != "")
                                                        {

                                                            //save approver
                                                            List<da_report_approver.bl_report_approver> appList = new List<da_report_approver.bl_report_approver>();
                                                            appList = da_report_approver.GetApproverList();
                                                            var myApprove = new da_report_approver.bl_report_approver();

                                                            foreach (da_report_approver.bl_report_approver ap in appList.Where(_ => _.NameEn == approver))
                                                            {
                                                                myApprove = ap;
                                                            }

                                                            if (!string.IsNullOrWhiteSpace(myApprove.NameKh))
                                                            {

                                                                result = da_report_approver.InsertApproverPolicy(new da_report_approver.bl_report_approver_policy()
                                                                {
                                                                    Approver_ID = myApprove.ID,
                                                                    Policy_ID = POLICY.POLICY_ID,
                                                                    Created_By = obj.CreatedBy,
                                                                    Created_On = now
                                                                });
                                                                if (!result)
                                                                {
                                                                    breakAll = true;
                                                                   // response = this.RoleBackPolicy(existingCus ? "" : CUSTOMER.ID, POLICY.POLICY_ID);
                                                                }
                                                            }
                                                            else/*approver not found*/
                                                            {
                                                                Log.AddExceptionToLog( $"Error Function [IssueMultiPolicy(RequestParam.IssueMultiPolicy obj)] in class [{nameof(PolicyController)}], detail:Approver Not Found.",obj.CreatedBy );
                                                                breakAll = true;
                                                                result = false;
                                                                //response = this.RoleBackPolicy(existingCus ? "" : CUSTOMER.ID, POLICY.POLICY_ID);
                                                            }
                                                        }
                                                        else/*approver configuration not found*/
                                                        {
                                                            Log.AddExceptionToLog($"Error Function [IssueMultiPolicy(RequestParam.IssueMultiPolicy obj)] in class [{nameof(PolicyController)}], detail:Approver configuration Not Found.", obj.CreatedBy);
                                                            breakAll = true;
                                                            result = false;
                                                           // response = this.RoleBackPolicy(existingCus ? "" : CUSTOMER.ID, POLICY.POLICY_ID);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        breakAll = true;
                                                       // response = this.RoleBackPolicy(existingCus ? "" : CUSTOMER.ID, POLICY.POLICY_ID);
                                                    }
                                                }
                                                else
                                                {
                                                    breakAll = true;
                                                   // response = this.RoleBackPolicy(existingCus ? "" : CUSTOMER.ID, POLICY.POLICY_ID);
                                                }
                                            }
                                            else
                                            {
                                                breakAll = true;
                                               // response = this.RoleBackPolicy(existingCus ? "" : CUSTOMER.ID, POLICY.POLICY_ID);
                                            }
                                        }
                                        else
                                        {
                                            breakAll = true;
                                           // response = this.RoleBackPolicy(existingCus ? "" : CUSTOMER.ID, POLICY.POLICY_ID);
                                        }
                                    }
                                    else
                                    {
                                        breakAll = true;
                                      //  response = this.RoleBackPolicy(existingCus ? "" : CUSTOMER.ID, POLICY.POLICY_ID);
                                    }
                                }
                                else
                                {
                                    breakAll = true;
                                   // response = this.RoleBackPolicy(existingCus ? "" : CUSTOMER.ID, POLICY.POLICY_ID);
                                }
                            }
                            else
                            {
                                breakAll = true;
                                response = (object)new ResponseExecuteError()
                                {
                                    code = errorCode.UnexpectedErrorCode,
                                    message = "Save customer fail."
                                };
                            }
                        }
                        else
                        {
                            breakAll = true;
                            response = (object)new ResponseExecuteError()
                            {
                                code = errorCode.ExistingCode,
                                message = $"Application :{application.ApplicationNumber} was already issued."
                            };
                        }
                    }
                    else
                    {
                        response = (object)new ResponseExecuteSuccess()
                        {
                            message = "Application is not found.",
                            detail = (object)""
                        };
                    }
                    if (breakAll)
                        break;/*exist from main loop*/
                    step++;
                }

                if (result)
                {
                    response = (object)new ResponseExecuteSuccess()
                    {
                        message = "Success",
                        detail = (object)savedIssuePolicyList
                    };
                }
                else
                {
                    foreach (PolicyController.ResponseParam.SavedIssuePolicy savedIssuePolicy in savedIssuePolicyList)
                        response = this.RoleBackPolicy(savedIssuePolicy.IsExistingCustomer ? "" : savedIssuePolicy.CustomerId, savedIssuePolicy.PolicyId);
                }
                return response;
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog(nameof(PolicyController), "IssueMultiPolicy(RequestParam.IssueMultiPolicy obj)", ex);
                object err = new object();
                foreach (PolicyController.ResponseParam.SavedIssuePolicy savedIssuePolicy in savedIssuePolicyList)
                    err = this.RoleBackPolicy(savedIssuePolicy.IsExistingCustomer ? "" : savedIssuePolicy.CustomerId, savedIssuePolicy.PolicyId);
                return err;
            }
        }

        private object RoleBackPolicy(string customerId, string policyId)
        {

            var result = da_banca.RoleBackIssuePolicy(customerId, policyId);
            var err = new ErrorCode();
            if (result)
            {

                return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = "Issue policy fail, system roleback successfully." };
            }
            else
            {
                return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = "Issue policy fail, system roleback fail." };
            }
        }
        public class RequestParam
        {
            public class Customer
            {
                public string CustomerType { get; set; }
                public int IdType { get; set; }
                public string IdNumber { get; set; }
                public string FirstNameEn { get; set; }
                public string LastNameEn { get; set; }
                public string FirstNameKh { get; set; }
                public string LastNameKh { get; set; }
                public int Gender { get; set; }
                public DateTime Dob { get; set; }
                public string Nationality { get; set; }
                public string MaritalStatus { get; set; }
                public string Occupation { get; set; }
                public string HouseNo { get; set; }
                public string StreetNo { get; set; }
                public string Village { get; set; }
                public string Commune
                {
                    get; set;
                }
                public string District { get; set; }
                public string Province
                {
                    get; set;
                }
                public string Email { get; set; }
                public string CreatedBy { get; set; }

            }

            public class Policy
            {
                public string PolicyType { get; set; }
                public string ApplictionId { get; set; }
                public string CustomerId { get; set; }
                public string ChannelId { get; set; }
                public string ChannelItemId { get; set; }
                public string ChannelLocationId { get; set; }
                public string AgentCode { get; set; }
                public string RenewalFromPolicy { get; set; }

            }
            public class PolicyDetail
            {

                public DateTime IssueDate { get; set; }
                public DateTime EffectiveDate { get; set; }
                public int IssueAge { get; set; }
                public double SumAssure { get; set; }
                public int PayMode { get; set; }
                public string polPaymentCode { get; set; }
                public double Premium { get; set; }
                public double AnnualPremium { get; set; }
                public double DiscountAmount { get; set; }
                public double TotalAmount { get; set; }
                public int CoverYear { get; set; }
                public int PayYear { get; set; }
                public string PolicyStatusRemarks { get; set; }


            }
            public class PolicyRider
            {
                public string PolicyId { get; set; }
                public string ProductId { get; set; }
                public double SumAssure { get; set; }
                public double AnnualPremium { get; set; }
                public double DiscountAmount { get; set; }
                public double TotalAmount { get; set; }

            }
            public class PolicypolPayment
            {

                public DateTime DueDate { get; set; }
                public DateTime PayDate { get; set; }
                public int PremiumYear { get; set; }
                public int PremiumLot { get; set; }
                public double UserPremium { get; set; }
                public double Amount { get; set; }
                public double DiscountAmount { get; set; }
                public double TotalAmount { get; set; }
                public string TransactionType { get; set; }
                public string ReferenceTransactionCode { get; set; }

            }
            public class PolicyBeneficiaries : ApplicationFormController.RequestParameters.BeneficiaryObject
            {

            }
            public class PolicyAddress
            {
                public string HouseNo { get; set; }
                public string StreetNo { get; set; }
                public string Province { get; set; }
                public string District { get; set; }
                public string Commune
                {
                    get; set;
                }
            }

            public class IssuePolicy
            {
                //public Customer Customer { get; set; }
                //public Policy Policy { get; set; }
                //public PolicyDetail PolicyDetail { get; set; }
                //public PolicyRider PolicyRide { get; set; }
                //public PolicypolPayment PolicypolPayment { get; set; }
                //public List<PolicyBeneficiaries> Beneficiaries { get; set; }
                //public PolicyAddress Address { get; set; }
                public string ApplicationNumber { get; set; }
                /// <summary>
                /// [DD-MM-YYYY]
                /// </summary>
                public string IssueDate { get; set; }
                /// <summary>
                /// [DD-MM-YYYY]
                /// </summary>
                public string EffectiveDate { get; set; }
                public double CollectedPremium { get; set; }
                public string ReferenceNo { get; set; }
                public string CreatedBy { get; set; }
            }

            public class IssueMultiPolicy
            {

                public List<ApplicationToBeIssued> Applications { get; set; }
                /// <summary>
                /// [DD-MM-YYYY]
                /// </summary>
                public string IssueDate { get; set; }
                /// <summary>
                /// [DD-MM-YYYY]
                /// </summary>
                public string EffectiveDate { get; set; }
                public double CollectedPremium { get; set; }
                public string ReferenceNo { get; set; }
                public string CreatedBy { get; set; }
            }

            public class ApplicationToBeIssued
            {
                public string ApplicationNumber { get; set; }
                public bool IsMainApplication { get; set; }
                public string ClientType { get; set; }
            }
        }

        public class ResponseParam
        {
            public class Customer
            {
                public string CustomerNumber { get; set; }
                public string CustomerId { get; set; }
            }
            public class Policy
            {
                public string PolicyNumber { get; set; }
                public string PolicyId { get; set; }
            }
            public class PolicyDetail
            {
                public string PolicyDetailId { get; set; }
            }
            public class PolicyRider
            {
                public string Id { get; set; }
            }
            public class PolicypolPayment
            {
                public string PolicypolPaymentId { get; set; }
            }
            public class PolicyBeneficiaries
            {
                public string Id { get; set; }
            }

            public class IssuePolicy
            {
                public string PolicyId { get; set; }
                public string PolicyNumber { get; set; }
            }

            public class IssueMultiPolicy
            {
                public List<SavedIssuePolicy> SavedPolicies { get; set; }
            }

            public class SavedIssuePolicy
            {
                public string PolicyId { get; set; }
                public string PolicyNumber { get; set; }
                public string ApplicationNumber { get; set; }
                public string CustomerId { get; set; }
                public bool IsExistingCustomer { get; set; }
            }
        }


        public ResponseValidateError ValidatIssue(RequestParam.IssuePolicy request)
        {

            var validate = new ResponseValidateError();
            var vList = new List<ValidateRequest>();
            try
            {
                if (request == null)
                {
                    vList.Add(new ValidateRequest() { field = "Issue Policy", message = "Object reference not set to an instance of an object." });
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(request.ApplicationNumber))
                    {
                        vList.Add(new ValidateRequest() { field = "ApplicationNumber", message = "Application Number is required." });
                    }
                    else
                    {
                        bl_application_for_issue app = new bl_application_for_issue();
                        app = da_micro_application.GetApplicationForIssuePolicy(request.ApplicationNumber);
                        double totalBasic = 0.0;
                        double totalRider = 0.0;
                        double totalAmount = 0.0;
                        if (app != null)
                        {
                            totalBasic = app.Insurance.TOTAL_AMOUNT;
                            if (app.Rider != null)
                            {
                                totalRider = app.Rider.TOTAL_AMOUNT;
                            }
                            totalAmount = totalBasic + totalRider;
                            if (request.CollectedPremium != totalBasic + totalRider)
                            {
                                vList.Add(new ValidateRequest() { field = "CollectedPremium", message = "Collected premium must be equal to total amount." });
                            }
                        }
                    }
                    
                    if (!Helper.IsDate(request.EffectiveDate))
                    {
                        vList.Add(new ValidateRequest() { field = "EffecitveDate", message = "Effective date is required as date." });
                    }
                    if (!Helper.IsDate(request.IssueDate))
                    {
                        vList.Add(new ValidateRequest() { field = "IssueDate", message = "Issue date is required as date." });
                    }
                    if (request.CollectedPremium.GetType() != typeof(double))
                    {
                        vList.Add(new ValidateRequest() { field = "CollectedPremium", message = "Collected Premium is required as double." });
                    }
                    else
                    {
                        if (request.CollectedPremium == 0)
                        {
                            vList.Add(new ValidateRequest() { field = "CollectedPremium", message = "Collected Premium must be greater than zero." });
                        }
                    }
                    if (string.IsNullOrWhiteSpace(request.ReferenceNo))
                    {
                        vList.Add(new ValidateRequest() { field = "RefereranceNo", message = "Refererance number is required." });
                    }
                }

                if (vList.Count > 0)
                {
                    validate = new ResponseValidateError()
                    {
                        message = "Validation error, The request has " + vList.Count + " errors.",
                        detail = vList
                    };
                }
            }
            catch (Exception ex)
            {
                vList.Add(new ValidateRequest()
                {
                    field = "ERROR",
                    message = "Unexpected error."
                });
                validate = new ResponseValidateError() { message = "Validation error", detail = vList };
                Log.AddExceptionToLog("PolicyController", "ValidatIssue(RequestParam.IssuePolicy reqeust)", ex);
            }
            return validate;
        }

        public ResponseValidateError ValidatIssueMultiPolicy(RequestParam.IssueMultiPolicy request)
        {

            var validate = new ResponseValidateError();
            var vList = new List<ValidateRequest>();
            try
            {
                if (request == null)
                {
                    vList.Add(new ValidateRequest() { field = "Issue Policy", message = "Object reference not set to an instance of an object." });
                }
                else
                {
                    if (request.Applications.Count == 0)
                    {
                        vList.Add(new ValidateRequest() { field = "Applications", message = "Applications list is required." });
                    }
                    else
                    {
                        bl_application_for_issue app = new bl_application_for_issue();
                        decimal totalBasic =0;
                        decimal totalRider = 0;
                        decimal totalAmount = 0;
                        foreach (var obj in request.Applications)
                        {
                            app = da_micro_application.GetApplicationForIssuePolicy(obj.ApplicationNumber);
                            if (app != null)
                            {
                                totalBasic +=  decimal.Parse( app.Insurance.TOTAL_AMOUNT+"");
                                if (app.Rider != null)
                                {
                                    totalRider += decimal.Parse(app.Rider.TOTAL_AMOUNT+"");
                                }

                            }
                        }
                        totalAmount = totalBasic + totalRider;
                        if (decimal.Parse( request.CollectedPremium+"") != totalBasic + totalRider)
                        {
                            vList.Add(new ValidateRequest() { field = "CollectedPremium", message = "Collected premium must be equal to total amount." });
                        }


                    }

                    if (!Helper.IsDate(request.EffectiveDate))
                    {
                        vList.Add(new ValidateRequest() { field = "EffecitveDate", message = "Effective date is required as date." });
                    }
                    if (!Helper.IsDate(request.IssueDate))
                    {
                        vList.Add(new ValidateRequest() { field = "IssueDate", message = "Issue date is required as date." });
                    }
                    else
                    {
                        //check back date
                        DateTime date = Helper.FormatDateTime(request.IssueDate);
                        int d = date.Subtract(DateTime.Now.Date).Days;
                        if (d > 0)
                        {
                            vList.Add(new ValidateRequest() { field = "IssueDate", message = "Issue date is greater than current system date is not allowed." });
                        }
                        else if (d < -3)
                        {
                            vList.Add(new ValidateRequest() { field = "IssueDate", message = "Back date is allowed only 3 days." });

                        }

                    }


                    if (request.CollectedPremium.GetType() != typeof(double))
                    {
                        vList.Add(new ValidateRequest() { field = "CollectedPremium", message = "Collected Premium is required as double." });
                    }
                    else
                    {
                        if (request.CollectedPremium == 0)
                        {
                            vList.Add(new ValidateRequest() { field = "CollectedPremium", message = "Collected Premium must be greater than zero." });
                        }
                    }
                    if (string.IsNullOrWhiteSpace(request.ReferenceNo))
                    {
                        vList.Add(new ValidateRequest() { field = "RefereranceNo", message = "Refererance number is required." });
                    }
                }

                if (vList.Count > 0)
                {
                    validate = new ResponseValidateError()
                    {
                        message = "Validation error, The request has " + vList.Count + " errors.",
                        detail = vList
                    };
                }
            }
            catch (Exception ex)
            {
                vList.Add(new ValidateRequest()
                {
                    field = "ERROR",
                    message = "Unexpected error."
                });
                validate = new ResponseValidateError() { message = "Validation error", detail = vList };
                Log.AddExceptionToLog("PolicyController", "ValidatIssueMultiPolicy(RequestParam.IssueMultiPolicy request)", ex);
            }
            return validate;
        }
    }
}
