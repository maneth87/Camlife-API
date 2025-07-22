using CamlifeAPI1.Class;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using NPOI.DDF;
using NPOI.HSSF.Record.PivotTable;
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
    public class MicroProductController : ApiController
    {
        public string Get() => "Micro Prouct Configuration.";

        [Route("api/GetProductConfigList")]
        [HttpGet]
        public object GetProductConfigList()
        {
            object obj = new object();
            return (object)new ResponseExecuteSuccess()
            {
                message = da_banca.MESSAGE,
                detail = (object)da_micro_product_config.ProductConfig.GetProductMicroProductSO()
            };
        }

        [Route("api/product/GetProductConfigListByChannelItem")]
        [HttpPost]
        public object GetProductConfigListByChannelItem(
          MicroProductController.ParaProductConfigListByChannelItem paras)
        {
            object obj = new object();
            List<bl_micro_product_config> microProductConfigList = new List<bl_micro_product_config>();
            List<bl_micro_product_config> listByChannelItemId = da_micro_product_config.ProductConfig.GetMicroProductConfigListByChannelItemId(paras.ChannelItemId);
            return (object)new ResponseExecuteSuccess()
            {
                message = "Success",
                detail = (object)listByChannelItemId
            };
        }

        [Route("api/product/GetProductConfigListByChannelItemSaleAgent")]
        [HttpPost]
        public object GetProductConfigListByChannelItemSaleAgent(
          MicroProductController.ParaProductConfigListByChannelItemSaleAgent paras)
        {
            object obj = new object();
            List<bl_micro_product_config> microProductConfigList = new List<bl_micro_product_config>();
            List<bl_micro_product_config> channelItemIdSaleAgent = da_micro_product_config.ProductConfig.GetMicroProductConfigListByChannelItemIdSaleAgent(paras.ChannelItemId, paras.SaleAgentId);
            return (object)new ResponseExecuteSuccess()
            {
                message = "Success",
                detail = (object)channelItemIdSaleAgent
            };
        }

        [Route("api/product/GetProductConfigListByChannelItemForPortal")]
        [HttpPost]
        public object GetProductConfigListByChannelItemForPortal(
          MicroProductController.ParaProductConfigListByChannelItem paras)
        {
            object obj = new object();
            List<bl_micro_product_config> microProductConfigList = new List<bl_micro_product_config>();
            List<bl_micro_product_config> channelItemIdForPortal = da_micro_product_config.ProductConfig.GetMicroProductConfigListByChannelItemIdForPortal(paras.ChannelItemId);
            return (object)new ResponseExecuteSuccess()
            {
                message = "Success",
                detail = (object)channelItemIdForPortal
            };
        }

        [Route("api/product/GetProductDetail")]
        [HttpGet]
        public object GetProductDetail(string productId)
        {
            object obj = new object();
            bl_micro_product_config microProductConfig = new bl_micro_product_config();
            bl_micro_product_config productMicroProduct = da_micro_product_config.ProductConfig.GetProductMicroProduct(productId);
            object productDetail;
            if (!string.IsNullOrWhiteSpace(productMicroProduct.ProductId))
                productDetail = (object)new ResponseExecuteSuccess()
                {
                    message = "Success",
                    detail = (object)productMicroProduct
                };
            else
                productDetail = (object)new ResponseExecuteSuccess()
                {
                    message = "Success",
                    detail = (object)null
                };
            return productDetail;
        }

        [Route("api/product/GetPremium")]
        [HttpGet]
        public object GetPremium(string productId, int gender, int age, double sumAssured, int payMode)
        {
            CamlifeAPI1.Class.ErrorCode errorCode = new CamlifeAPI1.Class.ErrorCode();
            try
            {
                Calculation.Premium microProducPremium = Calculation.GetMicroProducPremium(productId, gender, age, sumAssured, payMode);
                return (object)new ResponseExecuteSuccess()
                {
                    message = "Success",
                    detail = (object)microProducPremium
                };
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog($"Error function [GetPremium(string productId, int Gender, int age, double sumAssured, int payMode)] in class [MicroProductControl], line number:{Log.GetLineNumber(ex).ToString()}, detail:{ex.Message}");
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
        }

        [Route("api/product/GetPremiumRider")]
        [HttpGet]
        public object GetPremiumRider(
          string productId,
          int gender,
          int age,
          double sumAssured,
          int payMode)
        {
            CamlifeAPI1.Class.ErrorCode errorCode = new CamlifeAPI1.Class.ErrorCode();
            try
            {
                Calculation.Premium productRiderPremium = Calculation.GetMicroProductRiderPremium(productId, gender, age, sumAssured, payMode);
                return (object)new ResponseExecuteSuccess()
                {
                    message = "Success",
                    detail = (object)productRiderPremium
                };
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog($"Error function [GetPremium(string productId, int Gender, int age, double sumAssured, int payMode)] in class [MicroProductControl], line number:{Log.GetLineNumber(ex).ToString()}, detail:{ex.Message}");
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
        }

        [Route("api/product/GetPremiumDiscount")]
        [HttpPost]
        public object GetPremiumDiscount(MicroProductController.ParaGetProductDiscount para)
        {
            CamlifeAPI1.Class.ErrorCode errorCode = new CamlifeAPI1.Class.ErrorCode();
            MicroProductController.PremiumDiscount premiumDiscount = new MicroProductController.PremiumDiscount();
            try
            {
                bl_micro_product_discount_config productDiscountConfig = new bl_micro_product_discount_config();
                bl_micro_product_discount_config productDiscount = da_micro_product_config.DiscountConfig.GetProductDiscount(para.ProductId, para.ProductRiderId, para.SumAssured, para.SumAssuredRider, para.ClientType);
                if (!string.IsNullOrWhiteSpace(productDiscount.ProductID) && productDiscount.ExpiryDate >= Helper.FormatDateTime(para.ApplicaitonDate))
                {
                    premiumDiscount.BasicDiscountAmount = productDiscount.BasicDiscountAmount;
                    premiumDiscount.RiderDiscountAmount = productDiscount.RiderDiscountAmount;
                }
                return (object)new ResponseExecuteSuccess()
                {
                    message = "Success",
                    detail = (object)premiumDiscount
                };
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("MicroProductControl", "GetPremiumDiscount(ParaGetProductDiscount para)", ex);
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
        }

        [Route("api/product/GetAge")]
        [HttpGet]
        public object GetAge(string dob, string compareDate)
        {
            CamlifeAPI1.Class.ErrorCode errorCode = new CamlifeAPI1.Class.ErrorCode();
            try
            {
                int num = Calculation.Culculate_Customer_Age(dob, Helper.FormatDateTime(compareDate));
                return (object)new ResponseExecuteSuccess()
                {
                    message = "Success",
                    detail = (object)num
                };
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog($"Error function [GetAge(string dob, string compareDate)] in class [MicroProductControl], line number:{Log.GetLineNumber(ex).ToString()}, detail:{ex.Message}");
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
        }

        private class PremiumDiscount
        {
            public double BasicDiscountAmount { get; set; }

            public double RiderDiscountAmount { get; set; }
        }

        public class ParaProductConfigListByChannelItem
        {
            public string ChannelItemId { get; set; }
        }

        public class ParaProductConfigListByChannelItemSaleAgent
        {
            public string ChannelItemId { get; set; }

            public string SaleAgentId { get; set; }
        }

        public class ParaGetProductDiscount
        {
            public string ProductId { get; set; }

            public string ProductRiderId { get; set; }

            public double SumAssured { get; set; }

            public double SumAssuredRider { get; set; }

            public string ClientType { get; set; }

            public string ApplicaitonDate { get; set; }
        }
    }
}