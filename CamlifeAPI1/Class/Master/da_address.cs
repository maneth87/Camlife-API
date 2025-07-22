using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for da_address
/// </summary>
public class da_address
{
	public da_address()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public class province
    {
        public static List<bl_address.province> GetProvince()
        {
            List<bl_address.province> ProList = new List<bl_address.province>();
            try
            {
                DataTable tbl = new DB().GetData(AppConfiguration.GetConnectionString(), "SP_TB_PROVINCE_GET", new string[,] { }, "da_address=>province=>GetProvince()");
                foreach (DataRow r in tbl.Rows)
                {
                    ProList.Add(new bl_address.province() { Code= r["code"].ToString(), Khmer=r["khmer"].ToString(), English=r["english"].ToString() });
                }
            }
            catch (Exception ex)
            {
                ProList = new List<bl_address.province>();
                Log.AddExceptionToLog("Error function [GetProvince()] in call [da_address=>province], detail:" + ex.Message + "=>" + ex.StackTrace);
            }
            return ProList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="proCode"></param>
        /// <param name="inKh">show as khmer</param>
        /// <returns></returns>
        public static string GetProvinceName(string proCode, bool inKh = false)
        {
            List<bl_address.province> ProList = new List<bl_address.province>();
            string proName = "";
            try
            {
                ProList = GetProvince().Where(_ => _.Code.Contains(proCode)).ToList();
                var pro = ProList[0];
                proName = inKh == true ? pro.Khmer : pro.English;

            }
            catch (Exception ex)
            {
                proName = "";
                Log.AddExceptionToLog("Error function [GetProvinceName(string proCode, bool inKh = false)] in call [da_address=>province], detail:" + ex.Message + "=>" + ex.StackTrace);
            }
            return proName;
        }
    }

    

    public class district
    {
        /// <summary>
        /// Get District by province code
        /// </summary>
        /// <param name="provinceCode"></param>
        /// <returns></returns>
        public static List<bl_address.district> GetDistrict(string provinceCode)
        {
            List<bl_address.district> disList = new List<bl_address.district>();
            try
            {
                DataTable tbl = new DB().GetData(AppConfiguration.GetConnectionString(), "SP_TB_DISTRICT_GET", new string[,] {{"@PROVINCE_CODE", provinceCode }}, "da_address=>district=>GetDistrict(string provinceCode)");
                foreach (DataRow r in tbl.Rows)
                {
                    disList.Add(new bl_address.district() { Code = r["code"].ToString(), Khmer = r["khmer"].ToString(), English = r["english"].ToString() });
                }

            }
            catch (Exception ex)
            {
                disList = new List<bl_address.district>();
                Log.AddExceptionToLog("Error function [GetDistrict(string provinceCode)] in call [da_address=>district], detail:" + ex.Message + "=>" + ex.StackTrace);
            }
            return disList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="procode"></param>
        /// <param name="disCode"></param>
        /// <param name="inKh">show in khmer</param>
        /// <returns></returns>
        public static string GetDistrictName(string procode, string disCode, bool inKh = false)
        {
            List<bl_address.district> disList = new List<bl_address.district>();
            string disName = "";
            try
            {
                disList = GetDistrict(procode).Where(_ => _.Code.Contains(disCode)).ToList();
                var dis = disList[0];
                disName = inKh == true ? dis.Khmer : dis.English;

            }
            catch (Exception ex)
            {
                disName = "";
                Log.AddExceptionToLog("Error function [GetDistrictName(string procode, string disCode, bool inKh = false)] in call [da_address=>province], detail:" + ex.Message + "=>" + ex.StackTrace);
            }
            return disName;
        }
    }
    public class commune
    {
        /// <summary>
        /// Get commune by district code
        /// </summary>
        /// <param name="districtCode"></param>
        /// <returns></returns>
        public static List<bl_address.commune> GetCommune(string districtCode)
        {
            List<bl_address.commune> comList = new List<bl_address.commune>();
            try
            {
                DataTable tbl = new DB().GetData(AppConfiguration.GetConnectionString(), "SP_TB_COMMUNE_GET", new string[,] { { "@DISTRICT_CODE", districtCode } }, "da_address=>commune=>GetCommune(string districtCode)");
                foreach (DataRow r in tbl.Rows)
                {
                    comList.Add(new bl_address.commune() { Code = r["code"].ToString(), Khmer = r["khmer"].ToString(), English = r["english"].ToString() });
                }

            }
            catch (Exception ex)
            {
                comList = new List<bl_address.commune>();
                Log.AddExceptionToLog("Error function [GetCommune(string districtCode)] in call [da_address=>commune], detail:" + ex.Message + "=>" + ex.StackTrace);
            }
            return comList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disCode">District Code</param>
        /// <param name="comCode">Commune Code</param>
        /// <param name="inKh">Show as khmer set true</param>
        /// <returns></returns>
        public static string GetCommuneName(string disCode, string comCode, bool inKh = false)
        {
            List<bl_address.commune>comList = new List<bl_address.commune>();
            string comName = "";
            try
            {
                comList = GetCommune(disCode).Where(_ => _.Code.Contains(comCode)).ToList();
                var com = comList[0];
                comName = inKh == true ? com.Khmer : com.English;

            }
            catch (Exception ex)
            {
                comName = "";
                Log.AddExceptionToLog("Error function [GetCommuneName(string disCode, string comCode, bool inKh = false)] in call [da_address=>province], detail:" + ex.Message + "=>" + ex.StackTrace);
            }
            return comName;
        }
    }
         public class village
    {
      /// <summary>
      /// Get village by commune code
      /// </summary>
      /// <param name="communeCode"></param>
      /// <returns></returns>
        public static List<bl_address.village> GetVillage(string communeCode)
        {
            List<bl_address.village> viList = new List<bl_address.village>();
            try
            {
                DataTable tbl = new DB().GetData(AppConfiguration.GetConnectionString(), "SP_TB_VILLAGE_GET", new string[,] { { "@COMMUNE_CODE", communeCode } }, "da_address=>village=>GetVillage(string communeCode)");
                foreach (DataRow r in tbl.Rows)
                {
                    viList.Add(new bl_address.village() { Code = r["code"].ToString(), Khmer = r["khmer"].ToString(), English = r["english"].ToString() });
                }

            }
            catch (Exception ex)
            {
                viList = new List<bl_address.village>();
                Log.AddExceptionToLog("Error function [GetVillage(string communeCode)] in call [da_address=>village], detail:" + ex.Message + "=>" + ex.StackTrace);
            }
            return viList;
        }
             /// <summary>
             /// 
             /// </summary>
             /// <param name="comCode">Commune Code</param>
             /// <param name="vilCode">Village Code</param>
             /// <param name="inKh">Show as khmer set true</param>
             /// <returns></returns>
        public static string GetVillageName(string comCode, string vilCode, bool inKh = false)
        {
            List<bl_address.village> vilList = new List<bl_address.village>();
            string vilName = "";
            try
            {
                vilList = GetVillage(comCode).Where(_ => _.Code.Contains(vilCode)).ToList();
                var vil = vilList[0];
                vilName = inKh == true ? vil.Khmer : vil.English;

            }
            catch (Exception ex)
            {
                vilName = "";
                Log.AddExceptionToLog("Error function [GetVillageName(string comCode, string vilCode, bool inKh = false)] in call [da_address=>province], detail:" + ex.Message + "=>" + ex.StackTrace);
            }
            return vilName;
        }
    }
}