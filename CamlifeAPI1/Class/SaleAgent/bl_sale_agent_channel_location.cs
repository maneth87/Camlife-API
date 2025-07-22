using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_sale_agent_channel_location
/// </summary>
public class bl_sale_agent_channel_location
{
	 #region "Private Variable"

        private string _Sale_Agent_Channel_Location_ID;
        private string _Sale_Agent_ID;
        private string _Channel_Channel_Item_ID;
        private string _Channel_Location_ID;       
        private DateTime _Created_On;
        private string _Created_By;
        private string _Created_Note;
        private int _Status;
     
    #endregion


    #region "Constructor"
        public bl_sale_agent_channel_location()
        {

        }
        #endregion

    #region "Public Property"

        public string Sale_Agent_Channel_Location_ID
        {
            get { return _Sale_Agent_Channel_Location_ID; }
            set { _Sale_Agent_Channel_Location_ID = value; }
        }

        public string Sale_Agent_ID
        {
            get { return _Sale_Agent_ID; }
            set { _Sale_Agent_ID = value; }
        }

        public string Channel_Channel_Item_ID
        {
            get { return _Channel_Channel_Item_ID; }
            set { _Channel_Channel_Item_ID = value; }
        }

        public string Channel_Location_ID
        {
            get { return _Channel_Location_ID; }
            set { _Channel_Location_ID = value; }
        }
     
        public DateTime Created_On
        {
            get { return _Created_On; }
            set { _Created_On = value; }
        }

        public string Created_By
        {
            get { return _Created_By; }
            set { _Created_By = value; }
        }

        public string Created_Note
        {
            get { return _Created_Note; }
            set { _Created_Note = value; }
        }

        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
    #endregion
}