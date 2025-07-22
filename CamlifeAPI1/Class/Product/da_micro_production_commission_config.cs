using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for da_micro_production_commission_config
/// </summary>
public class da_micro_production_commission_config
{
    private string _errorMessage = "";
    private bool _transactionStatus=false;
    private string _clasName = "da_micro_production_commission_config";
	public da_micro_production_commission_config()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string ErrorMessage { get { return _errorMessage; } }
    public bool TransactionStatus{get{return _transactionStatus;}}
    public bool Save(bl_micro_product_commission_config proConfig)
    {
       
        bool result = false;
        try
        {
            DB db = new DB();

            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_PRODUCT_COMMISSION_CONFIG_INSERT", new string[,] { 
            {"@id", proConfig.ID},
            {"@channel_item_id", proConfig.ChannelItemId},
            {"@product_id", proConfig.ProductId},
            {"@commission_type", proConfig.CommissionType},
            {"@value_type", proConfig.ValueType},
            {"@value", proConfig.Value+""},
             {"@effective_from",proConfig.EffectiveFrom+""},
            {"@effective_to",proConfig.EffectiveTo+""},
            {"@status", proConfig.Status+""},
            {"@created_by", proConfig.CreatedBy},
            {"@created_on", proConfig.CreatedOn+""},
            {"@created_remarks", proConfig.CreatedRemarks},
            {"@client_type",proConfig.ClientType}
            }, "da_micro_production_commission_config=>Save(bl_micro_product_commission_config proConfig)");

