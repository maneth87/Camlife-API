using NPOI.SS.Formula.PTG;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class da_PostPayment
{
   
    public bool SUCCESS;
    public string MESSAGE;
    public string CODE;
    public da_PostPayment() {
        SUCCESS = false;
        MESSAGE = "";
        CODE = "";
    }
    

    public bool SavePostPayment(bl_PostPayment obj)
    {
        bool result = false;
        try
        {
            DB db = new DB();
          result=  db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_BANK_PAYMENT_TRANSACTION_REALTIME_INSERT", new string[,] {
            {"@payment_code", obj.PaymentCode},
            {"@Bill_No", obj.BillNo },
            {"@Biller_id", obj.BillerId },
            {"@Biller_name",obj.BillerName}, 
            {"@bill_amount",obj.BillAmount+""},
            {"@fee_charge", obj.FeeCharge+"" },
            {"@total_amount", obj.TotalAmount+"" },
            {"@transaction_amount",obj.TransactionAmount+"" },
            {"@transaction_type", obj.TransactionType },
            { "@transaction_reference_number", obj.TransactionReferenceNumber},
            {"@transaction_date",obj.TransactionDate+"" },
            {"@bank_name", obj.BankName },
            {"@created_by", obj.CreatedBy },
            {"@created_on", obj.CreatedOn+"" },
            {"@sys_remarks", obj.SysRemarks }
            }, "SavePostPayment(bl_PostPayment obj)");

            SUCCESS = result;
            MESSAGE = db.Message;
            CODE= db.Code;
        }
        catch (Exception ex)
        {
            CODE = "0";
            MESSAGE = ex.Message;
            SUCCESS = false;
            Log.AddExceptionToLog("Error function [SavePostPayment(bl_PostPayment obj)] in class [da_PostPayment], detail: " + ex.Message + " ==> " + ex.StackTrace);

        }
        return result;
    }

    public string GetExistingTransactionNumber(string transactionNumber)
    {
        string tranNo = "";
        try
        {
            DB db = new DB();
            DataTable dt = new DataTable();
            dt = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_BANK_PAYMENT_TRANSACTION_REALTIME_GET_BY_TRAN_REF_NO", new string[,] {
            {"@TRANSACTION_REFERENCE_NUMBER",transactionNumber }
            }, "da_PostPayment=>GetExistingTransactionNumber(string transactionNumber)");

            if(db.RowEffect>=0)
            {
               if(dt.Rows.Count>0)
                {
                    tranNo = dt.Rows[0]["TRANSACTION_REFERENCE_NUMBER"].ToString();
                }
               else
                {
                    tranNo = "";
                }
                MESSAGE = db.Message;
                SUCCESS = true;
            }
            else
            {
                CODE= "0";
                MESSAGE = db.Message;
                tranNo = transactionNumber;
            }
        }
        catch(Exception ex)
        {
            tranNo = transactionNumber;
            //CODE = "0";
            //MESSAGE = ex.Message;
            //SUCCESS = false;
            //Log.AddExceptionToLog("Error function [SavePostPayment(bl_PostPayment obj)] in class [da_PostPayment], detail: " + ex.Message + " ==> " + ex.StackTrace);
            CallError("SavePostPayment(bl_PostPayment obj)", "da_PostPayment", ex);
        }
        return tranNo;
    }


    private void CallError(string functionName, string className, Exception errorMessage)
    {
        CODE = "0";
        MESSAGE = errorMessage.Message;
        SUCCESS = false;
        Log.AddExceptionToLog("Error function ["+ functionName +"] in class ["+  className +"], detail: " + errorMessage.Message + " ==> " + errorMessage.StackTrace);

    }
}