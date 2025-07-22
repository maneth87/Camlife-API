using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Ajax.Utilities;
using System.Web.Http.Results;

namespace CamlifeAPI1
{
    public partial class myToken : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected  void btnToken_Click(object sender, EventArgs e)
        {
            var client = new RestClient("http://192.168.1.9:156/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=password&username=hattha@bank.com&password=HathaBank@2022", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            // Response.Write("Status Codt:" + response.StatusCode + " Response Status:" + response.ResponseStatus);
            myRest mrest = new myRest();
            mrest= JsonConvert.DeserializeObject<myRest>(response.Content);
           // Response.Write(JsonConvert.SerializeObject(response));

            Response.Write(response.Content);
            Response.Write(mrest);
        }
        class myRest
        {
            string access_token { get; set; }
            string token_type { get; set; }
            int expires_in { get; set; }
            string userName { get; set; }
        }

        class Toten { 
        string access_token { get; set; }
        } 
        protected void btnTokenJson_Click(object sender, EventArgs e)
        {
            var client = new RestClient("http://192.168.1.9:156/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "userName=hattha@bank.com&password=HathaBank@2022&grant_type=password", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            // Response.Write("Status Codt:" + response.StatusCode + " Response Status:" + response.ResponseStatus);
            myRest mrest = new myRest();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            mrest = JsonConvert.DeserializeObject<myRest>(response.Content,settings);
            // Response.Write(JsonConvert.SerializeObject(response));

            Response.Write(response.Content);
        }

        protected void btnMyData_Click(object sender, EventArgs e)
        {
            string smsRespond = "";
            System.Net.WebClient web = new WebClient();
            try
            {
               
                string strResponse = "";
                myRest resToken = new myRest();
                try
                {
                    string credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("fYUPFkpsflBTLOW6fBjhGEscOawa:gilxmEWCjAfXntJaGzKBPwMEZOYa"));
                    web.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                    web.Headers.Add("content-type", "application/x-www-form-urlencoded");
                    web.Encoding = System.Text.Encoding.UTF8;
                    strResponse = web.UploadString("https://stg-api.cellcard.com.kh:8243/token", "grant_type=client_credentials");

                    resToken = JsonConvert.DeserializeObject<myRest>(strResponse);
                }
                catch (Exception ex)
                {
                    resToken = new myRest();
               
                }

                //web.Headers[HttpRequestHeader.Authorization] = "Bearer " + resToken.;
                //web.Headers.Add("content-type", "application/x-www-form-urlencoded");
                ////web.Headers.Add("authorization", "Bearer " + resToken.access_token);
                //web.Encoding = System.Text.Encoding.UTF8;
                //cellCard.MessageTo = cellCard.MessageTo.Replace("+", "");

                //smsRespond = web.UploadString("https://stg-api.cellcard.com.kh:8243/pushsms/v1/accounts/" + cellCard.MessageTo + "/push", "originator=Camlife&content_message=" + cellCard.MessageTextAPI + "&request_id=" + cellCard.ClientCorrelator);

                //ResponseAPIM resAPIM = JsonConvert.DeserializeObject<ResponseAPIM>(smsRespond);

                //if (resAPIM.Data.error_code == "0000")
                //{
                //    result = new string[] { resAPIM.Meta.server_correlation_id, "Success" };
                //}
                //else
                //{
                //    result = new string[] { resAPIM.Data.error_code + ":" + resAPIM.Data.error_message, "Fail" };
                //}



            }
            catch (Exception ex)
            {
              
            }
        }
    }
   
}