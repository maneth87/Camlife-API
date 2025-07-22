using CamlifeAPI1.Class;
using CamlifeAPI1.Class.Application;
using Microsoft.AspNetCore.Mvc;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Web;
using System.Web.UI;
using static bl_micro_application_beneficiary;
using static Calculation;
using static Helper;
/// <summary>
/// Summary description for da_micro_application
/// </summary>
public class da_micro_application
{
    public da_micro_application()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private static bool _SUCCESS = false;
    private static string _MESSAGE = "";
    private static string _CODE = "";
    public static bool SUCCESS { get { return _SUCCESS; } }
    public static string MESSAGE { get { return _MESSAGE; } }
    public static string CODE { get { return _CODE; } }

    private static DB db = new DB();
    public static bool SaveApplication(bl_micro_application APPLICATION)
    {
        bool result = false;

        try
        {
            //  DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_INSERT", new string[,] {
            {"@APPLICATION_ID", APPLICATION.APPLICATION_ID},
            {"@SEQ",APPLICATION.SEQ+""},
            {"@APPLICATION_NUMBER",APPLICATION.APPLICATION_NUMBER},
            {"@APPLICATION_DATE", APPLICATION.APPLICATION_DATE+""},
             {"@CHANNEL_ID",APPLICATION.CHANNEL_ID},
            {"@CHANNEL_ITEM_ID",APPLICATION.CHANNEL_ITEM_ID},
            {"@CHANNEL_LOCATION_ID",APPLICATION.CHANNEL_LOCATION_ID},
            {"@SALE_AGENT_ID",APPLICATION.SALE_AGENT_ID},
            {"@APPLICATION_CUSTOMER_ID",APPLICATION.APPLICATION_CUSTOMER_ID},
            { "@REFERRER_ID", APPLICATION.REFERRER_ID},
            {"@RENEW_FROM_POLICY", APPLICATION.RENEW_FROM_POLICY},
            {"@CREATED_BY",APPLICATION.CREATED_BY},
            {"@CREATED_ON", APPLICATION.CREATED_ON+""},
            {"@REMARKS",APPLICATION.REMARKS},
            {"@CLIENT_TYPE", APPLICATION.CLIENT_TYPE},
            {"@CLIENT_TYPE_REMARKS", APPLICATION.CLIENT_TYPE_REMARKS},
            {"@CLIENT_TYPE_RELATION", APPLICATION.CLIENT_TYPE_RELATION},
            {"@MAIN_APPLICATION", APPLICATION.MainApplicationNumber },
            {"@NUMBERS_OF_PURCHASING_YEAR", APPLICATION.NumbersOfPurchasingYear+"" },
            {"@NUMBERS_OF_APPLICATION_FIRST_YEAR", APPLICATION.NumbersOfApplicationFirstYear+"" },
            {"@LOAN_NUMBER", APPLICATION.LoanNumber},
            {"@POLICYHOLDER_NAME",APPLICATION.PolicyholderName == null ? "" : APPLICATION.PolicyholderName },
            {"@POLICYHOLDER_GENDER", APPLICATION.PolicyholderGender+"" },
            {"@POLICYHOLDER_DOB", APPLICATION.PolicyholderDOB+"" },
            {"@POLICYHOLDER_ID_TYPE", APPLICATION.PolicyholderIDType+"" },
            {"@POLICYHOLDER_ID_NO", APPLICATION.PolicyholderIDNo == null ? "" : APPLICATION.PolicyholderIDNo },
            {"@POLICYHOLDER_PHONE_NUMBER",APPLICATION.PolicyholderPhoneNumber == null ? "" : APPLICATION.PolicyholderPhoneNumber },
            {"@POLICYHOLDER_PHONE_NUMBER2",APPLICATION.PolicyholderPhoneNumber2 == null ? "" : APPLICATION.PolicyholderPhoneNumber2 },
            {"@POLICYHOLDER_EMAIL",APPLICATION.PolicyholderEmail == null ? "" : APPLICATION.PolicyholderEmail },
            {"@POLICYHOLDER_ADDRESS", APPLICATION.PolicyholderAddress == null ? "" : APPLICATION.PolicyholderAddress }
        }, "da_micro_application => SaveApplication(bl_micro_application APPLICATION)");

            if (db.RowEffect == -1)
            {
                _MESSAGE = db.Message;
                _SUCCESS = false;
            }
            else
            {
                _MESSAGE = "Sucess";
                _SUCCESS = true;
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("Error function [SaveApplication(bl_micro_application APPLICATION)] in class [da_micro_application], detail: " + ex.Message + " => " + ex.StackTrace);
        }

