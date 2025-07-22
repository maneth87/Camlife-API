using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
/// <summary>
/// Summary description for da_micro_application_insurance
/// </summary>
public class da_micro_application_insurance
{
	public da_micro_application_insurance()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private static bool _SUCCESS = false;
    private static string _MESSAGE = "";

    public static bool SUCCESS { get { return _SUCCESS; } }
    public static string MESSAGE { get { return _MESSAGE; } }

    private static DB db = new DB();

    public static bl_micro_application_insurance GetApplicationInsurance(string applicationNo, string userName="")
    {
        bl_micro_application_insurance app = new bl_micro_application_insurance();
        try
        {
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_INSURANCE_GET", new string[,] { 
            {"@APPLICATION_NUMBER",applicationNo}
            }, "da_micro_application_insurance => GetApplicationInsurance(string applicationNo, string userName)");

            if (db.RowEffect == -1) //error
            {
                _MESSAGE = db.Message;
            }
            else
            {
                _MESSAGE = "Success";
                foreach (DataRow r in tbl.Rows)
                {
                    app = new bl_micro_application_insurance() {
                     APPLICATION_NUMBER = r["application_number"].ToString(),
                      PRODUCT_ID=r["product_id"].ToString(),
                      TERME_OF_COVER=Convert.ToInt32(r["term_of_cover"].ToString()),
                      PAYMENT_PERIOD=Convert.ToInt32(r["payment_period"].ToString()),
                      SUM_ASSURE=Convert.ToDouble(r["sum_assure"].ToString()),
                      PAY_MODE=Convert.ToInt32(r["pay_mode"].ToString()),
                      PREMIUM=Convert.ToDouble(r["premium"].ToString()),
                      ANNUAL_PREMIUM=Convert.ToDouble(r["annual_premium"].ToString()),
                      DISCOUNT_AMOUNT=Convert.ToDouble(r["discount_amount"].ToString()),
                      TOTAL_AMOUNT=Convert.ToDouble(r["total_amount"].ToString()),
                      USER_PREMIUM=Convert.ToDouble(r["user_premium"].ToString()),
                      PACKAGE=r["package"].ToString(),
                      PAYMENT_CODE=r["payment_code"].ToString()
                    };
                }
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
           
            Log.AddExceptionToLog("Error function [da_micro_application_insurance] in class [" + MethodBase.GetCurrentMethod().Name + "], Line:"+Log.GetLineNumber(ex)+", detail:" + ex.Message);
        }
        return app;
    }

    public static bool SaveApplicationInsurance(bl_micro_application_insurance APP_INSURANCE)
    {
        bool result = false;
        try
        {
            //DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_INSURANCE_INSERT", new string[,] {
            {"@ID", APP_INSURANCE.ID},
            {"@APPLICATION_NUMBER", APP_INSURANCE.APPLICATION_NUMBER}, 
            {"@PRODUCT_ID",APP_INSURANCE.PRODUCT_ID}, 
            {"@TERM_OF_COVER" ,APP_INSURANCE.TERME_OF_COVER+""},
            {"@TERM_OF_COVER_TYPE", APP_INSURANCE.COVER_TYPE },
            {"@PAYMENT_PERIOD", APP_INSURANCE.PAYMENT_PERIOD+""}, 
            {"@SUM_ASSURE", APP_INSURANCE.SUM_ASSURE+""}, 
            {"@PAY_MODE",APP_INSURANCE.PAY_MODE+""}, 
            {"@PREMIUM", APP_INSURANCE.PREMIUM+""}, 
            {"@ANNUAL_PREMIUM", APP_INSURANCE.ANNUAL_PREMIUM+""}, 
            {"@TOTAL_AMOUNT",APP_INSURANCE.TOTAL_AMOUNT+""}, 
            {"@DISCOUNT_AMOUNT",APP_INSURANCE.DISCOUNT_AMOUNT+""}, 
            {"@USER_PREMIUM",APP_INSURANCE.USER_PREMIUM+""},  
            {"@PACKAGE",APP_INSURANCE.PACKAGE}, 
            {"@CREATED_BY", APP_INSURANCE.CREATED_BY}, 
            {"@CREATED_ON", APP_INSURANCE.CREATED_ON+""}, 
            {"@REMARKS", APP_INSURANCE.REMARKS},
            {"@PAYMENT_CODE", APP_INSURANCE.PAYMENT_CODE}
            }, "da_micro_application_insurance=>SaveApplicationInsurance(bl_micro_application_insurance APP_INSURANCE)");

            if (db.RowEffect == -1) //error
            {
                _MESSAGE = db.Message;
            }
            else
            {
                _MESSAGE = "Success";
            }
            _SUCCESS = result;
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [SaveApplicationInsurance(bl_micro_application_insurance APP_INSURANCE)] in class [da_micro_application_insurance], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    
    }
    public static bool UpdateApplicationInsurance(bl_micro_application_insurance APP_INSURANCE)
    {
        bool result = false;
        try
        {
            //DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_INSURANCE_UPDATE", new string[,] {
            {"@ID", APP_INSURANCE.ID},
            {"@APPLICATION_NUMBER", APP_INSURANCE.APPLICATION_NUMBER}, 
            {"@PRODUCT_ID",APP_INSURANCE.PRODUCT_ID}, 
            {"@TERM_OF_COVER" ,APP_INSURANCE.TERME_OF_COVER+""},
             {"@TERM_OF_COVER_TYPE", APP_INSURANCE.COVER_TYPE },
            {"@PAYMENT_PERIOD", APP_INSURANCE.PAYMENT_PERIOD+""}, 
            {"@SUM_ASSURE", APP_INSURANCE.SUM_ASSURE+""}, 
            {"@PAY_MODE",APP_INSURANCE.PAY_MODE+""}, 
            {"@PREMIUM", APP_INSURANCE.PREMIUM+""}, 
            {"@ANNUAL_PREMIUM", APP_INSURANCE.ANNUAL_PREMIUM+""}, 
            {"@TOTAL_AMOUNT",APP_INSURANCE.TOTAL_AMOUNT+""}, 
            {"@DISCOUNT_AMOUNT",APP_INSURANCE.DISCOUNT_AMOUNT+""}, 
            {"@USER_PREMIUM",APP_INSURANCE.USER_PREMIUM+""},  
            {"@PACKAGE",APP_INSURANCE.PACKAGE}, 
            {"@UPDATED_BY", APP_INSURANCE.UPDATED_BY}, 
            {"@UPDATED_ON", APP_INSURANCE.UPDATED_ON+""}, 
            {"@REMARKS", APP_INSURANCE.REMARKS},
            {"@PAYMENT_CODE", APP_INSURANCE.PAYMENT_CODE}
            }, "da_micro_application_insurance=>UpdateApplicationInsurance(bl_micro_application_insurance APP_INSURANCE)");

            if (db.RowEffect == -1) //error
            {
                _MESSAGE = db.Message;
            }
            else
            {
                _MESSAGE = "Success";
            }
            _SUCCESS = result;
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [UpateApplicationInsurance(bl_micro_application_insurance APP_INSURANCE)] in class [da_micro_application_insurance], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;

    }
    public static bool DeleteApplicationInsurance(string APPLICATION_NUMBER)
    {
        bool result = false;
        try
        {
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_INSURANCE_DELETE", new string[,] {
            {"@APPLICATION_NUMBER", APPLICATION_NUMBER}
            
            }, "da_micro_application_insurance=>DeleteApplicationInsurance(string APPLICATION_NUMBER)");
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [DeleteApplicationInsurance(string APPLICATION_NUMBER)] in class [da_micro_application_insurance], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }
}