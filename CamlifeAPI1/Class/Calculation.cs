using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using static Calculation;

/// <summary>
/// Summary description for Calculation
/// </summary>
public class Calculation
{
    #region "Constructor(s)"

    private static Calculation mytitle = null;
    public Calculation()
    {
        if (mytitle == null)
        {
            mytitle = new Calculation();
        }

    }
    #endregion

    #region "Public Functions"
   
    //Calculate GTLI premium old rate (effective date < 01-02-2015)
    public static decimal CalculateGTLIPremium(System.DateTime dob, string gender, decimal sum_insured, string product_id, int number_of_staff, System.DateTime effective_date)
    {
        decimal premium = 0;
        decimal myRate = default(decimal);

        string connString = AppConfiguration.GetConnectionString();

        TimeSpan mytimespan = effective_date.Subtract(dob);
        int no_of_day = mytimespan.Days;

        //Get leap year count
        int number_of_leap_year = Helper.Get_Number_Of_Leap_Year(dob.Year, effective_date.Year);

        int age_band = Convert.ToInt32(Math.Floor(Convert.ToDecimal((no_of_day - number_of_leap_year) / 365)));

        try
        {
            using (SqlConnection myConnection = new SqlConnection(connString))
            {
                //Get premium rate by age and product ID

                SqlCommand myCommand = new SqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SP_Get_GTLI_Premium_Rate";
                myCommand.Parameters.AddWithValue("@Age_Band", age_band);
                myCommand.Parameters.AddWithValue("@Gender", gender);
                myCommand.Parameters.AddWithValue("@Product_ID", product_id);

                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {
                        myRate = Convert.ToDecimal(myReader.GetString(myReader.GetOrdinal("Rate")));
                    }
                    myReader.Close();
                }

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();

                //Normal rate for any number of staffs

                if (number_of_staff < 20)
                {
                    premium = ((myRate * sum_insured) / 100) * Convert.ToDecimal(2.07); // 2.07 factor
                }
                else if (number_of_staff >= 20 & number_of_staff <= 29)
                {
                    premium = ((myRate * sum_insured) / 100) * Convert.ToDecimal(2.07);// 2.07 factor
                }
                else if (number_of_staff >= 30 & number_of_staff <= 39)
                {
                    premium = ((myRate * sum_insured) / 100) * Convert.ToDecimal(1.46);// 1.46 factor
                }
                else if (number_of_staff >= 40 & number_of_staff <= 49)
                {
                    premium = ((myRate * sum_insured) / 100) * Convert.ToDecimal(1.13);// 1.13 factor
                }
                else if (number_of_staff >= 50)
                {
                    premium = (myRate * sum_insured) / 100;
                    //Normal rate for 50 staffs or more
                }

                ////over age get new rate by: maneth
                //if (myRate == 0) {
                //    string newProductID = "";
                   
                //    //premium life
                //    if(product_id=="8") {
                 
                //        newProductID = "11";
                //    }
                //        //100plus
                //    else if (product_id == "10") {

                //        newProductID = "12";
                //    }
                //        //tpd
                //    else if (product_id == "9") {

                //        newProductID = "13";
                //    }

                //    premium = Calculation.CalculateGTLINewPremiumRate3(dob, gender, sum_insured, newProductID, number_of_staff, effective_date);
                    
                //}//end over age

            }
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error calculating GTLI premium. Function [CalculateGTLIPremium], class [Calculation]. Details: " + ex.Message);
        }

