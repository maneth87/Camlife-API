using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
/// <summary>
/// Summary description for da_micro_customer
/// </summary>
public class da_micro_customer
{
	public da_micro_customer()
	{
		//
		// TODO: Add constructor logic here
		//
      
	}
    private static string MYNAME = "da_micro_customer";
    private static DB db = new DB();
    private static bool _SUCCESS = false;
    private static string _MESSAGE = "";
    public static bool SUCCESS { get { return _SUCCESS; } }
    public static string MESSAGE { get { return _MESSAGE; } }
    public static bool SaveCustomer(bl_micro_customer CUSTOMER)
    {
        bool result = false;
        try
        {
            // DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_CUSTOMER1_INSERT", new string[,] {
            {"@ID", CUSTOMER.ID}, 
            {"@SEQ", CUSTOMER.SEQ+""},
            {"@CUSTOMER_NUMBER", CUSTOMER.CUSTOMER_NUMBER},
            {"@CUSTOMER_TYPE", CUSTOMER.CUSTOMER_TYPE},
            {"@ID_TYPE", CUSTOMER.ID_TYPE}, 
            {"@ID_NUMBER", CUSTOMER.ID_NUMBER}, 
            {"@FIRST_NAME_IN_ENGLISH", CUSTOMER.FIRST_NAME_IN_ENGLISH},
            {"@LAST_NAME_IN_ENGLISH",CUSTOMER.LAST_NAME_IN_ENGLISH}, 
            {"@FIRST_NAME_IN_KHMER",CUSTOMER.FIRST_NAME_IN_KHMER},
            {"@LAST_NAME_IN_KHMER", CUSTOMER.LAST_NAME_IN_KHMER}, 
            {"@GENDER",CUSTOMER.GENDER}, 
            {"@DATE_OF_BIRTH",CUSTOMER.DATE_OF_BIRTH+""}, 
            {"@NATIONALITY", CUSTOMER.NATIONALITY}, 
            {"@MARITAL_STATUS", CUSTOMER.MARITAL_STATUS}, 
            {"@OCCUPATION",CUSTOMER.OCCUPATION}, 
            {"@PLACE_OF_BIRTH",CUSTOMER.PLACE_OF_BIRTH},
            {"@HOUSE_NO_KH",CUSTOMER.HOUSE_NO_KH}, 
            {"@STREET_NO_KH", CUSTOMER.STREET_NO_KH}, 
            {"@VILLAGE_KH",CUSTOMER.VILLAGE_KH}, 
            {"@COMMUNE_KH", CUSTOMER.COMMUNE_KH}, 
            {"@DISTRICT_KH",CUSTOMER.DISTRICT_KH}, 
            {"@PROVINCE_KH",CUSTOMER.PROVINCE_KH},
            {"@HOUSE_NO_EN", CUSTOMER.HOUSE_NO_KH}, 
            {"@STREET_NO_EN",CUSTOMER.STREET_NO_EN}, 
            {"@VILLAGE_EN",CUSTOMER.VILLAGE_EN}, 
            {"@COMMUNE_EN",CUSTOMER.COMMUNE_EN}, 
            {"@DISTRICT_EN", CUSTOMER.DISTRICT_EN}, 
            {"@PROVINCE_EN", CUSTOMER.PROVINCE_EN}, 
            {"@PHONE_NUMBER1", CUSTOMER.PHONE_NUMBER1},
            {"@PHONE_NUMBER2", CUSTOMER.PHONE_NUMBER2}, 
            {"@PHONE_NUMBER3", CUSTOMER.PHONE_NUMBER3}, 
            {"@EMAIL1",CUSTOMER.EMAIL1}, 
            {"@EMAIL2", CUSTOMER.EMAIL2}, 
            {"@EMAIL3", CUSTOMER.EMAIL3}, 
            {"@CREATED_ON",CUSTOMER.CREATED_ON+""}, 
            {"@CREATED_BY",CUSTOMER.CREATED_BY}, 
            {"@REMARKS", CUSTOMER.REMARKS}, 
            {"@STATUS", CUSTOMER.STATUS+""}
            
            }, "da_micro_customer=>SaveCustomer(bl_micro_customer CUSTOMER)");

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
            Log.AddExceptionToLog("da_micro_customer", "SaveCustomer(bl_micro_customer CUSTOMER)",  ex);
        }
        return result;

    }
    public static bool UpdateCustomer(bl_micro_customer CUSTOMER)
    {
        bool result = false;
        try
        {
            db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_CUSTOMER1_UPDATE", new string[,] {
            {"@ID", CUSTOMER.ID}, 
            //{"@CUSTOMER_NUMBER", CUSTOMER.CUSTOMER_NUMBER},
            {"@CUSTOMER_TYPE", CUSTOMER.CUSTOMER_TYPE},
            {"@ID_TYPE", CUSTOMER.ID_TYPE}, 
            {"@ID_NUMBER", CUSTOMER.ID_NUMBER}, 
            {"@FIRST_NAME_IN_ENGLISH", CUSTOMER.FIRST_NAME_IN_ENGLISH},
            {"@LAST_NAME_IN_ENGLISH",CUSTOMER.LAST_NAME_IN_ENGLISH}, 
            {"@FIRST_NAME_IN_KHMER",CUSTOMER.FIRST_NAME_IN_KHMER},
            {"@LAST_NAME_IN_KHMER", CUSTOMER.LAST_NAME_IN_KHMER}, 
            {"@GENDER",CUSTOMER.GENDER}, 
            {"@DATE_OF_BIRTH",CUSTOMER.DATE_OF_BIRTH+""}, 
            {"@NATIONALITY", CUSTOMER.NATIONALITY}, 
            {"@MARITAL_STATUS", CUSTOMER.MARITAL_STATUS}, 
            {"@OCCUPATION",CUSTOMER.OCCUPATION}, 
            {"@PLACE_OF_BIRTH",CUSTOMER.PLACE_OF_BIRTH},
            {"@HOUSE_NO_KH",CUSTOMER.HOUSE_NO_KH}, 
            {"@STREET_NO_KH", CUSTOMER.STREET_NO_KH}, 
            {"@VILLAGE_KH",CUSTOMER.VILLAGE_KH}, 
            {"@COMMUNE_KH", CUSTOMER.COMMUNE_KH}, 
            {"@DISTRICT_KH",CUSTOMER.DISTRICT_KH}, 
            {"@PROVINCE_KH",CUSTOMER.PROVINCE_KH},
            {"@HOUSE_NO_EN", CUSTOMER.HOUSE_NO_KH}, 
            {"@STREET_NO_EN",CUSTOMER.STREET_NO_EN}, 
            {"@VILLAGE_EN",CUSTOMER.VILLAGE_EN}, 
            {"@COMMUNE_EN",CUSTOMER.COMMUNE_EN}, 
            {"@DISTRICT_EN", CUSTOMER.DISTRICT_EN}, 
            {"@PROVINCE_EN", CUSTOMER.PROVINCE_EN}, 
            {"@PHONE_NUMBER1", CUSTOMER.PHONE_NUMBER1},
            {"@PHONE_NUMBER2", CUSTOMER.PHONE_NUMBER2}, 
            {"@PHONE_NUMBER3", CUSTOMER.PHONE_NUMBER3}, 
            {"@EMAIL1",CUSTOMER.EMAIL1}, 
            {"@EMAIL2", CUSTOMER.EMAIL2}, 
            {"@EMAIL3", CUSTOMER.EMAIL3}, 
            {"@UPDATED_ON",CUSTOMER.UPDATED_ON+""}, 
            {"@UPDATED_BY",CUSTOMER.UPDATED_BY}, 
            {"@REMARKS", CUSTOMER.REMARKS}, 
            {"@STATUS", CUSTOMER.STATUS+""}
            
            }, "da_micro_customer=>UpdateCustomer(bl_micro_customer CUSTOMER)");

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
            Log.AddExceptionToLog("Error function [UpdateCustomer(bl_micro_customer CUSTOMER)] in class [da_micro_customer], detail :" + ex.Message + "==>" + ex.StackTrace);
        }
        return result;

    }
    /// <summary>
    /// Delete customer by id
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public static bool DeleteCustomer(string  ID)
    {
        bool result = false;
        try
        {
            // DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_CUSTOMER1_DELETE", new string[,] {
            {"@ID", ID}
            
            }, "da_micro_customer=>DeleteCustomer(string  ID)");

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
            Log.AddExceptionToLog("Error function [DeleteCustomer(string  ID)] in class [da_micro_customer], detail :" + ex.Message + "==>" + ex.StackTrace);
        }
        return result;

    }

    public static bl_micro_customer GetExistCustomer(string FIRST_NAME_IN_ENGLISH, string LAST_NAME_IN_ENGLISH, DateTime DATE_OF_BIRTH)
    {
        bl_micro_customer cus = new bl_micro_customer();
        try
        {
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_CUSTOMER1_GET_EXISTING", new string[,] {
            {"@FIRST_NAME_IN_ENGLISH", FIRST_NAME_IN_ENGLISH},
            {"@LAST_NAME_IN_ENGLISH", LAST_NAME_IN_ENGLISH},
            {"@DATE_OF_BIRTH", DATE_OF_BIRTH+""}
            }, "da_micro_customer=>GetExistCustomer(string FIRST_NAME_IN_ENGLISH, string LAST_NAME_IN_ENGLISH, DateTime DATE_OF_BIRTH)");

            if (db.RowEffect == -1)
            {
                _MESSAGE = db.Message;
                _SUCCESS = false;
            }
            else
            {
                if (tbl.Rows.Count == 0)
                {
                    _MESSAGE = "No Record Found.";
                    _SUCCESS = true;
                }
                else
                {
                    var r = tbl.Rows[0];
                    cus = new bl_micro_customer()
                    {
                        ID=r["id"].ToString(),
                        CUSTOMER_TYPE=r["customer_type"].ToString(),
                        SEQ=Convert.ToInt32(r["seq"].ToString()),
                        CUSTOMER_NUMBER=r["customer_number"].ToString(),
                        ID_TYPE=r["id_type"].ToString(),
                        ID_NUMBER=r["id_number"].ToString(),
                        LAST_NAME_IN_ENGLISH=r["last_name_in_english"].ToString(),
                        FIRST_NAME_IN_ENGLISH=r["first_name_in_english"].ToString(),
                        LAST_NAME_IN_KHMER=r["last_name_in_khmer"].ToString(),
                        FIRST_NAME_IN_KHMER=r["first_name_in_khmer"].ToString(),
                        GENDER=r["gender"].ToString(),
                        DATE_OF_BIRTH=Convert.ToDateTime(r["date_of_birth"].ToString()),
                        NATIONALITY=r["nationality"].ToString(),
                        MARITAL_STATUS =r["marital_status"].ToString(),
                        OCCUPATION=r["occupation"].ToString(),
                        HOUSE_NO_EN=r["house_no_en"].ToString(),
                  STREET_NO_EN=r["street_no_en"].ToString(),
                  VILLAGE_EN =r["village_en"].ToString(),
                  COMMUNE_EN=r["commune_en"].ToString(),
                  DISTRICT_EN=r["district_en"].ToString(),
                  PROVINCE_EN=r["province_en"].ToString(),
                        HOUSE_NO_KH = r["house_no_kh"].ToString(),
                        STREET_NO_KH = r["street_no_kh"].ToString(),
                        VILLAGE_KH = r["village_kh"].ToString(),
                        COMMUNE_KH = r["commune_kh"].ToString(),
                        DISTRICT_KH = r["district_kh"].ToString(),
                        PROVINCE_KH = r["province_kh"].ToString(),
                        PHONE_NUMBER1=r["phone_number1"].ToString(),
                        PHONE_NUMBER2 = r["phone_number2"].ToString(),
                        PHONE_NUMBER3 = r["phone_number3"].ToString(),
                        EMAIL1=r["email1"].ToString(),
                        EMAIL2 = r["email2"].ToString(),
                        EMAIL3 = r["email3"].ToString(),
                        CREATED_BY=r["created_by"].ToString(),
                        CREATED_ON=Convert.ToDateTime( r["created_on"].ToString()),
                        UPDATED_BY=r["updated_by"].ToString(),
                        UPDATED_ON=Convert.ToDateTime(r["updated_on"].ToString()),
                        PLACE_OF_BIRTH=r["place_of_birth"].ToString(),
                         REMARKS=r["remarks"].ToString(),
                         STATUS=Convert.ToInt32(r["status"].ToString())
                    };
                }
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            cus = new bl_micro_customer();
            Log.AddExceptionToLog("Error function [GetExistCustomer(string FIRST_NAME_IN_ENGLISH, string LAST_NAME_IN_ENGLISH, DateTime DATE_OF_BIRTH)] in class [da_micro_customer], detail:" + ex.Message + "==>" + ex.StackTrace);
        }

        return cus;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="custId"></param>
    /// <param name="userName">use for tracking log</param>
    /// <returns></returns>
    public static bl_micro_customer GetCustomer(string custId, string userName="")
    {
        bl_micro_customer cus = new bl_micro_customer();
        try
        {
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_CUSTOMER1_GET_BY_ID", new string[,] {
            {"@ID", custId}
            }, "da_micro_customer=>GetCustomer(string custId)");

            if (db.RowEffect == -1)
            {
                _MESSAGE = db.Message;
                _SUCCESS = false;
            }
            else
            {
                if (tbl.Rows.Count == 0)
                {
                    _MESSAGE = "No Record Found.";
                    _SUCCESS = true;
                }
                else
                {
                    var r = tbl.Rows[0];
                    cus = new bl_micro_customer()
                    {
                        ID = r["id"].ToString(),
                        CUSTOMER_TYPE = r["customer_type"].ToString(),
                        SEQ = Convert.ToInt32(r["seq"].ToString()),
                        CUSTOMER_NUMBER = r["customer_number"].ToString(),
                        ID_TYPE = r["id_type"].ToString(),
                        ID_NUMBER = r["id_number"].ToString(),
                        LAST_NAME_IN_ENGLISH = r["last_name_in_english"].ToString(),
                        FIRST_NAME_IN_ENGLISH = r["first_name_in_english"].ToString(),
                        LAST_NAME_IN_KHMER = r["last_name_in_khmer"].ToString(),
                        FIRST_NAME_IN_KHMER = r["first_name_in_khmer"].ToString(),
                        GENDER = r["gender"].ToString(),
                        DATE_OF_BIRTH = Convert.ToDateTime(r["date_of_birth"].ToString()),
                        NATIONALITY = r["nationality"].ToString(),
                        MARITAL_STATUS = r["marital_status"].ToString(),
                        OCCUPATION = r["occupation"].ToString(),
                        HOUSE_NO_EN = r["house_no_en"].ToString(),
                        STREET_NO_EN = r["street_no_en"].ToString(),
                        VILLAGE_EN = r["village_en"].ToString(),
                        COMMUNE_EN = r["commune_en"].ToString(),
                        DISTRICT_EN = r["district_en"].ToString(),
                        PROVINCE_EN = r["province_en"].ToString(),
                        HOUSE_NO_KH = r["house_no_kh"].ToString(),
                        STREET_NO_KH = r["street_no_kh"].ToString(),
                        VILLAGE_KH = r["village_kh"].ToString(),
                        COMMUNE_KH = r["commune_kh"].ToString(),
                        DISTRICT_KH = r["district_kh"].ToString(),
                        PROVINCE_KH = r["province_kh"].ToString(),
                        PHONE_NUMBER1 = r["phone_number1"].ToString(),
                        PHONE_NUMBER2 = r["phone_number2"].ToString(),
                        PHONE_NUMBER3 = r["phone_number3"].ToString(),
                        EMAIL1 = r["email1"].ToString(),
                        EMAIL2 = r["email2"].ToString(),
                        EMAIL3 = r["email3"].ToString(),
                        CREATED_BY = r["created_by"].ToString(),
                        CREATED_ON = Convert.ToDateTime(r["created_on"].ToString()),
                        UPDATED_BY = r["updated_by"].ToString(),
                        UPDATED_ON = Convert.ToDateTime(r["updated_on"].ToString()),
                        PLACE_OF_BIRTH = r["place_of_birth"].ToString(),
                        REMARKS = r["remarks"].ToString(),
                        STATUS = Convert.ToInt32(r["status"].ToString())
                    };
                }
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            cus = null;
            Log.AddExceptionToLog(MYNAME, MethodBase.GetCurrentMethod().Name, ex);
           
        }

        return cus;
    }
    public static bl_micro_customer GetCustomerByIdNumber(Int32 idType, string idNo, string userName = "")
    {
        bl_micro_customer cus = new bl_micro_customer();
        try
        {
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_CUSTOMER1_GET_BY_ID_NUMBER", new string[,] {
            {"@ID_TYPE", idType+""},{"@id_number", idNo}
            }, "da_micro_customer=>GetCustomer(string custId)");

            if (db.RowEffect == -1)
            {
                _MESSAGE = db.Message;
                _SUCCESS = false;
            }
            else
            {
                if (tbl.Rows.Count == 0)
                {
                    _MESSAGE = "No Record Found.";
                    _SUCCESS = true;
                }
                else
                {
                    var r = tbl.Rows[0];
                    cus = new bl_micro_customer()
                    {
                        ID = r["id"].ToString(),
                        CUSTOMER_TYPE = r["customer_type"].ToString(),
                        SEQ = Convert.ToInt32(r["seq"].ToString()),
                        CUSTOMER_NUMBER = r["customer_number"].ToString(),
                        ID_TYPE = r["id_type"].ToString(),
                        ID_NUMBER = r["id_number"].ToString(),
                        LAST_NAME_IN_ENGLISH = r["last_name_in_english"].ToString(),
                        FIRST_NAME_IN_ENGLISH = r["first_name_in_english"].ToString(),
                        LAST_NAME_IN_KHMER = r["last_name_in_khmer"].ToString(),
                        FIRST_NAME_IN_KHMER = r["first_name_in_khmer"].ToString(),
                        GENDER = r["gender"].ToString(),
                        DATE_OF_BIRTH = Convert.ToDateTime(r["date_of_birth"].ToString()),
                        NATIONALITY = r["nationality"].ToString(),
                        MARITAL_STATUS = r["marital_status"].ToString(),
                        OCCUPATION = r["occupation"].ToString(),
                        HOUSE_NO_EN = r["house_no_en"].ToString(),
                        STREET_NO_EN = r["street_no_en"].ToString(),
                        VILLAGE_EN = r["village_en"].ToString(),
                        COMMUNE_EN = r["commune_en"].ToString(),
                        DISTRICT_EN = r["district_en"].ToString(),
                        PROVINCE_EN = r["province_en"].ToString(),
                        HOUSE_NO_KH = r["house_no_kh"].ToString(),
                        STREET_NO_KH = r["street_no_kh"].ToString(),
                        VILLAGE_KH = r["village_kh"].ToString(),
                        COMMUNE_KH = r["commune_kh"].ToString(),
                        DISTRICT_KH = r["district_kh"].ToString(),
                        PROVINCE_KH = r["province_kh"].ToString(),
                        PHONE_NUMBER1 = r["phone_number1"].ToString(),
                        PHONE_NUMBER2 = r["phone_number2"].ToString(),
                        PHONE_NUMBER3 = r["phone_number3"].ToString(),
                        EMAIL1 = r["email1"].ToString(),
                        EMAIL2 = r["email2"].ToString(),
                        EMAIL3 = r["email3"].ToString(),
                        CREATED_BY = r["created_by"].ToString(),
                        CREATED_ON = Convert.ToDateTime(r["created_on"].ToString()),
                        UPDATED_BY = r["updated_by"].ToString(),
                        UPDATED_ON = Convert.ToDateTime(r["updated_on"].ToString()),
                        PLACE_OF_BIRTH = r["place_of_birth"].ToString(),
                        REMARKS = r["remarks"].ToString(),
                        STATUS = Convert.ToInt32(r["status"].ToString())
                    };
                }
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            cus = null;
            Log.AddExceptionToLog(MYNAME, MethodBase.GetCurrentMethod().Name, ex);
        }

        return cus;
    }
    /// <summary>
    /// Get customer by customer number
    /// </summary>
    /// <param name="custNo"></param>
    /// <param name="userName"></param>
    /// <returns></returns>
    public static bl_micro_customer GetCustomerByCustomerNo(string custNo, string userName = "")
    {
        bl_micro_customer cus = new bl_micro_customer();
        try
        {
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_CUSTOMER1_GET_BY_NO", new string[,] {
            {"@CUSTOMER_NO", custNo}
            }, "da_micro_customer=>GetCustomerByCustomerNo(string custNo, string userName = \"\")");

            if (db.RowEffect == -1)
            {
                _MESSAGE = db.Message;
                _SUCCESS = false;
            }
            else
            {
                if (tbl.Rows.Count == 0)
                {
                    _MESSAGE = "No Record Found.";
                    _SUCCESS = true;
                }
                else
                {
                    var r = tbl.Rows[0];
                    cus = new bl_micro_customer()
                    {
                        ID = r["id"].ToString(),
                        CUSTOMER_TYPE = r["customer_type"].ToString(),
                        SEQ = Convert.ToInt32(r["seq"].ToString()),
                        CUSTOMER_NUMBER = r["customer_number"].ToString(),
                        ID_TYPE = r["id_type"].ToString(),
                        ID_NUMBER = r["id_number"].ToString(),
                        LAST_NAME_IN_ENGLISH = r["last_name_in_english"].ToString(),
                        FIRST_NAME_IN_ENGLISH = r["first_name_in_english"].ToString(),
                        LAST_NAME_IN_KHMER = r["last_name_in_khmer"].ToString(),
                        FIRST_NAME_IN_KHMER = r["first_name_in_khmer"].ToString(),
                        GENDER = r["gender"].ToString(),
                        DATE_OF_BIRTH = Convert.ToDateTime(r["date_of_birth"].ToString()),
                        NATIONALITY = r["nationality"].ToString(),
                        MARITAL_STATUS = r["marital_status"].ToString(),
                        OCCUPATION = r["occupation"].ToString(),
                        HOUSE_NO_EN = r["house_no_en"].ToString(),
                        STREET_NO_EN = r["street_no_en"].ToString(),
                        VILLAGE_EN = r["village_en"].ToString(),
                        COMMUNE_EN = r["commune_en"].ToString(),
                        DISTRICT_EN = r["district_en"].ToString(),
                        PROVINCE_EN = r["province_en"].ToString(),
                        HOUSE_NO_KH = r["house_no_kh"].ToString(),
                        STREET_NO_KH = r["street_no_kh"].ToString(),
                        VILLAGE_KH = r["village_kh"].ToString(),
                        COMMUNE_KH = r["commune_kh"].ToString(),
                        DISTRICT_KH = r["district_kh"].ToString(),
                        PROVINCE_KH = r["province_kh"].ToString(),
                        PHONE_NUMBER1 = r["phone_number1"].ToString(),
                        PHONE_NUMBER2 = r["phone_number2"].ToString(),
                        PHONE_NUMBER3 = r["phone_number3"].ToString(),
                        EMAIL1 = r["email1"].ToString(),
                        EMAIL2 = r["email2"].ToString(),
                        EMAIL3 = r["email3"].ToString(),
                        CREATED_BY = r["created_by"].ToString(),
                        CREATED_ON = Convert.ToDateTime(r["created_on"].ToString()),
                        UPDATED_BY = r["updated_by"].ToString(),
                        UPDATED_ON = Convert.ToDateTime(r["updated_on"].ToString()),
                        PLACE_OF_BIRTH = r["place_of_birth"].ToString(),
                        REMARKS = r["remarks"].ToString(),
                        STATUS = Convert.ToInt32(r["status"].ToString())
                    };
                }
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            cus = null;
            Log.AddExceptionToLog(MYNAME, MethodBase.GetCurrentMethod().Name, ex);
        }

        return cus;
    }

}