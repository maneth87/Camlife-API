using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for da_micro_policy_detail
/// </summary>
public class da_micro_policy_detail
{
    private static bool _SUCCESS = false;
    private static string _MESSAGE = "";

    public static bool SUCCESS { get { return _SUCCESS; } }
    public static string MESSAGE { get { return _MESSAGE; } }
    private static DB db = new DB();
    public da_micro_policy_detail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static bool SavePolicyDetail(bl_micro_policy_detail POLICY_DETAIL)
    {
        bool result = false;
        try
        {
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_DETAIL_INSERT", new string[,] {
            {"@POLICY_DETAIL_ID", POLICY_DETAIL.POLICY_DETAIL_ID}, 
            {"@POLICY_ID",POLICY_DETAIL.POLICY_ID}, 
            {"@EFFECTIVE_DATE",POLICY_DETAIL.EFFECTIVE_DATE+""}, 
            {"@MATURITY_DATE",POLICY_DETAIL.MATURITY_DATE+""},
            {"@EXPIRY_DATE",POLICY_DETAIL.EXPIRY_DATE+""},
            {"@ISSUED_DATE", POLICY_DETAIL.ISSUED_DATE+""}, 
            {"@AGE",POLICY_DETAIL.AGE+""},
            {"@CURRENCY",POLICY_DETAIL.CURRANCY},
            {"@SUM_ASSURE", POLICY_DETAIL.SUM_ASSURE+""},
            {"@PAY_MODE", POLICY_DETAIL.PAY_MODE+""},
            {"@PAYMENT_CODE",POLICY_DETAIL.PAYMENT_CODE},
            {"@PREMIUM", POLICY_DETAIL.PREMIUM+""}, 
            {"@ANNUAL_PREMIUM",POLICY_DETAIL.ANNUAL_PREMIUM+""},
            {"@DISCOUNT_AMOUNT", POLICY_DETAIL.DISCOUNT_AMOUNT+""},
            {"@TOTAL_AMOUNT",POLICY_DETAIL.TOTAL_AMOUNT+""},
            {"@REFERRAL_FEE", POLICY_DETAIL.REFERRAL_FEE+""},
            {"@REFERRAL_INCENTIVE", POLICY_DETAIL.REFERRAL_INCENTIVE+""},
            {"@COVER_YEAR", POLICY_DETAIL.COVER_YEAR+""},
            {"@PAY_YEAR", POLICY_DETAIL.PAY_YEAR+""},
            {"@COVER_UP_TO_AGE", POLICY_DETAIL.COVER_UP_TO_AGE+""},
            {"@PAY_UP_TO_AGE", POLICY_DETAIL.PAY_UP_TO_AGE+""},
           {"@POLICY_STATUS_REMARKS", POLICY_DETAIL.POLICY_STATUS_REMARKS},
            {"@CREATED_BY", POLICY_DETAIL.CREATED_BY},
            {"@CREATED_ON", POLICY_DETAIL.CREATED_ON+""},
            {"@REMARKS",POLICY_DETAIL.REMARKS } ,
            {"@COVER_TYPE", POLICY_DETAIL.COVER_TYPE.ToString() }
            }, "da_micro_policy_detail=>SavePolicyDetail(bl_micro_policy_detail POLICY_DETAIL)");

