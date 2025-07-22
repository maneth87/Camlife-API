using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for da_customer_lead
/// </summary>
public class da_customer_lead
{

	public da_customer_lead()
	{
		//
		// TODO: Add constructor logic here
		//
        SUCCESS = false;
        MESSAGE = "";
        CODE = "";
	}
    public static bool SUCCESS;
    public static string MESSAGE;
    public static string CODE;
    public static string InsertCustomerLead(bl_customer_lead obj_customer_lead)
    {
        string customer_lead_id = "";
        bool result = false;
        try
        {
            DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_CUSTOMER_LEAD_INSERT", new string[,] {
            {"@ID",obj_customer_lead.ID},
	        {"@BRANCH_CODE",obj_customer_lead.BranchCode}  ,
            {"@BRANCH_NAME",obj_customer_lead.BranchName}  ,
	        {"@APPLICATION_ID",obj_customer_lead.ApplicationID}  ,
	        {"@REFERRAL_STAFF_ID",obj_customer_lead.ReferralStaffId} ,
	        {"@REFERRAL_STAFF_NAME", obj_customer_lead.ReferralStaffName}  ,
	        {"@REFERRAL_STAFF_POSITION",obj_customer_lead.ReferralStaffPosition}  ,
	        {"@CLIENT_TYPE",obj_customer_lead.ClientType} ,
	        {"@CIF", obj_customer_lead.ClientCIF} ,
	        {"@CLIENT_NAME_IN_ENGLISH", obj_customer_lead.ClientNameENG} ,
	        {"@CLIENT_NAME_IN_KHMER", obj_customer_lead.ClientNameKHM} ,
	        {"@GENDER",obj_customer_lead.ClientGender},
	        {"@NATIONALITY", obj_customer_lead.ClientNationality},
	        {"@DATE_OF_BIRTH", obj_customer_lead.ClientDoB+""},
	        {"@VILLAGE",obj_customer_lead.ClientVillage} ,
	        {"@COMMUNE", obj_customer_lead.ClientCommune} ,
	        {"@DISTRICT", obj_customer_lead.ClientDistrict} ,
	        {"@PROVINCE", obj_customer_lead.ClientProvince},
	        {"@ID_TYPE", obj_customer_lead.DocumentType} ,
	        {"@ID_NUMBER", obj_customer_lead.DocumentId} ,
	        {"@PHONE_NUMBER", obj_customer_lead.ClientPhoneNumber},
	        {"@INTEREST", obj_customer_lead.Interest},
	        {"@REFERRED_DATE", obj_customer_lead.ReferredDate+""} ,
	        {"@STATUS", obj_customer_lead.Status},
            {"@STATUS_REMARKS", obj_customer_lead.StatusRemarks},
	        {"@Remarks", obj_customer_lead.Remarks},
	        {"@Created_By", obj_customer_lead.CreatedBy} ,
	        {"@Created_On", obj_customer_lead.CreatedOn+""} ,
            {"@API_USER", obj_customer_lead.APIUserID },
            {"@LEAD_TYPE", obj_customer_lead.LeadType }
            }, "da_customer_lead ==> InsertCustomerLead(bl_customer_lead obj_customer_lead)");
            if (result)
            {
                customer_lead_id = obj_customer_lead.ID;

            }
            else
            {
                customer_lead_id = "";

            }
            SUCCESS = result;
            MESSAGE = db.Message;
            CODE = db.Code;
        }
        catch (Exception ex)
        {
            CODE = "0";
            MESSAGE = ex.Message;
            customer_lead_id = "";
            Log.AddExceptionToLog("Error function [InsertCustomerLead(bl_customer_lead obj_customer_lead)] in class [da_customer_lead], detail: " + ex.Message + " ==> " + ex.StackTrace);
        }
       
        return customer_lead_id;
        
    }
    public static string UpdateCustomerLead(bl_customer_lead obj_customer_lead)
    {
        string customer_lead_id = "";
        bool result = false;
        try
        {
            DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_CUSTOMER_LEAD_UPDATE_BY_EXTERNAL", new string[,] {
            {"@ID",obj_customer_lead.ID},
            {"@BRANCH_CODE",obj_customer_lead.BranchCode}  ,
            {"@BRANCH_NAME",obj_customer_lead.BranchName}  ,
            {"@APPLICATION_ID",obj_customer_lead.ApplicationID}  ,
            {"@REFERRAL_STAFF_ID",obj_customer_lead.ReferralStaffId} ,
            {"@REFERRAL_STAFF_NAME", obj_customer_lead.ReferralStaffName}  ,
            {"@REFERRAL_STAFF_POSITION",obj_customer_lead.ReferralStaffPosition}  ,
            {"@CLIENT_TYPE",obj_customer_lead.ClientType} ,
            {"@CIF", obj_customer_lead.ClientCIF} ,
            {"@CLIENT_NAME_IN_ENGLISH", obj_customer_lead.ClientNameENG} ,
            {"@CLIENT_NAME_IN_KHMER", obj_customer_lead.ClientNameKHM} ,
            {"@GENDER",obj_customer_lead.ClientGender},
            {"@NATIONALITY", obj_customer_lead.ClientNationality},
            {"@DATE_OF_BIRTH", obj_customer_lead.ClientDoB+""},
            {"@VILLAGE",obj_customer_lead.ClientVillage} ,
            {"@COMMUNE", obj_customer_lead.ClientCommune} ,
            {"@DISTRICT", obj_customer_lead.ClientDistrict} ,
            {"@PROVINCE", obj_customer_lead.ClientProvince},
            {"@ID_TYPE", obj_customer_lead.DocumentType} ,
            {"@ID_NUMBER", obj_customer_lead.DocumentId} ,
            {"@PHONE_NUMBER", obj_customer_lead.ClientPhoneNumber},
            {"@INTEREST", obj_customer_lead.Interest},
            {"@REFERRED_DATE", obj_customer_lead.ReferredDate+""} ,
            //{"@STATUS", obj_customer_lead.Status},
            //{"@STATUS_REMARKS", obj_customer_lead.StatusRemarks},
            //{"@Remarks", obj_customer_lead.Remarks},
            {"@EXTERNAL_UPDATED_BY", obj_customer_lead.ExternalUpdatedBy} ,
            {"@EXTERNAL_UPDATED_ON", obj_customer_lead.ExternalUpdatedOn+""}//,
            //{"@API_User", obj_customer_lead.APIUserID }
            }, "da_customer_lead ==> UpdateCustomerLead(bl_customer_lead obj_customer_lead)");
            if (result)
            {
                customer_lead_id = obj_customer_lead.ID;
            }
            else
            {
                customer_lead_id = "";

            }
            SUCCESS = result;
            MESSAGE = db.Message;
            CODE = db.Code;

        }
       
        catch (Exception ex)
        {
            CODE = "0";//unexpected error
            MESSAGE = ex.Message;
            customer_lead_id = "";
            Log.AddExceptionToLog("Error function [InsertCustomerLead(bl_customer_lead obj_customer_lead)] in class [da_customer_lead], detail: " + ex.Message + " ==> " + ex.StackTrace);
        }

        return customer_lead_id;

    }

    public static bool UpdateCustomerLeadStatus(string status, string status_remarks, string id, string updated_by, DateTime updated_on)
    {
        bool result=false;
        try
        {
           DB  db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_CUSTOMER_LEAD_UPDATE_STATUS", new string[,] {
            {"@ID",id},
	        
	        {"@STATUS", status},
	        {"@STATUS_REMARKS", status_remarks},
	        {"@UPDATED_BY", updated_by} ,
	        {"@UPDATED_ON", updated_on+""} 
            }, "da_customer_lead ==> UpdateCustomerLeadStatus(string status, string status_remarks, string id, string updated_by, DateTime updated_on)");

            MESSAGE = db.Message;
        }
        catch (Exception ex)
        {
            MESSAGE = ex.Message;
           result=false;
           Log.AddExceptionToLog("Error function [UpdateCustomerLeadStatus(string status, string status_remarks, string id, string updated_by, DateTime updated_on)] in class [da_customer_lead], detail: " + ex.Message + " ==> " + ex.StackTrace);
        }
        SUCCESS = result;
        return result;

    }
    public static bool UpdateCustomerLeadInsuranceApplicationNumber(string INSURANCE_APPLICATION_NUMBER,  string id, string updated_by, DateTime updated_on)
    {
        bool result = false;
        try
        {
            DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_CUSTOMER_LEAD_UPDATE_INSURANCE_APP_NUMBER", new string[,] {
            {"@ID",id},
	        {"@INSURANCE_APPLICATION_NUMBER", INSURANCE_APPLICATION_NUMBER},
	        {"@UPDATED_BY", updated_by} ,
	        {"@UPDATED_ON", updated_on+""} 
            }, "da_customer_lead ==> UpdateCustomerLeadInsuranceApplicationNumber(string INSURANCE_APPLICATION_NUMBER,  string id, string updated_by, DateTime updated_on)");

            MESSAGE = db.Message;
        }
        catch (Exception ex)
        {
            MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [UpdateCustomerLeadInsuranceApplicationNumber(string INSURANCE_APPLICATION_NUMBER,  string id, string updated_by, DateTime updated_on)] in class [da_customer_lead], detail: " + ex.Message + " ==> " + ex.StackTrace);
        }
        SUCCESS = result;
        return result;

    }

    public static List<bl_customer_lead> GetCustomerLead()
    {
        List<bl_customer_lead> list_obj = new List<bl_customer_lead>();
        try
        {
            DB db = new DB();
            DataTable tbl = new DataTable();
            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_CUSTOMER_LEAD_GET", new string[,] { }, "da_customer_lead ==> GetCustomerLead()");
            foreach (DataRow row in tbl.Rows)
            {
                list_obj.Add(new bl_customer_lead() {
                ID= row["ID"].ToString(),
                BranchCode=row["BRANCH_CODE"].ToString(),
                BranchName=row["BRANCH_NAME"].ToString(),
                ApplicationID=row["APPLICATION_ID"].ToString(),
                ReferralStaffId=row["REFERRAL_STAFF_ID"].ToString(),
                ReferralStaffName=row["REFERRAL_STAFF_NAME"].ToString(),
                ReferralStaffPosition=row["REFERRAL_STAFF_POSITION"].ToString(),
                ClientType=row["CLIENT_TYPE"].ToString(),
                ClientCIF=row["CIF"].ToString(),
                ClientNameENG=row["CLIENT_NAME_IN_ENGLISH"].ToString(),
                ClientNameKHM=row["CLIENT_NAME_IN_KHMER"].ToString(),
                ClientGender=row["GENDER"].ToString(),
                ClientNationality=row["NATIONALITY"].ToString(),
                ClientDoB=Convert.ToDateTime(row["DATE_OF_BIRTH"].ToString()),
                ClientVillage=row["VILLAGE"].ToString(),
                ClientCommune=row["COMMUNE"].ToString(),
                ClientDistrict=row["DISTRICT"].ToString(),
                ClientProvince=row["PROVINCE"].ToString(),
                DocumentType= row["ID_TYPE"].ToString(),
                DocumentId=row["ID_NUMBER"].ToString(),
                ClientPhoneNumber = row["PHONE_NUMBER"].ToString(),
                Interest=row["INTEREST"].ToString(),
                ReferredDate=Convert.ToDateTime(row["REFERRED_DATE"].ToString()),
                Status=row["STATUS"].ToString(),
                StatusRemarks=row["STATUS_REMARKS"].ToString(),
                Remarks=row["REMARKS"].ToString(),
                CreatedBy=row["CREATED_BY"].ToString(),
                CreatedOn=Convert.ToDateTime(row["CREATED_ON"].ToString()),
                UpdatedBy=row["UPDATED_BY"].ToString(),
                UpdatedOn= Convert.ToDateTime( row["UPDATED_ON"].ToString())
                });
            }
            
        }
        catch (Exception ex)
        {
            list_obj = new List<bl_customer_lead>();
            Log.AddExceptionToLog("Error function [GetCustomerLead()] in class [da_customer_lead], detail: " + ex.Message + " ==> " + ex.StackTrace);

        }
        return list_obj;
    }
    public static List<bl_customer_lead> GetCustomerLead(string cif)
    {
        List<bl_customer_lead> list_obj = new List<bl_customer_lead>();
        try
        {
            DB db = new DB();
            DataTable tbl = new DataTable();
            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_CUSTOMER_LEAD_GET_BY_CIF", new string[,] { 
           {"@cif", cif}
            }, "da_customer_lead ==> GetCustomerLead(string cif)");
            foreach (DataRow row in tbl.Rows)
            {
                list_obj.Add(new bl_customer_lead()
                {
                    ID = row["ID"].ToString(),
                    BranchCode = row["BRANCH_CODE"].ToString(),
                    BranchName = row["BRANCH_NAME"].ToString(),
                    ApplicationID = row["APPLICATION_ID"].ToString(),
                    ReferralStaffId = row["REFERRAL_STAFF_ID"].ToString(),
                    ReferralStaffName = row["REFERRAL_STAFF_NAME"].ToString(),
                    ReferralStaffPosition = row["REFERRAL_STAFF_POSITION"].ToString(),
                    ClientType = row["CLIENT_TYPE"].ToString(),
                    ClientCIF = row["CIF"].ToString(),
                    ClientNameENG = row["CLIENT_NAME_IN_ENGLISH"].ToString(),
                    ClientNameKHM = row["CLIENT_NAME_IN_KHMER"].ToString(),
                    ClientGender = row["GENDER"].ToString(),
                    ClientNationality = row["NATIONALITY"].ToString(),
                    ClientDoB = Convert.ToDateTime(row["DATE_OF_BIRTH"].ToString()),
                    ClientVillage = row["VILLAGE"].ToString(),
                    ClientCommune = row["COMMUNE"].ToString(),
                    ClientDistrict = row["DISTRICT"].ToString(),
                    ClientProvince = row["PROVINCE"].ToString(),
                    DocumentType = row["ID_TYPE"].ToString(),
                    DocumentId = row["ID_NUMBER"].ToString(),
                    ClientPhoneNumber = row["PHONE_NUMBER"].ToString(),
                    Interest = row["INTEREST"].ToString(),
                    ReferredDate = Convert.ToDateTime(row["REFERRED_DATE"].ToString()),
                    InsuranceApplicationId = row["insurance_application_number"].ToString(),
                    Status = row["STATUS"].ToString(),
                    StatusRemarks = row["STATUS_REMARKS"].ToString(),
                    Remarks = row["REMARKS"].ToString(),
                    CreatedBy = row["CREATED_BY"].ToString(),
                    CreatedOn = Convert.ToDateTime(row["CREATED_ON"].ToString()),
                    UpdatedBy = row["UPDATED_BY"].ToString(),
                    UpdatedOn = Convert.ToDateTime(row["UPDATED_ON"].ToString()),
                    LeadType = row["lead_type"].ToString()
                });
            }

        }
        catch (Exception ex)
        {
            list_obj = new List<bl_customer_lead>();
            Log.AddExceptionToLog("Error function [GetCustomerLead(string cif)] in class [da_customer_lead], detail: " + ex.Message + " ==> " + ex.StackTrace);

        }
        return list_obj;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="clientName">Client name in english</param>
    /// <param name="clientGender"></param>
    /// <param name="clientDob"></param>
    /// <param name="clientIdType"></param>
    /// <param name="clientIdNumber"></param>
    /// <returns></returns>
    public static List<bl_customer_lead> GetCustomerLead(string clientName, string clientGender, DateTime clientDob, string clientIdType, string clientIdNumber)
    {
        List<bl_customer_lead> list_obj = new List<bl_customer_lead>();
        try
        {
            DB db = new DB();
            DataTable tbl = new DataTable();
            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_CUSTOMER_LEAD_GET_BY_NAME", new string[,] {
           {"@client_name_en",clientName},{"@client_gender", clientGender},{"@client_dob", clientDob+""},{"@client_id_type", clientIdType},{"@client_id_number", clientIdNumber}
            }, "da_customer_lead ==> GetCustomerLead(string clientName, string clientGender, DateTime clientDob, string clientIdType, string clientIdNumber)");
            foreach (DataRow row in tbl.Rows)
            {
                list_obj.Add(new bl_customer_lead()
                {
                    ID = row["ID"].ToString(),
                    BranchCode = row["BRANCH_CODE"].ToString(),
                    BranchName = row["BRANCH_NAME"].ToString(),
                    ApplicationID = row["APPLICATION_ID"].ToString(),
                    ReferralStaffId = row["REFERRAL_STAFF_ID"].ToString(),
                    ReferralStaffName = row["REFERRAL_STAFF_NAME"].ToString(),
                    ReferralStaffPosition = row["REFERRAL_STAFF_POSITION"].ToString(),
                    ClientType = row["CLIENT_TYPE"].ToString(),
                    ClientCIF = row["CIF"].ToString(),
                    ClientNameENG = row["CLIENT_NAME_IN_ENGLISH"].ToString(),
                    ClientNameKHM = row["CLIENT_NAME_IN_KHMER"].ToString(),
                    ClientGender = row["GENDER"].ToString(),
                    ClientNationality = row["NATIONALITY"].ToString(),
                    ClientDoB = Convert.ToDateTime(row["DATE_OF_BIRTH"].ToString()),
                    ClientVillage = row["VILLAGE"].ToString(),
                    ClientCommune = row["COMMUNE"].ToString(),
                    ClientDistrict = row["DISTRICT"].ToString(),
                    ClientProvince = row["PROVINCE"].ToString(),
                    DocumentType = row["ID_TYPE"].ToString(),
                    DocumentId = row["ID_NUMBER"].ToString(),
                    ClientPhoneNumber = row["PHONE_NUMBER"].ToString(),
                    Interest = row["INTEREST"].ToString(),
                    ReferredDate = Convert.ToDateTime(row["REFERRED_DATE"].ToString()),
                    InsuranceApplicationId = row["insurance_application_number"].ToString(),
                    Status = row["STATUS"].ToString(),
                    StatusRemarks = row["STATUS_REMARKS"].ToString(),
                    Remarks = row["REMARKS"].ToString(),
                    CreatedBy = row["CREATED_BY"].ToString(),
                    CreatedOn = Convert.ToDateTime(row["CREATED_ON"].ToString()),
                    UpdatedBy = row["UPDATED_BY"].ToString(),
                    UpdatedOn = Convert.ToDateTime(row["UPDATED_ON"].ToString()),
                    LeadType = row["lead_type"].ToString()
                });
            }

        }
        catch (Exception ex)
        {
            list_obj = new List<bl_customer_lead>();
            Log.AddExceptionToLog("Error function [GetCustomerLead(string clientName, string clientGender, DateTime clientDob, string clientIdType, string clientIdNumber)] in class [da_customer_lead], detail: " + ex.Message + " ==> " + ex.StackTrace);

        }
        return list_obj;
    }
    public static bl_customer_lead GetCustomerLeadByApplicationID(string APPLICATION_ID)
    {
        bl_customer_lead obj = new bl_customer_lead();
        try
        {
            DB db = new DB();
            DataTable tbl = new DataTable();
            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_CUSTOMER_LEAD_GET_BY_APPLICATION_ID", new string[,] {
            
            {"@APPLICATION_ID", APPLICATION_ID}
            }, "da_customer_lead ==> GetCustomerLeadByApplicationID(string APPLICATION_ID)");
            foreach (DataRow row in tbl.Rows)
            {
                obj=(new bl_customer_lead()
                {
                    ID = row["ID"].ToString(),
                    BranchCode = row["BRANCH_CODE"].ToString(),
                    BranchName = row["BRANCH_NAME"].ToString(),
                    ApplicationID = row["APPLICATION_ID"].ToString(),
                    ReferralStaffId = row["REFERRAL_STAFF_ID"].ToString(),
                    ReferralStaffName = row["REFERRAL_STAFF_NAME"].ToString(),
                    ReferralStaffPosition = row["REFERRAL_STAFF_POSITION"].ToString(),
                    ClientType = row["CLIENT_TYPE"].ToString(),
                    ClientCIF = row["CIF"].ToString(),
                    ClientNameENG = row["CLIENT_NAME_IN_ENGLISH"].ToString(),
                    ClientNameKHM = row["CLIENT_NAME_IN_KHMER"].ToString(),
                    ClientGender = row["GENDER"].ToString(),
                    ClientNationality = row["NATIONALITY"].ToString(),
                    ClientDoB = Convert.ToDateTime(row["DATE_OF_BIRTH"].ToString()),
                    ClientVillage = row["VILLAGE"].ToString(),
                    ClientCommune = row["COMMUNE"].ToString(),
                    ClientDistrict = row["DISTRICT"].ToString(),
                    ClientProvince = row["PROVINCE"].ToString(),
                    DocumentType = row["ID_TYPE"].ToString(),
                    DocumentId = row["ID_NUMBER"].ToString(),
                    ClientPhoneNumber = row["PHONE_NUMBER"].ToString(),
                    Interest = row["INTEREST"].ToString(),
                    ReferredDate = Convert.ToDateTime(row["REFERRED_DATE"].ToString()),
                    InsuranceApplicationId = row["insurance_application_number"].ToString(),
                    Status  = row["STATUS"].ToString(),
                    StatusRemarks = row["STATUS_REMARKS"].ToString(),
                    Remarks = row["REMARKS"].ToString(),
                    CreatedBy = row["CREATED_BY"].ToString(),
                    CreatedOn = Convert.ToDateTime(row["CREATED_ON"].ToString()),
                    UpdatedBy = row["UPDATED_BY"].ToString(),
                    UpdatedOn = Convert.ToDateTime(row["UPDATED_ON"].ToString())
                });
            }

        }
        catch (Exception ex)
        {
            obj = new bl_customer_lead();
            Log.AddExceptionToLog("Error function [GetCustomerLeadByApplicationID(string APPLICATION_ID)] in class [da_customer_lead], detail: " + ex.Message + " ==> " + ex.StackTrace);

        }
        return obj;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="APPLICATION_ID">Application from HTB</param>
    /// <param name="CREATED_BY">Camlife IA</param>
    /// <param name="CREATED_ON">Date time of copying</param>
    /// <returns></returns>
    public static bool CopyToApplication(string APPLICATION_ID, string CREATED_BY, DateTime CREATED_ON, string CHANNEL_ITEM_ID , string CHANNEL_LOCATION_ID, string SALE_AGENT_ID, string SALE_AGENT_NAME)
    {
        bool result = false;
        try
        {
            DB db = new DB();

            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_CUSTOMER_LEAD_APP_TEMP_INSERT", new string[,] {
            {"@APPLICATION_ID", APPLICATION_ID}, {"@CREATED_BY", CREATED_BY}, {"@CREATED_ON", CREATED_ON+""},
            {"@CHANNEL_ITEM_ID", CHANNEL_ITEM_ID},{"@CHANNEL_LOCATION_ID", CHANNEL_LOCATION_ID},{"@SALE_AGENT_ID", SALE_AGENT_ID},{"@SALE_AGENT_NAME", SALE_AGENT_NAME}
            }, "[da_customer_lead => CopyToApplication(string APPLICATION_ID, string CREATED_BY, DateTime CREATED_ON, string CHANNEL_ITEM_ID , string CHANNEL_LOCATION_ID, string SALE_AGENT_ID, string SALE_AGENT_NAME)]");

            if (result)
            {
                MESSAGE = "Success";
            }
            else 
            {
                MESSAGE = db.Message;
            }
        }
        catch (Exception ex)
        {
            result = false;
            MESSAGE = ex.Message;
            Log.AddExceptionToLog("Error function[CopyToApplication(string APPLICATION_ID, string CREATED_BY, DateTime CREATED_ON, string CHANNEL_ITEM_ID , string CHANNEL_LOCATION_ID, string SALE_AGENT_ID, string SALE_AGENT_NAME)] in class [da_customer_lead], detail: " + ex.Message + "==>" + ex.StackTrace);

        }
        SUCCESS = result;
        return result;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="F_DATE">Start Refer Date</param>
    /// <param name="T_DATE">End Refer Date</param>
    /// <returns></returns>
    public static DataTable GetLeadReport(DateTime F_DATE, DateTime T_DATE)
    {
        DataTable tbl = new DataTable();
        try
        {
            DB db = new DB();
            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_CUSTOMER_LEAD_GET_REPORT", new string[,] {
            {"@REFERRED_DATE_F", F_DATE+""},{"@REFERRED_DATE_T",T_DATE+""}
            }, "da_customer_lead=>GetLeadReport(DateTime F_DATE, DateTime T_DATE)");
            if (db.RowEffect == -1)//error
            {
                MESSAGE = db.Message;
                SUCCESS = false;

            }
            else
            {
                MESSAGE = "Success";
                SUCCESS = true;
            }

        }
        catch (Exception ex)
        {
            MESSAGE = ex.Message;
            SUCCESS = false;
            tbl = new DataTable();
            Log.AddExceptionToLog("Error function [GetLeadReport(DateTime F_DATE, DateTime T_DATE)] in class [da_customer_lead], detail: " + ex.Message + "=>" + ex.StackTrace);
        }
        return tbl;
    }
}