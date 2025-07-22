using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_micro_application_customer
/// </summary>
public class bl_micro_application_customer
{
	public bl_micro_application_customer()
	{
		//
		// TODO: Add constructor logic here
		//
        _ID = GetID();

        if (PHONE_NUMBER2 == null)
        {
            PHONE_NUMBER2 = "";
        }
        if (PHONE_NUMBER3 == null)
        {
            PHONE_NUMBER3 = "";
        }
        if (EMAIL1 == null)
        {
            EMAIL1 = "";
        }
        if (EMAIL2 == null)
        {
            EMAIL2 = "";
        }
        if (EMAIL3 == null)
        {
            EMAIL3= "";
        }
        if (REMARKS == null)
        {
            REMARKS = "";
        }
        if (PLACE_OF_BIRTH == null)
        {
            PLACE_OF_BIRTH = "";
        }
	}
    private string GetID()
    {
        string id = "";
        id = Helper.GetNewGuid(new string[,] { { "TABLE", "CT_MICRO_APPLICATION_CUSTOMER" }, { "FIELD", "CUSTOMER_ID" } });
        return id;
    }
    private string _ID = "";
    public string CUSTOMER_ID
    {
        get { return _ID; }
        set { _ID = value; }
    }
    public Int32 SEQ { get; set; }
    public string CUSTOMER_NUMBER { get; set; }
    public int ID_TYPE { get; set; }
    public string ID_NUMBER { get; set; }
    public string FIRST_NAME_IN_ENGLISH { get; set; }
    public string LAST_NAME_IN_ENGLISH { get; set; }
    public string FIRST_NAME_IN_KHMER { get; set; }
    public string LAST_NAME_IN_KHMER { get; set; }
    public int GENDER { get; set; }
    public DateTime DATE_OF_BIRTH { get; set; }
    public string NATIONALITY { get; set; }
    public string MARITAL_STATUS { get; set; }
    public string OCCUPATION { get; set; }
    public string PLACE_OF_BIRTH { get; set; }
    public string HOUSE_NO_KH { get; set; }
    public string STREET_NO_KH { get; set; }
    public string VILLAGE_KH { get; set; }
    public string COMMUNE_KH { get; set; }
    public string DISTRICT_KH { get; set; }
    public  string PROVINCE_KH{get;set;}
    public string HOUSE_NO_EN{get; set; }
    public string STREET_NO_EN { get; set; }
    public string VILLAGE_EN { get; set; }
    public string COMMUNE_EN { get; set; }
    public string DISTRICT_EN { get; set; }
    public string PROVINCE_EN { get; set; }
    public string PHONE_NUMBER1 { get; set; }
    public string PHONE_NUMBER2 { get; set; }
    public string PHONE_NUMBER3 { get; set; }
    public string EMAIL1 { get; set; }
    public string EMAIL2 { get; set; }
    public string EMAIL3 { get; set; }
    public string CREATED_BY { get; set; }
    public DateTime CREATED_ON { get; set; }
    public string UPDATED_BY { get; set; }
    public DateTime UPDATED_ON { get; set; }
    public string REMARKS { get; set; }
    public int STATUS { get; set; }

}