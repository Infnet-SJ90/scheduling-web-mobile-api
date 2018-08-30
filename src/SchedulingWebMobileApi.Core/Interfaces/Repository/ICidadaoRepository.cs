using SchedulingWebMobileApi.Domain;

namespace SchedulingWebMobileApi.Core.Interfaces
{
    public interface ICidadaoRepository : IRepositoryBase<Cidadao>
    {
        bool Exists(Cidadao cidadao);
        bool Exists(string email);
    }
}