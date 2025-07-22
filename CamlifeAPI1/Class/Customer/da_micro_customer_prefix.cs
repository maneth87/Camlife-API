using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for da_microc_customer_prefix
/// </summary>
public class da_micro_customer_prefix
{
	public da_micro_customer_prefix()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private static DB db = new DB();
    private static bool _SUCCESS = false;
    private static string _MESSAGE = "";
    public static bool SUCCESS { get { return _SUCCESS; } }
    public static string MESSAGE { get { return _MESSAGE; } }

    public static bl_micro_customer_prefix GetLastCustomerPrefix()
    {
        bl_micro_customer_prefix prefix = new bl_micro_customer_prefix();

        try
        {

            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_CUSTOMER_PREFIX_GET_LAST_PREFIX", new string[,]{
            
        }, "da_micro_customer_prefix => GetLastCustomerPrefix()");
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
                    prefix = new bl_micro_customer_prefix()
                    {
                       CUSTOMER_PREFIX_ID = row["CUSTOMER_PREFIX_ID"].ToString(),
                        PREFIX1 = row["PREFIX1"].ToString(),
                        PREFIX2 = row["PREFIX2"].ToString(),
                        DIGITS = row["DIGITS"].ToString(),
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

            prefix = new bl_micro_customer_prefix();
            Log.AddExceptionToLog("Error function [bl_micro_customer_prefix GetLastCustomerPrefix()] in class [da_micro_customer_prefix], detail: " + ex.Message + " => " + ex.StackTrace);

        }
        return prefix;
    }
}