        return Math.Ceiling(premium);
    }

    //Calculate GTLI premium New Rate
    public static decimal CalculateGTLINewPremiumRate(System.DateTime dob, string gender, decimal sum_insured, string product_id, int number_of_staff, System.DateTime effective_date)
    {
        decimal premium = 0;
        decimal myRate = default(decimal);

        string connString = AppConfiguration.GetConnectionString();

        TimeSpan mytimespan = effective_date.Subtract(dob);
        int no_of_day = mytimespan.Days;

        //Get leap year count
        int number_of_leap_year = Helper.Get_Number_Of_Leap_Year(dob.Year, effective_date.Year);

        int age_band = Convert.ToInt32(Math.Floor(Convert.ToDecimal((no_of_day - number_of_leap_year) / 365)));

        try
        {
            using (SqlConnection myConnection = new SqlConnection(connString))
            {
                //Get premium rate by age and product ID

                SqlCommand myCommand = new SqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SP_Get_GTLI_New_Premium_Rate";
                myCommand.Parameters.AddWithValue("@Age_Band", age_band);
                myCommand.Parameters.AddWithValue("@Gender", gender);
                myCommand.Parameters.AddWithValue("@Product_ID", product_id);
                myCommand.Parameters.AddWithValue("@Effective_Date", effective_date);

                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {
                        myRate = Convert.ToDecimal(myReader.GetString(myReader.GetOrdinal("Rate")));
                    }
                    myReader.Close();
                }

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();

                //Normal rate for any number of staffs

                if (number_of_staff < 20)
                {
                    premium = ((myRate * sum_insured) / 1000) * Convert.ToDecimal(2.07);
                }
                else if (number_of_staff >= 20 & number_of_staff <= 29)
                {
                    premium = ((myRate * sum_insured) / 1000) * Convert.ToDecimal(2.07);
                }
                else if (number_of_staff >= 30 & number_of_staff <= 39)
                {
                    premium = ((myRate * sum_insured) / 1000) * Convert.ToDecimal(1.46);
                }
                else if (number_of_staff >= 40 & number_of_staff <= 49)
                {
                    premium = ((myRate * sum_insured) / 1000) * Convert.ToDecimal(1.13);
                }
                else if (number_of_staff >= 50)
                {
                    premium = (myRate * sum_insured) / 1000;
                    //Normal rate for 50 staffs or more
                }

                ////over age get new rate by: maneth
                //if (myRate == 0)
                //{
                //    string newProductID = "";

                //    //premium life
                //    if (product_id == "8")
                //    {

                //        newProductID = "11";
                //    }
                //    //100plus
                //    else if (product_id == "10")
                //    {

                //        newProductID = "12";
                //    }
                //    //tpd
                //    else if (product_id == "9")
                //    {

                //        newProductID = "13";
                //    }

                //    premium = Calculation.CalculateGTLINewPremiumRate3(dob, gender, sum_insured, newProductID, number_of_staff, effective_date);

                //}//end over age
            }
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error calculating GTLI premium. Function [CalculateGTLINewPremiumRate], class [Calculation]. Details: " + ex.Message);
        }

        return Math.Ceiling(premium);
    }

    //Calculate GTLI premium New Rate 2
    public static decimal CalculateGTLINewPremiumRate2(System.DateTime dob, string gender, decimal sum_insured, string product_id, int number_of_staff, System.DateTime effective_date)
    {
        decimal premium = 0;
        decimal myRate = default(decimal);

        string connString = AppConfiguration.GetConnectionString();

        TimeSpan mytimespan = effective_date.Subtract(dob);
        int no_of_day = mytimespan.Days;

        //Get leap year count
        int number_of_leap_year = Helper.Get_Number_Of_Leap_Year(dob.Year, effective_date.Year);

        int age_band = Convert.ToInt32(Math.Floor(Convert.ToDecimal((no_of_day - number_of_leap_year) / 365)));

        try
        {
            using (SqlConnection myConnection = new SqlConnection(connString))
            {
                //Get premium rate by age and product ID

                SqlCommand myCommand = new SqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SP_Get_GTLI_New_Premium_Rate_2";
                myCommand.Parameters.AddWithValue("@Age_Band", age_band);
                myCommand.Parameters.AddWithValue("@Gender", gender);
                myCommand.Parameters.AddWithValue("@Product_ID", product_id);
                myCommand.Parameters.AddWithValue("@Effective_Date", effective_date);

                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {
                        myRate = Convert.ToDecimal(myReader.GetString(myReader.GetOrdinal("Rate")));
                    }
                    myReader.Close();
                }

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();


                //Discount for number of staffs
                //if (number_of_staff >= 10 && number_of_staff < 51)
                //{
                //    //Factor
                //    if (number_of_staff < 15)
                //    {
                //        premium = ((myRate * sum_insured) / 1000) * Convert.ToDecimal(1.5);
                //    }
                //    else
                //    {
                //        premium = ((myRate * sum_insured) / 1000);
                //    }
                //}
                //else if (number_of_staff >= 51 & number_of_staff <= 100)
                //{
                //    premium = ((myRate * sum_insured) / 1000) - (((myRate * sum_insured) / 1000) * Convert.ToDecimal(0.05));

                //    //No Discount
                //   // premium = ((myRate * sum_insured) / 1000); 
                //}
                //else if (number_of_staff > 100)
                //{
                //    premium = ((myRate * sum_insured) / 1000) - (((myRate * sum_insured) / 1000) * Convert.ToDecimal(0.1));
                //}

                if (number_of_staff >= 10 && number_of_staff < 51)
                {
                   
                        premium = ((myRate * sum_insured) / 1000);
                  
                }
                else if (number_of_staff >= 51 & number_of_staff <= 100)
                {
                    premium = ((myRate * sum_insured) / 1000);

                    //No Discount
                    // premium = ((myRate * sum_insured) / 1000); 
                }
                else if (number_of_staff > 100)
                {
                    premium = ((myRate * sum_insured) / 1000);
                }

                ////over age get new rate by: maneth
                //if (myRate == 0)
                //{
                //    string newProductID = "";

                //    //premium life
                //    if (product_id == "8")
                //    {

                //        newProductID = "11";
                //    }
                //    //100plus
                //    else if (product_id == "10")
                //    {

                //        newProductID = "12";
                //    }
                //    //tpd
                //    else if (product_id == "9")
                //    {

                //        newProductID = "13";
                //    }

                //    premium = Calculation.CalculateGTLINewPremiumRate3(dob, gender, sum_insured, newProductID, number_of_staff, effective_date);

                //}//end over age
            }
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error calculating GTLI premium. Function [CalculateGTLINewPremiumRate2], class [Calculation]. Details: " + ex.Message);
        }

        return Math.Ceiling(premium);
    }

    //Calculate GTLI premium New Rate 3: effective from 12/april/2016
    public static decimal CalculateGTLINewPremiumRate3(System.DateTime dob, string gender, decimal sum_insured, string product_id, int number_of_staff, System.DateTime effective_date)
    {
        decimal premium = 0;
        decimal myRate = default(decimal);

        string connString = AppConfiguration.GetConnectionString();

        TimeSpan mytimespan = effective_date.Subtract(dob);
        int no_of_day = mytimespan.Days;

        //Get leap year count
        int number_of_leap_year = Helper.Get_Number_Of_Leap_Year(dob.Year, effective_date.Year);

        int age_band = Convert.ToInt32(Math.Floor(Convert.ToDecimal((no_of_day - number_of_leap_year) / 365)));

        try
        {
            using (SqlConnection myConnection = new SqlConnection(connString))
            {
                //Get premium rate by age and product ID

                SqlCommand myCommand = new SqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SP_Get_GTLI_New_Premium_Rate_3";
                myCommand.Parameters.AddWithValue("@Age_Band", age_band);
                myCommand.Parameters.AddWithValue("@Gender", gender);
                myCommand.Parameters.AddWithValue("@Product_ID", product_id);
                myCommand.Parameters.AddWithValue("@Effective_Date", effective_date);

                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {
                        myRate = Convert.ToDecimal(myReader.GetString(myReader.GetOrdinal("Rate")));
                    }
                    myReader.Close();
                }

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();


                if (number_of_staff >= 20 && number_of_staff < 51)
                {

                    premium = ((myRate * sum_insured) / 1000);

                }
                else if (number_of_staff >= 51 & number_of_staff <= 100)
                {
                    premium = ((myRate * sum_insured) / 1000);

                }
                else if (number_of_staff > 100)
                {
                    premium = ((myRate * sum_insured) / 1000);
                }

            }
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error calculating GTLI premium. Function [CalculateGTLINewPremiumRate3], class [Calculation]. Details: " + ex.Message);
        }

        return Math.Ceiling(premium);
    }

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

    //Get Customer Age
    public static int Culculate_Customer_Age(string strdob, DateTime compare_date)
    {
        DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
        dtfi.ShortDatePattern = "dd/MM/yyyy";
        dtfi.DateSeparator = "/";
        int customer_age = 0;
        try
        {
            DateTime dob = Convert.ToDateTime(strdob, dtfi);

            TimeSpan mytimespan = compare_date.Subtract(dob);
            int no_of_day = mytimespan.Days;

            //Get leap year count
            int number_of_leap_year = Get_Number_Of_Leap_Year(dob.Year, compare_date.Year);

            double result = (Convert.ToDouble(no_of_day) - Convert.ToDouble(number_of_leap_year)) / 365;

            if (dob.Month.Equals(compare_date.Month) && dob.Day.Equals(compare_date.Day))
            {
                //round .99 age
                double round_result = Math.Ceiling(result);

                //minus rould result 0.1
                double sub_result = round_result - 0.02;

                //if result ~ #.99 then round up
                if (result >= sub_result)
                {
                    result = Math.Ceiling(result);
                }
            }
            else
            {
                result = Math.Floor(result);
            }


            customer_age = Convert.ToInt32(result);
        }
        catch (Exception ex)
        {
            customer_age = 0;
            Log.AddExceptionToLog("Error function [Culculate_Customer_Age(string strdob, DateTime compare_date)] in class [Calculation], detail: " + ex.Message);
        }
        return customer_age;


    }

    //Get MRTA Assure Year
    public static int Culculate_MRTA_Assure_Year(DateTime current_date, DateTime loan_effective_date, int term_loan)
    {
        int assure_year = 0;
        int no_of_day = 0;

        //Get leap year count
        if (current_date.CompareTo(loan_effective_date) == 1)
        {
            TimeSpan mytimespan = current_date.Subtract(loan_effective_date);

            no_of_day = mytimespan.Days;

            decimal result = (no_of_day) / 365;

            int substract_year = Convert.ToInt32(Math.Floor(result));

            assure_year = term_loan - substract_year;
        }
        else
        {
            assure_year = term_loan;
        }


        return assure_year;

    }

    //Function to find Used Days (GTLI)
    public static int CalculateUsedDays(System.DateTime sale_date, System.DateTime date_of_modify)
    {
        TimeSpan mytimespan = date_of_modify.Subtract(sale_date);
        int used_days = mytimespan.Days;
        return used_days;
    }

    //Function to calculate total points for Point Reward
    public static double CalculateTotalPoint(double premium, int point)
    {
        double total_point = (premium * point) / 100;
        return Convert.ToDouble(String.Format("{0:#0.##}", total_point));
    }

    //Function to calculate total cash premium from points for Point Reward
    public static double CalculateTotalPremium(double total_point, int point)
    {
        double total_premium = (total_point * 100) / point;
        return Convert.ToDouble(String.Format("{0:#0.##}", total_point));
    }

    //Get Customer Age Micro
    public static int Culculate_Customer_Age_Micro(DateTime dob, DateTime compare_date)
    {

        TimeSpan mytimespan = compare_date.Subtract(dob);
        int no_of_day = mytimespan.Days;

        //Get leap year count
        int number_of_leap_year = Get_Number_Of_Leap_Year(dob.Year, compare_date.Year);

        double result = (Convert.ToDouble(no_of_day) - Convert.ToDouble(number_of_leap_year)) / 365;

        if (dob.Month.Equals(compare_date.Month) && dob.Day.Equals(compare_date.Day))
        {
            //round .99 age
            double round_result = Math.Ceiling(result);

            //minus rould result 0.1
            double sub_result = round_result - 0.02;

            //if result ~ #.99 then round up
            if (result >= sub_result)
            {
                result = Math.Ceiling(result);
            }
        }
        else
        {
            result = Math.Floor(result);
        }

        int customer_age = Convert.ToInt32(result);

        

        return customer_age;

    }

    //Get GTLI Premium tax amount
    public static decimal Calculate_GTLI_Premium_Tax(decimal original_premium, decimal discount)
    {
        decimal tax_amount = Convert.ToDecimal(((original_premium - discount) * Convert.ToDecimal(0.05)).ToString("N2"));

        return tax_amount;
    }

    //Get GTLI Premium After Discount & Tax
    public static decimal Calculate_GTLI_Premium_After_Discount_And_Tax(decimal original_premium, decimal discount, decimal tax_amount)
    {              
        decimal premium_after_tax = (original_premium - discount) + tax_amount;

        return premium_after_tax;
    }

    public static DateTime GetNext_Due(DateTime next_due, DateTime due_date, DateTime effective_date)
    {
        DateTime result = DateTime.Now;
        try
        {

            result = next_due;

            int i = next_due.Year, checking_day_per_month = 0, add_days = 0;

            if (((i % 4 == 0) && (i % 100 != 0) || (i % 400 == 0))) // Leap Year, 29 last day of Feb
            {
                if (next_due.Month == 2)
                {
                    if (next_due.Day >= 29)
                    {
                        if (due_date.Day == 28)
                        {
                            result = next_due.AddDays(1);
                        }
                        else { result = next_due; }
                    }
                    else { result = next_due; }
                }
                else if (next_due.Month == 1 || next_due.Month == 3 || next_due.Month == 5 || next_due.Month == 7 || next_due.Month == 8 || next_due.Month == 11 || next_due.Month == 12 || next_due.Month == 10)
                {
                    if (next_due.Day >= 31)
                    {
                        if (effective_date.Day < next_due.Day)
                        {
                            result = next_due.AddDays(-1);
                        }
                        else { result = next_due; }
                    }
                    else
                    {
                        checking_day_per_month = DateTime.DaysInMonth(next_due.Year, next_due.Month);

                        if (effective_date.Day > next_due.Day)
                        {
                            add_days = effective_date.Day - next_due.Day;

                            if (checking_day_per_month == next_due.Day)
                            {
                                result = next_due;
                            }
                            else
                            {
                                result = next_due.AddDays(add_days); //due_date = next_due.AddDays(1);
                            }
                        }
                        else if (effective_date.Day < next_due.Day)
                        {
                            result = next_due.AddDays(-(next_due.Day - effective_date.Day));
                        }
                        else
                        {
                            result = next_due;
                        }
                    }
                }
                else
                {
                    result = next_due;
                }
            }
            else
            {
                if (next_due.Month == 1 || next_due.Month == 3 || next_due.Month == 5 || next_due.Month == 7 || next_due.Month == 8 || next_due.Month == 11 || next_due.Month == 12 || next_due.Month == 10)
                {
                    if (next_due.Day >= 31)
                    {
                        if (effective_date.Day < next_due.Day)
                        {
                            result = next_due.AddDays(-1);
                        }
                        else { result = next_due; }
                    }
                    else
                    {
                        checking_day_per_month = DateTime.DaysInMonth(next_due.Year, next_due.Month);

                        if (effective_date.Day > next_due.Day)
                        {
                            add_days = effective_date.Day - next_due.Day;

                            if (checking_day_per_month == next_due.Day)
                            {
                                result = next_due;
                            }
                            else
                            {
                                result = next_due.AddDays(add_days); //due_date = next_due.AddDays(1);
                            }
                        }
                        else if (effective_date.Day < next_due.Day)
                        {
                            result = next_due.AddDays(-(next_due.Day - effective_date.Day));
                        }
                        else
                        {
                            result = next_due;
                        }
                    }
                }
                else
                {
                    result = next_due;
                }
            }
        }
        catch (Exception ex)
        {
            //write error to log
            Log.AddExceptionToLog("Error: class [da_Calculation], function [GetNext_Due]. Details: " + ex.Message);
        }

        return result;
    }

    #endregion
    #region

    public static DateTime Culculate_Maturity_Date(DateTime effective_date, DateTime policy_pay_mode_date)
    {
        DateTime result = DateTime.Now;
        try
        {


        }
        catch (Exception ex)
        {
            //write error to log
            Log.AddExceptionToLog("Error: class [da_policy_prem_pay], function [CheckDayNextDueWithEffectiveDay]. Details: " + ex.Message);
        }

        return result;
    }


    #endregion

    #region miscro product SO
    /// <summary>
    /// Return [annualy premium , premium by pay mode]
    /// </summary>
    /// <param name="productID"></param>
    /// <param name="gender"></param>
    /// <param name="age"></param>
    /// <param name="SumAssured"></param>
    /// <param name="paymode"></param>
    /// <returns></returns>
    public static Premium GetMicroProducPremium(string productID, int gender, int age, double SumAssured, int paymode)
    {
        #region old Calc
        /*
        int divide = -1;
        bl_micro_product_rate basic;
        string[] splitBasic = new string[] { };
        string[] splitAnnual = new string[] { };
        double basic1, basic2, basic2LastDegitCheck;
        double annualPremium, PremiumByPaymode;
        string basic2stDegit, basic2LastDegit;
        var premium =new Premium();
        try
        {
            switch (paymode)
            {
                case 1:
                    divide = 1;
                    break;
                case 2:
                    divide = 2;
                    break;
                case 3:
                    divide = 4;
                    break;
                case 4:
                    divide = 12;
                    break;
                default:
                    divide = -1;
                    break;

            }
            basic = da_micro_product_rate.GetProductRate(productID, gender, age, SumAssured);
            if (basic.PRODUCT_ID != null)
            {
                if (divide > 0)
                {
                    if (basic.RATE_TYPE.ToUpper() == "VALUE")
                    {
                        // annualPremium = basic.RATE;
                        if (SumAssured > basic.RATE_PER)
                        {
                            int t = Convert.ToInt32(SumAssured / basic.RATE_PER);
                            annualPremium = basic.RATE * t;
                        }
                        else
                        {
                            annualPremium = basic.RATE;
                        }
                    }
                    else
                    {
                        annualPremium = (basic.RATE * SumAssured) / basic.RATE_PER;
                    }


                    if (divide != 1)
                    {
                        // basic_premium = Math.Round((rateByPaymode * SA) / basic.RATE_PER, 3);
                        PremiumByPaymode = Math.Round(annualPremium / divide, 3, MidpointRounding.AwayFromZero);
                        splitBasic = PremiumByPaymode.ToString().Split('.');
                        basic1 = Convert.ToDouble(splitBasic[0]);
                        basic2 = Convert.ToDouble(splitBasic[1]);
                        basic2stDegit = basic2.ToString().Substring(0, 1);
                        basic2LastDegit = basic2.ToString().Substring(basic2stDegit.Length, basic2.ToString().Length - basic2stDegit.Length);
                        basic2LastDegitCheck = Convert.ToDouble("0.0" + basic2LastDegit.ToString());

                        if (basic2LastDegitCheck >= 0.05)
                        {
                            PremiumByPaymode = Convert.ToDouble(basic1.ToString() + "." + basic2stDegit.ToString()) + 0.10;
                        }
                        else
                        {
                            PremiumByPaymode = Convert.ToDouble(basic1.ToString() + "." + basic2stDegit.ToString()) + 0.05;
                        }
                        PremiumByPaymode = Math.Round(PremiumByPaymode, 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        PremiumByPaymode = annualPremium;
                    }
                  
                    premium.AnnualPremium = annualPremium;
                    premium.PremiumByPaymode = PremiumByPaymode;
                }

            }

        }
        catch (Exception ex)
        {
            premium = new Premium();
            Log.AddExceptionToLog("Error function [GetMicroProducPremium(string productID, int gender, int age, double SumAssured)] in class [Calculation], detail:" + ex.Message + "=>" + ex.StackTrace);
        }

        return premium;
        */
        #endregion 

        #region new calc
        int divide = -1;
        bl_micro_product_rate basic;
        string[] splitBasic = new string[] { };
        string[] splitAnnual = new string[] { };
        double basic1, basic2, basic2LastDegitCheck;
        double annualPremium, PremiumByPaymode;
        string basic2stDegit, basic2LastDegit;
        var premium = new Premium();
        try
        {
            switch (paymode)
            {
                case 1:
                    divide = 1;
                    break;
                case 2:
                    divide = 2;
                    break;
                case 3:
                    divide = 4;
                    break;
                case 4:
                    divide = 12;
                    break;
                default:
                    divide = -1;
                    break;

            }
            basic = da_micro_product_rate.GetProductRate(productID, gender, age, SumAssured, paymode);
            if (basic.PRODUCT_ID != null)
            {
                if (divide > 0)
                {
                    if (basic.RATE_TYPE.ToUpper() == "VALUE")
                    {
                        // annualPremium = basic.RATE;
                        if (SumAssured > basic.RATE_PER)
                        {
                            int t = Convert.ToInt32(SumAssured / basic.RATE_PER);
                            PremiumByPaymode = basic.RATE * t;

                        }
                        else
                        {
                            PremiumByPaymode = basic.RATE;
                        }
                        annualPremium = PremiumByPaymode * divide;
                    }
                    else if (basic.RATE_TYPE.ToUpper() == "RATE")
                    {
                        PremiumByPaymode = (basic.RATE * SumAssured) / basic.RATE_PER;

                        if (divide != 1)
                        {

                            PremiumByPaymode = Math.Round(PremiumByPaymode, 3, MidpointRounding.AwayFromZero);
                            splitBasic = PremiumByPaymode.ToString().Split('.');
                            basic1 = Convert.ToDouble(splitBasic[0]);
                            basic2 = Convert.ToDouble(splitBasic[1]);
                            basic2stDegit = basic2.ToString().Substring(0, 1);
                            basic2LastDegit = basic2.ToString().Substring(basic2stDegit.Length, basic2.ToString().Length - basic2stDegit.Length);
                            basic2LastDegitCheck = Convert.ToDouble("0.0" + basic2LastDegit.ToString());

                            if (basic2LastDegitCheck >= 0.05)
                            {
                                PremiumByPaymode = Convert.ToDouble(basic1.ToString() + "." + basic2stDegit.ToString()) + 0.10;
                            }
                            else
                            {
                                PremiumByPaymode = Convert.ToDouble(basic1.ToString() + "." + basic2stDegit.ToString()) + 0.05;
                            }
                            PremiumByPaymode = Math.Round(PremiumByPaymode, 2, MidpointRounding.AwayFromZero);
                        }

                        annualPremium = Math.Round(PremiumByPaymode, 2, MidpointRounding.AwayFromZero) * divide;
                    }
                    else {

                        PremiumByPaymode = Math.Round(SumAssured * (basic.RATE / basic.RATE_PER), 3, MidpointRounding.AwayFromZero);
                        annualPremium = Math.Round(PremiumByPaymode * (double)divide, 3, MidpointRounding.AwayFromZero);
                        
                    }
                    premium.PremiumByPaymode = PremiumByPaymode;
                    premium.AnnualPremium = annualPremium;
                }

            }

        }
        catch (Exception ex)
        {
            premium = new Premium();
            Log.AddExceptionToLog("Error function [GetMicroProducPremium(string productID, int gender, int age, double SumAssured)] in class [Calculation], detail:" + ex.Message + "=>" + ex.StackTrace);
        }

        return premium;
        #endregion

    }

    public static Premium GetMicroProductRiderPremium(string productID, int gender, int age, double SumAssured, int paymode)
    {
        #region Old Calc
        /*
        var premium = new Premium();
        int divide = -1;

        string[] splitBasic = new string[] { };

        double basic1, basic2, basic2LastDegitCheck;
        double annualPremium, PremiumByPaymode;
        string basic2stDegit, basic2LastDegit;
        bl_micro_product_rider_rate rate;
        try
        {

            switch (paymode)
            {
                case 1:
                    divide = 1;
                    break;
                case 2:
                    divide = 2;
                    break;
                case 3:
                    divide = 4;
                    break;
                case 4:
                    divide = 12;
                    break;
                default:
                    divide = -1;
                    break;

            }
            rate = da_micro_product_rider_rate.GetProductRate(productID, gender, age, SumAssured);
            if (rate.PRODUCT_ID != null)
            {
                if (divide > 0)
                {
                    if (rate.RATE_TYPE == "VALUE")
                    {
                        if (SumAssured > rate.RATE_PER)
                        {
                            int t = Convert.ToInt32(SumAssured / rate.RATE_PER);
                            annualPremium = rate.RATE * t;
                        }
                        else
                        {
                            annualPremium = rate.RATE;
                        }

                        // annualPremium = rate.RATE;
                    }
                    else
                    {
                        annualPremium = (rate.RATE * SumAssured) / rate.RATE_PER;
                    }
                    if (divide != 1)
                    {
                        // basic_premium = Math.Round((rateByPaymode * SA) / basic.RATE_PER, 3);
                        PremiumByPaymode = Math.Round(annualPremium / divide, 3, MidpointRounding.AwayFromZero);
                        splitBasic = PremiumByPaymode.ToString().Split('.');
                        basic1 = Convert.ToDouble(splitBasic[0]);
                        basic2 = Convert.ToDouble(splitBasic[1]);
                        basic2stDegit = basic2.ToString().Substring(0, 1);
                        basic2LastDegit = basic2.ToString().Substring(basic2stDegit.Length, basic2.ToString().Length - basic2stDegit.Length);
                        basic2LastDegitCheck = Convert.ToDouble("0.0" + basic2LastDegit.ToString());

                        if (basic2LastDegitCheck >= 0.05)
                        {
                            PremiumByPaymode = Convert.ToDouble(basic1.ToString() + "." + basic2stDegit.ToString()) + 0.10;
                        }
                        else
                        {
                            PremiumByPaymode = Convert.ToDouble(basic1.ToString() + "." + basic2stDegit.ToString()) + 0.05;
                        }
                        PremiumByPaymode = Math.Round(PremiumByPaymode, 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        PremiumByPaymode = annualPremium;
                    }
                    premium.AnnualPremium = annualPremium;
                    premium.PremiumByPaymode = PremiumByPaymode;
                }
            }

        }
        catch (Exception ex)
        {
            premium = new Premium();
            Log.AddExceptionToLog("Error function [GetMicroProductRiderPremium(string productID, int gender, int age, double SumAssured)] in class [Calculation], detail:" + ex.Message + "=>" + ex.StackTrace);
        }

        return premium;
        */
        #endregion

        #region new calc
        var premium = new Premium();
        int divide = -1;

        string[] splitBasic = new string[] { };

        double basic1, basic2, basic2LastDegitCheck;
        double annualPremium, PremiumByPaymode;
        string basic2stDegit, basic2LastDegit;
        bl_micro_product_rider_rate rate;
        try
        {

            switch (paymode)
            {
                case 1:
                    divide = 1;
                    break;
                case 2:
                    divide = 2;
                    break;
                case 3:
                    divide = 4;
                    break;
                case 4:
                    divide = 12;
                    break;
                default:
                    divide = -1;
                    break;

            }
            rate = da_micro_product_rider_rate.GetProductRate(productID, gender, age, SumAssured,paymode);
            if (rate.PRODUCT_ID != null)
            {
                if (divide > 0)
                {
                    if (rate.RATE_TYPE == "VALUE")
                    {
                        if (SumAssured > rate.RATE_PER)
                        {
                            int t = Convert.ToInt32(SumAssured / rate.RATE_PER);
                            PremiumByPaymode = rate.RATE * t;
                        }
                        else
                        {
                            PremiumByPaymode = rate.RATE;
                        }
                        annualPremium = PremiumByPaymode * divide;
                    }
                    else if (rate.RATE_TYPE.ToUpper()=="RATE")
                    {
                        PremiumByPaymode = (rate.RATE * SumAssured) / rate.RATE_PER;
                        if (divide != 1)
                        {
                            PremiumByPaymode = Math.Round(PremiumByPaymode, 3, MidpointRounding.AwayFromZero);
                            splitBasic = PremiumByPaymode.ToString().Split('.');
                            basic1 = Convert.ToDouble(splitBasic[0]);
                            basic2 = Convert.ToDouble(splitBasic[1]);
                            basic2stDegit = basic2.ToString().Substring(0, 1);
                            basic2LastDegit = basic2.ToString().Substring(basic2stDegit.Length, basic2.ToString().Length - basic2stDegit.Length);
                            basic2LastDegitCheck = Convert.ToDouble("0.0" + basic2LastDegit.ToString());

                            if (basic2LastDegitCheck >= 0.05)
                            {
                                PremiumByPaymode = Convert.ToDouble(basic1.ToString() + "." + basic2stDegit.ToString()) + 0.10;
                            }
                            else
                            {
                                PremiumByPaymode = Convert.ToDouble(basic1.ToString() + "." + basic2stDegit.ToString()) + 0.05;
                            }
                            PremiumByPaymode = Math.Round(PremiumByPaymode, 2, MidpointRounding.AwayFromZero);
                        }

                        annualPremium = Math.Round(PremiumByPaymode, 2, MidpointRounding.AwayFromZero) * divide;
                    }
                    else
                    {

                        PremiumByPaymode = Math.Round(SumAssured * (rate.RATE / rate.RATE_PER), 3, MidpointRounding.AwayFromZero);
                        annualPremium = Math.Round(PremiumByPaymode * (double)divide, 3, MidpointRounding.AwayFromZero);

                    }
                    premium.PremiumByPaymode = PremiumByPaymode;
                    premium.AnnualPremium = annualPremium;
                }
            }

        }
        catch (Exception ex)
        {
            premium = new Premium();
            Log.AddExceptionToLog("Error function [GetMicroProductRiderPremium(string productID, int gender, int age, double SumAssured)] in class [Calculation], detail:" + ex.Message + "=>" + ex.StackTrace);
        }

        return premium;
        #endregion
    }


    public class Premium
    { 
        public double AnnualPremium { get; set; }
        public double PremiumByPaymode { get; set;}
    }
   
    #endregion

}