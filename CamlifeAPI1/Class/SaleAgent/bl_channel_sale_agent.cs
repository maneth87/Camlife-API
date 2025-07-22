using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_channel_sale_agent
/// </summary>
public class bl_channel_sale_agent
{
	public bl_channel_sale_agent()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string ID { get; set; }
    public string USER_NAME { get; set; }
    public string SALE_AGENT_ID { get; set; }
    public string CHANNEL_ITEM_ID { get; set; }
    public string CHANNEL_LOCATION_ID { get; set; }
    public DateTime CREATED_ON { get; set; }
    public string CREATED_BY { get; set; }
    public DateTime UPDATED_ON { get; set; }
    public string UPDATED_BY { get; set; }
    public string REMARKS { get; set; }

    #region these addition fields are optional 
    public string AgentNameEn { get; set; }
    public string AgentNameKh { get; set; }

    #endregion
    public List<AgentChannel> Channels { get; set; }
    
    public List<bl_channel_item> ChannelItems { get; set; }
}
public class AgentChannel
{
    public string ChannelItemId { get; set; }
    public string ChannelName { get; set; } 
    public string ChannelLocationId { get; set; }
    public string OfficeCode { get; set; }
    public string OfficeName { get; set; }
}
