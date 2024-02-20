using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Web;

namespace Healing2Peace.Model
{
    public class ResponseModel
    {
        public bool doLogOut { get; set; }
        public string languageCode { get; set; }
        public ResponseData responseData { get; set; }
        public ResponseMessage responseMsg { get; set; }


    }
    public class ResponseMessage
    {
        public bool isError { get; set; }
        public string errorMessage { get; set; }
        public bool isWarning { get; set; }
        public string warningMessage { get; set; }
        public bool isEmptyCollection { get; set; }
        public string successMessage { get; set; }

    }

    public class ResponseData
    {
        public bool isObject { get; set; }
        public bool isCollection { get; set; }
        public string responseDataType { get; set; }
        public dynamic data = new ExpandoObject();
    }
}