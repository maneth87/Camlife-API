using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_address
/// </summary>
public class bl_address
{
	public bl_address()
	{
		//
		// TODO: Add constructor logic here
		//
        if (ADDRESS2 == null)
            ADDRESS2 = "";
        if (ADDRESS3 == null)
            ADDRESS3 = "";
        if (REMARKS == null)
            REMARKS = "";
	}
    public class province
    {
        public string Code { get; set; }
        public string Khmer { get; set; }
        public string English { get; set; }
    }
    public class district
    {
        public string ProvinceCode { get; set; }
        public string Code { get; set; }
        public string Khmer { get; set; }
        public string English { get; set; }
        
    }
    public class commune
    {
        public string DistrictCode { get; set; }
        public string Code { get; set; }
        public string Khmer { get; set; }
        public string English { get; set; }
    }
    public class village
    {
        public string CommuneCode { get; set; }
        public string Code { get; set; }
        public string Khmer { get; set; }
        public string English { get; set; }
    }

    public string ADDRESS_ID { get; set; }
    public string CUSTOMER_ID { get; set; }
    public string ADDRESS1 { get; set; }
    public string ADDRESS2 { get; set; }
    public string ADDRESS3 { get; set; }
    public string CREATED_BY { get; set; }
    public DateTime CREATED_ON { get; set; }
    public string UPDATED_BY { get; set; }
    public DateTime UPDATED_ON { get; set; }
    public string REMARKS { get; set; }

    
}