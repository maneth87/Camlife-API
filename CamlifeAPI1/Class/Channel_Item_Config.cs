using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Data;
public class Channel_Item_Config
{
    public string ID { get; set; }
    public string ProductId { get; set; }
    public string ChannelItemId { get; set; }
    public int MaxPolicyPerLife { get; set; }
    public int Status { get; set; }
    public string Remarks { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedRemarks { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedOn { get; set; }
    public string UpdatedRemarks { get; set; }

    public static bool Transection { get { return _tranSection; } }
    public static string Message { get { return _message; } }
    //Private variable
    private static string _message = "";
    private static bool _tranSection=false;

    public Channel_Item_Config() { }
    /// <summary>
    /// Constructor for create new record
    /// </summary>
    /// <param name="chanelItemId"></param>
    /// <param name="status">set 0 inactive, set 1 active</param>
    /// <param name="remarks">[Optional: set empty]</param>
    /// <param name="createdBy"></param>
    /// <param name="createdOn"></param>
    /// <param name="createdRemarks">[Optional: set empty]</param>
    public Channel_Item_Config(string productId, string chanelItemId,int maxPolicyPerLife, int status, string remarks, string createdBy, DateTime createdOn, string createdRemarks)
    {
        ProductId= productId;
        ChannelItemId = chanelItemId;
        MaxPolicyPerLife = maxPolicyPerLife;
        Status = status;
        Remarks = remarks;
        CreatedBy = createdBy;
        CreatedOn = createdOn;
        CreatedRemarks = createdRemarks;

    }
    /// <summary>
    /// Constructor for update record
    /// </summary>
    /// <param name="id"></param>
    /// <param name="status">set 0 inactive, set 1 active</param>
    /// <param name="updatedBy"></param>
    /// <param name="updatedOn"></param>
    /// <param name="updatedRemarks">[Optional: set empty]</param>
    public Channel_Item_Config(string id,string productId, int maxPolicyPerLife, int status,  string updatedBy, DateTime updatedOn, string updatedRemarks)
    {
        ProductId = productId;
        ID = id;
        MaxPolicyPerLife= maxPolicyPerLife;
        Status = status;
        UpdatedBy    = updatedBy;
        UpdatedOn = updatedOn;
        UpdatedRemarks = UpdatedRemarks;

    }
   /// <summary>
   /// Created by maneth @19 Oct 2023
   /// </summary>
   /// <param name="channelItemId"></param>
   /// <param name="status">[1: Active, 0: Inactive, -1: all]</param>
   /// <returns></returns>
    public List<Channel_Item_Config> GetChannelItemConfig(string channelItemId, int status)
    { 
        List<Channel_Item_Config> chList=new List<Channel_Item_Config> ();
        try
        {

            DB db = new DB();
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_CHANNEL_ITEM_CONFIG_GET", new string[,] {
            { "@CHANNEL_ITEM_ID", channelItemId},
            { "@IS_ACTIVE",status+""}
            }, "Channel_Item_config=>GetChannelItemConfig(string channelItemId, int status)");
            if (db.RowEffect >= 0)
            {
                try
                {
                    foreach (DataRow r in tbl.Rows)
                    {
                        chList.Add(new Channel_Item_Config() {
                            ChannelItemId = r["channel_item_id"].ToString(),
                            ProductId = r["product_id"].ToString(),
                            ID = r["ID"].ToString(),
                            MaxPolicyPerLife = Convert.ToInt32(r["max_policy_per_life"].ToString()),
                            Status = Convert.ToInt32(r["status"].ToString()),
                            Remarks = r["remarks"].ToString(),
                            CreatedBy = r["created_by"].ToString(),
                            CreatedOn = Convert.ToDateTime(r["created_on"].ToString()),
                            CreatedRemarks = r["created_remarks"].ToString(),
                            UpdatedBy = r["updated_by"].ToString(),
                            UpdatedOn = Convert.ToDateTime(r["updated_on"].ToString()),
                            UpdatedRemarks = r["updated_remarks"].ToString()
                        });
                    }
                    _tranSection = true;
                }
                catch (Exception ex) {
                    _tranSection = false;
                    _message = ex.Message;
                    chList=new List<Channel_Item_Config>();
                    Log.AddExceptionToLog("Error function [GetChannelItemConfig(string channelItemId, int status)] in class [Channel_Item_Config], detail: " + ex.Message);
                }
            }
            else
            { 
                //get data error
                _tranSection=false;
                _message=db.Message;
            }
          
        }
        catch(Exception ex)
        {
            _message= ex.Message;
            _tranSection= false;
            chList=new List<Channel_Item_Config>();
            Log.AddExceptionToLog("Error function [GetChannelItemConfig(string channelItemId, int status)] in class [Channel_Item_Config], detail: " + ex.Message);

        }
        return chList;
    }

    
}
