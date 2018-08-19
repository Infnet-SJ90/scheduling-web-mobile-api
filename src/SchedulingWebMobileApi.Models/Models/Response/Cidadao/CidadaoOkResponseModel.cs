using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Models.Models.Response.Cidadao
{
    public class CidadaoOkResponseModel : IResponse<CidadaoResponseModel>
    {
        public CidadaoOkResponseModel() { }
        public CidadaoOkResponseModel(string messageFail)
        {
            Errors = new List<ErrorModelResponse>
            {
                new ErrorModelResponse(messageFail)
            };
        }

        public CidadaoResponseModel Result { get; set; }
        public IList<ErrorModelResponse> Errors { get; set; }

        public int StatusCode()
        {
            return 200;
        }
    }
}
