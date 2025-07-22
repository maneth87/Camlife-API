using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// Summary description for da_micro_policy
/// </summary>
public class da_micro_policy_beneficiary
{
    public da_micro_policy_beneficiary()
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

    public static bool SaveBeneficiary(bl_micro_policy_beneficiary BENEFICIARY)
    {

        try
        {
            //DB db = new DB();
            _SUCCESS = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_BENEFICIARY_INSERT", new string[,] {
                {"@ID", BENEFICIARY.ID},
            {"@POLICY_ID", BENEFICIARY.POLICY_ID},
            {"@FULL_NAME",BENEFICIARY.FULL_NAME},
            {"@AGE", BENEFICIARY.AGE},
            {"@RELATION", BENEFICIARY.RELATION},
            {"@PERCENTAGE_OF_SHARE", BENEFICIARY.PERCENTAGE_OF_SHARE+""},
            {"@ADDRESS", BENEFICIARY.ADDRESS},
            {"@CREATED_BY", BENEFICIARY.CREATED_BY},
            {"@CREATED_ON", BENEFICIARY.CREATED_ON+""},
            {"@REMARKS", BENEFICIARY.REMARKS},
            { "@DOB", BENEFICIARY.BirthDate+"" },
            { "@GENDER", BENEFICIARY.Gender+"" },
            { "@ID_TYPE",BENEFICIARY.IdType+"" },
            { "@ID_NO",BENEFICIARY.IdNo }
            }, "da_micro_policy_benificiary=>SaveBeneficiary(bl_micro_policy_beneficiary BENEFICIARY)");

            if (db.RowEffect == -1) //error
            {
                _MESSAGE = db.Message;
            }
            else
            {
                _MESSAGE = "Success";
            }

        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;

