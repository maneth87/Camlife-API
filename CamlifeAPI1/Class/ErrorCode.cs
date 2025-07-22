using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamlifeAPI1.Class
{
    public class ErrorCode
    {
        /// <summary>
        /// Data type conversion error code
        /// </summary>
        public string DatatypeErrorCode   { get { return "1001"; } }
        /// <summary>
        /// Database connection time out code
        /// </summary>
        public string DbConnectionTimeOutCode { get { return "1002"; } }
        /// <summary>
        /// Database server not found code
        /// </summary>
        public string DbServerNotFoundCode { get { return "1003"; } }
        /// <summary>
        /// Invalid database name code
        /// </summary>
        public string InvalidDatabaseNameCode { get { return "1004"; } }
        /// <summary>
        /// Validation error code
        /// </summary>
        public string ValidationErrorCode { get { return "1005"; } }
        /// <summary>
        /// Duplicated primary key code
        /// </summary>
        public string DuplicatePrimaryKeyCode { get { return "1006"; } }
        /// <summary>
        /// Sql paremater was not supplied.
        /// </summary>
        public string ParameterNotSuppliedCode { get { return "1007"; } }
        /// <summary>
        /// Unexpected error
        /// </summary>
        public string UnexpectedErrorCode{ get { return "0"; } }
        public string ExistingCode { get { return "1009"; } }
        /// <summary>
        /// Data type conversion error message
        /// </summary>
        public string DataTypeError { get { return "Data type conversion error."; } }
        public string DbConnectionTimeOut { get { return "Database connection time out."; } }
        public string DbServerNotFound { get { return "Database sever not found."; } }
        public string InvalidDatabaseName { get { return "Invalid database name."; } }
        public string ValidationError { get { return "Validation error."; } }
        public string DuplicatePrimaryKey { get { return "Duplicated primary key."; } }
        public string ParameterNotSupplied { get { return "Parameter was not supplied."; } }
        /// <summary>
        /// Unexpected error message
        /// </summary>
        public string UnexpectedError { get { return "Unexpected error."; } }
        /// <summary>
        /// Existing policy
        /// </summary>
        public string Existing { get { return "This lead is already exist with policy number [{0}] which will be expired in {1} days. System is not allowed to post new lead."; } }
       /// <summary>
       /// Lead is reach to limited number
       /// </summary>
        public string LeadCountReachToLimitedNumber { get { return "Number of lead is reach to limited number [{0}], application id [{1}]. System is not allowed to post new lead."; } }
       /// <summary>
       /// policy count is reach to limited number
       /// </summary>
        public string PolicyCountReachToLimitedNumber { get { return "Number of policy is reach to limited number [{0}], policy number [{1}]. System is not allowed to post new lead."; } }

    }
}