using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;
#region Excell
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.HSSF.Util;
using System.Text;
#endregion

/// <summary>
/// Summary description for Helper
/// </summary>
public class Helper
{
	#region "Constructor(s)"

    private static Helper mytitle = null;
    public Helper()
    {
        if (mytitle == null)
        {
            mytitle = new Helper();
        }

    }
    #endregion

    #region "Public Functions"

    //Get unique and available guid id
    public static string GetNewGuid(string stored_procedure_name, string paramenter_name)
    {
        Guid myGuid = default(Guid);
        do
        {
            myGuid = Guid.NewGuid();

        } while (CheckGuid(myGuid.ToString().ToUpper(), stored_procedure_name, paramenter_name));

        return myGuid.ToString().ToUpper();
    }
    public static string GetNewGuid(string connection_string, string stored_procedure_name, string paramenter_name)
    {
        Guid myGuid = default(Guid);
        do
        {
            myGuid = Guid.NewGuid();

        } while (CheckGuid(myGuid.ToString().ToUpper(), connection_string, stored_procedure_name, paramenter_name));

        return myGuid.ToString().ToUpper();
    }
    //Check existing guid id in the table
    public static bool CheckGuid(string myGuid, string stored_procedure_name, string parameter_name)
    {
        string connString = AppConfiguration.GetConnectionString();
        bool result = false;
        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand(stored_procedure_name, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramName1 = new SqlParameter();
            paramName1.ParameterName = parameter_name;
            paramName1.Value = myGuid;
            cmd.Parameters.Add(paramName1);

            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {

                if (rdr.HasRows)
                {

                    result = true; //exist

                }

            }
            rdr.Close();
            con.Close();

        }
        return result;
    }

