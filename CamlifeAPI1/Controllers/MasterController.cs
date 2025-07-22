using CamlifeAPI1.Class;
using CamlifeAPI1.Class.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CamlifeAPI1.Controllers
{
    [Authorize]
    public class MasterController : ApiController
    {
        [Route("api/Master/GetOccupationList")]
        [HttpGet]
        public object GetOccupationList()
        {
            var err = new ErrorCode();
            try
            {
                var obj = da_master.da_occupation.GetOccupationList();
                if (obj != null)
                {
                    return new ResponseExecuteSuccess() { message = "Success", detail = obj };
                }
                else
                {
                    return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
                }

            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("MasterFormController", "GetOccupationList()", ex);
                return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
            }
        }
        [Route("api/Master/GetBeneficiaryRelationList")]
        [HttpGet]
        public object GetBeneficiaryRelationList()
        {
            var err = new ErrorCode();
            try
            {
                var obj = da_master.da_beneficiary_relation.GetBeneficiaryRelationList();
                if (obj != null)
                {
                    return new ResponseExecuteSuccess() { message = "Success", detail = obj };
                }
                else
                {
                    return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
                }

            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("MasterFormController", "GetOccupationList()", ex);
                return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
            }
        }

        [Route("api/Master/GetBeneficiaryRelationListFilterGender")]
        [HttpGet]
        public object GetBeneficiaryRelationList(int gender)
        {
           ErrorCode errorCode = new ErrorCode();
            try
            {
                IEnumerable<bl_master.bl_relation> blRelations = da_master.da_beneficiary_relation.GetBeneficiaryRelationList().Where<bl_master.bl_relation>((Func<bl_master.bl_relation, bool>)(_ => _.GenderCode == gender));
                if (blRelations != null)
                    return (object)new ResponseExecuteSuccess()
                    {
                        message = "Success",
                        detail = (object)blRelations
                    };
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("MasterFormController", "GetBeneficiaryRelationList(int gender)", ex);
                return (object)new ResponseExecuteError()
                {
                    code = errorCode.UnexpectedErrorCode,
                    message = errorCode.UnexpectedError
                };
            }
        }

        [Route("api/Master/GetMasterRelation")]
        [HttpGet]
        public object GetMasterRelation( string masterCode)
        {
            var err = new ErrorCode();
            try
            {
                var obj = da_master.da_master_relation.GetMasterRelationList(masterCode);
                if (obj != null)
                {
                    return new ResponseExecuteSuccess() { message = "Success", detail = obj };
                }
                else
                {
                    return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
                }

            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("MasterFormController", "GetOccupationList()", ex);
                return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
            }
        }
        [Route("api/Master/GetCountryList")]
        [HttpGet]
        public object GetCountryList()
        {
            var err = new ErrorCode();
            try
            {
                var obj = da_master.da_countries.GetCountryList();
                if (obj != null)
                {
                    return new ResponseExecuteSuccess() { message = "Success", detail = obj };
                }
                else
                {
                    return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
                }

            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("MasterFormController", "GetCountryList()", ex);
                return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
            }
        }

        [Route("api/Master/GetProvinceList")]
        [HttpGet]
        public object GetProvinceList()
        {
            var err = new ErrorCode();
            try
            {
                var obj = da_address.province.GetProvince();
                if (obj != null)
                {
                    return new ResponseExecuteSuccess() { message = "Success", detail = obj };
                }
                else
                {
                    return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
                }

            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("MasterFormController", "GetProvinceList()", ex);
                return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
            }
        }

        [Route("api/Master/GetDistrictList")]
        [HttpGet]
        public object GetDisctrinctList(string provinceCode)
        {
            var err = new ErrorCode();
            try
            {
                var obj = da_address.district.GetDistrict(provinceCode);
                if (obj != null)
                {
                    return new ResponseExecuteSuccess() { message = "Success", detail = obj };
                }
                else
                {
                    return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
                }

            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("MasterFormController", "GetDisctrinctList(string provinceCode)", ex);
                return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
            }
        }


        [Route("api/Master/GetCommuneList")]
        [HttpGet]
        public object GetCoummuneList(string districtCode)
        {
            var err = new ErrorCode();
            try
            {
                var obj = da_address.commune.GetCommune(districtCode);
                if (obj != null)
                {
                    return new ResponseExecuteSuccess() { message = "Success", detail = obj };
                }
                else
                {
                    return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
                }

            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("MasterFormController", "GetCoummuneList(string districtCode)", ex);
                return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
            }
        }

        [Route("api/Master/GetVillageList")]
        [HttpGet]
        public object GetVillageList(string communeCode)
        {
            var err = new ErrorCode();
            try
            {
                var obj = da_address.village.GetVillage(communeCode);
                if (obj != null)
                {
                    return new ResponseExecuteSuccess() { message = "Success", detail = obj };
                }
                else
                {
                    return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
                }

            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("MasterFormController", "GetVillageList(string communeCode)", ex);
                return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
            }
        }

        [Route("api/Master/GetMasterList")]
        [HttpGet]
        public object GetMasterList(string masterCode)
        {
            var err = new ErrorCode();
            try
            {
                var obj = da_master.da_master_list.GetMasterList(masterCode);
                if (obj != null)
                {
                    return new ResponseExecuteSuccess() { message = "Success", detail = obj };
                }
                else
                {
                    return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
                }

            }
            catch (Exception ex)
            {
                Log.AddExceptionToLog("MasterFormController", "GetMasterList(string masterCode)", ex);
                return new ResponseExecuteError() { code = err.UnexpectedErrorCode, message = err.UnexpectedError };
            }
        }


    }
}
