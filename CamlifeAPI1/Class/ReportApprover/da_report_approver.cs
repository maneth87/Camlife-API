using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
//using System.Windows.Forms;

/// <summary>
/// Summary description for da_report_approver
/// </summary>
public class da_report_approver
{
	public da_report_approver()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static List<bl_report_approver> GetApproverList() {
        bl_report_approver approver; 
        List<bl_report_approver> approverList = new List<bl_report_approver>();
        try
        {
            SqlConnection con = new SqlConnection(AppConfiguration.GetConnectionString());
            SqlCommand cmd = new SqlCommand("SP_GET_REPORT_APPROVER",con);
            SqlDataReader dr ;
            cmd.CommandType=CommandType.StoredProcedure;
            con.Open();
            dr=cmd.ExecuteReader();
            while(dr.Read())
            {
                approver = new bl_report_approver();
                approver.ID=Convert.ToInt32(dr["id"].ToString());
                approver.PositionEn=dr["Position_En"].ToString();
                approver.PositionKh=dr["position_Kh"].ToString();
                approver.NameEn=dr["name_En"].ToString();
                approver.NameKh=dr["name_kh"].ToString();
                approver.Status=dr["status"].ToString();
                approver.Remarks=dr["remarks"].ToString();

                approverList.Add(approver);
            }
           
            dr.Close();
            cmd.Dispose();
            con.Close();

        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [GetApproverList] in class [da_report_approver] ,Detail: " + ex.Message);
        }

        return approverList;

    }

    public static bool InsertApproverPolicy(bl_report_approver_policy approver)
    {
        bool status = false;
        try
        {
            string[,] para = new string[,]{{"@Policy_ID",approver.Policy_ID}, {"@Approver_ID", approver.Approver_ID+""},{"@Created_On", approver.Created_On+""}, {"@Created_By", approver.Created_By}};
            status = Helper.ExecuteProcedure(AppConfiguration.GetConnectionString(), "SP_INSERT_REPORT_APPROVER_POLICY", para, "da_report_approve => InsertApproverPolicy");
        }
        catch (Exception ex)
        {
            status = false;
            Log.AddExceptionToLog("Error function [InsertApproverPolicy] in class [da_report_approver], Detail: " + ex.Message);
        }

        return status;
    }

    public static bl_report_approver GetAproverInfo(string policy_id)
    {
        bl_report_approver approver = new bl_report_approver();
        try
        {
            DataTable tbl = DataSetGenerator.Get_Data_Soure("SP_GET_REPORT_APPROVER_POLICY_DETAIL_BY_POLICY_ID", new string[,] { { "@Policy_ID", policy_id } });
            foreach (DataRow row in tbl.Rows)
            {
                approver.ID =Convert.ToInt32( row["approver_id"].ToString());
                approver.PositionEn = row["Position_En"].ToString();
                approver.PositionKh = row["position_Kh"].ToString();
                approver.NameEn = row["name_En"].ToString();
                approver.NameKh = row["name_kh"].ToString();
                approver.Status = row["status"].ToString();
                approver.Remarks = row["remarks"].ToString();
                approver.Email = row["email"].ToString();
                approver.Signature = row["signature"].ToString();
                approver.Module = row["module"].ToString();
            }
         
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [AproverInfo] in class [da_report_approver], Detail: " + ex.Message);
        }
        

        return approver;
    }
    /// <summary>
    /// Get approver information for simple life product (cellcard)
    /// </summary>
    /// <param name="effective_date"></param>
    /// <returns></returns>
    public static bl_report_approver GetAproverSL(DateTime effective_date)
    {
        bl_report_approver approver = new bl_report_approver();
        try
        {
            DataTable tbl = DataSetGenerator.Get_Data_Soure("SP_GET_REPORT_APPROVER_SL_BY_EFFECTIVE_DATE", new string[,] { { "@EFFECTIVE_DATE", effective_date+"" } });
            foreach (DataRow row in tbl.Rows)
            {
                approver.ID = Convert.ToInt32(row["approver_id"].ToString());
                approver.PositionEn = row["Position_En"].ToString();
                approver.PositionKh = row["position_Kh"].ToString();
                approver.NameEn = row["name_En"].ToString();
                approver.NameKh = row["name_kh"].ToString();
                approver.Status = row["status"].ToString();
                approver.Remarks = row["remarks"].ToString();
                approver.Email = row["email"].ToString();
                approver.Signature = row["signature"].ToString();
                approver.Module = row["module"].ToString();
            }

        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog("Error function [GetAproverSL(DateTime effective_date)] in class [da_report_approver], Detail: " + ex.Message);
        }


        return approver;
    }
    public class bl_report_approver
    {
        public int ID { get; set; }
        public string PositionEn { get; set; }
        public string PositionKh { get; set; }
        public string NameEn { get; set; }
        public string NameKh { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string Email { get; set; }
        public string Signature { get; set; }
        public string Module { get; set; }
    }

    public class bl_report_approver_policy
    {
        public string Policy_ID { get; set; }
        public int Approver_ID { get; set; }
        public DateTime Created_On { get; set; }
        public string Created_By { get; set; }
    }
    
}