    public static bool CheckGuid(string myGuid, string connection_string, string stored_procedure_name, string parameter_name)
    {
      
        bool result = false;
        using (SqlConnection con = new SqlConnection(connection_string))
        {
            SqlCommand cmd = new SqlCommand(stored_procedure_name, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramName1 = new SqlParameter();
            paramName1.ParameterName = parameter_name;
            paramName1.Value = myGuid;
            cmd.Parameters.Add(paramName1);

            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {

                if (rdr.HasRows)
                {

                    result = true; //exist

                }

            }
            rdr.Close();
            con.Close();

        }
        return result;
    }

    public static string GetApplicationID(string applicationName) 
    {
        string result = "";
        using (SqlConnection con = new SqlConnection(AppConfiguration.GetAccountConnectionString()))
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GET_APPLICATION_ID";
            cmd.Parameters.AddWithValue("@APPLICATION_NAME", applicationName);
            cmd.Connection = con;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr.HasRows)
                {
                    result = rdr["ApplicationId"].ToString(); //exist
                }
            }
            rdr.Close();
            con.Close();

        }
        return result;
    }

    #region By Maneth 21082017
    /*
     Generate new guid by using sql query
     */
    //Get unique and available guid id
    /// <summary>
    /// Get a new guid
    /// </summary>
    /// <param name="param">{"TABLE","TABLE_NAME"},{"FIELD","FIELD_NAME"}</param>
    /// <returns></returns>
    public static string GetNewGuid(string [,] param)
    {
        
        Guid myGuid = default(Guid);
        do
        {
            myGuid = Guid.NewGuid();

        } while (CheckGuid(new string[,]{{param[1,1], myGuid.ToString().ToUpper()},{param[0,0],param[0,1]},{param[1,0],param[1,1]}}));

        return myGuid.ToString().ToUpper();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="connection_string">Database Connection string</param>
    /// <param name="param">{"TABLE","TABLE_NAME"},{"FIELD","FIELD_NAME"}</param>
    /// <returns></returns>
    public static string GetNewGuid(string connection_string, string[,] param)
    {

        Guid myGuid = default(Guid);
        do
        {
            myGuid = Guid.NewGuid();

        } while (CheckGuid( connection_string, new string[,] { { param[1, 1], myGuid.ToString().ToUpper() }, { param[0, 0], param[0, 1] }, { param[1, 0], param[1, 1] } }));

        return myGuid.ToString().ToUpper();
    }

    //Check existing guid id in the table
    public static bool CheckGuid(string[,] param)
    {
        bool result = false;
        string table_name = param[1, 1];
        string field_name = param[2, 1];
        string guid = param[0, 1];
        
       DataTable tbl= DataSetGenerator.Get_Data_Soure("SELECT " + field_name + " FROM " + table_name + " WHERE " + param[0,0] + " ='" + guid + "'" );
      
        if (tbl.Rows.Count>0)
        {
            result = true; //exist
        }
       
        return result;
    }
    public static bool CheckGuid( string connection_string, string[,] param)
    {
        bool result = false;
        string table_name = param[1, 1];
        string field_name = param[2, 1];
        string guid = param[0, 1];

        DataTable tbl = DataSetGenerator.Get_Data_Soure(connection_string,  "SELECT " + field_name + " FROM " + table_name + " WHERE " + param[0, 0] + " ='" + guid + "'");

        if (tbl.Rows.Count > 0)
        {
            result = true; //exist
        }

        return result;
    }
    #endregion

    //Get Leap Year Count
    public static int Get_Number_Of_Leap_Year(int dob_year, int this_year)
    {

        int number_of_year = 0;
        int i = dob_year;


        while ((i <= this_year))
        {
            if (((i % 4 == 0) && (i % 100 != 0) || (i % 400 == 0)))
            {
                number_of_year += 1;
            }

            i += 1;
        }

        return number_of_year;
    }

    //Check Leap Year
    public static bool Check_Leap_Year(int year)
    {

        bool result = false;

        if (((year % 4 == 0) && (year % 100 != 0) || (year % 400 == 0)))
        {
            result = true;
        }

        return result;
    }

    //Function to check date format
    public static bool CheckDateFormat(string check_date)
    {
        bool valid = true;
        if (check_date.Trim() == "")
        {
            valid = false;
            return valid;
        }
        string[] dArr = check_date.Split('/');

        int month = Convert.ToInt32(dArr[1]);
        int day = Convert.ToInt32(dArr[0]);
        int year = Convert.ToInt32(dArr[2].Substring(0, 4));

        //check valid month
        if (month > 12)
        {
            //invalid month
            valid = false;
            //valid month
        }
        else
        {
            //check valid day
            switch (month)
            {
                case 4:
                case 6:
                case 9:
                case 11:
                    //30 days
                    //invalid day
                    if (day > 30)
                    {
                        valid = false;
                    }
                    break;
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    //31 days
                    //invalid day
                    if (day > 31)
                    {
                        valid = false;
                    }
                    break;
                case 2:
                    //check leap year
                    if (Check_Leap_Year(year))
                    {
                        //is leap year
                        //invalid day
                        if (day > 29)
                        {
                            valid = false;
                        }
                    }
                    else
                    {
                        //not leap year
                        //invalid day
                        if (day > 28)
                        {
                            valid = false;
                        }
                    }
                    break;
            }
        }
        return valid;
    }

    //Function to check date format (MM/dd/yyyy)
    public static bool CheckDateFormatMDY(string check_date)
    {
        bool valid = true;

        string[] dArr = check_date.Split('/');

        int month = Convert.ToInt32(dArr[0]);
        int day = Convert.ToInt32(dArr[1]);
        int year = Convert.ToInt32(dArr[2].Substring(0, 4));

        //check valid month
        if (month > 12)
        {
            //invalid month
            valid = false;
            //valid month
        }
        else
        {
            //check valid day
            switch (month)
            {
                case 4:
                case 6:
                case 9:
                case 11:
                    //30 days
                    //invalid day
                    if (day > 30)
                    {
                        valid = false;
                    }
                    break;
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    //31 days
                    //invalid day
                    if (day > 31)
                    {
                        valid = false;
                    }
                    break;
                case 2:
                    //check leap year
                    if (Check_Leap_Year(year))
                    {
                        //is leap year
                        //invalid day
                        if (day > 29)
                        {
                            valid = false;
                        }
                    }
                    else
                    {
                        //not leap year
                        //invalid day
                        if (day > 28)
                        {
                            valid = false;
                        }
                    }
                    break;
            }
        }
        return valid;
    }


    //Get Month in Khmer
    public static string Get_month_in_khmer(int month)
    {
        string kh_month = null;

        switch (month)
        {
            case 1:
                kh_month = "មករា";
                break;
            case 2:
                kh_month = "កុម្ភៈ";
                break;
            case 3:
                kh_month = "មីនា";
                break;
            case 4:
                kh_month = "មេសា";
                break;
            case 5:
                kh_month = "ឧសភា";
                break;
            case 6:
                kh_month = "មិថុនា";
                break;
            case 7:
                kh_month = "កក្កដា";
                break;
            case 8:
                kh_month = "សីហា";
                break;
            case 9:
                kh_month = "កញ្ញា";
                break;
            case 10:
                kh_month = "តុលា";
                break;
            case 11:
                kh_month = "វិច្ឆិកា";
                break;
            default:
                kh_month = "ធ្នូ";
                break;
        }

        return kh_month;
    }

    //Get Month in English
    public static string Get_month_in_english(int month)
    {
        string en_month = null;

        switch (month)
        {
            case 1:
                en_month = "January";
                break;
            case 2:
                en_month = "February";
                break;
            case 3:
                en_month = "March";
                break;
            case 4:
                en_month = "April";
                break;
            case 5:
                en_month = "May";
                break;
            case 6:
                en_month = "June";
                break;
            case 7:
                en_month = "July";
                break;
            case 8:
                en_month = "August";
                break;
            case 9:
                en_month = "September";
                break;
            case 10:
                en_month = "October";
                break;
            case 11:
                en_month = "November";
                break;
            default:
                en_month = "December";
                break;
        }

        return en_month;
    }

    //check if numeric
    public static bool IsNumeric(object Expression)
    {
        double retNum;

        bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        return isNum;
    }
   
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="strDate">Input Date Format:dd/MM/yyyy</param>
    /// <returns></returns>
  public static DateTime  FormatDateTime(string strDate){
      DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
      dtfi.ShortDatePattern = "dd/MM/yyyy";
      dtfi.DateSeparator = "/";

     return Convert.ToDateTime(strDate, dtfi);
  
  }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="option">"Text" or "Value"</param>
    /// <param name="ddl"></param>
    /// <param name="value"></param>
    public static void SelectedDropDownListIndex(string option, DropDownList ddl, string value){
        for(int i=0; i<ddl.Items.Count;i++){
            if (option.Trim().ToUpper() == "TEXT")
            {
                if (ddl.Items[i].Text.Trim() == value.Trim())
                {
                    ddl.SelectedIndex = i;
                    break;
                }
            }
            else if (option.Trim().ToUpper() == "VALUE")
            {
                if (ddl.Items[i].Value.Trim() == value.Trim())
                {
                    ddl.SelectedIndex = i;
                    break;
                }
            }
           
        }
    }
    public static void SelectedRadioListIndex(RadioButtonList rbl, string value)
    {
        for (int i = 0; i < rbl.Items.Count; i++)
        {

            if (rbl.Items[i].Value.Trim() == value.Trim())
            {
                rbl.SelectedIndex = i;
                break;
            }
        }
    }
    public static string GetPaymentModeInKhmer(int pay_mode)
    {
        string pay_mode_khmer = "";
        try
        {
            if (pay_mode == 0)//single
            {
                pay_mode_khmer = "បង់តែម្តង";

            }
            else if (pay_mode == 1)//annually
            {
                pay_mode_khmer = "ប្រចាំឆ្នាំ";

            }
            else if (pay_mode == 2)//semi-annul
            {
                pay_mode_khmer = "ប្រចាំឆមាស";

            }
            else if (pay_mode == 3)//quarterly
            {
                pay_mode_khmer = "ប្រចាំត្រីមាស";

            }
            else if (pay_mode == 4)//monthly
            {
                pay_mode_khmer = "ប្រចាំខែ";

            }
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [GetPaymentModeInKhmer] in class [Helper], Detail: " + ex.Message);
        }
        return pay_mode_khmer;
    }
    public static string GetPaymentModeEnglish(int pay_mode)
    {
        string pay_mode_en = "";
        try
        {
            if (pay_mode == 0)//single
            {
                pay_mode_en = "Single";

            }
            else if (pay_mode == 1)//annually
            {
                pay_mode_en = "Annually";

            }
            else if (pay_mode == 2)//semi-annul
            {
                pay_mode_en = "semi-annul";

            }
            else if (pay_mode == 3)//quarterly
            {
                pay_mode_en = "quarterly";

            }
            else if (pay_mode == 4)//monthly
            {
                pay_mode_en = "monthly";

            }
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [GetPaymentModeEnglish] in clase [Helper], Detail: " + ex.Message);
        }
        return pay_mode_en;
    }
    public static int GetPayModeID(string paymode_text)
    {
        int paymode = -1;
        try
        {
            switch (paymode_text.Trim().ToLower())
            {
                case "semi":
                    paymode=2;
                    break;
                case "semi-annual":
                    paymode = 2;
                    break;
                case "annual":
                    paymode = 1;
                    break;
                case "annually":
                    paymode = 1;
                    break;
                case "ann":
                    paymode = 1;
                    break;
                case "single":
                    paymode = 0;
                    break;
                case "quarterly":
                    paymode = 3;
                    break;
                case "monthly":
                    paymode = 4;
                    break;
                default:
                    paymode = -1;
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [GetPayModeID(string paymode_text)] in class [Helper], detail: " + ex.Message);
        }
        return paymode;
    }

    public static int GetIDCardTypeID(string id_type)
    {
        int id = -1;
        try
        {
            switch (id_type.Trim().ToLower())
            {
                case "id_card":
                    id = 0;
                    break;
                case "id card":
                    id = 0;
                    break;
                case "id":
                    id = 0;
                    break;
                case "passport":
                    id = 1;
                    break;
                case "visa":
                    id = 2;
                    break;
                case "birth_certificate":
                    id = 3;
                    break;
                case "birth certificate":
                    id = 3;
                    break;
                case "birth_cert":
                    id = 3;
                    break;
                case "family book":
                    id = 4;
                    break;
                case "proof of resident":
                    id = 5;
                    break;
                case "certificate of identity":
                    id = 6;
                    break;
                case "others":
                    id = 7;
                    break;
                default:
                    id = -1;
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [GetIDCardTypeID(string id_type)] in class [Helper], detail: " + ex.Message);
        }
        return id;
    }
    public static string GetIDCardTypeText(int id_type)
    {
        string id_type_text = "";
        try
        {
            switch (id_type)
            {
                case 0:
                    id_type_text = "ID Card";
                    break;
                
                case 1:
                    id_type_text = "Passport";
                    break;
                case 2:
                    id_type_text = "visa";
                    break;
                case 3:
                    id_type_text = "Birth Certificate";
                    break;
                case 4:
                    id_type_text = "Family Book";
                    break;
                case 5:
                    id_type_text = "Proof of Resident";
                    break;
                case 6:
                    id_type_text = "Certificate of Identity";
                    break;
                case 7:
                    id_type_text = "Others";
                    break;
                default:
                    id_type_text = "";
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [GetIDCardTypeText(int id_type)] in class [Helper], detail: " + ex.Message);
        }
        return id_type_text.ToUpper();
    }
    public static string GetIDCardTypeTextKh(int id_type)
    {
        string id_type_text = "";
        try
        {
            switch (id_type)//: ID/Birth Cert./Passport:     
            {
                case 0:
                    id_type_text = "លេខអត្តសញ្ញាណប័ណ្ណ";
                    break;

                case 1:
                    id_type_text = "លិខិតឆ្លងដែន";
                    break;
                case 2:
                    id_type_text = "visa";
                    break;
                case 3:
                    id_type_text = "សំបុត្រកំណើត";
                    break;
                case 4:
                    id_type_text = "សៀវភៅគ្រួសារ";
                    break;
                case 5:
                    id_type_text = "សៀវភៅស្នាក់នៅ/លិខិតបញ្ជាក់ទីលំនៅ";
                    break;
                case 6:
                    id_type_text = "លិខិតបញ្ជាក់អត្តសញ្ញាណ";
                    break;
                case 7:
                    id_type_text = "ផ្សេងៗ";
                    break;
                default:
                    id_type_text = "";
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [GetIDCardTypeText(int id_type)] in class [Helper], detail: " + ex.Message);
        }
        return id_type_text.ToUpper();
    }

    /// <summary>
    /// Get gender as integer
    /// </summary>
    /// <param name="gender">Gender Text</param>
    /// <returns></returns>
    public static int GetGender(string gender)
    {
        int val = -1;
        try
        {
            switch (gender.Trim().ToLower())
            {
                case "f":
                    val = 0;
                    break;
                case "m":
                    val = 1;
                    break;
                case "male":
                    val = 1;
                    break;
                case "female":
                    val = 0;
                    break;
                case "mr":
                    val = 1;
                    break;
                case "ms":
                    val = 0;
                    break;
                default:
                    val = -1;
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [GetGender(string gender)] in class [Helper], detail: " + ex.Message);
        }
        return val;
    }

    /// <summary>
    /// Get gender as integer
    /// </summary>
    /// <param name="gender">Gender Text</param>
    /// <returns></returns>
    public static int GetCurrancy(string currancy)
    {
        int val = -1;
        try
        {
            switch (currancy.Trim().ToUpper())
            {
                case "USD":
                    val = 0;
                    break;
                case "KHR":
                    val = 1;
                    break;
                default:
                    val = -1;
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [GetCurrancy(string currancy)] in class [Helper], detail: " + ex.Message);
        }
        return val;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="gender"></param>
    /// <param name="full_text">[True=Male or Female], [Fail=M or F]</param>
    /// <returns></returns>
    public static string GetGenderText(int gender, bool full_text, bool as_khmer=false)
    {
        string gender_text = "";
        try
        {
            switch (gender)
            {
                case 0:
                    gender_text = full_text == true ? (as_khmer == true ? "ស្រី" : "Female") : (as_khmer == true ? "ស្រី" : "F");
                    break;
                case 1:
                    gender_text = full_text == true ? (as_khmer == true ? "ប្រុស" : "Male") : (as_khmer == true ? "ប្រុស" : "M");
                    break;
                default:
                    gender_text = "";
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [GetGenderText(int gender, bool full_text)] in class [Helper], detail: " + ex.Message);
        }
        return gender_text;
    }
    public static string GetGenderInKhmer(string GENDER)
    {
        string gender = "";
        switch (GENDER.ToUpper())
        {
            case "M":
                gender = "ប្រុស";
                break;
            case "MALE":
                gender = "ប្រុស";
                break;
            case "F":
                gender = "ស្រី";
                break;
            case "FEMALE":
                gender = "ស្រី";
                break;
            default:
                gender = "";
                break;
        }
        return gender;
    }
    public static string GetMaritalStatusInKhmer(string MARITAL_STATUS_IN_EN)
    {
        string mar_status="";
         switch (MARITAL_STATUS_IN_EN.ToUpper())
            {
                case "SINGLE":
                    mar_status = "នៅលីវ";
                    break;
                case "MARRIED":
                     mar_status = "រៀបការ";
                    break;
                  case "WINDOWER":
                     mar_status = "ពោះម៉ាយ";
                     break;
                  case "WINDOW":
                     mar_status = "មេម៉ាយ";
                     break;
                default:
                    mar_status ="";
                    break;
            }
        return mar_status;
    }
    public static String FormatDec(double value)
    {
        string format = "";

        format = string.Format("{0:n0}", value);
        return format;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="connection_string"></param>
    /// <param name="procedure_name"></param>
    /// <param name="para_value"></param>
    /// <param name="function">Function name.</param>
    /// <returns></returns>
    public static bool ExecuteProcedure(string connection_string, String procedure_name, String[,] para_value, string function)
    {
        bool result = true;
        SqlConnection my_connection = new SqlConnection();
        SqlCommand my_command;
        try
        {

            my_connection = new SqlConnection(connection_string);
            my_connection.Open();

            my_command = new SqlCommand(procedure_name, my_connection);
            my_command.CommandType = CommandType.StoredProcedure;
            //initialize parameters
            for (int i = 0; i <= para_value.GetUpperBound(0); i++)
            {
                my_command.Parameters.AddWithValue(para_value[i, 0], para_value[i, 1]);

            }
            my_command.ExecuteNonQuery();
            my_command.Dispose();
            my_connection.Close();
        }
        catch (Exception ex)
        {
            my_connection.Close();

            Log.AddExceptionToLog("Error function [ExecuteProcedure => " + function + "] in class [Helper], Detail: " + ex.InnerException + "==>" + ex.Message + "==>" + ex.StackTrace,"");
            result = false;
        }
        return result;
    }
    public static bool ExecuteCommand(string connection_string, string query)
    {
        bool result = true;
        SqlConnection my_connection;
        SqlCommand my_command;
        try
        {

            my_connection = new SqlConnection(connection_string);
            my_connection.Open();

            my_command = new SqlCommand(query, my_connection);
            my_command.CommandType = CommandType.Text;
            
            my_command.ExecuteNonQuery();
            my_command.Dispose();
            my_connection.Close();
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [ExecuteCommand] in class [Helper], Detail: " + ex.Message);
            result = false;
        }
        return result;
    }

    public static bool ExecuteCommand(string connection_string, string query, string[,] parameters)
    {
        bool result = true;
        SqlConnection my_connection;
        SqlCommand my_command;
        try
        {

            my_connection = new SqlConnection(connection_string);
            my_connection.Open();

            my_command = new SqlCommand(query, my_connection);

            //initialize parameters
            for (int i = 0; i <= parameters.GetUpperBound(0); i++)
            {
                my_command.Parameters.AddWithValue(parameters[i, 0], parameters[i, 1]);

            }

            my_command.CommandType = CommandType.Text;

            my_command.ExecuteNonQuery();
            my_command.Dispose();
            my_connection.Close();
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [ExecuteCommand (string connection_string, string query, string[,] parameters)] in class [Helper], Detail: " + ex.Message);
            result = false;
        }
        return result;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="connection_string"></param>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="called_by"></param>
    /// <returns></returns>
    public static bool ExecuteCommand(string connection_string, string query, string[,] parameters, string called_by)
    {
        bool result = true;
        SqlConnection my_connection;
        SqlCommand my_command;
        try
        {

            my_connection = new SqlConnection(connection_string);
            my_connection.Open();

            my_command = new SqlCommand(query, my_connection);

            //initialize parameters
            for (int i = 0; i <= parameters.GetUpperBound(0); i++)
            {
                my_command.Parameters.AddWithValue(parameters[i, 0], parameters[i, 1]);

            }

            my_command.CommandType = CommandType.Text;

            my_command.ExecuteNonQuery();
            my_command.Dispose();
            my_connection.Close();
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [ExecuteCommand (string connection_string, string query, string[,] parameters, string called_by)] in class [Helper], called by ["+ called_by+"], Detail: " + ex.Message);
            result = false;
        }
        return result;
    }
    public static int GetAge(DateTime dob, DateTime compare_date)
    {
        int age = 0;
        try
        {
            // get the difference in years
            age = compare_date.Year - dob.Year;
            // subtract another year if we're before the
            // birth day in the current year
            if (compare_date.Month < dob.Month || (compare_date.Month == dob.Month && compare_date.Day < dob.Day))
            {
                age--;
            }
               
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [GetAge] in class [Helper], Detail: " + ex.Message);
            age = 0;
        }
        return age;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="date_string"></param>
    /// <returns></returns>
    public static bool IsDateTime(string date_string)
    {
        bool isdate = true;
        try
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "dd/MM/yyyy HH:mm:ss";
            dtfi.DateSeparator = "/";
           DateTime t= Convert.ToDateTime(date_string, dtfi);
            DateTime dDate;

            if (DateTime.TryParse(t+"", out dDate))
            {
                isdate = true;
            }
            else
            {
                isdate = false;
            }
        }
        catch (Exception ex)
        {
            isdate = false;
            Log.AddExceptionToLog("Error function [IsDateTime(string date_string)] in class [Helper], detail: " + ex.Message);
        }
            return isdate;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="date_string">DD/MM/YYYY</param>
    
    /// <returns></returns>
    
    public static bool IsDate(string date_string)
    {
        bool isdate = true;
        try
        {

            DateTime dDate;

            if (DateTime.TryParse(FormatDateTime(date_string) + "", out dDate))
            {
                isdate = true;
            }
            else
            {
                isdate = false;
            }
        }
        catch (Exception ex)
        {
            isdate = false;
          
           // throw ex;
           Log.AddExceptionToLog("Error function [IsDate(string date_string)] in class [Helper], detail: " + ex.Message);
        }
        return isdate;
    }

    public static bool IsNumber(string input_string)
    {
        bool isnumber = true;
        try
        {
           
            int number;
            bool isNumerical = int.TryParse(input_string, out number);
            if (isNumerical)
            {
                isnumber = true;
            }
            else
            {
                isnumber = false;   
            }
            
        }
        catch (Exception ex)
        {
            isnumber = false;
            Log.AddExceptionToLog("Error function [bool IsNumber(string input_string)] in class [Helper], detail: " + ex.Message);
        }
        return isnumber;
    }
    public static bool IsAmount(string input_string)
    {
        bool isAmount = true;
        try
        {

            double result;
         bool isNumerical=   double.TryParse(input_string, out result);
            if (isNumerical)
            {
                isAmount = true;
            }
            else
            {
                isAmount = false;
            }

        }
        catch (Exception ex)
        {
            isAmount = false;
            Log.AddExceptionToLog("Error function [IsAmount(string input_string)] in class [Helper], detail: " + ex.Message);
        }
        return isAmount;
    }
    #region Excel
    public class excel
    {
        public static string[] HeaderText { get; set; }
        public static HSSFSheet Sheet { get; set; }
        public static string[] Title { get; set; }
        public static int NewRowIndex
        {
            get
            {
                return Title.Length + 1; //1 is the row of header

            }

        }
        public static void generateHeader()
        {
            int title_no = 0;
            //Title
            if (Title != null)
            {
                title_no = Title.Length;
                for (int j = 0; j < Title.Length; j++)
                {
                    HSSFRow row0 = (HSSFRow)Sheet.CreateRow(j);
                    HSSFCell cell0 = (HSSFCell)row0.CreateCell(0);
                    cell0.SetCellValue(Title[j].ToString());
                }
            }
            else
            {
                title_no = 0;
            }
            //column header
            HSSFRow row1 = (HSSFRow)Sheet.CreateRow(title_no);
            for (int i = 0; i < HeaderText.Length; i++)
            {
                HSSFCell cell = (HSSFCell)row1.CreateCell(i);
                cell.SetCellValue(HeaderText[i].ToString());

            }
        }
    }
    #endregion

    #region Page information
    public static string GetPageName()
    {
        string page_name = "";
        try
        {
            string full_page = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string[] arr_path = full_page.Split('/');
            if (arr_path.Length > 0)
            {
                int last_part_index = arr_path.Length - 1;
                page_name = arr_path[last_part_index].ToString();
            }
        }
        catch(Exception ex)
        {
            Log.AddExceptionToLog("Error function [GetPageName] in class [Helper], Detail: " + ex.Message);
        }
        return page_name;
    }
    #endregion

    #region
    /// <summary>
    /// Get list of premium due date.
    /// </summary>
    /// <param name="effective_date">Policy effective date is the first due date of policy</param>
    /// <param name="pay_mode">Mode of policy premium payment [0=single, 1=annually, 2=semi, 3=quarter, 4=monthly]</param>
    /// <returns></returns>
    public static List<DateTime> GetDueDateList(DateTime effective_date, int pay_mode)
    {
        List<DateTime> dueList = new List<DateTime>();
        try
        {
            int time_payment = 0;
            DateTime firstDue = effective_date;
            DateTime nextDue = new DateTime();
            DateTime dueDate = new DateTime();

            nextDue = firstDue;

            //get times of payment
            switch (pay_mode)
            {
                case 0:
                case 1:
                    time_payment = 1;
                    break;
                case 2:
                    time_payment = 2;
                    break;
                case 3:
                    time_payment = 4;
                    break;
                case 4:
                    time_payment = 12;
                    break;
            }

            //First due date
            dueList.Add(firstDue);
           
            //loop time of payment
            for (int i = 1; i <= time_payment; i++)
            {
                switch (pay_mode)
                {
                    case 0:
                    case 1:
                        nextDue = nextDue.AddYears(1);//every year
                        break;
                    case 2:
                        nextDue = nextDue.AddMonths(6);//every 6 months
                        break;
                    case 3:
                        nextDue = nextDue.AddMonths(3);//every 3 months
                        break;
                    case 4:
                        nextDue = nextDue.AddMonths(1);// every moneth
                        break;
                }
                if (time_payment==1)//single and annaully
                {
                    dueDate = Calculation.GetNext_Due(nextDue, firstDue, effective_date);
                    dueList.Add(dueDate);
                }
                else if(time_payment> i)
                {
                    dueDate = Calculation.GetNext_Due(nextDue, firstDue, effective_date);
                    dueList.Add(dueDate);
                }
                
            }
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [GetDueDateList(DateTime effective_date, int pay_mode)] in class [Helper], Detail: " + ex.Message);
        }
        return dueList;
    }
    #endregion

    public static bool IsLockedOut(string user_name)
    {
        bool status = false;
        try
        {
            status = new DB().Execute(AppConfiguration.GetAccountConnectionString(), "aspnet_Membership_UnlockUser", new string[,] { { "@ApplicationName", "CLIFE" }, { "@UserName", user_name } });
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error functin [IsLockedOut(string user_name)] in page [authentication], detail:" + ex.Message);
            status = false;
        }
        return status;
    }

    public static void Alert(bool is_error, string message, Label LBL)
    {
        ShowMsgBox(is_error == true ? "ERROR" : "INFORMATION", message, LBL, "white", is_error == true ? "red" : "black", is_error == true ? "error" : "header");
        
    }     

    private static void ShowMsgBox(string H, string C, Label L , string BGCOLOR, string FORECOLOR, string CL)
    {
   StringBuilder sb =new StringBuilder();
   sb.Append("<div id='msgbox' class='dialog'>");
            sb.Append("<div id='tr'>&nbsp;</div>");
            sb.Append("<div id='main' style='color:" + FORECOLOR + ";background-color:" + BGCOLOR + "'>");
            sb.Append("<span class='" + CL + "'>" + H + "</span><hr>");
            sb.Append("<p id='content'>" + C + "</p><hr>");
            sb.Append("<p><input type='button' class='btnsmall' CauseValidation='false' value='OK'");
           
            sb.AppendLine("onclick=$('#msgbox').fadeOut();></input>");
            //sb.AppendLine("onclick=$('#msgbox').hide();></input>");
            sb.Append("</div>");
            sb.Append("</script>");
            L.Text = sb.ToString();


    }
    public enum ClientType
    {
        SELF,CO_BORROWER, CLIENT_FAMILY, BANK_STAFF, BANK_STAFF_FAMILY, REPAYMENT, INDIVIDUAL
    }


}