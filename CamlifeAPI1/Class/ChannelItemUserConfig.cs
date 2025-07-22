using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class ChannelItemUserConfig
{
    #region  Properties
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string ChannelItemId { get; set; }
    public string ChannelName { get; set; }
    public string ChannelNameKh { get; set; }
    public  bool Transection { get { return _tranSection; } }
    public  string Message { get { return _message; } }
    //Private variable
    private static string _message = "";
    private static bool _tranSection = false;
    #endregion
    public ChannelItemUserConfig() { }

    public ChannelItemUserConfig GetByUserId(string userId)
    {
        ChannelItemUserConfig obj = new ChannelItemUserConfig();
        try
        {

            DB db = new DB();
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_CHANNEL_ITEM_LINK_USER_CONFIG_GET_BY_USER_ID", new string[,] {
            { "@user_id", userId},
            }, "ChannelItemUserConfig=>GetByUserId(string userId)");
            if (db.RowEffect >= 0)
            {
                try
                {
                    foreach (DataRow r in tbl.Rows)
                    {
                        obj = new ChannelItemUserConfig()
                        {
                            ChannelItemId = r["channel_item_id"].ToString(),
                            UserId = r["user_id"].ToString(),
                            UserName = r["User_name"].ToString(),
                            ChannelName = r["channel_name"].ToString(),
                            ChannelNameKh = r["channel_name_kh"].ToString()
                        };
                        break;


                    }
                    _tranSection = true;
                }
                catch (Exception ex)
                {
                    _tranSection = false;
                    _message = ex.Message;
                    obj = new ChannelItemUserConfig();
                    Log.AddExceptionToLog("Error function [GetByUserId(string userId)] in class [ChannelItemUserConfig], detail: " + ex.Message);

                }
            }
            else
            {
                //get data error
                _tranSection = false;
                _message = db.Message;
            }

        }
        catch (Exception ex)
        {
            _message = ex.Message;
            _tranSection = false;
            obj = new ChannelItemUserConfig();
            Log.AddExceptionToLog("Error function [GetByUserId(string userId)] in class [ChannelItemUserConfig], detail: " + ex.Message);

        }
        return obj;
    }

    public ChannelItemUserConfig GetByUserName(string userName)
    {
        ChannelItemUserConfig obj = new ChannelItemUserConfig();
        try
        {

            DB db = new DB();
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_CHANNEL_ITEM_LINK_USER_CONFIG_GET_BY_USER_NAME", new string[,] {
            { "@user_name", userName},
            }, "ChannelItemUserConfig=>GetByUserName(string userName)");
            if (db.RowEffect >= 0)
            {
                try
                {
                    foreach (DataRow r in tbl.Rows)
                    {
                        obj = new ChannelItemUserConfig()
                        {
                            ChannelItemId = r["channel_item_id"].ToString(),
                            UserId = r["user_id"].ToString(),
                            UserName = r["User_name"].ToString(),
                            ChannelName = r["channel_name"].ToString(),
                            ChannelNameKh = r["channel_name_kh"].ToString()
                        };
                        break;


                    }
                    _tranSection = true;
                }
                catch (Exception ex)
                {
                    _tranSection = false;
                    _message = ex.Message;
                    obj = new ChannelItemUserConfig();
                    Log.AddExceptionToLog("Error function [GetByUserName(string userName)] in class [ChannelItemUserConfig], detail: " + ex.Message);

                }
            }
            else
            {
                //get data error
                _tranSection = false;
                _message = db.Message;
            }

        }
        catch (Exception ex)
        {
            _message = ex.Message;
            _tranSection = false;
            obj = new ChannelItemUserConfig();
            Log.AddExceptionToLog("Error function [GetByUserName(string userName)] in class [ChannelItemUserConfig], detail: " + ex.Message);

        }
        return obj;
    }

}
