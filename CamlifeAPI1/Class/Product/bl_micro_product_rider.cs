using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_micro_product_rider
/// </summary>
public class bl_micro_product_rider
{
	public bl_micro_product_rider()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string PRODUCT_MICRO_RIDER_ID { get; set; }
    public string PRODUCT_ID { get; set; }
    public string EN_TITLE { get; set; }
    public string EN_ABBR { get; set; }
    public string KH_TITLE { get; set; }
    public Int32 AGE_MIN { get; set; }
    public Int32 AGE_MAX { get; set; }
    public Int32 SUM_ASSURE_MIX { get; set; }
    public Int32 SUM_ASSURE_MIN { get; set; }
    public string REMARKS { get; set; }
    public string CREATED_BY { get; set; }
    public DateTime CREATED_ON { get; set; }
    public string UPDATED_BY { get; set; }
    public DateTime UPDATED_ON { get; set; }
    public string UPDATED_REMARKS { get; set; }
    /// <summary>
    /// Return product id + remarks
    /// </summary>
    public string PRODUCT_ID_REMARKS { get { return PRODUCT_ID + " " + REMARKS; } }
    /// <summary>
    /// Return Property name
    /// </summary>
    public class Name
    {
        public static string PRODUCT_MICRO_RIDER_ID { get { return "PRODUCT_MICRO_RIDER_ID"; } }
        public static string PRODUCT_ID { get { return "PRODUCT_ID"; } }
        public static string EN_TITLE { get { return "EN_TITLE"; } }
        public static string EN_ABBR { get { return "EN_ABBR"; } }
        public static string KH_TITLE { get { return "KH_TITLE"; } }
        public static string AGE_MIN { get { return "AGE_MIN"; } }
        public static string AGE_MAX { get { return "AGE_MAX"; } }
        public static string SUM_ASSURE_MAX { get { return "SUM_ASSURE_MIX"; } }
        public static string SUM_ASSURE_MIN { get { return "SUM_ASSURE_MIN"; } }
        public static string REMARKS { get { return "REMARKS"; } }
        public static string CREATED_BY { get { return "CREATED_BY"; } }
        public static string CREATED_ON { get { return "CREATED_ON"; } }
        public static string UPDATED_BY { get { return "UPDATED_BY"; } }
        public static string UPDATED_ON { get { return "UPDATED_ON"; } }
        public static string UPDATED_REMARKS { get { return "UPDATED_REMARKS"; } }
        /// <summary>
        /// Product id + remarks
        /// </summary>
        public static string PRODUCT_ID_REMARKS { get { return "PRODUCT_ID_REMARKS"; } }
    }
}