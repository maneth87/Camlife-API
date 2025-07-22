using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_sale_agent_micro
/// </summary>
public class bl_sale_agent_micro
{
	public bl_sale_agent_micro()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string SaleAgentId { get; set; }
    public int SaleAgentType { get; set; }
    public string FullNameEn { get; set; }
    public string FullNameKh { get; set; }
    public string Position { get; set; }
    public string Email { get; set; }
    public int Status { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedNote { get; set; }

    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}