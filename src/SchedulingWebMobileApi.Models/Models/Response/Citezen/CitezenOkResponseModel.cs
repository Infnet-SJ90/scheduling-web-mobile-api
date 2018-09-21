using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Models.Models.Response.Citezen
{
    public class CitezenOkResponseModel : IResponse<CitezenResponseModel>
    {
        public CitezenOkResponseModel() { }
        public CitezenOkResponseModel(string messageFail)
        {
            Errors = new List<ErrorModelResponse>
            {
                new ErrorModelResponse(messageFail)
            };
        }

        public CitezenResponseModel Result { get; set; }
        public IList<ErrorModelResponse> Errors { get; set; }

        public int StatusCode()
        {
            return 200;
        }
    }
}
