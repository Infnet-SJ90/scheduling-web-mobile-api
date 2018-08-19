using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingWebMobileApi.Models.Response.Common
{
    public class ErrorModelResponse
    {
        public ErrorModelResponse(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}
