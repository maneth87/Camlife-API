using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_group_micro_application_number
/// </summary>
public class bl_group_micro_application_number
{
    private string className = "bl_group_micro_application_number";
	public bl_group_micro_application_number()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public bl_group_micro_application_number(string id, double seq, string applicationNumber, string groupCode, string channelId, string channelItemId, string channelLocationId , string createdBy, DateTime createdOn)
    {
        Id = id;
        Seq = seq;
        ApplicationNumber = applicationNumber;
        GroupCode = groupCode;
        ChannelId = channelId;
        ChannelItemId = channelItemId;
        ChannelLocationId = channelLocationId;
        CreatedBy = createdBy;
        CreatedOn = createdOn;
    }
    public string Id { get; set; }
    public double Seq { get; set; }
    public string ApplicationNumber { get; set; }
    public string GroupCode { get; set; }
    public string ChannelId { get; set; }
    public string ChannelItemId { get; set; }
    public string ChannelLocationId { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedOn { get; set; }
    public string Remarks { get; set; }
    public string PrefixYear { get { return GetPrefixYear(); } }

    private string GetPrefixYear()
    {
        string prefix = "";
        try
        {
            if (ApplicationNumber != null)
            {
                string[] arr = ApplicationNumber.Split('-');
                if (arr.Length > 0)
                {
                    prefix = arr[arr.Length - 1].ToString().Substring(0, 2);
                }
            }
        }
        catch (Exception ex)
        {
            prefix = "";
            Log.AddExceptionToLog("Error function [GetPrefixYear()] in class [" + className + "], detail:" + ex.Message);
        }
        return prefix;
    }

}