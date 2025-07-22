using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
/// <summary>
/// Summary description for Log
/// </summary>
public class Log
{
   
	public Log()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void SaveEventLog(string MessageIN)
    {
        DateTime dt = DateTime.Now;

        string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log\\event_log_" + dt.ToString("yyyy-MM-dd") + ".log";
        if (!File.Exists(filePath))
        {
            FileStream fs = File.Create(filePath);
            fs.Close();
        }
        try
        {
            StreamWriter sw = File.AppendText(filePath);
            if (!string.IsNullOrEmpty(MessageIN))
            {
                sw.WriteLine(dt.ToString("HH:mm:ss") + " " + MessageIN + System.Environment.NewLine);
            }
            else
            {
                sw.WriteLine(dt.ToString("HH:mm:ss") + " " + MessageIN + System.Environment.NewLine);
            }

            sw.Flush();
            sw.Close();

        }
        catch (Exception ex)
        {
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="log_name">Log file name</param>
    /// <param name="MessageIN">Text to save</param>
    public static void CreateLog(string log_name,string MessageIN)
    {
        DateTime dt = DateTime.Now;

        string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log\\" + log_name + "_" + dt.ToString("yyyy-MM-dd") + ".log";
        if (!File.Exists(filePath))
        {
            FileStream fs = File.Create(filePath);
            fs.Close();
        }
        try
        {
            StreamWriter sw = File.AppendText(filePath);
            if (!string.IsNullOrEmpty(MessageIN))
            {
                sw.WriteLine(dt.ToString("HH:mm:ss") + " " + MessageIN);
            }
            else
            {
                sw.WriteLine(dt.ToString("HH:mm:ss") + " " + MessageIN);
            }

            sw.Flush();
            sw.Close();

        }
        catch (Exception ex)
        {
        }
    }
    /// <summary>
    /// Generate error log 
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static string GenerateLog(Exception ex)
    {
        return ex.StackTrace + "=>" + ex.InnerException + "=>" + ex.Message;
    }
    public static void AddExceptionToLog(string MessageIN)
    {

        DateTime dt = DateTime.Now;


        string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log\\log_" + dt.ToString("yyyy-MM-dd") + ".log";
        if (!File.Exists(filePath))
        {
            FileStream fs = File.Create(filePath);
            fs.Close();
        }
        try
        {
            StreamWriter sw = File.AppendText(filePath);
            if (!string.IsNullOrEmpty(MessageIN))
            {
                sw.WriteLine(dt.ToString("HH:mm:ss") + " " + MessageIN + System.Environment.NewLine);
            }
            else
            {
                sw.WriteLine(dt.ToString("HH:mm:ss") + " " + MessageIN + System.Environment.NewLine);
            }

            sw.Flush();
            sw.Close();

        }
        catch (Exception ex)
        {
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="MessageIN">Error message</param>
    /// <param name="user_name">Current user</param>
    public static void AddExceptionToLog(string MessageIN, string user_name)
    {
        DateTime dt = DateTime.Now;

        string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log\\log_" + dt.ToString("yyyy-MM-dd") + ".log";
        if (!File.Exists(filePath))
        {
            FileStream fs = File.Create(filePath);
            fs.Close();
        }
        try
        {
            StreamWriter sw = File.AppendText(filePath);
            if (!string.IsNullOrEmpty(MessageIN))
            {
                sw.WriteLine(dt.ToString("HH:mm:ss") + "   [" + user_name + "]   " + MessageIN + System.Environment.NewLine);
            }
            else
            {
                sw.WriteLine(dt.ToString("HH:mm:ss") + "   [" + user_name + "]   " + MessageIN + System.Environment.NewLine);
            }

            sw.Flush();
            sw.Close();

        }
        catch (Exception ex)
        {
        }
    }

    public static void AddExceptionToLog(string className, string functionName, Exception error, string userName ="")
    {
        DateTime dt = DateTime.Now;

        string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log\\log_" + dt.ToString("yyyy-MM-dd") + ".log";
        if (!File.Exists(filePath))
        {
            FileStream fs = File.Create(filePath);
            fs.Close();
        }
        try
        {
            StreamWriter sw = File.AppendText(filePath);
            string err = string.Format("Error Time: {0}" + System.Environment.NewLine +
                "UserName: {1}" + System.Environment.NewLine +
                "Class Name: {2}" + System.Environment.NewLine +
                "Functioin Name: {3}" + System.Environment.NewLine +
                "Error Line: {4}" + System.Environment.NewLine +
                "Error Message: {5}" + System.Environment.NewLine + "==========================================================",
                dt.ToString("HH:mm:ss"), userName, className, functionName, GetLineNumber(error), error.Message);
            sw.WriteLine(err);
            sw.Flush();
            sw.Close();
        }
        catch (Exception ex)
        {
            Log.AddExceptionToLog($"Error Function [AddExceptionToLog(string className, string functionName, Exception error, string userName =\"\")] in class [Log.cs] {ex.Message}", userName);
        }
    }

    /// <summary>
    /// Return HTML format
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static string ReadLog(DateTime date)
    {
        string logText = "";
        try
        {
            string fileName = "log_" + date.ToString("yyyy-MM-dd") + ".log";
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log\\" + fileName;
            StreamReader read = new StreamReader(filePath);
           // int i = 1;
            string html = "<html><body>";
            while (read.Peek() > 0)
            {
                string str = read.ReadLine();
                //int b = i % 2;
                //if (i % 2 == 0)
                //{
                //    html += "<p style='color:white;'>" + str + "</p>";
                   
                //}
                //else
                //{
                //    html += "<p style='color:blue;'>" +str + "</p>";
                    
                //}
                //i += 1;

                html += "<p style='color:blue;font-family:Arial; font-size:12px;'>" + str + "</p>";
            }
            html += "</body></html>";
            logText = html;
        }
        catch (Exception ex)
        {
            logText = "<font color=red>Opppppp!</font>";// ex.Message;
        }
        return logText;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="html"></param>
    /// <param name="file_name">put only file name</param>
    public static void CreateHTMLFile(string html, string file_name)
    {
        DateTime dt = DateTime.Now;

        MembershipUser myUser = Membership.GetUser();
        string user_id = myUser.ProviderUserKey.ToString();
        string user_name = myUser.UserName;

        string filePath = AppDomain.CurrentDomain.BaseDirectory + "Pages\\Business\\Reports\\Temp\\" + file_name;//html_" + dt.ToString("yyyyMMdd") + ".html";
        if (!File.Exists(filePath))
        {
            FileStream fs = File.Create(filePath);
            fs.Close();

        }
        else
        {
            File.Delete(filePath);
        }
        try
        {
            StreamWriter sw = File.AppendText(filePath);
           
            if (!string.IsNullOrEmpty(html))
            {
                sw.WriteLine(html + System.Environment.NewLine);
            }
            else
            {
                sw.WriteLine(html + System.Environment.NewLine);
            }

            sw.Flush();
            sw.Close();

        }
        catch (Exception ex)
        {
        }
    }
    public static void ReplaceHTMLFile(string html)
    {
        DateTime dt = DateTime.Now;

        MembershipUser myUser = Membership.GetUser();
        string user_id = myUser.ProviderUserKey.ToString();
        string user_name = myUser.UserName;

        string filePath = AppDomain.CurrentDomain.BaseDirectory + "Pages\\Business\\Reports\\Temp\\inform_letter.html";
       
        try
        {
            StreamWriter sw = File.CreateText(filePath);

            if (!string.IsNullOrEmpty(html))
            {
                sw.WriteLine(html + System.Environment.NewLine);
            }
            else
            {
                sw.WriteLine(html + System.Environment.NewLine);
            }

            sw.Flush();
            sw.Close();

        }
        catch (Exception ex)
        {
        }
    }

    public static string GetIPAddress(HttpRequest req)
    {

        string strUserAgent = "";
        string strBrowser = "";
        string strBrowserVersion = "";
        strUserAgent = req.UserAgent;
        strBrowser = req.Browser.Browser;
        strBrowserVersion = req.Browser.Version;

        string ipaddress;
        ipaddress = req.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (ipaddress == "" || ipaddress == null)
            ipaddress = req.ServerVariables["REMOTE_ADDR"];
        //return "[Client IP Address: " + ipaddress + "] [Browser Info: " + strUserAgent + ";  " + strBrowser + ";   " + strBrowserVersion + "]";
        return "[Client IP Address: " + ipaddress + "] [Browser Info: " + strBrowser + ";   " + strBrowserVersion + "]";
    }

    public static int GetLineNumber(Exception ex)
    {
        var lineNumber = 0;
        const string lineSearch = ":line ";
        var index = ex.StackTrace.LastIndexOf(lineSearch);
        if (index != -1)
        {
            var lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
            if (int.TryParse(lineNumberText, out lineNumber))
            {
            }
        }
        return lineNumber;
    }
}