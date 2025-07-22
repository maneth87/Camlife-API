using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using iTextSharp.text.pdf;
/// <summary>
/// Summary description for da_micro_policy
/// </summary>
public class da_micro_policy
{
    public da_micro_policy()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private static bool _SUCCESS = false;
    private static string _MESSAGE = "";
    private static string MYNAME = "da_micro_policy";
    public static bool SUCCESS { get { return _SUCCESS; } }
    public static string MESSAGE { get { return _MESSAGE; } }
    private static DB db = new DB();

    public static bool SavePolicy(bl_micro_policy POLICY)
    {
        bool result = false;
        try
        {
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_INSERT", new string[,] {
            {"@POLICY_ID",POLICY.POLICY_ID},
            {"@APPLICATION_ID", POLICY.APPLICATION_ID},
            {"@POLICY_TYPE", POLICY.POLICY_TYPE},
            {"@SEQ", POLICY.SEQ+""},
            {"@POLICY_NUMBER", POLICY.POLICY_NUMBER},
            {"@CUSTOMER_ID", POLICY.CUSTOMER_ID},
            {"@PRODUCT_ID", POLICY.PRODUCT_ID},
            {"@AGENT_CODE", POLICY.AGENT_CODE},
            {"@CHANNEL_ID", POLICY.CHANNEL_ID},
            {"@CHANNEL_ITEM_ID", POLICY.CHANNEL_ITEM_ID},
            {"@CHANNEL_LOCATION_ID", POLICY.CHANNEL_LOCATION_ID},
            {"@POLICY_STATUS", POLICY.POLICY_STATUS},
            {"@CREATED_BY", POLICY.CREATED_BY},
            {"@CREATED_ON", POLICY.CREATED_ON+""},
            {"@REMARKS", POLICY.REMARKS},
            {"@RENEW_FROM_POLICY", POLICY.RenewFromPolicy}
            }, "da_micro_policy=>SavePolicy(bl_micro_policy POLICY)");
            if (db.RowEffect == -1) //error
            {
                _MESSAGE = db.Message;
            }
            else
            {
                _MESSAGE = "Success";
            }
            _SUCCESS = result;
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [SavePolicy(bl_micro_policy POLICY)] in class [da_micro_policy], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }
    public static bool UpdatePolicy(bl_micro_policy POLICY)
    {
        bool result = false;
        try
        {
            db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_UPDATE", new string[,] {
            {"@POLICY_ID",POLICY.POLICY_ID},
            {"@APPLICATION_ID", POLICY.APPLICATION_ID},
            //{"@POLICY_TYPE", POLICY.POLICY_TYPE},
            //{"@POLICY_NUMBER", POLICY.POLICY_NUMBER},
            {"@CUSTOMER_ID", POLICY.CUSTOMER_ID},
            {"@PRODUCT_ID", POLICY.PRODUCT_ID},
            {"@AGENT_CODE", POLICY.AGENT_CODE},
            {"@CHANNEL_ID", POLICY.CHANNEL_ID},
            {"@CHANNEL_ITEM_ID", POLICY.CHANNEL_ITEM_ID},
            {"@CHANNEL_LOCATION_ID", POLICY.CHANNEL_LOCATION_ID},
            {"@POLICY_STATUS", POLICY.POLICY_STATUS},
            {"@UPDATED_BY", POLICY.UPDATED_BY},
            {"@UPDATED_ON", POLICY.UPDATED_ON+""},
            {"@REMARKS", POLICY.REMARKS}
            }, "da_micro_policy=>UpdatePolicy(bl_micro_policy POLICY)");
            if (db.RowEffect == -1) //error
            {
                _MESSAGE = db.Message;
            }
            else
            {
                _MESSAGE = "Success";
            }
            _SUCCESS = result;
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [UpdatePolicy(bl_micro_policy POLICY)] in class [da_micro_policy], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="policyNumber"></param>
    /// <param name="tranType">[UPDATE, DELETE]</param>
    /// <param name="tranBy">[UER NAME WHO DOES TRANSACTION]</param>
    /// <param name="tranDate">[DATE OF TRANSACTION]</param>
    /// <returns></returns>
    public static bool BackupPolicy(string policyNumber, string tranType, string tranBy, DateTime tranDate)
    {
        bool result = false;
        try
        {
            db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_MICRO_POLICY_BACKUP", new string[,] {
            {"@POLICY_NUMBER",policyNumber},
            {"@TRAN_TYPE", tranType},
            {"@TRAN_BY", tranBy},
            {"@TRAN_DATE", tranDate+""}
            }, "da_micro_policy=>BackupPolicy(string policyNumber, string tranType,string tranBy, DateTime tranDate)");
            if (db.RowEffect == -1) //error
            {
                _MESSAGE = db.Message;
            }
            else
            {
                _MESSAGE = "Success";
            }
            _SUCCESS = result;
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [BackupPolicy(string policyNumber, string tranType,string tranBy, DateTime tranDate)] in class [da_micro_policy], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="policyNumber"></param>
    /// <param name="tranBy">[USER NAME WHO DID BACKUP]</param>
    /// <param name="tranDate">[DATE OF BACKUP]</param>
    /// <returns></returns>
    public static bool RestorePolicy(string policyNumber, string tranBy, DateTime tranDate)
    {
        bool result = false;
        try
        {
            db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_MICRO_POLICY_RESTORE", new string[,] {
            {"@POLICY_NUMBER",policyNumber},
            {"@TRAN_BY", tranBy},
            {"@TRAN_DATE", tranDate+""}
            }, "da_micro_policy=>RestorePolicy(string policyNumber,  string tranBy, DateTime tranDate)");
            if (db.RowEffect == -1) //error
            {
                _MESSAGE = db.Message;
            }
            else
            {
                _MESSAGE = "Success";
            }
            _SUCCESS = result;
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [RestorePolicy(string policyNumber,  string tranBy, DateTime tranDate)] in class [da_micro_policy], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="policyNumber"></param>
    /// <param name="tranBy">[USER NAME WHO DID BACKUP]</param>
    /// <param name="tranDate">[DATE OF BACKUP]</param>
    /// <returns></returns>
    public static bool DeleteBackupPolicy(string policyNumber, string tranBy, DateTime tranDate)
    {
        bool result = false;
        try
        {
            db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_MICRO_POLICY_BACKUP_DELETE", new string[,] {
            {"@POLICY_NUMBER",policyNumber},
            {"@TRAN_BY", tranBy},
            {"@TRAN_DATE", tranDate+""}
            }, "da_micro_policy=>DeleteBackupPolicy(string policyNumber, string tranBy, DateTime tranDate)");
            if (db.RowEffect == -1) //error
            {
                _MESSAGE = db.Message;
            }
            else
            {
                _MESSAGE = "Success";
            }
            _SUCCESS = result;
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [DeleteBackupPolicy(string policyNumber, string tranBy, DateTime tranDate)] in class [da_micro_policy], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }

    public static bl_micro_policy GetPolicyByApplicationID(string APPLICAITON_ID)
    {
        bl_micro_policy pol = new bl_micro_policy();
        try
        {
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_GET_BY_APPLICATION_ID", new string[,] {
            {"@APPLICATION_ID", APPLICAITON_ID}
            }, "da_micro_policy=>GetPolicyByApplicationID(string APPLICAITON_ID)");
            if (db.RowEffect == -1)
            {
                _SUCCESS = false;
                _MESSAGE = db.Message;

            }
            else
            {
                if (tbl.Rows.Count == 0)
                {
                    _SUCCESS = true;
                    _MESSAGE = "Record not found.";
                }
                else
                {
                    var r = tbl.Rows[0];
                    pol = new bl_micro_policy()
                    {
                        POLICY_ID = r["policy_id"].ToString(),
                        SEQ = Convert.ToInt32(r["seq"].ToString()),
                        POLICY_NUMBER = r["policy_number"].ToString(),
                        POLICY_TYPE = r["policy_type"].ToString(),
                        CUSTOMER_ID = r["customer_id"].ToString(),
                        PRODUCT_ID = r["product_id"].ToString(),
                        AGENT_CODE = r["agent_code"].ToString(),
                        CHANNEL_ID = r["channel_id"].ToString(),
                        CHANNEL_ITEM_ID = r["channel_item_id"].ToString(),
                        CHANNEL_LOCATION_ID = r["channel_location_id"].ToString(),
                        APPLICATION_ID = r["application_id"].ToString(),
                        POLICY_STATUS = r["policy_status"].ToString(),
                        REMARKS = r["remarks"].ToString(),
                        CREATED_BY = r["created_by"].ToString(),
                        CREATED_ON = Convert.ToDateTime(r["created_on"].ToString()),
                        UPDATED_BY = r["updated_by"].ToString(),
                        UPDATED_ON = Convert.ToDateTime(r["updated_on"].ToString()),
                        RenewFromPolicy = r["renew_from_policy"].ToString()

                    };
                    _SUCCESS = true;
                    _MESSAGE = "Success";
                }
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            pol = new bl_micro_policy();
            Log.AddExceptionToLog("Error function [GetPolicyByApplicationID(string APPLICAITON_ID)] in class [da_micro_policy], detail: " + ex.Message + "==>" + ex.StackTrace);

        }
        return pol;
    }
    public static bl_micro_policy GetPolicyByID(string policyId, string userName = "")
    {
        bl_micro_policy pol = new bl_micro_policy();
        try
        {
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_GET_BY_ID", new string[,] {
            {"@POLICY_ID", policyId}
            }, "da_micro_policy=>GetPolicyByID(string policyId)");
            if (db.RowEffect == -1)
            {
                _SUCCESS = false;
                _MESSAGE = db.Message;

            }
            else
            {
                if (tbl.Rows.Count == 0)
                {
                    _SUCCESS = true;
                    _MESSAGE = "Record not found.";
                }
                else
                {
                    var r = tbl.Rows[0];
                    pol = new bl_micro_policy()
                    {
                        POLICY_ID = r["policy_id"].ToString(),
                        SEQ = Convert.ToInt32(r["seq"].ToString()),
                        POLICY_NUMBER = r["policy_number"].ToString(),
                        POLICY_TYPE = r["policy_type"].ToString(),
                        CUSTOMER_ID = r["customer_id"].ToString(),
                        PRODUCT_ID = r["product_id"].ToString(),
                        AGENT_CODE = r["agent_code"].ToString(),
                        CHANNEL_ID = r["channel_id"].ToString(),
                        CHANNEL_ITEM_ID = r["channel_item_id"].ToString(),
                        CHANNEL_LOCATION_ID = r["channel_location_id"].ToString(),
                        APPLICATION_ID = r["application_id"].ToString(),
                        POLICY_STATUS = r["policy_status"].ToString(),
                        REMARKS = r["remarks"].ToString(),
                        CREATED_BY = r["created_by"].ToString(),
                        CREATED_ON = Convert.ToDateTime(r["created_on"].ToString()),
                        UPDATED_BY = r["updated_by"].ToString(),
                        UPDATED_ON = Convert.ToDateTime(r["updated_on"].ToString())

                    };

                }
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            pol = new bl_micro_policy();
       
            Log.AddExceptionToLog("micro_policy_exipring", "GetPolicyByID(string policyId, string userName = \"\")", ex);

        }
        return pol;
    }
    public static bl_micro_policy GetPolicyByNumber(string policyNumber, string userName = "")
    {
        bl_micro_policy pol = new bl_micro_policy();
        try
        {
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_GET_BY_POLICY_NUMBER", new string[,] {
            {"@POLICY_NUMBER", policyNumber}
            }, "da_micro_policy=>GetPolicyByNumber(string policyId)");
            if (db.RowEffect == -1)
            {
                _SUCCESS = false;
                _MESSAGE = db.Message;

            }
            else
            {
                if (tbl.Rows.Count == 0)
                {
                    _SUCCESS = true;
                    _MESSAGE = "Record not found.";
                }
                else
                {
                    var r = tbl.Rows[0];
                    pol = new bl_micro_policy()
                    {
                        POLICY_ID = r["policy_id"].ToString(),
                        SEQ = Convert.ToInt32(r["seq"].ToString()),
                        POLICY_NUMBER = r["policy_number"].ToString(),
                        POLICY_TYPE = r["policy_type"].ToString(),
                        CUSTOMER_ID = r["customer_id"].ToString(),
                        PRODUCT_ID = r["product_id"].ToString(),
                        AGENT_CODE = r["agent_code"].ToString(),
                        CHANNEL_ID = r["channel_id"].ToString(),
                        CHANNEL_ITEM_ID = r["channel_item_id"].ToString(),
                        CHANNEL_LOCATION_ID = r["channel_location_id"].ToString(),
                        APPLICATION_ID = r["application_id"].ToString(),
                        POLICY_STATUS = r["policy_status"].ToString(),
                        REMARKS = r["remarks"].ToString(),
                        CREATED_BY = r["created_by"].ToString(),
                        CREATED_ON = Convert.ToDateTime(r["created_on"].ToString()),
                        UPDATED_BY = r["updated_by"].ToString(),
                        UPDATED_ON = Convert.ToDateTime(r["updated_on"].ToString()),
                        RenewFromPolicy=r["renew_from_policy"].ToString()
                    };
                    _SUCCESS = true;
                    _MESSAGE = "Success";
                }
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            pol = new bl_micro_policy();
         
            Log.AddExceptionToLog("micro_policy_exipring", "GetPolicyByNumber(string policyNumber, string userName = \"\")", ex);

        }
        return pol;
    }
    public static DataTable GetPolicyDetailByPolicyID(string POLICY_ID)
    {
        DataTable tbl = new DataTable();
        try
        {
            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_GET_POLICY_DETAIL_BY_POLICY_ID", new string[,] {
            {"@POLICY_ID",POLICY_ID}
            }, "da_micro_policy=>GetPolicyDetailByPolicyID(string POLICY_ID)");
            if (db.RowEffect == -1)
            {
                _SUCCESS = false;
                _MESSAGE = db.Message;
            }
            else
            {
                if (tbl.Rows.Count == 0)
                {
                    _SUCCESS = true;
                    _MESSAGE = "No Record Found";
                }
                else
                {
                    _SUCCESS = true;
                    _MESSAGE = "Success";
                }
            }
        }
        catch (Exception ex)
        {
            tbl = new DataTable();
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("Error function [GetPolicyDetailByPolicyID(string POLICY_ID)] in class [da_micro_policy], detail: " + ex.Message + "==>", ex.StackTrace);
        }

        return tbl;
    }
    #region
    /* created by :kehong, created_date:28/11/2022*/
    public static DataTable GetPolicyDetailByPolicyNumber(string policyNumber)
    {
        DataTable tbl = new DataTable();
        try
        {
            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_GET_POLICY_DETAIL_BY_POLICY_NUMBER", new string[,] {
            {"@POLICY_Number",policyNumber}
            }, "da_micro_policy=>GetPolicyDetailByPolicyNumber(string policyNumber)");
            if (db.RowEffect == -1)
            {
                _SUCCESS = false;
                _MESSAGE = db.Message;
            }
            else
            {
                if (tbl.Rows.Count == 0)
                {
                    _SUCCESS = true;
                    _MESSAGE = "No Record Found";
                }
                else
                {
                    _SUCCESS = true;
                    _MESSAGE = "Success";
                }
            }
        }
        catch (Exception ex)
        {
            tbl = new DataTable();
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("Error function [GetPolicyDetailByPolicyID(string POLICY_ID)] in class [da_micro_policy], detail: " + ex.Message + "==>", ex.StackTrace);
        }

        return tbl;
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="F_DATE">Start Issued Date</param>
    /// <param name="T_DATE">End Issued Date</param>
    /// <param name="CHANNEL_ID">To Skip this condition keep it blank</param>
    /// <param name="CHANNEL_ITEM_ID">To Skip this condition keep it blank</param>
    /// <param name="CHANNEL_LOCATION_ID">To Skip this condition keep it blank</param>
    /// <returns></returns>
    public static DataTable GetPolicyInsuranceReport(DateTime F_DATE, DateTime T_DATE, string CHANNEL_ID, string CHANNEL_ITEM_ID, string CHANNEL_LOCATION_ID)
    {
        DataTable tbl = new DataTable();
        try
        {
            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_GET_POLICY_INSURANCE", new string[,] {
            {"@F_DATE",F_DATE+""},
            {"@T_DATE", T_DATE+""},
            {"@CHANNEL_ID", @CHANNEL_ID},
            {"@CHANNEL_ITEM_ID", @CHANNEL_ITEM_ID},
            {"@CHANNEL_LOCATION_ID", @CHANNEL_LOCATION_ID}
            }, "da_micro_policy=>GetPolicyInsuranceReport(DateTime F_DATE, DateTime T_DATE, string CHANNEL_ID, string CHANNEL_ITEM_ID, string CHANNEL_LOCATION_ID)");
            if (db.RowEffect == -1)
            {
                _SUCCESS = false;
                _MESSAGE = db.Message;
            }
            else
            {
                if (tbl.Rows.Count == 0)
                {
                    _SUCCESS = true;
                    _MESSAGE = "No Record Found";
                }
                else
                {
                    _SUCCESS = true;
                    _MESSAGE = "Success";
                }
            }
        }
        catch (Exception ex)
        {
            tbl = new DataTable();
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("Error function [GetPolicyInsuranceReport(DateTime F_DATE, DateTime T_DATE, string CHANNEL_ID, string CHANNEL_ITEM_ID, string CHANNEL_LOCATION_ID)] in class [da_micro_policy], detail: " + ex.Message + "==>", ex.StackTrace);
        }

        return tbl;
    }
    /*modify by kehong *///get all policy status 
    public static DataTable GetPolicyInsuranceReportListing(DateTime F_DATE, DateTime T_DATE, string CHANNEL_ID, string CHANNEL_ITEM_ID, string CHANNEL_LOCATION_ID,string POLICY_STATUS)
    {
        DataTable tbl = new DataTable();
        try
        {
            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_GET_POLICY_LISTING_V1", new string[,] {
            {"@F_DATE",F_DATE+""},
            {"@T_DATE", T_DATE+""},
            {"@CHANNEL_ID", @CHANNEL_ID},
            {"@CHANNEL_ITEM_ID", @CHANNEL_ITEM_ID},
            {"@CHANNEL_LOCATION_ID", @CHANNEL_LOCATION_ID},
            {"@POLICY_STATUS",@POLICY_STATUS}
            }, "da_micro_policy=>GetPolicyInsuranceReport(DateTime F_DATE, DateTime T_DATE, string CHANNEL_ID, string CHANNEL_ITEM_ID, string CHANNEL_LOCATION_ID,string POLICY_STATUS)");
            if (db.RowEffect == -1)
            {
                _SUCCESS = false;
                _MESSAGE = db.Message;
            }
            else
            {
                if (tbl.Rows.Count == 0)
                {
                    _SUCCESS = true;
                    _MESSAGE = "No Record Found";
                }
                else
                {
                    _SUCCESS = true;
                    _MESSAGE = "Success";
                }
            }
        }
        catch (Exception ex)
        {
            tbl = new DataTable();
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("Error function [GetPolicyInsuranceReport(DateTime F_DATE, DateTime T_DATE, string CHANNEL_ID, string CHANNEL_ITEM_ID, string CHANNEL_LOCATION_ID,string POLICY_STATUS)] in class [da_micro_policy], detail: " + ex.Message + "==>", ex.StackTrace);
        }

        return tbl;
    }
    public static DataTable GetPolicyInsuranceSummaryReport(DateTime F_DATE, DateTime T_DATE, string CHANNEL_ID, string CHANNEL_ITEM_ID, string CHANNEL_LOCATION_ID)
    {
        DataTable tbl = new DataTable();
        try
        {
            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_GET_POLICY_INSURANCE_SUMMARY", new string[,] {
            {"@F_DATE",F_DATE+""},
            {"@T_DATE", T_DATE+""},
            {"@CHANNEL_ID", @CHANNEL_ID},
            {"@CHANNEL_ITEM_ID", @CHANNEL_ITEM_ID},
            {"@CHANNEL_LOCATION_ID", @CHANNEL_LOCATION_ID}
            }, "da_micro_policy=>GetPolicyInsuranceSummaryReport(DateTime F_DATE, DateTime T_DATE, string CHANNEL_ID, string CHANNEL_ITEM_ID, string CHANNEL_LOCATION_ID)");
            if (db.RowEffect == -1)
            {
                _SUCCESS = false;
                _MESSAGE = db.Message;
            }
            else
            {
                if (tbl.Rows.Count == 0)
                {
                    _SUCCESS = true;
                    _MESSAGE = "No Record Found";
                }
                else
                {
                    _SUCCESS = true;
                    _MESSAGE = "Success";
                }
            }
        }
        catch (Exception ex)
        {
            tbl = new DataTable();
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("Error function [GetPolicyInsuranceSummaryReport(DateTime F_DATE, DateTime T_DATE, string CHANNEL_ID, string CHANNEL_ITEM_ID, string CHANNEL_LOCATION_ID)] in class [da_micro_policy], detail: " + ex.Message + "==>", ex.StackTrace);
        }

        return tbl;
    }
    /*MODIFY BY KEHONG SUMARY POLICY BY SPECIFIC BY POLICY STATUS*/
    public static DataTable GetPolicyInsuranceSummaryReportV1(DateTime F_DATE, DateTime T_DATE, string CHANNEL_ID, string CHANNEL_ITEM_ID, string CHANNEL_LOCATION_ID,string POLICY_STATUS)
    {
        DataTable tbl = new DataTable();
        try
        {
            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_GET_POLICY_INSURANCE_SUMMARY_V1", new string[,] {
            {"@F_DATE",F_DATE+""},
            {"@T_DATE", T_DATE+""},
            {"@CHANNEL_ID", @CHANNEL_ID},
            {"@CHANNEL_ITEM_ID", @CHANNEL_ITEM_ID},
            {"@CHANNEL_LOCATION_ID", @CHANNEL_LOCATION_ID},
            {"@POLICY_STATUS",@POLICY_STATUS}
            }, "da_micro_policy=>GetPolicyInsuranceSummaryReportV1(DateTime F_DATE, DateTime T_DATE, string CHANNEL_ID, string CHANNEL_ITEM_ID, string CHANNEL_LOCATION_ID,string POLICY_STATUS)");
            if (db.RowEffect == -1)
            {
                _SUCCESS = false;
                _MESSAGE = "Get data error.";
            }
            else
            {
                if (tbl.Rows.Count == 0)
                {
                    _SUCCESS = true;
                    _MESSAGE = "No Record Found";
                }
                else
                {
                    _SUCCESS = true;
                    _MESSAGE = "Success";
                }
            }
        }
        catch (Exception ex)
        {
            tbl = new DataTable();
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("Error function [GetPolicyInsuranceSummaryReport(DateTime F_DATE, DateTime T_DATE, string CHANNEL_ID, string CHANNEL_ITEM_ID, string CHANNEL_LOCATION_ID,string POLICY_STATUS)] in class [da_micro_policy], detail: " + ex.Message + "==>", ex.StackTrace);
        }

        return tbl;
    }
    #region //UpdatePolicyStatus(bl_micro_policy POLICY)
    /*
     Crated by: kehong date:28/11/2022
     */
    public static bool UpdatePolicyStatus(bl_micro_policy POLICY)
    {
        bool result = false;
        try
        {
            db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_UPDATE_STATUS", new string[,] {
            {"@POLICY_ID",POLICY.POLICY_ID},
            {"@POLICY_STATUS", POLICY.POLICY_STATUS},
            {"@UPDATED_BY", POLICY.UPDATED_BY},
            {"@UPDATED_ON", POLICY.UPDATED_ON+""},
            {"@POLICY_STATUS_DATE", POLICY.POLICY_STATUS_DATE+""},
            {"@POLICY_STATUS_REMARKS", POLICY.POLICY_STATUS_REMARKS},
            }, "da_micro_policy=>UpdatePolicyStatus(bl_micro_policy POLICY)");
            if (db.RowEffect == -1) //error
            {
                _MESSAGE = db.Message;
            }
            else
            {
                _MESSAGE = "Success";
            }
            _SUCCESS = result;
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [UpdatePolicyStatus(bl_micro_policy POLICY)] in class [da_micro_policy], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }
    #endregion

    public static List<bl_micro_policy> GetPolicyByCustomerID(string customerId, string userName = "")
    {
        List<bl_micro_policy> polList = new List<bl_micro_policy>();
        bl_micro_policy pol = new bl_micro_policy();
        try
        {
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_GET_BY_CUSTOMER_ID", new string[,] {
            {"@customer_id",customerId}
            }, MYNAME + " => GetPolicyByCustomerID(string customerId, string userName)");
            if (db.RowEffect < 0)
            {
                //error
                _SUCCESS = false;
                _MESSAGE = db.Message;
            }
            else
            {

                _SUCCESS = true;

                _MESSAGE = tbl.Rows.Count + " record(s) found.";
                foreach (DataRow r in tbl.Rows)
                {
                    pol = new bl_micro_policy()
                    {
                        POLICY_ID = r["policy_id"].ToString(),
                        SEQ = Convert.ToInt32(r["seq"].ToString()),
                        POLICY_NUMBER = r["policy_number"].ToString(),
                        POLICY_TYPE = r["policy_type"].ToString(),
                        CUSTOMER_ID = r["customer_id"].ToString(),
                        PRODUCT_ID = r["product_id"].ToString(),
                        AGENT_CODE = r["agent_code"].ToString(),
                        CHANNEL_ID = r["channel_id"].ToString(),
                        CHANNEL_ITEM_ID = r["channel_item_id"].ToString(),
                        CHANNEL_LOCATION_ID = r["channel_location_id"].ToString(),
                        APPLICATION_ID = r["application_id"].ToString(),
                        POLICY_STATUS = r["policy_status"].ToString(),
                        REMARKS = r["remarks"].ToString(),
                        CREATED_BY = r["created_by"].ToString(),
                        CREATED_ON = Convert.ToDateTime(r["created_on"].ToString()),
                        UPDATED_BY = r["updated_by"].ToString(),
                        UPDATED_ON = Convert.ToDateTime(r["updated_on"].ToString())

                    };
                    polList.Add(pol);

                }
            }
        }
        catch (Exception ex)
        {
            polList = new List<bl_micro_policy>();
          
            Log.AddExceptionToLog(MYNAME, "GetPolicyByCustomerID(string customerId, string userName = \"\")", ex);
        }
        return polList;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idType"></param>
    /// <param name="idNumber"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    public static List<bl_micro_policy> GetPolicyByCustomerIDType(int idType, string idNumber, int year)
    {
        List<bl_micro_policy> polList = new List<bl_micro_policy>();
        bl_micro_policy pol = new bl_micro_policy();
        try
        {
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_GET_BY_CUSTOMER_ID_TYPE", new string[,] {
            {"@id_type",idType+""},{"@id_number", idNumber},{"@year",year+""}
            }, MYNAME + " => GetPolicyByCustomerIDType(int idType, string idNumber, int year)");
            if (db.RowEffect < 0)
            {
                //error
                _SUCCESS = false;
                _MESSAGE = db.Message;
            }
            else
            {

                _SUCCESS = true;

                _MESSAGE = tbl.Rows.Count + " record(s) found.";
                foreach (DataRow r in tbl.Rows)
                {
                    pol = new bl_micro_policy()
                    {
                        POLICY_ID = r["policy_id"].ToString(),
                        SEQ = Convert.ToInt32(r["seq"].ToString()),
                        POLICY_NUMBER = r["policy_number"].ToString(),
                        POLICY_TYPE = r["policy_type"].ToString(),
                        CUSTOMER_ID = r["customer_id"].ToString(),
                        PRODUCT_ID = r["product_id"].ToString(),
                        AGENT_CODE = r["agent_code"].ToString(),
                        CHANNEL_ID = r["channel_id"].ToString(),
                        CHANNEL_ITEM_ID = r["channel_item_id"].ToString(),
                        CHANNEL_LOCATION_ID = r["channel_location_id"].ToString(),
                        APPLICATION_ID = r["application_id"].ToString(),
                        POLICY_STATUS = r["policy_status"].ToString(),
                        REMARKS = r["remarks"].ToString(),
                        CREATED_BY = r["created_by"].ToString(),
                        CREATED_ON = Convert.ToDateTime(r["created_on"].ToString()),
                        UPDATED_BY = r["updated_by"].ToString(),
                        UPDATED_ON = Convert.ToDateTime(r["updated_on"].ToString())

                    };
                    polList.Add(pol);

                }
            }
        }
        catch (Exception ex)
        {
            polList = new List<bl_micro_policy>();

            Log.AddExceptionToLog(MYNAME, "GetPolicyByCustomerIDType(int idType, string idNumber, int year)", ex);
        }
        return polList;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="IF">if true count only inforce </param>
    /// <param name="userName"></param>
    /// <returns></returns>
    public static Int32 CountPolicy(string customerId, bool IF, string userName = "")
    {
        Int32 noPol = 0;
        try
        {
            List<bl_micro_policy> polList = new List<bl_micro_policy>();
            polList = GetPolicyByCustomerID(customerId, userName);
            if (SUCCESS)
            {
                if (IF)//only inforce policy
                {
                    noPol = polList.AsEnumerable().Count(_ => _.POLICY_STATUS == "IF");
                }
                else//all
                {
                    noPol = polList.Count;
                }

                _MESSAGE = noPol + " record(s) found.";
            }
            else
            {
                _SUCCESS = false;
                _MESSAGE = MESSAGE;
            }
        }
        catch (Exception ex)
        {
            noPol = 0;
            _SUCCESS = false;
            _MESSAGE = ex.Message;
         
            Log.AddExceptionToLog(MYNAME, "CountPolicy(string customerId, bool IF, string userName = \"\")", ex);
        }
        return noPol;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="idType"></param>
    /// <param name="idNumber"></param>
    /// <param name="year"></param>
    /// <param name="IF"></param>
    /// <returns></returns>
    public static Int32 CountPolicyByCustomerIdTypeInYear(int idType, string idNumber, int year, bool IF=true)
    {
        Int32 noPol = 0;
        try
        {
            List<bl_micro_policy> polList = new List<bl_micro_policy>();
            polList = GetPolicyByCustomerIDType(idType,idNumber,year);
            if (SUCCESS)
            {
                if (IF)//only inforce policy
                {
                    noPol = polList.AsEnumerable().Count(_ => _.POLICY_STATUS == "IF");
                }
                else//all
                {
                    noPol = polList.Count;
                }

                _MESSAGE = noPol + " record(s) found.";
            }
            else
            {
                _SUCCESS = false;
                _MESSAGE = MESSAGE;
            }
        }
        catch (Exception ex)
        {
            noPol = 0;
            _SUCCESS = false;
            _MESSAGE = ex.Message;

            Log.AddExceptionToLog(MYNAME, "CountPolicyByCustomerIdTypeInYear(int idType, string idNumber, int year, bool IF=true)", ex);
        }
        return noPol;
    }
}