using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bl_micro_product_rider_rate
/// </summary>
public class bl_micro_product_rider_rate:bl_micro_product_rate
{
	public bl_micro_product_rider_rate()
	{
		//
		// TODO: Add constructor logic here
		//
	}

   

    public class RATE_TYPE_OPTION
    {
        /// <summary>
        /// Fix amount of rate
        /// </summary>
        public static string VALUE { get { return "VALUE"; } }
        /// <summary>
        /// Percentage
        /// </summary>
        public static string RATE { get { return "RATE"; } }
    }
    
}