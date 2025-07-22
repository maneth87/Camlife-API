using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_micro_application_questionair
/// </summary>
public class bl_micro_application_questionaire
{
	public bl_micro_application_questionaire()
	{
		//
		// TODO: Add constructor logic here
		//
        _ID = GetID();
	}
    private string GetID()
    {
        string id = "";
        id = Helper.GetNewGuid(new string[,] { { "TABLE", "CT_MICRO_APPLICATION_QUESTIONAIRE" }, { "FIELD", "ID" } });
        return id;
    }
    private string _ID = "";
    public string ID
    {
        get { return _ID; }
        set { _ID = value; }
    }
    public string QUESTION_ID { get; set; }
    public string APPLICATION_NUMBER { get; set; }
    public Int32 ANSWER { get; set; }
    public string ANSWER_REMARKS { get; set; }
    public string CREATED_BY { get; set; }
    public DateTime CREATED_ON { get; set; }
    public string UPDATED_BY { get; set; }
    public DateTime UPDATED_ON { get; set; }
    public string REMARKS { get; set; }

}