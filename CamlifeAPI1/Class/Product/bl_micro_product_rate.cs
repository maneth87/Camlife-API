using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_micro_product_rate
/// </summary>
public class bl_micro_product_rate
{
	public bl_micro_product_rate()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string PRODUCT_RATE_ID { get; set; }
    public string PRODUCT_ID { get; set; }
    public Int32 GENDER { get; set; }
    public Int32 PAY_MODE { get; set; }
    public Int32 AGE_MIN { get; set; }
    public Int32 AGE_MAX{get;set;}
    public double SUM_ASSURE_START{get;set;}
    public double SUM_ASSURE_END { get; set; }
    public double RATE_PER { get; set; }
    public string RATE_TYPE{get;set;}
    public double RATE { get; set; }
    public string REMARKS { get; set; }
    public string CREATED_BY { get; set; }
    public DateTime CREATED_ON { get; set; }
    /// <summary>
    /// Return payment mode name
    /// </summary>
    public string PAY_MODE_STRING { get { return Helper.GetPaymentModeEnglish(PAY_MODE); } }
    public string UPDATED_BY { get; set; }
    public DateTime UPDATED_ON { get; set; }
    public string UPDATED_REMARKS { get; set; }

   
}