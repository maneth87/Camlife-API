using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_micro_product_discount_config
/// </summary>
public class bl_micro_product_discount_config
{
	public bl_micro_product_discount_config()
	{
		//
		// TODO: Add constructor logic here
		//
 
        _ID = Helper.GetNewGuid(new string[,] { { "TABLE", "CT_MICRO_PRODUCT_DISCOUNT_CONFIG" }, { "FIELD", "ID" } });
	}
    private string _ID = "";


    public string ID
    {
        get
        {
            return _ID;
        }
        set
        {
            _ID = value;
        }
    }
    public string ProductID { get; set; }
 
    public DateTime EffectiveDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool Status { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedOn { get; set; }
    public string ProductRiderID { get; set; }
    public double BasicSumAssured { get; set; }
    public double RiderSumAssured { get; set; }
    public double BasicDiscountAmount { get; set; }
    public double RiderDiscountAmount { get; set; }
    public string ChannelItemId { get; set; }
    /// <summary>
    /// Return channel item name
    /// </summary>
    public string ChannelName { get { return da_channel.GetChannelItemNameByID(ChannelItemId); } }
    public string Remarks { get; set; }
    public string ClientType { get; set; }

}