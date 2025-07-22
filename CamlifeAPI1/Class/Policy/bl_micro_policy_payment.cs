using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_micro_policy_payment
/// </summary>
public class bl_micro_policy_payment
{
	public bl_micro_policy_payment()
	{
		//
		// TODO: Add constructor logic here
		//
        _ID = GetID();

        if (REMARKS == null)
        {
            REMARKS = "";
        }
	}
    private string _ID = "";

    private string GetID()
    {
        string id = "";
        id = Helper.GetNewGuid(new string[,] { { "TABLE", "CT_MICRO_POLICY_PAYMENT" }, { "FIELD", "POLICY_PAYMENT_ID" } });
        return id;
    }

    public string POLICY_PAYMENT_ID
    {
        get { return _ID; }
        set { _ID = value; }
    }
  
    public string POLICY_DETAIL_ID { get; set; }
    public DateTime DUE_DATE { get; set; }
    public DateTime PAY_DATE { get; set; }
    public DateTime NEXT_DUE { get; set; }
    public Int32 PAY_MODE { get; set; }
    public double USER_PREMIUM { get; set; }
    public double AMOUNT { get; set; }
    public double DISCOUNT_AMOUNT { get; set; }
    public double TOTAL_AMOUNT { get; set; }
    public Int32 PREMIUM_YEAR { get; set; }
    public Int32 PREMIUM_LOT { get; set; }
    public string POLICY_STATUS { get; set; }
    public string OFFICE_ID { get; set; }
    public string REFERANCE_TRANSACTION_CODE { get; set; }
    public double REFERRAL_FEE { get; set; }
    public double REFERRAL_INCENTIVE { get; set; }
    public string TRANSACTION_TYPE { get; set; }
    public string CREATED_BY { get; set; }
    public DateTime CREATED_ON { get; set; }
    public string UPDATED_BY { get; set; }
    public DateTime UPDATED_ON { get; set; }
    public string REMARKS { get; set; }
}