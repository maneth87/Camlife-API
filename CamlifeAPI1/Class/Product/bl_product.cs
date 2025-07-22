using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
/// <summary>
/// Summary description for bl_product
/// </summary>
public class bl_product
{
	 #region "Private Variable"
        private string _Product_ID;
        private string _Plan_Block;
        private string _Plan_Type;
        private string _Plan_Code;
        private string _Plan_Code2;
        private string _Kh_Title;
        private string _Kh_Abbr;
        private string _En_Title;
        private string _En_Abbr;
        private int _Age_Min;
        private int _Age_Max;
        private double _Sum_Min;
        private double _Sum_Max;
        private int _Product_Type_ID;
          
    #endregion


    #region "Constructor"
        public bl_product()
        {

        }
        #endregion

    #region "Public Property"

    public string Product_ID
    {
        get { return _Product_ID; }
        set { _Product_ID = value; }
    }

    public string Plan_Block
    {
        get { return _Plan_Block; }
        set { _Plan_Block = value; }
    }

    public string Plan_Type
    {
        get { return _Plan_Type; }
        set { _Plan_Type = value; }
    }

    public string Plan_Code
    {
        get { return _Plan_Code; }
        set { _Plan_Code = value; }
    }

    public string Plan_Code2
    {
        get { return _Plan_Code2; }
        set { _Plan_Code2 = value; }
    }

    public string Kh_Title
    {
        get { return _Kh_Title; }
        set { _Kh_Title = value; }
    }

    public string Kh_Abbr
    {
        get { return _Kh_Abbr; }
        set { _Kh_Abbr = value; }
    }

    public string En_Title
    {
        get { return _En_Title; }
        set { _En_Title = value; }
    }

    public string En_Abbr
    {
        get { return _En_Abbr; }
        set { _En_Abbr = value; }
    }

    public int Age_Min
    {
        get { return _Age_Min; }
        set { _Age_Min = value; }
    }

    public int Age_Max
    {
        get { return _Age_Max; }
        set { _Age_Max = value; }
    }

    public double Sum_Min
    {
        get { return _Sum_Min; }
        set { _Sum_Min = value; }
    }

    public double Sum_Max
    {
        get { return _Sum_Max; }
        set { _Sum_Max = value; }
    }

    public int Product_Type_ID
    {
        get { return _Product_Type_ID; }
        set { _Product_Type_ID = value; }
    }
    public string Remarks { get; set; }
    /// <summary>
    /// Return product id + remarks
    /// </summary>
    public string ProductIDRemarks { get { return Product_ID + " " + Remarks; } }
    #endregion

    public class NAME
    {
        public static string Product_ID { get { return "Product_ID"; } }
        public static string Plan_Block { get { return "Plan_Block"; } }
        public static string ChannelItemId { get { return "ChannelItemId"; } }
        public static string ChannelName { get { return "ChannelName"; } }
        /// <summary>
        /// Return product id + remarks
        /// </summary>
        public static string ProductRemarks { get { return "ProductIDRemarks"; } }
    }
}