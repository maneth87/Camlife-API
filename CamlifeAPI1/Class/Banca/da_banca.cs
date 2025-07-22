using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using NPOI.SS.Formula.Eval;
/// <summary>
/// Summary description for da_banca
/// </summary>
public class da_banca
{
	public da_banca()
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
    private static DB db = new DB();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="START_DATE">Issued Start Date</param>
    /// <param name="TO_DATE">Issued End Date</param>
    /// <param name="USER_ID">USER ID</param>
    /// <returns></returns>
    public static List<bl_daily_insurance_booking_htb> GetDailyInsuranceBookingHTB(DateTime  START_DATE, DateTime TO_DATE, string USER_ID)
    {
        List<bl_daily_insurance_booking_htb> obj = new List<bl_daily_insurance_booking_htb>();
        try
        {
            DataTable tbl = new DataTable();
            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_MICRO_POLICY_GET_INSURANCE_BOOKING", new string[,] {
            {"@ISSUED_DATE_F", START_DATE+""},
            {"@ISSUED_DATE_T", TO_DATE+""}
            }, "da_banca=>GetDailyInsuranceBookingHTB(DateTime  START_DATE, DateTime TO_DATE)");

            foreach (DataRow r in tbl.Select("API_USER='"+ USER_ID+ "'"))
            {
                obj.Add(new bl_daily_insurance_booking_htb()
                {
                    BranchCode = r["office_code"].ToString(),
                    BranchName = r["office_name"].ToString(),
                    ApplicationID = r["application_id"].ToString(),
                    ClientType = r["client_type"].ToString(),
                    InsuranceApplicationId = r["application_number"].ToString(),
                    CertificateNumber = r["policy_number"].ToString(),
                    PaymentReferenceNo = r["payment_reference_no"].ToString(),
                    ReferralStaffId = r["referral_staff_id"].ToString(),
                    ReferralStaffName = r["referral_staff_name"].ToString(),
                    ReferralStaffPosition = r["referral_staff_position"].ToString(),
                    ClientCIF = r["cif"].ToString(),
                    ClientNameENG = r["last_name_in_english"].ToString() + " " + r["first_name_in_english"].ToString(),
                    ClientNameKHM = r["last_name_in_khmer"].ToString()+ " " +r["first_name_in_khmer"].ToString(),
                    ClientDoB = Convert.ToDateTime(r["date_of_birth"].ToString()).ToString("dd-MMM-yyyy"),
                    ClientPhoneNumber = r["phone_number"].ToString(),
                    ClientGender =  r["gender"].ToString()=="1" ? "MALE": "Female",
                    DocumentType=r["id_type_text"].ToString(),
                    DocumentId=r["id_number"].ToString(),
                    ClientProvince = r["province_en"].ToString(),
                    ClientDistrict = r["district_en"].ToString(),
                    ClientCommune = r["commune_en"].ToString(),
                    ClientVillage = r["village_en"].ToString(),
                    EffectiveDate = Convert.ToDateTime(r["effective_date"].ToString()).ToString("dd-MMM-yyyy"),
                    MaturityDate = Convert.ToDateTime(r["maturity_date"].ToString()).ToString("dd-MMM-yyyy"),
                    InsuranceTenor = Convert.ToInt32( r["TERM_OF_COVER"].ToString()),
       
                    Premium = Convert.ToDouble(r["amount"].ToString()),
                    Currency = r["currency"].ToString(),
                    InsuranceType = "Micro Insurance",
                    PackageType = r["package"].ToString(),
                    InsuranceStatus = r["policy_status"].ToString()=="IF" ? "Approved": r["policy_status"].ToString() == "CAN" ? "Canceled" : r["policy_status"].ToString(),
                    ReferralFee = Convert.ToDouble(r["referral_fee"].ToString()),
                    ReferralIncentive = Convert.ToDouble(r["referral_incentive"].ToString()),
                    IAName = r["agent_name_en"].ToString(),
                    ClientStatus=r["Policy_status_remarks"].ToString(),
                    ReferredDate = Convert.ToDateTime(r["referred_date"].ToString()).ToString("dd-MMM-yyyy"),
                    IssuedDate = Convert.ToDateTime(r["issued_date"].ToString()).ToString("dd-MMM-yyyy")
                });
            }

