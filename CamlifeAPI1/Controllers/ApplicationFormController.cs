using CamlifeAPI1.Class;
using CamlifeAPI1.Class.Application;
using iTextSharp.text;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using NPOI.DDF;
using NPOI.OpenXmlFormats.Dml;
using NPOI.OpenXmlFormats.Spreadsheet;
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
using static CamlifeAPI1.Controllers.ApplicationFormController.RequestParameters.ResubmitApplication;

namespace CamlifeAPI1.Controllers
{
    [Authorize]
    public class ApplicationFormController : ApiController
    {

        [Route("api/Application/SubmitApplication")]
        [HttpPost]
        public object SubmitApplication(
          RequestParameters.SubmitApplication app)
        {
            ResponseValidateError responseValidateError = this.ValidateSummitApplication(app);
            CamlifeAPI1.Class.ErrorCode errorCode = new CamlifeAPI1.Class.ErrorCode();
            List<CountSavedApplication> savedApplicationList = new List<CountSavedApplication>();
            try
            {
                DateTime now = DateTime.Now;
                if (responseValidateError.detail != null)
                    return (object)responseValidateError;
                bool result = false;
                int numOfApplication = 1;
                int numOfYear = 1;
                bl_micro_application_customer APPLICATION_CUSTOMER = new bl_micro_application_customer();
                string mainApplicationNumber = "";
                bl_micro_product_config microProductConfig = new bl_micro_product_config();
                while (numOfApplication <= app.Application.NumberOfApplications)
                {
                    RequestParameters.SaveApplicationInsurance.SumAssurePremiumEntry assurePremiumEntry = app.ApplicationInsurance.SumAssurePremium[numOfApplication - 1];
                    double sumAssure = assurePremiumEntry.SumAssure;
                    double premium = assurePremiumEntry.Premium;
                    double discountAmount = assurePremiumEntry.DiscountAmount;
                    double totalAmount = assurePremiumEntry.TotalAmount;
                    double annualPremium = assurePremiumEntry.AnnualPremium;
                    if (numOfApplication == 1)
                        microProductConfig = da_micro_product_config.ProductConfig.GetProductMicroProduct(app.ApplicationInsurance.PRODUCT_ID);
                    while (numOfYear <= app.Application.NumberOfPurchasingYears)
                    {
                        RequestParameters.Customer customer = app.Customer;
                        if (numOfApplication == 1 && numOfYear == 1)
                        {
                            APPLICATION_CUSTOMER = new bl_micro_application_customer()
                            {
                                ID_TYPE = customer.ID_TYPE,
                                ID_NUMBER = customer.ID_NUMBER,
                                LAST_NAME_IN_ENGLISH = customer.LAST_NAME_IN_ENGLISH,
                                LAST_NAME_IN_KHMER = customer.LAST_NAME_IN_KHMER,
                                FIRST_NAME_IN_ENGLISH = customer.FIRST_NAME_IN_ENGLISH,
                                FIRST_NAME_IN_KHMER = customer.FIRST_NAME_IN_KHMER,
                                GENDER = customer.GENDER,
                                DATE_OF_BIRTH = Helper.FormatDateTime(customer.DATE_OF_BIRTH),
                                NATIONALITY = customer.NATIONALITY,
                                MARITAL_STATUS = customer.MARITAL_STATUS,
                                OCCUPATION = customer.OCCUPATION,
                                HOUSE_NO_EN = customer.HOUSE_NO,
                                HOUSE_NO_KH = customer.HOUSE_NO,
                                STREET_NO_EN = customer.STREET_NO,
                                STREET_NO_KH = customer.STREET_NO,
                                VILLAGE_EN = customer.VILLAGE,
                                VILLAGE_KH = customer.VILLAGE,
                                COMMUNE_EN = customer.COMMUNE,
                                COMMUNE_KH = customer.COMMUNE,
                                DISTRICT_EN = customer.DISTRICT,
                                DISTRICT_KH = customer.DISTRICT,
                                PROVINCE_EN = customer.PROVINCE,
                                PROVINCE_KH = customer.PROVINCE,
                                PHONE_NUMBER1 = customer.PHONE_NUMBER,
                                EMAIL1 = customer.EMAIL,
                                CREATED_BY = customer.CREATED_BY,
                                CREATED_ON = now,
                                STATUS = 1
                            };
                            result = da_micro_application_customer.SaveApplicationCustomer(APPLICATION_CUSTOMER);
                            if (!result)
                                return this.RoleBackApplicaitonSubmit(APPLICATION_CUSTOMER.CUSTOMER_ID);
                        }
                        else
                            result = true;/*skip saving same customer*/
                        if (result)
                        {
                            bl_micro_application_prefix app_prefix = new bl_micro_application_prefix();
                            app_prefix = da_micro_application_prefix.GetLastApplicationPrefix();

                            string appNumber = "";
                            RequestParameters.SaveApplication appRequest = app.Application;
                            bl_micro_application appToSave = new bl_micro_application()
                            {
                                APPLICATION_DATE = Helper.FormatDateTime(appRequest.ApplicationDate),
                                CHANNEL_ITEM_ID = appRequest.ChannelItemId,
                                CHANNEL_LOCATION_ID = appRequest.ChannelLocationId,
                                CHANNEL_ID = appRequest.ChannelId,
                                APPLICATION_CUSTOMER_ID = APPLICATION_CUSTOMER.CUSTOMER_ID,
                                REMARKS = appRequest.Remarks,
                                SALE_AGENT_ID = appRequest.AgentCode,
                                REFERRER_ID = appRequest.ReferrerId,
                                CLIENT_TYPE = numOfYear == 1 ? appRequest.ClientType : "REPAYMENT",
                                CLIENT_TYPE_RELATION = appRequest.ClientTypeRelation,
                                CLIENT_TYPE_REMARKS = appRequest.ClientTypeRemarks,
                                RENEW_FROM_POLICY = "",
                                CREATED_BY = APPLICATION_CUSTOMER.CREATED_BY,
                                CREATED_ON = now,
                                LoanNumber = appRequest.LoanNumber,
                                PolicyholderName = appRequest.PolicyholderName,
                                PolicyholderGender = appRequest.PolicyholderGender,
                                PolicyholderIDType = appRequest.PolicyholderIDType,
                                PolicyholderIDNo = appRequest.PolicyholderIDNo,
                                PolicyholderDOB = string.IsNullOrWhiteSpace(appRequest.PolicyholderDOB) ? new DateTime(1900, 1, 1) : Helper.FormatDateTime(appRequest.PolicyholderDOB),
                                PolicyholderPhoneNumber = appRequest.PolicyholderPhoneNumber,
                                PolicyholderPhoneNumber2 = appRequest.PolicyholderPhoneNumber2,
                                PolicyholderAddress = appRequest.PolicyholderAddress,
                                PolicyholderEmail = appRequest.PolicyholderEmail
                            };
                            if (numOfApplication == 1 && numOfYear == 1)
                            {
                                appToSave.NumbersOfApplicationFirstYear = appRequest.NumberOfApplications;
                                appToSave.NumbersOfPurchasingYear = appRequest.NumberOfPurchasingYears;
                            }
                            else
                            {
                                appToSave.NumbersOfApplicationFirstYear = 0;
                                appToSave.NumbersOfPurchasingYear = 0;
                            }
                            appToSave.SEQ = appToSave.LAST_SEQ + 1;
                            if (appToSave.LAST_PREFIX == app_prefix.PREFIX2)// in same year
                            {
                                appNumber = app_prefix.PREFIX1 + "" + app_prefix.PREFIX2 + "" + (appToSave.SEQ).ToString(app_prefix.DIGITS);
                            }
                            else
                            {
                                int newNumber = 1;
                                appToSave.SEQ = newNumber;
                                appNumber = app_prefix.PREFIX1 + "" + app_prefix.PREFIX2 + "" + newNumber.ToString(app_prefix.DIGITS);
                            }
                            appToSave.APPLICATION_NUMBER = appNumber;

                            if (numOfApplication == 1 && numOfYear == 1)
                            {
                                mainApplicationNumber = appToSave.APPLICATION_NUMBER;
                                result = da_micro_application.SaveApplication(appToSave);
                            }
                            else
                            {
                                appToSave.MainApplicationNumber = mainApplicationNumber;
                                result = da_micro_application.SaveApplication(appToSave);
                            }
                            if (!result)
                                return this.RoleBackApplicaitonSubmit(APPLICATION_CUSTOMER.CUSTOMER_ID, appToSave.APPLICATION_NUMBER);

                            if (numOfApplication == 1 && numOfYear == 1)
                                savedApplicationList.Add(new CountSavedApplication()
                                {
                                    ApplicationId = appToSave.APPLICATION_ID,
                                    ApplicationNumber = appToSave.APPLICATION_NUMBER,
                                    CustomerId = appToSave.APPLICATION_CUSTOMER_ID,
                                    IsMainApplication = true,
                                    ClientType = appToSave.CLIENT_TYPE
                                });
                            else
                                savedApplicationList.Add(new CountSavedApplication()
                                {
                                    ApplicationId = appToSave.APPLICATION_ID,
                                    ApplicationNumber = appToSave.APPLICATION_NUMBER,
                                    CustomerId = appToSave.APPLICATION_CUSTOMER_ID,
                                    IsMainApplication = false,
                                    ClientType = appToSave.CLIENT_TYPE
                                });
                            RequestParameters.SaveApplicationInsurance applicationInsurance = app.ApplicationInsurance;
                            bl_micro_application_insurance APP_INSURANCE = new bl_micro_application_insurance()
                            {
                                APPLICATION_NUMBER = appToSave.APPLICATION_NUMBER,
                                PRODUCT_ID = applicationInsurance.PRODUCT_ID,
                                TERME_OF_COVER = applicationInsurance.TERME_OF_COVER,
                                PAYMENT_PERIOD = applicationInsurance.PAYMENT_PERIOD,
                                SUM_ASSURE = sumAssure,
                                PAY_MODE = applicationInsurance.PAY_MODE,
                                PREMIUM = premium,
                                ANNUAL_PREMIUM = annualPremium,
                                USER_PREMIUM = 0.0,
                                DISCOUNT_AMOUNT = discountAmount,
                                TOTAL_AMOUNT = totalAmount,
                                PACKAGE = applicationInsurance.PACKAGE,
                                PAYMENT_CODE = applicationInsurance.PAYMENT_CODE,
                                REMARKS = applicationInsurance.REMARKS,
                                CREATED_BY = APPLICATION_CUSTOMER.CREATED_BY,
                                CREATED_ON = now,
                                COVER_TYPE = applicationInsurance.COVER_TYPE
                            };
                            result = da_micro_application_insurance.SaveApplicationInsurance(APP_INSURANCE);
                            if (!result)
                                return this.RoleBackApplicaitonSubmit(APPLICATION_CUSTOMER.CUSTOMER_ID, appToSave.APPLICATION_NUMBER);

                            da_micro_product_config.ProductConfig.GetProductMicroProduct(APP_INSURANCE.PRODUCT_ID);
                            if (app.ApplicationInsuranceRider != null)
                            {

                                RequestParameters.SaveApplicationInsuranceRider applicationInsuranceRider = app.ApplicationInsuranceRider;
                                bl_micro_application_insurance_rider APP_INSURANCE_RIDER = new bl_micro_application_insurance_rider()
                                {
                                    APPLICATION_NUMBER = appToSave.APPLICATION_NUMBER,
                                    PRODUCT_ID = applicationInsuranceRider.PRODUCT_ID,
                                    SUM_ASSURE = applicationInsuranceRider.SUM_ASSURE,
                                    PREMIUM = applicationInsuranceRider.PREMIUM,
                                    ANNUAL_PREMIUM = applicationInsuranceRider.ANNUAL_PREMIUM,
                                    DISCOUNT_AMOUNT = applicationInsuranceRider.DISCOUNT_AMOUNT,
                                    TOTAL_AMOUNT = applicationInsuranceRider.TOTAL_AMOUNT,
                                    REMARKS = applicationInsuranceRider.REMARKS,
                                    CREATED_BY = APPLICATION_CUSTOMER.CREATED_BY,
                                    CREATED_ON = now
                                };

                                if (numOfApplication == 1)
                                {
                                    result = da_micro_application_insurance_rider.SaveApplicationInsuranceRider(APP_INSURANCE_RIDER);
                                }

                                if (!result)
                                    return this.RoleBackApplicaitonSubmit(APPLICATION_CUSTOMER.CUSTOMER_ID, appToSave.APPLICATION_NUMBER);
                            }

                            List<RequestParameters.BeneficiaryObject> beneficiaries = app.Beneficiaries;
                            if (beneficiaries != null)
                            {
                                foreach (RequestParameters.BeneficiaryObject beneficiaryObject in beneficiaries)
                                {
                                    result = da_micro_application_beneficiary.SaveApplicationBeneficiary(new bl_micro_application_beneficiary()
                                    {
                                        FULL_NAME = beneficiaryObject.FULL_NAME,
                                        ADDRESS = beneficiaryObject.ADDRESS,
                                        AGE = beneficiaryObject.AGE,
                                        APPLICATION_NUMBER = appToSave.APPLICATION_NUMBER,
                                        RELATION = beneficiaryObject.RELATION,
                                        PERCENTAGE_OF_SHARE = beneficiaryObject.PERCENTAGE_OF_SHARE,
                                        REMARKS = beneficiaryObject.REMARKS,
                                        CREATED_BY = APPLICATION_CUSTOMER.CREATED_BY,
                                        CREATED_ON = now,
                                        DOB = string.IsNullOrWhiteSpace(beneficiaryObject.DOB) ? new DateTime(1900, 1, 1) : Helper.FormatDateTime(beneficiaryObject.DOB),
                                        Gender = beneficiaryObject.Gender,
                                        IdType = beneficiaryObject.IdType,
                                        IdNo = beneficiaryObject.IdNo
                                    });
                                    if (!result)
                                        break;
                                }
                            }
                            if (!result)
                                return this.RoleBackApplicaitonSubmit(APPLICATION_CUSTOMER.CUSTOMER_ID, appToSave.APPLICATION_NUMBER);
                            if (microProductConfig.ProductType.ToUpper() == bl_micro_product_config.PRODUCT_TYPE.MICRO_LOAN.ToString())
                            {
                                RequestParameters.SavePrimaryBeneficiary primaryBeneficiary = app.PrimaryBeneficiary;
                                result = da_micro_application_beneficiary.PremaryBeneficiary.Save(new bl_micro_application_beneficiary.PrimaryBeneciary()
                                {
                                    ApplicationNumber = appToSave.APPLICATION_NUMBER,
                                    FullName = primaryBeneficiary.FullName,
                                    LoanNumber = primaryBeneficiary.LoanNumber,
                                    Address = primaryBeneficiary.Address,
                                    CreatedBy = APPLICATION_CUSTOMER.CREATED_BY,
                                    CreatedOn = now,
                                    CreatedRemarks = ""
                                });
                            }
                            if (!result)
                                return this.RoleBackApplicaitonSubmit(APPLICATION_CUSTOMER.CUSTOMER_ID, appToSave.APPLICATION_NUMBER);
                            RequestParameters.SaveApplicationQuestionaire questionaire = app.Questionaire;
                            result = da_micro_application_questionaire.SaveQuestionaire(new bl_micro_application_questionaire()
                            {
                                APPLICATION_NUMBER = appToSave.APPLICATION_NUMBER,
                                QUESTION_ID = questionaire.QUESTION_ID,
                                ANSWER = questionaire.ANSWER,
                                ANSWER_REMARKS = questionaire.ANSWER_REMARKS,
                                REMARKS = questionaire.REMARKS,
                                CREATED_BY = APPLICATION_CUSTOMER.CREATED_BY,
                                CREATED_ON = now
                            });
                            if (!result)
                                return this.RoleBackApplicaitonSubmit(APPLICATION_CUSTOMER.CUSTOMER_ID, appToSave.APPLICATION_NUMBER);
                        }
                        numOfYear++;
                    }
                    numOfYear = 1;/*reset year*/
                    numOfApplication++;
                }


                if (result)
                {
                    return new ResponseExecuteSuccess() { message = da_micro_application.MESSAGE, detail = savedApplicationList };
                }
                else
                {
                    object roleBack = new object();
                    foreach (var item in savedApplicationList)
                    {
                        roleBack = RoleBackApplicaitonSubmit(item.CustomerId, item.ApplicationNumber);

                    }
                    return roleBack;
                }
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("ApplicationController", "SubmitApplication(RequestParameters.SubmitApplication app)", ex);
                object roleBack = new object();
                foreach (var item in savedApplicationList)
                {
                    roleBack = RoleBackApplicaitonSubmit(item.CustomerId, item.ApplicationNumber);

                }
                return roleBack;
            }
        }

