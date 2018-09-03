using SchedulingWebMobileApi.Models.Models.Response.Agendamento;
using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Models.Models.Response.Local
{
    public class LocaisOkResponseModel : IResponse<IList<LocalResponseModel>>
    {
        public LocaisOkResponseModel() { }
        public LocaisOkResponseModel(string messageFail)
        {
            Errors = new List<ErrorModelResponse>
            {
                new ErrorModelResponse(messageFail)
            };
        }

        public IList<LocalResponseModel> Result { get; set; }
        public IList<ErrorModelResponse> Errors { get; set; }

        public int StatusCode()
        {
            return 200;
        }
    }
}
