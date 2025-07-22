using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// Summary description for DataSetGenerater
/// </summary>
public class DataSetGenerator
{
	public DataSetGenerator()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataSet GetDataSet(string sqlProcedureName, String policy_id)
    {
         //retrive data
   
        DataSet ds = new DataSet();
        DataTable tbl = new DataTable();
        try
        {
            string connString = AppConfiguration.GetConnectionString();

            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sqlProcedureName, con);
                SqlDataAdapter da;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@PolicyID";
                paramName.Value = policy_id;
                cmd.Parameters.Add(paramName);

                con.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                SqlDataReader rdr = cmd.ExecuteReader();
                da.Dispose();
                cmd.Dispose();
                con.Close();
                
            }

        }
        catch (Exception ex) 
        {
            Log.AddExceptionToLog("Error function GetDataTable in class DataSetGenerator, Detail: " + ex.Message);
        }
        //count row
       
        return ds;
    
    }

    public static DataTable Get_Data_Soure(string procedure_name, string[,] paramaters)
    {
        DataTable myDataTable = new DataTable();
        DataSet myDataSet = new DataSet();
        try
        {

            string connString = AppConfiguration.GetConnectionString();

            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(procedure_name, con);
                SqlDataAdapter da;
                cmd.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i <= paramaters.GetUpperBound(0); i++)
                {
                    cmd.Parameters.AddWithValue(paramaters[i, 0], paramaters[i, 1]);
                }
                //SqlParameter paramName = new SqlParameter();
                //paramName.ParameterName = "@App_Register_ID";
                //paramName.Value = paramater;
                //cmd.Parameters.Add(paramName);

                con.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(myDataSet);
                SqlDataReader rdr = cmd.ExecuteReader();
                da.Dispose();
                cmd.Dispose();
                con.Close();
                myDataTable = myDataSet.Tables[0];
            }

        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [Get_Data_Soure] in Class [DataSetGenerater], Detail: " + ex.Message);
        }
        return myDataTable;

    }

    public static DataTable Get_Data_Soure(string connection_string, string procedureName, string[,] parameters, string contact)
    {
        DataTable myDataTable = new DataTable();
        DataSet myDataSet = new DataSet();
        try
        {

            using (SqlConnection con = new SqlConnection(connection_string))
            {
                SqlCommand cmd = new SqlCommand(procedureName, con);
                SqlDataAdapter da;
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i <= parameters.GetUpperBound(0); i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i, 0], parameters[i, 1]);
                }
                string str = cmd.CommandText;
                con.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(myDataSet);
                SqlDataReader rdr = cmd.ExecuteReader();
                da.Dispose();
                cmd.Dispose();
                con.Close();
                myDataTable = myDataSet.Tables[0];
            }

        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [Get_Data_Soure(string connection_string, string procedureName, string[,] parameters, string contact)] in Class [DataSetGenerater] from  " + contact + ", Detail: " + ex.Message + "==>" + ex.InnerException);
        }
        return myDataTable;

    }
    public static DataSet GetDataSet(string procedure_name, string[,] paramaters)
    {
      
        DataSet myDataSet = new DataSet();
        try
        {

            string connString = AppConfiguration.GetConnectionString();

            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(procedure_name, con);
                SqlDataAdapter da;
                cmd.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i <= paramaters.GetUpperBound(0); i++)
                {
                    cmd.Parameters.AddWithValue(paramaters[i, 0], paramaters[i, 1]);
                }
                //SqlParameter paramName = new SqlParameter();
                //paramName.ParameterName = "@App_Register_ID";
                //paramName.Value = paramater;
                //cmd.Parameters.Add(paramName);

                con.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(myDataSet);
                SqlDataReader rdr = cmd.ExecuteReader();
                da.Dispose();
                cmd.Dispose();
                con.Close();
              
            }

        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [GetDataSet] in Class [DataSetGenerator], Detail: " + ex.Message);
        }
        return myDataSet;

    }
    
    public static DataTable Get_Data_Soure(string query_string)
    {
        DataTable myDataTable = new DataTable();
        DataSet myDataSet = new DataSet();
        try
        {

            string connString = AppConfiguration.GetConnectionString();

            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(query_string, con);
                SqlDataAdapter da;
                cmd.CommandType = CommandType.Text;

                con.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(myDataSet);
                SqlDataReader rdr = cmd.ExecuteReader();
                da.Dispose();
                cmd.Dispose();
                con.Close();
                myDataTable = myDataSet.Tables[0];
            }

        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [Get_Data_Soure] in Class [DataSetGenerator], Detail: " + ex.Message);
        }
        return myDataTable;

    }
    public static DataTable Get_Data_Soure(string connection_string, string query_string)
    {
        DataTable myDataTable = new DataTable();
        DataSet myDataSet = new DataSet();
        try
        {

            using (SqlConnection con = new SqlConnection(connection_string))
            {
                SqlCommand cmd = new SqlCommand(query_string, con);
                SqlDataAdapter da;
                cmd.CommandType = CommandType.Text;

                con.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(myDataSet);
                SqlDataReader rdr = cmd.ExecuteReader();
                da.Dispose();
                cmd.Dispose();
                con.Close();
                myDataTable = myDataSet.Tables[0];
            }

        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [Get_Data_Soure] in Class [DataSetGenerator], Detail: " + ex.Message);
        }
        return myDataTable;

    }
    public static DataTable Get_Data_Soure(string connection_string, string query_string, string[,] parameters)
    {
        DataTable myDataTable = new DataTable();
        DataSet myDataSet = new DataSet();
        try
        {

            using (SqlConnection con = new SqlConnection(connection_string))
            {
                SqlCommand cmd = new SqlCommand(query_string, con);

                //initialize parameters
                for (int i = 0; i <= parameters.GetUpperBound(0); i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i, 0], parameters[i, 1]);

                }

                SqlDataAdapter da;
                cmd.CommandType = CommandType.Text;

                con.Open();
                da = new SqlDataAdapter(cmd);
                da.Fill(myDataSet);
                SqlDataReader rdr = cmd.ExecuteReader();
                da.Dispose();
                cmd.Dispose();
                con.Close();
                myDataTable = myDataSet.Tables[0];
            }

        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [Get_Data_Soure(string connection_string, string query_string, string[,] parameters)] in Class [DataSetGenerater.cs], Detail: " + ex.Message);
        }
        return myDataTable;

    }
}