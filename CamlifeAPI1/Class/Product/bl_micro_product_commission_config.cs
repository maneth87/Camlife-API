using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_miro_product_commission_config
/// </summary>
public class bl_micro_product_commission_config 
{
	public bl_micro_product_commission_config()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public bl_micro_product_commission_config(string id, string channelItemId, string productId, string commissionType, string valueType, double value, int status, string createdBy, DateTime createdOn, string createdRemarks, string updatedBy, DateTime updatedOn, string updatedRemarks)
    {
        ID = id;
        ChannelItemId = channelItemId;
        ProductId = productId;
        CommissionType = commissionType;
        ValueType = valueType;
        Value = value;
        Status = status;
        CreatedBy = createdBy;
        CreatedOn = createdOn;
        CreatedRemarks = createdRemarks;
        UpdatedBy = updatedBy;
        UpdatedOn = updatedOn;
        UpdatedRemarks = updatedRemarks;
    }
    //public enum SelectCommissionType { REFERRAL_FEE, INCENTIVE }
    //public enum SelectValueType { FIX, PERCENTAGE }
    public string ID { get; set; }
    public string ChannelItemId { get; set; }
    public string ProductId { get; set; }
    public string CommissionType { get; set; }
    public string ValueType { get; set; }
    public double Value { get; set; }
    public DateTime EffectiveFrom { get; set; }
    public DateTime EffectiveTo { get; set; }
    public int Status { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedRemarks { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedOn { get; set; }
    public string UpdatedRemarks { get; set; }
    public string Remarks { get; set; }
    /// <summary>
    /// Return product title in english
    /// </summary>
    public string ProductName { get { return da_product.GetProductByProductID(ProductId).En_Title; } }
    public string ChannelName { get { return da_channel.GetChannelItemNameByID(ChannelItemId); } }
    public string ClientType { get; set; }
    public class Table {
        public class Columns
        {
            public static string ProductId { get { return "PRODUCT_ID"; } }
            public static string ChannelName { get { return "CHANNEL_NAME"; } }
        }
    }
    public class CommissionTypeOption {
        public static string ReferralFee { get { return "REFERRAL_FEE"; } }
        public static string Incentive { get { return "INCENTIVE"; } }
    }
    public class ValueTypeOption
    {
        public static string Fix { get { return "FIX"; } }
        public static string Percentage { get { return "PERCENTAGE"; } }
    }
}