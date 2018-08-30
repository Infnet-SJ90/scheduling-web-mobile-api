using SchedulingWebMobileApi.Models.Response.Common;
using System.Collections.Generic;

namespace SchedulingWebMobileApi.Models.Models.Response.Authentication
{
    public class AuthenticationOkResponseModel : IResponse<AuthenticationResponseModel>
    {
        public AuthenticationOkResponseModel() { }
        public AuthenticationOkResponseModel(string messageFail)
        {
            Errors = new List<ErrorModelResponse>
            {
                new ErrorModelResponse(messageFail)
            };
        }

        public AuthenticationResponseModel Result { get; set; }
        public IList<ErrorModelResponse> Errors { get; set; }

        public int StatusCode()
        {
            return 200;
        }
    }
}
