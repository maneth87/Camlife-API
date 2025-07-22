using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamlifeAPI1.Class
{
    public class Security
    {
        public static bool Login(string username, string password)
        {
            string myuser = "maneth.som";string mypassword = "miscamlife?";
            if (username == myuser && password == mypassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}