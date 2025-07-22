using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_channel
/// </summary>
public class bl_channel
{
	#region "Private Variable"
	
    private string _Channel_ID;	
    private string _Details;
    private string _Type;  
    private string _Created_By;
    private DateTime _Created_On;
    private string _Created_Note;
    private int _Status; 
  
    #endregion

	#region "Constructor"
    public bl_channel()
	{
	}
	#endregion
    
	#region "Public Properties"

    public string Channel_ID
    {
        get { return _Channel_ID; }
        set { _Channel_ID = value; }
	}
    
    public string Details
    { 
        get { return _Details; }
        set { _Details = value; }
    }

    public string Type
    {
        get { return _Type; }
        set { _Type = value; }
    }
    
      
    public int Status
    {
        get { return _Status; }
        set { _Status = value; }
    }

    public string Created_By
    {
        get { return _Created_By; }
        set { _Created_By = value; }
    }

    public DateTime Created_On
    {
        get { return _Created_On; }
        set { _Created_On = value; }
    }
       

	#endregion
}