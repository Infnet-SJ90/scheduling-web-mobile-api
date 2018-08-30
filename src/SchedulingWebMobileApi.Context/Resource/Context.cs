using Microsoft.AspNetCore.Http;
using SchedulingWebMobileApi.Domain;

namespace SchedulingWebMobileApi.Context
{
    public class Context
    {
        public HttpRequest Request { get; set; }
        public Cidadao Cidadao { get; set; }
        //public Endereco Cidadao { get; set; }

        public Context(){ }

        public Context(Cidadao cidadao)
        {
            Cidadao = cidadao;
        }
    }

}
