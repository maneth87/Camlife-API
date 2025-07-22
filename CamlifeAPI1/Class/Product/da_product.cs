using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for da_product
/// </summary>
public class da_product
{
    private static da_product mytitle = null;
    public da_product()
    {
        if (mytitle == null)
        {
            mytitle = new da_product();
        }

    }
    
    #region "Public Functions"
    
    //Function to get product list by product_type
    public static List<bl_product> GetProductsByProductType(string product_type)
    {
        List<bl_product> product_list = new List<bl_product>();

        string connString = AppConfiguration.GetConnectionString();

        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand("SP_Get_Product_List_By_Product_Type", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramName = new SqlParameter();
            paramName.ParameterName = "@ProductTypeID";
            paramName.Value = product_type;
            cmd.Parameters.Add(paramName);

            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                
                if (rdr.HasRows)
                {

                    bl_product product = new bl_product();

                    product.Product_ID = rdr.GetString(rdr.GetOrdinal("Product_ID"));
                    product.Age_Max = rdr.GetInt32(rdr.GetOrdinal("Age_Max"));
                    product.Age_Min = rdr.GetInt32(rdr.GetOrdinal("Age_Min"));
                    product.En_Abbr = rdr.GetString(rdr.GetOrdinal("En_Abbr"));
                    product.En_Title = rdr.GetString(rdr.GetOrdinal("En_Title"));
                    product.Kh_Abbr = rdr.GetString(rdr.GetOrdinal("Kh_Abbr"));
                    product.Kh_Title = rdr.GetString(rdr.GetOrdinal("Kh_Title"));
                    product.Plan_Block = rdr.GetString(rdr.GetOrdinal("Plan_Block"));
                    product.Plan_Code = rdr.GetString(rdr.GetOrdinal("Plan_Code"));
                    product.Plan_Code2 = rdr.GetString(rdr.GetOrdinal("Plan_Code2"));
                    product.Plan_Type = rdr.GetString(rdr.GetOrdinal("Plan_Type"));
                    product.Product_Type_ID = rdr.GetInt32(rdr.GetOrdinal("Product_Type_ID"));
                    product.Sum_Max = rdr.GetDouble(rdr.GetOrdinal("Sum_Max"));
                    product.Sum_Min = rdr.GetDouble(rdr.GetOrdinal("Sum_Min"));
                    product.Remarks = rdr["remarks"].ToString();
                    if (product.Product_ID != "T1011")
                    {
                        product_list.Add(product);
                    }
                    
                }

            }
            con.Close();
        }        
      
