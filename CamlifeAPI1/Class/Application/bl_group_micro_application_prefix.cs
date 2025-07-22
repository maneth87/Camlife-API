using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_group_micro_application_prefix
/// </summary>
public class bl_group_micro_application_prefix
{
	public bl_group_micro_application_prefix()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string Id { get; set; }
    public string GroupCode { get; set; }
    public string Prefix1 { get; set; }
    public string Prefix2 { get; set; }
    public string Digits { get; set; }
    public int Status { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedOn { get; set; }
    public string Remarks { get; set; }
}