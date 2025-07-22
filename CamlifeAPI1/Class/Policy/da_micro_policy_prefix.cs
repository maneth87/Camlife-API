using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for da_micro_policy_prefix
/// </summary>
public class da_micro_policy_prefix
{
    private static DB db = new DB();
    private static bool _SUCCESS = false;
    private static string _MESSAGE = "";
    public static bool SUCCESS { get { return _SUCCESS; } }
    public static string MESSAGE { get { return _MESSAGE; } }

	public da_micro_policy_prefix()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bl_micro_policy_prefix GetLastPolicyPrefix()
    {
        bl_micro_policy_prefix prefix = new bl_micro_policy_prefix();

        try
        {

            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_PREFIX_GET_LAST_PREFIX", new string[,]{
           
        }, "da_micro_policy_prefix => GetLastPolicyPrefix()");
            if (db.RowEffect == -1)//error get data
            {
                _SUCCESS = false;
                _MESSAGE = db.Message;
            }
            else
            {
                if (tbl.Rows.Count > 0)
                {
                    var row = tbl.Rows[0];
                    prefix = new bl_micro_policy_prefix()
                    {
                        POLICY_PREFIX_ID = row["POLICY_PREFIX_ID"].ToString(),
                        PREFIX1 = row["PREFIX1"].ToString(),
                        PREFIX2 = row["PREFIX2"].ToString(),
                        DIGITS =row["DIGITS"].ToString(),
                        STATUS = Convert.ToInt32(row["STATUS"].ToString())
                    };

                    _SUCCESS = true;
                    _MESSAGE = "Success";
                }
                else
                {
                    _SUCCESS = true;
                    _MESSAGE = "No records found.";
                }
            }
        }
        catch (Exception ex)
        {
            _MESSAGE = ex.Message;

            prefix = new bl_micro_policy_prefix();
            Log.AddExceptionToLog("Error function [GetLastPolicyPrefix()] in class [da_micro_policy_prefix], detail: " + ex.Message + " => " + ex.StackTrace);

        }
        return prefix;
    }
    public static bl_micro_policy_prefix GetLastPolicyPrefix(string productId)
    {
        bl_micro_policy_prefix prefix = new bl_micro_policy_prefix();

        try
        {

            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_PREFIX_GET_LAST_PREFIX", new string[,]{
            { "@PRODUCT_ID", productId}
        }, "da_micro_policy_prefix => GetLastPolicyPrefix()");
            if (db.RowEffect == -1)//error get data
            {
                _SUCCESS = false;
                _MESSAGE = db.Message;
            }
            else
            {
                if (tbl.Rows.Count > 0)
                {
                    var row = tbl.Rows[0];
                    prefix = new bl_micro_policy_prefix()
                    {
                        POLICY_PREFIX_ID = row["POLICY_PREFIX_ID"].ToString(),
                        PREFIX1 = row["PREFIX1"].ToString(),
                        PREFIX2 = row["PREFIX2"].ToString(),
                        DIGITS = row["DIGITS"].ToString(),
                        STATUS = Convert.ToInt32(row["STATUS"].ToString()),
                        PRODUCT_ID = row["product_id"].ToString()
                    };

                    _SUCCESS = true;
                    _MESSAGE = "Success";
                }
                else
                {
                    _SUCCESS = true;
                    _MESSAGE = "No records found.";
                }
            }
        }
        catch (Exception ex)
        {
            _MESSAGE = ex.Message;

            prefix = new bl_micro_policy_prefix();
            Log.AddExceptionToLog("Error function [GetLastPolicyPrefix()] in class [da_micro_policy_prefix], detail: " + ex.Message + " => " + ex.StackTrace);

        }
        return prefix;
    }
}