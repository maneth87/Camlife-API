using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for da_micro_product_rider
/// </summary>
public class da_micro_product_rider
{
	public da_micro_product_rider()
	{
		//
		// TODO: Add constructor logic here
		//
        MESSAGE = "";
        SUCCESS = false;
	}
    public static string MESSAGE;
    public static bool SUCCESS;
    public static bl_micro_product_rider GetMicroProductByProductID(string PRODUCT_ID)
    {
        bl_micro_product_rider obj = new bl_micro_product_rider();
        try
        {
            DB db = new DB();

            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_PRODUCT_RIDER_GET_BY_PRODUCT_ID", new string[,] { 
            {"@PRODUCT_ID", PRODUCT_ID}
            }, "da_micro_product_rider => GetMicroProductByProductID(string PRODUCT_ID)");

            if (db.RowEffect == -1)//error
            {
                MESSAGE = db.Message;
                SUCCESS = false;
            }
            else
            {
                if (tbl.Rows.Count > 0)
                {
                    var row = tbl.Rows[0];
                    obj = new bl_micro_product_rider()
                    {
                        PRODUCT_MICRO_RIDER_ID = row["PRODUCT_MICRO_RIDER_ID"].ToString(),
                        PRODUCT_ID = row["PRODUCT_ID"].ToString(),
                        EN_TITLE = row["EN_TITLE"].ToString(),
                        EN_ABBR = row["EN_ABBR"].ToString(),
                        KH_TITLE = row["KH_TITLE"].ToString(),
                        AGE_MIN = Convert.ToInt32(row["AGE_MIN"].ToString()),
                        AGE_MAX = Convert.ToInt32(row["AGE_MAX"].ToString()),
                        SUM_ASSURE_MIN = Convert.ToInt32(row["SUM_MIN"].ToString()),
                        SUM_ASSURE_MIX = Convert.ToInt32(row["SUM_MAX"].ToString()),
                        REMARKS = row["REMARKS"].ToString(),
                        CREATED_BY = row["CREATED_BY"].ToString(),
                        CREATED_ON = Convert.ToDateTime(row["CREATED_ON"].ToString())

                    };
                    SUCCESS = true;
                    MESSAGE = "Success";
                }
                else
                {
                    SUCCESS = true;
                    MESSAGE = "No Records Found.";
                }
            }
            
        }
        catch (Exception ex)
        {
            obj = new bl_micro_product_rider();
            MESSAGE = ex.Message;
            SUCCESS = false;
            Log.AddExceptionToLog("Error function [GetMicroProductByProductID(string PRODUCT_ID)] in class [da_micro_product_rider], detail: " + ex.Message + ex.StackTrace);

        }

        return obj;
    }
    public static List< bl_micro_product_rider> GetMicroProductList()
    {
        List<bl_micro_product_rider> listObj = new List<bl_micro_product_rider>();
        try
        {
            DB db = new DB();

            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_PRODUCT_RIDER_GET", new string[,] { 
            
            }, "da_micro_product_rider => GetMicroProductList()");

            if (db.RowEffect == -1)//error
            {
                MESSAGE = db.Message;
                SUCCESS = false;
            }
            else
            {
                if (tbl.Rows.Count > 0)
                {
                   // var row = tbl.Rows[0];
                    foreach (DataRow row in tbl.Rows)
                    {
                        listObj.Add(new bl_micro_product_rider()
                        {
                            PRODUCT_MICRO_RIDER_ID = row["PRODUCT_MICRO_RIDER_ID"].ToString(),
                            PRODUCT_ID = row["PRODUCT_ID"].ToString(),
                            EN_TITLE = row["EN_TITLE"].ToString(),
                            EN_ABBR = row["EN_ABBR"].ToString(),
                            KH_TITLE = row["KH_TITLE"].ToString(),
                            AGE_MIN = Convert.ToInt32(row["AGE_MIN"].ToString()),
                            AGE_MAX = Convert.ToInt32(row["AGE_MAX"].ToString()),
                            SUM_ASSURE_MIN = Convert.ToInt32(row["SUM_MIN"].ToString()),
                            SUM_ASSURE_MIX = Convert.ToInt32(row["SUM_MAX"].ToString()),
                            REMARKS = row["REMARKS"].ToString(),
                            CREATED_BY = row["CREATED_BY"].ToString(),
                            CREATED_ON = Convert.ToDateTime(row["CREATED_ON"].ToString())

                        });
                    }
                    SUCCESS = true;
                    MESSAGE = "Success";
                }
                else
                {
                    SUCCESS = true;
                    MESSAGE = "No Records Found.";
                }
            }

        }
        catch (Exception ex)
        {
            listObj = new List<bl_micro_product_rider>();
            MESSAGE = ex.Message;
            SUCCESS = false;
            Log.AddExceptionToLog("Error function [GetMicroProductList()] in class [da_micro_product_rider], detail: " + ex.Message + ex.StackTrace);

        }

        return listObj;
    }
    /// <summary>
    /// Get Miro product list by En title 
    /// </summary>
    /// <param name="productName"></param>
    /// <returns></returns>
    public static List<bl_micro_product_rider> GetMicroProductList(string productName)
    {
        List<bl_micro_product_rider> listObj = new List<bl_micro_product_rider>();
        try
        {
            DB db = new DB();

            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_PRODUCT_RIDER_GET_BY_RIDER_NAME", new string[,] { 
            {"@rider_name", productName}
            }, "da_micro_product_rider => GetMicroProductList(string productName)");

            if (db.RowEffect == -1)//error
            {
                MESSAGE = db.Message;
                SUCCESS = false;
            }
            else
            {
                if (tbl.Rows.Count > 0)
                {
                   // var row = tbl.Rows[0];
                    foreach (DataRow row in tbl.Rows)
                    {
                        listObj.Add(new bl_micro_product_rider()
                        {
                            PRODUCT_MICRO_RIDER_ID = row["PRODUCT_MICRO_RIDER_ID"].ToString(),
                            PRODUCT_ID = row["PRODUCT_ID"].ToString(),
                            EN_TITLE = row["EN_TITLE"].ToString(),
                            EN_ABBR = row["EN_ABBR"].ToString(),
                            KH_TITLE = row["KH_TITLE"].ToString(),
                            AGE_MIN = Convert.ToInt32(row["AGE_MIN"].ToString()),
                            AGE_MAX = Convert.ToInt32(row["AGE_MAX"].ToString()),
                            SUM_ASSURE_MIN = Convert.ToInt32(row["SUM_MIN"].ToString()),
                            SUM_ASSURE_MIX = Convert.ToInt32(row["SUM_MAX"].ToString()),
                            REMARKS = row["REMARKS"].ToString(),
                            CREATED_BY = row["CREATED_BY"].ToString(),
                            CREATED_ON = Convert.ToDateTime(row["CREATED_ON"].ToString())

                        });
                    }
                    SUCCESS = true;
                    MESSAGE = "Success";
                }
                else
                {
                    SUCCESS = true;
                    MESSAGE = "No Records Found.";
                }
            }

        }
        catch (Exception ex)
        {
            listObj = new List<bl_micro_product_rider>();
            MESSAGE = ex.Message;
            SUCCESS = false;
            Log.AddExceptionToLog("Error function [GetMicroProductList(string productName)] in class [da_micro_product_rider], detail: " + ex.Message + ex.StackTrace);

        }

        return listObj;
    }
    public static bool Save(bl_micro_product_rider rider)
    {
        bool result = false;
        try
        {
            DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_PRODUCT_RIDER_INSERT", new string[,] { 
            {"@ID", rider.PRODUCT_MICRO_RIDER_ID},{"@PRODUCT_ID", rider.PRODUCT_ID},{"@EN_TITLE",rider.EN_TITLE},
            {"@EN_ABBR",rider.EN_ABBR},{"@KH_TITLE",rider.KH_TITLE},{"@AGE_MIN",rider.AGE_MIN+""},
            {"@AGE_MAX",rider.AGE_MAX+""}, {"@SUM_MIN",rider.SUM_ASSURE_MIN+""}, {"@SUM_MAX",rider.SUM_ASSURE_MIX+""},
            {"@REMARKS",rider.REMARKS},{"@CREATED_BY",rider.CREATED_BY},{"@CREATED_ON",rider.CREATED_ON+""}
            }, "da_micro_product_rider=>Save(bl_micro_product_rider rider)");
            if (db.RowEffect == -1)
            {
                MESSAGE = db.Message;
            }
        }
        catch (Exception ex)
        {
            result = false;
            MESSAGE = ex.Message;
            SUCCESS = false;
            Log.AddExceptionToLog("Error function [Save(bl_micro_product_rider rider)] in class [da_micro_product_rider], detail: " + ex.Message + ex.StackTrace);
        }
        return result;
    }

    /// <summary>
    /// update rider product by  id
    /// </summary>
    /// <param name="rider"></param>
    /// <returns></returns>
    public static bool Update(bl_micro_product_rider rider)
    {
        bool result = false;
        try
        {
            DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_PRODUCT_RIDER_UPDATE", new string[,] { 
            {"@ID", rider.PRODUCT_MICRO_RIDER_ID},
            {"@PRODUCT_ID", rider.PRODUCT_ID},{"@EN_TITLE",rider.EN_TITLE},
            {"@EN_ABBR",rider.EN_ABBR},{"@KH_TITLE",rider.KH_TITLE},{"@AGE_MIN",rider.AGE_MIN+""},
            {"@AGE_MAX",rider.AGE_MAX+""}, {"@SUM_MIN",rider.SUM_ASSURE_MIN+""}, {"@SUM_MAX",rider.SUM_ASSURE_MIX+""},
            {"@REMARKS",rider.REMARKS},{"@UPDATED_BY",rider.UPDATED_BY},{"@UPDATED_ON",rider.UPDATED_ON+""}, {"@UPDATED_REMARKS", rider.UPDATED_REMARKS}
            }, "da_micro_product_rider=>Update(bl_micro_product_rider rider)");
            if (db.RowEffect == -1)
            {
                MESSAGE = db.Message;
            }
        }
        catch (Exception ex)
        {
            result = false;
            MESSAGE = ex.Message;
            SUCCESS = false;
            Log.AddExceptionToLog("Error function [Update(bl_micro_product_rider rider)] in class [da_micro_product_rider], detail: " + ex.Message + ex.StackTrace);
        }
        return result;
    }
}