using CamlifeAPI1.Class.Application;
using CamlifeAPI1.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static CamlifeAPI1.Controllers.PolicyController;

namespace CamlifeAPI1.Controllers
{
    [Authorize]
    public class CustomerController : ApiController
    {
        [Route("api/customer/Get")]
        [HttpPost]
        public object GetCustomerInfo(RequestParameter.GetCustomer obj)
        {

            var err = new ErrorCode();
            var tranDate = DateTime.Now;

            try
            {
                object response = new object();
                var existCust = da_micro_customer.GetCustomerByIdNumber(obj.IdType, obj.IdNumber);
                if (existCust != null && existCust.ID_NUMBER != null)
                {
                    ResponseCustomer.Customer objCust = new ResponseCustomer.Customer()
                    {
                        ID = existCust.ID,
                        CUSTOMER_NUMBER = existCust.CUSTOMER_NUMBER,
                        CUSTOMER_TYPE = existCust.CUSTOMER_TYPE,
                        ID_TYPE = existCust.ID_TYPE,
                        ID_NUMBER = existCust.ID_NUMBER,
                        FIRST_NAME_IN_ENGLISH = existCust.FIRST_NAME_IN_ENGLISH,
                        LAST_NAME_IN_ENGLISH = existCust.LAST_NAME_IN_ENGLISH,
                        FIRST_NAME_IN_KHMER = existCust.FIRST_NAME_IN_KHMER,
                        LAST_NAME_IN_KHMER = existCust.LAST_NAME_IN_KHMER,
                        GENDER = existCust.GENDER,
                        DATE_OF_BIRTH = existCust.DATE_OF_BIRTH,
                        NATIONALITY = existCust.NATIONALITY.ToUpper() == "CAMBODIAN" ? "ខ្មែរ" : existCust.NATIONALITY,
                        MARITAL_STATUS = existCust.MARITAL_STATUS,
                        OCCUPATION = existCust.OCCUPATION,
                        HOUSE_NO = existCust.HOUSE_NO_EN == null ? existCust.HOUSE_NO_KH : existCust.HOUSE_NO_EN,
                        STREET_NO = existCust.STREET_NO_EN == null ? existCust.STREET_NO_KH : existCust.STREET_NO_EN,
                        VILLAGE = existCust.VILLAGE_EN,
                        COMMUNE = existCust.COMMUNE_EN,
                        DISTRICT = existCust.DISTRICT_EN,
                        PROVINCE = existCust.PROVINCE_EN,
                        PHONE_NUMBER = existCust.PHONE_NUMBER1,
                        EMAIL = existCust.EMAIL1
                    };
                    response = new ResponseExecuteSuccess() { message = "Success", detail = objCust };


                }
                else
                {
                    //da_micro_application_customer obj = new da_micro_application_customer();
                   
                    response = new ResponseExecuteSuccess() { message = "Success", detail = "Customer not found." };
                }

                return response;
            }
            catch (Exception ex)
            {

                Log.AddExceptionToLog("CustomerController", "RequestParameter.GetCustomer obj", ex);
                return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
            }

        }

        public class RequestParameter
        {
            public class GetCustomer
            {
                public int IdType { get; set; }
                public string IdNumber { get; set; }
            }
        }

        public class ResponseCustomer
        {
            public class Customer
            {
                public string ID { get; set; }

                public string CUSTOMER_NUMBER { get; set; }
                public string CUSTOMER_TYPE { get; set; }
                public string ID_TYPE { get; set; }
                public string ID_NUMBER { get; set; }
                public string FIRST_NAME_IN_ENGLISH { get; set; }
                public string LAST_NAME_IN_ENGLISH { get; set; }
                public string FIRST_NAME_IN_KHMER { get; set; }
                public string LAST_NAME_IN_KHMER { get; set; }
                public string GENDER { get; set; }
                public DateTime DATE_OF_BIRTH { get; set; }
                public string NATIONALITY { get; set; }
                public string MARITAL_STATUS { get; set; }
                public string OCCUPATION { get; set; }
                public string HOUSE_NO { get; set; }
                public string STREET_NO { get; set; }
                public string VILLAGE { get; set; }
                public string COMMUNE { get; set; }
                public string DISTRICT { get; set; }
                public string PROVINCE { get; set; }

                public string PHONE_NUMBER { get; set; }
                public string EMAIL { get; set; }

                public int STATUS { get; set; }

                public string FullNameKh { get { return GetFullNameKH(); } }
                public string FullNameEn { get { return GetFullNameEn(); } }

                private string GetFullNameKH()
                {
                    return LAST_NAME_IN_KHMER + " " + FIRST_NAME_IN_KHMER;
                }
                private string GetFullNameEn()
                {
                    return LAST_NAME_IN_ENGLISH + " " + FIRST_NAME_IN_ENGLISH;
                }
            }

        }
    }
}
