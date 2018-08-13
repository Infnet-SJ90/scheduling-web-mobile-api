using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingWebMobileApi.Models.Response.Common
{
    public class BadRequestResponse : IResponse
    {
        public BadRequestResponse(string message)
        {
            Errors = new List<ErrorModelResponse>
            {
                new ErrorModelResponse(message)
            };
        }

        public IList<ErrorModelResponse> Errors { get; set; }

        public int StatusCode()
        {
            return 400;
        }
    }
}
