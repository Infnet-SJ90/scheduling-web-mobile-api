using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingWebMobileApi.Models.Response.Common
{
    public interface IResponse<T> : IResponse where T : class
    {
        T Result { get; set; }
    }

    public interface IResponse
    {
        IList<ErrorModelResponse> Errors { get; set; }
        int StatusCode();
    }
}
