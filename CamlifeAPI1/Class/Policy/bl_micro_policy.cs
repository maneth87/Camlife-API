using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for bl_mirco_policy
/// </summary>
public class bl_micro_policy
{
    public bl_micro_policy()
    {
        //
        // TODO: Add constructor logic here
        //
        _ID = GetID();
        if (REMARKS == null)
        {
            REMARKS = "";
        }
        RenewFromPolicy = RenewFromPolicy == null ? "" : RenewFromPolicy;
        //_LAST_SEQ = GetLastSEQ();
    }
    public bl_micro_policy(string productId)
    {
        PRODUCT_ID = productId;
        _ID = GetID();
     // _LAST_SEQ=  this.GetLastSEQ();

        if (REMARKS == null)
        {
            REMARKS = "";
        }
        RenewFromPolicy = RenewFromPolicy == null ? "" : RenewFromPolicy;
    }
    private static DB db = new DB();
    private string _ID = "";
    private Int32 _LAST_SEQ = 0;
    private string _LAST_CERTIFICATE_NO = "";
    private string _LAST_PREFIX = "";
    private string _LAST_PREFIX1 = "";
    private string _ProductID = "";
    private string GetID()
    {
        string id = "";
        id = Helper.GetNewGuid(new string[,] { { "TABLE", "CT_MICRO_POLICY" }, { "FIELD", "POLICY_ID" } });
        return id;
    }

    private int GetLastSEQ()
    {
        int seq = 0;

        DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_GET_LAST_SEQ", new string[,] {
        { "@PRODUCT_ID", PRODUCT_ID}
        }, "bl_micro_policy => GetLastSEQ()");
        if (db.RowEffect == -1)//error
        {
            seq = -1;
        }
        else
        {
            if (tbl.Rows.Count > 0)
            {
                if (tbl.Rows[0]["POLICY_NUMBER"].ToString().Length <= 1) {
                    seq = 0;
                    _LAST_CERTIFICATE_NO = "";
                    _LAST_PREFIX = "";
                    _LAST_PREFIX1 = "";
                }
                else
                {
                    seq = Convert.ToInt32(tbl.Rows[0]["seq"].ToString());
                    _LAST_CERTIFICATE_NO = tbl.Rows[0]["POLICY_NUMBER"].ToString();
                    _LAST_PREFIX = _LAST_CERTIFICATE_NO.Substring(5, 2);
                    _LAST_PREFIX1 = _LAST_CERTIFICATE_NO.Substring(0, 5);
                }
                  
            }
            else
            {
                seq = 0;
                _LAST_CERTIFICATE_NO = "";
                _LAST_PREFIX = "";
                _LAST_PREFIX1 = "";
            }
        }
        return seq;
    }
    public string POLICY_ID
    {
        get { return _ID; }
        set { _ID = value; }
    }
    public string POLICY_TYPE { get; set; }
    public string APPLICATION_ID { get; set; }
    public Int32 SEQ { get; set; }
    public string POLICY_NUMBER { get; set; }
    public string CUSTOMER_ID { get; set; }
    public string PRODUCT_ID { get { return _ProductID; } set { _ProductID = value; _LAST_SEQ= this.GetLastSEQ(); } }
    public string AGENT_CODE { get; set; }
    public string CHANNEL_ID { get; set; }
    public string CHANNEL_ITEM_ID { get; set; }
    public string CHANNEL_LOCATION_ID { get; set; }
    public string POLICY_STATUS { get; set; }
    public string CREATED_BY { get; set; }
    public DateTime CREATED_ON { get; set; }
    public string UPDATED_BY { get; set; }
    public DateTime UPDATED_ON { get; set; }
    public string REMARKS { get; set; }

    public Int32 LAST_SEQ { get { return _LAST_SEQ; } }
    public string LAST_PREFIX { get { return _LAST_PREFIX; } }
    public string LAST_PREFIX1 { get { return _LAST_PREFIX1; } }
    public string LAST_CERTIFICATE_NUMBER { get { return _LAST_CERTIFICATE_NO; } }
    //add new properties by kehong
    public DateTime POLICY_STATUS_DATE { get; set; }
    public string POLICY_STATUS_REMARKS { get; set; }
    public string RenewFromPolicy { get; set; }




}