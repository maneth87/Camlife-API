using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for bl_micro_customer
/// </summary>
public class bl_micro_customer
{
	public bl_micro_customer()
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
            EMAIL3 = "";
        }
        if (REMARKS == null)
        {
            REMARKS = "";
        }
        if (PLACE_OF_BIRTH == null)
        {
            PLACE_OF_BIRTH = "";
        }
        _LAST_SEQ = GetLastSEQ();
	}
    private string GetID()
    {
        string id = "";
        id = Helper.GetNewGuid(new string[,] { { "TABLE", "CT_MICRO_CUSTOMER1" }, { "FIELD", "ID" } });
        return id;
    }
    private string _ID = "";
    private Int32 _LAST_SEQ = 0;
    private string _LAST_PREFIX = "";
    private string _LAST_CUSTOMER_NO = "";
    private DB db = new DB();
    public string ID
    {
        get { return _ID; }
        set { _ID = value; }
    }
    public Int32 SEQ { get; set; }
    public string CUSTOMER_NUMBER { get; set; }
    public string CUSTOMER_TYPE { get; set; }
    public string ID_TYPE { get; set; }
    public string ID_NUMBER { get; set; }
    public string FIRST_NAME_IN_ENGLISH { get; set; }
    public string LAST_NAME_IN_ENGLISH { get; set; }
    public string FIRST_NAME_IN_KHMER { get; set; }
    public string LAST_NAME_IN_KHMER { get; set; }
    public string GENDER { get; set; }
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
    public string PROVINCE_KH { get; set; }
    public string HOUSE_NO_EN { get; set; }
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
    public int LAST_SEQ { get { return _LAST_SEQ; } }
    public string LAST_PREFIX { get { return _LAST_PREFIX; } }
    public string LAST_CUSTOMER_NUMBER { get { return _LAST_CUSTOMER_NO; } }
    public string FullNameKh { get { return GetFullNameKH(); } }
    public string FullNameEn { get { return GetFullNameEn(); } }

    private string GetFullNameKH()
    {
        return LAST_NAME_IN_KHMER + " " + FIRST_NAME_IN_KHMER;
    }
    private string GetFullNameEn()
    {
        return LAST_NAME_IN_ENGLISH + " " + FIRST_NAME_IN_ENGLISH;
    }
    private int GetLastSEQ()
    {
        int seq = 0;

        DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_CUSTOMER1_GET_LAST_SEQ", new string[,] { }, "bl_micro_customer1 => GetLastSEQ()");
        if (db.RowEffect == -1)//error
        {
            seq = -1;
        }
        else
        {
            if (tbl.Rows.Count > 0)
            {
                seq = Convert.ToInt32(tbl.Rows[0]["seq"].ToString());
                _LAST_CUSTOMER_NO = tbl.Rows[0]["customer_number"].ToString();
                _LAST_PREFIX = _LAST_CUSTOMER_NO.Substring(4, 2);
            }
            else
            {
                seq = 0;
                _LAST_CUSTOMER_NO = "";
                _LAST_PREFIX = "";
            }
        }
        return seq ;
    }
}