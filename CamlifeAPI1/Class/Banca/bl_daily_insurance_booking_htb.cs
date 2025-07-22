using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_daily_insurance_booking_htb
/// </summary>
public class bl_daily_insurance_booking_htb
{
	public bl_daily_insurance_booking_htb()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string  BranchCode {get;set;}
    public string BranchName{get;set;}
    public string ApplicationID{get;set;}
    public string ClientType{get;set;}
    public string InsuranceApplicationId { get; set; }
    public string CertificateNumber { get; set; }
    public string PaymentReferenceNo { get; set; }
    public string ReferralStaffId { get; set; }
    public string ReferralStaffName { get; set; }
    public string ReferralStaffPosition { get; set; }
    public string ClientCIF { get; set; }
    public string ClientNameENG { get; set; }
    public string ClientNameKHM { get; set; }
    public string ClientDoB { get; set; }
    public string ClientPhoneNumber { get; set; }
    public string ClientGender { get; set; }
    /// <summary>
    /// ID Type
    /// </summary>
    public string DocumentType { get; set; }
    /// <summary>
    /// ID Number
    /// </summary>
    public string DocumentId { get; set; }
    public string ClientProvince { get; set; }
    public string ClientDistrict { get; set; }
    public string ClientCommune { get; set; }
    public string ClientVillage { get; set; }
    public string EffectiveDate { get; set; }
    public string MaturityDate { get; set; }
    public int InsuranceTenor { get; set; }
    public double Premium { get; set; }
    public string Currency { get; set; }
    public string InsuranceType { get; set; }
    public string PackageType { get; set; }
    public string InsuranceStatus { get; set; }
    public double ReferralFee { get; set; }
    public double ReferralIncentive { get; set; }
    public string IAName { get; set; }
    /// <summary>
    /// New or Renew
    /// </summary>
    public string ClientStatus { get; set; }
    public string ReferredDate { get; set; }
    public string IssuedDate { get; set; }
}

