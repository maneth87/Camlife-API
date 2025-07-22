using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_override_discount_config
/// </summary>
public class bl_override_discount_config
{
	public bl_override_discount_config()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string Id { get; set; }
    public string CustomerId{get;set;}
    public string PolicyId{get;set;}
    public string ProductId { get; set; }
    public string ProductRiderId { get; set; }
    public double BasicSumAssured { get; set; }
    public double RiderSumAssured { get; set; }
    public double BasicDiscountAmount { get; set; }
    public double RiderDiscountAmount { get; set; }
    public string ChannelItemId { get; set; }
    public DateTime EffectiveDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool Status { get; set; }
    public string Remarks { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedOn { get; set; }
    /*Read only Property*/
    //public string ChannelName { get { return da_channel.GetChannelItemNameByID(ChannelItemId); } }
    //public string CustomerName { get { return da_micro_customer.GetCustomer(CustomerId).FullNameEn; } }
    //public string CustomerNumber { get { return da_micro_customer.GetCustomer(CustomerId).CUSTOMER_NUMBER; } }
    //public string PolicyNumber { get { return da_micro_policy.GetPolicyByID(PolicyId).POLICY_NUMBER; } }

    public string ChannelName { get; set; }
    public string CustomerName { get; set; }
    public string CustomerNumber { get; set; }
    public string PolicyNumber { get; set; }


    public class Table
    {
        /// <summary>
        /// Return table name
        /// </summary>
        public static string Name { get { return "CT_MICRO_PRODUCT_OVERRIDE__DISCOUNT_CONFIG"; } }
        /// <summary>
        /// Return column name
        /// </summary>
        public class Columns
        {
            public static string ProductId { get { return "PRODUCT_ID"; } }
            public static string PolicyId { get { return "POLICY_ID"; } }
            /// <summary>
            /// READONLY COLUMN
            /// </summary>
            public static string PolicyNumber { get { return "POLICY_NUMBER"; } }
            public static string CustomerId { get { return "CUSTOMER_ID"; } }
            /// <summary>
            /// READONLY COLUMN
            /// </summary>
            public static string CustomerNumber { get { return "CUSTOMER_NUMBER"; } }
            /// <summary>
            /// READONLY COLUMN
            /// </summary>
            public static string CustomerLastName { get { return "LAST_NAME_IN_ENGLISH"; } }
            /// <summary>
            /// READONLY COLUMN
            /// </summary>
            public static string CustomerFirstName { get { return "FIRST_NAME_IN_ENGLISH"; } }
        }
    }
}