            if (db.RowEffect == -1)
            {

            }
            if (result)
            {
                _MESSAGE = "Success";
                _SUCCESS = true;
            }
            else
            {
                _MESSAGE = db.Message;
                _SUCCESS = false;
            }

        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [SavePolicyDetail(bl_micro_policy_detail POLICY_DETAIL)] in class [da_micro_policy_detail], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }
    public static bool UpdatePolicyDetail(bl_micro_policy_detail POLICY_DETAIL)
    {
        bool result = false;
        try
        {
            db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_DETAIL_UPDATE", new string[,] {
            {"@POLICY_DETAIL_ID", POLICY_DETAIL.POLICY_DETAIL_ID}, 
            {"@POLICY_ID",POLICY_DETAIL.POLICY_ID}, 
            {"@EFFECTIVE_DATE",POLICY_DETAIL.EFFECTIVE_DATE+""}, 
            {"@MATURITY_DATE",POLICY_DETAIL.MATURITY_DATE+""},
            {"@EXPIRY_DATE",POLICY_DETAIL.EXPIRY_DATE+""},
            {"@ISSUED_DATE", POLICY_DETAIL.ISSUED_DATE+""}, 
            {"@AGE",POLICY_DETAIL.AGE+""},
            {"@CURRENCY",POLICY_DETAIL.CURRANCY},
            {"@SUM_ASSURE", POLICY_DETAIL.SUM_ASSURE+""},
            {"@PAY_MODE", POLICY_DETAIL.PAY_MODE+""},
            {"@PAYMENT_CODE",POLICY_DETAIL.PAYMENT_CODE},
            {"@PREMIUM", POLICY_DETAIL.PREMIUM+""}, 
            {"@ANNUAL_PREMIUM",POLICY_DETAIL.ANNUAL_PREMIUM+""},
            {"@DISCOUNT_AMOUNT", POLICY_DETAIL.DISCOUNT_AMOUNT+""},
           {"@TOTAL_AMOUNT",POLICY_DETAIL.TOTAL_AMOUNT+""},
            {"@REFERRAL_FEE", POLICY_DETAIL.REFERRAL_FEE+""},
            {"@REFERRAL_INCENTIVE", POLICY_DETAIL.REFERRAL_INCENTIVE+""},
            {"@COVER_YEAR", POLICY_DETAIL.COVER_YEAR+""},
            {"@PAY_YEAR", POLICY_DETAIL.PAY_YEAR+""},
            {"@COVER_UP_TO_AGE", POLICY_DETAIL.COVER_UP_TO_AGE+""},
            {"@PAY_UP_TO_AGE", POLICY_DETAIL.PAY_UP_TO_AGE+""},
             {"@POLICY_STATUS_REMARKS", POLICY_DETAIL.POLICY_STATUS_REMARKS},
            {"@UPDATED_BY", POLICY_DETAIL.UPDATED_BY},
            {"@UPDATED_ON", POLICY_DETAIL.UPDATED_ON+""},
            {"@REMARKS",POLICY_DETAIL.REMARKS }
            }, "da_micro_policy_detail=>UpdatePolicyDetail(bl_micro_policy_detail POLICY_DETAIL)");
            if (db.RowEffect == -1) //error
            {
                _MESSAGE = db.Message;
                _SUCCESS = false;
            }
            else
            {
                _SUCCESS = true;
                _MESSAGE = "Success";
            }
           
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [UpdatePolicyDetail(bl_micro_policy_detail POLICY_DETAIL)] in class [da_micro_policy_detail], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }

    public static bl_micro_policy_detail GetPolicyDetailByPolicyID(string POLICY_ID)
    {
        bl_micro_policy_detail de = new bl_micro_policy_detail();

        try
        {
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_DETAIL_GET_BY_POLICY_ID", new string[,] {
            {"@POLICY_ID", POLICY_ID}
            }, "da_micro_policy_detail=>GetPolicyDetailByPolicyID(string POLICY_ID)");
            if (db.RowEffect == -1)//Error
            {
                _SUCCESS = false;
                _MESSAGE = db.Message;
            }
            else
            {
                if (tbl.Rows.Count > 0)
                {
                    _SUCCESS = true;
                    _MESSAGE = "Success";
                    var r = tbl.Rows[0];
                    de = new bl_micro_policy_detail()
                    {
                        POLICY_DETAIL_ID = r["policy_detail_id"].ToString(),
                        POLICY_ID = r["policy_id"].ToString(),
                        EFFECTIVE_DATE = Convert.ToDateTime(r["effective_date"].ToString()),
                        MATURITY_DATE = Convert.ToDateTime(r["maturity_date"].ToString()),
                        EXPIRY_DATE = Convert.ToDateTime(r["expiry_date"].ToString()),
                        ISSUED_DATE = Convert.ToDateTime(r["issued_date"].ToString()),
                        AGE = Convert.ToInt32(r["age"].ToString()),
                        CURRANCY = r["currency"].ToString(),
                        PAY_MODE = Convert.ToInt32(r["pay_mode"].ToString()),
                        SUM_ASSURE = Convert.ToDouble(r["sum_assure"].ToString()),
                        PREMIUM = Convert.ToDouble(r["premium"].ToString()),
                        ANNUAL_PREMIUM = Convert.ToDouble(r["annual_premium"].ToString()),
                        PAYMENT_CODE = r["payment_code"].ToString(),
                        DISCOUNT_AMOUNT = Convert.ToDouble(r["discount_amount"].ToString()),
                        TOTAL_AMOUNT = Convert.ToDouble(r["total_amount"].ToString()),
                        COVER_YEAR = Convert.ToInt32(r["cover_year"].ToString()),
                        PAY_YEAR = Convert.ToInt32(r["pay_year"].ToString()),
                        COVER_UP_TO_AGE = Convert.ToInt32(r["cover_up_to_age"].ToString()),
                        PAY_UP_TO_AGE = Convert.ToInt32(r["pay_up_to_age"].ToString()),
                        REFERRAL_FEE = Convert.ToDouble(r["referral_fee"].ToString()),
                        REFERRAL_INCENTIVE = Convert.ToDouble(r["referral_incentive"].ToString()),
                        CREATED_BY = r["created_by"].ToString(),
                        CREATED_ON = Convert.ToDateTime(r["created_on"].ToString()),
                        UPDATED_BY = r["updated_by"].ToString(),
                        UPDATED_ON = Convert.ToDateTime(r["updated_on"].ToString()),
                        REMARKS = r["remarks"].ToString()
                    };
                    _SUCCESS = true;
                    _MESSAGE = "Success";
                }
                else
                {
                    _SUCCESS = true;
                    _MESSAGE = "No Record Found";
                }
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            de = new bl_micro_policy_detail();
            Log.AddExceptionToLog("Error function [GetPolicyDetailByPolicyID(string POLICY_ID)] in class [da_micro_policy_detail], detail: " + ex.Message + "==>" + ex.StackTrace);

        }
        return de;
    }

    public static bool DeletePolicyDetail(string POLICY_DETAIL_ID)
    {
        bool result = false;
        try
        {
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_DETAIL_DELETE", new string[,] {
            {"@POLICY_DETAIL_ID", POLICY_DETAIL_ID}
            
            }, "da_micro_policy_detail=> DeletePolicyDetail(string POLICY_DETAIL_ID)");
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [DeletePolicyDetail(string POLICY_DETAIL_ID)] in class [da_micro_policy_detail], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }
}