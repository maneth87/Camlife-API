using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for da_micro_application_prefix
/// </summary>
public class da_micro_application_prefix
{
    public da_micro_application_prefix()
    {
        //
        // TODO: Add constructor logic here
        //
        MESSAGE = "";
        SUCCESS = false;
    }
    public static string MESSAGE;
    public static bool SUCCESS;
    public static bl_micro_application_prefix GetLastApplicationPrefix()
    {
        bl_micro_application_prefix prefix = new bl_micro_application_prefix();

        try
        {
            DB db = new DB();
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_PREFIX_GET_LAST_PREFIX", new string[,]{
            
        }, "da_micro_application_prefix => GetLastApplicationPrefix()");
            if (db.RowEffect == -1)//error get data
            {
                SUCCESS = false;
                MESSAGE = db.Message;
            }
            else
            {
                if (tbl.Rows.Count > 0)
                {
                    var row = tbl.Rows[0];
                    prefix = new bl_micro_application_prefix() {
                    APPLICATION_PREFIX_ID=row["APPLICATION_PREFIX_ID"].ToString(),
                    PREFIX1= row["PREFIX1"].ToString(),
                    PREFIX2=row["PREFIX2"].ToString(),
                    DIGITS= row["DIGITS"].ToString(),
                    STATUS=Convert.ToInt32(row["STATUS"].ToString())
                    };

                    SUCCESS = true;
                    MESSAGE = "Success";
                }
                else
                {
                    SUCCESS = true;
                    MESSAGE = "No records found.";
                }
            }
        }
        catch (Exception ex)
        {
            MESSAGE = ex.Message;

            prefix = new bl_micro_application_prefix();
            Log.AddExceptionToLog("Error function [GetLastApplicationPrefix()] in class [da_micro_application_prefix], detail: " + ex.Message + " => " + ex.StackTrace);

        }
        return prefix;
    }

}