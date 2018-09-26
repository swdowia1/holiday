using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using taskApi.Enums;

namespace taskApi.Contract
{
    /// <summary>
    /// Base response
    /// </summary>
    public class ServiceBaseResponse
    {
        public bool isError { get; set; }
        public bool isInfo { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> InfoMessage { get; set; }

        //HttpRequestMessage
        public ServiceBaseResponse()
        {
            InfoMessage = new List<string>();
        }

        public HttpResponseMessage Response(HttpRequestMessage Request)
        {
            if (this.isError)
            {
                if (string.IsNullOrEmpty(ErrorMessage))
                {
                    StackTrace stackTrace = new StackTrace();

                    // get calling method name
                    string method = stackTrace.GetFrame(1).GetMethod().ReflectedType.Name + "_" + stackTrace.GetFrame(1).GetMethod().Name;
                    ErrorMessage = string.Format("Unknown error in {0}. Please contact Administrator", method);
                }
            }
            else
                this.ErrorMessage = "";

            return Request.CreateResponse(HttpStatusCode.OK, this);
        }

        /// <summary>
        /// Response witch code 403 Forbiten
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public HttpResponseMessage ResponseForbidden(HttpRequestMessage Request)
        {
            return Request.CreateResponse(HttpStatusCode.Forbidden, this);
        }

        public void AddErrorIsNullOrEmpty(string param)
        {
            AddError(ErrorCode.NullError, "Empty " + param);
        }

        public void AddInfo(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                InfoMessage.Add(message);
                isInfo = true;
            }
        }

        public void AddInfo(ErrorCode err)
        {
            InfoMessage.Add(err.ToString());
            isInfo = true;
        }

        #region AddError

        public void AddError(ErrorCode errorCode, List<string> message)
        {
            SetError(errorCode);
            ErrorMessage = string.Join(" ", message.ToArray());
        }

        public void AddError(ErrorCode errorCode, string message)
        {
            SetError(errorCode);
            ErrorMessage = message;
        }

        public void AddError(string message)
        {
            SetError(ErrorCode.UnknownError);
            ErrorMessage = message;
        }

        public void AddError(ErrorCode errorCode)
        {
            SetError(errorCode);
            ErrorMessage = errorCode.ToString();
        }

        private void SetError(ErrorCode errorCode)
        {
            isError = true;
            ErrorCode = errorCode;
        }

        #endregion AddError
    }
}