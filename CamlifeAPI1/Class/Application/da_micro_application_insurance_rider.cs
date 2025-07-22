using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for da_micro_application_insurance_rider
/// </summary>
public class da_micro_application_insurance_rider
{
    public da_micro_application_insurance_rider()
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
    public static bool SaveApplicationInsuranceRider(bl_micro_application_insurance_rider APP_INSURANCE_RIDER)
    {
        bool result = false;
        try
        {
            //DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_INSURANCE_RIDER_INSERT", new string[,] {
                  {"@ID", APP_INSURANCE_RIDER.ID},
            {"@APPLICATION_NUMBER", APP_INSURANCE_RIDER.APPLICATION_NUMBER}, 
            {"@PRODUCT_ID",APP_INSURANCE_RIDER.PRODUCT_ID}, 
            
            {"@SUM_ASSURE", APP_INSURANCE_RIDER.SUM_ASSURE+""}, 
           
            {"@PREMIUM", APP_INSURANCE_RIDER.PREMIUM+""}, 
            {"@ANNUAL_PREMIUM", APP_INSURANCE_RIDER.ANNUAL_PREMIUM+""}, 
            {"@DISCOUNT_AMOUNT",APP_INSURANCE_RIDER.DISCOUNT_AMOUNT+""},
            {"@TOTAL_AMOUNT",APP_INSURANCE_RIDER.TOTAL_AMOUNT+""},
            {"@CREATED_BY", APP_INSURANCE_RIDER.CREATED_BY}, 
            {"@CREATED_ON", APP_INSURANCE_RIDER.CREATED_ON+""}, 
            {"@REMARKS", APP_INSURANCE_RIDER.REMARKS}
            }, "da_micro_application_insurance_rider=>SaveApplicationInsuranceRider(bl_micro_application_insurance_rider APP_INSURANCE_RIDER)");

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
            Log.AddExceptionToLog("Error function [SaveApplicationInsuranceRider(bl_micro_application_insurance_rider APP_INSURANCE_RIDER)] in class [da_micro_application_insurance_rider], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;

    }
    public static bool UpdateApplicationInsuranceRider(bl_micro_application_insurance_rider APP_INSURANCE_RIDER)
    {
        bool result = false;
        try
        {
            //DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_INSURANCE_RIDER_UPDATE", new string[,] {
             {"@ID", APP_INSURANCE_RIDER.ID},
            {"@APPLICATION_NUMBER", APP_INSURANCE_RIDER.APPLICATION_NUMBER}, 
            {"@PRODUCT_ID",APP_INSURANCE_RIDER.PRODUCT_ID}, 
            {"@SUM_ASSURE", APP_INSURANCE_RIDER.SUM_ASSURE+""}, 
            {"@PREMIUM", APP_INSURANCE_RIDER.PREMIUM+""}, 
            {"@ANNUAL_PREMIUM", APP_INSURANCE_RIDER.ANNUAL_PREMIUM+""}, 
             {"@DISCOUNT_AMOUNT",APP_INSURANCE_RIDER.DISCOUNT_AMOUNT+""},
            {"@TOTAL_AMOUNT",APP_INSURANCE_RIDER.TOTAL_AMOUNT+""},
            {"@UPDATED_BY", APP_INSURANCE_RIDER.UPDATED_BY}, 
            {"@UPDATED_ON", APP_INSURANCE_RIDER.UPDATED_ON+""}, 
            {"@REMARKS", APP_INSURANCE_RIDER.REMARKS}
            }, "da_micro_application_insurance_rider=>UpdateApplicationInsuranceRider(bl_micro_application_insurance_rider APP_INSURANCE_RIDER)");

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
            Log.AddExceptionToLog("Error function [UpdateApplicationInsuranceRider(bl_micro_application_insurance_rider APP_INSURANCE_RIDER)] in class [da_micro_application_insurance_rider], detail: " + ex.Message );
        }
        return result;

    }
    public static bool DeleteApplicationInsuranceRider(string APPLICATION_NUMBER)
    {
        bool result = false;
        try
        {
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_INSURANCE_RIDER_DELETE", new string[,] {
            {"@APPLICATION_NUMBER", APPLICATION_NUMBER}
            
            }, "da_micro_application_insurance_rider=>DeleteApplicationInsuranceRider(string APPLICATION_NUMBER)");
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [DeleteApplicationInsuranceRider(string APPLICATION_NUMBER)] in class [da_micro_application_insurance_rider], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }
    public static bl_micro_application_insurance_rider GetApplicationRider(string APPLICATION_NUMBER)
    {
        bl_micro_application_insurance_rider rider = new bl_micro_application_insurance_rider();
        try
        {
            DataTable tbl = new DB().GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_INSURANCE_RIDER_GET_BY_APPLICATION_NUMBER", new string[,] {
            {"@APPLICATION_NUMBER", APPLICATION_NUMBER}
            
            }, "da_micro_application_insurance_rider=>GetApplicationRider(string APPLICATION_NUMBER)");

            if (tbl.Rows.Count > 0)
            {
                var r = tbl.Rows[0];
                rider = new bl_micro_application_insurance_rider()
                {
                    ID = r["id"].ToString(),
                    APPLICATION_NUMBER = r["application_number"].ToString(),
                    SUM_ASSURE=Convert.ToDouble(r["sum_assure"].ToString()),
                    PRODUCT_ID = r["product_id"].ToString(),
                    PREMIUM = Convert.ToDouble(r["premium"].ToString()),
                    ANNUAL_PREMIUM = Convert.ToDouble(r["annual_premium"].ToString()),
                    DISCOUNT_AMOUNT = Convert.ToDouble(r["discount_amount"].ToString()),
                    TOTAL_AMOUNT = Convert.ToDouble(r["total_amount"].ToString()),
                    CREATED_BY = r["created_by"].ToString(),
                    CREATED_ON = Convert.ToDateTime(r["created_on"].ToString()),
                    REMARKS = r["remarks"].ToString()

                };
            }
            else
            {
                rider = new bl_micro_application_insurance_rider();
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            rider = new bl_micro_application_insurance_rider();
            Log.AddExceptionToLog("Error function [GetApplicationRider(string APPLICATION_NUMBER)] in class [da_micro_application_insurance_rider], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return rider;
    }
}