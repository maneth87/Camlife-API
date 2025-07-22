using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DB
/// </summary>
public class DB
{
    private SqlConnection my_connection;
    private SqlCommand my_command;
    private SqlDataAdapter my_da;

    public Int32 RowEffect { get; set; }
    public string Message { get; set; }
    public string Code { get; set; }
	public DB()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    public  bool Execute(string connection_string, string procedure_name, string[,] parameters)
    {
        bool status = false;
        try
        {
            my_connection = new SqlConnection(connection_string);
            my_connection.Open();
            my_command = new SqlCommand(procedure_name, my_connection);
            my_command.CommandType = CommandType.StoredProcedure;
            //initialize parameters
            for (int i = 0; i <= parameters.GetUpperBound(0); i++)
            {
                my_command.Parameters.AddWithValue(parameters[i, 0], parameters[i, 1]);

            }
            my_command.ExecuteNonQuery();
            my_command.Parameters.Clear();
            my_command.Dispose();
            my_connection.Close();
            status = true;
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [Execute] in class[DB], Detail: " + ex.Message);
            status = false;
        }
        return status;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="connection_string"></param>
    /// <param name="procedure_name"></param>
    /// <param name="parameters"></param>
    /// <param name="function_name">Current function name which call function [Execute]</param>
    /// <returns></returns>
    public bool Execute(string connection_string, string procedure_name, string[,] parameters, string function_name)
    {
        bool status = false;
        try
        {
            my_connection = new SqlConnection(connection_string);
            my_connection.Open();
            my_command = new SqlCommand(procedure_name, my_connection);
            my_command.CommandType = CommandType.StoredProcedure;
            //initialize parameters
            for (int i = 0; i <= parameters.GetUpperBound(0); i++)
            {
                my_command.Parameters.AddWithValue(parameters[i, 0], parameters[i, 1]);

            }
            RowEffect = my_command.ExecuteNonQuery();
            my_command.Parameters.Clear();
            my_command.Dispose();
            my_connection.Close();
            status = true;
            Message = "Execute successfully.";
        }
        catch (SqlException sqlEx) 
        {
            status = false;
            foreach (SqlError sqlError in sqlEx.Errors)
            {
                switch (sqlError.Number)
                {
                    case -2: // connection time out
                        Message = "Database connection time out.";
                        Code = "1002";
                        break;
                    case 53: // server not found
                               //do your Stuff here
                        Message = "Database server not found.";
                        Code = "1003";
                        break;
                    case 4060: // wronge database Name
                               //do your Stuff here
                        Message = "Invalid database name.";
                        Code = "1004";
                        break;
                    case 2627: // 2601 = (Primary) key violation
                               //do your Stuff here
                        Message = "Duplicated primary key.";
                        Code = "1006";
                        break;
                    case 201: // Paremater was not supplied.
                              //do your Stuff here
                        Message = "Paremater was not supplied.";
                        Code = "1007";
                        break;
                    //case 400: // 2601 = (Primary) key violation
                    //          //do your Stuff here
                    //    Message = sqlError.Message;
                    //    Code = "1005";
                    //    break;
                    
                    default:
                        //do your Stuff here
                       Message = "";
                        Code = "0";
                        break;
                }
            }
                RowEffect = -1;//error
            Log.AddExceptionToLog("DB", "Execute, execute by [" + function_name + "]", sqlEx);
        }
        catch (Exception ex)
        {
            Code = "0"; //unexpected error.
            Message = ex.Message;
            RowEffect = -1;//error
            Log.AddExceptionToLog("DB", "Execute, execute by [" + function_name + "]", ex);
            status = false;
        }
        return status;
    }
    public bool Execute(string connection_string, string sql_query)
    {
        bool status = false;
        try
        {
            my_connection = new SqlConnection(connection_string);
            my_connection.Open();
            my_command = new SqlCommand(sql_query, my_connection);
            my_command.CommandType = CommandType.Text;
            
            my_command.ExecuteNonQuery();
            my_command.Parameters.Clear();
            my_command.Dispose();
            my_connection.Close();
            status = true;
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("DB","Execute(string connection_string, string sql_query)", ex);
            status = false;
        }
        return status;
    }
    public DataTable GetData(string connection_string, string procedure_name, string[,] parameters, string function_name)
    {
        DataTable my_data = new DataTable();

        try
        {
            my_connection = new SqlConnection(connection_string);
            my_connection.Open();
            my_command = new SqlCommand(procedure_name, my_connection);
            my_command.CommandType = CommandType.StoredProcedure;
            //initialize parameters
            for (int i = 0; i <= parameters.GetUpperBound(0); i++)
            {
                my_command.Parameters.AddWithValue(parameters[i, 0], parameters[i, 1]);

            }
            my_da = new SqlDataAdapter(my_command);
            my_da.Fill(my_data);
            my_da.Dispose();
            my_command.Parameters.Clear();
            my_command.Dispose();
            my_connection.Close();
            RowEffect = my_data.Rows.Count;
            Message = "Success";
           
        }
        catch (SqlException sqlEx)
        {
          
            foreach (SqlError sqlError in sqlEx.Errors)
            {
                switch (sqlError.Number)
                {
                    case -2: // connection time out
                        Message = "Database connection time out.";
                        Code = "1002";
                        break;
                    case 53: // server not found
                             //do your Stuff here
                        Message = "Database server not found.";
                        Code = "1003";
                        break;
                    case 4060: // wronge database Name
                               //do your Stuff here
                        Message = "Invalid database name.";
                        Code = "1004";
                        break;
                    case 2627: // 2601 = (Primary) key violation
                               //do your Stuff here
                        Message = "Duplicated primary key.";
                        Code = "1006";
                        break;
                    case 201: // Paremater was not supplied.
                              //do your Stuff here
                        Message = "Parameter was not supplied.";
                        Code = "1007";
                        break;
                    case 2812: // 2812 = Could not find stored procedure
                               //do your Stuff here
                        Message = "Could not find stored procedure";
                        Code = "1008";
                        break;

                    default:
                        //do your Stuff here
                        Message = sqlError.Message;
                        Code = sqlError.Number+"";
                        break;
                }
            }
            RowEffect = -1;//error
            Log.AddExceptionToLog("DB","GetData(string connection_string, string procedure_name, string[,] parameters, string function_name), called by [" + function_name + "]" , sqlEx);
        }
        catch (Exception ex)
        {
            Code = "0";//unexpected error
            RowEffect = -1;
            Message = ex.Message;
            Log.AddExceptionToLog("DB", "GetData(string connection_string, string procedure_name, string[,] parameters, string function_name), called by [" + function_name + "]", ex);
        }

        return my_data;
    }
    public DataSet GetDataSet(string connection_string, string procedure_name, string[,] parameters)
    {
        DataSet ds = new DataSet();

        try
        {
            my_connection = new SqlConnection(connection_string);
            my_connection.Open();
            my_command = new SqlCommand(procedure_name, my_connection);
            my_command.CommandType = CommandType.StoredProcedure;
            //initialize parameters
            for (int i = 0; i <= parameters.GetUpperBound(0); i++)
            {
                my_command.Parameters.AddWithValue(parameters[i, 0], parameters[i, 1]);

            }
            my_da = new SqlDataAdapter(my_command);
            my_da.Fill(ds);
            my_da.Dispose();
            my_command.Parameters.Clear();
            my_command.Dispose();
            my_connection.Close();
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("DB","GetData", ex);
        }

        return ds;
    }
    public DataTable GetData(string connection_string, string query)
    {
        DataTable my_data = new DataTable();

        try
        {
            my_connection = new SqlConnection(connection_string);
            my_connection.Open();
            my_command = new SqlCommand(query, my_connection);
            my_command.CommandType = CommandType.Text;
           
            my_da = new SqlDataAdapter(my_command);
            my_da.Fill(my_data);
            my_da.Dispose();
            my_command.Parameters.Clear();
            my_command.Dispose();
            my_connection.Close();
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("DB", "GetData(string connection_string, string query)", ex);
        }

        return my_data;
    }
    /// <summary>
    /// Make up sql format
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public  string FormatSQLConditionFilds(List<string[, ,]> condition)
    {

        string my_condition = "";

        try
        {
            foreach (string[, ,] cond in condition)
            {
                for (int index = 0; index <= cond.GetUpperBound(0); index++)
                {
                    /*check operator "IN" */
                    if (cond[index, 0, 1].Trim().ToUpper() == "IN")
                    {
                        /*Make up sql format*/
                        cond[index, 0, 2] = " ('" + cond[index, 0, 2].Trim().Replace(",", "','") + "') ";
                    }
                    else if (cond[index, 0, 1].Trim().ToUpper() == "LIKE")
                    {
                        /*Make up sql format*/
                        cond[index, 0, 2] = " '%" + cond[index, 0, 2].Trim() + "%' ";
                    }
                    else if (cond[index, 0, 1].Trim().ToUpper() == "BETWEEN")
                    {
                        /*Make up sql format*/
                        cond[index, 0, 2] = " '" + cond[index, 0, 2].Trim().Replace(",", "' AND '") + "' ";
                        cond[index, 0, 0] = " CAST(" + cond[index, 0, 0] + " AS DATE) ";
                    }
                    else
                    {
                        /*Make up sql format*/
                        cond[index, 0, 2] = " '" + cond[index, 0, 2] + "' ";
                    }
                    cond[index, 0, 1] = " " + cond[index, 0, 1] + " ";
                    //my_condition += my_condition.Trim() == "" ? " WHERE " + cond[index, 0, 0] + cond[index, 0, 1] + cond[index, 0, 2] : " AND " + cond[index, 0, 0] + cond[index, 0, 1] + cond[index, 0, 2];
                    my_condition += " AND " + cond[index, 0, 0] + cond[index, 0, 1] + cond[index, 0, 2];

                }
            }

        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [FormatSQLConditionFilds(List<string[, ,]> condition)] in call [DB], detail:" + ex.Message);

        }
        return my_condition;
    }
}