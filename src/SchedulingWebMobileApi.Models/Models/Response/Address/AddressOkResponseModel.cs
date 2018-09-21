using SchedulingWebMobileApi.Models.Models.Response.Scheduling;
using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Models.Models.Response.Address
{
    public class AddressOkResponseModel : IResponse<AddressResponseModel>
    {
        public AddressOkResponseModel() { }
        public AddressOkResponseModel(string messageFail)
        {
            Errors = new List<ErrorModelResponse>
            {
                new ErrorModelResponse(messageFail)
            };
        }

        public AddressResponseModel Result { get; set; }
        public IList<ErrorModelResponse> Errors { get; set; }

        public int StatusCode()
        {
            return 200;
        }
    }
}
