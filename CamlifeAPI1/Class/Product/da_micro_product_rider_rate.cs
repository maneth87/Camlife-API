using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for da_micro_product_rider_rate
/// </summary>
public class da_micro_product_rider_rate
{
	public da_micro_product_rider_rate()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    /// <summary>
    /// Get rider rate by condition
    /// </summary>
    /// <param name="PRODUCT_ID"></param>
    /// <param name="GENDER"></param>
    /// <param name="AGE"></param>
    /// <param name="SUM_ASSURE"></param>
    /// <returns></returns>
    public static bl_micro_product_rider_rate GetProductRate(string PRODUCT_ID, int GENDER, int AGE, double SUM_ASSURE, int paymentMode)
    {
        bl_micro_product_rider_rate rate = new bl_micro_product_rider_rate();
        try
        {
            DB db = new DB();

            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_PRODUCT_RIDER_RATE_GET", new string[,] { 
            {"@PRODUCT_ID", PRODUCT_ID},{"@GENDER", GENDER+""},{"@CURR_AGE", AGE+""},{"@SUM_ASSURE",  SUM_ASSURE+""},{"@PAYMENT_MODE", paymentMode+"" }
            }, "da_micro_product_rider_rate => GetProductRate(string PRODUCT_ID, int GENDER, int AGE)");

