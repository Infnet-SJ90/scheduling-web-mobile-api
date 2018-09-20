using SchedulingWebMobileApi.Domain;

namespace SchedulingWebMobileApi.Core.Interfaces
{
    public interface ICitezenRepository : IRepositoryBase<Citezen>
    {
        bool Exists(Citezen citezen);
        bool Exists(string email);
    }
}