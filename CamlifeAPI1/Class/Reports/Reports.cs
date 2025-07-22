using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CamlifeAPI1.Class.Reports
{
    public class Reports
    {
        public enum DateOption { APP_DATE, EFF_DATE }
        public class SaleMicro
        { 
            public DateTime ApplicationDate { get; set; }   
            public DateTime EffectiveDate { get; set; }
            public string ApplicationNumber { get; set; }
            public string PolicyNumber { get; set; }
            public string Package { get; set; }
            public double Premium { get; set; }
            public string BranchCode { get; set; }
            public string BranchName { get; set; }
            public string CreatedBy { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateOption"> by application or effective date</param>
        /// <param name="fDate">from date</param>
        /// <param name="tDate">to date</param>
        /// <param name="channelLocationId"></param>
        /// <returns></returns>
        public static List<SaleMicro> GetSaleMicroReport(DateOption dateOption, DateTime fDate, DateTime tDate, List< string> channelLocationId)
        { 
            List<SaleMicro> rpt =new List<SaleMicro>();
            try {
                string ch = string.Join(",", channelLocationId);
                DB db = new DB();

                DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_REPORT_SALES_MICRO_BY_CHANNEL_LOCATION", new string[,] {
                { "@D_OPTION", dateOption.ToString()},{"@f_date", fDate+"" },{ "@t_date", tDate+""},{"@channel_location_id",ch }
                }, "Reports.GetSaleMicroReport(DateOption dateOption, DateTime fDate, DateTime tDate, string channelLocationId)");
                if (db.RowEffect == -1 )/*get data error*/
                {
                    rpt = new List<SaleMicro>(); 
                }
                else
                {
                    if (db.RowEffect > 0)
                    {
                        foreach (DataRow r in tbl.Rows)
                        {
                            rpt.Add(new SaleMicro
                            {
                                ApplicationDate = Convert.ToDateTime(r["application_date"].ToString()),
                                EffectiveDate = Convert.ToDateTime(r["effective_date"].ToString()),
                                ApplicationNumber = r["application_number"].ToString(),
                                PolicyNumber = r["policy_number"].ToString(),
                                Package = r["package"].ToString(),
                                Premium = Convert.ToDouble(r["premium"].ToString()),
                                BranchCode = r["branch_code"].ToString(),
                                BranchName = r["branch_name"].ToString(),
                                CreatedBy = r["created_by"].ToString()
                            });
                        }

                    }
                    else
                    {
                        rpt = new List<SaleMicro>(); 
                    }
                }
            }
            catch (Exception ex){
                rpt = new List<SaleMicro>(); 
                Log.AddExceptionToLog("Reports", " GetSaleMicroReport(DateOption dateOption, DateTime fDate, DateTime tDate, string channelLocationId)", ex);
            }

            return rpt;
        }

       public static List<SaleMicro> GetAppPendingIssuePolicy( DateTime fDate, DateTime tDate, List< string> channelLocationId)
        { 
            List<SaleMicro> rpt =new List<SaleMicro>();
            try {
                string ch = string.Join(",", channelLocationId);
                DB db = new DB();

                DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_REPORT_APPLICATION_PENDING_ISSUE_POLICY", new string[,] {
               {"@f_date", fDate+"" },{ "@t_date", tDate+""},{"@channel_location_id",ch }
                }, "Reports.GetAppPendingIssuePolicy(DateTime fDate, DateTime tDate, string channelLocationId)");
                if (db.RowEffect == -1 )/*get data error*/
                {
                    rpt = new List<SaleMicro>(); 
                }
                else
                {
                    if (db.RowEffect > 0)
                    {
                        foreach (DataRow r in tbl.Rows)
                        {
                            rpt.Add(new SaleMicro
                            {
                                ApplicationDate = Convert.ToDateTime(r["application_date"].ToString()),
                                ApplicationNumber = r["application_number"].ToString(),
                                Package = r["package"].ToString(),
                                Premium = Convert.ToDouble(r["premium"].ToString()),
                                BranchCode = r["branch_code"].ToString(),
                                BranchName = r["branch_name"].ToString(),
                                CreatedBy = r["created_by"].ToString()
                            });
                        }

                    }
                    else
                    {
                        rpt = new List<SaleMicro>(); 
                    }
                }
            }
            catch (Exception ex){
                rpt = new List<SaleMicro>(); 
                Log.AddExceptionToLog("Reports", "GetAppPendingIssuePolicy(DateTime fDate, DateTime tDate, string channelLocationId)", ex);
            }

            return rpt;
        }

    }
}