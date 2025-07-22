using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamlifeAPI1.Class
{
    public class Inquiries
    {
        public class ApplicationInquiry
        {
            public string ApplicationNumber { get; set; }
            public DateTime ApplicationDate { get; set; }
            public string ChannelId { get; set; }
            public string ChannelItemId { get; set; }
            public string ChannelItemName { get; set; }
            public string ChannelItemNameKh { get; set; }
            public string ChannelLocationId { get; set; }
            public string BranchCode { get; set; }
            public string BranchName { get; set; }
            public string FullNameEn { get; set; }
            public string FullNameKh { get; set; }
            public DateTime Dob { get; set; }
            public string Gender { get; set; }
            public string GenderKh { get { return Helper.GetGenderInKhmer(Gender); } }
            public int IdType { get; set; }
            public string IdTypeKh { get { return Helper.GetIDCardTypeTextKh(IdType); } }
            public string IdNumber { get; set; }
            public string PhoneNumber { get; set; }
            public string PolicyNumber { get; set; }
            public string PolicyStatus { get; set; }
            public string CreatedBy { get; set; }
            public string ApplicationId { get; set; }
            public string PolicyId { get; set; }
            /// <summary>
            /// R is repayment
            /// N is new (application in first year)
            /// </summary>
            public string ApplicationType { get; set; }

        }

        public class ApplicationFilter
        {
            public string ApplicationId { get; set; }
            public string ApplicationNumber { get; set; }
            public string MainApplicationNumber { get; set; }
            public string ApplicationType { get; set; }
            public int NumbersApplication { get; set; }
            public int NumbersPurchasingYear { get; set; }
        }
    }
}