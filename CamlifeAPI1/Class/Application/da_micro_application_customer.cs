using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for da_application_customer
/// </summary>
public class da_micro_application_customer
{
    public da_micro_application_customer()
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
    public static bool SaveApplicationCustomer(bl_micro_application_customer APPLICATION_CUSTOMER)
    {
        bool result = false;
        try
        {
           // DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_CUSTOMER_INSERT", new string[,] {
            {"@CUSTOMER_ID", APPLICATION_CUSTOMER.CUSTOMER_ID}, 
            {"@ID_TYPE", APPLICATION_CUSTOMER.ID_TYPE+""}, 
            {"@ID_NUMBER", APPLICATION_CUSTOMER.ID_NUMBER}, 
            {"@FIRST_NAME_IN_ENGLISH", APPLICATION_CUSTOMER.FIRST_NAME_IN_ENGLISH},
            {"@LAST_NAME_IN_ENGLISH",APPLICATION_CUSTOMER.LAST_NAME_IN_ENGLISH}, 
            {"@FIRST_NAME_IN_KHMER",APPLICATION_CUSTOMER.FIRST_NAME_IN_KHMER},
            {"@LAST_NAME_IN_KHMER", APPLICATION_CUSTOMER.LAST_NAME_IN_KHMER}, 
            {"@GENDER",APPLICATION_CUSTOMER.GENDER+""}, 
            {"@DATE_OF_BIRTH",APPLICATION_CUSTOMER.DATE_OF_BIRTH+""}, 
            {"@NATIONALITY", APPLICATION_CUSTOMER.NATIONALITY}, 
            {"@MARITAL_STATUS", APPLICATION_CUSTOMER.MARITAL_STATUS}, 
            {"@OCCUPATION",APPLICATION_CUSTOMER.OCCUPATION}, 
            {"@PLACE_OF_BIRTH",APPLICATION_CUSTOMER.PLACE_OF_BIRTH},
            {"@HOUSE_NO_KH",APPLICATION_CUSTOMER.HOUSE_NO_KH}, 
            {"@STREET_NO_KH", APPLICATION_CUSTOMER.STREET_NO_KH}, 
            {"@VILLAGE_KH",APPLICATION_CUSTOMER.VILLAGE_KH}, 
            {"@COMMUNE_KH", APPLICATION_CUSTOMER.COMMUNE_KH}, 
            {"@DISTRICT_KH",APPLICATION_CUSTOMER.DISTRICT_KH}, 
            {"@PROVINCE_KH",APPLICATION_CUSTOMER.PROVINCE_KH},
            {"@HOUSE_NO_EN", APPLICATION_CUSTOMER.HOUSE_NO_KH}, 
            {"@STREET_NO_EN",APPLICATION_CUSTOMER.STREET_NO_EN}, 
            {"@VILLAGE_EN",APPLICATION_CUSTOMER.VILLAGE_EN}, 
            {"@COMMUNE_EN",APPLICATION_CUSTOMER.COMMUNE_EN}, 
            {"@DISTRICT_EN", APPLICATION_CUSTOMER.DISTRICT_EN}, 
            {"@PROVINCE_EN", APPLICATION_CUSTOMER.PROVINCE_EN}, 
            {"@PHONE_NUMBER1", APPLICATION_CUSTOMER.PHONE_NUMBER1},
            {"@PHONE_NUMBER2", APPLICATION_CUSTOMER.PHONE_NUMBER2}, 
            {"@PHONE_NUMBER3", APPLICATION_CUSTOMER.PHONE_NUMBER3}, 
            {"@EMAIL1",APPLICATION_CUSTOMER.EMAIL1}, 
            {"@EMAIL2", APPLICATION_CUSTOMER.EMAIL2}, 
            {"@EMAIL3", APPLICATION_CUSTOMER.EMAIL3}, 
            {"@CREATED_ON",APPLICATION_CUSTOMER.CREATED_ON+""}, 
            {"@CREATED_BY",APPLICATION_CUSTOMER.CREATED_BY}, 
            {"@REMARKS", APPLICATION_CUSTOMER.REMARKS}, 
            {"@STATUS", APPLICATION_CUSTOMER.STATUS+""}
            
            }, "da_micro_application_customer=>SaveApplicationCustomer(bl_micro_application_customer APPLICATION_CUSTOMER)");

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
            Log.AddExceptionToLog("Error function [SaveApplicationCustomer(bl_micro_application_customer APPLICATION_CUSTOMER)] in class [da_micro_application_customer], detail :" + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    
    }
    public static bool DeleteApplicationCustomer(string CUSTOMER_ID)
    {
        bool result = false;
        try
        {
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_CUSTOMER_DELETE", new string[,] {
            {"@CUSTOMER_ID", CUSTOMER_ID}
            
            }, "da_micro_application_customer=>DeleteApplicationCustomer(string CUSTOMER_ID)");
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [DeleteApplicationCustomer(string CUSTOMER_ID)] in class [da_micro_application_customer], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }
    public static bool UpdateApplicationCustomer(bl_micro_application_customer APPLICATION_CUSTOMER)
    {
        bool result = false;
        try
        {
            // DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_CUSTOMER_UPDATE", new string[,] {
            {"@CUSTOMER_ID", APPLICATION_CUSTOMER.CUSTOMER_ID}, 
            {"@ID_TYPE", APPLICATION_CUSTOMER.ID_TYPE + ""}, 
            {"@ID_NUMBER", APPLICATION_CUSTOMER.ID_NUMBER}, 
            {"@FIRST_NAME_IN_ENGLISH", APPLICATION_CUSTOMER.FIRST_NAME_IN_ENGLISH},
            {"@LAST_NAME_IN_ENGLISH",APPLICATION_CUSTOMER.LAST_NAME_IN_ENGLISH}, 
            {"@FIRST_NAME_IN_KHMER",APPLICATION_CUSTOMER.FIRST_NAME_IN_KHMER},
            {"@LAST_NAME_IN_KHMER", APPLICATION_CUSTOMER.LAST_NAME_IN_KHMER}, 
            {"@GENDER",APPLICATION_CUSTOMER.GENDER + ""}, 
            {"@DATE_OF_BIRTH",APPLICATION_CUSTOMER.DATE_OF_BIRTH+""}, 
            {"@NATIONALITY", APPLICATION_CUSTOMER.NATIONALITY}, 
            {"@MARITAL_STATUS", APPLICATION_CUSTOMER.MARITAL_STATUS}, 
            {"@OCCUPATION",APPLICATION_CUSTOMER.OCCUPATION}, 
            {"@PLACE_OF_BIRTH",APPLICATION_CUSTOMER.PLACE_OF_BIRTH},
            {"@HOUSE_NO_KH",APPLICATION_CUSTOMER.HOUSE_NO_KH}, 
            {"@STREET_NO_KH", APPLICATION_CUSTOMER.STREET_NO_KH}, 
            {"@VILLAGE_KH",APPLICATION_CUSTOMER.VILLAGE_KH}, 
            {"@COMMUNE_KH", APPLICATION_CUSTOMER.COMMUNE_KH}, 
            {"@DISTRICT_KH",APPLICATION_CUSTOMER.DISTRICT_KH}, 
            {"@PROVINCE_KH",APPLICATION_CUSTOMER.PROVINCE_KH},
            {"@HOUSE_NO_EN", APPLICATION_CUSTOMER.HOUSE_NO_EN}, 
            {"@STREET_NO_EN",APPLICATION_CUSTOMER.STREET_NO_EN}, 
            {"@VILLAGE_EN",APPLICATION_CUSTOMER.VILLAGE_EN}, 
            {"@COMMUNE_EN",APPLICATION_CUSTOMER.COMMUNE_EN}, 
            {"@DISTRICT_EN", APPLICATION_CUSTOMER.DISTRICT_EN}, 
            {"@PROVINCE_EN", APPLICATION_CUSTOMER.PROVINCE_EN}, 
            {"@PHONE_NUMBER1", APPLICATION_CUSTOMER.PHONE_NUMBER1},
            {"@PHONE_NUMBER2", APPLICATION_CUSTOMER.PHONE_NUMBER2}, 
            {"@PHONE_NUMBER3", APPLICATION_CUSTOMER.PHONE_NUMBER3}, 
            {"@EMAIL1",APPLICATION_CUSTOMER.EMAIL1}, 
            {"@EMAIL2", APPLICATION_CUSTOMER.EMAIL2}, 
            {"@EMAIL3", APPLICATION_CUSTOMER.EMAIL3}, 
            {"@UPDATED_ON",APPLICATION_CUSTOMER.UPDATED_ON+""}, 
            {"@UPDATED_BY",APPLICATION_CUSTOMER.UPDATED_BY}, 
            {"@REMARKS", APPLICATION_CUSTOMER.REMARKS}
            
            }, "da_micro_application_customer=>UpdateApplicationCustomer(bl_micro_application_customer APPLICATION_CUSTOMER)");

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
            Log.AddExceptionToLog("Error function [UpdateApplicationCustomer(bl_micro_application_customer APPLICATION_CUSTOMER)] in class [da_micro_application_customer], detail :" + ex.Message + "==>" + ex.StackTrace);
        }
        return result;

    }

}