        [Route("api/Application/ReSubmitApplicationBatch")]
        [HttpPost]
        public object ReSubmitApplicationBatch(
          RequestParameters.ResubmitApplication application)
        {
            object obj1 = new object();
            ResponseValidateError responseValidateError = this.ValidateReSummitApplication(application);
            CamlifeAPI1.Class.ErrorCode errorCode = new CamlifeAPI1.Class.ErrorCode();
            DateTime now = DateTime.Now;
            bool flag1 = false;
            RequestParameters.ResubmitApplication.ApplicationResubmit objApp = new RequestParameters.ResubmitApplication.ApplicationResubmit();
            RequestParameters.SaveApplicationInsurance applicationInsurance1 = new RequestParameters.SaveApplicationInsurance();
            RequestParameters.SaveApplicationInsuranceRider applicationInsuranceRider1 = new RequestParameters.SaveApplicationInsuranceRider();
            List<RequestParameters.ResubmitApplication.BeneficiariesResubmit> beneficiariesResubmitList1 = new List<RequestParameters.ResubmitApplication.BeneficiariesResubmit>();
            RequestParameters.SaveApplicationQuestionaire applicationQuestionaire = new RequestParameters.SaveApplicationQuestionaire();
            RequestParameters.ResubmitApplication.CustomerResumbit customerResumbit = new RequestParameters.ResubmitApplication.CustomerResumbit();
            List<RequestParameters.ResubmitApplication.BeneficiariesResubmit> beneficiariesResubmitList2 = new List<RequestParameters.ResubmitApplication.BeneficiariesResubmit>();
            List<bl_micro_application> microApplicationList = new List<bl_micro_application>();
            bl_micro_application_insurance_rider applicationInsuranceRider2 = new bl_micro_application_insurance_rider();
            List<string> newAppList = new List<string>();
            List<string> existAppList = new List<string>();
            RequestParameters.SavePrimaryBeneficiary primaryBeneficiary1 = new RequestParameters.SavePrimaryBeneficiary();
            object obj2;
            try
            {
                if (responseValidateError.detail == null)
                {
                    objApp = application.Application;
                    RequestParameters.SaveApplicationInsurance applicationInsurance2 = application.ApplicationInsurance;
                    RequestParameters.SaveApplicationInsuranceRider applicationInsuranceRider3 = application.ApplicationInsuranceRider;
                    List<RequestParameters.ResubmitApplication.BeneficiariesResubmit> beneficiaries = application.Beneficiaries;
                    RequestParameters.SaveApplicationQuestionaire questionaire = application.Questionaire;
                    RequestParameters.ResubmitApplication.CustomerResumbit customer = application.Customer;
                    List<RequestParameters.ResubmitApplication.BeneficiariesResubmit> beneficiariesToDelete = application.BeneficiariesToDelete;
                    microApplicationList = da_micro_application.GetApplicationBatchByApplicationID(objApp.ApplicationId);
                    RequestParameters.SavePrimaryBeneficiary primaryBeneficiary2 = application.PrimaryBeneficiary;
                    int num1 = application.Application.NumberOfApplications * application.Application.NumberOfPurchasingYears;
                    int count = microApplicationList.Count;
                    int num2 = 1;
                    int num3 = 1;
                    int index1 = 0;
                    string applicationNumber1 = "";
                    string str1 = "";
                    string clientType = objApp.ClientType;
                    int num4 = 0;
                    int num5 = 0;
                    bl_micro_product_config microProductConfig = new bl_micro_product_config();
                    for (; num2 <= application.Application.NumberOfApplications; ++num2)
                    {
                        RequestParameters.SaveApplicationInsurance.SumAssurePremiumEntry assurePremiumEntry = application.ApplicationInsurance.SumAssurePremium[num2 - 1];
                        double sumAssure = assurePremiumEntry.SumAssure;
                        double premium = assurePremiumEntry.Premium;
                        double discountAmount = assurePremiumEntry.DiscountAmount;
                        double totalAmount = assurePremiumEntry.TotalAmount;
                        double annualPremium = assurePremiumEntry.AnnualPremium;
                        if (num2 == 1)
                            microProductConfig = da_micro_product_config.ProductConfig.GetProductMicroProduct(application.ApplicationInsurance.PRODUCT_ID);
                        while (num3 <= application.Application.NumberOfPurchasingYears)
                        {
                            bl_micro_product_config.PRODUCT_TYPE productType;
                            if (index1 < count)
                            {
                                bl_micro_application microApplication = microApplicationList[index1];
                                if (da_micro_application.BackupApplication(microApplication.APPLICATION_ID, "UPDATE", objApp.UpdatedBy, now))
                                {
                                    existAppList.Add(microApplication.APPLICATION_NUMBER);
                                    if (num2 == 1 && num3 == 1)
                                    {
                                        applicationNumber1 = microApplication.APPLICATION_NUMBER;
                                        num4 = microApplication.NumbersOfApplicationFirstYear;
                                        num5 = microApplication.NumbersOfPurchasingYear;
                                        flag1 = this.updateCustomer(customer, objApp.UpdatedBy, now, out str1);
                                    }
                                    bl_micro_application_insurance_rider applicationRider = da_micro_application_insurance_rider.GetApplicationRider(microApplication.APPLICATION_NUMBER);
                                    if (flag1)
                                    {
                                        objApp.ClientType = num3 == 1 ? clientType : "REPAYMENT";
                                        flag1 = this.updateApplication(objApp, microApplication.APPLICATION_NUMBER, customer.CustomerId, now, out str1);
                                        if (flag1)
                                        {
                                            applicationInsurance2.PREMIUM = premium;
                                            applicationInsurance2.ANNUAL_PREMIUM = annualPremium;
                                            applicationInsurance2.TOTAL_AMOUNT = totalAmount;
                                            applicationInsurance2.DISCOUNT_AMOUNT = discountAmount;
                                            applicationInsurance2.SUM_ASSURE = sumAssure;
                                            flag1 = this.updateApplicationInsurance(applicationInsurance2, microApplication.APPLICATION_NUMBER, objApp.UpdatedBy, now, out str1);
                                            if (flag1)
                                            {
                                                if (applicationInsuranceRider3 != null)
                                                {
                                                    if (!string.IsNullOrWhiteSpace(applicationInsuranceRider3.PRODUCT_ID))
                                                    {
                                                        bool flag2 = false;
                                                        if (applicationRider != null)
                                                            flag2 = !string.IsNullOrWhiteSpace(applicationRider.PRODUCT_ID);
                                                        if (flag2)
                                                            flag1 = num2 != 1 ? da_micro_application_insurance_rider.DeleteApplicationInsuranceRider(microApplication.APPLICATION_NUMBER) : this.updateApplicationInsuranceRider(applicationInsuranceRider3, microApplication.APPLICATION_NUMBER, objApp.UpdatedBy, now, out str1);
                                                        else if (num2 == 1)
                                                            flag1 = this.addApplicationInsuranceRider(applicationInsuranceRider3, microApplication.APPLICATION_NUMBER, objApp.UpdatedBy, now, out str1);
                                                    }
                                                    else
                                                        da_micro_application_insurance_rider.DeleteApplicationInsuranceRider(microApplication.APPLICATION_NUMBER);
                                                }
                                                else
                                                    da_micro_application_insurance_rider.DeleteApplicationInsuranceRider(microApplication.APPLICATION_NUMBER);
                                                if (flag1)
                                                {
                                                    if (beneficiaries != null)
                                                    {
                                                        foreach (RequestParameters.ResubmitApplication.BeneficiariesResubmit objBen in beneficiaries)
                                                        {
                                                            flag1 = objBen.Id.Length > 2 ? this.updateBeneficiary(objBen, microApplication.APPLICATION_NUMBER, objApp.UpdatedBy, now, out str1) : this.addBeneficiary(objBen, microApplication.APPLICATION_NUMBER, objApp.UpdatedBy, now, out str1);
                                                            if (!flag1)
                                                                break;
                                                        }
                                                    }
                                                    if (flag1)
                                                    {
                                                        bl_micro_application_beneficiary.PrimaryBeneciary primaryBeneciary = da_micro_application_beneficiary.PremaryBeneficiary.Get(microApplication.APPLICATION_NUMBER);
                                                        if (primaryBeneficiary2 != null)
                                                        {
                                                            if (primaryBeneciary == null)
                                                            {
                                                                string upper = microProductConfig.ProductType.ToUpper();
                                                                productType = bl_micro_product_config.PRODUCT_TYPE.MICRO_LOAN;
                                                                string str2 = productType.ToString();
                                                                if (upper == str2)
                                                                    flag1 = da_micro_application_beneficiary.PremaryBeneficiary.Save(new bl_micro_application_beneficiary.PrimaryBeneciary()
                                                                    {
                                                                        ApplicationNumber = microApplication.APPLICATION_NUMBER,
                                                                        FullName = primaryBeneficiary2.FullName,
                                                                        LoanNumber = primaryBeneficiary2.LoanNumber,
                                                                        Address = primaryBeneficiary2.Address,
                                                                        CreatedBy = objApp.UpdatedBy,
                                                                        CreatedOn = now,
                                                                        CreatedRemarks = ""
                                                                    });
                                                            }
                                                            else
                                                                flag1 = da_micro_application_beneficiary.PremaryBeneficiary.Update(new bl_micro_application_beneficiary.PrimaryBeneciary()
                                                                {
                                                                    ApplicationNumber = microApplication.APPLICATION_NUMBER,
                                                                    FullName = primaryBeneficiary2.FullName,
                                                                    LoanNumber = primaryBeneficiary2.LoanNumber,
                                                                    Address = primaryBeneficiary2.Address,
                                                                    UpdatedBy = objApp.UpdatedBy,
                                                                    UpdatedOn = now,
                                                                    UpdatedRemarks = ""
                                                                });
                                                        }
                                                        else if (primaryBeneciary != null)
                                                            da_micro_application_beneficiary.PremaryBeneficiary.Delete(primaryBeneciary.ApplicationNumber);
                                                    }
                                                    if (flag1)
                                                    {
                                                        flag1 = this.updateQuestion(questionaire, microApplication.APPLICATION_NUMBER, objApp.UpdatedBy, now, out str1);
                                                        if (flag1)
                                                        {
                                                            if (beneficiariesToDelete != null)
                                                            {
                                                                foreach (RequestParameters.ResubmitApplication.BeneficiariesResubmit beneficiariesResubmit in beneficiariesToDelete)
                                                                {
                                                                    flag1 = da_micro_application_beneficiary.DeleteApplicationBeneficiary(da_micro_application_beneficiary.DeleteApplicationBeneficiaryOption.ID, beneficiariesResubmit.Id);
                                                                    if (!flag1)
                                                                        break;
                                                                }
                                                            }
                                                            if (!flag1)
                                                            {
                                                                str1 = "Update application fail, error: Delete beneficiary.";
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            str1 = "Update application fail, error: Update answer.";
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        str1 = "Update application fail, error: Update beneficiary.";
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    str1 = "Update application fail, error: Update rider";
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                str1 = "Update application fail, error: Update insurance";
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            str1 = "Update application fail, error: Update application";
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        str1 = "Update application fail, error: Update customer.";
                                        break;
                                    }
                                }
                                else
                                    obj1 = (object)new ResponseExecuteError()
                                    {
                                        code = errorCode.UnexpectedErrorCode,
                                        message = "System backup application fail."
                                    };
                            }
                            else
                            {
                                bl_micro_application_prefix applicationPrefix1 = new bl_micro_application_prefix();
                                bl_micro_application_prefix applicationPrefix2 = da_micro_application_prefix.GetLastApplicationPrefix();
                                bl_micro_application APPLICATION = new bl_micro_application()
                                {
                                    APPLICATION_DATE = Helper.FormatDateTime(objApp.ApplicationDate),
                                    CHANNEL_ITEM_ID = objApp.ChannelItemId,
                                    CHANNEL_LOCATION_ID = objApp.ChannelLocationId,
                                    CHANNEL_ID = objApp.ChannelId,
                                    APPLICATION_CUSTOMER_ID = customer.CustomerId,
                                    REMARKS = objApp.Remarks,
                                    SALE_AGENT_ID = objApp.AgentCode,
                                    REFERRER_ID = objApp.ReferrerId,
                                    CLIENT_TYPE = num3 == 1 ? clientType : "REPAYMENT",
                                    CLIENT_TYPE_RELATION = objApp.ClientTypeRelation,
                                    CLIENT_TYPE_REMARKS = objApp.ClientTypeRemarks,
                                    RENEW_FROM_POLICY = "",
                                    CREATED_BY = objApp.UpdatedBy,
                                    CREATED_ON = now,
                                    NumbersOfApplicationFirstYear = 0,
                                    NumbersOfPurchasingYear = 0,
                                    MainApplicationNumber = applicationNumber1,
                                    LoanNumber = objApp.LoanNumber,
                                    PolicyholderName = objApp.PolicyholderName,
                                    PolicyholderGender = objApp.PolicyholderGender,
                                    PolicyholderIDType = objApp.PolicyholderIDType,
                                    PolicyholderIDNo = objApp.PolicyholderIDNo,
                                    PolicyholderDOB = string.IsNullOrWhiteSpace(objApp.PolicyholderDOB) ? new DateTime(1900, 1, 1) : Helper.FormatDateTime(objApp.PolicyholderDOB),
                                    PolicyholderPhoneNumber = objApp.PolicyholderPhoneNumber,
                                    PolicyholderPhoneNumber2 = objApp.PolicyholderPhoneNumber2,
                                    PolicyholderAddress = objApp.PolicyholderAddress,
                                    PolicyholderEmail = objApp.PolicyholderEmail
                                };
                                APPLICATION.SEQ = APPLICATION.LAST_SEQ + 1;
                                string str3 = applicationPrefix2.PREFIX1 + applicationPrefix2.PREFIX2 + APPLICATION.SEQ.ToString(applicationPrefix2.DIGITS);
                                APPLICATION.APPLICATION_NUMBER = str3;
                                flag1 = da_micro_application.SaveApplication(APPLICATION);
                                if (flag1)
                                {
                                    newAppList.Add(APPLICATION.APPLICATION_NUMBER);
                                    flag1 = da_micro_application_insurance.SaveApplicationInsurance(new bl_micro_application_insurance()
                                    {
                                        APPLICATION_NUMBER = APPLICATION.APPLICATION_NUMBER,
                                        PRODUCT_ID = applicationInsurance2.PRODUCT_ID,
                                        TERME_OF_COVER = applicationInsurance2.TERME_OF_COVER,
                                        PAYMENT_PERIOD = applicationInsurance2.PAYMENT_PERIOD,
                                        SUM_ASSURE = sumAssure,
                                        PAY_MODE = applicationInsurance2.PAY_MODE,
                                        PREMIUM = premium,
                                        ANNUAL_PREMIUM = annualPremium,
                                        USER_PREMIUM = 0.0,
                                        DISCOUNT_AMOUNT = discountAmount,
                                        TOTAL_AMOUNT = totalAmount,
                                        PACKAGE = applicationInsurance2.PACKAGE,
                                        PAYMENT_CODE = applicationInsurance2.PAYMENT_CODE,
                                        REMARKS = applicationInsurance2.REMARKS,
                                        CREATED_BY = objApp.UpdatedBy,
                                        CREATED_ON = now,
                                        COVER_TYPE = applicationInsurance2.COVER_TYPE
                                    });
                                    if (flag1)
                                    {
                                        if (beneficiaries != null)
                                        {
                                            foreach (RequestParameters.BeneficiaryObject beneficiaryObject in beneficiaries)
                                            {
                                                flag1 = da_micro_application_beneficiary.SaveApplicationBeneficiary(new bl_micro_application_beneficiary()
                                                {
                                                    FULL_NAME = beneficiaryObject.FULL_NAME,
                                                    ADDRESS = beneficiaryObject.ADDRESS,
                                                    AGE = beneficiaryObject.AGE,
                                                    APPLICATION_NUMBER = APPLICATION.APPLICATION_NUMBER,
                                                    RELATION = beneficiaryObject.RELATION,
                                                    PERCENTAGE_OF_SHARE = beneficiaryObject.PERCENTAGE_OF_SHARE,
                                                    REMARKS = beneficiaryObject.REMARKS,
                                                    DOB = string.IsNullOrWhiteSpace(beneficiaryObject.DOB) ? new DateTime(1900, 1, 1) : Helper.FormatDateTime(beneficiaryObject.DOB),
                                                    Gender = beneficiaryObject.Gender,
                                                    IdType = beneficiaryObject.IdType,
                                                    IdNo = beneficiaryObject.IdNo,
                                                    CREATED_BY = objApp.UpdatedBy,
                                                    CREATED_ON = now
                                                });
                                                if (!flag1)
                                                    break;
                                            }
                                        }
                                        if (flag1)
                                        {
                                            string upper = microProductConfig.ProductType.ToUpper();
                                            productType = bl_micro_product_config.PRODUCT_TYPE.MICRO_LOAN;
                                            string str4 = productType.ToString();
                                            if (upper == str4)
                                                flag1 = da_micro_application_beneficiary.PremaryBeneficiary.Save(new bl_micro_application_beneficiary.PrimaryBeneciary()
                                                {
                                                    ApplicationNumber = APPLICATION.APPLICATION_NUMBER,
                                                    FullName = primaryBeneficiary2.FullName,
                                                    LoanNumber = primaryBeneficiary2.LoanNumber,
                                                    Address = primaryBeneficiary2.Address,
                                                    CreatedBy = objApp.UpdatedBy,
                                                    CreatedOn = now,
                                                    CreatedRemarks = ""
                                                });
                                        }
                                        if (flag1)
                                        {
                                            flag1 = da_micro_application_questionaire.SaveQuestionaire(new bl_micro_application_questionaire()
                                            {
                                                APPLICATION_NUMBER = APPLICATION.APPLICATION_NUMBER,
                                                QUESTION_ID = questionaire.QUESTION_ID,
                                                ANSWER = questionaire.ANSWER,
                                                ANSWER_REMARKS = questionaire.ANSWER_REMARKS,
                                                REMARKS = questionaire.REMARKS,
                                                CREATED_BY = objApp.UpdatedBy,
                                                CREATED_ON = now
                                            });
                                            if (flag1)
                                            {
                                                if (applicationInsuranceRider3 != null)
                                                {
                                                    bl_micro_application_insurance_rider APP_INSURANCE_RIDER = new bl_micro_application_insurance_rider()
                                                    {
                                                        APPLICATION_NUMBER = APPLICATION.APPLICATION_NUMBER,
                                                        PRODUCT_ID = applicationInsuranceRider3.PRODUCT_ID,
                                                        SUM_ASSURE = applicationInsuranceRider3.SUM_ASSURE,
                                                        PREMIUM = applicationInsuranceRider3.PREMIUM,
                                                        ANNUAL_PREMIUM = applicationInsuranceRider3.ANNUAL_PREMIUM,
                                                        DISCOUNT_AMOUNT = applicationInsuranceRider3.DISCOUNT_AMOUNT,
                                                        TOTAL_AMOUNT = applicationInsuranceRider3.TOTAL_AMOUNT,
                                                        REMARKS = applicationInsuranceRider3.REMARKS,
                                                        CREATED_BY = objApp.UpdatedBy,
                                                        CREATED_ON = now
                                                    };
                                                    if (num2 == 1)
                                                    {
                                                        flag1 = da_micro_application_insurance_rider.SaveApplicationInsuranceRider(APP_INSURANCE_RIDER);
                                                        if (!flag1)
                                                            break;
                                                    }
                                                    else
                                                        flag1 = true;
                                                }
                                            }
                                            else
                                                break;
                                        }
                                        else
                                            break;
                                    }
                                    else
                                        break;
                                }
                                else
                                    obj1 = (object)new ResponseExecuteError()
                                    {
                                        code = errorCode.UnexpectedErrorCode,
                                        message = "Saved application fail."
                                    };
                            }
                            ++num3;
                            ++index1;
                        }
                        num3 = 1;
                    }
                    if (flag1)
                    {
                        if (num1 < count)
                        {
                            int num6 = count - num1;
                            int index2 = count - num6;
                            while (index2 < count && da_micro_application.UpdateApplicationStatus(microApplicationList[index2].APPLICATION_NUMBER, "DEL", objApp.UpdatedBy, now, "user changed total applications"))
                                ++index2;
                        }
                        string updatedRemarks = "";
                        if (num4 != objApp.NumberOfApplications)
                            updatedRemarks += $"Change Numbers of Application First Year [{(object)num4}] To [{(object)objApp.NumberOfApplications}]";
                        if (num5 != objApp.NumberOfPurchasingYears)
                            updatedRemarks += $"{(updatedRemarks == "" ? (object)"Change Numbers of Purchasing Year " : (object)"Numbers of Purchasing Year ")} [{(object)num5}] To [{(object)objApp.NumberOfPurchasingYears}]";
                        if (da_micro_application.UpdateApplicationTotalNumbers(applicationNumber1, objApp.NumberOfApplications, objApp.NumberOfPurchasingYears, objApp.UpdatedBy, now, updatedRemarks))
                        {
                            foreach (string applicationNumber2 in existAppList)
                                da_micro_application.DeleteBackupApplication(applicationNumber2, objApp.UpdatedBy, now);
                            string str5 = "";
                            int num7 = 1;
                            foreach (string str6 in existAppList)
                            {
                                str5 = $"{str5}{num7.ToString()}.{str6} - Updated <br />";
                                ++num7;
                            }
                            string str7 = "";
                            foreach (string str8 in newAppList)
                            {
                                str7 = $"{str7}{num7.ToString()}.{str8} - Added <br />";
                                ++num7;
                            }
                            obj2 = (object)new ResponseExecuteSuccess()
                            {
                                message = "Success",
                                detail = (object)(str5 + str7)
                            };
                        }
                        else
                        {
                            this.roleBack(newAppList, existAppList, objApp.UpdatedBy, now, out str1);
                            obj2 = (object)new ResponseExecuteError()
                            {
                                code = errorCode.UnexpectedErrorCode,
                                message = (" System roleback:" + str1)
                            };
                        }
                    }
                    else
                    {
                        this.roleBack(newAppList, existAppList, objApp.UpdatedBy, now, out str1);
                        obj2 = (object)new ResponseExecuteError()
                        {
                            code = errorCode.UnexpectedErrorCode,
                            message = (" System roleback:" + str1)
                        };
                    }
                }
                else
                    obj2 = (object)responseValidateError;
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog(nameof(ApplicationFormController), "ReSubmitApplication(RequestParameters.ResubmitApplication app)", ex);
                foreach (bl_micro_application microApplication in microApplicationList)
                {
                    if (!da_micro_application.RestoreApplication(microApplication.APPLICATION_NUMBER, objApp.UpdatedBy, now))
                        Log.AddExceptionToLog(nameof(ApplicationFormController), "ReSubmitApplication(RequestParameters.ResubmitApplication app)=>RestoreApplication", ex);
                }
                obj2 = (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
            return obj2;
        }

        [Route("api/Application/GetApplicationDetail")]
        [HttpGet]
        public object GetApplicationDetail(string applicationNumber)
        {
            object obj = new object();
            CamlifeAPI1.Class.ErrorCode errorCode = new CamlifeAPI1.Class.ErrorCode();
            bl_application_detail_response applicationDetailResponse = new bl_application_detail_response();
            object applicationDetail1;
            try
            {
                bl_application_detail_response applicationDetail2 = da_micro_application.GetApplicationDetail(applicationNumber);
                if (applicationDetail2 == null)
                    applicationDetail1 = (object)new ResponseExecuteSuccess()
                    {
                        message = "Application is not found.",
                        detail = (object)""
                    };
                else
                    applicationDetail1 = (object)new ResponseExecuteSuccess()
                    {
                        message = "Success",
                        detail = (object)applicationDetail2
                    };
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog(nameof(ApplicationFormController), "GetApplicationDetail(string applicationNumber)", ex);
                applicationDetail1 = (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
            return applicationDetail1;
        }

        [Route("api/Application/SaveApplication")]
        [HttpPost]
        public object SaveApplication(
          RequestParameters.SaveApplication app)
        {
            ResponseValidateError responseValidateError = this.ValidateSaveApplication(app);
            CamlifeAPI1.Class.ErrorCode errorCode = new CamlifeAPI1.Class.ErrorCode();
            try
            {
                if (responseValidateError.detail != null)
                    return (object)responseValidateError;
                bl_micro_application_prefix applicationPrefix1 = new bl_micro_application_prefix();
                bl_micro_application_prefix applicationPrefix2 = da_micro_application_prefix.GetLastApplicationPrefix();
                bl_micro_application APPLICATION = new bl_micro_application()
                {
                    APPLICATION_DATE = Helper.FormatDateTime(app.ApplicationDate),
                    CHANNEL_ITEM_ID = app.ChannelItemId,
                    CHANNEL_LOCATION_ID = app.ChannelLocationId,
                    CHANNEL_ID = app.ChannelId,
                    REMARKS = app.Remarks,
                    SALE_AGENT_ID = app.AgentCode,
                    REFERRER_ID = app.ReferrerId,
                    RENEW_FROM_POLICY = "",
                    CREATED_ON = DateTime.Now
                };
                APPLICATION.SEQ = APPLICATION.LAST_SEQ + 1;
                string str;
                if (APPLICATION.LAST_PREFIX == applicationPrefix2.PREFIX2)
                {
                    str = applicationPrefix2.PREFIX1 + applicationPrefix2.PREFIX2 + APPLICATION.SEQ.ToString(applicationPrefix2.DIGITS);
                }
                else
                {
                    int num = 1;
                    APPLICATION.SEQ = num;
                    str = applicationPrefix2.PREFIX1 + applicationPrefix2.PREFIX2 + num.ToString(applicationPrefix2.DIGITS);
                }
                APPLICATION.APPLICATION_NUMBER = str;
                if (da_micro_application.SaveApplication(APPLICATION))
                {
                    ResponseParameters.SaveApplication saveApplication = new ResponseParameters.SaveApplication()
                    {
                        ApplicationId = APPLICATION.APPLICATION_ID,
                        ApplicationNumber = APPLICATION.APPLICATION_NUMBER
                    };
                    return (object)new ResponseExecuteSuccess()
                    {
                        message = da_micro_application.MESSAGE,
                        detail = (object)saveApplication
                    };
                }
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("ApplicationController", "SaveApplication(RequestParameters.SaveApplication app)", ex);
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
        }

        [Route("api/Application/SaveCustomer")]
        [HttpPost]
        public object SaveApplicationCustomer(
          RequestParameters.Customer cust)
        {
            CamlifeAPI1.Class.ErrorCode errorCode = new CamlifeAPI1.Class.ErrorCode();
            try
            {
                ResponseValidateError responseValidateError = this.ValidateCustomer(cust);
                if (responseValidateError.detail.Count != 0)
                    return (object)responseValidateError;
                bl_micro_application_customer APPLICATION_CUSTOMER = new bl_micro_application_customer()
                {
                    ID_TYPE = cust.ID_TYPE,
                    ID_NUMBER = cust.ID_NUMBER,
                    LAST_NAME_IN_ENGLISH = cust.LAST_NAME_IN_ENGLISH,
                    LAST_NAME_IN_KHMER = cust.LAST_NAME_IN_KHMER,
                    FIRST_NAME_IN_ENGLISH = cust.FIRST_NAME_IN_ENGLISH,
                    FIRST_NAME_IN_KHMER = cust.FIRST_NAME_IN_KHMER,
                    GENDER = cust.GENDER,
                    DATE_OF_BIRTH = Helper.FormatDateTime(cust.DATE_OF_BIRTH),
                    NATIONALITY = cust.NATIONALITY,
                    MARITAL_STATUS = cust.MARITAL_STATUS,
                    OCCUPATION = cust.OCCUPATION,
                    HOUSE_NO_EN = cust.HOUSE_NO,
                    HOUSE_NO_KH = cust.HOUSE_NO,
                    STREET_NO_EN = cust.STREET_NO,
                    STREET_NO_KH = cust.STREET_NO,
                    VILLAGE_EN = cust.VILLAGE,
                    VILLAGE_KH = cust.VILLAGE,
                    COMMUNE_EN = cust.COMMUNE,
                    COMMUNE_KH = cust.COMMUNE,
                    DISTRICT_EN = cust.DISTRICT,
                    DISTRICT_KH = cust.DISTRICT,
                    PROVINCE_EN = cust.PROVINCE,
                    PROVINCE_KH = cust.PROVINCE,
                    PHONE_NUMBER1 = cust.PHONE_NUMBER,
                    EMAIL1 = cust.EMAIL,
                    CREATED_BY = cust.CREATED_BY,
                    CREATED_ON = DateTime.Now,
                    STATUS = 1
                };
                if (da_micro_application_customer.SaveApplicationCustomer(APPLICATION_CUSTOMER))
                {
                    ResponseParameters.SaveCustomer saveCustomer = new ResponseParameters.SaveCustomer()
                    {
                        CustomerId = APPLICATION_CUSTOMER.CUSTOMER_ID
                    };
                    return (object)new ResponseExecuteSuccess()
                    {
                        message = "Success",
                        detail = (object)saveCustomer
                    };
                }
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog(nameof(ApplicationFormController), "SaveApplicationCustomer(RequestParameters.Customer cust)", ex);
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
        }

        [Route("api/Application/SaveApplicationInsurance")]
        [HttpPost]
        public object SaveApplicationInsurance(
          RequestParameters.SaveApplicationInsurance obj)
        {
            ResponseValidateError responseValidateError = this.ValidateApplicationInsurance(obj);
            CamlifeAPI1.Class.ErrorCode errorCode = new CamlifeAPI1.Class.ErrorCode();
            try
            {
                if (responseValidateError.detail != null)
                    return (object)responseValidateError;
                if (da_micro_application_insurance.SaveApplicationInsurance(new bl_micro_application_insurance()
                {
                    PRODUCT_ID = obj.PRODUCT_ID,
                    TERME_OF_COVER = obj.TERME_OF_COVER,
                    PAYMENT_PERIOD = obj.PAYMENT_PERIOD,
                    SUM_ASSURE = obj.SUM_ASSURE,
                    PAY_MODE = obj.PAY_MODE,
                    PREMIUM = obj.PREMIUM,
                    ANNUAL_PREMIUM = obj.ANNUAL_PREMIUM,
                    USER_PREMIUM = 0.0,
                    DISCOUNT_AMOUNT = obj.DISCOUNT_AMOUNT,
                    TOTAL_AMOUNT = obj.TOTAL_AMOUNT,
                    PACKAGE = obj.PACKAGE,
                    PAYMENT_CODE = obj.PAYMENT_CODE,
                    REMARKS = obj.REMARKS,
                    CREATED_ON = DateTime.Now
                }))
                {
                    ResponseParameters.SaveApplicationInsurance applicationInsurance = new ResponseParameters.SaveApplicationInsurance()
                    {
                        Premium = obj.PREMIUM,
                        TototalPremium = obj.TOTAL_AMOUNT
                    };
                    return (object)new ResponseExecuteSuccess()
                    {
                        message = da_micro_application.MESSAGE,
                        detail = (object)applicationInsurance
                    };
                }
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("ApplicationController", "SaveApplicationInsurance(RequestParameters.SaveApplicationInsurance obj)", ex);
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
        }

        [Route("api/Application/SaveApplicationInsuranceRider")]
        [HttpPost]
        public object SaveApplicationInsuranceRider(
          RequestParameters.SaveApplicationInsuranceRider obj)
        {
            ResponseValidateError responseValidateError = this.ValidateApplicationInsuranceRider(obj);
            CamlifeAPI1.Class.ErrorCode errorCode = new CamlifeAPI1.Class.ErrorCode();
            try
            {
                if (responseValidateError.detail != null)
                    return (object)responseValidateError;
                if (da_micro_application_insurance_rider.SaveApplicationInsuranceRider(new bl_micro_application_insurance_rider()
                {
                    PRODUCT_ID = obj.PRODUCT_ID,
                    SUM_ASSURE = obj.SUM_ASSURE,
                    PREMIUM = obj.PREMIUM,
                    ANNUAL_PREMIUM = obj.ANNUAL_PREMIUM,
                    DISCOUNT_AMOUNT = obj.DISCOUNT_AMOUNT,
                    TOTAL_AMOUNT = obj.TOTAL_AMOUNT,
                    REMARKS = obj.REMARKS,
                    CREATED_ON = DateTime.Now
                }))
                {
                    ResponseParameters.SaveApplicationInsurance applicationInsurance = new ResponseParameters.SaveApplicationInsurance()
                    {
                        Premium = obj.PREMIUM,
                        TototalPremium = obj.TOTAL_AMOUNT
                    };
                    return (object)new ResponseExecuteSuccess()
                    {
                        message = da_micro_application.MESSAGE,
                        detail = (object)applicationInsurance
                    };
                }
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("ApplicationController", "SaveApplicationInsuranceRider(RequestParameters.SaveApplicationInsuranceRider obj)", ex);
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
        }

        [Route("api/Application/ApplicationInquiry")]
        [HttpPost]
        public object GetApplicationInquiry(
          RequestParameters.ApplicationInquiry obj)
        {
            ResponseValidateError applicationInquiry1 = this.ValidateApplicationInquiry(obj);
            CamlifeAPI1.Class.ErrorCode errorCode = new CamlifeAPI1.Class.ErrorCode();
            try
            {
                if (applicationInquiry1.detail != null)
                    return (object)applicationInquiry1;
                List<Inquiries.ApplicationInquiry> applicationInquiry2 = da_micro_application.GetApplicationInquiry(string.Join(",", (IEnumerable<string>)obj.ChannelLocationId), obj.ApplicationNumber, obj.FullNameEn, obj.IdNumber, obj.PolicyNumber, Helper.FormatDateTime(obj.ApplicationDateFrom), Helper.FormatDateTime(obj.ApplicationDateTo));
                return (object)new ResponseExecuteSuccess()
                {
                    message = ("Record(s) found : " + applicationInquiry2.Count.ToString()),
                    detail = (object)applicationInquiry2
                };
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("ApplicationController", "GetApplicationInquiry(RequestParameters.ApplicationInquiry obj)", ex);
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
        }

        [Route("api/Application/PrintApplication")]
        [HttpPost]
        public object PrintApplications(
          RequestParameters.PrintApplications obj)
        {
            CamlifeAPI1.Class.ErrorCode errorCode = new CamlifeAPI1.Class.ErrorCode();
            try
            {
                if (obj != null)
                {
                    List<Inquiries.ApplicationFilter> applicationFilter = da_micro_application.GetApplicationFilter(string.Join(",", (IEnumerable<string>)obj.ApplicationNumber), obj.OnlyFirstYear);
                    if (da_micro_application.SUCCESS)
                        return (object)new ResponseExecuteSuccess()
                        {
                            message = ("Record(s) found : " + applicationFilter.Count.ToString()),
                            detail = (object)applicationFilter
                        };
                    return (object)new ResponseExecuteError()
                    {
                        code = errorCode.ParameterNotSuppliedCode,
                        message = da_micro_application.MESSAGE
                    };
                }
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.ValidationErrorCode,
                    message = "Record(s) found : "
                };
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("ApplicationController", "PrintApplications(RequestParameters.PrintApplications obj)", ex);
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
        }

        public object RoleBackAppliction(
          RequestParameters.RoleBackAppliction app)
        {
            CamlifeAPI1.Class.ErrorCode errorCode = new CamlifeAPI1.Class.ErrorCode();
            ResponseValidateError responseValidateError = this.ValidateRoleBackApplication(app);
            try
            {
                if (responseValidateError.detail != null)
                    return (object)responseValidateError;
                da_micro_application_customer.DeleteApplicationCustomer(app.ApplicationCustomerId);
                da_micro_application.DeleteApplication(app.ApplicationNumber);
                da_micro_application_insurance.DeleteApplicationInsurance(app.ApplicationNumber);
                da_micro_application_insurance_rider.DeleteApplicationInsuranceRider(app.ApplicationNumber);
                da_micro_application_beneficiary.DeleteApplicationBeneficiary(app.ApplicationNumber);
                da_micro_application_questionaire.DeleteQuestionaire(app.ApplicationNumber);
                da_micro_application_beneficiary.PremaryBeneficiary.Delete(app.ApplicationNumber);
                return (object)new ResponseExecuteSuccess()
                {
                    message = "Success",
                    detail = (object)"Application role back successfully."
                };
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("ApplicationController", "SaveApplicationInsuranceRider(RequestParameters.SaveApplicationInsuranceRider obj)", ex);
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
        }

        private object RoleBackApplicaitonSubmit(string customerId, string ApplicationNumber = "")
        {
            CamlifeAPI1.Class.ErrorCode errorCode = new CamlifeAPI1.Class.ErrorCode();
            try
            {
                if (customerId != "")
                    da_micro_application_customer.DeleteApplicationCustomer(customerId);
                if (ApplicationNumber != "")
                {
                    da_micro_application.DeleteApplication(ApplicationNumber);
                    da_micro_application_insurance.DeleteApplicationInsurance(ApplicationNumber);
                    da_micro_application_insurance_rider.DeleteApplicationInsuranceRider(ApplicationNumber);
                    da_micro_application_beneficiary.DeleteApplicationBeneficiary(ApplicationNumber);
                    da_micro_application_beneficiary.PremaryBeneficiary.Delete(ApplicationNumber);
                    da_micro_application_questionaire.DeleteQuestionaire(ApplicationNumber);
                    da_micro_application_beneficiary.PremaryBeneficiary.Delete(ApplicationNumber);
                }
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = "Submit appliction fail, system roleback successfully."
                };
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("ApplicationController", "RoleBackApplicaitonSubmit(string customerId, string ApplicationNumber)", ex);
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = ("Submit appliction fail, system roleback fail. Error:" + errorCode.UnexpectedError)
                };
            }
        }

        [Route("api/Helper/SaveApplicationRequest")]
        [HttpGet]
        public object HelperSaveApplication()
        {
            return (object)new RequestParameters.SaveApplication();
        }

        [Route("api/Helper/SaveApplicationCustomerRequest")]
        [HttpGet]
        public object HelperSaveApplicationCustomer()
        {
            return (object)new RequestParameters.Customer();
        }

        [Route("api/Helper/SummitApplication")]
        [HttpGet]
        public object HelperSummitApplication()
        {
            return (object)new RequestParameters.SubmitApplication()
            {
                Customer = new RequestParameters.Customer(),
                Application = new RequestParameters.SaveApplication(),
                ApplicationInsurance = new RequestParameters.SaveApplicationInsurance(),
                ApplicationInsuranceRider = new RequestParameters.SaveApplicationInsuranceRider(),
                Beneficiaries = new List<RequestParameters.BeneficiaryObject>()
      {
        new RequestParameters.BeneficiaryObject()
        {
          FULL_NAME = (string) null,
          ADDRESS = (string) null,
          AGE = (string) null,
          RELATION = (string) null,
          PERCENTAGE_OF_SHARE = 0.0
        },
        new RequestParameters.BeneficiaryObject()
        {
          FULL_NAME = (string) null,
          ADDRESS = (string) null,
          AGE = (string) null,
          RELATION = (string) null,
          PERCENTAGE_OF_SHARE = 0.0
        }
      },
                Questionaire = new RequestParameters.SaveApplicationQuestionaire()
            };
        }

        private ResponseValidateError ValidateCustomer(
          RequestParameters.Customer cus)
        {
            ResponseValidateError responseValidateCuz = new ResponseValidateError();
            List<ValidateRequest> validateRequestList = new List<ValidateRequest>();
            try
            {
               
                if (cus == null)
                {
                    validateRequestList.Add(new ValidateRequest()
                    {
                        field = "Customer object",
                        message = "Object reference not set to an instance of an object"
                    });
                }
                else
                {
                    
                    if (string.IsNullOrWhiteSpace(cus.ID_TYPE.ToString() ?? "") || cus.ID_TYPE < 0)
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "ID_TYPE",
                            message = "Id Type is required."
                        });
                    if (string.IsNullOrWhiteSpace(cus.ID_NUMBER))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "ID_NUMBER",
                            message = "Id Number is required."
                        });
                    if (string.IsNullOrWhiteSpace(cus.LAST_NAME_IN_ENGLISH))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "LAST_NAME_IN_ENGLISH",
                            message = "Last name in english is required."
                        });
                    if (string.IsNullOrWhiteSpace(cus.FIRST_NAME_IN_ENGLISH))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "FIRST_NAME_IN_ENGLISH",
                            message = "First name in english is required."
                        });
                    if (string.IsNullOrWhiteSpace(cus.LAST_NAME_IN_KHMER))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "LAST_NAME_IN_KHMER",
                            message = "Last name in khmer is required."
                        });
                    if (string.IsNullOrWhiteSpace(cus.FIRST_NAME_IN_KHMER))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "FIRST_NAME_IN_KHMER",
                            message = "First name in khmer is required."
                        });
                    if (cus.GENDER == -1)
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "GENDER",
                            message = "Gender is required."
                        });
                    if (string.IsNullOrWhiteSpace(cus.DATE_OF_BIRTH) || !Helper.IsDate(cus.DATE_OF_BIRTH))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "DATE_OF_BIRTH",
                            message = "Date of birth is required with a format [dd/mm/yyyy]."
                        });
                    if (string.IsNullOrWhiteSpace(cus.NATIONALITY))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "NATIONALITY",
                            message = "Nationality is required."
                        });
                    if (string.IsNullOrWhiteSpace(cus.MARITAL_STATUS))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "MARITAL_STATUS",
                            message = "Marital status is required."
                        });
                    if (string.IsNullOrWhiteSpace(cus.PHONE_NUMBER))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "PHONE_NUMBER",
                            message = "Phone number status is required."
                        });
                    if (string.IsNullOrWhiteSpace(cus.PROVINCE))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "PROVINCE",
                            message = "Province status is required."
                        });
                }
                if (validateRequestList.Count > 0)
                {
                    responseValidateCuz.message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.";
                    responseValidateCuz.detail = validateRequestList;
                }
            }
            catch (Exception ex)
            {
                validateRequestList.Add(new ValidateRequest()
                {
                    field = "ERROR",
                    message = "Unexpected error."
                });
                responseValidateCuz = new ResponseValidateError()
                {
                    message = "Validation error",
                    detail = validateRequestList
                };
                Log.AddExceptionToLog("ApplicationController", "ValidateCustomer(RequestParameters.Customer cus)", ex);
            }
            return responseValidateCuz;
        }

        private ResponseValidateError ValidateSaveApplication(
          RequestParameters.SaveApplication app)
        {
            ResponseValidateError responseValidateError = new ResponseValidateError();
            List<ValidateRequest> validateRequestList = new List<ValidateRequest>();
            try
            {
                if (app == null)
                {
                    validateRequestList.Add(new ValidateRequest()
                    {
                        field = "Application object",
                        message = "Object reference not set to an instance of an object."
                    });
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(app.ClientType))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "ClientType",
                            message = "Client type is required."
                        });
                    else if (!Enum.IsDefined(typeof(Helper.ClientType), (object)app.ClientType))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "ClientType",
                            message = $"Client type [{app.ClientType}] is not definded."
                        });
                    if (string.IsNullOrWhiteSpace(app.ApplicationDate) || !Helper.IsDate(app.ApplicationDate))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "ApplicationDate",
                            message = "Application date is required with a format [dd/mm/yyyy]."
                        });
                    if (string.IsNullOrEmpty(app.ChannelId))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "ChannelId",
                            message = "Channel id is required."
                        });
                    if (string.IsNullOrWhiteSpace(app.ChannelItemId))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "ChannelItemId",
                            message = "Channel item id is required."
                        });
                    if (string.IsNullOrWhiteSpace(app.ChannelLocationId))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "Channel location id is required."
                        });
                    if (string.IsNullOrWhiteSpace(app.AgentCode))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "AgentCode",
                            message = "Agent code is required."
                        });
                }
                if (validateRequestList.Count > 0)
                    responseValidateError = new ResponseValidateError()
                    {
                        message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                        detail = validateRequestList
                    };
            }
            catch (Exception ex)
            {
                validateRequestList.Add(new ValidateRequest()
                {
                    field = "ERROR",
                    message = "Unexpected error."
                });
                responseValidateError = new ResponseValidateError()
                {
                    message = "Validation error",
                    detail = validateRequestList
                };
                Log.AddExceptionToLog("ApplicationController", "ValidateSaveApplication(RequestParameters.SaveApplication app)", ex);
            }
            return responseValidateError;
        }

        private ResponseValidateError ValidateApplicationInsurance(
          RequestParameters.SaveApplicationInsurance obj)
        {
            ResponseValidateError responseValidateError = new ResponseValidateError();
            List<ValidateRequest> validateRequestList = new List<ValidateRequest>();
            try
            {
                if (obj == null)
                {
                    validateRequestList.Add(new ValidateRequest()
                    {
                        field = "Application Insurance",
                        message = "Application Insurance is required."
                    });
                }
                else
                {
                    if (string.IsNullOrEmpty(obj.PRODUCT_ID))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "PRODUCT_ID",
                            message = "Product id is required."
                        });
                    if (obj.TERME_OF_COVER <= 0)
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "TERME_OF_COVER",
                            message = "Term of cover is required."
                        });
                    if (obj.PAYMENT_PERIOD <= 0)
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "PAYMENT_PERIOD is required."
                        });
                    if (obj.SUM_ASSURE <= 0.0)
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "SUM_ASSURE",
                            message = "Sum Assured is required."
                        });
                    if (obj.PAY_MODE < 0)
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "PAY_MODE",
                            message = "Pay mode is required."
                        });
                    if (obj.PREMIUM <= 0.0)
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "PREMIUM",
                            message = "Premium is required."
                        });
                    if (obj.DISCOUNT_AMOUNT < 0.0)
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "DISCOUNT_AMOUNT",
                            message = "Discount Amount must be greater than or equal zero."
                        });
                    if (obj.TOTAL_AMOUNT != obj.PREMIUM - obj.DISCOUNT_AMOUNT)
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "TOTAL_AMOUNT",
                            message = "Total amount must be equal to [premium - discount amount]."
                        });
                    if (string.IsNullOrWhiteSpace(obj.PACKAGE))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "PACKAGE",
                            message = "Package is required."
                        });
                }
                if (validateRequestList.Count > 0)
                    responseValidateError = new ResponseValidateError()
                    {
                        message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                        detail = validateRequestList
                    };
            }
            catch (Exception ex)
            {
                validateRequestList.Add(new ValidateRequest()
                {
                    field = "ERROR",
                    message = "Unexpected error."
                });
                responseValidateError = new ResponseValidateError()
                {
                    message = "Validation error",
                    detail = validateRequestList
                };
                Log.AddExceptionToLog("ApplicationController", "ValidateApplicationInsurance ( RequestParameters.SaveApplicationInsurance obj)", ex);
            }
            return responseValidateError;
        }

        private ResponseValidateError ValidateApplicationInsuranceRider(
          RequestParameters.SaveApplicationInsuranceRider obj)
        {
            ResponseValidateError responseValidateRider = new ResponseValidateError();
            List<ValidateRequest> validateRequestList = new List<ValidateRequest>();
            try
            {
                if (obj == null)
                {
                    validateRequestList.Add(new ValidateRequest()
                    {
                        field = "Application Insurance Rider",
                        message = "Object reference not set to an instance of an object."
                    });
                }
                else
                {
                    if (string.IsNullOrEmpty(obj.PRODUCT_ID))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "PRODUCT_ID",
                            message = "Product id is required."
                        });
                    if (obj.SUM_ASSURE <= 0.0)
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "SUM_ASSURE",
                            message = "Sum Assured is required."
                        });
                    if (obj.ANNUAL_PREMIUM <= 0.0)
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "ANNUAL_PREMIUM",
                            message = "Annual premium is required."
                        });
                    if (obj.PREMIUM <= 0.0)
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "PREMIUM",
                            message = "Premium is required."
                        });
                    if (obj.DISCOUNT_AMOUNT < 0.0)
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "DISCOUNT_AMOUNT",
                            message = "Discount Amount must be greater than or equal zero."
                        });
                    if (obj.TOTAL_AMOUNT != obj.PREMIUM + obj.DISCOUNT_AMOUNT)
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "TOTAL_AMOUNT",
                            message = "Total amount must be equal to [premium + discount amount]."
                        });
                }
                if (validateRequestList.Count > 0)
                    responseValidateRider = new ResponseValidateError()
                    {
                        message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                        detail = validateRequestList
                    };
            }
            catch (Exception ex)
            {
                validateRequestList.Add(new ValidateRequest()
                {
                    field = "ERROR",
                    message = "Unexpected error."
                });
                responseValidateRider = new ResponseValidateError()
                {
                    message = "Validation error",
                    detail = validateRequestList
                };
                Log.AddExceptionToLog("ApplicationController", "ValidateApplicationInsuranceRider(RequestParameters.SaveApplicationInsuranceRider obj)", ex);
            }
            return responseValidateRider;
        }

        private ResponseValidateError ValidateApplicationBeneficiary(
          List<RequestParameters.BeneficiaryObject> obj)
        {
            ResponseValidateError responseValidateError = new ResponseValidateError();
            List<ValidateRequest> validateRequestList = new List<ValidateRequest>();
            try
            {
                if (obj == null)
                    validateRequestList.Add(new ValidateRequest()
                    {
                        field = "Application Beneficiary",
                        message = "Object reference not set to an instance of an object."
                    });
                else if (obj == null)
                {
                    validateRequestList.Add(new ValidateRequest()
                    {
                        field = "Beneficiary",
                        message = "Beneficiary is required."
                    });
                }
                else
                {
                    double percentage = 0.0;
                    int benIndx = 0;
                    foreach (RequestParameters.BeneficiaryObject beneficiaryObject in obj)
                    {
                        if (string.IsNullOrWhiteSpace(beneficiaryObject.FULL_NAME))
                            validateRequestList.Add(new ValidateRequest()
                            {
                                field = $"Beneficiary [{benIndx.ToString()}]",
                                message = "Full name is required."
                            });
                        if (string.IsNullOrWhiteSpace(beneficiaryObject.ADDRESS))
                            validateRequestList.Add(new ValidateRequest()
                            {
                                field = $"Beneficiary [{benIndx.ToString()}]",
                                message = "Address is required."
                            });
                        if (string.IsNullOrWhiteSpace(beneficiaryObject.RELATION))
                            validateRequestList.Add(new ValidateRequest()
                            {
                                field = $"Beneficiary [{benIndx.ToString()}]",
                                message = "Relation is required."
                            });
                        if (beneficiaryObject.PERCENTAGE_OF_SHARE <= 0.0 || beneficiaryObject.PERCENTAGE_OF_SHARE > 100.0)
                            validateRequestList.Add(new ValidateRequest()
                            {
                                field = $"Beneficiary [{benIndx.ToString()}]",
                                message = "Percentage of share must be [ >0 and <=100 ]."
                            });
                        percentage += beneficiaryObject.PERCENTAGE_OF_SHARE;
                        ++benIndx;
                    }
                    if (percentage != 100.0)
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "Beneficiary",
                            message = "Total percentage of share must be equal 100%."
                        });
                }
                if (validateRequestList.Count > 0)
                    responseValidateError = new ResponseValidateError()
                    {
                        message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                        detail = validateRequestList
                    };
            }
            catch (Exception ex)
            {
                validateRequestList.Add(new ValidateRequest()
                {
                    field = "ERROR",
                    message = "Unexpected error."
                });
                responseValidateError = new ResponseValidateError()
                {
                    message = "Validation error",
                    detail = validateRequestList
                };
                Log.AddExceptionToLog("ApplicationController", "ValidateApplicationBeneficiary(RequestParameters.SaveApplicationBeneficiary obj)", ex);
            }
            return responseValidateError;
        }

        private ResponseValidateError ValidateApplicationBeneficiaryResubmit(
          List<RequestParameters.ResubmitApplication.BeneficiariesResubmit> obj)
        {
            ResponseValidateError responseValidateError = new ResponseValidateError();
            List<ValidateRequest> validateRequestList = new List<ValidateRequest>();
            try
            {
                if (obj == null)
                {
                    validateRequestList.Add(new ValidateRequest()
                    {
                        field = "Beneficiary",
                        message = "Beneficiary is required."
                    });
                }
                else
                {
                    double percentage = 0.0;
                    int benIndx = 0;
                    foreach (RequestParameters.BeneficiaryObject beneficiaryObject in obj)
                    {
                        if (string.IsNullOrWhiteSpace(beneficiaryObject.FULL_NAME))
                            validateRequestList.Add(new ValidateRequest()
                            {
                                field = $"Beneficiary [{benIndx.ToString()}]",
                                message = "Full name is required."
                            });
                        if (string.IsNullOrWhiteSpace(beneficiaryObject.ADDRESS))
                            validateRequestList.Add(new ValidateRequest()
                            {
                                field = $"Beneficiary [{benIndx.ToString()}]",
                                message = "Address is required."
                            });
                        if (string.IsNullOrWhiteSpace(beneficiaryObject.RELATION))
                            validateRequestList.Add(new ValidateRequest()
                            {
                                field = $"Beneficiary [{benIndx.ToString()}]",
                                message = "Relation is required."
                            });
                        if (beneficiaryObject.PERCENTAGE_OF_SHARE <= 0.0 || beneficiaryObject.PERCENTAGE_OF_SHARE > 100.0)
                            validateRequestList.Add(new ValidateRequest()
                            {
                                field = $"Beneficiary [{benIndx.ToString()}]",
                                message = "Percentage of share must be [ >0 and <=100 ]."
                            });
                        percentage += beneficiaryObject.PERCENTAGE_OF_SHARE;
                        ++benIndx;
                    }
                    if (percentage != 100.0)
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "Beneficiary",
                            message = "Total percentage of share must be equal 100%."
                        });
                }
                if (validateRequestList.Count > 0)
                    responseValidateError = new ResponseValidateError()
                    {
                        message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                        detail = validateRequestList
                    };
            }
            catch (Exception ex)
            {
                validateRequestList.Add(new ValidateRequest()
                {
                    field = "ERROR",
                    message = "Unexpected error."
                });
                responseValidateError = new ResponseValidateError()
                {
                    message = "Validation error",
                    detail = validateRequestList
                };
                Log.AddExceptionToLog("ApplicationController", "ValidateApplicationBeneficiaryResubmit(List<RequestParameters.ResubmitApplication.BeneficiariesResubmit> obj)", ex);
            }
            return responseValidateError;
        }

        private ResponseValidateError ValidateApplicationQuestionair(
          RequestParameters.SaveApplicationQuestionaire obj)
        {
            ResponseValidateError responseValidateError = new ResponseValidateError();
            List<ValidateRequest> validateRequestList = new List<ValidateRequest>();
            try
            {
                if (obj == null)
                {
                    validateRequestList.Add(new ValidateRequest()
                    {
                        field = "Application Questionair",
                        message = "Object reference not set to an instance of an object."
                    });
                }
                else
                {
                    if (obj.ANSWER < 0 || obj.ANSWER > 1)
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "ANSWER",
                            message = "Answer is required [0 - 1]."
                        });
                    if (obj.ANSWER == 1 && string.IsNullOrWhiteSpace(obj.ANSWER_REMARKS))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "ANSWER",
                            message = "Answer remarks is required."
                        });
                }
                if (validateRequestList.Count > 0)
                    responseValidateError = new ResponseValidateError()
                    {
                        message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                        detail = validateRequestList
                    };
            }
            catch (Exception ex)
            {
                validateRequestList.Add(new ValidateRequest()
                {
                    field = "ERROR",
                    message = "Unexpected error."
                });
                responseValidateError = new ResponseValidateError()
                {
                    message = "Validation error",
                    detail = validateRequestList
                };
                Log.AddExceptionToLog("ApplicationController", "ValidateApplicationQuestionair(RequestParameters.SaveApplicationQuestionaire obj)", ex);
            }
            return responseValidateError;
        }

        private ResponseValidateError ValidateRoleBackApplication(
          RequestParameters.RoleBackAppliction obj)
        {
            ResponseValidateError responseValidateError = new ResponseValidateError();
            List<ValidateRequest> validateRequestList = new List<ValidateRequest>();
            try
            {
                if (obj == null)
                {
                    validateRequestList.Add(new ValidateRequest()
                    {
                        field = "RoleBackApplication",
                        message = "Object reference not set to an instance of an object."
                    });
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(obj.ApplicationNumber) || !Helper.IsDate(obj.ApplicationNumber))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "ApplicationNumber",
                            message = "Application number is required."
                        });
                    if (string.IsNullOrWhiteSpace(obj.ApplicationCustomerId))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "ApplicationCustomerId",
                            message = "ApplicationCustomerId is required."
                        });
                    bl_micro_application application = da_micro_application.GetApplication(obj.ApplicationNumber);
                    if (application.APPLICATION_NUMBER != null && !string.IsNullOrWhiteSpace(da_micro_policy.GetPolicyByApplicationID(application.APPLICATION_ID).POLICY_NUMBER))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "ApplicationNumber",
                            message = $"Application number [{application.APPLICATION_NUMBER}] is already converted to policy."
                        });
                }
                if (validateRequestList.Count > 0)
                    responseValidateError = new ResponseValidateError()
                    {
                        message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                        detail = validateRequestList
                    };
            }
            catch (Exception ex)
            {
                validateRequestList.Add(new ValidateRequest()
                {
                    field = "ERROR",
                    message = "Unexpected error."
                });
                responseValidateError = new ResponseValidateError()
                {
                    message = "Validation error",
                    detail = validateRequestList
                };
                Log.AddExceptionToLog("ApplicationController", "ValidateRoleBackApplication(RequestParameters.RoleBackAppliction obj)", ex);
            }
            return responseValidateError;
        }

        private ResponseValidateError ValidateSummitApplication(
          RequestParameters.SubmitApplication obj)
        {
            ResponseValidateError responseValidateError = new ResponseValidateError();
            List<ValidateRequest> validateRequestList = new List<ValidateRequest>();
            try
            {
                if (obj == null)
                {
                    validateRequestList.Add(new ValidateRequest()
                    {
                        field = "SubmitApplication",
                        message = "Object reference not set to an instance of an object."
                    });
                    responseValidateError = new ResponseValidateError()
                    {
                        message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                        detail = validateRequestList
                    };
                }
                else
                {
                    ResponseValidateError responseValidateCuz = this.ValidateCustomer(obj.Customer);
                    ResponseValidateError responseValidateSaveApp = this.ValidateSaveApplication(obj.Application);
                    ResponseValidateError responseValidateAppInsurance = this.ValidateApplicationInsurance(obj.ApplicationInsurance);
                    ResponseValidateError responseValidateBen = new ResponseValidateError();
                    if (da_micro_product_config.ProductConfig.GetProductMicroProduct(obj.ApplicationInsurance.PRODUCT_ID).ProductType.ToUpper() == bl_micro_product_config.PRODUCT_TYPE.MICRO_LOAN.ToString())
                    {
                        if (obj.Beneficiaries != null)
                            responseValidateBen = this.ValidateApplicationBeneficiary(obj.Beneficiaries);
                        else
                            responseValidateBen.detail = (List<ValidateRequest>)null;
                    }
                    else
                        responseValidateBen = this.ValidateApplicationBeneficiary(obj.Beneficiaries);

                    ResponseValidateError responseValidateQuestion = this.ValidateApplicationQuestionair(obj.Questionaire);
                    if (responseValidateCuz.detail != null)
                        return responseValidateCuz;
                    if (responseValidateSaveApp.detail != null)
                        return responseValidateSaveApp;
                    if (responseValidateAppInsurance.detail != null)
                        return responseValidateAppInsurance;
                    bl_micro_product_config proConfig = da_micro_product_config.ProductConfig.GetProductMicroProduct(obj.ApplicationInsurance.PRODUCT_ID);

                    bl_micro_product_config.PRODUCT_TYPE productType = bl_micro_product_config.PRODUCT_TYPE.MICRO_LOAN;
                    string str1 = productType.ToString();
                    if (proConfig.ProductType.ToUpper() == bl_micro_product_config.PRODUCT_TYPE.MICRO_LOAN.ToString() && string.IsNullOrWhiteSpace(obj.Application.LoanNumber))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "LoanNumber",
                            message = "Loan Number is required."
                        });

                    if (proConfig.IsRequiredRider)
                    {
                        ResponseValidateError responseValidateRider = this.ValidateApplicationInsuranceRider(obj.ApplicationInsuranceRider);
                        if (responseValidateRider.detail != null)
                            return responseValidateRider;
                    }
                    if (proConfig.BusinessType == bl_micro_product_config.BusinussTypeOption.BANCA_REFERRAL.ToString() && obj.Application.ReferrerId == null)
                    {
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "Referral ID",
                            message = "Referral Id is required."
                        });
                        responseValidateError = new ResponseValidateError()
                        {
                            message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                            detail = validateRequestList
                        };
                    }

                    if (proConfig.ProductType.ToUpper() == bl_micro_product_config.PRODUCT_TYPE.MICRO_LOAN.ToString())
                    {
                        if (obj.Application.PolicyholderName.Trim() == "")
                        {
                            validateRequestList.Add(new ValidateRequest()
                            {
                                field = "Policy Holder Name",
                                message = "Policy Holder Name is required."
                            });
                            responseValidateError = new ResponseValidateError()
                            {
                                message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                                detail = validateRequestList
                            };
                        }
                        if (obj.Application.PolicyholderIDType >= 0 && obj.Application.PolicyholderIDNo.Trim() == "")
                        {
                            validateRequestList.Add(new ValidateRequest()
                            {
                                field = "Policy Holder ID No",
                                message = "Policy Holder ID No is required."
                            });
                            responseValidateError = new ResponseValidateError()
                            {
                                message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                                detail = validateRequestList
                            };
                        }
                        if (obj.PrimaryBeneficiary == null)
                        {
                            validateRequestList.Add(new ValidateRequest()
                            {
                                field = "PrimaryBeneficiary",
                                message = "PrimaryBeneficiary object is required."
                            });
                            responseValidateError = new ResponseValidateError()
                            {
                                message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                                detail = validateRequestList
                            };
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(obj.PrimaryBeneficiary.FullName))
                            {
                                validateRequestList.Add(new ValidateRequest()
                                {
                                    field = "PrimaryBeneficiary Full Name",
                                    message = "PrimaryBeneficiary Full Name is required."
                                });
                                responseValidateError = new ResponseValidateError()
                                {
                                    message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                                    detail = validateRequestList
                                };
                            }
                            if (string.IsNullOrWhiteSpace(obj.PrimaryBeneficiary.LoanNumber))
                            {
                                validateRequestList.Add(new ValidateRequest()
                                {
                                    field = "PrimaryBeneficiary Loan Number",
                                    message = "PrimaryBeneficiary Loan Number is required."
                                });
                                responseValidateError = new ResponseValidateError()
                                {
                                    message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                                    detail = validateRequestList
                                };
                            }
                            if (string.IsNullOrWhiteSpace(obj.PrimaryBeneficiary.Address))
                            {
                                validateRequestList.Add(new ValidateRequest()
                                {
                                    field = "PrimaryBeneficiary Address",
                                    message = "PrimaryBeneficiary Address is required."
                                });
                                responseValidateError = new ResponseValidateError()
                                {
                                    message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                                    detail = validateRequestList
                                };
                            }
                        }
                    }

                    if (responseValidateBen.detail != null)
                        return responseValidateBen;
                    if (responseValidateQuestion.detail != null)
                        return responseValidateQuestion;

                    List<Channel_Item_Config> channelConfigLists = new Channel_Item_Config().GetChannelItemConfig(obj.Application.ChannelItemId, 1);
                    Channel_Item_Config channelConfig = new Channel_Item_Config();
                    if (Channel_Item_Config.Transection)
                    {
                        foreach (var chConfig in channelConfigLists.Where(_ => _.ProductId == obj.ApplicationInsurance.PRODUCT_ID))
                            channelConfig = chConfig;

                        if (channelConfig.ProductId != null)
                        {
                            int maxPolicyCount = da_micro_policy.CountPolicyByCustomerIdTypeInYear(obj.Customer.ID_TYPE, obj.Customer.ID_NUMBER, Helper.FormatDateTime(obj.Application.ApplicationDate).Year);
                            if (da_micro_policy.SUCCESS)
                            {
                                if (maxPolicyCount > channelConfig.MaxPolicyPerLife)
                                {
                                    validateRequestList.Add(new ValidateRequest()
                                    {
                                        field = "Number of Policies",
                                        message = "Maximum policies count is exceed."
                                    });
                                    responseValidateError = new ResponseValidateError()
                                    {
                                        message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                                        detail = validateRequestList
                                    };
                                }
                            }
                        }
                        else
                        {
                            validateRequestList.Add(new ValidateRequest()
                            {
                                field = (string)null,
                                message = "Count Inforce policy error."
                            });
                            responseValidateError = new ResponseValidateError()
                            {
                                message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                                detail = validateRequestList
                            };
                        }
                    }

                    else
                    {
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = (string)null,
                            message = "Get channel configuration error."
                        });
                        responseValidateError = new ResponseValidateError()
                        {
                            message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                            detail = validateRequestList
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                validateRequestList.Add(new ValidateRequest()
                {
                    field = "ERROR",
                    message = "Unexpected error."
                });
                responseValidateError = new ResponseValidateError()
                {
                    message = "Validation error",
                    detail = validateRequestList
                };
                Log.AddExceptionToLog("ApplicationController", "ValidateSummitApplication(RequestParameters.SubmitApplication obj)", ex);
            }
            return responseValidateError;
        }

        private ResponseValidateError ValidateReSummitApplicationBatch(
          RequestParameters.ResubmitApplication obj)
        {
            ResponseValidateError responseValidateError = new ResponseValidateError();
            List<ValidateRequest> validateRequestList = new List<ValidateRequest>();
            try
            {
                if (obj == null)
                {
                    validateRequestList.Add(new ValidateRequest()
                    {
                        field = "ReSubmitApplicationBatch",
                        message = "Object reference not set to an instance of an object."
                    });
                    responseValidateError = new ResponseValidateError()
                    {
                        message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                        detail = validateRequestList
                    };
                }
                else
                {
                    ResponseValidateError responseValidateCuz = this.ValidateCustomer((RequestParameters.Customer)obj.Customer);
                    ResponseValidateError responseValidateApp = this.ValidateSaveApplication((RequestParameters.SaveApplication)obj.Application);
                    ResponseValidateError responseValidateInsurance= this.ValidateApplicationInsurance(obj.ApplicationInsurance);
                    List<RequestParameters.BeneficiaryObject> beneficiaryObjectList = new List<RequestParameters.BeneficiaryObject>();
                    foreach (RequestParameters.ResubmitApplication.BeneficiariesResubmit beneficiary in obj.Beneficiaries)
                        beneficiaryObjectList.Add(new RequestParameters.BeneficiaryObject()
                        {
                            FULL_NAME = beneficiary.FULL_NAME,
                            AGE = beneficiary.AGE,
                            RELATION = beneficiary.RELATION,
                            PERCENTAGE_OF_SHARE = beneficiary.PERCENTAGE_OF_SHARE,
                            ADDRESS = beneficiary.ADDRESS,
                            REMARKS = beneficiary.REMARKS
                        });
                    ResponseValidateError responseValidateBen = this.ValidateApplicationBeneficiary(beneficiaryObjectList);
                    ResponseValidateError responseValidateQuestion = this.ValidateApplicationQuestionair(obj.Questionaire);
                    if (responseValidateCuz.detail != null)
                        return responseValidateCuz;
                    if (responseValidateApp.detail != null)
                        return responseValidateApp;
                    if (responseValidateInsurance.detail != null)
                        return responseValidateInsurance;
                    bl_micro_product_config productMicroProduct = da_micro_product_config.ProductConfig.GetProductMicroProduct(obj.ApplicationInsurance.PRODUCT_ID);
                    if (productMicroProduct.IsRequiredRider)
                    {
                        ResponseValidateError responseValidateRider = this.ValidateApplicationInsuranceRider(obj.ApplicationInsuranceRider);
                        if (responseValidateRider.detail != null)
                            return responseValidateRider;
                    }
                    if (productMicroProduct.BusinessType == bl_micro_product_config.BusinussTypeOption.BANCA_REFERRAL.ToString() && obj.Application.ReferrerId == null)
                    {
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "Referral ID",
                            message = "Referral Id is required."
                        });
                        responseValidateError = new ResponseValidateError()
                        {
                            message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                            detail = validateRequestList
                        };
                    }
                    if (responseValidateBen.detail != null)
                        return responseValidateBen;
                    if (responseValidateQuestion.detail != null)
                        return responseValidateQuestion;
                    List<Channel_Item_Config> channelConfigLists = new Channel_Item_Config().GetChannelItemConfig(obj.Application.ChannelItemId, 1);
                    Channel_Item_Config channelConfig = new Channel_Item_Config();
                    if (Channel_Item_Config.Transection)
                    {
                        foreach (var ch in channelConfigLists.Where(_ => _.ProductId == obj.ApplicationInsurance.PRODUCT_ID))
                            channelConfig = ch;
                        if (channelConfig.ProductId != null)
                        {
                            int maxPolicyCount = da_micro_policy.CountPolicyByCustomerIdTypeInYear(obj.Customer.ID_TYPE, obj.Customer.ID_NUMBER, Helper.FormatDateTime(obj.Application.ApplicationDate).Year);
                            if (da_micro_policy.SUCCESS)
                            {
                                if (maxPolicyCount > channelConfig.MaxPolicyPerLife)
                                {
                                    validateRequestList.Add(new ValidateRequest()
                                    {
                                        field = "Number of Policies",
                                        message = "Maximum policies count is exceed."
                                    });
                                    responseValidateError = new ResponseValidateError()
                                    {
                                        message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                                        detail = validateRequestList
                                    };
                                }
                               
                            }
                            else
                            {
                                validateRequestList.Add(new ValidateRequest()
                                {
                                    field = (string)null,
                                    message = "Count Inforce policy error."
                                });
                                responseValidateError = new ResponseValidateError()
                                {
                                    message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                                    detail = validateRequestList
                                };
                            }
                        }
                    }
                    else
                    {
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = (string)null,
                            message = "Get channel configuration error."
                        });
                        responseValidateError = new ResponseValidateError()
                        {
                            message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                            detail = validateRequestList
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                validateRequestList.Add(new ValidateRequest()
                {
                    field = "ERROR",
                    message = "Unexpected error."
                });
                responseValidateError = new ResponseValidateError()
                {
                    message = "Validation error",
                    detail = validateRequestList
                };
                Log.AddExceptionToLog("ApplicationController", "ValidateSummitApplication(RequestParameters.SubmitApplication obj)", ex);
            }
            return responseValidateError;
        }

        private ResponseValidateError ValidateReSummitApplication(
          RequestParameters.ResubmitApplication obj)
        {
            ResponseValidateError responseValidateError1 = new ResponseValidateError();
            List<ValidateRequest> validateRequestList = new List<ValidateRequest>();
            ResponseValidateError responseValidateError2;
            try
            {
                if (obj == null)
                {
                    validateRequestList.Add(new ValidateRequest()
                    {
                        field = "ResubmitApplication",
                        message = "Object reference not set to an instance of an object."
                    });
                    responseValidateError2 = new ResponseValidateError()
                    {
                        message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                        detail = validateRequestList
                    };
                }
                else
                {
                    ResponseValidateError responseValidateError3 = this.ValidateCustomer((RequestParameters.Customer)obj.Customer);
                    ResponseValidateError responseValidateError4 = this.ValidateSaveApplication((RequestParameters.SaveApplication)obj.Application);
                    ResponseValidateError responseValidateError5 = this.ValidateApplicationInsurance(obj.ApplicationInsurance);
                    bl_micro_product_config productMicroProduct = da_micro_product_config.ProductConfig.GetProductMicroProduct(obj.ApplicationInsurance.PRODUCT_ID);
                    ResponseValidateError responseValidateError6 = new ResponseValidateError();
                    if (productMicroProduct.ProductType == bl_micro_product_config.PRODUCT_TYPE.MICRO_LOAN.ToString())
                    {
                        if (obj.Beneficiaries != null)
                            this.ValidateApplicationBeneficiaryResubmit(obj.Beneficiaries);
                    }
                    else
                        this.ValidateApplicationBeneficiaryResubmit(obj.Beneficiaries);
                    ResponseValidateError responseValidateError7 = this.ValidateApplicationQuestionair(obj.Questionaire);
                    if (responseValidateError3.detail != null)
                        return responseValidateError3;
                    if (responseValidateError4.detail != null)
                        return responseValidateError4;
                    if (responseValidateError5.detail != null)
                        return responseValidateError5;
                    if (da_micro_product_config.ProductConfig.GetProductMicroProduct(obj.ApplicationInsurance.PRODUCT_ID).IsRequiredRider)
                    {
                        ResponseValidateError responseValidateError8 = this.ValidateApplicationInsuranceRider(obj.ApplicationInsuranceRider);
                        if (responseValidateError8.detail != null)
                            return responseValidateError8;
                    }
                    if (responseValidateError6.detail != null)
                        return responseValidateError6;
                    return responseValidateError7.detail != null ? responseValidateError7 : responseValidateError1;
                }
            }
            catch (Exception ex)
            {
                validateRequestList.Add(new ValidateRequest()
                {
                    field = "ERROR",
                    message = "Unexpected error."
                });
                responseValidateError2 = new ResponseValidateError()
                {
                    message = "Validation error",
                    detail = validateRequestList
                };
                Log.AddExceptionToLog("ApplicationController", "ValidateReSummitApplication(RequestParameters.ResubmitApplication obj)", ex);
            }
            return responseValidateError2;
        }

        private ResponseValidateError ValidateApplicationInquiry(
          RequestParameters.ApplicationInquiry obj)
        {
            ResponseValidateError responseValidateError = new ResponseValidateError();
            List<ValidateRequest> validateRequestList = new List<ValidateRequest>();
            try
            {
                if (obj == null)
                {
                    validateRequestList.Add(new ValidateRequest()
                    {
                        field = "ApplicationInquiry",
                        message = "Object reference not set to an instance of an object."
                    });
                }
                else
                {
                    if (obj.ChannelLocationId.Count == 0)
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "ChannelLocationId",
                            message = "Channel location id is required."
                        });
                    if (!Helper.IsDate(obj.ApplicationDateFrom))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "ApplicationDateFrom",
                            message = "Application date [From] is required.[DD-MM-YYYY]"
                        });
                    if (!Helper.IsDate(obj.ApplicationDateTo))
                        validateRequestList.Add(new ValidateRequest()
                        {
                            field = "ApplicationDateTo",
                            message = "Application date [To] is required.[DD-MM-YYYY]"
                        });
                }
                if (validateRequestList.Count > 0)
                    responseValidateError = new ResponseValidateError()
                    {
                        message = $"Validation error, The request has {validateRequestList.Count.ToString()} errors.",
                        detail = validateRequestList
                    };
            }
            catch (Exception ex)
            {
                validateRequestList.Add(new ValidateRequest()
                {
                    field = "ERROR",
                    message = "Unexpected error."
                });
                responseValidateError = new ResponseValidateError()
                {
                    message = "Validation error",
                    detail = validateRequestList
                };
                Log.AddExceptionToLog("ApplicationController", "ValidateReSummitApplication(RequestParameters.ResubmitApplication obj)", ex);
            }
            return responseValidateError;
        }

        private bool updateCustomer(
          RequestParameters.ResubmitApplication.CustomerResumbit objCust,
          string updatedBy,
          DateTime updatedOn,
          out string ErrorMessage)
        {
            try
            {
                bl_micro_application_customer APPLICATION_CUSTOMER = new bl_micro_application_customer();
                APPLICATION_CUSTOMER.CUSTOMER_ID = objCust.CustomerId;
                APPLICATION_CUSTOMER.ID_TYPE = objCust.ID_TYPE;
                APPLICATION_CUSTOMER.ID_NUMBER = objCust.ID_NUMBER;
                APPLICATION_CUSTOMER.FIRST_NAME_IN_ENGLISH = objCust.FIRST_NAME_IN_ENGLISH;
                APPLICATION_CUSTOMER.LAST_NAME_IN_ENGLISH = objCust.LAST_NAME_IN_ENGLISH;
                APPLICATION_CUSTOMER.FIRST_NAME_IN_KHMER = objCust.FIRST_NAME_IN_KHMER;
                APPLICATION_CUSTOMER.LAST_NAME_IN_KHMER = objCust.LAST_NAME_IN_KHMER;
                APPLICATION_CUSTOMER.GENDER = objCust.GENDER;
                APPLICATION_CUSTOMER.DATE_OF_BIRTH = Helper.FormatDateTime(objCust.DATE_OF_BIRTH);
                APPLICATION_CUSTOMER.NATIONALITY = objCust.NATIONALITY;
                APPLICATION_CUSTOMER.OCCUPATION = objCust.OCCUPATION;
                APPLICATION_CUSTOMER.MARITAL_STATUS = objCust.MARITAL_STATUS;
                APPLICATION_CUSTOMER.PHONE_NUMBER1 = objCust.PHONE_NUMBER;
                APPLICATION_CUSTOMER.EMAIL1 = objCust.EMAIL;
                APPLICATION_CUSTOMER.HOUSE_NO_EN = objCust.HOUSE_NO;
                APPLICATION_CUSTOMER.HOUSE_NO_KH = objCust.HOUSE_NO;
                APPLICATION_CUSTOMER.STREET_NO_EN = objCust.STREET_NO;
                APPLICATION_CUSTOMER.STREET_NO_KH = objCust.STREET_NO;
                APPLICATION_CUSTOMER.VILLAGE_EN = objCust.VILLAGE;
                APPLICATION_CUSTOMER.VILLAGE_KH = objCust.VILLAGE;
                APPLICATION_CUSTOMER.COMMUNE_EN = objCust.COMMUNE;
                APPLICATION_CUSTOMER.COMMUNE_KH = objCust.COMMUNE;
                APPLICATION_CUSTOMER.DISTRICT_EN = objCust.DISTRICT;
                APPLICATION_CUSTOMER.DISTRICT_KH = objCust.DISTRICT;
                APPLICATION_CUSTOMER.PROVINCE_EN = objCust.PROVINCE;
                APPLICATION_CUSTOMER.PROVINCE_KH = objCust.PROVINCE;
                APPLICATION_CUSTOMER.UPDATED_BY = updatedBy;
                APPLICATION_CUSTOMER.UPDATED_ON = updatedOn;
                ErrorMessage = "";
                return da_micro_application_customer.UpdateApplicationCustomer(APPLICATION_CUSTOMER);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Log.AddExceptionToLog(nameof(ApplicationFormController), "updateCustomer(RequestParameters.ResubmitApplication.CustomerResumbit objCust, string updatedBy, DateTime updatedOn)", ex);
                return false;
            }
        }

        private bool updateApplication(
          RequestParameters.ResubmitApplication.ApplicationResubmit objApp,
          string applicationNumber,
          string customerId,
          DateTime updatedOn,
          out string ErrorMessage)
        {
            try
            {
                bl_micro_application APPLICATION = new bl_micro_application();
                APPLICATION.APPLICATION_NUMBER = applicationNumber;
                APPLICATION.APPLICATION_DATE = Helper.FormatDateTime(objApp.ApplicationDate);
                APPLICATION.CHANNEL_ITEM_ID = objApp.ChannelItemId;
                APPLICATION.CHANNEL_LOCATION_ID = objApp.ChannelLocationId;
                APPLICATION.CHANNEL_ID = objApp.ChannelId;
                APPLICATION.APPLICATION_CUSTOMER_ID = customerId;
                APPLICATION.REMARKS = objApp.Remarks;
                APPLICATION.SALE_AGENT_ID = objApp.AgentCode;
                APPLICATION.REFERRER_ID = objApp.ReferrerId;
                APPLICATION.CLIENT_TYPE = objApp.ClientType;
                APPLICATION.CLIENT_TYPE_RELATION = objApp.ClientTypeRelation;
                APPLICATION.CLIENT_TYPE_REMARKS = objApp.ClientTypeRemarks;
                APPLICATION.RENEW_FROM_POLICY = "";
                APPLICATION.CREATED_BY = objApp.UpdatedBy;
                APPLICATION.CREATED_ON = updatedOn;
                APPLICATION.LoanNumber = objApp.LoanNumber;
                APPLICATION.PolicyholderName = objApp.PolicyholderName;
                APPLICATION.PolicyholderGender = objApp.PolicyholderGender;
                APPLICATION.PolicyholderDOB = string.IsNullOrWhiteSpace(objApp.PolicyholderDOB) ? new DateTime(1900, 1, 1) : Helper.FormatDateTime(objApp.PolicyholderDOB);
                APPLICATION.PolicyholderIDType = objApp.PolicyholderIDType;
                APPLICATION.PolicyholderIDNo = objApp.PolicyholderIDNo;
                APPLICATION.PolicyholderPhoneNumber = objApp.PolicyholderPhoneNumber;
                APPLICATION.PolicyholderPhoneNumber2 = objApp.PolicyholderPhoneNumber2;
                APPLICATION.PolicyholderEmail = objApp.PolicyholderEmail;
                APPLICATION.PolicyholderAddress = objApp.PolicyholderAddress;
                ErrorMessage = "";
                return da_micro_application.UpdateApplication(APPLICATION);
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog(nameof(ApplicationFormController), "updateApplication(RequestParameters.ResubmitApplication.ApplicationResubmit objApp, string applicationNumber, string customerId, string updatedBy, DateTime updatedOn, out string ErrorMessage)", ex);
                ErrorMessage = ex.Message;
                return false;
            }
        }

        private bool updateApplicationInsurance(
          RequestParameters.SaveApplicationInsurance objInsurance,
          string applicationNumber,
          string updatedBy,
          DateTime updatedOn,
          out string ErrorMessage)
        {
            try
            {
                bl_micro_application_insurance APP_INSURANCE = new bl_micro_application_insurance();
                APP_INSURANCE.APPLICATION_NUMBER = applicationNumber;
                APP_INSURANCE.PRODUCT_ID = objInsurance.PRODUCT_ID;
                APP_INSURANCE.TERME_OF_COVER = objInsurance.TERME_OF_COVER;
                APP_INSURANCE.PAYMENT_PERIOD = objInsurance.PAYMENT_PERIOD;
                APP_INSURANCE.SUM_ASSURE = objInsurance.SUM_ASSURE;
                APP_INSURANCE.PAY_MODE = objInsurance.PAY_MODE;
                APP_INSURANCE.PREMIUM = objInsurance.PREMIUM;
                APP_INSURANCE.ANNUAL_PREMIUM = objInsurance.ANNUAL_PREMIUM;
                APP_INSURANCE.USER_PREMIUM = 0.0;
                APP_INSURANCE.DISCOUNT_AMOUNT = objInsurance.DISCOUNT_AMOUNT;
                APP_INSURANCE.TOTAL_AMOUNT = objInsurance.TOTAL_AMOUNT;
                APP_INSURANCE.PACKAGE = objInsurance.PACKAGE;
                APP_INSURANCE.PAYMENT_CODE = objInsurance.PAYMENT_CODE;
                APP_INSURANCE.REMARKS = objInsurance.REMARKS;
                APP_INSURANCE.UPDATED_BY = updatedBy;
                APP_INSURANCE.UPDATED_ON = updatedOn;
                APP_INSURANCE.COVER_TYPE = objInsurance.COVER_TYPE;
                ErrorMessage = "";
                return da_micro_application_insurance.UpdateApplicationInsurance(APP_INSURANCE);
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog(nameof(ApplicationFormController), "updateApplicationInsurance(RequestParameters.SaveApplicationInsurance objInsurance, string applicationNumber, string updatedBy , DateTime updatedOn, out string ErrorMessage)", ex);
                ErrorMessage = ex.Message;
                return false;
            }
        }

        private bool updateApplicationInsuranceRider(
          RequestParameters.SaveApplicationInsuranceRider objRider,
          string applicationNumber,
          string updatedBy,
          DateTime updatedOn,
          out string ErrorMessage)
        {
            try
            {
                bl_micro_application_insurance_rider APP_INSURANCE_RIDER = new bl_micro_application_insurance_rider();
                APP_INSURANCE_RIDER.APPLICATION_NUMBER = applicationNumber;
                APP_INSURANCE_RIDER.PRODUCT_ID = objRider.PRODUCT_ID;
                APP_INSURANCE_RIDER.SUM_ASSURE = objRider.SUM_ASSURE;
                APP_INSURANCE_RIDER.PREMIUM = objRider.PREMIUM;
                APP_INSURANCE_RIDER.ANNUAL_PREMIUM = objRider.ANNUAL_PREMIUM;
                APP_INSURANCE_RIDER.DISCOUNT_AMOUNT = objRider.DISCOUNT_AMOUNT;
                APP_INSURANCE_RIDER.TOTAL_AMOUNT = objRider.TOTAL_AMOUNT;
                APP_INSURANCE_RIDER.REMARKS = objRider.REMARKS;
                APP_INSURANCE_RIDER.UPDATED_BY = updatedBy;
                APP_INSURANCE_RIDER.UPDATED_ON = updatedOn;
                ErrorMessage = "";
                return da_micro_application_insurance_rider.UpdateApplicationInsuranceRider(APP_INSURANCE_RIDER);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Log.AddExceptionToLog(nameof(ApplicationFormController), "updateApplicationInsuranceRider(RequestParameters.SaveApplicationInsuranceRider objRider, string applicationNumber, string updatedBy, DateTime updatedOn, out string ErrorMessage)", ex);
                return false;
            }
        }

        private bool addApplicationInsuranceRider(
          RequestParameters.SaveApplicationInsuranceRider objRider,
          string applicationNumber,
          string createdBy,
          DateTime createdOn,
          out string ErrorMessage)
        {
            try
            {
                bl_micro_application_insurance_rider APP_INSURANCE_RIDER = new bl_micro_application_insurance_rider();
                APP_INSURANCE_RIDER.APPLICATION_NUMBER = applicationNumber;
                APP_INSURANCE_RIDER.PRODUCT_ID = objRider.PRODUCT_ID;
                APP_INSURANCE_RIDER.SUM_ASSURE = objRider.SUM_ASSURE;
                APP_INSURANCE_RIDER.PREMIUM = objRider.PREMIUM;
                APP_INSURANCE_RIDER.ANNUAL_PREMIUM = objRider.ANNUAL_PREMIUM;
                APP_INSURANCE_RIDER.DISCOUNT_AMOUNT = objRider.DISCOUNT_AMOUNT;
                APP_INSURANCE_RIDER.TOTAL_AMOUNT = objRider.TOTAL_AMOUNT;
                APP_INSURANCE_RIDER.REMARKS = objRider.REMARKS;
                APP_INSURANCE_RIDER.CREATED_BY = createdBy;
                APP_INSURANCE_RIDER.CREATED_ON = createdOn;
                ErrorMessage = "";
                return da_micro_application_insurance_rider.SaveApplicationInsuranceRider(APP_INSURANCE_RIDER);
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog(nameof(ApplicationFormController), "addApplicationInsuranceRider(RequestParameters.SaveApplicationInsuranceRider objRider, string applicationNumber, string createdBy, DateTime createdOn, out string ErrorMessage)", ex);
                ErrorMessage = ex.Message;
                return false;
            }
        }

        private bool updateBeneficiary(
          RequestParameters.ResubmitApplication.BeneficiariesResubmit objBen,
          string applicationNumber,
          string updatedBy,
          DateTime updatedOn,
          out string ErrorMessage)
        {
            try
            {
                bl_micro_application_beneficiary APP_BENEFICIARY = new bl_micro_application_beneficiary();
                APP_BENEFICIARY.ID = objBen.Id;
                APP_BENEFICIARY.FULL_NAME = objBen.FULL_NAME;
                APP_BENEFICIARY.AGE = objBen.AGE;
                APP_BENEFICIARY.RELATION = objBen.RELATION;
                APP_BENEFICIARY.ADDRESS = objBen.ADDRESS;
                APP_BENEFICIARY.PERCENTAGE_OF_SHARE = objBen.PERCENTAGE_OF_SHARE;
                APP_BENEFICIARY.APPLICATION_NUMBER = applicationNumber;
                APP_BENEFICIARY.UPDATED_BY = updatedBy;
                APP_BENEFICIARY.UPDATED_ON = updatedOn;
                APP_BENEFICIARY.Gender = objBen.Gender;
                APP_BENEFICIARY.DOB = string.IsNullOrWhiteSpace(objBen.DOB) ? new DateTime(1900, 1, 1) : Helper.FormatDateTime(objBen.DOB);
                APP_BENEFICIARY.IdType = objBen.IdType;
                APP_BENEFICIARY.IdNo = objBen.IdNo;
                ErrorMessage = "";
                return da_micro_application_beneficiary.UpdateApplicationBeneficiary(APP_BENEFICIARY);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Log.AddExceptionToLog(nameof(ApplicationFormController), "updateBeneficiary(RequestParameters.ResubmitApplication.BeneficiariesResubmit objBen, string applicationNumber, string updatedBy, DateTime updatedOn, out string ErrorMessage)", ex);
                return false;
            }
        }

        private bool addBeneficiary(
          RequestParameters.ResubmitApplication.BeneficiariesResubmit objBen,
          string applicationNumber,
          string createdBy,
          DateTime createdOn,
          out string ErrorMessage)
        {
            try
            {
                bl_micro_application_beneficiary APP_BENEFICIARY = new bl_micro_application_beneficiary();
                APP_BENEFICIARY.FULL_NAME = objBen.FULL_NAME;
                APP_BENEFICIARY.AGE = objBen.AGE;
                APP_BENEFICIARY.RELATION = objBen.RELATION;
                APP_BENEFICIARY.ADDRESS = objBen.ADDRESS;
                APP_BENEFICIARY.PERCENTAGE_OF_SHARE = objBen.PERCENTAGE_OF_SHARE;
                APP_BENEFICIARY.APPLICATION_NUMBER = applicationNumber;
                APP_BENEFICIARY.CREATED_BY = createdBy;
                APP_BENEFICIARY.CREATED_ON = createdOn;
                APP_BENEFICIARY.Gender = objBen.Gender;
                APP_BENEFICIARY.DOB = string.IsNullOrWhiteSpace(objBen.DOB) ? new DateTime(1900, 1, 1) : Helper.FormatDateTime(objBen.DOB);
                APP_BENEFICIARY.IdType = objBen.IdType;
                APP_BENEFICIARY.IdNo = objBen.IdNo;
                ErrorMessage = "";
                return da_micro_application_beneficiary.SaveApplicationBeneficiary(APP_BENEFICIARY);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Log.AddExceptionToLog(nameof(ApplicationFormController), "addBeneficiary(RequestParameters.ResubmitApplication.BeneficiariesResubmit objBen, string applicationNumber, string createdBy, DateTime createdOn, out string ErrorMessage)", ex);
                return false;
            }
        }

        private bool updateQuestion(
          RequestParameters.SaveApplicationQuestionaire objQuestion,
          string applicationNumber,
          string updatedBy,
          DateTime updatedOn,
          out string ErrorMessage)
        {
            try
            {
                bl_micro_application_questionaire QUESTIONIARE = new bl_micro_application_questionaire();
                QUESTIONIARE.QUESTION_ID = objQuestion.QUESTION_ID;
                QUESTIONIARE.APPLICATION_NUMBER = applicationNumber;
                QUESTIONIARE.ANSWER = objQuestion.ANSWER;
                QUESTIONIARE.ANSWER_REMARKS = objQuestion.ANSWER_REMARKS;
                QUESTIONIARE.UPDATED_BY = updatedBy;
                QUESTIONIARE.UPDATED_ON = updatedOn;
                ErrorMessage = "";
                return da_micro_application_questionaire.UpdateQuestionaire(QUESTIONIARE);
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog(nameof(ApplicationFormController), "updateQuestion(RequestParameters.SaveApplicationQuestionaire objQuestion, string applicationNumber, string updatedBy, DateTime updatedOn, out string ErrorMessage)", ex);
                ErrorMessage = ex.Message;
                return false;
            }
        }

        private void roleBack(
          List<string> newAppList,
          List<string> existAppList,
          string userName,
          DateTime tranDate,
          out string message)
        {
            string str = "";
            foreach (string newApp in newAppList)
                this.RoleBackApplicaitonSubmit("", newApp);
            foreach (string existApp in existAppList)
                str = !da_micro_application.RestoreApplication(existApp, userName, tranDate) ? str + (str == "" ? existApp + " fail " : existApp + " fail <br />") : str + (str == "" ? existApp + " success " : existApp + " success <br />");
            message = str;
        }

        public class RequestParameters
        {
            public class SaveApplication
            {
                public SaveApplication()
                {
                    if (this.ReferrerId == null)
                        this.ReferrerId = "";
                    if (this.Remarks != null)
                        return;
                    this.Remarks = "";
                }

                public string ApplicationDate { get; set; }

                public string ChannelId { get; set; }

                public string ChannelItemId { get; set; }

                public string ChannelLocationId { get; set; }

                public string AgentCode { get; set; }

                public string ReferrerId { get; set; }

                public string Remarks { get; set; }

                public string ClientType { get; set; }

                public string ClientTypeRemarks { get; set; }

                public string ClientTypeRelation { get; set; }

                public int NumberOfPurchasingYears { get; set; }

                public int NumberOfApplications { get; set; }

                public string LoanNumber { get; set; }

                public string PolicyholderName { get; set; }

                public int PolicyholderGender { get; set; }

                public string PolicyholderDOB { get; set; }

                public int PolicyholderIDType { get; set; }

                public string PolicyholderIDNo { get; set; }

                public string PolicyholderPhoneNumber { get; set; }

                public string PolicyholderPhoneNumber2 { get; set; }

                public string PolicyholderEmail { get; set; }

                public string PolicyholderAddress { get; set; }
            }

            public class SaveApplicationInsurance
            {
                public string PRODUCT_ID { get; set; }

                public int TERME_OF_COVER { get; set; }

                public string COVER_TYPE { get; set; }

                public int PAYMENT_PERIOD { get; set; }

                public double SUM_ASSURE { get; set; }

                public int PAY_MODE { get; set; }

                public string PAYMENT_CODE { get; set; }

                public double PREMIUM { get; set; }

                public double ANNUAL_PREMIUM { get; set; }

                public double TOTAL_AMOUNT { get; set; }

                public string PACKAGE { get; set; }

                public double DISCOUNT_AMOUNT { get; set; }

                public string REMARKS { get; set; }

                public List<RequestParameters.SaveApplicationInsurance.SumAssurePremiumEntry> SumAssurePremium { get; set; }

                public class SumAssurePremiumEntry
                {
                    public double SumAssure { get; set; }

                    public double Premium { get; set; }

                    public double DiscountAmount { get; set; }

                    public double TotalAmount { get; set; }

                    public double AnnualPremium { get; set; }
                }
            }

            public class SaveApplicationInsuranceRider
            {
                public string PRODUCT_ID { get; set; }

                public double SUM_ASSURE { get; set; }

                public double PREMIUM { get; set; }

                public double ANNUAL_PREMIUM { get; set; }

                public double DISCOUNT_AMOUNT { get; set; }

                public double TOTAL_AMOUNT { get; set; }

                public string REMARKS { get; set; }
            }

            public class SaveApplicationBeneficiary
            {
                public List<RequestParameters.BeneficiaryObject> Beneficiary { get; set; }
            }

            public class BeneficiaryObject
            {
                public string FULL_NAME { get; set; }

                public string AGE { get; set; }

                public string RELATION { get; set; }

                public double PERCENTAGE_OF_SHARE { get; set; }

                public string ADDRESS { get; set; }

                public string REMARKS { get; set; }

                public string DOB { get; set; }

                public int Gender { get; set; }

                public int IdType { get; set; }

                public string IdNo { get; set; }
            }

            public class SavePrimaryBeneficiary
            {
                public string FullName { get; set; }

                public string LoanNumber { get; set; }

                public string Address { get; set; }
            }

            public class SaveApplicationQuestionaire
            {
                public string QUESTION_ID { get; set; }

                public int ANSWER { get; set; }

                public string ANSWER_REMARKS { get; set; }

                public string REMARKS { get; set; }
            }

            public class Customer
            {
                public Customer()
                {
                    if (this.HOUSE_NO == null)
                        this.HOUSE_NO = "";
                    if (this.STREET_NO == null)
                        this.STREET_NO = "";
                    if (this.DISTRICT == null)
                        this.DISTRICT = "";
                    if (this.COMMUNE == null)
                        this.COMMUNE = "";
                    if (this.VILLAGE != null)
                        return;
                    this.VILLAGE = "";
                }

                public int ID_TYPE { get; set; }

                public string ID_NUMBER { get; set; }

                public string FIRST_NAME_IN_ENGLISH { get; set; }

                public string LAST_NAME_IN_ENGLISH { get; set; }

                public string FIRST_NAME_IN_KHMER { get; set; }

                public string LAST_NAME_IN_KHMER { get; set; }

                public int GENDER { get; set; }

                public string DATE_OF_BIRTH { get; set; }

                public string NATIONALITY { get; set; }

                public string MARITAL_STATUS { get; set; }

                public string OCCUPATION { get; set; }

                public string HOUSE_NO { get; set; }

                public string STREET_NO { get; set; }

                public string VILLAGE { get; set; }

                public string COMMUNE { get; set; }

                public string DISTRICT { get; set; }

                public string PROVINCE { get; set; }

                public string PHONE_NUMBER { get; set; }

                public string EMAIL { get; set; }

                public string CREATED_BY { get; set; }

                public string UPDATED_BY { get; set; }

                public string REMARKS { get; set; }
            }

            public class RoleBackAppliction
            {
                public string ApplicationCustomerId { get; set; }

                public string ApplicationNumber { get; set; }
            }

            public class SubmitApplication
            {
                public RequestParameters.Customer Customer { get; set; }

                public RequestParameters.SaveApplication Application { get; set; }

                public RequestParameters.SaveApplicationInsurance ApplicationInsurance { get; set; }

                public RequestParameters.SaveApplicationInsuranceRider ApplicationInsuranceRider { get; set; }

                public List<RequestParameters.BeneficiaryObject> Beneficiaries { get; set; }

                public RequestParameters.SavePrimaryBeneficiary PrimaryBeneficiary { get; set; }

                public RequestParameters.SaveApplicationQuestionaire Questionaire { get; set; }
            }

            public class ResubmitApplication
            {
                public RequestParameters.ResubmitApplication.CustomerResumbit Customer { get; set; }

                public RequestParameters.ResubmitApplication.ApplicationResubmit Application { get; set; }

                public RequestParameters.SaveApplicationInsurance ApplicationInsurance { get; set; }

                public RequestParameters.SaveApplicationInsuranceRider ApplicationInsuranceRider { get; set; }

                public List<RequestParameters.ResubmitApplication.BeneficiariesResubmit> Beneficiaries { get; set; }

                public List<RequestParameters.ResubmitApplication.BeneficiariesResubmit> BeneficiariesToDelete { get; set; }

                public RequestParameters.SaveApplicationQuestionaire Questionaire { get; set; }

                public RequestParameters.SavePrimaryBeneficiary PrimaryBeneficiary { get; set; }

                public class CustomerResumbit : RequestParameters.Customer
                {
                    public string CustomerId { get; set; }
                }

                public class ApplicationResubmit : RequestParameters.SaveApplication
                {
                    public string ApplicationId { get; set; }

                    public string UpdatedBy { get; set; }
                }

                public class BeneficiariesResubmit :
                  RequestParameters.BeneficiaryObject
                {
                    public string Id { get; set; }
                }
            }

            public class ApplicationInquiry
            {
                public List<string> ChannelLocationId { get; set; }

                public string ApplicationNumber { get; set; }

                public string FullNameEn { get; set; }

                public string IdNumber { get; set; }

                public string PolicyNumber { get; set; }

                public string ApplicationDateFrom { get; set; }

                public string ApplicationDateTo { get; set; }
            }

            public class PrintApplications
            {
                public List<string> ApplicationNumber { get; set; }

                public bool OnlyFirstYear { get; set; }
            }
        }

        public class ResponseParameters
        {
            public class SaveApplication
            {
                public string ApplicationNumber { get; set; }

                public string ApplicationId { get; set; }
            }

            public class SaveApplicationInsurance
            {
                public string ApplicationNumber { get; set; }

                public double Premium { get; set; }

                public double TototalPremium { get; set; }
            }

            public class SaveCustomer
            {
                public string CustomerId { get; set; }
            }
        }

        public class CountSavedApplication
        {
            public string ApplicationNumber { get; set; }

            public string ApplicationId { get; set; }

            public string CustomerId { get; set; }

            public bool IsMainApplication { get; set; }

            public string ClientType { get; set; }
        }
    }
}
