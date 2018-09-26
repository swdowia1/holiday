using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using taskApi.Contract;
using taskApi.Enums;
using taskApi.VM;

namespace taskApi.serwisy
{
    public class FunctionService : IFunctionService
    {
        public UserVM CurrentUser()
        {

            var principal = HttpContext.Current.User as ClaimsPrincipal;
            if (principal == null || principal.Claims.Count() == 0) return null;
            var userId = principal.FindFirst(x => x.Type == "sub")?.Value;
            var user = new UserVM() { Id = int.Parse(userId) };
            return user;
        }

        public void MessageErrorSetResponse(ServiceBaseResponse response, Exception ex)
        {
            var builder = new StringBuilder();
            WriteExceptionDetails(ex, builder, 0);

            string errorMSG = builder.ToString();
            response.isError = true;
            LogManager.GetCurrentClassLogger().Error(errorMSG + " ");

            response.ErrorMessage = errorMSG;
            response.ErrorCode = ErrorCode.Excetion;

        }

        public static void WriteExceptionDetails(Exception exception, StringBuilder builderToFill, int level)
        {
            var indent = new string(' ', level);

            if (level > 0)
            {
                builderToFill.AppendLine(indent + "=== INNER EXCEPTION ===");
            }

            Action<string> append = (prop) =>
            {
                var propInfo = exception.GetType().GetProperty(prop);
                var val = propInfo.GetValue(exception);

                if (val != null)
                {
                    builderToFill.AppendFormat("{0}{1}: {2}{3}", indent, prop, val.ToString(), Environment.NewLine);
                }
            };

            append("Message");
            append("HResult");
            append("HelpLink");
            append("Source");
            append("StackTrace");
            append("TargetSite");

            foreach (DictionaryEntry de in exception.Data)
            {
                builderToFill.AppendFormat("{0} {1} = {2}{3}", indent, de.Key, de.Value, Environment.NewLine);
            }

            if (exception.InnerException != null)
            {
                WriteExceptionDetails(exception.InnerException, builderToFill, ++level);
            }
        }
    }
}