            if (db.RowEffect == -1)
            {
                SUCCESS = false;
                MESSAGE = db.Message;
                CODE = db.Code;

            }
            else {
                SUCCESS = true;
                MESSAGE = "Success";
                CODE = "1000";//success code
            }

           
        }
        catch (Exception ex)
        {

            obj = new List<bl_daily_insurance_booking_htb>();
            SUCCESS = false;
            MESSAGE = ex.Message;
            CODE = "0";// unexpected error
        }
        return obj;
    }
    public static DataTable GetDataInsuranceBookingHTB(DateTime START_DATE, DateTime TO_DATE)
    {
        DataTable tbl = new DataTable();
        try
        {
            
            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_MICRO_POLICY_GET_INSURANCE_BOOKING", new string[,] {
            {"@ISSUED_DATE_F", START_DATE+""},
            {"@ISSUED_DATE_T", TO_DATE+""}
            }, "da_banca=>GetDailyInsuranceBookingHTB(DateTime  START_DATE, DateTime TO_DATE)");

            SUCCESS = true;
            MESSAGE = "Success";
        }
        catch (Exception ex)
        {
            tbl = new DataTable();
            SUCCESS = false;
            MESSAGE = ex.Message;
        }
        return tbl;
    }
    public class ApplicationConsumer
    {
        public string InsuranceApplicationId { get; set; }
        public string ClientNameENG { get; set; }
        public double Premium { get; set; }
        public string Currency { get; set; }

        /// <summary>
        /// GET APPLICATION NUMBER OF CAMLIFE
        /// </summary>
        /// <param name="APPLICATION_NUMBER"></param>
        /// <returns></returns>
        public static ApplicationConsumer GetApplicationConsumer(string APPLICATION_NUMBER)
        {
            MESSAGE = "Success";
            SUCCESS = true;

            bl_micro_application app = new bl_micro_application();
            try
            {
                app = da_micro_application.GetApplication(APPLICATION_NUMBER);
                if (app.APPLICATION_NUMBER != null)
                {
                    DataTable tbl = da_micro_application.GetApplicationDetailByApplicationID(app.APPLICATION_ID);
                    if (tbl.Rows.Count > 0)
                    {
                        var r = tbl.Rows[0];
                        return new ApplicationConsumer() { InsuranceApplicationId = r["application_number"].ToString(), ClientNameENG = r["last_name_in_english"].ToString() + " " + r["first_name_in_english"].ToString(), Currency = "USD", Premium = (Convert.ToDouble(r["total_amount"].ToString()) + Convert.ToDouble(r["rider_total_amount"].ToString())) };

                    }
                    else
                    {
                        if (!da_micro_application.SUCCESS)
                        {
                            MESSAGE = da_micro_application.MESSAGE;
                            CODE = da_micro_application.CODE;
                            SUCCESS = false;
                        }
                        return new ApplicationConsumer();

                    }
                }
                else
                {
                    
                    if (!da_micro_application.SUCCESS)
                    {
                        MESSAGE = da_micro_application.MESSAGE;
                        CODE = da_micro_application.CODE;
                        SUCCESS = false;
                    }

                    return new ApplicationConsumer();
                }
            }
            catch (Exception ex)
            {
                MESSAGE = ex.Message;
                CODE = "0";
                Log.AddExceptionToLog("Error function [GetApplicationConsumer(string APPLICATION_NUMBER)] in class [da_banca => ApplicationConsumer], detail:" + ex.Message + " ==>" + ex.StackTrace);
                return new ApplicationConsumer();
            }

        }
        public static ApplicationConsumer GetApplicationConsumer(string APPLICATION_NUMBER, string CHANNEL_ITEM_ID)
        {
            MESSAGE = "Success";
            SUCCESS = true;
           // bl_micro_application app = new bl_micro_application();
            try
            {
                #region V1
                /* V1
                app = da_micro_application.GetApplication(APPLICATION_NUMBER);
                if (app.APPLICATION_NUMBER != null)
                {
                    if (app.CHANNEL_ITEM_ID == CHANNEL_ITEM_ID)
                    {
                        DataTable tbl = da_micro_application.GetApplicationDetailByApplicationID(app.APPLICATION_ID);
                        if (tbl.Rows.Count > 0)
                        {
                            var r = tbl.Rows[0];
                            return new ApplicationConsumer() { InsuranceApplicationId = r["application_number"].ToString(), ClientNameENG = r["last_name_in_english"].ToString() + " " + r["first_name_in_english"].ToString(), Currency = "USD", Premium = (Convert.ToDouble(r["total_amount"].ToString()) + Convert.ToDouble(r["rider_total_amount"].ToString())) };

                        }
                        else
                        {
                            if (!da_micro_application.SUCCESS)
                            {
                                MESSAGE = da_micro_application.MESSAGE;
                                CODE = da_micro_application.CODE;
                                SUCCESS = false;
                            }
                            return new ApplicationConsumer();

                        }
                    }
                    else
                    {
                        return new ApplicationConsumer();
                    }

                }
                else
                {

                    if (!da_micro_application.SUCCESS)
                    {
                        MESSAGE = da_micro_application.MESSAGE;
                        CODE = da_micro_application.CODE;
                        SUCCESS = false;
                    }

                    return new ApplicationConsumer();
                }
                */
                #endregion V1
                /*V2 check application consumer with new store procedure 
                 updated by maneth @07-Dec-2023
                 */
                DataTable tbl = da_micro_application.GetApplicationConsumer(APPLICATION_NUMBER);
                if (tbl.Rows.Count > 0)
                {
                    var r = tbl.Rows[0];
                    if (CHANNEL_ITEM_ID == r["CHANNEL_ITEM_ID"].ToString())
                    {
                        return new ApplicationConsumer() { InsuranceApplicationId = r["application_number"].ToString(), ClientNameENG = r["last_name_in_english"].ToString() + " " + r["first_name_in_english"].ToString(), Currency = "USD", Premium = (Convert.ToDouble(r["total_amount"].ToString()) + Convert.ToDouble(r["rider_total_amount"].ToString())) };
                    }
                   else
                    {
                        return new ApplicationConsumer();
                    }
                }
                else
                {
                    if (!da_micro_application.SUCCESS)
                    {
                        MESSAGE = da_micro_application.MESSAGE;
                        CODE = da_micro_application.CODE;
                        SUCCESS = false;
                    }
                    return new ApplicationConsumer();

                }
            }
            catch (Exception ex)
            {
                MESSAGE = ex.Message;
                CODE = "0";
                Log.AddExceptionToLog("Error function [GetApplicationConsumer(string APPLICATION_NUMBER)] in class [da_banca => ApplicationConsumer], detail:" + ex.Message + " ==>" + ex.StackTrace);
                return new ApplicationConsumer();
            }

        }
    }

    public class PaymentHTB
    {
        private string _ID;
        public PaymentHTB()
        {
            _ID = Helper.GetNewGuid(new string[,] { { "TABLE", "CT_MICRO_PAYMENT_HTB" }, { "FIELD", "ID" } });
        }
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
        public string PaymentReferenceNo { get; set; }
        public string TransactionType { get; set; }
        public string InsuranceApplicationId { get; set; }
        public string ClientNameENG { get; set; }
        public string Currency { get; set; }
        public double Premium { get; set; }
        public DateTime PaymentDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Remarks { get; set; }


        public static bool SavePaymentHTB(PaymentHTB OBJ_PAYMENT)
        {
            bool result = false;
            DB db= new DB();
            try
            {
                result = db.Execute(AppConfiguration.GetConnectionString(),"SP_CT_MICRO_PAYMENT_HTB_INSERT",new string[,]{
                {"@ID",OBJ_PAYMENT.ID}, {"@BRANCH_CODE",OBJ_PAYMENT.BranchCode}, {"@BRANCH_NAME",OBJ_PAYMENT.BranchName}, {"@PAYMENT_REFERENCE_NO",OBJ_PAYMENT.PaymentReferenceNo},
                {"@TRANSACTION_TYPE",OBJ_PAYMENT.TransactionType},{"@INSURANCE_APPLICATION_NUMBER",OBJ_PAYMENT.InsuranceApplicationId},{"@CLIENT_NAME",OBJ_PAYMENT.ClientNameENG}, 
                {"@PREMIUM",OBJ_PAYMENT.Premium+""}, {"@PAYMENT_DATE",OBJ_PAYMENT.PaymentDate+""}, {"@CREATED_BY",OBJ_PAYMENT.CreatedBy}, {"@CREATED_ON",OBJ_PAYMENT.CreatedOn+""}, {"@REMARKS", OBJ_PAYMENT.Remarks}
                }, "da_banca => PaymentHTB => SavePaymentHTB(PaymentHTB OBJ_PAYMENT)");

                SUCCESS = result;
                MESSAGE = db.Message;
            }
            catch (Exception ex)
            {
                SUCCESS = false;
                result = false;
                MESSAGE = ex.Message;
                Log.AddExceptionToLog("Error function [SavePaymentHTB(PaymentHTB OBJ_PAYMENT)] in class [da_banca => PaymentHTB], detail:" + ex.Message + " ==>" + ex.StackTrace);
            }
            return result;
        }
        public class PaymentHTBReport : PaymentHTB
        {
            public string CHANNEL_NAME { get; set; }
            public string CHANNEL_ITEM_ID { get; set; }
            public string CHANNEL_LOCATION_ID { get; set; }
            public string OFFICE_CODE { get; set; }
            public string OFFICE_NAME { get; set; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="F_DATE">From Payment Date</param>
        /// <param name="T_DATE">To Payment Date</param>
        /// <param name="CHANNEL_ITEM_ID">Company ID, Skip condition input blank</param>
        /// <param name="CHANNEL_LOCATION_ID">ID of the location channel, Skip condition input blank</param>
        /// <returns></returns>
        public static List<PaymentHTBReport> GetPaymentReport(DateTime F_DATE, DateTime T_DATE, string CHANNEL_ITEM_ID, string CHANNEL_LOCATION_ID)
        {
            List<PaymentHTBReport> list = new List<PaymentHTBReport>();
            try
            {
                DB db = new DB();
                DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "CT_MICRO_PAYMENT_HTB_GET", new string[,] {
                {"@F_DATE", F_DATE+""},{"@T_DATE",T_DATE+""},
                {"@CHANNEL_ITEM_ID", CHANNEL_ITEM_ID},{"@CHANNEL_LOCATION_ID", CHANNEL_LOCATION_ID}
                }, "da_banca => PaymentHTB => GetPaymentReport(DateTime F_DATE, DateTime T_DATE)");
                if (db.RowEffect == -1)
                {
                    MESSAGE = db.Message;
                    SUCCESS = false;

                }
                else
                {
                    if (tbl.Rows.Count > 0)
                    {
                        SUCCESS = true;
                        MESSAGE = "Success";
                        foreach (DataRow r in tbl.Rows)
                        {
                            list.Add(new PaymentHTBReport()
                            { 
                            ID = r["id"].ToString(),
                            BranchCode=r["branch_code"].ToString(),
                            BranchName=r["branch_name"].ToString(),
                             ClientNameENG=r["client_name"].ToString(),
                              InsuranceApplicationId=r["insurance_application_number"].ToString(),
                              PaymentDate= Convert.ToDateTime(r["payment_date"].ToString()),
                              PaymentReferenceNo=r["payment_reference_no"].ToString(),
                               Premium=Convert.ToDouble(r["premium"].ToString()),
                                Currency= r["currency"].ToString(),
                                 TransactionType=r["transaction_type"].ToString(),
                                  CreatedBy=r["created_by"].ToString(),
                                  CreatedOn=Convert.ToDateTime(r["created_on"].ToString()),
                                  UpdatedBy=r["updated_by"].ToString(),
                                  UpdatedOn=Convert.ToDateTime(r["updated_on"].ToString()),
                                  Remarks=r["remarks"].ToString(),
                                  CHANNEL_NAME=r["channel_name"].ToString(),
                                  CHANNEL_ITEM_ID=r["channel_item_id"].ToString(),
                                  CHANNEL_LOCATION_ID=r["channel_location_id"].ToString(),
                                  OFFICE_CODE=r["office_code"].ToString(),
                                  OFFICE_NAME=r["office_name"].ToString()
                            });
                        
                        }
                    }
                    else
                    {
                        MESSAGE = "No Record Found";
                        SUCCESS = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MESSAGE = ex.Message;
                SUCCESS = false;
                Log.AddExceptionToLog("Error function [GetPaymentReport(DateTime F_DATE, DateTime T_DATE)] in class [da_banca => PaymentHTB], detail: " + ex.Message + "=>" + ex.StackTrace);
                list = new List<PaymentHTBReport>();
            }
            return list;
        }
    }
    public class PaymentHTBObjectString
    {
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string PaymentReferenceNo { get; set; }
        public string TransactionType { get; set; }
        public string InsuranceApplicationId { get; set; }
        public string ClientNameENG { get; set; }
        public string Currency { get; set; }
        public string Premium { get; set; }
        public string PaymentDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public string Remarks { get; set; }
    }


    public static bool RoleBackIssuePolicy(string CUSTOMER_ID, string POLICY_ID)
    {
        bool result = false;
        try
        {
            DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_MICRO_ROLEBACK_ISSUE_CUST_ID_POL_ID", new string[,] {
            {"@CUSTOMER_ID", CUSTOMER_ID},
            {"@POLICY_ID", POLICY_ID}
            }, "da_banca=>RoleBackIssuePolicy((string CUSTOMER_ID, string POLICY_ID)");
        }
        catch (Exception ex)
        {
            SUCCESS = false;
            result = false;
            MESSAGE = ex.Message;
            Log.AddExceptionToLog("da_banca, detail:", "RoleBackIssuePolicy(string CUSTOMER_ID, string POLICY_ID)" , ex );
        }

        return result;
    }

    /// <summary>
    /// Get lead data + application + policy information
    /// </summary>
    /// <param name="CIF"></param>
    /// <returns></returns>
    public static DataTable GetLeadApplicationPolicy(string CIF)
    {
        DataTable tbl = new DataTable();
        try
        {

            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_V_CT_MICRO_APPLICATION_LEAD_POLICY_GET", new string[,] {
            {"@cif",CIF},
          
            }, "da_banca=>GetLeadApplicationPolicy(string CIF)");
            if (db.RowEffect < 0)
            {
                SUCCESS = false;
                MESSAGE = db.Message;
                CODE = db.Code;
            }
            else
            {
                MESSAGE = "Success";
                SUCCESS = true;
            }

        }
        catch (Exception ex)
        {
            tbl = new DataTable();
            SUCCESS = false;
            MESSAGE = ex.Message;
            Log.AddExceptionToLog("Error function [GetLeadApplicationPolicy(string CIF)] in class [da_banca], detail:" + ex.Message + " ==>" + ex.StackTrace);

        }
        return tbl;
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
    public static DataTable GetLeadApplicationPolicy(string clientName, string clientGender, DateTime clientDob, string clientIdType, string clientIdNumber)
    {
        DataTable tbl = new DataTable();
        try
        {

            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_V_CT_MICRO_APPLICATION_LEAD_POLICY_GET_BY_NAME", new string[,] {
            {"@client_name_en",clientName},{"@client_gender", clientGender},{"@client_dob", clientDob+""},{"@client_id_type", clientIdType},{"@client_id_number", clientIdNumber}

            }, "da_banca=>GetLeadApplicationPolicy(string clientName, string clientGender, DateTime clientDob, string clientIdType, string clientIdNumber)");
            if (db.RowEffect < 0)
            {
                SUCCESS = false;
                MESSAGE = db.Message;
                CODE = db.Code;
            }
            else
            {
                MESSAGE = "Success";
                SUCCESS = true;
            }
           
          
        }
        catch (Exception ex)
        {
            tbl = new DataTable();
            SUCCESS = false;
            MESSAGE = ex.Message;
            CODE = "0";
            Log.AddExceptionToLog("Error function [GetLeadApplicationPolicy(string clientName, string clientGender, DateTime clientDob, string clientIdType, string clientIdNumber)] in class [da_banca], detail:" + ex.Message + " ==>" + ex.StackTrace);
        }
        return tbl;
    }
    /// <summary>
    /// V_CT_MICRO_APPLICATION_LEAD_POLICY + CT_MICRO_POLICY_EXPIRING
    /// </summary>
    /// <param name="CIF">Client identify from bank</param>
    /// <returns></returns>
    public static DataTable GetLeadPolicyExpiring(string CIF)
    {
        DataTable tbl = new DataTable();
        try
        {

            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_V_CT_CUSTOMER_LEAD_POLICY_EXPIRING_GET", new string[,] {
            {"@cif",CIF},

            }, "da_banca=>GetLeadPolicyExpiring(string CIF)");

            if (db.RowEffect < 0)
            {
                SUCCESS = false;
                MESSAGE = db.Message;
                CODE = db.Code;
            }
            else
            {
                MESSAGE = "Success";
                SUCCESS = true;
            }

        }
        catch (Exception ex)
        {
            tbl = new DataTable();
            SUCCESS = false;
            MESSAGE = ex.Message;
            Log.AddExceptionToLog("Error function [GetLeadPolicyExpiring(string CIF)] in class [da_banca], detail:" + ex.Message + " ==>" + ex.StackTrace);

        }
        return tbl;
    }
    /// <summary>
    /// V_CT_MICRO_APPLICATION_LEAD_POLICY + CT_MICRO_POLICY_EXPIRING
    /// </summary>
    /// <param name="clientName">Client name in english</param>
    /// <param name="clientGender"></param>
    /// <param name="clientDob"></param>
    /// <param name="clientIdType"></param>
    /// <param name="clientIdNumber"></param>
    /// <returns></returns>
    public static DataTable GetLeadPolicyExpiring(string clientName, string clientGender, DateTime clientDob, string clientIdType, string clientIdNumber)
    {
        DataTable tbl = new DataTable();
        try
        {

            tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_V_CT_CUSTOMER_LEAD_POLICY_EXPIRING_GET_BY_NAME", new string[,] {
            {"@client_name_en",clientName},{"@client_gender", clientGender},{"@client_dob", clientDob+""},{"@client_id_type", clientIdType},{"@client_id_number", clientIdNumber}

            }, "da_banca=> GetLeadPolicyExpiring(string clientName, string clientGender, DateTime clientDob, string clientIdType, string clientIdNumber)");
            if (db.RowEffect < 0)
            {
                SUCCESS = false;
                MESSAGE = db.Message;
                CODE = db.Code;
            }
            else
            {
                MESSAGE = "Success";
                SUCCESS = true;
            }


        }
        catch (Exception ex)
        {
            tbl = new DataTable();
            SUCCESS = false;
            MESSAGE = ex.Message;
            CODE = "0";
            Log.AddExceptionToLog("Error function [GetLeadPolicyExpiring(string clientName, string clientGender, DateTime clientDob, string clientIdType, string clientIdNumber)] in class [da_banca], detail:" + ex.Message + " ==>" + ex.StackTrace);
        }
        return tbl;
    }
}