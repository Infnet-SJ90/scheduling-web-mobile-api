using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Models.Models.Response.Scheduling
{
    public class SchedulingsOkResponseModel : IResponse<IList<SchedulingResponseModel>>
    {
        public SchedulingsOkResponseModel() { }
        public SchedulingsOkResponseModel(string messageFail)
        {
            Errors = new List<ErrorModelResponse>
            {
                new ErrorModelResponse(messageFail)
            };
        }

        public IList<SchedulingResponseModel> Result { get; set; }
        public IList<ErrorModelResponse> Errors { get; set; }

        public int StatusCode()
        {
            return 200;
        }
    }
}
