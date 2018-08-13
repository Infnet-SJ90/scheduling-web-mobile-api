using SchedulingWebMobileApi.Core.Entities;
using System;

namespace SchedulingWebMobileApi.Core.Interfaces
{
    public interface ICidadaoRepository : IRepositoryBase<Cidadao>
    {
        bool Exists(Cidadao cidadao);
        bool Exists(string email);
        Guid Authentication(Authentication cidadao);
    }
}