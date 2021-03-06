﻿using SchedulingWebMobileApi.Models.Models.Request;
using SchedulingWebMobileApi.Models.Request;
using SchedulingWebMobileApi.Models.Response.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Application.Interfaces
{
    public interface IAddressAppService : IAppServiceBase<AddressRequestModel, IResponse>
    {
        IResponse GetAll();
    }
}
