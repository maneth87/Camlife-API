using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Reflection;
using ICSharpCode.SharpZipLib.Zip;
using static System.Net.Mime.MediaTypeNames;

/// <summary>
/// Get connection string from web.config
/// </summary>
public class AppConfiguration
{
    public AppConfiguration()
    {

    }

    public static string GetConnectionString()
    {
        string connString = ConfigurationManager.ConnectionStrings["ApplicationDBContext"].ConnectionString.ToString();
        return connString;
    }

    public static string GetAccountConnectionString()
    {
        string connString = ConfigurationManager.ConnectionStrings["ApplicationParameter"].ConnectionString.ToString();
        return connString;
    }

    //ConnectionString "DefaultConnection" of database's name CAMLIFEAUTH
    public static string GetConnectionString_User()
    {
        string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();
        return connString;
    }

    //Flexi Term Temp Database Connection String
    public static string GetFlexiTermConnectionString()
    {
        string connString = ConfigurationManager.ConnectionStrings["FlexiTermDBContext"].ConnectionString.ToString();
        return connString;
    }

    //Quotation Database Connection String
    public static string GetQuotationConnectionString()
    {
        string connString = ConfigurationManager.ConnectionStrings["QuotationDBContext"].ConnectionString.ToString();
        return connString;
    }
    //CellCard Message Database
    public static string GetCellCardMessageDbConnectionString()
    {
        string connString = ConfigurationManager.ConnectionStrings["CellCardMessageDb"].ConnectionString.ToString();
        return connString;
    }
    //CellCard Message Database
    public static string GetCallCenterConnectionString()
    {
        string connString = ConfigurationManager.ConnectionStrings["CallCenterDB"].ConnectionString.ToString();
        return connString;
    }
    //CAN Database
    public static string GetCanDbConnectionString()
    {
        string connString = ConfigurationManager.ConnectionStrings["CAN"].ConnectionString.ToString();
        return connString;
    }
    //Camlife Intranet
    public static string GetCamlifeIntranetConnectionString()
    {
        string connString = ConfigurationManager.ConnectionStrings["CamlifeIntranet"].ConnectionString.ToString();
        return connString;
    }

  public enum MultiPolicyType { NEW, REPAYMENT};
    public static string CheckMultiPolicy()
    {
        string para = ConfigurationManager.AppSettings["CHECK-MULTI-POLICY-BY"].ToString();
        return para;
    }
        

    /// <summary>
    /// This setting is use for control policy repayment/renewal
    /// </summary>
    /// <returns></returns>
    public static bool AllowMultiPolicyPerLife()
    {

        try
        {
            string para = ConfigurationManager.AppSettings["ALLOW-MULTI-POLICY-PER-LIFE"].ToString();

            return para.ToUpper() == "YES" ? true : false;
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [AllowMultiPolicyPerLife()] in class [AppConfiguration], detail: " + ex.Message + " ==> " + ex.StackTrace);

            return false;
        }

    }

    /// <summary>
    /// This setting is use for control new policy
    /// </summary>
    /// <returns></returns>
    public static bool AllowMultiNewPolicyPerLife()
    {
        try
        {
            string para = ConfigurationManager.AppSettings["ALLOW-MULTI-NEW_POLICY-PER-LIFE"].ToString();

            return para.ToUpper() == "YES" ? true : false;
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [AllowMultiNewPolicyPerLife()] in class [AppConfiguration], detail: " + ex.Message + " ==> " + ex.StackTrace);

            return false;
        }

    }
    public static int AllowMultiPolicyBeforeExpireDays()
    {

        try
        {
            string para = ConfigurationManager.AppSettings["ALLOW-MULTI-POLICY-BEFORE-EXPIRE-DAYS"].ToString();

            return Convert.ToInt32(para);
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [AllowMultiPolicyBeforeExpireDays()] in class [AppConfiguration], detail: " + ex.Message + " ==> " + ex.StackTrace);

            return -1;
        }

    }
    public static int AllowMRepaymentPolicyAfterExpireDays()
    {

        try
        {
            string para = ConfigurationManager.AppSettings["ALLOW-REPAYMENT-POLICY-AFTER-EXPIRE-DAYS"].ToString();

            return Convert.ToInt32(para);
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [AllowMRepaymentPolicyAfterExpireDays()] in class [AppConfiguration], detail: " + ex.Message + " ==> " + ex.StackTrace);

            return -1;
        }

    }
    public static string GetApplicationApprover()
    {
        try
        {
            return ConfigurationManager.AppSettings["APP-APPROVER"].ToString();
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("AppConfiguration", "GetApplicationApprover()",  ex);
            return "";
        }

    }
    public static string GetCertificateGenerateUrl()
    {
        try
        {
            return ConfigurationManager.AppSettings["CERT-GENERATE-URL"].ToString();
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("AppConfiguration", "GetCertificateGenerateUrl()", ex);
            return "";
        }

    }
    public static string GetApplicationFormGenerateUrl()
    {
        try
        {
            return ConfigurationManager.AppSettings["APP-FORM-GENERATE-URL"].ToString();
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("AppConfiguration", "GetApplicationFormGenerateUrl()", ex);
            return "";
        }

    }

    #region Document

