using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taskApi.Contract;
using taskApi.VM;

namespace taskApi.serwisy
{
    public interface IFunctionService : IDependency
    {
        UserVM CurrentUser();
        void MessageErrorSetResponse(ServiceBaseResponse response, Exception ex);
    }
}