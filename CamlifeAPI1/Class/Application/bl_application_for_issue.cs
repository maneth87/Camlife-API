using NPOI.OpenXml4Net.OPC.Internal.Unmarshallers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace CamlifeAPI1.Class.Application
{
    public class bl_application_for_issue
    {
        public string PolicyNumber { get; set; }
        public bl_micro_application_customer Customer { get; set; }
        public bl_micro_application Application { get; set; }
        public bl_micro_application_insurance Insurance { get; set; }
        public bl_micro_application_insurance_rider Rider { get; set; }
        public List<bl_micro_application_beneficiary> Beneficiaries { get; set; }
        public bl_micro_application_questionaire Questionaire { get; set; }
        public bl_micro_application_beneficiary.PrimaryBeneciary PrimaryBeneciary { get; set; }

    }

    public class bl_application_detail_response
    {
        public string PolicyId { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyStatus { get; set; }
        public int Age { get; set; }
        public DateTime IssueDate { get; set; }
        public double CollectedPremium { get; set; }
        public string PaymentReferenceNo { get; set; }
        public CustomerResponse Customer { get; set; }
        public ApplicationResponse Application { get; set; }
        public AgentResponse Agent { get; set; }
        public ChannelResponse Channel { get; set; }
        public InsuranceResponse Insurance { get; set; }
        public RiderResponse Rider { get; set; }
        public List<BeneficiaryResponse> Beneficiaries { get; set; }
        public PrimaryBeneficiaryResponse PrimaryBeneficiary { get; set; }
        public QuestionaireResponse Questionaire { get; set; }
        public List<SubApplication> SubApplications { get; set; }


    }
    public class SubApplication
    {
        public string ApplicationNumber { get; set; }
        public string ApplicationId { get; set; }
        /// <summary>
        /// Amount after discount
        /// </summary>
        public double BasicAmount { get; set; }
        /// <summary>
        /// Amount after discount
        /// </summary>
        public double RiderAmount { get; set; }
        /// <summary>
        /// Total amount = Total basic after discount + total rider after discount
        /// </summary>
        public double TotalAmount { get; set; }
        public string ClientType { get; set; }
        public double SumAssure { get; set; }
    }
    public class CustomerResponse
    {
        public string CustomerId { get; set; }
        public int IdType { get; set; }
        public string IdTypeEn { get; set; }
        public string IdTypeKh { get; set; }
        public string IdNumber { get; set; }
        public string LastNameEn { get; set; }
        public string FirstNameEn { get; set; }
        public string LastNameKh { get; set; }
        public string FirstNameKh { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameKh { get; set; }
        public int Gender { get; set; }
        public string GenderEn { get; set; }
        public string GenderKh { get; set; }
        public DateTime Dob { get; set; }
        public string Nationality { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public AddressResponse Address { get; set; }
    }

    public class AddressResponse
    {
        public string HouseNo { get; set; }
        public string StreetNo { get; set; }
        public string Village { get; set; }
        public string VillageEn { get; set; }
        public string VillageKh { get; set; }
        public string Commune { get; set; }
        public string CommuneEn { get; set; }
        public string CommuneKh { get; set; }
        public string District { get; set; }
        public string DistrictEn { get; set; }
        public string DistrictKh { get; set; }
        public string Province { get; set; }
        public string ProvinceEn { get; set; }
        public string ProvinceKh { get; set; }
    }
    public class ApplicationResponse
    {
        public string ApplicationId { get; set; }
        public string ApplicationNumber { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string RenewalFromPolicy { get; set; }
        public string ClientType { get; set; }
        public string ClientTypeRemarks { get; set; }
        public string ClientTypeRelation { get; set; }
        public string ReferrerId { get; set; }
        public string Referrer { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn
        {
            get; set;
        }
        public String MainApplicationNumber { get; set; }
        public int NumbersOfApplicationFirstYear { get; set; }
        public int NumbersOfPurchasingYear { get; set; }
        public string LoanNumber { get; set; }


        public string PolicyholderName { get; set; }

        public int PolicyholderGender { get; set; }

        public DateTime PolicyholderDOB { get; set; }

        public int PolicyholderIDType { get; set; }

        public string PolicyholderIDNo { get; set; }

        public string PolicyholderPhoneNumber { get; set; }

        public string PolicyholderPhoneNumber2 { get; set; }

        public string PolicyholderEmail { get; set; }

        public string PolicyholderAddress { get; set; }
    }
    public class AgentResponse
    {
        public string AgentCode { get; set; }
        public string AgentNameEn { get; set; }
        public string AgentNameKh { get; set; }
    }
    public class ChannelResponse
    {
        public string ChannelId { get; set; }
        public string ChannelType { get; set; }
        public string ChannelItemId { get; set; }
        public string ChannelItemName { get; set; }
        public string ChannelItemNameKh { get; set; }
        public string ChannelLocationId { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
    }

    public class InsuranceResponse
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductNameKh { get; set; }
        public string Package { get; set; }
        public int CoverYear { get; set; }
        public int PaymentPeriod { get; set; }
        public int PaymentMode { get; set; }
        public string PaymentModeEn { get; set; }
        public string PaymentModeKh { get; set; }
        public string PaymentCode { get; set; }
        public double SumAssure { get; set; }
        public double Premium { get; set; }
        public double AnnualPremium { get; set; }
        public double DiscountAmount { get; set; }
        public double TotalAmount { get; set; }
        public double UserPremium { get; set; }

    }
    public class RiderResponse
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductNameKh { get; set; }
        public double SumAssure { get; set; }
        public double Premium { get; set; }
        public double AnnualPremium { get; set; }
        public double DiscountAmount { get; set; }
        public double TotalAmount { get; set; }
    }
    public class BeneficiaryResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Age { get; set; }
        public string Relation { get; set; }
        public double PercentageOfShare { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }

        public int Gender { get; set; }

        public int IdType { get; set; }

        public string IdNo { get; set; }
        public string IdTypeString { get { return IdType < 0 ? "" : Helper.GetIDCardTypeTextKh(IdType); } }

        public string GenderString
        {
            get {return Gender < 0 ? "" : Helper.GetGenderText(Gender, true, true); }
        }
    }
    public class PrimaryBeneficiaryResponse
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string LoanNumber { get; set; }

        public string Address { get; set; }
    }
    public class QuestionaireResponse
    {
        public string Id { get; set; }
        public string QuestionId { get; set; }
        public int Answer { get; set; }
        public string AnswerRemarks { get; set; }
    }
}