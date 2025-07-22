using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace CamlifeAPI1.Class.Master
{
    public static class da_master
    {
        public static class da_occupation
        {
            public static List<bl_master.bl_occupation> GetOccupationList()
            {
                List<bl_master.bl_occupation> oList = new List<bl_master.bl_occupation>();
                try
                {
                    DataTable tbl = new DB().GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_OCCUPATION_GET", new string[,] { },
                        "da_master=>da_occupation=>GetOccupationList()");
                    foreach (DataRow dr in tbl.Rows)
                    {
                        oList.Add(new bl_master.bl_occupation() { Id = dr["id"].ToString(), OccupationEn = dr["occupation_en"].ToString(), OccupationKh = dr["occupation_kh"].ToString() });
                    }
                }
                catch (Exception ex)
                {
                    oList = new List<bl_master.bl_occupation>();
                    Log.AddExceptionToLog("da_master=>da_occupation", "[GetOccupationList()", ex);
                }

                return oList;
            }
        }
        public static class da_beneficiary_relation
        {
            public static List<bl_master.bl_relation> GetBeneficiaryRelationList()
            {
                List<bl_master.bl_relation> rList = new List<bl_master.bl_relation>();

                try
                {
                    DataTable tbl = new DB().GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_BENEFICIARY_RELATION_GET", new string[,] { },
                        "da_master=>da_beneficiary_relation=>GetBeneficiaryRelationList()");
                    foreach (DataRow dr in tbl.Rows)
                    {
                        rList.Add(new bl_master.bl_relation() { Id = dr["id"].ToString(), RelationEn = dr["relation_en"].ToString(), RelationKh = dr["relation_kh"].ToString(), GenderCode = Convert.ToInt32(dr["gender_code"].ToString()) });
                    }
                }
                catch (Exception ex)
                {
                    rList = new List<bl_master.bl_relation>();
                    Log.AddExceptionToLog("da_master=>da_beneficiary_relation", "GetBeneficiaryRelationList()", ex);
                }

                return rList;
            }


        }
        public static class da_countries
        {
            public static List<bl_master.bl_countries> GetCountryList()
            {
                List<bl_master.bl_countries> cList = new List<bl_master.bl_countries>();
                try
                {
                    DataTable tbl = new DB().GetData(AppConfiguration.GetConnectionString(), "SP_CT_COUNTRY_GET_COUNTRY", new string[,] { },
                        "da_master=>da_countries=>GetCountryList()");
                    foreach (DataRow dr in tbl.Rows)
                    {
                        cList.Add(new bl_master.bl_countries() { CountryId = dr["Country_id"].ToString(), CountryName = dr["Country_name"].ToString(), Nationality = dr["nationality"].ToString(), Status = Convert.ToInt32(dr["status"].ToString()) });
                    }
                }
                catch (Exception ex)
                {
                    cList = new List<bl_master.bl_countries>();
                    Log.AddExceptionToLog("da_master=>da_countries", "GetCountryList()", ex);
                }

                return cList;
            }
        }

        public static class da_master_relation
        {
            public static List<bl_master.bl_master_relation> GetMasterRelationList(string masterCode)
            {
                List<bl_master.bl_master_relation> mList = new List<bl_master.bl_master_relation>();
                try
                {
                    DataTable tbl = new DB().GetData(AppConfiguration.GetConnectionString(), "SP_CT_MASTER_RELATION_GET", new string[,] {
                    {"@master_code", masterCode}
                    }, "da_master=>da_master_relation=>GetMasterRelationList(string masterCode)");
                    foreach (DataRow dr in tbl.Rows)
                    {
                        mList.Add(new bl_master.bl_master_relation()
                        {
                            Id = dr["id"].ToString(),
                            MasterCode = dr["master_code"].ToString(),
                            RelationEn = dr["relation_en"].ToString(),
                            RelationKh = dr["relation_kh"].ToString()
                        });
                    }
                }
                catch (Exception ex)
                {
                    mList = null;
                    Log.AddExceptionToLog("da_master=>da_master_relation", "GetMasterRelationList(string masterCode)", ex);
                }
                return mList;
            }
        }
        public static class da_master_list
        { 
            public static List<bl_master.bl_master_list> GetMasterList(string masterCode)
            {
                List<bl_master.bl_master_list> mList = new List<bl_master.bl_master_list>();
                try
                {
                    DataTable tbl = new DB().GetData(AppConfiguration.GetConnectionString(), "SP_CT_MASTER_LIST_GET_ACTIVE_BY_MASTER_CODE", new string[,] {
                    {"@master_list_code", masterCode}
                    }, "da_master=>da_master_relation=>GetMasterList(string masterCode)");
                    foreach (DataRow dr in tbl.Rows)
                    {
                        mList.Add(new bl_master.bl_master_list()
                        {
                            Id = Convert.ToInt32( dr["id"].ToString()),
                            OrderNo = Convert.ToInt32(dr["order_no"].ToString()),
                            MasterListCode = dr["master_list_code"].ToString(),
                            Code = dr["code"].ToString(),
                            DescEn = dr["desc_en"].ToString(),
                            DescKh = dr["desc_kh"].ToString(),
                            Status = Convert.ToInt32(dr["status"].ToString())
                        });
                    }
                }
                catch (Exception ex)
                {
                    mList = null;
                    Log.AddExceptionToLog("da_master=>da_master_list", "GetMasterList(string masterCode)", ex);
                }
                return mList;
            }
        }
    }
}