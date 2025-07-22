using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for bl_micro_product_config
/// </summary>
/// 
public class bl_micro_product_config
{

    public bl_micro_product_config()
    {
        //
        // TODO: Add constructor logic here
        //

    }
    public string Id { get; set; }
    public string ProductId { get; set; }
    public string EnAbbr { get; set; }
    public string EnTitle { get; set; }
    public string KhAbbr { get; set; }
    public string KhTitle { get; set; }
    public int AgeMin { get; set; }
    public int AgeMax { get; set; }
    public double SumAssuredMin { get; set; }
    public double SumAssuredMax { get; set; }

    public string RiderProductID { get; set; }
    public string CoverPeriodType { get; set; }
    public bool Status { get; set; }
    public double[] BasicSumAssuredRange { get; set; }
    public double[] RiderSumAssuredRange { get; set; }
    //public int[] PayMode { get; set; }
    public List<PayModeObject> PayMode { get; set; }
    public string BusinessType { get; set; }
    public bool AllowRefer { get; set; }
    public bool IsRequiredRider { get; set; }
    public string MarketingName { get; set; }
    public string ChannelItemId { get; set; }
    /// <summary>
    /// This value use to check on referral id field in application registration 
    /// </summary>
    public bool IsValidateReferralId { get; set; }
    public string Remarks { get; set; }
    public string ChannelItemName
    {
        get
        {

            return da_channel.GetChannelItemNameByID(ChannelItemId);
        }
    }

    /// <summary>
    /// Return from Double array to string
    /// </summary>
    public string BasicSumAssured
    {
        get { return string.Join(",", BasicSumAssuredRange); }
    }
    /// <summary>
    /// Return from Double array to string
    /// </summary>
    public string RiderSumAssured
    {
        get { return string.Join(",", RiderSumAssuredRange); }
    }
    /// <summary>
    /// Return from Integer array to string
    /// </summary>
    public string PayModeString
    {
        get { return string.Join(",", PayMode); }
    }
    public string ProductType => this._getProductType();

    private string _getProductType()
    {
        if (this.ProductId == null)
            return "";
        DataTable data = new DB().GetData(AppConfiguration.GetConnectionString(), $"SELECT a.Product_Type_ID, Product_Type FROM Ct_Product_Type a inner join ct_product b on a.Product_Type_ID=b.Product_Type_ID WHERE Product_ID='{this.ProductId}';");
        return data.Rows.Count > 0 ? data.Rows[0]["Product_Type"].ToString().ToUpper() : "";
    }
    public class PayModeObject
    {
        public int Id { get; set; }
        public string ModeEn { get; set; }
        public string ModeKh { get; set; }
    }
    public enum PERIOD_TYPE { Y, M, D, H }
    public enum BusinussTypeOption { INDIVIDUAL, BANCA_REFERRAL, BUNDLE, BANCA_COOPERATE }

    public enum PRODUCT_TYPE
    {
        ORDINARY,
        MORTGAGE,
        SAVINGS,
        GTLI,
        MICRO,
        MICRO_LOAN,
    }

    /// <summary>
    /// RETURN PROPERTY NAME
    /// </summary>
    public class NAME
    {

        public static string Product_ID { get { return "Product_ID"; } }
        public static string Plan_Block { get { return "Plan_Block"; } }
        public static string ChannelItemId { get { return "ChannelItemId"; } }
        public static string ChannelName { get { return "ChannelName"; } }
        public static string MarketingName { get { return "MarketingName"; } }
        public static string ProductRemarks { get { return "ProductIdRemarks"; } }

    }
}