using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamlifeAPI1.Class
{
    public class ResponseValidateError
    {
       
             public string status
            {
                get
                {
                    return "500";
                }
            }
            public string code { get { return "1005"; } }
            public string message { get; set; }

            public List<ValidateRequest> detail { get; set; }
        }
    
}