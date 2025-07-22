using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for bl_micro_application
/// </summary>
public class bl_micro_application
{
	public bl_micro_application()
	{
		//
		// TODO: Add constructor logic here
		//

        _LAST_SEQ = GetLastSEQ();
        _ID = GetID();

        if (REFERRER == null)
            REFERRER = "";
        
        if (REFERRER_ID == null)
            REFERRER_ID = "";
        if (RENEW_FROM_POLICY == null)
            RENEW_FROM_POLICY = "";
        if (MainApplicationNumber == null)
            MainApplicationNumber = "";
	}
    private int _LAST_SEQ = 0;
    private string _LAST_APPLICATION_NUMBER = "";
    private string _LAST_PREFIX = "";
    private string _ID = "";
    public string APPLICATION_ID {
        get { return  _ID; }
        set { _ID = value; }
    }
    public int SEQ { get; set; }
    public string APPLICATION_NUMBER { get; set; }
    public DateTime APPLICATION_DATE { get; set; }
    public string CHANNEL_ID { get; set; }
    public string CHANNEL_ITEM_ID { get; set; }
    public string CHANNEL_LOCATION_ID { get; set; }
    public string SALE_AGENT_ID { get; set; }
    public string APPLICATION_CUSTOMER_ID { get; set; }
    public string CREATED_BY { get; set; }
    public DateTime CREATED_ON { get; set; }
    public string UPDATED_BY { get; set; }
    public DateTime UPDATED_ON { get; set; }
    public string REMARKS { get; set; }
    public string REFERRER_ID { get; set; }
    public string REFERRER { get; set; }
    public string RENEW_FROM_POLICY { get; set; }
    public int LAST_SEQ { get { return _LAST_SEQ; } }
    public string LAST_APPLICATION_NO { get { return _LAST_APPLICATION_NUMBER; } }
    public string LAST_PREFIX { get { return _LAST_PREFIX; } }
    public string CLIENT_TYPE { get; set; }
    public string CLIENT_TYPE_REMARKS { get; set; }
    public string CLIENT_TYPE_RELATION { get; set; }

    public string MainApplicationNumber { get; set; }
   public int NumbersOfPurchasingYear { get; set; }
    public int NumbersOfApplicationFirstYear { get; set; }
    public string LoanNumber { get; set; }
    public string PolicyholderName { get; set; }

    public int PolicyholderGender { get; set; }

    public DateTime PolicyholderDOB { get; set; }

    public int PolicyholderIDType { get; set; }

    public string PolicyholderIDNo { get; set; }

    public string PolicyholderPhoneNumber { get; set; }

    public string PolicyholderPhoneNumber2 { get; set; }

    public string PolicyholderEmail { get; set; }

    public string PolicyholderAddress { get; set; }
    private int GetLastSEQ()
    {
        int seq = 0;
        DB db = new DB();
        DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_GET_LAST_SEQ", new string[,] { }, "bl_micro_application => GetLastSEQ()");
        if (db.RowEffect == -1)//error
        {
            seq = -1;
        }
        else
        {
            if (tbl.Rows.Count > 0)
            {
                seq = Convert.ToInt32(tbl.Rows[0]["seq"].ToString());
                _LAST_APPLICATION_NUMBER = tbl.Rows[0]["application_number"].ToString();
                _LAST_PREFIX = _LAST_APPLICATION_NUMBER.Substring(3, 2);
            }
            else
            {
                seq = 0;
                _LAST_APPLICATION_NUMBER = "";
                _LAST_PREFIX = "";
            }
        }
        return seq ;
    }

    private string GetID()
    {
        string id = "";
        id = Helper.GetNewGuid(new string[,] { { "TABLE", "CT_MICRO_APPLICATION" }, { "FIELD", "APPLICATION_ID" } });
        return id;
    }
}