using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_micro_policy_detail
/// </summary>
public class bl_micro_policy_detail
{
	public bl_micro_policy_detail()
	{
		//
		// TODO: Add constructor logic here
		//
        _ID = GetID();

        if (DISCOUNT_AMOUNT == null)
        {
            DISCOUNT_AMOUNT = 0;
        }
        if (REFERRAL_FEE == null)
        {
            REFERRAL_FEE = 0;
        }
        if (REFERRAL_INCENTIVE == null)
        {
            REFERRAL_INCENTIVE = 0;
        }
        if (UPDATED_BY == null)
        {
            UPDATED_BY = "";
        }
        if (UPDATED_ON == null)
        {
            UPDATED_ON = new DateTime(1900, 1, 1);
        }
        if (REMARKS == null)
        {
            REMARKS = "";
        }
        if (PAY_MODE != null)
        {
            _PAY_MODE_TEXT = Helper.GetPaymentModeEnglish(PAY_MODE);
        }
	}

    private string _PAY_MODE_TEXT = "";
    
    public string POLICY_DETAIL_ID {
        get { return _ID; }
        set { _ID = value; }
    }
    public string POLICY_ID { get; set; }
    public DateTime EFFECTIVE_DATE { get; set; }
    public DateTime MATURITY_DATE { get; set; }
    public DateTime EXPIRY_DATE { get; set; }
    public DateTime ISSUED_DATE { get; set; }
    public Int32 AGE { get; set; }
    public string CURRANCY { get; set; }
    public double SUM_ASSURE { get; set; }
    public Int32 PAY_MODE { get; set; }
    /// <summary>
    /// Return Pay mode as text, Ex: Monthly, Annually,..
    /// </summary>
    public string PAY_MODE_TEXT {

        get {
            return _PAY_MODE_TEXT;
        }
    }
    public string PAYMENT_CODE { get; set; }
    
    public double PREMIUM { get; set; }
    public double ANNUAL_PREMIUM { get; set; }
    public double DISCOUNT_AMOUNT { get; set; }
    public double TOTAL_AMOUNT { get; set; }
    public double REFERRAL_FEE { get; set; }
    public double REFERRAL_INCENTIVE { get; set; }
    public Int32 COVER_YEAR { get; set; }
    public Int32 PAY_YEAR { get; set; }
    public Int32 COVER_UP_TO_AGE { get; set; }
    public Int32 PAY_UP_TO_AGE { get; set; }
    public bl_micro_product_config.PERIOD_TYPE COVER_TYPE { get; set; }
    /// <summary>
    /// Policy status NEW or RENEW
    /// </summary>
    public string POLICY_STATUS_REMARKS { get; set; }
    public string CREATED_BY { get; set; }
    public DateTime CREATED_ON { get; set; }
    public string UPDATED_BY { get; set; }
    public DateTime UPDATED_ON { get; set; }
    public string REMARKS { get; set; }
    private static DB db = new DB();
    private string _ID = "";
    
    private string GetID()
    {
        string id = "";
        id = Helper.GetNewGuid(new string[,] { { "TABLE", "CT_MICRO_POLICY_DETAIL" }, { "FIELD", "POLICY_DETAIL_ID" } });
        return id;
    }
}