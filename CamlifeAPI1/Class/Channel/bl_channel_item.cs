using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_channel_item
/// </summary>
public class bl_channel_item
{
	#region "Private Variable"

    private string _Channel_Item_ID;
    private string _Channel_Name;
    private string _Channel_HQ_Address;
    private string _Created_By;
    private DateTime _Created_On;
    private string _Created_Note;
    private int _Status;
    private string _Channel_Sub_ID;

  
    #endregion

	#region "Constructor"
    public bl_channel_item()
	{
	}
	#endregion
    
	#region "Public Properties"

    public string Channel_Item_ID
    {
        get { return _Channel_Item_ID; }
        set { _Channel_Item_ID = value; }
	}

    public string Channel_Name
    {
        get { return _Channel_Name; }
        set { _Channel_Name = value; }
	}

    public string Channel_HQ_Address
    {
        get { return _Channel_HQ_Address; }
        set { _Channel_HQ_Address = value; }
    }
       
    public int Status
    {
        get { return _Status; }
        set { _Status = value; }
    }

    public string Channel_Sub_ID {
        get { return _Channel_Sub_ID; }
        set { _Channel_Sub_ID = value; }
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

    public string Channel_HQ_Address_KH { get; set; }

    #endregion

    /// <summary>
    /// Return Properties Name
    /// </summary>
    public class NAME
    {
        public static string Channel_Item_ID { get { return "Channel_Item_ID"; } }
        public static string Channel_Name { get { return "Channel_Name"; } }
        public static string Channel_HQ_Address { get { return "Channel_HQ_Address"; } }
    }
}