            if (tbl.Rows.Count > 0)
            {
                var row = tbl.Rows[0];
                rate = new bl_micro_product_rider_rate()
                {
                    PRODUCT_RATE_ID = row["product_rate_id"].ToString(),
                    PRODUCT_ID = row["product_id"].ToString(),
                    PAY_MODE = Convert.ToInt32(row["pay_mode"].ToString()),
                    GENDER = Convert.ToInt32(row["gender"].ToString()),
                    AGE_MIN = Convert.ToInt32(row["age_min"].ToString()),
                    AGE_MAX = Convert.ToInt32(row["age_max"].ToString()),
                    SUM_ASSURE_START = Convert.ToDouble(row["sum_assure_start"].ToString()),
                    SUM_ASSURE_END = Convert.ToDouble(row["sum_assure_end"].ToString()),
                    RATE = Convert.ToDouble(row["rate"].ToString()),
                    RATE_PER = Convert.ToDouble(row["rate_per"].ToString()),
                    RATE_TYPE = row["rate_type"].ToString(),
                    CREATED_BY = row["created_by"].ToString(),
                    CREATED_ON = Convert.ToDateTime(row["created_on"].ToString()),
                    REMARKS = row["remarks"].ToString()
                };
            }

        }
        catch (Exception ex)
        {
            rate = new bl_micro_product_rider_rate();
            Log.AddExceptionToLog("Error function[GetProductRate(string PRODUCT_ID, int GENDER, int AGE)] in class [da_micro_product_rider_rate], detail: " + ex.Message + "==>" + ex.StackTrace);

        }
        return rate;
    }

    /// <summary>
    /// Get rider rate list
    /// </summary>
    /// <returns></returns>
    public static List<bl_micro_product_rider_rate> GetProductRate()
    {
        List<bl_micro_product_rider_rate> rateList = new List<bl_micro_product_rider_rate>();
        try
        {
            DB db = new DB();

            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_PRODUCT_RIDER_RATE_GET_LIST", new string[,] { 
            }, "da_micro_product_rider_rate => GetProductRate()");

            if (tbl.Rows.Count > 0)
            {
                foreach (DataRow row in tbl.Rows)
                {
                    rateList.Add(new bl_micro_product_rider_rate()
                    {
                        PRODUCT_RATE_ID = row["product_rate_id"].ToString(),
                        PRODUCT_ID = row["product_id"].ToString(),
                        PAY_MODE = Convert.ToInt32(row["pay_mode"].ToString()),
                        GENDER = Convert.ToInt32(row["gender"].ToString()),
                        AGE_MIN = Convert.ToInt32(row["age_min"].ToString()),
                        AGE_MAX = Convert.ToInt32(row["age_max"].ToString()),
                        SUM_ASSURE_START = Convert.ToDouble(row["sum_assure_start"].ToString()),
                        SUM_ASSURE_END = Convert.ToDouble(row["sum_assure_end"].ToString()),
                        RATE = Convert.ToDouble(row["rate"].ToString()),
                        RATE_PER = Convert.ToDouble(row["rate_per"].ToString()),
                        RATE_TYPE = row["rate_type"].ToString(),
                        CREATED_BY = row["created_by"].ToString(),
                        CREATED_ON = Convert.ToDateTime(row["created_on"].ToString()),
                        REMARKS = row["remarks"].ToString()
                    });
                }
            }

        }
        catch (Exception ex)
        {
            rateList = new List<bl_micro_product_rider_rate>();
            Log.AddExceptionToLog("Error function[GetProductRate()] in class [da_micro_product_rider_rate], detail: " + ex.Message + "==>" + ex.StackTrace);

        }
        return rateList;
    }
    public static List<bl_micro_product_rider_rate> GetProductRate(string productId)
    {
        List<bl_micro_product_rider_rate> rateList = new List<bl_micro_product_rider_rate>();
        try
        {
            DB db = new DB();

            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_PRODUCT_RIDER_RATE_GET_LIST_BY_PRODUCT_ID", new string[,] { 
                {"@product_id", productId}
            }, "da_micro_product_rider_rate => GetProductRate(string productId)");

            if (tbl.Rows.Count > 0)
            {
                foreach (DataRow row in tbl.Rows)
                {
                    rateList.Add(new bl_micro_product_rider_rate()
                    {
                        PRODUCT_RATE_ID = row["product_rate_id"].ToString(),
                        PRODUCT_ID = row["product_id"].ToString(),
                        PAY_MODE = Convert.ToInt32(row["pay_mode"].ToString()),
                        GENDER = Convert.ToInt32(row["gender"].ToString()),
                        AGE_MIN = Convert.ToInt32(row["age_min"].ToString()),
                        AGE_MAX = Convert.ToInt32(row["age_max"].ToString()),
                        SUM_ASSURE_START = Convert.ToDouble(row["sum_assure_start"].ToString()),
                        SUM_ASSURE_END = Convert.ToDouble(row["sum_assure_end"].ToString()),
                        RATE = Convert.ToDouble(row["rate"].ToString()),
                        RATE_PER = Convert.ToDouble(row["rate_per"].ToString()),
                        RATE_TYPE = row["rate_type"].ToString(),
                        CREATED_BY = row["created_by"].ToString(),
                        CREATED_ON = Convert.ToDateTime(row["created_on"].ToString()),
                        REMARKS = row["remarks"].ToString()
                    });
                }
            }

        }
        catch (Exception ex)
        {
            rateList = new List<bl_micro_product_rider_rate>();
            Log.AddExceptionToLog("Error function[GetProductRate(string productId)] in class [da_micro_product_rider_rate], detail: " + ex.Message + "==>" + ex.StackTrace);

        }
        return rateList;
    }

    public static bool Save(bl_micro_product_rider_rate riderRate)
    {
        bool result = false;
        try
        {
            DB db = new DB();
          
          result=  db.Execute(AppConfiguration.GetConnectionString(),"SP_CT_MICRO_PRODUCT_RIDER_RATE_INSERT", new string [,]{
            {"@PRODUCT_RATE_ID", riderRate.PRODUCT_RATE_ID}
            , {"@PRODUCT_ID", riderRate.PRODUCT_ID}
            ,{"@GENDER", riderRate.GENDER+""}
            , {"@PAY_MODE", riderRate.PAY_MODE+""}
            , {"@AGE_MIN", riderRate.AGE_MIN+""}
            , {"@AGE_MAX", riderRate.AGE_MAX+""}
            , {"@SA_MIN", riderRate.SUM_ASSURE_START+""}
            , {"@SA_MAX", riderRate.SUM_ASSURE_END+""}
            , {"@RATE_PER", riderRate.RATE_PER+""}
            , {"@RATE_TYPE", riderRate.RATE_TYPE}
            , {"@RATE", riderRate.RATE+""}
            , {"@REMARKS", riderRate.REMARKS}
            , {"@CREATED_BY", riderRate.CREATED_BY}
            , {"@CREATED_ON", riderRate.CREATED_ON+""}
            }, "da_micro_product_rider_rate=>Save(bl_micro_product_rider_rate riderRate)");
        }
        catch (Exception ex)
        {
            result = false;
            Log.AddExceptionToLog("Error function[Save(bl_micro_product_rider_rate riderRate)] in class [da_micro_product_rider_rate], detail: " + ex.Message + "==>" + ex.StackTrace);

        }
        return result;
    
    }

    /// <summary>
    /// Update Rider rate by product rate id
    /// </summary>
    /// <param name="riderRate"></param>
    /// <returns></returns>
    public static bool Update(bl_micro_product_rider_rate riderRate)
    {
        bool result = false;
        try
        {
            DB db = new DB();

            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_PRODUCT_RIDER_RATE_UPDATE", new string[,]{
            {"@PRODUCT_RATE_ID", riderRate.PRODUCT_RATE_ID}
            , {"@PRODUCT_ID", riderRate.PRODUCT_ID}
            ,{"@GENDER", riderRate.GENDER+""}
            , {"@PAY_MODE", riderRate.PAY_MODE+""}
            , {"@AGE_MIN", riderRate.AGE_MIN+""}
            , {"@AGE_MAX", riderRate.AGE_MAX+""}
            , {"@SA_MIN", riderRate.SUM_ASSURE_START+""}
            , {"@SA_MAX", riderRate.SUM_ASSURE_END+""}
            , {"@RATE_PER", riderRate.RATE_PER+""}
            , {"@RATE_TYPE", riderRate.RATE_TYPE}
            , {"@RATE", riderRate.RATE+""}
            , {"@REMARKS", riderRate.REMARKS}
            , {"@UPDATED_BY", riderRate.UPDATED_BY}
            , {"@UPDATED_ON", riderRate.UPDATED_ON+""}
            ,{"@UPDATED_REMARKS", riderRate.UPDATED_REMARKS}
            }, "da_micro_product_rider_rate=>Update(bl_micro_product_rider_rate riderRate)");
        }
        catch (Exception ex)
        {
            result = false;
            Log.AddExceptionToLog("Error function[Update(bl_micro_product_rider_rate riderRate)] in class [da_micro_product_rider_rate], detail: " + ex.Message + "==>" + ex.StackTrace);

        }
        return result;

    }
}