    private static readonly string ApplicationID = "5302F619-CB71-4C30-A244-E3EE152975C6";/*this application id is camlife application name*/
    public static string GetUploadDocumentPath()
    {
        try
        {
            bl_system.SYSTEM_PARAMATER sysObj = new bl_system.SYSTEM_PARAMATER();
            sysObj = sysObj.GetParamater(bl_system.SYSTEM_SETTING.DOCUMENT.LOCATION_OPTION.PATH, ApplicationID);
            return sysObj.ParamaterVal;
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("AppConfiguration", MethodBase.GetCurrentMethod().Name, ex);
            return "";
        }
    }
    /// <summary>
    /// File store location in location or remote server
    /// </summary>
    /// <returns></returns>
    public static string GetDocumentLocation()
    {
        try
        {
            bl_system.SYSTEM_PARAMATER sysObj = new bl_system.SYSTEM_PARAMATER();
            sysObj = sysObj.GetParamater(bl_system.SYSTEM_SETTING.DOCUMENT.LOCATION_OPTION.LOCATION, ApplicationID);
            return sysObj.ParamaterVal;

        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("AppConfiguration", MethodBase.GetCurrentMethod().Name, ex);
            return "";
        }
    }
    public static double GetDocumentSize()
    {
        try
        {
            bl_system.SYSTEM_PARAMATER sysObj = new bl_system.SYSTEM_PARAMATER();
            sysObj = sysObj.GetParamater(bl_system.SYSTEM_SETTING.DOCUMENT.DOCUMENT_SIZE, ApplicationID);
            return Convert.ToDouble(sysObj.ParamaterVal);
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("AppConfiguration", MethodBase.GetCurrentMethod().Name, ex);
            return 0;
        }
    }
    public static List< string> GetDocumentType()
    {
        List<string> lType = new List<string>();
        try
        {
            bl_system.SYSTEM_PARAMATER sysObj = new bl_system.SYSTEM_PARAMATER();
            sysObj = sysObj.GetParamater(bl_system.SYSTEM_SETTING.DOCUMENT.DOCUMENT_TYPE, ApplicationID);
            lType = sysObj.ParamaterVal.ToUpper().Split(',').ToList();
            return lType;
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("AppConfiguration", MethodBase.GetCurrentMethod().Name, ex);
            return new List<string>();
        }

    }

    public static string TransactionFilesPath()
    {
        try
        {
            bl_system.SYSTEM_PARAMATER sysObj = new bl_system.SYSTEM_PARAMATER();
            sysObj = sysObj.GetParamater(bl_system.SYSTEM_SETTING.DOCUMENT.TRANSACTION_FILES.PATH, ApplicationID);
            return sysObj.ParamaterVal;
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("AppConfiguration", MethodBase.GetCurrentMethod().Name, ex);
            return "";
        }
    }

    public static bl_system.SYSTEM_PARAMATER  GetRegistrationDocSendEmail()
    {
        try
        {
            bl_system.SYSTEM_PARAMATER sysObj = new bl_system.SYSTEM_PARAMATER();
            sysObj = sysObj.GetParamater(bl_system.SYSTEM_SETTING.DOCUMENT.TRANSACTION_FILES.REGISTRATIONSENDEMAIL, ApplicationID);
            return sysObj;
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("AppConfiguration", MethodBase.GetCurrentMethod().Name, ex);
            return null;
        }
    }
    #endregion Document

    #region Email
    public static string GetEmailHost()
    {
        //return ConfigurationManager.AppSettings["EMAIL-HOST"].ToString();
        bl_system.SYSTEM_PARAMATER sysObj = new bl_system.SYSTEM_PARAMATER();
        sysObj = sysObj.GetParamater(bl_system.SYSTEM_SETTING.SEND_EMAIL.EMAIL_HOST, ApplicationID);
        return sysObj.ParamaterVal;
    }
    public static Int32 GetEmailPort()
    {
        // return Convert.ToInt32(ConfigurationManager.AppSettings["EMAIL-PORT"].ToString());
        bl_system.SYSTEM_PARAMATER sysObj = new bl_system.SYSTEM_PARAMATER();
        sysObj = sysObj.GetParamater(bl_system.SYSTEM_SETTING.SEND_EMAIL.EMAIL_PORT, ApplicationID);
        return Convert.ToInt32(sysObj.ParamaterVal);

    }
    public static string GetEmailPassword()
    {
        // return ConfigurationManager.AppSettings["EMAIL-PASSWORD"].ToString();
        bl_system.SYSTEM_PARAMATER sysObj = new bl_system.SYSTEM_PARAMATER();
        sysObj = sysObj.GetParamater(bl_system.SYSTEM_SETTING.SEND_EMAIL.EMAIL_PASSWORD, ApplicationID);
        return sysObj.ParamaterVal;
    }

    public static string GetEmailFrom()
    {
        // return ConfigurationManager.AppSettings["EMAIL-FROM"].ToString();
        bl_system.SYSTEM_PARAMATER sysObj = new bl_system.SYSTEM_PARAMATER();
        sysObj = sysObj.GetParamater(bl_system.SYSTEM_SETTING.SEND_EMAIL.EMAIL_SENDER, ApplicationID);
        return sysObj.ParamaterVal;
    }
    #endregion
}