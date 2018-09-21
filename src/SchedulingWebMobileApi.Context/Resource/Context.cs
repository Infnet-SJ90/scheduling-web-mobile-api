using Microsoft.AspNetCore.Http;
using SchedulingWebMobileApi.Domain;

namespace SchedulingWebMobileApi.Context
{
    public class Context
    {
        public HttpRequest Request { get; set; }
        public Citezen Citezen { get; set; }

        public Context(){ }

        public Context(Citezen citezen)
        {
            Citezen = citezen;
        }
    }

}
