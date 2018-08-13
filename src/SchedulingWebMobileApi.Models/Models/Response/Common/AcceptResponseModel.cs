using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Models.Models.Response.Common
{
    public class AcceptResponseModel : IResponse
    {
        public IList<ErrorModelResponse> Errors { get; set; }

        public int StatusCode()
        {
            return 202;
        }
    }
}
