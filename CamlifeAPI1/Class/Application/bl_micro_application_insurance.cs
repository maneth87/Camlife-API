using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_micro_application_insurance
/// </summary>
public class bl_micro_application_insurance
{
	public bl_micro_application_insurance()
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
    private string GetID()
    {
        string id = "";
        id = Helper.GetNewGuid(new string[,] { { "TABLE", "CT_MICRO_APPLICATION_INSURANCE" }, { "FIELD", "ID" } });
        return id;
    }
    private string _ID = "";
    public string ID
    {
        get { return _ID; }
        set { _ID = value; }
    }
    public string APPLICATION_NUMBER { get; set; }
    public string PRODUCT_ID { get; set; }
    public string COVER_TYPE { get; set; }
    public int TERME_OF_COVER { get; set; }
    public int PAYMENT_PERIOD { get; set; }
    public double SUM_ASSURE { get; set; }
    public int PAY_MODE { get; set; }
    public string PAYMENT_CODE { get; set; }
    public double PREMIUM { get; set; }
    public double ANNUAL_PREMIUM { get; set; }
    public double TOTAL_AMOUNT { get; set; }
    public double USER_PREMIUM { get; set; }
    public string PACKAGE { get; set; }
    public string CREATED_BY { get; set; }
    public DateTime CREATED_ON { get; set; }
    public string UPDATED_BY { get; set; }
    public DateTime UPDATED_ON { get; set; }
    public double DISCOUNT_AMOUNT { get; set; }
    public string REMARKS { get; set; }

}