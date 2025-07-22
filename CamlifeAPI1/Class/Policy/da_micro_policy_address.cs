using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for da_micro_policy_address
/// </summary>
public class da_micro_policy_address
{
    private static bool _SUCCESS = false;
    private static string _MESSAGE = "";

    public static bool SUCCESS { get { return _SUCCESS; } }
    public static string MESSAGE { get { return _MESSAGE; } }
    private static DB db = new DB();

	public da_micro_policy_address()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static bool SaveAddress(bl_micro_policy_address ADDRESS)
    {
        bool result = false;
        try
        {
            //DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_ADDRESS_INSERT", new string[,] {
            {"@ID", ADDRESS.ID},
            {"@HOUSE_NO_KH", ADDRESS.HouseNoKh},
            {"@HOUSE_NO_EN", ADDRESS.HouseNoEn},
             {"@STREET_NO_KH", ADDRESS.StreetNoKh},
              {"@STREET_NO_EN", ADDRESS.StreetNoEn},
            {"@POLICY_ID", ADDRESS.PolicyID}, 
            {"@PROVINCE_CODE",ADDRESS.ProvinceCode}, 
            {"@DISTRICT_CODE", ADDRESS.DistrictCode}, 
            {"@COMMUNE_CODE", ADDRESS.CommuneCode}, 
            {"@VILLAGE_CODE", ADDRESS.VillageCode}, 
            {"@CREATED_BY", ADDRESS.CreatedBy}, 
            {"@CREATED_ON", ADDRESS.CreatedOn+""}
            
            }, "da_micro_policy_address=>SaveAddress(bl_micro_policy_address ADDRESS)");

            if (db.RowEffect == -1) //error
            {
                _MESSAGE = db.Message;
            }
            else
            {
                _MESSAGE = "Success";
            }
            _SUCCESS = result;
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [SaveAddress(bl_micro_policy_address ADDRESS)] in class [da_micro_policy_address], detail: " + ex.Message + "==>" + ex.StackTrace);

        }

        return result;
    }

    public static bool DeleteAddress(string ID)
    {
        bool result = false;
        try
        {
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_ADDRESS_DELETE", new string[,] {
            {"@ID", ID}
            
            }, "da_micro_policy_beneficiary=>DeleteBeneficiary(string ID)");
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [DeleteAddress(string ID)] in class [da_micro_policy_address], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }
}