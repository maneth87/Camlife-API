using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CamlifeAPI1.Class.Master
{
    public class bl_master
    {
        public class bl_occupation
        {
            public string Id { get; set; }
            public string OccupationEn { get; set; }
            public string OccupationKh { get; set; }
        }

        public class bl_relation
        {
            public string Id { get; set; }
            public string RelationEn { get; set; }
            public string RelationKh { get; set; }
            public int GenderCode { get; set; }
        }

        public class bl_master_relation : bl_relation
        {
            public int OrderNo { get; set; }
            public string MasterCode { get; set; }
        }
        public class bl_countries
        {
            public string CountryId { get; set; }
            public string CountryName { get; set; }
            public string Nationality { get; set; }
            public int Status { get; set; }
        }

        public class bl_master_list
        { 
            public int Id { get; set; }
            public int OrderNo { get; set; }
            public string MasterListCode { get; set; }
            public string Code { get;set; }
            public string DescEn { get; set; }
            public string DescKh { get; set; }
            public int Status { get; set; }
        }
    }
}