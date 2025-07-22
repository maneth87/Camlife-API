using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_micro_policy_address
/// </summary>
public class bl_micro_policy_address
{
      private string _ID = "";

    private string GetID()
    {
        string id = "";
        id = Helper.GetNewGuid(new string[,] { { "TABLE", "CT_MICRO_POLICY_ADDRESS" }, { "FIELD", "ID" } });
        return id;
    }
	public bl_micro_policy_address()
	{
		//
		// TODO: Add constructor logic here
		//
         _ID = GetID();
	}
    public string ID { get { return _ID; }
        set { _ID = value; }}
    public string PolicyID { get; set; }
    public string HouseNoKh { get; set; }
    public string HouseNoEn { get; set; }
    public string StreetNoKh { get; set; }
    public string StreetNoEn { get; set; }
    public string ProvinceCode { get; set; }
    public string DistrictCode { get; set; }
    public string CommuneCode { get; set; }
    public string VillageCode { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedOn { get; set; }
}