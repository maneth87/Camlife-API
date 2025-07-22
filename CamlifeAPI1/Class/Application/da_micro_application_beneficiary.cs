using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Http.Validation;
/// <summary>
/// Summary description for da_micro_application_beneficiary
/// </summary>
public class da_micro_application_beneficiary
{
    public da_micro_application_beneficiary()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private static bool _SUCCESS = false;
    private static string _MESSAGE = "";

    public static bool SUCCESS { get { return _SUCCESS; } }
    public static string MESSAGE { get { return _MESSAGE; } }
    private static DB db = new DB();
    public static bool SaveApplicationBeneficiary(bl_micro_application_beneficiary APP_BENEFICIARY)
    {
        bool result = false;
        try
        {
            //DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_BENEFICIARY_INSERT", new string[,] {
                {"@ID", APP_BENEFICIARY.ID},
            {"@APPLICATION_NUMBER", APP_BENEFICIARY.APPLICATION_NUMBER},
            {"@FULL_NAME",APP_BENEFICIARY.FULL_NAME},
            {"@AGE", APP_BENEFICIARY.AGE},
            {"@RELATION", APP_BENEFICIARY.RELATION},
            {"@PERCENTAGE_OF_SHARE", APP_BENEFICIARY.PERCENTAGE_OF_SHARE+""},
            {"@ADDRESS", APP_BENEFICIARY.ADDRESS},
            {"@CREATED_BY", APP_BENEFICIARY.CREATED_BY},
            {"@CREATED_ON", APP_BENEFICIARY.CREATED_ON+""},
            {"@REMARKS", APP_BENEFICIARY.REMARKS},
            { "@DOB", APP_BENEFICIARY.DOB+"" },
            { "@GENDER", APP_BENEFICIARY.Gender+"" },
            { "@ID_TYPE",APP_BENEFICIARY.IdType+"" },
            { "@ID_NO",APP_BENEFICIARY.IdNo }

        }, "da_micro_application_insurance_benificiary=>SaveApplicationBeneficiary(bl_micro_application_beneficiary APP_BENEFICIARY)");

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
            Log.AddExceptionToLog("Error function [SaveApplicationBeneficiary(bl_micro_application_beneficiary APP_BENEFICIARY)] in class [da_micro_application_insurance_beneficiary], detail: " + ex.Message + "==>" + ex.StackTrace);

        }

        return result;
    }
    public static bool UpdateApplicationBeneficiary(bl_micro_application_beneficiary APP_BENEFICIARY)
    {
        bool result = false;
        try
        {
            //DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_BENEFICIARY_UPDATE", new string[,] {
                {"@ID", APP_BENEFICIARY.ID},
            {"@APPLICATION_NUMBER", APP_BENEFICIARY.APPLICATION_NUMBER},
            {"@FULL_NAME",APP_BENEFICIARY.FULL_NAME},
            {"@AGE", APP_BENEFICIARY.AGE},
            {"@RELATION", APP_BENEFICIARY.RELATION},
            {"@PERCENTAGE_OF_SHARE", APP_BENEFICIARY.PERCENTAGE_OF_SHARE+""},
            {"@ADDRESS", APP_BENEFICIARY.ADDRESS},
            {"@UPDATED_BY", APP_BENEFICIARY.UPDATED_BY},
            {"@UPDATED_ON", APP_BENEFICIARY.UPDATED_ON+""},
            {"@REMARKS", APP_BENEFICIARY.REMARKS},
            { "@DOB", APP_BENEFICIARY.DOB+"" },
            { "@GENDER", APP_BENEFICIARY.Gender+"" },
            { "@ID_TYPE",APP_BENEFICIARY.IdType+"" },
            { "@ID_NO",APP_BENEFICIARY.IdNo }
            }, "da_micro_application_insurance_benificiary=>UpdateApplicationBeneficiary(bl_micro_application_beneficiary APP_BENEFICIARY)");

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
            Log.AddExceptionToLog("Error function [UpdateApplicationBeneficiary(bl_micro_application_beneficiary APP_BENEFICIARY)] in class [da_micro_application_insurance_beneficiary], detail: " + ex.Message + "==>" + ex.StackTrace);

        }

        return result;
    }
    public static bool DeleteApplicationBeneficiary(string APPLICATION_NUMBER)
    {
        bool result = false;
        try
        {
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_BENEFICIARY_DELETE", new string[,] {
            {"@APPLICATION_NUMBER", APPLICATION_NUMBER}

            }, "da_micro_application_insurance_beneficiary=>DeleteApplicationBeneficiary(string APPLICATION_NUMBER)");
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [DeleteApplicationBeneficiary(string APPLICATION_NUMBER)] in class [da_micro_application_insurance_beneficiary], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }

    public enum DeleteApplicationBeneficiaryOption { ID, APPLICATION_NUMBER }
    public static bool DeleteApplicationBeneficiary(DeleteApplicationBeneficiaryOption option, string value)
    {
        bool result = false;
        try
        {
            if (option == DeleteApplicationBeneficiaryOption.APPLICATION_NUMBER)
            {
                result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_BENEFICIARY_DELETE", new string[,] {
            {"@APPLICATION_NUMBER", value}

            }, "da_micro_application_insurance_beneficiary=>DeleteApplicationBeneficiary(DeleteApplicationBeneficiaryOption option, string value)");
            }
            else if (option == DeleteApplicationBeneficiaryOption.ID)
            {
                result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_BENEFICIARY_DELETE_BY_ID", new string[,] {
            {"@ID", value}

            }, "da_micro_application_insurance_beneficiary=>DeleteApplicationBeneficiary(DeleteApplicationBeneficiaryOption option, string value)");
            }
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("da_micro_application_insurance_beneficiary", "DeleteApplicationBeneficiary(DeleteApplicationBeneficiaryOption option, string value)", ex);
        }
        return result;
    }
    public class PremaryBeneficiary
    {
        public static bool Save(
          bl_micro_application_beneficiary.PrimaryBeneciary primaryBen)
        {
            bool flag;
            try
            {
                flag = da_micro_application_beneficiary.db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_BENEFICIARY_PRIMARY_INSERT", new string[8, 2]
                {
          {
            "@ID",
            primaryBen.Id
          },
          {
            "@APPLICATION_NUMBER",
            primaryBen.ApplicationNumber
          },
          {
            "@FULL_NAME",
            primaryBen.FullName
          },
          {
            "@LOAN_NUMBER",
            primaryBen.LoanNumber
          },
          {
            "@ADDRESS",
            primaryBen.Address
          },
          {
            "@CREATED_BY",
            primaryBen.CreatedBy
          },
          {
            "@CREATED_ON",
            primaryBen.CreatedOn.ToString() + ""
          },
          {
            "@CREATED_REMARKS",
            primaryBen.CreatedRemarks
          }
                }, "da_micro_application_insurance_benificiary.PremaryBeneficiary=>Save(bl_micro_application_beneficiary.PrimaryBeneciary primaryBen)");
                da_micro_application_beneficiary._MESSAGE = da_micro_application_beneficiary.db.RowEffect != -1 ? "Success" : da_micro_application_beneficiary.db.Message;
                da_micro_application_beneficiary._SUCCESS = flag;
            }
            catch (Exception ex)
            {
                da_micro_application_beneficiary._SUCCESS = false;
                da_micro_application_beneficiary._MESSAGE = ex.Message;
                flag = false;
                Log.AddExceptionToLog($"Error function [Save(bl_micro_application_beneficiary.PrimaryBeneciary primaryBen)] in class [da_micro_application_insurance_beneficiary.PremaryBeneficiary], detail: {ex.Message}==>{ex.StackTrace}");
            }
            return flag;
        }

        public static bool Update(
          bl_micro_application_beneficiary.PrimaryBeneciary primaryBen)
        {
            bool flag;
            try
            {
                flag = da_micro_application_beneficiary.db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_BENEFICIARY_PRIMARY_UPDATE", new string[7, 2]
                {
          {
            "@APPLICATION_NUMBER",
            primaryBen.ApplicationNumber
          },
          {
            "@FULL_NAME",
            primaryBen.FullName
          },
          {
            "@LOAN_NUMBER",
            primaryBen.LoanNumber
          },
          {
            "@ADDRESS",
            primaryBen.Address
          },
          {
            "@UPDATED_BY",
            primaryBen.UpdatedBy
          },
          {
            "@UPDATED_ON",
            primaryBen.UpdatedOn.ToString() ?? ""
          },
          {
            "@UPDATED_REMARKS",
            primaryBen.UpdatedRemarks
          }
                }, "da_micro_application_insurance_benificiary.PremaryBeneficiary=>Update(bl_micro_application_beneficiary.PrimaryBeneciary primaryBen)");
                da_micro_application_beneficiary._MESSAGE = da_micro_application_beneficiary.db.RowEffect != -1 ? "Success" : da_micro_application_beneficiary.db.Message;
                da_micro_application_beneficiary._SUCCESS = flag;
            }
            catch (Exception ex)
            {
                da_micro_application_beneficiary._SUCCESS = false;
                da_micro_application_beneficiary._MESSAGE = ex.Message;
                flag = false;
                Log.AddExceptionToLog($"Error function [Update(bl_micro_application_beneficiary.PrimaryBeneciary primaryBen)] in class [da_micro_application_insurance_beneficiary.PremaryBeneficiary], detail: {ex.Message}==>{ex.StackTrace}");
            }
            return flag;
        }

        public static bool Delete(string applicationNumber)
        {
            bool flag;
            try
            {
                flag = da_micro_application_beneficiary.db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_BENEFICIARY_PRIMARY_DELETE", new string[1, 2]
                {
          {
            "@APPLICATION_NUMBER",
            applicationNumber
          }
                }, "da_micro_application_insurance_benificiary.PremaryBeneficiary=>Delete(string applicationNumber)");
                da_micro_application_beneficiary._MESSAGE = da_micro_application_beneficiary.db.RowEffect != -1 ? "Success" : da_micro_application_beneficiary.db.Message;
                da_micro_application_beneficiary._SUCCESS = flag;
            }
            catch (Exception ex)
            {
                da_micro_application_beneficiary._SUCCESS = false;
                da_micro_application_beneficiary._MESSAGE = ex.Message;
                flag = false;
                Log.AddExceptionToLog($"Error function [Delete(string applicationNumber)] in class [da_micro_application_insurance_beneficiary.PremaryBeneficiary], detail: {ex.Message}==>{ex.StackTrace}");
            }
            return flag;
        }

        public static bl_micro_application_beneficiary.PrimaryBeneciary Get(string applicationNumber)
        {
            bl_micro_application_beneficiary.PrimaryBeneciary primaryBeneciary = new bl_micro_application_beneficiary.PrimaryBeneciary();
           
            try
            {
                DataTable data = new DB().GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_BENEFICIARY_PRIMARY_GET", new string[1, 2]
                {
          {
            "@APPLICATION_NUMBER",
            applicationNumber
          }
                }, "da_micro_application_insurance_beneficiary.PremaryBeneficiary=>bl_micro_application_beneficiary.PrimaryBeneciary Get(string applicationNumber)");
                if (data.Rows.Count > 0)
                {
                    DataRow row = data.Rows[0];
                    primaryBeneciary = new bl_micro_application_beneficiary.PrimaryBeneciary()
                    {
                        ApplicationNumber = row["application_number"].ToString(),
                        FullName = row["full_name"].ToString(),
                        LoanNumber = row["loan_number"].ToString(),
                        Address = row["address"].ToString(),
                        Id = row["id"].ToString(),
                        CreatedBy = row["created_by"].ToString(),
                        CreatedOn = Convert.ToDateTime(row["created_on"].ToString()),
                        CreatedRemarks = row["created_Remarks"].ToString()
                    };
                }
                else
                    primaryBeneciary = (bl_micro_application_beneficiary.PrimaryBeneciary)null;
            }
            catch (Exception ex)
            {
                da_micro_application_beneficiary._SUCCESS = false;
                da_micro_application_beneficiary._MESSAGE = ex.Message;
                primaryBeneciary = (bl_micro_application_beneficiary.PrimaryBeneciary)null;
                Log.AddExceptionToLog($"Error function [bl_micro_application_beneficiary.PrimaryBeneciary Get(string applicationNumber)] in class [da_micro_application_insurance_beneficiary.PremaryBeneficiary], detail: {ex.Message}==>{ex.StackTrace}");
            }
            return primaryBeneciary;
        }
    }

}