            if (db.RowEffect < 0)
            {
                _errorMessage = db.Message;
                _transactionStatus=false;
            }
            else
            {
           _transactionStatus=true;

            }
        }
        catch (Exception ex)
        {
            _transactionStatus=false;
            _errorMessage = "Saved fail, please contact your system administrator.";
            Log.AddExceptionToLog("Error function [Save(bl_micro_product_commission_config proConfig)] in class [da_micro_production_commission_config], detail" + ex.Message + "=>" + ex.StackTrace);
        }
        return result;
    }
    /// <summary>
    /// Update commission config by Id
    /// </summary>
    /// <param name="proConfig"></param>
    /// <returns></returns>
    public bool Update(bl_micro_product_commission_config proConfig)
    {

        bool result = false;
        try
        {
            DB db = new DB();

            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_PRODUCT_COMMISSION_CONFIG_UPDATE", new string[,] { 
            {"@id", proConfig.ID},
            {"@channel_item_id", proConfig.ChannelItemId},
            {"@product_id", proConfig.ProductId},
            {"@commission_type", proConfig.CommissionType},
            {"@effective_from",proConfig.EffectiveFrom+""},
            {"@effective_to",proConfig.EffectiveTo+""},
            {"@value_type", proConfig.ValueType},
            {"@value", proConfig.Value+""},
            {"@status", proConfig.Status+""},
            {"@updated_by", proConfig.UpdatedBy},
            {"@updated_on", proConfig.UpdatedOn+""},
            {"@created_remarks", proConfig.CreatedRemarks},
            {"@updated_remarks",proConfig.UpdatedRemarks},
             {"@client_type",proConfig.ClientType}
            }, "da_micro_production_commission_config=>Update(bl_micro_product_commission_config proConfig)");

            if (db.RowEffect < 0)
            {
                _errorMessage = db.Message;
                _transactionStatus = false;
            }
            else
            {
                _transactionStatus = true;

            }
        }
        catch (Exception ex)
        {
            _transactionStatus = false;
            _errorMessage = "Update fail, please contact your system administrator.";
            Log.AddExceptionToLog("Error function [Update(bl_micro_product_commission_config proConfig)] in class [da_micro_production_commission_config], detail" + ex.Message + "=>" + ex.StackTrace);
        }
        return result;
    }

    /// <summary>
    /// Get production commission config by channel item id and product id
    /// </summary>
    /// <param name="channelItemId"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    public List<bl_micro_product_commission_config> GetProductionCommConfig(string channelItemId, string productId, string clientType)
    {
       List< bl_micro_product_commission_config> proConfList = new List<bl_micro_product_commission_config>();
        try
        {
            DB db = new DB();
            bl_micro_product_commission_config proConf;
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_PRODUCT_COMMISSION_CONFIG_GET_BY_CH_PRO_ID", new string[,] {
            {"@channel_item_id", channelItemId},{"@product_id",productId},{"@client_type",clientType}
            }, "da_micro_production_commission_config=>GetProductionCommConfig(string channelItemId, string productId)");
            if (db.RowEffect >= 0)
            {
                foreach (DataRow row in tbl.Rows)
                {
                    proConf = new bl_micro_product_commission_config();
                    proConf.ID = row["id"].ToString();
                    proConf.ChannelItemId = row["channel_item_id"].ToString();
                    proConf.ProductId = row["product_id"].ToString();
                    proConf.CommissionType = row["commission_type"].ToString();
                    proConf.ValueType = row["value_type"].ToString();
                    proConf.EffectiveFrom = Convert.ToDateTime(row["effective_from"].ToString());
                    proConf.EffectiveTo = Convert.ToDateTime(row["effective_to"].ToString());
                    proConf.Value = Convert.ToDouble(row["value"].ToString());
                    proConf.Status = Convert.ToInt32(row["status"].ToString());
                    proConf.CreatedBy = row["created_by"].ToString();
                    proConf.CreatedOn = Convert.ToDateTime(row["created_on"].ToString());
                    proConf.CreatedRemarks = row["created_remarks"].ToString();
                    proConf.UpdatedBy = row["updated_by"].ToString();
                    proConf.UpdatedOn = Convert.ToDateTime(row["updated_on"].ToString());
                    proConf.UpdatedRemarks = row["updated_remarks"].ToString();
                    proConf.ClientType = row["client_type"].ToString();
                    proConfList.Add(proConf);
                }
                _transactionStatus = true;
              
            }
            else if (db.RowEffect == -1)
            {
                _transactionStatus = false;
                _errorMessage = db.Message;
                proConfList = new List<bl_micro_product_commission_config>();
            }
            
           
        }
        catch (Exception ex)
        {
            proConfList = new List<bl_micro_product_commission_config>();
            _transactionStatus = false;
            _errorMessage = "Get Production Commission Config error, please contact your system administrator.";
            Log.AddExceptionToLog("Error function [GetProductionCommConfig(string channelItemId, string productId)] in class [da_micro_production_commission_config], detail" + ex.Message + "=>" + ex.StackTrace);

        }
        return proConfList;
    }
    public List<bl_micro_product_commission_config> GetProductionCommConfigList()
    {
        List<bl_micro_product_commission_config> proConfList = new List<bl_micro_product_commission_config>();
        try
        {
            DB db = new DB();
            bl_micro_product_commission_config proConf;
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_PRODUCT_COMMISSION_CONFIG_GET_ALL", new string[,] {
            }, "da_micro_production_commission_config=>GetProductionCommConfigList()");
            if (db.RowEffect >= 0)
            {
                foreach (DataRow row in tbl.Rows)
                {
                    proConf = new bl_micro_product_commission_config();
                    proConf.ID = row["id"].ToString();
                    proConf.ChannelItemId = row["channel_item_id"].ToString();
                    proConf.ProductId = row["product_id"].ToString();
                    proConf.CommissionType = row["commission_type"].ToString();
                    proConf.ValueType = row["value_type"].ToString();
                    proConf.EffectiveFrom = Convert.ToDateTime(row["effective_from"].ToString());
                    proConf.EffectiveTo = Convert.ToDateTime(row["effective_to"].ToString());
                    proConf.Value = Convert.ToDouble(row["value"].ToString());
                    proConf.Status = Convert.ToInt32(row["status"].ToString());
                    proConf.CreatedBy = row["created_by"].ToString();
                    proConf.CreatedOn = Convert.ToDateTime(row["created_on"].ToString());
                    proConf.CreatedRemarks = row["created_remarks"].ToString();
                    proConf.UpdatedBy = row["updated_by"].ToString();
                    proConf.UpdatedOn = Convert.ToDateTime(row["updated_on"].ToString());
                    proConf.UpdatedRemarks = row["updated_remarks"].ToString();
                    proConf.ClientType = row["client_type"].ToString();
                    proConfList.Add(proConf);
                }
                _transactionStatus = true;

            }
            else if (db.RowEffect == -1)
            {
                _transactionStatus = false;
                _errorMessage = db.Message;
                proConfList = new List<bl_micro_product_commission_config>();
            }


        }
        catch (Exception ex)
        {
            proConfList = new List<bl_micro_product_commission_config>();
            _transactionStatus = false;
            _errorMessage = "Get Production Commission Config error, please contact your system administrator.";
            Log.AddExceptionToLog("Error function [GetProductionCommConfigList()] in class [da_micro_production_commission_config], detail" + ex.Message + "=>" + ex.StackTrace);

        }
        return proConfList;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="colName">Column name</param>
    /// <param name="val">value for searching</param>
    /// <returns></returns>
    public List<bl_micro_product_commission_config> GetProductionCommConfigList(string colName, string val)
    {
        List<bl_micro_product_commission_config> proConfList = new List<bl_micro_product_commission_config>();
        try
        {
            DB db = new DB();
            bl_micro_product_commission_config proConf;
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_PRODUCT_COMMISSION_CONFIG_GET_LIST_BY_CONDI", new string[,] {
                {"@COL_NAME", colName},{"@VAL", val}
            }, "da_micro_production_commission_config=>GetProductionCommConfigList(string colName, string val)");
            if (db.RowEffect >= 0)
            {
                foreach (DataRow row in tbl.Rows)
                {
                    proConf = new bl_micro_product_commission_config();
                    proConf.ID = row["id"].ToString();
                    proConf.ChannelItemId = row["channel_item_id"].ToString();
                    proConf.ProductId = row["product_id"].ToString();
                    proConf.CommissionType = row["commission_type"].ToString();
                    proConf.ValueType = row["value_type"].ToString();
                    proConf.EffectiveFrom = Convert.ToDateTime(row["effective_from"].ToString());
                    proConf.EffectiveTo = Convert.ToDateTime(row["effective_to"].ToString());
                    proConf.Value = Convert.ToDouble(row["value"].ToString());
                    proConf.Status = Convert.ToInt32(row["status"].ToString());
                    proConf.CreatedBy = row["created_by"].ToString();
                    proConf.CreatedOn = Convert.ToDateTime(row["created_on"].ToString());
                    proConf.CreatedRemarks = row["created_remarks"].ToString();
                    proConf.UpdatedBy = row["updated_by"].ToString();
                    proConf.UpdatedOn = Convert.ToDateTime(row["updated_on"].ToString());
                    proConf.UpdatedRemarks = row["updated_remarks"].ToString();
                    proConf.ClientType = row["client_type"].ToString();
                    proConfList.Add(proConf);
                }
                _transactionStatus = true;

            }
            else if (db.RowEffect == -1)
            {
                _transactionStatus = false;
                _errorMessage = db.Message;
                proConfList = new List<bl_micro_product_commission_config>();
            }


        }
        catch (Exception ex)
        {
            proConfList = new List<bl_micro_product_commission_config>();
            _transactionStatus = false;
            _errorMessage = "Get Production Commission Config error, please contact your system administrator.";
            Log.AddExceptionToLog("Error function [GetProductionCommConfigList(string colName, string val)] in class [da_micro_production_commission_config], detail" + ex.Message + "=>" + ex.StackTrace);

        }
        return proConfList;
    }
    /// <summary>
    /// Get production commission config by channel item id , product id and commission type
    /// </summary>
    /// <param name="channelItemId"></param>
    /// <param name="productId"></param>
    /// <param name="commissionType"></param>
    /// <returns></returns>
    public bl_micro_product_commission_config GetProductionCommConfig(string channelItemId, string productId, string commissionType, string clientType)
    {
        bl_micro_product_commission_config proConf = new bl_micro_product_commission_config();
        try
        {
            DB db = new DB();
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_PRODUCT_COMMISSION_CONFIG_GET_BY_CH_PRO_ID_COMM_TYPE", new string[,] {
            {"@channel_item_id", channelItemId},{"@product_id",productId},{"@commission_type",commissionType},{"@client_type",clientType}
            }, "da_micro_production_commission_config=>GetProductionCommConfig(string channelItemId, string productId)");
            if (db.RowEffect > 0)
            {
                _transactionStatus = true;
                var row = tbl.Rows[0];
                proConf.ID = row["id"].ToString();
                proConf.ChannelItemId = row["channel_item_id"].ToString();
                proConf.ProductId = row["product_id"].ToString();
                proConf.CommissionType = row["commission_type"].ToString();
                proConf.ValueType = row["value_type"].ToString();
                proConf.Value = Convert.ToDouble(row["value"].ToString());
                proConf.EffectiveFrom = Convert.ToDateTime(row["effective_from"].ToString());
                proConf.EffectiveTo = Convert.ToDateTime(row["effective_to"].ToString());
                proConf.Status = Convert.ToInt32(row["status"].ToString());
                proConf.CreatedBy = row["created_by"].ToString();
                proConf.CreatedOn = Convert.ToDateTime(row["created_on"].ToString());
                proConf.CreatedRemarks = row["created_remarks"].ToString();
                proConf.UpdatedBy = row["updated_by"].ToString();
                proConf.UpdatedOn = Convert.ToDateTime(row["updated_on"].ToString());
                proConf.UpdatedRemarks = row["updated_remarks"].ToString();
                clientType = row["client_type"].ToString();
                
            }
            else if (db.RowEffect == 0)
            {
                _transactionStatus = true;
                proConf = new bl_micro_product_commission_config();
            }
            else if (db.RowEffect == -1)
            {
                _transactionStatus = false;
                _errorMessage = db.Message;
                proConf = new bl_micro_product_commission_config();
            }


        }
        catch (Exception ex)
        {
            proConf = new bl_micro_product_commission_config();
            _transactionStatus = false;
            _errorMessage = "Get Production Commission Config error, please contact your system administrator.";
            Log.AddExceptionToLog("Error function [GetProductionCommConfig(string channelItemId, string productId)] in class [da_micro_production_commission_config], detail" + ex.Message + "=>" + ex.StackTrace);

        }
        return proConf;
    }
}