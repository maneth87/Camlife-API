using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_micro_application_insurance_rider
/// </summary>
public class bl_micro_application_insurance_rider
{
	public bl_micro_application_insurance_rider()
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
        id = Helper.GetNewGuid(new string[,] { { "TABLE", "CT_MICRO_APPLICATION_INSURANCE_RIDER" }, { "FIELD", "ID" } });
        return id;
    }

    public string ID
    {
        get { return _ID; }
        set { _ID = value; }
    }
    public string APPLICATION_NUMBER { get; set; }
    public string PRODUCT_ID { get; set; }
    public double SUM_ASSURE { get; set; }
    public double PREMIUM { get; set; }
    public double ANNUAL_PREMIUM { get; set; }
    public double DISCOUNT_AMOUNT { get; set; }
    public double TOTAL_AMOUNT { get; set; }
    public string CREATED_BY { get; set; }
    public DateTime CREATED_ON { get; set; }
    public string UPDATED_BY { get; set; }
    public DateTime UPDATED_ON { get; set; }
    public string REMARKS { get; set; }
}