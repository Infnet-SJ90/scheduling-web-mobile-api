using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Models.Models.Response.Agendamento
{
    public class AgendamentosOkResponseModel : IResponse<IList<AgendamentoResponseModel>>
    {
        public AgendamentosOkResponseModel() { }
        public AgendamentosOkResponseModel(string messageFail)
        {
            Errors = new List<ErrorModelResponse>
            {
                new ErrorModelResponse(messageFail)
            };
        }

        public IList<AgendamentoResponseModel> Result { get; set; }
        public IList<ErrorModelResponse> Errors { get; set; }

        public int StatusCode()
        {
            return 200;
        }
    }
}
