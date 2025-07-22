using CamlifeAPI1.Class;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using NPOI.DDF;
using NPOI.SS.Formula.Functions;
using NPOI.Util;
using Org.BouncyCastle.Bcpg.OpenPgp;
using RestSharp.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CamlifeAPI1.Controllers
{
    [Authorize]
    public class SaleAgentController : ApiController
    {
        [Route("api/SaleAgent/Get")]
        [HttpGet]
        public object GetSaleAgent(string saleAgentId)
        {
            try
            {
                bl_sale_agent_micro saleAgentMicro = da_sale_agent.GetSaleAgentMicro(saleAgentId);
                return (object)new SaleAgentController.ReposeFromServer()
                {
                    Status = "OK",
                    StatusCode = 200,
                    Message = "Success",
                    Detail = (object)saleAgentMicro
                };
            }
            catch
            {
                return (object)new SaleAgentController.ReposeFromServer()
                {
                    Status = "ERROR",
                    StatusCode = 0,
                    Message = "Unexpected error",
                    Detail = (object)null
                };
            }
        }

        [Route("api/SaleAgent/GetChannelSaleAgentByUserName")]
        [HttpGet]
        public object GetChannelSaleAgentByUserName(string userName)
        {
            try
            {
                bl_channel_sale_agent saleAgentByUserName = da_channel.GetChannelSaleAgentByUserName(userName);
                return (object)new SaleAgentController.ReposeFromServer()
                {
                    Status = "OK",
                    StatusCode = 200,
                    Message = "Success",
                    Detail = (object)saleAgentByUserName
                };
            }
            catch
            {
                return (object)new SaleAgentController.ReposeFromServer()
                {
                    Status = "ERROR",
                    StatusCode = 0,
                    Message = "Unexpected error",
                    Detail = (object)null
                };
            }
        }

        [Route("api/SaleAgent/GetSaleAgentByChannelItemIdBranchCode")]
        [HttpGet]
        public object GetSaleAgentByChannelItemIdBranchCode(string channelItemId, string branchCode)
        {
            try
            {
                object itemIdBranchCode = (object)da_sale_agent.GetSaleAgentMicroListByChannelItemIdBranchCode(channelItemId, branchCode);
                return (object)new SaleAgentController.ReposeFromServer()
                {
                    Status = "OK",
                    StatusCode = 200,
                    Message = "Success",
                    Detail = itemIdBranchCode
                };
            }
            catch
            {
                return (object)new SaleAgentController.ReposeFromServer()
                {
                    Status = "ERROR",
                    StatusCode = 0,
                    Message = "Unexpected error",
                    Detail = (object)null
                };
            }
        }

        [Route("api/SaleAgent/GetChannelLocationByChannelItemIdAndUser")]
        [HttpGet]
        public object GetChannelLocationByChannelItemIdAndUser(string channelItemId, string userName)
        {
            try
            {
                List<bl_channel_location> channelItemIdUser = da_channel.GetChannelLocationByChannelItemIdUser(channelItemId, userName);
                return (object)new ResponseExecuteSuccess()
                {
                    message = "Success",
                    detail = (object)channelItemIdUser
                };
            }
            catch
            {
                CamlifeAPI1.Class.ErrorCode errorCode = new CamlifeAPI1.Class.ErrorCode();
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
        }

        public class ReposeFromServer
        {
            public int StatusCode { get; set; }

            public string Status { get; set; }

            public string Message { get; set; }

            public object Detail { get; set; }
        }
    }
}
