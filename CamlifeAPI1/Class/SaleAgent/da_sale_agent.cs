using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

using System.Collections;

/// <summary>
/// Summary description for da_sale_agent
/// </summary>

using System.Configuration;
using System.Data;
using System.Reflection;

public class da_sale_agent
{
    private static da_sale_agent mytitle = null;
    private static string MYNAME = "da_sale_agent";
    public da_sale_agent()
    {
        if (mytitle == null)
        {
            mytitle = new da_sale_agent();
        }

    }

    /// <summary>
    /// Check Sale ID that is being used from Ct_Sale_Agent
    /// </summary>
    public static bool GetSaleAgent_IsUsed_By_Sale_ID(string sale_agent_id)
    {
        bool result = false;
        string connString = AppConfiguration.GetConnectionString();
        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Get_Sale_Agent_IsUsed_By_Sale_Agent_ID";
            cmd.Parameters.AddWithValue("@Sale_Agent_ID", sale_agent_id);
            cmd.Connection = con;
            DataTable dt = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter(cmd);

            con.Open();
            try
            {
                dap.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                //Add error to log 
                Log.AddExceptionToLog("Error in function [GetSaleAgent_IsUsed_By_Sale_ID] in class [bl_sale_agent]. Details: " + ex.Message);
            }
        }
        return result;
    }

    /// <summary>
    /// Get Sale Agent List
    /// </summary>

    public static DataTable GetSaleAgentList()
    {
        DataTable dt = new DataTable();
        string connString = AppConfiguration.GetConnectionString();
        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Get_Sale_Agent_List";
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            cmd.Connection = con;
            con.Open();
            try
            {
                dap.Fill(dt);
            }
            catch (Exception ex)
            {
                //Add error to log 
                Log.AddExceptionToLog("Error in function [GetSaleAgentList] in class [bl_sale_agent]. Details: " + ex.Message);
            }
        }
        return dt;
    }


    //Get channel_location_id from object sale_agent_channel_location by sale_agent_id
    public static string GetChannelLocationIDBySaleAgentID(string sale_agent_id)
    {
        //Declare object
        string channel_location_id = "";

        string connString = AppConfiguration.GetConnectionString();
        try
        {
            using (SqlConnection myConnection = new SqlConnection(connString))
            {

                SqlCommand myCommand = new SqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SP_Get_Channel_Location_ID_By_Sale_Agent_ID";
                myCommand.Parameters.AddWithValue("@Sale_Agent_ID", sale_agent_id);

                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {
                        //If found row, return true & do the statement
                        if (myReader.HasRows)
                        {
                            channel_location_id = myReader.GetString(myReader.GetOrdinal("Channel_Location_ID"));

                        }
                    }
                    myReader.Close();
                }
                myConnection.Open();

                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }

        }
        catch (Exception ex)
        {
            //Add error to log for analysis
            Log.AddExceptionToLog("Error function [GetChannelLocaitonIDBySaleAgentID] in class [da_sale_agent]. Details: " + ex.Message);
        }
        return channel_location_id;
    }
   

    //Get Sale_Agent_Team_ID by user id
    public static string GetSaleAgentTeamID(string user_id)
    {
        string sale_agent_team_id = "";
        string connString = AppConfiguration.GetConnectionString();
        try
        {
            using (SqlConnection myConnection = new SqlConnection(connString))
            {

                SqlCommand myCommand = new SqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SP_Get_Sale_Agent_By_User_ID";
                myCommand.Parameters.AddWithValue("@User_ID", user_id);
                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {

                        sale_agent_team_id = myReader.GetString(myReader.GetOrdinal("Sale_Agent_Team_ID"));

                    }
                    myReader.Close();
                }
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }
        catch (Exception ex)
        {
            //Add error to log 
            Log.AddExceptionToLog("Error in function [GetSaleAgentTeamID] in class [da_sale_sale]. Details: " + ex.Message);

        }
        return sale_agent_team_id;
    }

    //Get Sale_Agent_ID by user id
    public static string GetSaleAgentIDByUserID(string user_id)
    {
        string sale_agent_id = "";
        string connString = AppConfiguration.GetConnectionString();
        try
        {
            using (SqlConnection myConnection = new SqlConnection(connString))
            {

                SqlCommand myCommand = new SqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SP_Get_Sale_Agent_ID_By_User_ID";
                myCommand.Parameters.AddWithValue("@User_ID", user_id);
                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {

                        sale_agent_id = myReader.GetString(myReader.GetOrdinal("Sale_Agent_ID"));

                    }
                    myReader.Close();
                }
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }
        catch (Exception ex)
        {
            //Add error to log 
            Log.AddExceptionToLog("Error in function [GetSaleAgentIDByUserID] in class [da_sale_agent Details: " + ex.Message);

        }
        return sale_agent_id;
    }

    //Get Channel_ID by user id
    public static string GetChannelID(string user_id)
    {
        string channel_id = "";
        string connString = AppConfiguration.GetConnectionString();
        try
        {
            using (SqlConnection myConnection = new SqlConnection(connString))
            {

                SqlCommand myCommand = new SqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SP_Get_Sale_Agent_By_User_ID";
                myCommand.Parameters.AddWithValue("@User_ID", user_id);
                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {

                        channel_id = myReader.GetString(myReader.GetOrdinal("Channel_ID"));

                    }
                    myReader.Close();
                }
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }
        catch (Exception ex)
        {
            //Add error to log 
            Log.AddExceptionToLog("Error in function [GetChannelID] in class [da_sale_agent]. Details: " + ex.Message);

        }
        return channel_id;
    }

    //Get Code

    //Get Sale Agent Name
    public static string GetSaleAgentName(string sale_agent_id)
    {
        string sale_agent_name = "";
        string connString = AppConfiguration.GetConnectionString();
        try
        {
            using (SqlConnection myConnection = new SqlConnection(connString))
            {

                SqlCommand myCommand = new SqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SP_Get_Sale_Agent_Name_By_ID";
                myCommand.Parameters.AddWithValue("@Sale_Agent_ID", sale_agent_id);

                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {

                        sale_agent_name = myReader.GetString(myReader.GetOrdinal("Full_Name"));

                    }
                    myReader.Close();
                }
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }
        catch (Exception ex)
        {
            //Add error to log 
            Log.AddExceptionToLog("Error in function [GetSaleAgentName] in class [da_sale_agent]. Details: " + ex.Message);

        }
        return sale_agent_name;
    }


    public static List<bl_sale_agent_micro> GetSaleAgentMicroList()
    {
        List<bl_sale_agent_micro> objList = new List<bl_sale_agent_micro>();
        try
        {
            DB db = new DB();
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_SALE_AGENT_GET", new string[,] { }, "da_sale_agent=>GetSaleAgentMicroList()");
            foreach (DataRow r in tbl.Rows)
            {
                objList.Add(new bl_sale_agent_micro()
                {
                    SaleAgentId = r["sale_agent_id"].ToString(),
                    FullNameEn = r["full_name"].ToString(),
                    FullNameKh = r["full_name_kh"].ToString(),
                    Position = r["position"].ToString(),
                    Email = r["email"].ToString(),
                    SaleAgentType = Convert.ToInt32(r["sale_agent_type"].ToString()),
                    Status = Convert.ToInt32(r["status"].ToString()),
                    CreatedBy = r["created_by"].ToString(),
                    CreatedNote = r["created_note"].ToString(),
                    CreatedOn = Convert.ToDateTime(r["created_on"].ToString())
                });
            }
        }
        catch (Exception ex)
        {
            objList = new List<bl_sale_agent_micro>();
            Log.AddExceptionToLog("Error function [GetSaleAgentMicroList()] in class [da_sale_agent], detail:" + ex.Message);
        }
        return objList;
    }

    public static List<bl_sale_agent_micro> GetSaleAgentMicroListByChannelItemIdBranchCode(string channelItemId, string branchCode)
    {
        List<bl_sale_agent_micro> objList = new List<bl_sale_agent_micro>();
        try
        {
            DB db = new DB();
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_SALE_AGENT_GET_BY_CHANNEL_ITEM_BRANCH_CODE", new string[,] {
            {"@CHANNEL_ITEM_ID", channelItemId },
            {"@BRANCH_CODE", branchCode}
            }, "da_sale_agent=>GetSaleAgentMicroListByChannelItemIdBranchCode(string channelItemId, string branchCode)");
            foreach (DataRow r in tbl.Rows)
            {
                objList.Add(new bl_sale_agent_micro()
                {
                    SaleAgentId = r["sale_agent_id"].ToString(),
                    FullNameEn = r["full_name"].ToString(),
                    FullNameKh = r["full_name_kh"].ToString(),
                    Position = r["position"].ToString(),
                    Email = r["email"].ToString(),
                    SaleAgentType = Convert.ToInt32(r["sale_agent_type"].ToString()),
                    Status = Convert.ToInt32(r["status"].ToString())
                    //CreatedBy = r["created_by"].ToString(),
                    //CreatedNote = r["created_note"].ToString(),
                    //CreatedOn = Convert.ToDateTime(r["created_on"].ToString())
                    ,ValidFrom = Convert.ToDateTime(r["valid_from"].ToString())
                    ,
                    ValidTo = Convert.ToDateTime(r["valid_to"].ToString())
                });
            }
        }
        catch (Exception ex)
        {
            objList = new List<bl_sale_agent_micro>();
            Log.AddExceptionToLog("Error function [GetSaleAgentMicroListByChannelItemIdBranchCode(string channelItemId, string branchCode)] in class [da_sale_agent], detail:" + ex.Message);
        }
        return objList;
    }
    /// <summary>
    /// Get Sale agent micro by name or id
    /// </summary>
    /// <param name="value"> full name or sale agent id </param>
    /// <returns></returns>
    public static List<bl_sale_agent_micro> GetSaleAgentMicroList(string  value)
    {
        List<bl_sale_agent_micro> objList = new List<bl_sale_agent_micro>();
        try
        {
            DB db = new DB();
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_SALE_AGENT_GET_BY_INFO", new string[,] { 
             {"@value", value}
            }, "da_sale_agent=>GetSaleAgentMicroList(string  value)");
            foreach (DataRow r in tbl.Rows)
            {
                objList.Add(new bl_sale_agent_micro()
                {
                    SaleAgentId = r["sale_agent_id"].ToString(),
                    FullNameEn = r["full_name"].ToString(),
                    FullNameKh = r["full_name_kh"].ToString(),
                    Position = r["position"].ToString(),
                    Email = r["email"].ToString(),
                    SaleAgentType = Convert.ToInt32(r["sale_agent_type"].ToString()),
                    Status = Convert.ToInt32(r["status"].ToString()),
                    CreatedBy = r["created_by"].ToString(),
                    CreatedNote = r["created_note"].ToString(),
                    CreatedOn = Convert.ToDateTime(r["created_on"].ToString())
                });
            }
        }
        catch (Exception ex)
        {
            objList = new List<bl_sale_agent_micro>();
            Log.AddExceptionToLog("Error function [GetSaleAgentMicroList(string  value)] in class [da_sale_agent], detail:" + ex.Message);
        }
        return objList;
    }
    public static bl_sale_agent_micro GetSaleAgentMicro(string saleAgentId)
    {
        bl_sale_agent_micro obj = new bl_sale_agent_micro();
        try
        {
            DB db = new DB();
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_SALE_AGENT_GET_BY_SALE_AGENT_ID", new string[,] { 
            {"@sale_agent_id", saleAgentId}
            }, "da_sale_agent=>GetSaleAgentMicro(string saleAgentId)");
            foreach (DataRow r in tbl.Rows)
            {
              
             
                obj = new bl_sale_agent_micro()
                {
                    SaleAgentId = r["sale_agent_id"].ToString(),
                    FullNameEn = r["full_name"].ToString(),
                    FullNameKh = r["full_name_kh"].ToString(),
                    Position = r["position"].ToString(),
                    Email = r["email"].ToString(),
                    SaleAgentType = Convert.ToInt32(r["sale_agent_type"].ToString()),
                    Status = Convert.ToInt32(r["status"].ToString()),
                    CreatedBy = r["created_by"].ToString(),
                    CreatedNote = r["created_note"].ToString(),
                    CreatedOn = Convert.ToDateTime(r["created_on"].ToString()),
                    ValidFrom = Helper.FormatDateTime(Convert.ToDateTime(r["Valid_FROM"].ToString()).ToString("dd/MM/yyyy")),
                    ValidTo = Helper.FormatDateTime(Convert.ToDateTime(r["Valid_TO"].ToString()).ToString("dd/MM/yyyy"))
                };
            }
        }
        catch (Exception ex)
        {
            obj = new bl_sale_agent_micro();
            Log.AddExceptionToLog("Error function [GetSaleAgentMicro(string saleAgentId)] in class [da_sale_agent], detail:" + ex.Message);
        }
        return obj;
    }
    
}