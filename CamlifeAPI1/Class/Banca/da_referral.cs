using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for da_referral
/// </summary>
public class da_referral
{
    private static bool _SUCCESS = false;
    private static string _MESSAGE = "";
    private static string _CODE = "";

    public static bool SUCCESS { get { return _SUCCESS; } }
    public static string MESSAGE { get { return _MESSAGE; } }
    public static string CODE { get { return _CODE; } }
    private static string MYNAME = "da_referral";
    private static DB db = new DB();
    public da_referral()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// Get all Referral
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public static List<bl_referral> GetAllReferral(string userName = "")
    {
        List<bl_referral> reList = new List<bl_referral>();

        try
        {
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_REFERRAL", new string[,] {

            }, MYNAME + " => GetAllReferral(string userName)");
            if (db.RowEffect < 0)
            {
                //error generate
                _MESSAGE = db.Message;
                _SUCCESS = false;

            }
            else
            {
                _SUCCESS = true;
                foreach (DataRow r in tbl.Rows)
                {
                    reList.Add(new bl_referral()
                    {
                        ReferralId = r["id"].ToString(),
                        ChannelItemID = r["channel_item_id"].ToString(),
                        ChannelLocationId = r["channel_location_id"].ToString(),
                        ReferralStaffId = r["referral_staff_id"].ToString(),
                        ReferralStaffName = r["referral_staff_name"].ToString(),
                        ReferralStaffPosition = r["referral_staff_position"].ToString(),
                        Status = Convert.ToInt32(r["status"].ToString())

                    });
                }
            }
        }
        catch (Exception ex)
        {
            reList = new List<bl_referral>();
            _CODE = "0";
            _MESSAGE = ex.Message;
            _SUCCESS = false;
            Log.AddExceptionToLog("Error function [GetAllReferral(string userName = \"\")] in class [da_customer_lead], detail: " + ex.Message + " ==> " + ex.StackTrace);

        }
        return reList;
    }
    public static List<bl_referral> GetActiveReferral(string channelItemId, string referralStaffId,  string userName = "")
    {
        List<bl_referral> reList = new List<bl_referral>();
        List<bl_referral> reListFilter = new List<bl_referral>();
        try
        {
            reList = da_referral.GetAllReferral(userName);
            _SUCCESS = da_referral.SUCCESS;
            if (_SUCCESS)
            {
              

                reListFilter = reList.Where(_ => _.ChannelItemID == channelItemId && _.ReferralStaffId == referralStaffId && _.Status == 1).ToList();


            }
            else
            {
                _MESSAGE = da_referral.MESSAGE;
                reListFilter = new List<bl_referral>();
            }

        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            reListFilter = new List<bl_referral>();
            _CODE = "0";
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("Error function [GetAllReferral(string userName = \"\")] in class [da_customer_lead], detail: " + ex.Message + " ==> " + ex.StackTrace);

        }
        return reListFilter;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="channelItemId"></param>
    /// <param name="channelLocationId"></param>
    /// <param name="referralInfo">Referrer id or Name</param>
    /// <param name="userName"></param>
    /// <returns></returns>
    public static List<bl_referral> GetActiveReferral(string channelItemId, string channelLocationId, string referralInfo, string userName = "")
    {
        List<bl_referral> reList = new List<bl_referral>();

        try
        {
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_REFERRAL_ACTIVE", new string[,] {
           {"@channel_item_id", channelItemId},{"@channel_location_id", channelLocationId},{"@referral", referralInfo}
            }, MYNAME + " => GetActiveReferral(string channelItemId, string channelLocationId, string referralInfo, string userName)");
            if (db.RowEffect < 0)
            {
                //error generate
                _MESSAGE = db.Message;
                _SUCCESS = false;

            }
            else
            {
                _SUCCESS = true;
                int index = 1;
                foreach (DataRow r in tbl.Rows)
                {
                    reList.Add(new bl_referral()
                    {
                        No = index,
                        ReferralId = r["id"].ToString(),
                        ChannelItemID = r["channel_item_id"].ToString(),
                        ChannelLocationId = r["channel_location_id"].ToString(),
                        ReferralStaffId = r["referral_staff_id"].ToString(),
                        ReferralStaffName = r["referral_staff_name"].ToString(),
                        ReferralStaffPosition = r["referral_staff_position"].ToString(),
                        Status = Convert.ToInt32(r["status"].ToString())

                    });
                    index += 1;
                }
            }
        }
        catch (Exception ex)
        {
            _MESSAGE = ex.Message;
            reList = new List<bl_referral>();
            _CODE = "0";
         _SUCCESS=false;
            Log.AddExceptionToLog("Error function [GetAllReferral(string userName = \"\")] in class [da_customer_lead], detail: " + ex.Message + " ==> " + ex.StackTrace);

        }
        return reList;
    }
}