using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Models.Models.Response.Common
{
    public class ForbbidenResponseModel : IResponse
    {
        public ForbbidenResponseModel(string message)
        {
            Errors = new List<ErrorModelResponse>
            {
                new ErrorModelResponse(message)
            };
        }

        public IList<ErrorModelResponse> Errors { get; set; }

        public int StatusCode()
        {
            return 403;
        }
    }
}
