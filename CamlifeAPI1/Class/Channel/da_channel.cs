using CamlifeAPI1.Class;
using CamlifeAPI1.Class.Channel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.PeerToPeer;
using System.Web;

/// <summary>
/// Summary description for da_channel
/// </summary>
public class da_channel
{
    private static da_channel mytitle = null;
    public da_channel()
	{
        if (mytitle == null)
        {
            mytitle = new da_channel();
		}
	}
       

    //Function to get channel item list by channel
    public static List<bl_channel_item> GetChannelItemListByChannel(string channel_id)
    {
        List<bl_channel_item> channel_items = new List<bl_channel_item>();
        string connString = AppConfiguration.GetConnectionString();
        try
        {
            using (SqlConnection myConnection = new SqlConnection(connString))
            {

                SqlCommand myCommand = new SqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SP_Get_Channel_Item_List_By_Channel_ID";
                myCommand.Parameters.AddWithValue("@Channel_ID", channel_id);

                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {
                        bl_channel_item channel_item = new bl_channel_item();

                        channel_item.Channel_Item_ID = myReader.GetString(myReader.GetOrdinal("Channel_Item_ID"));
                        channel_item.Channel_Name = myReader.GetString(myReader.GetOrdinal("Channel_Name"));
                        channel_items.Add(channel_item);
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
            Log.AddExceptionToLog("Error in function [GetChannelItemListByChannel] in class [da_channel]. Details: " + ex.Message);
        }
        return channel_items;
    }
    //By Maneth, @28-04-2022
    public static List<bl_channel_item> GetChannelItemListMicroByChannel(string channel_id)
    {
        List<bl_channel_item> channel_items = new List<bl_channel_item>();
        string connString = AppConfiguration.GetConnectionString();
        try
        {
            using (SqlConnection myConnection = new SqlConnection(connString))
            {

                SqlCommand myCommand = new SqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SP_Get_Channel_Item_List_Micro_By_Channel_ID";
                myCommand.Parameters.AddWithValue("@Channel_ID", channel_id);

                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {
                        bl_channel_item channel_item = new bl_channel_item();

                        channel_item.Channel_Item_ID = myReader.GetString(myReader.GetOrdinal("Channel_Item_ID"));
                        channel_item.Channel_Name = myReader.GetString(myReader.GetOrdinal("Channel_Name"));
                        channel_items.Add(channel_item);
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
            Log.AddExceptionToLog("Error in function [GetChannelItemListMicroByChannel] in class [da_channel]. Details: " + ex.Message);
        }
        return channel_items;
    }
    //Function to get channel location list by channel item id
    public static List<bl_channel_location> GetChannelLocationListByChannelItemID(string channel_item_id)
    {
        List<bl_channel_location> channel_locations = new List<bl_channel_location>();
        string connString = AppConfiguration.GetConnectionString();
        try
        {
            using (SqlConnection myConnection = new SqlConnection(connString))
            {

                SqlCommand myCommand = new SqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SP_Get_Channel_Location_By_Channel_Item";
                myCommand.Parameters.AddWithValue("@Channel_Item_ID", channel_item_id);

                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {
                        bl_channel_location channel_location = new bl_channel_location();
                        channel_location.Channel_Item_ID = myReader.GetString(myReader.GetOrdinal("Channel_Item_ID"));
                        channel_location.Channel_Location_ID = myReader.GetString(myReader.GetOrdinal("Channel_Location_ID"));
                        channel_location.Office_Name = myReader.GetString(myReader.GetOrdinal("Office"));
                        channel_location.Office_Code = myReader.GetString(myReader.GetOrdinal("Office_Code"));
                        channel_location.Address = myReader.GetString(myReader.GetOrdinal("address"));
                       channel_location.AddressKh= myReader.GetString(myReader.GetOrdinal("address_kh"));
                       channel_location.PhoneNumber = myReader.GetString(myReader.GetOrdinal("tel"));
                       channel_location.Status = myReader.GetInt32(myReader.GetOrdinal("status"));
                       channel_location.Office = myReader.GetString(myReader.GetOrdinal("Office_Name"));
                        channel_locations.Add(channel_location);
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
            Log.AddExceptionToLog("Error in function [GetChannelLocaitonListByChannelItemID] in class [da_channel]. Details: " + ex.Message);
        }
        return channel_locations;
    }


    //Function to get channel_channel_item_id by channel sub id and channel item id
    public static string GetChannelChannelItemIDByChannelSubIDAndChannelItemID(int channel_sub_id, string channel_item_id)
    {
        string channel_channel_item_id = "";
        string connString = AppConfiguration.GetConnectionString();
        try
        {
            using (SqlConnection myConnection = new SqlConnection(connString))
            {

                SqlCommand myCommand = new SqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SP_Get_Channel_Channel_Item_ID_By_Channel_And_Channel_Item_ID";
                myCommand.Parameters.AddWithValue("@Channel_Sub_ID", channel_sub_id);
                myCommand.Parameters.AddWithValue("@Channel_Item_ID", channel_item_id);

                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {
                        bl_channel_location channel_location = new bl_channel_location();

                        channel_channel_item_id = myReader.GetString(myReader.GetOrdinal("Channel_Channel_Item_ID"));

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
            Log.AddExceptionToLog("Error in function [GetChannelChannelItemIDByChannelAndChannelItemID] in class [da_channel]. Details: " + ex.Message);
        }
        return channel_channel_item_id;
    }

    //Get channel_item_id by channel_location_id
    public static string GetChannelItemIDByChannelLocationID(string channel_location_id)
    {
        //Declare object
        string channel_item_id = "";

        string connString = AppConfiguration.GetConnectionString();
        try
        {
            using (SqlConnection myConnection = new SqlConnection(connString))
            {

                SqlCommand myCommand = new SqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SP_Get_Channel_Item_ID_By_Channel_Location_ID";
                myCommand.Parameters.AddWithValue("@Channel_Location_ID", channel_location_id);

                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {
                        //If found row, return true & do the statement
                        if (myReader.HasRows)
                        {
                            channel_item_id = myReader.GetString(myReader.GetOrdinal("Channel_Item_ID"));

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
            Log.AddExceptionToLog("Error function [GetChannelItemIDByChannelLocationID] in class [da_channel]. Details: " + ex.Message);
        }
        return channel_item_id;
    }

   
    public static string GetChannelLocationIDByLocationName(string office_code)
    {
        //Declare object
        string location_id = "";

        string connString = AppConfiguration.GetConnectionString();
        try
        {
            using (SqlConnection myConnection = new SqlConnection(connString))
            {

                SqlCommand myCommand = new SqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SP_Get_Channel_Location_ID_By_Location_Name";
                myCommand.Parameters.AddWithValue("@Office_Code", office_code);

                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {
                        //If found row, return true & do the statement
                        if (myReader.HasRows)
                        {
                            location_id = myReader.GetString(myReader.GetOrdinal("Channel_Location_ID"));

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
            Log.AddExceptionToLog("Error function [GetChannelLocationIDByLocationName] in class [da_channel]. Details: " + ex.Message);
        }
        return location_id;
    }

    public static string GetChannelLocationIDByLocationName(string office_code,string channel_item_id)
    {
        //Declare object
        string location_id = "";

        string connString = AppConfiguration.GetConnectionString();
        try
        {
            using (SqlConnection myConnection = new SqlConnection(connString))
            {

                SqlCommand myCommand = new SqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SP_Get_Channel_Location_ID_By_Location_Name_Channel_Item_ID";
                myCommand.Parameters.AddWithValue("@Channel_Item_ID", channel_item_id);
                myCommand.Parameters.AddWithValue("@Office_Code", office_code);

                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {
                        //If found row, return true & do the statement
                        if (myReader.HasRows)
                        {
                            location_id = myReader.GetString(myReader.GetOrdinal("Channel_Location_ID"));

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
            Log.AddExceptionToLog("Error function [GetChannelLocationIDByLocationName] in class [da_channel]. Details: " + ex.Message);
        }
        return location_id;
    }

    //Get channel_location_id by sale_agent_id
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
            Log.AddExceptionToLog("Error function [GetChannelLocationIDBySaleAgentID] in class [da_channel]. Details: " + ex.Message);
        }
        return channel_location_id;
    }

    //Function to get channel item name by id
    public static string GetChannelItemNameByID(string channel_item_id)
    {
        string channel_item_name = "";
        string connString = AppConfiguration.GetConnectionString();
        try
        {
            using (SqlConnection myConnection = new SqlConnection(connString))
            {

                SqlCommand myCommand = new SqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SP_Get_Channel_Item_Name_By_ID";
                myCommand.Parameters.AddWithValue("@Channel_Item_ID", channel_item_id);

                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {


                        channel_item_name = myReader.GetString(myReader.GetOrdinal("Channel_Name"));

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
            Log.AddExceptionToLog("Error in function [GetChannelItemNameByID] in class [da_channel]. Details: " + ex.Message);
        }
        return channel_item_name;
    }
    public static List<bl_channel_item> GetChannelItemByUserName(string userName)
    {
        List<bl_channel_item> ch = new List<bl_channel_item>();
        try
        {
            DB db = new DB();

            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_CHANNEL_ITEM_GET_BY_USER_NAME", new string[,] { 
        {"@USER_NAME", userName}
        }, "da_channel => GetChannelItemByUserName(string userName)");
            if (tbl.Rows.Count > 0)
            {
                foreach (DataRow row in tbl.Rows)
                {
                    ch.Add(new bl_channel_item()
                    {
                        Channel_Item_ID = row["channel_item_id"].ToString(),
                        Channel_Name = row["channel_name"].ToString(),
                        Channel_HQ_Address = row["channel_hq_address"].ToString(),
                        Channel_HQ_Address_KH = row["Channel_HQ_Address_KH"].ToString()
                    });
                }
            }
            else
            {
                ch = new List<bl_channel_item>();
            }

        }
        catch (Exception ex)
        {
            ch = new List<bl_channel_item>();
            Log.AddExceptionToLog("Error function [GetChannelItemByUserName(string userName)] in class [da_channel]. Details: " + ex.Message);
        }
        return ch;
    }

    public static bl_channel_sale_agent GetChannelSaleAgentByUserName(string USER_NAME)
    {
        bl_channel_sale_agent channel_agent;
        try
        {
            DB db = new DB();
            
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_SALE_AGENT_CHANNEL_LOCATION_GEY_BY_USER_NAME", new string[,] { 
        {"@USER_NAME", USER_NAME}
        }, "da_channel => GetChannelLocationByUserName(string USER_NAME)");


            if (tbl.Rows.Count > 0)
            {
                var row = tbl.Rows[0];
                channel_agent = new bl_channel_sale_agent()
                {
                    ID = row["id"].ToString(),
                    USER_NAME = row["user_name"].ToString(),
                    SALE_AGENT_ID = row["sale_agent_id"].ToString(),
                    CHANNEL_ITEM_ID = row["channel_item_id"].ToString(),
                    CHANNEL_LOCATION_ID = row["channel_location_id"].ToString(),
                    CREATED_BY = row["created_by"].ToString(),
                    CREATED_ON = Convert.ToDateTime(row["created_on"].ToString()),
                    REMARKS = row["remarks"].ToString(),
                    UPDATED_BY = row["updated_by"].ToString(),
                    UPDATED_ON = Convert.ToDateTime(row["updated_on"].ToString()),
                    AgentNameEn = row["full_name"].ToString(),
                    AgentNameKh = row["full_name_kh"].ToString()
                };

                List<AgentChannel> acList = new List<AgentChannel>();
                foreach (DataRow dr in tbl.Rows)
                {
                    acList.Add(new AgentChannel()
                    {
                        ChannelItemId = dr["channel_item_id"].ToString(),
                        ChannelLocationId = dr["channel_location_id"].ToString(),
                        OfficeCode = dr["office_code"].ToString(),
                        OfficeName = dr["office_name"].ToString(),
                        ChannelName = dr["channel_name"].ToString()
                    }) ;
                }
                channel_agent.Channels = acList;

                /*channel item*/
                List<bl_channel_item> chList = da_channel.GetChannelItemByUserName(USER_NAME);
                channel_agent.ChannelItems = chList;
            }
            else
            {
                channel_agent =null;
            }

        }
        catch (Exception ex)
        {
            channel_agent = null;
            Log.AddExceptionToLog( "da_channel","GetChannelLocationByUserName(string USER_NAME)",ex);
        }
        return channel_agent;
    }
    /// <summary>
    /// Get Channel Location by user/ sale agent
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static List<bl_channel_location> GetChannelLocationByUser(string user)
    {
        List<bl_channel_location> chList = new List<bl_channel_location>();
        try
        {
            DB db = new DB();

            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_CHANNEL_LOCATION_GET_BY_USER", new string[,] { 
        {"@USER", user}
        }, "da_channel => GelChannelLocationByUser(string user)");
            if (tbl.Rows.Count > 0)
            {
                foreach (DataRow row in tbl.Rows)
                {

                    chList.Add(new bl_channel_location()
                    {
                        Channel_Location_ID = row["channel_location_id"].ToString(),
                        Channel_Item_ID = row["channel_item_id"].ToString(),
                        Office_Code = row["office_code"].ToString(),
                        Office_Name = row["office_name"].ToString(),
                        Address = row["address"].ToString()
                    });
                }
            }
            else
            {
                chList = new List<bl_channel_location>();
            }
        }
        catch (Exception ex)
        {
            chList = new List<bl_channel_location>();
            Log.AddExceptionToLog("Error function [GelChannelLocationByUser(string user) in class [da_channel]. Details: " + ex.Message);
        }
        return chList;
    }

    /// <summary>
    /// Get list of channel location by channel item id and user name
    /// </summary>
    /// <param name="channel_item_id"></param>
    /// <param name="user">Use for log only is not use as condition</param>
    /// <returns></returns>
    public static List<bl_channel_location> GetChannelLocationByChannelItemIdUser(string channel_item_id, string user)
    {
        List<bl_channel_location> chList = new List<bl_channel_location>();
        try
        {
            
            DB db = new DB();

            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_CHANNEL_LOCATION_GET_BY_USER_CHANNE_ITEM_ID", new string[,] { 
        {"@USER_NAME", user},{"@CHANNEL_ITEM_ID",channel_item_id}
        }, "da_channel => GetChannelLocationByChannelItemIdUser(string channel_item_id, string user)");
            if (tbl.Rows.Count > 0)
            {
                foreach (DataRow row in tbl.Rows)
                {

                    chList.Add(new bl_channel_location()
                    {
                        Channel_Location_ID = row["channel_location_id"].ToString(),
                        Channel_Item_ID = row["channel_item_id"].ToString(),
                        Office_Code = row["office_code"].ToString(),
                        Office_Name = row["office_name"].ToString(),
                        Address = row["address"].ToString()
                    });
                }
            }
            else
            {
                chList = new List<bl_channel_location>();
            }
        }
        catch (Exception ex)
        {
            chList = new List<bl_channel_location>();
            Log.AddExceptionToLog("Error function [GetChannelLocationByChannelItemIdUser(string channel_item_id, string user)] in class [da_channel]. Details: " + ex.Message);
        }
        return chList;
    }

    public static bl_channel_location GetChannelLocationByChannelLocationID(string CHANNEL_LOCATION_ID)
    {
        bl_channel_location channel;
        try
        {
            DB db = new DB();

            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_CHANNEL_LOCATION_GET_BY_CHANNEL_LOCATION_ID", new string[,] { 
        {"@CHANNEL_LOCATION_ID", CHANNEL_LOCATION_ID}
        }, "da_channel => GetChannelLocationByChannelLocationID(string CHANNEL_LOCATION_ID)");
            if (tbl.Rows.Count > 0)
            {
                var row = tbl.Rows[0];
                channel = new bl_channel_location()
                {
                    Channel_Location_ID = row["channel_location_id"].ToString(),
                    Channel_Item_ID = row["channel_item_id"].ToString(),
                     Office_Code = row["office_code"].ToString(),
                    Office_Name = row["office_name"].ToString(),
                    Address = row["address"].ToString()
                };
            }
            else
            {
                channel = new bl_channel_location();
            }

        }
        catch (Exception ex)
        {
            channel = new bl_channel_location();
            Log.AddExceptionToLog("Error function [GetChannelLocationByChannelLocationID(string CHANNEL_LOCATION_ID) in class [da_channel]. Details: " + ex.Message);
        }
        return channel;
    }

    public static List<bl_channel> GetChannelList()
    {
        List<bl_channel> list = new List<bl_channel>();

        try
        {
            DataTable tbl = new DB().GetData(AppConfiguration.GetConnectionString(), "SP_CT_CHANNEL_GET", new string[,] { }, "da_channel=>GetChannelList()");
            foreach (DataRow r in tbl.Rows)
            {
                list.Add(new bl_channel() {
                Channel_ID=r["channel_id"].ToString(), Details=r["details"].ToString(), Type=r["type"].ToString(), Status=Convert.ToInt32(r["status"].ToString())
                });
            }
        }
        catch (Exception ex)
        {
            list = new List<bl_channel>();
            Log.AddExceptionToLog("Error Function [ GetChannelList()] in class [da_channel], detail:" + ex.Message + "=>" + ex.StackTrace);
        }
        return list;
    }
    public static List<bl_channel> GetChannelListActive()
    {
        List<bl_channel> list = new List<bl_channel>();

        try
        {
            foreach (bl_channel ch in GetChannelList().Where(_ => _.Status == 1))
            {
                list.Add(ch);
            }

          
        }
        catch (Exception ex)
        {
            list = new List<bl_channel>();
            Log.AddExceptionToLog("Error Function [ GetChannel()] in class [da_channel], detail:" + ex.Message + "=>" + ex.StackTrace);
        }
        return list;
    }

    //Get channel_channel_id by channel_sub_id
    public static List<bl_channel_location> GetChannelLocationIDByChannelSubID(string channel_sub_id)
    {
        //Declare object

        List<bl_channel_location> channel_locations = new List<bl_channel_location>();
        string connString = AppConfiguration.GetConnectionString();
        try
        {
            using (SqlConnection myConnection = new SqlConnection(connString))
            {

                SqlCommand myCommand = new SqlCommand();
                myConnection.Open();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "SP_Get_Channel_Location_ID_By_Channel_Sub_ID";
                myCommand.Parameters.AddWithValue("@Channel_Sub_ID", channel_sub_id);

                using (SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {
                        bl_channel_location channel_item = new bl_channel_location();

                        channel_item.Channel_Location_ID = myReader.GetString(myReader.GetOrdinal("Channel_Location_ID"));
                        channel_item.Channel_Item_ID = myReader.GetString(myReader.GetOrdinal("Channel_Item_ID"));
                        channel_locations.Add(channel_item);
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
            Log.AddExceptionToLog("Error function [GetChannelItemIDByChannelSubID] in class [da_channel]. Details: " + ex.Message);
        }
        return channel_locations;
    }

    public static bl_channel_item GetChannelChannelItem(string channelItemID)
    {
        bl_channel_item channel = new bl_channel_item();
        try
        {
            DataTable tbl = new DB().GetData(AppConfiguration.GetConnectionString(), "", new string[,] {
            
            }, "");
        }
        catch (Exception ex)
        {
            channel = new bl_channel_item();
            Log.AddExceptionToLog("Error function [GetChannelChannelItem(string channelItemID)] in class [da_channel], detail:" + ex.Message + "=>" + ex.StackTrace);
        }

        return channel;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cId"></param>
    /// <param name="gName"></param>
    /// <returns></returns>
    public static DataTable GetChannelLocationByGroup(string cId, string gName)
    {
        DataTable tbl = new DataTable();
        try
        {
            tbl = new DB().GetData(AppConfiguration.GetConnectionString(), "SP_CHANNEL_LOCATION_GET_BY_GROUP_NAME", new string[,] {
            {"@CHANNEL_ITEM_ID",cId},{"@group_name", gName}
            }, "da_channel => GetChannelLocationByGroup(string cId, string gName)");
        }
        catch (Exception ex)
        {

            Log.AddExceptionToLog("Error function [GetChannelLocationByGroup(string cId)] in class [da_channel], detail:" + ex.Message + "=>" + ex.StackTrace);
        }
        return tbl;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cId">Channel Item id</param>
    /// <returns></returns>
    public static DataTable GetChannelLocationGroup(string cId)
    {
        DataTable tbl = new DataTable();
        try
        {
            tbl = new DB().GetData(AppConfiguration.GetConnectionString(), "SP_CHANNEL_LOCATION_GROUP_DISTINCT_GET", new string[,] {
            {"@CHANNEL_ITEM_ID",cId}
            }, "da_channel => GetChannelLocationGroup(string cId)");
        }
        catch (Exception ex)
        {

            Log.AddExceptionToLog("Error function [GetChannelLocationGroup(string cId)] in class [da_channel], detail:" + ex.Message + "=>" + ex.StackTrace);
        }
        return tbl;
    }

    public static List<bl_channel_item> GetChannelItemList()
    {
        List<bl_channel_item> chList = new List<bl_channel_item>();
        try {
            DataTable tbl = new DataTable();
            tbl = new DB().GetData(AppConfiguration.GetConnectionString(), "SP_CT_CHANNEL_ITEM_GET_LIST", new string[,] {
           
            }, "da_channel=>GetChannelItemList()");
             foreach(DataRow r in tbl.Rows)
            {
                chList.Add(new bl_channel_item() { 
                Channel_Item_ID = r["channel_item_id"].ToString(),
                Channel_Name = r["channel_name"].ToString(),
                Channel_HQ_Address=r["channel_hq_address"].ToString()
                });
            }
        }
        catch (Exception ex)
        {
            chList = new List<bl_channel_item>();
            Log.AddExceptionToLog("Error function [GetChannelItemList()] in class [da_channel], detail:" + ex.Message + "=>" + ex.StackTrace);
        }
        return chList;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"> channel locaiton object </param>
    /// <returns></returns>
    public static bool AddChannelLocation(bl_channel_location obj)
    {
        bool add = false;
        try {
            DB db = new DB();
            add = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_CHANNEL_LOCATION_INSERT", new string[,] { 
                {"@CHANNEL_LOCATION_ID", obj.Channel_Location_ID}, 
                {"@CHANNEL_ITEM", obj.Channel_Item_ID},
                {"@OFFICE_CODE", obj.Office_Code},
                {"@OFFICE_NAME", obj.Office_Name},
                {"@TEL", obj.PhoneNumber},
                {"@ADDRESS_EN", obj.Address},
                {"@ADDRESS_KH", obj.AddressKh},
                {"@STATUS", obj.Status+""},
                {"@CREATED_NOTE", obj.CreatedNote},
                {"@CREATED_BY", obj.Created_By},
                {"@CREATED_ON", obj.Created_On+""}
            
            }, "da_channel=> AddChannelLocation(bl_channel_location obj)");
        }
        catch (Exception ex)
        {
            add = false;
            Log.AddExceptionToLog("Error function [AddChannelLocation(bl_channel_location obj)] in class [da_channel], detail:" + ex.Message);
        }
        return add;
    }
    /// <summary>
    /// Update channel location by channel location id
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool UpdateChannelLocation(bl_channel_location obj)
    {
        bool add = false;
        try
        {
            DB db = new DB();
            add = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_CHANNEL_LOCATION_UPDATE", new string[,] { 
                {"@CHANNEL_LOCATION_ID", obj.Channel_Location_ID}, 
                {"@CHANNEL_ITEM", obj.Channel_Item_ID},
                {"@OFFICE_CODE", obj.Office_Code},
                {"@OFFICE_NAME", obj.Office_Name},
                {"@TEL", obj.PhoneNumber},
                {"@ADDRESS_EN", obj.Address},
                {"@ADDRESS_KH", obj.AddressKh},
                {"@STATUS", obj.Status+""},
                {"@CREATED_NOTE", obj.CreatedNote}
            
            }, "da_channel=> UpdateChannelLocation(bl_channel_location obj)");
        }
        catch (Exception ex)
        {
            add = false;
            Log.AddExceptionToLog("Error function [UpdateChannelLocation(bl_channel_location obj)] in class [da_channel], detail:" + ex.Message);
        }
        return add;
    }
    /// <summary>
    /// Delete channel location by channel location id
    /// </summary>
    /// <param name="channelLocationId"></param>
    /// <returns></returns>
    public static bool DeleteChannelLocation(string channelLocationId)
    {
        bool add = false;
        try
        {
            DB db = new DB();
            add = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_CHANNEL_LOCATION_DELETE", new string[,] { 
                {"@CHANNEL_LOCATION_ID", channelLocationId}
            
            }, "da_channel=> DeleteChannelLocation(string channelLocationId)");
        }
        catch (Exception ex)
        {
            add = false;
            Log.AddExceptionToLog("Error function [DeleteChannelLocation(string channelLocationId)] in class [da_channel], detail:" + ex.Message);
        }
        return add;
    }
    ///// <summary>
    ///// Generate new id
    ///// </summary>
    ///// <returns></returns>
    //public static C GetChannelLocationId()
    //{
    //    return Helper.GetNewGuid(new string[,] { { "TABLE", "CT_CHANNEL_LOCATION" }, { "FIELD", "CHANNEL_LOCATION_ID" } });
    //}


    public static bl_channel_detail GetChannelDetail( string channelLocationId)
    {
        bl_channel_detail ch = new bl_channel_detail();
        try
        {
            DataTable tbl = new DataTable();
            tbl = new DB().GetData(AppConfiguration.GetConnectionString(), "SP_GET_CHANNEL_DETAIL_BY_CHANNEL_LOCATION", new string[,] {
                {"@channel_location_id",channelLocationId }
            }, "da_channel=>GetChannelDetail( string channelLocationId)");
            foreach (DataRow r in tbl.Rows)
            {
                ch = new bl_channel_detail() {
                    ChannelId = r["channel_id"].ToString(),
                    ChannelType = r["channel_type"].ToString(),
                    ChannelDetail = r["channel_detail"].ToString(),
                    ChannelItemId = r["channel_item_id"].ToString(),
                    ChannelItemName = r["channel_item_name_en"].ToString(),
                    ChannelItemNameKh = r["channel_item_name_kh"].ToString(),
                    ChannelLocationId = r["channel_location_id"].ToString(),
                    OfficeCode = r["office_code"].ToString(),
                    OfficeName = r["office_name"].ToString()
                };
            }
        }
        catch (Exception ex)
        {
            ch = null;
            Log.AddExceptionToLog("da_channel", "GetChannelDetail( string channelLocationId)",  ex);
        }
        return ch;
    }
}