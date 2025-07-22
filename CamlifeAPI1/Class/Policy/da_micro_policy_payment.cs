using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for da_micro_policy_payment
/// </summary>
public class da_micro_policy_payment
{
	public da_micro_policy_payment()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private static bool _SUCCESS = false;
    private static string _MESSAGE = "";

    public static bool SUCCESS { get { return _SUCCESS; } }
    public static string MESSAGE { get { return _MESSAGE; } }
    private static DB db = new DB();
    public static bool SavePayment(bl_micro_policy_payment PAYMENT)
    {
        bool result = false;
        try
        {
           db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_PAYMENT_INSERT", new string[,] {
            {"@POLICY_PAY_MENT_ID", PAYMENT.POLICY_PAYMENT_ID}, 
            {"@POLICY_DETAIL_ID", PAYMENT.POLICY_DETAIL_ID},
            {"@DUE_DATE",PAYMENT.DUE_DATE+""},
            {"@PAY_DATE",PAYMENT.PAY_DATE+""},
            {"@NEXT_DUE_DATE",PAYMENT.NEXT_DUE+""},
            {"@PREMIUM_YEAR",PAYMENT.PREMIUM_YEAR+""},
            {"@PREMIUM_LOT",PAYMENT.PREMIUM_LOT+""},
            {"@USER_PREMIUM",PAYMENT.USER_PREMIUM+""},
            {"@AMOUNT",PAYMENT.AMOUNT+""},
            {"@DISCOUNT_AMOUNT",PAYMENT.DISCOUNT_AMOUNT+""},
            {"@TOTAL_AMOUNT",PAYMENT.TOTAL_AMOUNT+""},
            {"@POLICY_STATUS",PAYMENT.POLICY_STATUS},
            {"@OFFICE_ID",PAYMENT.OFFICE_ID},
            {"@PAY_MODE",PAYMENT.PAY_MODE+""},
            {"@REFERRAL_FEE", PAYMENT.REFERRAL_FEE+""},
            {"@REFERRAL_INCENTIVE", PAYMENT.REFERRAL_INCENTIVE+""},
            {"@TRANSACTION_REFERRENCE_NO",PAYMENT.REFERANCE_TRANSACTION_CODE},
            {"@TRANSACTION_TYPE",PAYMENT.TRANSACTION_TYPE},
            {"@CREATED_BY",PAYMENT.CREATED_BY},
            {"@CREATED_ON",PAYMENT.CREATED_ON+""},
            {"@REMARKS", PAYMENT.REMARKS}
            }, "da_micro_policy_payment=>SavePayment(bl_micro_policy_payment PAYMENT)");

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
            Log.AddExceptionToLog("Error function [SavePayment(bl_micro_policy_payment PAYMENT)] in class [da_micro_policy_payment], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;

    }
    public static bool UpdatePayment(bl_micro_policy_payment PAYMENT)
    {
        bool result = false;
        try
        {
           db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_PAYMENT_UPDATE", new string[,] {
            {"@POLICY_PAY_MENT_ID", PAYMENT.POLICY_PAYMENT_ID}, 
            {"@POLICY_DETAIL_ID", PAYMENT.POLICY_DETAIL_ID},
            {"@DUE_DATE",PAYMENT.DUE_DATE+""},
            {"@PAY_DATE",PAYMENT.PAY_DATE+""},
            {"@NEXT_DUE_DATE",PAYMENT.NEXT_DUE+""},
            {"@PREMIUM_YEAR",PAYMENT.PREMIUM_YEAR+""},
            {"@PREMIUM_LOT",PAYMENT.PREMIUM_LOT+""},
            {"@USER_PREMIUM",PAYMENT.USER_PREMIUM+""}, 
            {"@AMOUNT",PAYMENT.AMOUNT+""},
            {"@DISCOUNT_AMOUNT",PAYMENT.DISCOUNT_AMOUNT+""},
             {"@TOTAL_AMOUNT",PAYMENT.TOTAL_AMOUNT+""},
            {"@POLICY_STATUS",PAYMENT.POLICY_STATUS},
            {"@OFFICE_ID",PAYMENT.OFFICE_ID},
            {"@PAY_MODE",PAYMENT.PAY_MODE+""},
            {"@REFERRAL_FEE", PAYMENT.REFERRAL_FEE+""},
            {"@REFERRAL_INCENTIVE", PAYMENT.REFERRAL_INCENTIVE+""},
            {"@TRANSACTION_REFERRENCE_NO",PAYMENT.REFERANCE_TRANSACTION_CODE},
            {"@TRANSACTION_TYPE",PAYMENT.TRANSACTION_TYPE},
            {"@UPDATED_BY",PAYMENT.UPDATED_BY},
            {"@UPDATED_ON",PAYMENT.UPDATED_ON+""},
            {"@REMARKS", PAYMENT.REMARKS}
            }, "da_micro_policy_payment=>UpdatePayment(bl_micro_policy_payment PAYMENT)");

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
            Log.AddExceptionToLog("Error function [UpdatePayment(bl_micro_policy_payment PAYMENT)] in class [da_micro_policy_payment], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;

    }
    public static bool DeletePayment(string POLICY_PAYMENT_ID)
    {
        bool result = false;
        try
        {
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_PAYMENT_DELETE", new string[,] {
            {"@POLICY_PAYMENT_ID", POLICY_PAYMENT_ID}
            
            }, "da_micro_policy_payment=> DeletePayment(string POLICY_PAYMENT_ID)");
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [DeletePayment(string POLICY_PAYMENT_ID)] in class [da_micro_policy_payment], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }
    public static DataTable GetPolicyPaymentDetail(string POLICY_ID)
    {
        DataTable tbl = new DataTable();
        try
        {
            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_PAYMENT_GET_PAYMENT_DETAIL", new string[,] {
        
        {"@POLICY_ID", POLICY_ID}}, "da_micro_policy_payment=>GetPolicyPaymentDetail(string POLICY_ID)");

            if (db.RowEffect == -1)//Error
            {
                _SUCCESS = false;
                _MESSAGE = db.Message;
            }
            else
            {
                if (tbl.Rows.Count > 0)
                {
                    _SUCCESS = true;
                    _MESSAGE = "Success";
                    
                }
                else {

                    _MESSAGE = "No Record Found";
                    _SUCCESS = true;
                }

            }

        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            tbl = new DataTable();
            Log.AddExceptionToLog("Error function [GetPolicyPaymentDetail(string POLICY_ID)] in class [da_micro_policy_payment], detail: " + ex.Message + "==>" + ex.StackTrace);

        }
        return tbl;
    }
}