using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for da_micro_product_rate
/// </summary>
public class da_micro_product_rate
{
    public da_micro_product_rate()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="PRODUCT_ID"></param>
    /// <param name="GENDER"></param>
    /// <param name="AGE"></param>
    /// <param name="SUM_ASSURE"></param>
    /// <returns></returns>
    public static bl_micro_product_rate GetProductRate(string PRODUCT_ID, int GENDER, int AGE, double SUM_ASSURE, int paymode)
    {
        bl_micro_product_rate rate = new bl_micro_product_rate();
        try
        {
            DB db = new DB();

            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_Ct_MICRO_PRODUCT_RATE_GET", new string[,] { 
            {"@PRODUCT_ID", PRODUCT_ID},{"@GENDER", GENDER+""},{"@CURR_AGE", AGE+""},{"@SUM_ASSURE",  SUM_ASSURE+""},{"@PAYMENT_MODE", paymode+"" }
            }, "da_micro_product_rate => GetProductRate(string PRODUCT_ID, int GENDER, int AGE)");

            if (tbl.Rows.Count > 0)
            {
                var row = tbl.Rows[0];
                rate = new bl_micro_product_rate()
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
            rate = new bl_micro_product_rate();
            Log.AddExceptionToLog("Error function[GetProductRate(string PRODUCT_ID, int GENDER, int AGE)] in class [da_micro_product_rate], detail: " + ex.Message + "==>" + ex.StackTrace);

        }
        return rate;
    }

   
    public static List<bl_micro_product_rate> GetProductRateList()
    {
        List<bl_micro_product_rate> rate = new List<bl_micro_product_rate>();
        try
        {
            DB db = new DB();

            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_Ct_MICRO_PRODUCT_RATE_GET_LIST", new string[,] { 
            }, "da_micro_product_rate => GetProductRateList()");

            if (tbl.Rows.Count > 0)
            {
                foreach (DataRow row in tbl.Rows)
                {
                    rate.Add(new bl_micro_product_rate()
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
            rate = new List<bl_micro_product_rate>();
            Log.AddExceptionToLog("Error function[GetProductRateList()] in class [da_micro_product_rate], detail: " + ex.Message + "==>" + ex.StackTrace);

        }
        return rate;
    }

    public static List<bl_micro_product_rate> GetProductRateList(string productId)
    {
        List<bl_micro_product_rate> rate = new List<bl_micro_product_rate>();
        try
        {
            DB db = new DB();

            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_Ct_MICRO_PRODUCT_RATE_GET_LIST_BY_PRODUCT_ID", new string[,] { 
                {"@product_id", productId}
            }, "da_micro_product_rate => GetProductRateList(string productId)");

            if (tbl.Rows.Count > 0)
            {
                foreach (DataRow row in tbl.Rows)
                {
                    rate.Add(new bl_micro_product_rate()
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
            rate = new List<bl_micro_product_rate>();
            Log.AddExceptionToLog("Error function[GetProductRateList(string productId)] in class [da_micro_product_rate], detail: " + ex.Message + "==>" + ex.StackTrace);

        }
        return rate;
    }

    public static bool Save(bl_micro_product_rate rate)
    {
        bool result = false;
        try
        {
            DB db = new DB();

            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_PRODUCT_RATE_INSERT", new string[,]{
            {"@PRODUCT_RATE_ID", rate.PRODUCT_RATE_ID}
            , {"@PRODUCT_ID", rate.PRODUCT_ID}
            ,{"@GENDER", rate.GENDER+""}
            , {"@PAY_MODE", rate.PAY_MODE+""}
            , {"@AGE_MIN", rate.AGE_MIN+""}
            , {"@AGE_MAX", rate.AGE_MAX+""}
            , {"@SA_MIN", rate.SUM_ASSURE_START+""}
            , {"@SA_MAX", rate.SUM_ASSURE_END+""}
            , {"@RATE_PER", rate.RATE_PER+""}
            , {"@RATE_TYPE", rate.RATE_TYPE}
            , {"@RATE", rate.RATE+""}
            , {"@REMARKS", rate.REMARKS}
            , {"@CREATED_BY", rate.CREATED_BY}
            , {"@CREATED_ON", rate.CREATED_ON+""}
            }, "da_micro_product_rider_rate=>Save(bl_micro_product_rate rate)");
        }
        catch (Exception ex)
        {
            result = false;
            Log.AddExceptionToLog("Error function[Save(bl_micro_product_rate rate)] in class [da_micro_product_rider_rate], detail: " + ex.Message + "==>" + ex.StackTrace);

        }
        return result;

    }

    /// <summary>
    /// Update Rider rate by product rate id
    /// </summary>
    /// <param name="riderRate"></param>
    /// <returns></returns>
    public static bool Update(bl_micro_product_rate rate)
    {
        bool result = false;
        try
        {
            DB db = new DB();

            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_PRODUCT_RATE_UPDATE", new string[,]{
            {"@PRODUCT_RATE_ID", rate.PRODUCT_RATE_ID}
            , {"@PRODUCT_ID", rate.PRODUCT_ID}
            ,{"@GENDER", rate.GENDER+""}
            , {"@PAY_MODE", rate.PAY_MODE+""}
            , {"@AGE_MIN", rate.AGE_MIN+""}
            , {"@AGE_MAX", rate.AGE_MAX+""}
            , {"@SA_MIN", rate.SUM_ASSURE_START+""}
            , {"@SA_MAX", rate.SUM_ASSURE_END+""}
            , {"@RATE_PER", rate.RATE_PER+""}
            , {"@RATE_TYPE", rate.RATE_TYPE}
            , {"@RATE", rate.RATE+""}
            , {"@REMARKS", rate.REMARKS}
            , {"@UPDATED_BY", rate.UPDATED_BY}
            , {"@UPDATED_ON", rate.UPDATED_ON+""}
            ,{"@UPDATED_REMARKS", rate.UPDATED_REMARKS}
            }, "da_micro_product_rider_rate=>Update(bl_micro_product_rate rate)");
        }
        catch (Exception ex)
        {
            result = false;
            Log.AddExceptionToLog("Error function[Update(bl_micro_product_rate rate)] in class [da_micro_product_rider_rate], detail: " + ex.Message + "==>" + ex.StackTrace);

        }
        return result;

    }

}