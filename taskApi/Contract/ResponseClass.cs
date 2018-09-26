using System;
using System.Collections.Generic;
using System.Linq;

namespace taskApi.Contract
{
    /// <summary>
    /// Response(One result T)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T> : ServiceBaseResponse
    {
        public T Result { get; set; }
    }

    /// <summary>
    /// Response(List of T)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceListResponse<T> : ServiceResponse<IEnumerable<T>>
    {
        //TODO: CR: the same model is in CVMine.WebApp.ViewModels.General.PaggingResponseVM
        public int TotalCount { get; set; }

        public int PageLimit { get; set; }

        public int PageCount
        {
            get
            {
                return this.PageLimit > 0 ? (int)Math.Ceiling((double)this.TotalCount / this.PageLimit) : 0;
            }
        }

        public void SetTotalCount()
        {
            this.TotalCount = this.Result.Count();
        }
    }
}