            Log.AddExceptionToLog("Error function [SaveBeneficiary(bl_micro_policy_beneficiary BENEFICIARY)] in class [da_micro_policy_beneficiary], detail: " + ex.Message + "==>" + ex.StackTrace);

        }

        return _SUCCESS;
    }
    public static bl_micro_policy_beneficiary GetBeneficiary(string policyId, string userName = "")
    {
        bl_micro_policy_beneficiary ben = new bl_micro_policy_beneficiary();
        try
        {
            //DB db = new DB();
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_BENEFICIARY_GET_BY_POLICY_ID", new string[,] {
            {"@POLICY_ID", policyId},
            }, "da_micro_policy_benificiary=>GetBeneficiary(string policyId, string userName)");

            if (db.RowEffect == -1) //error
            {
                _MESSAGE = db.Message;
            }
            else
            {
                _MESSAGE = "Success";
                foreach (DataRow r in tbl.Rows)
                {
                    ben = new bl_micro_policy_beneficiary()
                    {
                        POLICY_ID = r["policy_id"].ToString(),
                        FULL_NAME = r["full_name"].ToString(),
                        AGE = r["age"].ToString(),
                        RELATION = r["relation"].ToString(),
                        PERCENTAGE_OF_SHARE = Convert.ToDouble(r["percentage_of_share"].ToString()),
                        ADDRESS = r["address"].ToString(),
                        REMARKS = r["remarks"].ToString(),
                        BirthDate = Convert.ToDateTime(r["DOB"].ToString()),
                        Gender = Convert.ToInt32(r["GENDER"].ToString()),
                        IdType = Convert.ToInt32(r["ID_TYPE"].ToString()),
                        IdNo = r["ID_NO"].ToString()
                    };
                }
            }

        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;

            Log.AddExceptionToLog("da_micro_policy_beneficiary", "GetBeneficiary(string policyId, string userName=\"\")", ex);
        }

        return ben;
    }
    /// <summary>
    /// UPDATE BENEFICIARY BY POLICY ID
    /// </summary>
    /// <param name="BENEFICIARY"></param>
    /// <returns></returns>
    public static bool UpdateBeneficiary(bl_micro_policy_beneficiary BENEFICIARY)
    {
        bool result = false;
        try
        {
            //DB db = new DB();
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_BENEFICIARY_UPDATE", new string[,] {
            {"@POLICY_ID", BENEFICIARY.POLICY_ID},
            {"@FULL_NAME",BENEFICIARY.FULL_NAME},
            {"@AGE", BENEFICIARY.AGE},
            {"@RELATION", BENEFICIARY.RELATION},
            {"@PERCENTAGE_OF_SHARE", BENEFICIARY.PERCENTAGE_OF_SHARE+""},
            {"@ADDRESS", BENEFICIARY.ADDRESS},
            {"@UPDATED_BY", BENEFICIARY.UPDATED_BY},
            {"@UPDATED_ON", BENEFICIARY.UPDATED_ON+""},
            {"@REMARKS", BENEFICIARY.REMARKS},
            { "@DOB", BENEFICIARY.BirthDate+"" },
            { "@GENDER", BENEFICIARY.Gender+"" },
            { "@ID_TYPE",BENEFICIARY.IdType+"" },
            { "@ID_NO",BENEFICIARY.IdNo }
            }, "da_micro_policy_benificiary=>UpdateBeneficiary(bl_micro_policy_beneficiary BENEFICIARY)");

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
            Log.AddExceptionToLog("Error function [UpdateBeneficiary(bl_micro_policy_beneficiary BENEFICIARY)] in class [da_micro_policy_beneficiary], detail: " + ex.Message + "==>" + ex.StackTrace);

        }

        return result;
    }
    public static bool DeleteBeneficiary(string ID)
    {
        bool result = false;
        try
        {
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_BENEFICIARY_DELETE", new string[,] {
            {"@ID", ID}

            }, "da_micro_policy_beneficiary=>DeleteBeneficiary(string ID)");
        }
        catch (Exception ex)
        {
            _SUCCESS = false;
            _MESSAGE = ex.Message;
            result = false;
            Log.AddExceptionToLog("Error function [DeleteBeneficiary(string ID)] in class [da_micro_policy_beneficiary], detail: " + ex.Message + "==>" + ex.StackTrace);
        }
        return result;
    }

    public class beneficiary_primary
    {
        public static bool Delete(
          da_micro_policy_beneficiary.beneficiary_primary.DeleteOption deleteOption,
          string value)
        {
            bool flag = false;
            try
            {
                switch (deleteOption)
                {
                    case da_micro_policy_beneficiary.beneficiary_primary.DeleteOption.BY_ID:
                        flag = da_micro_policy_beneficiary.db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_BENEFICIARY_PRIMARY_DELETE_BY_ID", new string[,]
                        {
                            {"@ID", value}
                        }, "da_micro_policy_beneficiary.beneficiary_primary=>Delete(DeleteOption deleteOption, string value)");
                        break;
                    case da_micro_policy_beneficiary.beneficiary_primary.DeleteOption.BY_POLICY_ID:
                        flag = da_micro_policy_beneficiary.db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_BENEFICIARY_PRIMARY_DELETE", new string[1, 2]
                        {
                            {"@POLICY_ID",value}
                        }, "da_micro_policy_beneficiary.beneficiary_primary=>Delete(DeleteOption deleteOption, string value)");
                        break;
                }
            }
            catch (Exception ex)
            {
                da_micro_policy_beneficiary._SUCCESS = false;
                da_micro_policy_beneficiary._MESSAGE = ex.Message;
                flag = false;
                Log.AddExceptionToLog($"Error function [Delete(DeleteOption deleteOption, string value)] in class [da_micro_policy_beneficiary.beneficiary_primary], detail: {ex.Message}==>{ex.StackTrace}");
            }
            return flag;
        }

        public static bl_micro_policy_beneficiary.beneficiary_primary GetBeneficiaryPrimary(
          string policyId)
        {
            bl_micro_policy_beneficiary.beneficiary_primary beneficiaryPrimary = new bl_micro_policy_beneficiary.beneficiary_primary();
            try
            {
                DataTable data = da_micro_policy_beneficiary.db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_BENEFICIARY_PRIMARY_GET", new string[1, 2]
                {
          {
            "@POLICY_ID",
            policyId
          }
                }, "da_micro_policy_benificiary.beneficary_primary=>bl_micro_policy_beneficiary. beneficiary_primary GetBeneficiaryPrimary(string policyId)");
                if (da_micro_policy_beneficiary.db.RowEffect == -1)
                {
                    da_micro_policy_beneficiary._MESSAGE = da_micro_policy_beneficiary.db.Message;
                }
                else
                {
                    da_micro_policy_beneficiary._MESSAGE = "Success";
                    foreach (DataRow row in (InternalDataCollectionBase)data.Rows)
                        beneficiaryPrimary = new bl_micro_policy_beneficiary.beneficiary_primary()
                        {
                            Id = row["id"].ToString(),
                            FullName = row["full_name"].ToString(),
                            LoanNumber = row["loan_number"].ToString(),
                            PolicyId = row["policy_id"].ToString(),
                            Address = row["address"].ToString(),
                            CreatedBy = row["created_by"].ToString(),
                            CreatedOn = Convert.ToDateTime(row["created_on"].ToString()),
                            CreatedRemarks = row["created_remarks"].ToString(),
                            UpdatedBy = row["updated_by"].ToString(),
                            UpdatedOn = Convert.ToDateTime(row["updated_on"].ToString()),
                            UpdatedRemarks = row["updated_remarks"].ToString()
                        };
                }
            }
            catch (Exception ex)
            {
                da_micro_policy_beneficiary._SUCCESS = false;
                da_micro_policy_beneficiary._MESSAGE = ex.Message;
                Log.AddExceptionToLog("da_micro_policy_beneficiary.beneficary_primary", "bl_micro_policy_beneficiary. beneficiary_primary GetBeneficiaryPrimary(string policyId)", ex);
            }
            return beneficiaryPrimary;
        }

        public static bool Update(
          bl_micro_policy_beneficiary.beneficiary_primary beneficiary)
        {
            bool flag;
            try
            {
                bl_micro_policy_beneficiary.beneficiary_primary beneficiaryPrimary = beneficiary;
                flag = da_micro_policy_beneficiary.db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_BENEFICIARY_PRIMARY_UPDATE", new string[7, 2]
                {
          {
            "@POLICY_ID",
            beneficiaryPrimary.PolicyId
          },
          {
            "@FULL_NAME",
            beneficiaryPrimary.FullName
          },
          {
            "@LOAN_NUMBER",
            beneficiaryPrimary.LoanNumber
          },
          {
            "@ADDRESS",
            beneficiaryPrimary.Address
          },
          {
            "@UPDATED_BY",
            beneficiaryPrimary.UpdatedBy
          },
          {
            "@UPDATED_ON",
            beneficiaryPrimary.UpdatedOn.ToString() ?? ""
          },
          {
            "@UPDATED_REMARKS",
            beneficiaryPrimary.UpdatedRemarks
          }
                }, "da_micro_policy_benificiary.beneficiary_primary=>Update(bl_micro_policy_beneficiary.beneficiary_primary beneficiary))");
                da_micro_policy_beneficiary._MESSAGE = da_micro_policy_beneficiary.db.RowEffect != -1 ? "Success" : da_micro_policy_beneficiary.db.Message;
                da_micro_policy_beneficiary._SUCCESS = flag;
            }
            catch (Exception ex)
            {
                da_micro_policy_beneficiary._SUCCESS = false;
                da_micro_policy_beneficiary._MESSAGE = ex.Message;
                flag = false;
                Log.AddExceptionToLog($"Error function [Update(bl_micro_policy_beneficiary.beneficiary_primary beneficiary)] in class [da_micro_policy_beneficiary.beneficiary_primary], detail: {ex.Message}==>{ex.StackTrace}");
            }
            return flag;
        }

        public static bool Save(
          bl_micro_policy_beneficiary.beneficiary_primary beneficiary)
        {
            bool flag;
            try
            {
                bl_micro_policy_beneficiary.beneficiary_primary beneficiaryPrimary = beneficiary;
                flag = da_micro_policy_beneficiary.db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_POLICY_BENEFICIARY_PRIMARY_INSERT", new string[8, 2]
                {
          {
            "@ID",
            beneficiaryPrimary.Id
          },
          {
            "@POLICY_ID",
            beneficiaryPrimary.PolicyId
          },
          {
            "@FULL_NAME",
            beneficiaryPrimary.FullName
          },
          {
            "@LOAN_NUMBER",
            beneficiaryPrimary.LoanNumber
          },
          {
            "@ADDRESS",
            beneficiaryPrimary.Address
          },
          {
            "@CREATED_BY",
            beneficiaryPrimary.CreatedBy
          },
          {
            "@CREATED_ON",
            beneficiaryPrimary.CreatedOn.ToString() ?? ""
          },
          {
            "@CREATED_REMARKS",
            beneficiaryPrimary.CreatedRemarks
          }
                }, "da_micro_policy_benificiary.beneficiary_primary=>Save(bl_micro_policy_beneficiary.beneficiary_primary beneficiary))");
                da_micro_policy_beneficiary._MESSAGE = da_micro_policy_beneficiary.db.RowEffect != -1 ? "Success" : da_micro_policy_beneficiary.db.Message;
                da_micro_policy_beneficiary._SUCCESS = flag;
            }
            catch (Exception ex)
            {
                da_micro_policy_beneficiary._SUCCESS = false;
                da_micro_policy_beneficiary._MESSAGE = ex.Message;
                flag = false;
                Log.AddExceptionToLog($"Error function [Save(bl_micro_policy_beneficiary.beneficiary_primary beneficiary)] in class [da_micro_policy_beneficiary.beneficiary_primary], detail: {ex.Message}==>{ex.StackTrace}");
            }
            return flag;
        }

        public enum DeleteOption
        {
            BY_ID,
            BY_POLICY_ID,
        }
    }

}