using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for da_micro_application_questionaire
/// </summary>
public class da_micro_application_questionaire
{
    private static bool _SUCCESS = false;
    private static string _MESSAGE = "";

    public static bool SUCCESS { get { return _SUCCESS; } }
    public static string MESSAGE { get { return _MESSAGE; } }

    private static DB db = new DB();
	public da_micro_application_questionaire()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static bool SaveQuestionaire(bl_micro_application_questionaire QUESTIONIARE)
    {
        bool result = false;
        try
        {
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_QUESTIONAIRE_INSERT", new string[,] {
            {"@ID", QUESTIONIARE.ID},
            {"@QUESTION_ID",QUESTIONIARE.QUESTION_ID},
            {"@APPLICATION_NUMBER",QUESTIONIARE.APPLICATION_NUMBER},
            {"@ANSWER",QUESTIONIARE.ANSWER+""},
            {"@ANSWER_REMARKS",QUESTIONIARE.ANSWER_REMARKS},
            {"@CREATED_BY", QUESTIONIARE.CREATED_BY},
            {"@CREATED_ON", QUESTIONIARE.CREATED_ON+""}
            }, "da_micro_application_questionaire=> SaveQuestionaire(bl_micro_application_questionaire QUESTIONIARE)");

            if (db.RowEffect == -1)
            {
                _MESSAGE = db.Message;
            }
            else
            {
                _MESSAGE = "Success";
            }
            _SUCCESS=result;
        }

        catch (Exception ex)
        {
            _SUCCESS = false;
            result = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("Error function [SaveQuestionaire(bl_micro_application_questionaire QUESTIONIARE)] in class [da_micro_application_questionaire], detail: " + ex.Message + "=>" + ex.StackTrace);
        
        }

        return result;
    }
    public static bool UpdateQuestionaire(bl_micro_application_questionaire QUESTIONIARE)
    {
        bool result = false;
        try
        {
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_QUESTIONAIRE_UPDATE", new string[,] {
            {"@ID", QUESTIONIARE.ID},
            {"@QUESTION_ID",QUESTIONIARE.QUESTION_ID},
            {"@APPLICATION_NUMBER",QUESTIONIARE.APPLICATION_NUMBER},
            {"@ANSWER",QUESTIONIARE.ANSWER+""},
            {"@ANSWER_REMARKS",QUESTIONIARE.ANSWER_REMARKS},
            {"@UPDATED_BY", QUESTIONIARE.UPDATED_BY},
            {"@UPDATED_ON", QUESTIONIARE.UPDATED_ON+""}
            }, "da_micro_application_questionaire=> UpdateQuestionaire(bl_micro_application_questionaire QUESTIONIARE)");

            if (db.RowEffect == -1)
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
            result = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("Error function [UpdateQuestionaire(bl_micro_application_questionaire QUESTIONIARE)] in class [da_micro_application_questionaire], detail: " + ex.Message + "=>" + ex.StackTrace);

        }

        return result;
    }
    public static bool DeleteQuestionaire(string APPLICATION_NUMBER)
    {
        bool result = false;
        try
        {
            result = db.Execute(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_QUESTIONAIRE_DELETE", new string[,] {
            
            {"@APPLICATION_NUMBER",APPLICATION_NUMBER}
            
            }, "da_micro_application_questionaire=> DeleteQuestionaire(bl_micro_application_questionaire QUESTIONIARE)");

            if (db.RowEffect == -1)
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
            result = false;
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("Error function [DeleteQuestionaire(bl_micro_application_questionaire QUESTIONIARE)] in class [da_micro_application_questionaire], detail: " + ex.Message + "=>" + ex.StackTrace);

        }

        return result;
    }

    public static bl_micro_application_questionaire GetQuestionaire(string APPLICATION_NUMBER)
    {
        bl_micro_application_questionaire ques = new bl_micro_application_questionaire();
        try
        {
            DataTable tbl = db.GetData(AppConfiguration.GetConnectionString(), "SP_CT_MICRO_APPLICATION_QUESTIONAIRE_GET_BY_APPLICATION_NUMBER", new string[,] {
            
            {"@APPLICATION_NUMBER",APPLICATION_NUMBER}
            
            }, "da_micro_application_questionaire=> GetQuestionaire(string APPLICATION_NUMBER)");

            if (db.RowEffect == -1)
            {
                _MESSAGE = db.Message;
                _SUCCESS = false;
            }
            else
            {
                if (tbl.Rows.Count > 0)
                {
                    var r = tbl.Rows[0];
                    ques.ID = r["id"].ToString();
                    ques.QUESTION_ID = r["question_id"].ToString();
                    ques.APPLICATION_NUMBER = r["application_number"].ToString();
                    ques.ANSWER = Convert.ToInt32(r["answer"].ToString());
                    ques.ANSWER_REMARKS = r["answer_remarks"].ToString();
                    ques.CREATED_BY = r["created_by"].ToString();
                    ques.CREATED_ON = Convert.ToDateTime(r["created_on"].ToString());
                    ques.UPDATED_BY = r["updated_by"].ToString();
                    ques.UPDATED_ON = Convert.ToDateTime(r["updated_on"].ToString());
                    _SUCCESS = true;
                    _MESSAGE = "Success";

                }
                else
                {
                    _SUCCESS = true;
                    _MESSAGE = "No Record Found.";
                }
            }
            
        }

        catch (Exception ex)
        {
            _SUCCESS = false;
            ques = new bl_micro_application_questionaire();
            _MESSAGE = ex.Message;
            Log.AddExceptionToLog("Error function [DeleteQuestionaire(bl_micro_application_questionaire QUESTIONIARE)] in class [da_micro_application_questionaire], detail: " + ex.Message + "=>" + ex.StackTrace);

        }

        return ques;
    }
}