using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for da_group_micro_application_prefix
/// </summary>
public class da_group_micro_application_prefix
{
    private static string className = "da_group_micro_application_prefix";
    public da_group_micro_application_prefix()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static bl_group_micro_application_prefix GetLastPrefix(string groupCode)
    {
        bl_group_micro_application_prefix obj = new bl_group_micro_application_prefix();
        try
        {
            DataTable tbl = new DB().GetData(AppConfiguration.GetConnectionString(), "SP_CT_GROUP_MICRO_APPLICATION_NUMBER_PREFIX_GET_LAST_PREFIX", new string[,] {
            {"@group_code", groupCode}
            }, className + "=>GetLastPrefix(string groupCode)");
            if (tbl.Rows.Count > 0)
            {
                var r = tbl.Rows[0];
                obj = new bl_group_micro_application_prefix()
                {
                    Id = r["id"].ToString(),
                    GroupCode = r["group_code"].ToString(),
                    Prefix1 = r["prefix1"].ToString(),
                    Prefix2 = r["prefix2"].ToString(),
                    Digits = r["digits"].ToString(),
                    Status = Convert.ToInt32(r["status"].ToString())
                };
            }
        }
        catch (Exception ex)
        {
            obj = new bl_group_micro_application_prefix();
            Log.AddExceptionToLog("Error function [GetLastPrefix(string groupCode)] in class [" + className + "], detail:" + ex.Message);
        }
        return obj;
    }
}