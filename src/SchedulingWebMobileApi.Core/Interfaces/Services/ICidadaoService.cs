using SchedulingWebMobileApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Core.Interfaces.Services
{
    public interface ICidadaoService : IServiceBase<Cidadao>
    {
        Guid Authentication(Authentication cidadao);
    }
}
