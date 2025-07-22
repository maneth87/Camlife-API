using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_customer_lead
/// </summary>
public class bl_customer_lead
{
    private string _ID;
	public bl_customer_lead()
	{
		//
		// TODO: Add constructor logic here
		//
        _ID = Helper.GetNewGuid(new string[,] { { "TABLE", "CT_CUSTOMER_LEAD" }, { "FIELD", "ID" } });
    }

    #region Properties
    public string ID
    {
        get
        {
            return _ID;
        }
        set
        {
            _ID = value;
        }
    }
    public string BranchCode { get; set; }
    public string BranchName { get; set; }
    public string ApplicationID { get; set; }
    public string ReferralStaffId { get; set; }
    public string ReferralStaffName { get; set; }
    public string ReferralStaffPosition { get; set; }
    public string ClientType { get; set; }
    public string ClientCIF { get; set; }
    public string ClientNameENG { get; set; }
    public string ClientNameKHM { get; set; }
    public string ClientGender { get; set; }
    public string ClientNationality { get; set; }
    public DateTime ClientDoB { get; set; }
    public string ClientVillage { get; set; }
    public string ClientCommune { get; set; }
    public string ClientDistrict { get; set; }
    public string ClientProvince { get; set; }
    /// <summary>
    /// ID type
    /// </summary>
    public string DocumentType { get; set; }
    /// <summary>
    /// ID Number
    /// </summary>
    public string DocumentId { get; set; }
    public string ClientPhoneNumber { get; set; }
    public string Interest { get; set; }
    public DateTime ReferredDate { get; set; }
    /// <summary>
    /// Is an insurance application number
    /// </summary>
    public string InsuranceApplicationId { get; set; }
    public string Status { get; set; }
    public string StatusRemarks { get; set; }
    public string Remarks { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedOn { get; set; }
    public string ExternalUpdatedBy { get;set; }
    public DateTime ExternalUpdatedOn { get; set; }

    public string Address
    {
        get
        {

            return ClientVillage + " " + ClientCommune + " " + ClientDistrict;
        }
    }
    public string APIUserID { get; set; }

    public string LeadType { get; set; }
    #endregion

}