        return result;
    }
    public static bool UpdateApplication(bl_micro_application APPLICATION)
    {
        bool result = false;

        try
        {
            //DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_UPDATE", new string[,] {
            {"@APPLICATION_ID", APPLICATION.APPLICATION_ID},
            {"@SEQ",APPLICATION.SEQ+""},
            {"@APPLICATION_NUMBER",APPLICATION.APPLICATION_NUMBER},
            {"@APPLICATION_DATE", APPLICATION.APPLICATION_DATE+""},
            {"@CHANNEL_ID",APPLICATION.CHANNEL_ID},
            {"@CHANNEL_ITEM_ID",APPLICATION.CHANNEL_ITEM_ID},
            {"@CHANNEL_LOCATION_ID",APPLICATION.CHANNEL_LOCATION_ID},
            {"@SALE_AGENT_ID",APPLICATION.SALE_AGENT_ID},
            {"@APPLICATION_CUSTOMER_ID",APPLICATION.APPLICATION_CUSTOMER_ID},
            {"@UPDATED_BY",APPLICATION.CREATED_BY},
            {"@UPDATED_ON", APPLICATION.CREATED_ON+""},
            {"@REMARKS",APPLICATION.REMARKS},
            {"@REFERRER_ID", APPLICATION.REFERRER_ID },
             {"@RENEW_FROM_POLICY", APPLICATION.RENEW_FROM_POLICY},
            {"@CLIENT_TYPE", APPLICATION.CLIENT_TYPE},
            {"@CLIENT_TYPE_REMARKS", APPLICATION.CLIENT_TYPE_REMARKS},
            {"@CLIENT_TYPE_RELATION", APPLICATION.CLIENT_TYPE_RELATION},
            {"@LOAN_NUMBER", APPLICATION.LoanNumber},
             {"@POLICYHOLDER_NAME",APPLICATION.PolicyholderName == null ? "" : APPLICATION.PolicyholderName },
            {"@POLICYHOLDER_GENDER", APPLICATION.PolicyholderGender+"" },
            {"@POLICYHOLDER_DOB", APPLICATION.PolicyholderDOB+"" },
            {"@POLICYHOLDER_ID_TYPE", APPLICATION.PolicyholderIDType+"" },
            {"@POLICYHOLDER_ID_NO", APPLICATION.PolicyholderIDNo == null ? "" : APPLICATION.PolicyholderIDNo },
            {"@POLICYHOLDER_PHONE_NUMBER",APPLICATION.PolicyholderPhoneNumber == null ? "" : APPLICATION.PolicyholderPhoneNumber },
            {"@POLICYHOLDER_PHONE_NUMBER2",APPLICATION.PolicyholderPhoneNumber2 == null ? "" : APPLICATION.PolicyholderPhoneNumber2 },
            {"@POLICYHOLDER_EMAIL",APPLICATION.PolicyholderEmail == null ? "" : APPLICATION.PolicyholderEmail },
            {"@POLICYHOLDER_ADDRESS", APPLICATION.PolicyholderAddress == null ? "" : APPLICATION.PolicyholderAddress }
            }, "da_micro_application => UpdateApplication(bl_micro_application APPLICATION)");

            if (db.RowEffect == -1)
            {
                _MESSAGE = db.Message;
                _SUCCESS = false;
            }
            else
            {
                _MESSAGE = "Sucess";
                _SUCCESS = true;
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("Error function [UpdateApplication(bl_micro_application APPLICATION)] in class [da_micro_application], detail: " + ex.Message + " => " + ex.StackTrace);
        }

        return result;
    }

    public static bool DeleteApplication(string APPLICATION_NUMBER)
    {
        bool result = false;
        try
        {
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_DELETE", new string[,] {
            {"@APPLICATION_NUMBER", APPLICATION_NUMBER}

            }, "da_micro_application=>DeleteApplication(string APPLICATION_NUMBER)");
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [DeleteApplication(string APPLICATION_NUMBER)] in class [da_micro_application], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }

    public static bool UpdateApplicationStatus(string applicationNumber, string status, string updatedBy, DateTime updatedOn, string updatedRemarks)
    {
        bool result = false;
        try
        {
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_UPDATE_STATUS", new string[,] {
            {"@APPLICATION_NUMBER", applicationNumber}
            ,{"@STATUS", status}
                ,{"@UPDATED_BY", updatedBy}
                ,{"@UPDATED_ON", updatedOn+""}
                ,{"@UPDATED_REMARKS", updatedRemarks}
            }, "da_micro_application=>UpdateApplicationStatus(string applicationNumber,string status, string updatedBy, DateTime updatedOn, string updatedRemarks)");
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [UpdateApplicationStatus(string applicationNumber,string status, string updatedBy, DateTime updatedOn, string updatedRemarks)] in class [da_micro_application], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }
    /// <summary>
    /// Update total of applications
    /// </summary>
    /// <param name="applicationNumber"></param>
    /// <param name="status"></param>
    /// <param name="updatedBy"></param>
    /// <param name="updatedOn"></param>
    /// <param name="updatedRemarks"></param>
    /// <returns></returns>
    public static bool UpdateApplicationTotalNumbers(string applicationNumber, int numbersOfApplicationFirstYear, int numbersOfPurchasingYear, string updatedBy, DateTime updatedOn, string updatedRemarks)
    {
        bool result = false;
        try
        {
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_UPDATE_NUMBERS_OF_APP", new string[,] {
            {"@APPLICATION_NUMBER", applicationNumber}
            ,{"@NUMBERS_OF_APPLICATION_FIRST_YEAR", numbersOfApplicationFirstYear+""}
            ,{"@NUMBERS_OF_PURCHASING_YEAR", numbersOfPurchasingYear+""}
            ,{"@UPDATED_BY", updatedBy}
            ,{"@UPDATED_ON", updatedOn+""}
            ,{"@UPDATED_REMARKS", updatedRemarks}
            }, "da_micro_application=>UpdateApplicationTotalNumbers(string applicationNumber, int numbersOfApplicationFirstYear, int numbersOfPurchasingYear, string updatedBy, DateTime updatedOn, string updatedRemarks)");
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [UpdateApplicationTotalNumbers(string applicationNumber, int numbersOfApplicationFirstYear, int numbersOfPurchasingYear, string updatedBy, DateTime updatedOn, string updatedRemarks)] in class [da_micro_application], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }
    public static bl_micro_application GetApplication(string APPLICATION_NUMBER)
    {

        bl_micro_application app = new bl_micro_application();
        try
        {
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_GET_BY_APPLICATION_NUMBER", new string[,] {
            {"@APPLICATION_NUMBER", APPLICATION_NUMBER}
            }, "da_micro_application=>GetApplication(string APPLICATION_NUMBER)");
            if (db.RowEffect == -1)
            {
                _SUCCESS = false;
                _MESSAGE = db.Message;
                _CODE = db.Code;
            }
            else
            {
                if (tbl.Rows.Count > 0)
                {
                    var row = tbl.Rows[0];
                    app = new bl_micro_application()
                    {

                        APPLICATION_ID = row["application_id"].ToString(),
                        APPLICATION_NUMBER = row["application_number"].ToString(),
                        APPLICATION_DATE = Convert.ToDateTime(row["application_date"].ToString()),
                        SEQ = Convert.ToInt32(row["seq"].ToString()),
                        APPLICATION_CUSTOMER_ID = row["application_customer_id"].ToString(),
                        CHANNEL_ITEM_ID = row["channel_item_id"].ToString(),
                        CHANNEL_LOCATION_ID = row["channel_location_id"].ToString(),
                        SALE_AGENT_ID = row["sale_agent_id"].ToString(),
                        CREATED_BY = row["created_by"].ToString(),
                        CREATED_ON = Convert.ToDateTime(row["created_on"].ToString()),
                        UPDATED_BY = row["updated_by"].ToString(),
                        UPDATED_ON = Convert.ToDateTime(row["updated_on"].ToString()),
                        REMARKS = row["remarks"].ToString(),
                        CLIENT_TYPE = row["CLIENT_TYPE"].ToString(),
                        CLIENT_TYPE_RELATION = row["CLIENT_TYPE_RELATION"].ToString(),
                        CLIENT_TYPE_REMARKS = row["CLIENT_TYPE_REMARKS"].ToString(),
                        LoanNumber = row["LOAN_NUMBER"].ToString(),
                        PolicyholderName = row["policyholder_name"].ToString(),
                        PolicyholderGender = Convert.ToInt32(row["policyholder_gender"].ToString()),
                        PolicyholderDOB = Convert.ToDateTime(row["policyholder_dob"].ToString()),
                        PolicyholderIDType = Convert.ToInt32(row["policyholder_id_type"].ToString()),
                        PolicyholderIDNo = row["policyholder_Id_No"].ToString(),
                        PolicyholderPhoneNumber = row["policyholder_phone_number"].ToString(),
                        PolicyholderPhoneNumber2 = row["policyholder_phone_number2"].ToString(),
                        PolicyholderEmail = row["policyholder_email"].ToString(),
                        PolicyholderAddress = row["policyholder_address"].ToString()
                    };
                }
                _SUCCESS = true;
                _MESSAGE = "Success";
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            _CODE = "0";
            app = new bl_micro_application();
            Log.AddExceptionToLog("Error function [GetApplication(string APPLICATION_NUMBER)] in class [da_micro_application], detail:" + ex.Message + "==>" + ex.StackTrace);
        }
        return app;
    }
    public static bl_micro_application GetApplication(string APPLICATION_NUMBER, string CHANNEL_ITEM_ID)
    {

        bl_micro_application app = new bl_micro_application();
        try
        {
            app = GetApplication(APPLICATION_NUMBER);

            if (app.APPLICATION_NUMBER != null)//has data
            {
                if (app.CHANNEL_ITEM_ID != CHANNEL_ITEM_ID)//filter channel item id
                {
                    _SUCCESS = true;
                    _MESSAGE = "Success";
                    app = new bl_micro_application();
                }

            }
            else// no data found
            {
                app = new bl_micro_application();
                _SUCCESS = true;
                _MESSAGE = "Success";
            }


        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            _CODE = "0";
            app = new bl_micro_application();
            Log.AddExceptionToLog("Error function [GetApplication(string APPLICATION_NUMBER, string CHANNEL_ITEM_ID)] in class [da_micro_application], detail:" + ex.Message + "==>" + ex.StackTrace);
        }
        return app;
    }
    public static bl_micro_application GetApplicationByApplicationID(string APPLICATION_ID)
    {

        bl_micro_application app = new bl_micro_application();
        try
        {
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_GET_BY_APPLICATION_ID", new string[,] {
            {"@APPLICATION_ID", APPLICATION_ID}
            }, "da_micro_application=>GetApplicationByApplicationID(string APPLICATION_ID)");
            if (db.RowEffect == -1)
            {
                _SUCCESS = false;
                _MESSAGE = db.Message;
            }
            else
            {
                if (tbl.Rows.Count > 0)
                {
                    var row = tbl.Rows[0];
                    app = new bl_micro_application()
                    {

                        APPLICATION_ID = row["application_id"].ToString(),
                        APPLICATION_NUMBER = row["application_number"].ToString(),
                        APPLICATION_DATE = Convert.ToDateTime(row["application_date"].ToString()),
                        SEQ = Convert.ToInt32(row["seq"].ToString()),
                        APPLICATION_CUSTOMER_ID = row["application_customer_id"].ToString(),
                        CHANNEL_ITEM_ID = row["channel_item_id"].ToString(),
                        CHANNEL_LOCATION_ID = row["channel_location_id"].ToString(),
                        SALE_AGENT_ID = row["sale_agent_id"].ToString(),
                        CREATED_BY = row["created_by"].ToString(),
                        CREATED_ON = Convert.ToDateTime(row["created_on"].ToString()),
                        UPDATED_BY = row["updated_by"].ToString(),
                        UPDATED_ON = Convert.ToDateTime(row["updated_on"].ToString()),
                        REMARKS = row["remarks"].ToString(),
                        CLIENT_TYPE = row["CLIENT_TYPE"].ToString(),
                        CLIENT_TYPE_RELATION = row["CLIENT_TYPE_RELATION"].ToString(),
                        CLIENT_TYPE_REMARKS = row["CLIENT_TYPE_REMARKS"].ToString()
                    };
                }
                _SUCCESS = true;
                _MESSAGE = "Success";
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            app = new bl_micro_application();
            Log.AddExceptionToLog("Error function [GetApplicationByApplicationID(string APPLICATION_ID)] in class [da_micro_application], detail:" + ex.Message + "==>" + ex.StackTrace);
        }
        return app;
    }
    /// <summary>
    /// Get Application (Main+Sub)
    /// </summary>
    /// <param name="APPLICATION_ID">Main Application Id</param>
    /// <returns></returns>
    public static List<bl_micro_application> GetApplicationBatchByApplicationID(string APPLICATION_ID)
    {
        List<bl_micro_application> appList = new List<bl_micro_application>();
        try
        {
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_BATCH_GET_BY_APPLICATION_ID", new string[,] {
            {"@APPLICATION_ID", APPLICATION_ID}
            }, "da_micro_application=>GetApplicationBatchByApplicationID(string APPLICATION_ID)");
            if (db.RowEffect == -1)
            {
                _SUCCESS = false;
                _MESSAGE = db.Message;
            }
            else
            {
                foreach (DataRow row in tbl.Rows)
                {
                    appList.Add(new bl_micro_application()
                    {
                        APPLICATION_ID = row["application_id"].ToString(),
                        APPLICATION_NUMBER = row["application_number"].ToString(),
                        APPLICATION_DATE = Convert.ToDateTime(row["application_date"].ToString()),
                        SEQ = Convert.ToInt32(row["seq"].ToString()),
                        APPLICATION_CUSTOMER_ID = row["application_customer_id"].ToString(),
                        CHANNEL_ITEM_ID = row["channel_item_id"].ToString(),
                        CHANNEL_LOCATION_ID = row["channel_location_id"].ToString(),
                        SALE_AGENT_ID = row["sale_agent_id"].ToString(),
                        CREATED_BY = row["created_by"].ToString(),
                        CREATED_ON = Convert.ToDateTime(row["created_on"].ToString()),
                        UPDATED_BY = row["updated_by"].ToString(),
                        UPDATED_ON = Convert.ToDateTime(row["updated_on"].ToString()),
                        REMARKS = row["remarks"].ToString(),
                        CLIENT_TYPE = row["CLIENT_TYPE"].ToString(),
                        CLIENT_TYPE_RELATION = row["CLIENT_TYPE_RELATION"].ToString(),
                        CLIENT_TYPE_REMARKS = row["CLIENT_TYPE_REMARKS"].ToString(),
                        NumbersOfApplicationFirstYear = Convert.ToInt32(row["NUMBERS_OF_APPLICATION_FIRST_YEAR"].ToString()),
                        NumbersOfPurchasingYear = Convert.ToInt32(row["NUMBERS_OF_PURCHASING_YEAR"].ToString())
                    });
                }

                _SUCCESS = true;
                _MESSAGE = "Success";
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            appList = new List<bl_micro_application>();
            Log.AddExceptionToLog("Error function [GetApplicationBatchByApplicationID(string APPLICATION_ID)] in class [da_micro_application], detail:" + ex.Message + "==>" + ex.StackTrace);
        }
        return appList;
    }

    public static DataTable GetApplicationDetailByApplicationID(string APPLICATION_ID)
    {
        DataTable tbl = new DataTable();
        try
        {
            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_GET_APPLICATION_DETAIL", new string[,] {
            {"@APPLICATION_ID", APPLICATION_ID}
            }, "da_micro_application=>GetApplicationDetailByApplicationID(string APPLICATION_ID)");
            if (db.RowEffect == -1)
            {
                _SUCCESS = false;
                _MESSAGE = db.Message;
                _CODE = db.Code;
            }
            else
            {
                _CODE = "1000";//success code
                _SUCCESS = true;
                _MESSAGE = "Success";
            }
        }
        catch (Exception ex)
        {
            _CODE = "0";//unexpected errro.
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            tbl = new DataTable();
            Log.AddExceptionToLog("Error function [GetApplicationDetailByApplicationID(string APPLICATION_ID)] in class [da_micro_application], detail:" + ex.Message + "==>" + ex.StackTrace);
        }
        return tbl;
    }

    public static DataTable GetApplicationConsumer(string applicationNumber)
    {

        DataTable tbl = new DataTable();
        try
        {
            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATON_GET_CONSUMER_BY_APP_NUMBER", new string[,] {
            {"@APPLICATION_NUMBER", applicationNumber}
            }, "da_micro_application=>GetApplicationConsumer(string applicationNumber)");
            if (db.RowEffect == -1)
            {
                _SUCCESS = false;
                _MESSAGE = db.Message;
                _CODE = db.Code;
            }
            else
            {
                _CODE = "1000";//success code
                _SUCCESS = true;
                _MESSAGE = "Success";
            }
        }
        catch (Exception ex)
        {
            _CODE = "0";//unexpected errro.
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            tbl = new DataTable();
            Log.AddExceptionToLog("Error function [GetApplicationConsumer(string applicationNumber)] in class [da_micro_application], detail:" + ex.Message + "==>" + ex.StackTrace);
        }
        return tbl;
    }

    public static bl_application_for_issue GetApplicationForIssuePolicy(string applicationNumber)
    {
        bl_application_for_issue obj = new bl_application_for_issue();
        try
        {
            DB db = new DB();
            DataSet ds = db.GetDataSet(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_FOR_ISSUE_POLICY", new string[,] { { "@APPLICATION_NUMBER", applicationNumber } });
            var cus = obj.Customer;
            var app = obj.Application;
            var appInurance = obj.Insurance;
            var appRider = obj.Rider;
            var appBen = new List<bl_micro_application_beneficiary>();
            var appQuestion = obj.Questionaire;
            var primaryBen = obj.PrimaryBeneciary;
            if (ds.Tables.Count > 0)
            {
                DataTable tblApp = ds.Tables[0];
                DataTable tblBen = ds.Tables[1];
                DataTable tblPrimaryBen = ds.Tables[2];
                var r = tblApp.Rows[0];

                cus = new bl_micro_application_customer()
                {
                    CUSTOMER_ID = r["application_customer_id"].ToString(),
                    ID_TYPE = Convert.ToInt32(r["id_type"].ToString()),
                    ID_NUMBER = r["id_number"].ToString(),
                    FIRST_NAME_IN_ENGLISH = r["first_name_in_english"].ToString(),
                    LAST_NAME_IN_ENGLISH = r["last_name_in_english"].ToString(),
                    FIRST_NAME_IN_KHMER = r["first_name_in_khmer"].ToString(),
                    LAST_NAME_IN_KHMER = r["last_name_in_khmer"].ToString(),
                    GENDER = Convert.ToInt32(r["gender"].ToString()),
                    DATE_OF_BIRTH = Convert.ToDateTime(r["date_of_birth"].ToString()),
                    NATIONALITY = r["nationality"].ToString(),
                    MARITAL_STATUS = r["marital_status"].ToString(),
                    OCCUPATION = r["occupation"].ToString(),
                    PHONE_NUMBER1 = r["phone_number"].ToString(),
                    EMAIL1 = r["email"].ToString(),
                    VILLAGE_EN = r["village_en"].ToString(),
                    COMMUNE_EN = r["commune_en"].ToString(),
                    DISTRICT_EN = r["district_en"].ToString(),
                    PROVINCE_EN = r["province_en"].ToString(),
                    HOUSE_NO_EN = r["house_no_en"].ToString(),
                    STREET_NO_EN = r["street_no_en"].ToString()

                };

                app = new bl_micro_application()
                {
                    APPLICATION_NUMBER = r["application_number"].ToString(),
                    APPLICATION_CUSTOMER_ID = r["application_customer_id"].ToString(),
                    CHANNEL_ID = r["channel_id"].ToString(),
                    CHANNEL_ITEM_ID = r["channel_item_id"].ToString(),
                    CHANNEL_LOCATION_ID = r["channel_location_id"].ToString(),
                    APPLICATION_DATE = Convert.ToDateTime(r["application_date"].ToString()),
                    APPLICATION_ID = r["Application_id"].ToString(),
                    SALE_AGENT_ID = r["sale_agent_id"].ToString(),
                    REFERRER = r["referrer"].ToString(),
                    REFERRER_ID = r["referrer_id"].ToString(),
                    CLIENT_TYPE = r["client_type"].ToString(),
                    CLIENT_TYPE_REMARKS = r["client_type_remarks"].ToString(),
                    CLIENT_TYPE_RELATION = r["client_type_relation"].ToString(),
                    RENEW_FROM_POLICY = r["renew_from_policy"].ToString()

                };

                appInurance = new bl_micro_application_insurance()
                {
                    APPLICATION_NUMBER = app.APPLICATION_NUMBER,
                    PRODUCT_ID = r["product_id"].ToString(),
                    PACKAGE = r["package"].ToString(),
                    PAYMENT_PERIOD = Convert.ToInt32(r["payment_period"].ToString()),
                    TERME_OF_COVER = Convert.ToInt32(r["term_of_cover"].ToString()),
                    SUM_ASSURE = Convert.ToDouble(r["sum_assure"].ToString()),
                    PAY_MODE = Convert.ToInt32(r["pay_mode"].ToString()),
                    PREMIUM = Convert.ToDouble(r["premium"].ToString()),
                    ANNUAL_PREMIUM = Convert.ToDouble(r["annual_premium"].ToString()),
                    DISCOUNT_AMOUNT = Convert.ToDouble(r["discount_amount"].ToString()),
                    TOTAL_AMOUNT = Convert.ToDouble(r["total_amount"].ToString()),
                    USER_PREMIUM = Convert.ToDouble(r["user_premium"].ToString()),
                    PAYMENT_CODE = r["payment_code"].ToString(),
                    COVER_TYPE = r["term_of_cover_type"].ToString()
                };

                appRider = new bl_micro_application_insurance_rider()
                {
                    APPLICATION_NUMBER = app.APPLICATION_NUMBER,
                    PRODUCT_ID = r["rider_product_id"].ToString(),
                    SUM_ASSURE = Convert.ToDouble(r["rider_sum_assure"].ToString()),
                    PREMIUM = Convert.ToDouble(r["rider_premium"].ToString()),
                    ANNUAL_PREMIUM = Convert.ToDouble(r["rider_annual_premium"].ToString()),
                    DISCOUNT_AMOUNT = Convert.ToDouble(r["rider_discount_amount"].ToString()),
                    TOTAL_AMOUNT = Convert.ToDouble(r["rider_total_amount"].ToString())
                };

                foreach (DataRow dr in tblBen.Rows)
                {
                    var ben = new bl_micro_application_beneficiary();
                    ben.APPLICATION_NUMBER = dr["application_number"].ToString();
                    ben.ID = dr["id"].ToString();
                    ben.FULL_NAME = dr["full_name"].ToString();
                    ben.AGE = dr["age"].ToString();
                    ben.RELATION = dr["relation"].ToString();
                    ben.PERCENTAGE_OF_SHARE = Convert.ToDouble(dr["percentage_of_share"].ToString());
                    ben.ADDRESS = dr["address"].ToString();
                    ben.DOB = Convert.ToDateTime(dr["dob"].ToString());
                    ben.IdType = Convert.ToInt32(dr["id_type"].ToString());
                    ben.IdNo = dr["id_no"].ToString();
                    ben.Gender = Convert.ToInt32(dr["gender"].ToString());
                    appBen.Add(ben);
                    
                }

                appQuestion = new bl_micro_application_questionaire()
                {
                    APPLICATION_NUMBER = app.APPLICATION_NUMBER,
                    QUESTION_ID = r["question_id"].ToString(),
                    ANSWER = Convert.ToInt32(r["answer"].ToString()),
                    ANSWER_REMARKS = r["answer_remarks"].ToString()
                };

                if (tblPrimaryBen.Rows.Count > 0)
                {
                    DataRow row3 = tblPrimaryBen.Rows[0];
                    primaryBen = new bl_micro_application_beneficiary.PrimaryBeneciary()
                    {
                        Id = row3["ID"].ToString(),
                        ApplicationNumber = row3["application_number"].ToString(),
                        FullName = row3["full_name"].ToString(),
                        LoanNumber = row3["loan_number"].ToString(),
                        Address = row3["address"].ToString()
                    };
                }

                obj.PolicyNumber = r["policy_number"].ToString();
                obj.Customer = cus;
                obj.Application = app;
                obj.Insurance = appInurance;
                obj.Rider = appRider;
                obj.Beneficiaries = appBen;
                obj.Questionaire = appQuestion;
                obj.PrimaryBeneciary = primaryBen;
            }
        }
        catch (Exception ex)
        {
            obj = null;
            _CODE = "0";
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("da_micro_application", "GetApplicationForIssuePolicy(string applicationNumber)", ex);
        }
        return obj;

    }

    public static bl_application_detail_response GetApplicationDetail(string applicationNumber)
    {
        bl_application_detail_response obj = new bl_application_detail_response();
        try
        {
            DB db = new DB();
            DataSet ds = db.GetDataSet(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_DETAIL_BY_APPLICATION_NUMBER", new string[,] { { "@APPLICATION_NUMBER", applicationNumber } });
            var cus = obj.Customer;
            var app = obj.Application;
            var appInurance = obj.Insurance;
            var appRider = obj.Rider;
            var appBen = new List<BeneficiaryResponse>();
            var appQuestion = obj.Questionaire;
            var primaryBen = obj.PrimaryBeneficiary;
            if (ds.Tables.Count > 0)
            {
                DataTable tblApp = ds.Tables[0];
                DataTable tblBen = ds.Tables[1];
                DataTable tblSubApplication = ds.Tables[2];
                DataTable tblPremaryBen = ds.Tables[3];
                var r = tblApp.Rows[0];

                cus = new CustomerResponse()
                {
                    CustomerId = r["APPLICATION_CUSTOMER_ID"].ToString(),
                    IdType = Convert.ToInt32(r["id_type"].ToString()),
                    IdTypeEn = Helper.GetIDCardTypeText(Convert.ToInt32(r["id_type"].ToString())),
                    IdTypeKh = Helper.GetIDCardTypeTextKh(Convert.ToInt32(r["id_type"].ToString())),
                    IdNumber = r["id_number"].ToString(),
                    FirstNameEn = r["first_name_in_english"].ToString(),
                    LastNameEn = r["last_name_in_english"].ToString(),
                    FirstNameKh = r["first_name_in_khmer"].ToString(),
                    LastNameKh = r["last_name_in_khmer"].ToString(),
                    FullNameEn = r["last_name_in_english"].ToString() + " " + r["first_name_in_english"].ToString(),
                    FullNameKh = r["last_name_in_khmer"].ToString() + " " + r["first_name_in_khmer"].ToString(),
                    Gender = Convert.ToInt32(r["gender"].ToString()),
                    GenderEn = Helper.GetGenderText(Convert.ToInt32(r["gender"].ToString()), false),
                    GenderKh = Helper.GetGenderText(Convert.ToInt32(r["gender"].ToString()), false, true),
                    Dob = Convert.ToDateTime(r["date_of_birth"].ToString()),
                    Nationality = r["nationality"].ToString(),
                    MaritalStatus = r["marital_status"].ToString(),
                    Occupation = r["occupation"].ToString(),
                    PhoneNumber = r["phone_number"].ToString(),
                    Email = r["email"].ToString(),

                    Address = new AddressResponse()
                    {
                        HouseNo = r["house_no_en"].ToString(),
                        StreetNo = r["street_no_en"].ToString(),
                        Village = r["village_code"].ToString(),
                        VillageEn = r["village_en"].ToString(),
                        VillageKh = r["village_kh"].ToString(),
                        Commune = r["commune_code"].ToString(),
                        CommuneEn = r["commune_en"].ToString(),
                        CommuneKh = r["commune_kh"].ToString(),
                        District = r["district_code"].ToString(),
                        DistrictEn = r["district_en"].ToString(),
                        DistrictKh = r["district_kh"].ToString(),
                        Province = r["province_code"].ToString(),
                        ProvinceEn = r["province_en"].ToString(),
                        ProvinceKh = r["province_kh"].ToString()
                    }

                };

                app = new ApplicationResponse()
                {
                    ApplicationId = r["application_id"].ToString(),
                    ApplicationNumber = r["application_number"].ToString(),
                    ApplicationDate = Convert.ToDateTime(r["application_date"].ToString()),
                    Referrer = r["referrer"].ToString(),
                    ReferrerId = r["referrer_id"].ToString(),
                    ClientType = r["client_type"].ToString(),
                    ClientTypeRemarks = r["client_type_remarks"].ToString(),
                    ClientTypeRelation = r["client_type_relation"].ToString(),
                    RenewalFromPolicy = r["renew_from_policy"].ToString(),
                    CreatedBy = r["created_by"].ToString(),
                    CreatedOn = Convert.ToDateTime(r["created_on"].ToString()),
                    MainApplicationNumber = r["Main_application_number"].ToString(),
                    NumbersOfPurchasingYear = Convert.ToInt32(r["NUMBERS_OF_PURCHASING_YEAR"].ToString()),
                    NumbersOfApplicationFirstYear = Convert.ToInt32(r["NUMBERS_OF_APPLICATION_FIRST_YEAR"].ToString()),
                    LoanNumber= r["Loan_Number"].ToString(),
                    PolicyholderName = r["policyholder_name"].ToString(),
                    PolicyholderGender = Convert.ToInt32(r["policyholder_gender"].ToString()),
                    PolicyholderDOB = Convert.ToDateTime(r["policyholder_dob"].ToString()),
                    PolicyholderIDType = Convert.ToInt32(r["policyholder_id_type"].ToString()),
                    PolicyholderIDNo = r["policyholder_Id_No"].ToString(),
                    PolicyholderPhoneNumber = r["policyholder_phone_number"].ToString(),
                    PolicyholderPhoneNumber2 = r["policyholder_phone_number2"].ToString(),
                    PolicyholderEmail = r["policyholder_email"].ToString(),
                    PolicyholderAddress = r["policyholder_address"].ToString()

                };

                var product = da_product.GetProductByProductID(r["product_id"].ToString());

                appInurance = new InsuranceResponse()
                {
                    ProductId = r["product_id"].ToString(),
                    Package = r["package"].ToString(),
                    PaymentPeriod = Convert.ToInt32(r["payment_period"].ToString()),
                    CoverYear = Convert.ToInt32(r["term_of_cover"].ToString()),
                    SumAssure = Convert.ToDouble(r["sum_assure"].ToString()),
                    PaymentMode = Convert.ToInt32(r["pay_mode"].ToString()),
                    Premium = Convert.ToDouble(r["premium"].ToString()),
                    AnnualPremium = Convert.ToDouble(r["annual_premium"].ToString()),
                    DiscountAmount = Convert.ToDouble(r["discount_amount"].ToString()),
                    TotalAmount = Convert.ToDouble(r["total_amount"].ToString()),
                    UserPremium = Convert.ToDouble(r["user_premium"].ToString()),
                    PaymentCode = r["payment_code"].ToString(),
                    PaymentModeEn = Helper.GetPaymentModeEnglish(Convert.ToInt32(r["pay_mode"].ToString())),
                    PaymentModeKh = Helper.GetPaymentModeInKhmer(Convert.ToInt32(r["pay_mode"].ToString())),
                    ProductName = product.En_Title,
                    ProductNameKh = product.Kh_Title
                };


                if (r["rider_product_id"].ToString() != "")
                {
                    var rider = da_micro_product_rider.GetMicroProductByProductID(r["rider_product_id"].ToString());
                    appRider = new RiderResponse()
                    {
                        ProductId = r["rider_product_id"].ToString(),
                        ProductName = rider.EN_TITLE,
                        ProductNameKh = rider.KH_TITLE,
                        SumAssure = Convert.ToDouble(r["rider_sum_assure"].ToString()),
                        Premium = Convert.ToDouble(r["rider_premium"].ToString()),
                        AnnualPremium = Convert.ToDouble(r["rider_annual_premium"].ToString()),
                        DiscountAmount = Convert.ToDouble(r["rider_discount_amount"].ToString()),
                        TotalAmount = Convert.ToDouble(r["rider_total_amount"].ToString())
                    };
                }
                else
                {
                    appRider = null;
                }

                foreach (DataRow dr in tblBen.Rows)
                {
                    var ben = new BeneficiaryResponse();
                    ben.Id = dr["id"].ToString();
                    ben.FullName = dr["full_name"].ToString();
                    ben.Age = dr["age"].ToString();
                    ben.Relation = dr["relation"].ToString();
                    ben.PercentageOfShare = Convert.ToDouble(dr["percentage_of_share"].ToString());
                    ben.Address = dr["address"].ToString();
                    ben.IdType = Convert.ToInt32(dr["id_type"].ToString() );
                    ben.IdNo = dr["id_no"].ToString();
                    ben.Gender = Convert.ToInt32(dr["gender"].ToString());
                    ben.DOB = Convert.ToDateTime(dr["dob"].ToString());
                    appBen.Add(ben);
                }
                if (tblPremaryBen.Rows.Count > 0)
                {
                    DataRow row3 = tblPremaryBen.Rows[0];
                    primaryBen = new PrimaryBeneficiaryResponse()
                    {
                        Id = row3["id"].ToString(),
                        FullName = row3["full_name"].ToString(),
                        LoanNumber = row3["Loan_number"].ToString(),
                        Address = row3["Address"].ToString()
                    };
                }
                appQuestion = new QuestionaireResponse()
                {

                    QuestionId = r["question_id"].ToString(),
                    Answer = Convert.ToInt32(r["answer"].ToString()),
                    AnswerRemarks = r["answer_remarks"].ToString()
                };

                var channel = da_channel.GetChannelDetail(r["channel_location_id"].ToString());

                obj.Channel = new ChannelResponse()
                {
                    ChannelId = channel.ChannelId,
                    ChannelType = channel.ChannelType,
                    ChannelItemId = channel.ChannelItemId,
                    ChannelItemName = channel.ChannelItemName,
                    ChannelItemNameKh = channel.ChannelItemNameKh,
                    ChannelLocationId = channel.ChannelLocationId,
                    OfficeCode = channel.OfficeCode,
                    OfficeName = channel.OfficeName
                };

                obj.Agent = new AgentResponse()
                {
                    AgentCode = r["Sale_Agent_ID"].ToString(),
                    AgentNameEn = r["full_name"].ToString(),
                    AgentNameKh = r["full_name_kh"].ToString()
                };

                obj.PolicyId = r["policy_id"].ToString();
                obj.PolicyNumber = r["policy_number"].ToString();
                obj.PolicyStatus = r["policy_status"].ToString();
                obj.Age = Calculation.Culculate_Customer_Age(cus.Dob.ToString("dd/MM/yyyy"), app.ApplicationDate);
                obj.IssueDate = Convert.ToDateTime(r["issued_date"].ToString());
                obj.CollectedPremium = Convert.ToDouble(r["collected_premium"].ToString());
                obj.PaymentReferenceNo = r["transaction_referrence_no"].ToString();
                obj.Customer = cus;
                obj.Application = app;
                obj.Insurance = appInurance;
                obj.Rider = appRider;
                obj.Beneficiaries = appBen;
                obj.Questionaire = appQuestion;
                obj.PrimaryBeneficiary = primaryBen;

                /*sub application*/
                List<SubApplication> subApplications = new List<SubApplication>();
                foreach (DataRow rr in tblSubApplication.Rows)
                {
                    var objSubApp = new SubApplication();
                    objSubApp.ApplicationId = rr["application_id"].ToString().Trim();
                    objSubApp.ApplicationNumber = rr["application_number"].ToString().Trim();
                    objSubApp.BasicAmount = Convert.ToDouble(rr["Total_basic_amount"].ToString());
                    objSubApp.RiderAmount = Convert.ToDouble(rr["total_rider_amount"].ToString());
                    objSubApp.TotalAmount = Convert.ToDouble(rr["total_amount"].ToString());
                    objSubApp.ClientType = rr["client_type"].ToString();
                    objSubApp.SumAssure = Convert.ToDouble(rr["sum_assure"].ToString());
                    subApplications.Add(objSubApp);
                }
                obj.SubApplications = subApplications;
            }
        }
        catch (Exception ex)
        {
            obj = null;
            _CODE = "0";
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("da_micro_application", "GetApplicationDetail(string applicationNumber)", ex);
        }
        return obj;

    }

    /// <summary>
    /// BACKUP A COPY OF DATA WHILE USER DOES UPDATE, DELETE TRANSACTION
    /// </summary>
    /// <param name="applicationID"></param>
    /// <param name="tranType">[UPDATE, DELETE]</param>
    /// <param name="tranBy">[USER NAME WHO DOES TRANSACTION]</param>
    /// <param name="tranDate">[DATE OF TRANSACTION]</param>
    /// <returns></returns>
    public static bool BackupApplication(string applicationID, string tranType, string tranBy, DateTime tranDate)
    {
        bool result = false;

        try
        {
            db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_MICRO_APPLICATION_BACKUP", new string[,] {
            {"@APPLICATION_ID", applicationID},
            {"@TRAN_TYPE",tranType},
            {"@TRAN_DATE", tranDate+""},
            {"@TRAN_BY",tranBy}

            }, "da_micro_application => BackupApplication(string applicationID, string tranType, string tranBy, DateTime tranDate)");

            if (db.RowEffect == -1)
            {
                _MESSAGE = db.Message;
                _SUCCESS = false;
            }
            else
            {
                _MESSAGE = "Sucess";
                _SUCCESS = true;
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("da_micro_application", "BackupApplication(string applicationID, string tranType, string tranBy, DateTime tranDate)", ex);
        }

        return result;
    }
    /// <summary>
    /// RESTORE DATA FROM BACKUP
    /// </summary>
    /// <param name="applicationNumber"></param>
    /// <param name="tranBy">[USER WHO DID PREVIOUS TRANSACTION UPDATE, DELETE]</param>
    /// <param name="tranDate">[DATE OF PREVIOUS TRANSACTION]</param>
    /// <returns></returns>
    public static bool RestoreApplication(string applicationNumber, string tranBy, DateTime tranDate)
    {
        bool result = false;

        try
        {
            db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_MICRO_APPLICATION_RESTORE", new string[,] {
            {"@APPLICATION_NUMBER", applicationNumber},
            {"@TRAN_DATE", tranDate+""},
            {"@TRAN_BY",tranBy}

            }, "da_micro_application => RestoreApplication(string applicationNumber, string tranBy, DateTime tranDate)");

            if (db.RowEffect == -1)
            {
                _MESSAGE = db.Message;
                _SUCCESS = false;
            }
            else
            {
                _MESSAGE = "Sucess";
                _SUCCESS = true;
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("da_micro_application", "RestoreApplication(string applicationNumber, string tranBy, DateTime tranDate)", ex);
        }

        return result;
    }

    /// <summary>
    /// DELETE BACKUP DATA
    /// </summary>
    /// <param name="applicationNumber"></param>
    /// <param name="tranBy">USER NAME WHO DID BACKUP</param>
    /// <param name="tranDate">DATE OF DID BACKUP</param>
    /// <returns></returns>
    public static bool DeleteBackupApplication(string applicationNumber, string tranBy, DateTime tranDate)
    {
        bool result = false;

        try
        {
            db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_MICRO_APPLICATION_BACKUP_DELETE", new string[,] {
            {"@APPLICATION_NUMBER", applicationNumber},
            {"@TRAN_DATE", tranDate+""},
            {"@TRAN_BY",tranBy}

            }, "da_micro_application => DeleteBackupApplication(string applicationNumber, string tranBy, DateTime tranDate)");

            if (db.RowEffect == -1)
            {
                _MESSAGE = db.Message;
                _SUCCESS = false;
            }
            else
            {
                _MESSAGE = "Sucess";
                _SUCCESS = true;
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("da_micro_application", "DeleteBackupApplication(string applicationNumber, string tranBy, DateTime tranDate)", ex);
        }

        return result;
    }

    public static List<Inquiries.ApplicationInquiry> GetApplicationInquiry(string channelLocationId, string applicationNumber, string fullNameEn, string IdNumber, string policyNumber, DateTime appDateF, DateTime appDateT)
    {
        List<Inquiries.ApplicationInquiry> list = new List<Inquiries.ApplicationInquiry>();

        try
        {
            db = new DB();
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_INQUIRY_MICRO_APP_CUST_GET", new string[,] {
            {"@CHANNEL_LOCATION_ID", channelLocationId},
            {"@APPLICATION_NUMBER", applicationNumber},
            {"@FULL_NAME_EN",fullNameEn},
            {"@ID_NUMBER",IdNumber},
            {"@POLICY_NUMBER",policyNumber},
            {"@app_date_f", appDateF.ToString("yyyy-MM-dd") },
            {"@app_date_t", appDateT.ToString("yyyy-MM-dd")}
            }, "da_micro_application =>GetApplicationInquiry(string channelLocationId, string applicationNumber, string fullNameEn, string IdNumber, string policyNumber, DateTime appDateF, DateTime appDateT)");

            if (db.RowEffect == -1)
            {
                _MESSAGE = db.Message;
                _SUCCESS = false;
            }
            else
            {
                _MESSAGE = "Sucess";
                _SUCCESS = true;
                foreach (DataRow r in tbl.Rows)
                {
                    list.Add(new Inquiries.ApplicationInquiry()
                    {
                        ApplicationNumber = r["application_number"].ToString(),
                        ApplicationDate = Convert.ToDateTime(r["application_date"].ToString()),
                        ChannelId = r["channel_id"].ToString(),
                        ChannelItemId = r["channel_item_id"].ToString(),
                        ChannelItemName = r["channel_name"].ToString(),
                        ChannelItemNameKh = r["channel_name_kh"].ToString(),
                        ChannelLocationId = r["channel_location_id"].ToString(),
                        BranchCode = r["branch_code"].ToString(),
                        BranchName = r["branch_name"].ToString(),
                        FullNameEn = r["full_name_en"].ToString(),
                        FullNameKh = r["full_name_kh"].ToString(),
                        Dob = Convert.ToDateTime(r["date_of_birth"].ToString()),
                        Gender = r["gender"].ToString(),
                        IdType = Convert.ToInt32(r["id_type"].ToString()),
                        IdNumber = r["id_number"].ToString(),
                        PhoneNumber = r["phone_number"].ToString(),
                        PolicyNumber = r["policy_number"].ToString(),
                        PolicyStatus = r["policy_status"].ToString(),
                        CreatedBy = r["created_by"].ToString(),
                        ApplicationId = r["Application_id"].ToString(),
                        PolicyId = r["Policy_id"].ToString(),
                        ApplicationType = r["application_type"].ToString()
                    });
                }
            }
        }
        catch (Exception ex)
        {
            list = null;
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("da_micro_application", "GetApplicationInquiry(string channelLocationId, string applicationNumber, string fullNameEn, string IdNumber, string policyNumber)", ex);

        }

        return list;
    }
    public static List<Inquiries.ApplicationFilter> GetApplicationFilter(string applicationNumber, bool onlyFirstYear = false)
    {
        List<Inquiries.ApplicationFilter> list = new List<Inquiries.ApplicationFilter>();

        try
        {
            db = new DB();
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_GET_FILTER_APPLICATION", new string[,] {

            {"@APP_NUMBER", applicationNumber},
            {"@FIRST_Y",onlyFirstYear ==false ? "0": "1"}

            }, "da_micro_application =>GetApplicationFilter(string applicationNumber, bool onlyFirstYear =false)");

            if (db.RowEffect == -1)
            {
                _MESSAGE = db.Message;
                _SUCCESS = false;
            }
            else
            {
                _MESSAGE = "Sucess";
                _SUCCESS = true;
                foreach (DataRow r in tbl.Rows)
                {
                    list.Add(new Inquiries.ApplicationFilter()
                    {
                        ApplicationId = r["application_id"].ToString(),
                        ApplicationNumber = r["application_number"].ToString(),
                        MainApplicationNumber = r["main_application_number"].ToString(),
                        ApplicationType = r["application_type"].ToString()
                    });
                }
            }
        }
        catch (Exception ex)
        {
            list = null;
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("da_micro_application", "GetApplicationFilter(string applicationNumber, bool onlyFirstYear =false)", ex);

        }

        return list;
    }
    /// <summary>
    /// Return all application number included sub application
    /// </summary>
    /// <param name="applicationNumber"></param>
    /// <returns></returns>
    public static List<Inquiries.ApplicationFilter> GetApplicationNumberMainSub(string applicationNumber)
    {
        List<Inquiries.ApplicationFilter> list = new List<Inquiries.ApplicationFilter>();

        try
        {
            db = new DB();
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_GET_APPLICATION_NO_MAIN_SUB", new string[,] {

            {"@APPLICATION_NUMBER", applicationNumber}

            }, "da_micro_application =>GetApplicationNumberMainSub(string applicationNumber)");

            if (db.RowEffect == -1)
            {
                _MESSAGE = db.Message;
                _SUCCESS = false;
            }
            else
            {
                _MESSAGE = "Sucess";
                _SUCCESS = true;
                foreach (DataRow r in tbl.Rows)
                {
                    list.Add(new Inquiries.ApplicationFilter()
                    {
                        ApplicationId = r["application_id"].ToString(),
                        ApplicationNumber = r["application_number"].ToString(),
                        MainApplicationNumber = r["main_application_number"].ToString(),
                        ApplicationType = r["application_type"].ToString(),
                        NumbersApplication = Convert.ToInt32( r["NUMBERS_OF_APPLICATION_FIRST_YEAR"].ToString() ),
                        NumbersPurchasingYear =Convert.ToInt32(r["NUMBERS_OF_PURCHASING_YEAR"].ToString())
                    });
                }
            }
        }
        catch (Exception ex)
        {
            list = null;
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("da_micro_application", "GetApplicationNumberMainSub(string applicationNumber)", ex);

        }

        return list;
    }

}