        return product_list;
    }

    //Function to get pay year by product_id
    public static int GetPayYearByProductID(string product_id)
    {
        int pay_year = 0;

        string connString = AppConfiguration.GetConnectionString();

        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand("SP_Get_PayYear_By_Product_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramName = new SqlParameter();
            paramName.ParameterName = "@ProductID";
            paramName.Value = product_id;
            cmd.Parameters.Add(paramName);

            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {

                if (rdr.HasRows)
                {
                    pay_year = rdr.GetInt32(rdr.GetOrdinal("Pay_Year"));
               
                }

            }
            con.Close();
        }       
        return pay_year;
    }

    //Function to get assure year by product_id
    public static int GetAssureYearByProductID(string product_id)
    {
        int assure_year = 0;

        string connString = AppConfiguration.GetConnectionString();

        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand("SP_Get_AssureYear_By_Product_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramName = new SqlParameter();
            paramName.ParameterName = "@ProductID";
            paramName.Value = product_id;
            cmd.Parameters.Add(paramName);

            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {

                if (rdr.HasRows)
                {
                    assure_year = rdr.GetInt32(rdr.GetOrdinal("Assure_Year"));

                }

            }
            con.Close();
        }
        return assure_year;
    }

     //Function to get product by product_id
    public static bl_product GetProductByProductID(string product_id)
    {
        bl_product product = new bl_product();
        try
        {
            string connString = AppConfiguration.GetConnectionString();

            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SP_Get_Product_By_Product_ID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@ProductID";
                paramName.Value = product_id;
                cmd.Parameters.Add(paramName);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    if (rdr.HasRows)
                    {
                        product.Product_ID = rdr.GetString(rdr.GetOrdinal("Product_ID"));
                        product.Age_Max = rdr.GetInt32(rdr.GetOrdinal("Age_Max"));
                        product.Age_Min = rdr.GetInt32(rdr.GetOrdinal("Age_Min"));
                        product.En_Abbr = rdr.GetString(rdr.GetOrdinal("En_Abbr"));
                        product.En_Title = rdr.GetString(rdr.GetOrdinal("En_Title"));
                        product.Kh_Abbr = rdr.GetString(rdr.GetOrdinal("Kh_Abbr"));
                        product.Kh_Title = rdr.GetString(rdr.GetOrdinal("Kh_Title"));
                        product.Plan_Block = rdr.GetString(rdr.GetOrdinal("Plan_Block"));
                        product.Plan_Code = rdr.GetString(rdr.GetOrdinal("Plan_Code"));
                        product.Plan_Code2 = rdr.GetString(rdr.GetOrdinal("Plan_Code2"));
                        product.Plan_Type = rdr.GetString(rdr.GetOrdinal("Plan_Type"));
                        product.Product_Type_ID = rdr.GetInt32(rdr.GetOrdinal("Product_Type_ID"));
                        product.Sum_Max = rdr.GetDouble(rdr.GetOrdinal("Sum_Max"));
                        product.Sum_Min = rdr.GetDouble(rdr.GetOrdinal("Sum_Min"));

                    }

                }
                con.Close();
            }
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [GetProductByProductID(string product_id)] in class [da_product], detail: " + ex.Message);
            product = new bl_product();
        }
        return product;
    }

   
    #endregion

    #region Add by maneth @01-02-24
    public static List< bl_product> GetProductList()
    {
        List<bl_product> proList = new List<bl_product>();
        bl_product product;
        try
        {
            DataTable tbl = new DataTable();
            DB db = new DB();
            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_PRODUCT_GET_LIST", new string[,] { }, "da_proucd=>GetProductList()");
            foreach (DataRow r in tbl.Rows)
            {
                product = new bl_product();
                product.Product_ID = r["Product_ID"].ToString();
                product.Age_Max = Convert.ToInt32(r["Age_Max"].ToString());
                product.Age_Min = Convert.ToInt32(r["Age_Min"].ToString());
                product.En_Abbr = r["En_Abbr"].ToString();
                product.En_Title = r["En_Title"].ToString();
                product.Kh_Abbr = r["Kh_Abbr"].ToString();
                product.Kh_Title = r["Kh_Title"].ToString();
                product.Plan_Block = r["Plan_Block"].ToString();
                product.Plan_Code = r["Plan_Code"].ToString();
                product.Plan_Code2 = r["Plan_Code2"].ToString();
                product.Plan_Type = r["Plan_Type"].ToString();
                product.Product_Type_ID = Convert.ToInt32(r["Product_Type_ID"].ToString());
                product.Sum_Max = Convert.ToInt32(r["sum_max"].ToString());
                product.Sum_Min = Convert.ToInt32(r["sum_min"].ToString());
                product.Remarks = r["remarks"].ToString();
                proList.Add(product);
            }
           
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [GetProductList()] in class [da_product], detail: " + ex.Message);
           proList=new List<bl_product>();
        }
        return proList;
    }
    public static List<bl_product> GetProductList(string productName)
    {
        List<bl_product> proList = new List<bl_product>();
        bl_product product;
        try
        {
            DataTable tbl = new DataTable();
            DB db = new DB();
            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_PRODUCT_GET_LIST_BY_PRODUCT_NAME", new string[,] { { "@product_name", productName } }, "da_proucd=>GetProductList(string productName)");
            foreach (DataRow r in tbl.Rows)
            {
                product = new bl_product();
                product.Product_ID = r["Product_ID"].ToString();
                product.Age_Max = Convert.ToInt32(r["Age_Max"].ToString());
                product.Age_Min = Convert.ToInt32(r["Age_Min"].ToString());
                product.En_Abbr = r["En_Abbr"].ToString();
                product.En_Title = r["En_Title"].ToString();
                product.Kh_Abbr = r["Kh_Abbr"].ToString();
                product.Kh_Title = r["Kh_Title"].ToString();
                product.Plan_Block = r["Plan_Block"].ToString();
                product.Plan_Code = r["Plan_Code"].ToString();
                product.Plan_Code2 = r["Plan_Code2"].ToString();
                product.Plan_Type = r["Plan_Type"].ToString();
                product.Product_Type_ID = Convert.ToInt32(r["Product_Type_ID"].ToString());
                product.Sum_Max = Convert.ToInt32(r["sum_max"].ToString());
                product.Sum_Min = Convert.ToInt32(r["sum_min"].ToString());
                product.Remarks = r["remarks"].ToString();
                proList.Add(product);
            }

        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [GetProductList(string productName)] in class [da_product], detail: " + ex.Message);
            proList = new List<bl_product>();
        }
        return proList;
    }
    #endregion
}