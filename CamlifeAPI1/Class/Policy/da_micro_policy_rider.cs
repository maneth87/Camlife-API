using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
/// <summary>
/// Summary description for da_micro_policy_rider
/// </summary>
public class da_micro_policy_rider
{
	public da_micro_policy_rider()
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
    public static bool SaveRider(bl_micro_policy_rider RIDER)
    {
        bool result = false;
        try
        {
            //DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_RIDER_INSERT", new string[,] {
            {"@ID", RIDER.ID},
            {"@POLICY_ID", RIDER.POLICY_ID}, 
            {"@PRODUCT_ID",RIDER.PRODUCT_ID}, 
            {"@SUM_ASSURE", RIDER.SUM_ASSURE+""}, 
            {"@PREMIUM", RIDER.PREMIUM+""}, 
            {"@ANNUAL_PREMIUM", RIDER.ANNUAL_PREMIUM+""}, 
            {"@DISCOUNT_AMOUNT",RIDER.DISCOUNT_AMOUNT+""},
            {"@TOTAL_AMOUNT",RIDER.TOTAL_AMOUNT+""},
            {"@CREATED_BY", RIDER.CREATED_BY}, 
            {"@CREATED_ON", RIDER.CREATED_ON+""}, 
            {"@REMARKS", RIDER.REMARKS}
            }, "da_micro_policy_rider=>SaveRider(bl_micro_policy_rider RIDER)");

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
            Log.AddExceptionToLog("Error function [SaveRider(bl_micro_policy_rider RIDER)] in class [da_micro_policy_rider], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;

    }
    public static bl_micro_policy_rider GetRider(string policyId, string userName = "")
    {
        bl_micro_policy_rider rider = new bl_micro_policy_rider();
        try
        {
            //DB db = new DB();
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_RIDER_GET_BY_POLICY_ID", new string[,] {
            {"@POLICY_ID", policyId}, 
           
            }, "da_micro_policy_rider=>GetRider(string policyId, string userName");

            if (db.RowEffect == -1) //error
            {
                _MESSAGE = db.Message;
            }
            else
            {
                _MESSAGE = "Success";

                foreach (DataRow r in tbl.Rows)
                {
                    rider = new bl_micro_policy_rider() {
                     POLICY_ID= r["policy_id"].ToString(),
                     PRODUCT_ID=r["product_id"].ToString(),
                     SUM_ASSURE = Convert.ToDouble(r["sum_assure"].ToString()),
                     PREMIUM = Convert.ToDouble(r["premium"].ToString()),
                     ANNUAL_PREMIUM = Convert.ToDouble(r["annual_premium"].ToString()),
                     DISCOUNT_AMOUNT = Convert.ToDouble(r["discount_amount"].ToString()),
                     TOTAL_AMOUNT = Convert.ToDouble(r["total_amount"].ToString()),
                 
                    };
                }
            }
           
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
         
            Log.AddExceptionToLog("da_micro_policy_rider", "GetRider(string policyId, string userName = \"\")", ex);
        }
        return rider;

    }
    public static bool UpdateRider(bl_micro_policy_rider RIDER)
    {
        bool result = false;
        try
        {
            //DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_RIDER_UPDATE", new string[,] {
             {"@ID", RIDER.ID},
            {"@POLICY_ID", RIDER.POLICY_ID}, 
            {"@PRODUCT_ID",RIDER.PRODUCT_ID}, 
            {"@SUM_ASSURE", RIDER.SUM_ASSURE+""}, 
            {"@PREMIUM", RIDER.PREMIUM+""}, 
            {"@ANNUAL_PREMIUM", RIDER.ANNUAL_PREMIUM+""}, 
             {"@DISCOUNT_AMOUNT",RIDER.DISCOUNT_AMOUNT+""},
            {"@TOTAL_AMOUNT",RIDER.TOTAL_AMOUNT+""},
            {"@UPDATED_BY", RIDER.UPDATED_BY}, 
            {"@UPDATED_ON", RIDER.UPDATED_ON+""}, 
            {"@REMARKS", RIDER.REMARKS}
            }, "da_micro_policy_rider=>UpdateRider(bl_micro_policy_rider RIDER)");

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
            Log.AddExceptionToLog("Error function [UpdateRider(bl_micro_policy_rider RIDER)] in class [da_micro_policy_rider], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;

    }
    public static bool DeleteRider(string ID)
    {
        bool result = false;
        try
        {
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_RIDER_DELETE", new string[,] {
            {"@ID", ID}
            
            }, "da_micro_policy_rider=> DeleteRider(string ID)");
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [ DeleteRider(string ID)] in class [da_micro_policy_rider], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }
}