using SchedulingWebMobileApi.Domain;
using SchedulingWebMobileApi.Models.Models.Request;
using SchedulingWebMobileApi.Models.Models.Response.Agendamento;
using SchedulingWebMobileApi.Models.Models.Response.Authentication;
using SchedulingWebMobileApi.Models.Models.Response.Cidadao;
using SchedulingWebMobileApi.Models.Request;
using System;

namespace SchedulingWebMobileApi.Core.Mapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<AuthenticationRequestModel, Authentication>();
                config.CreateMap<CidadaoRequestModel, Cidadao>();
                config.CreateMap<AgendamentoRequestModel, Agendamento>()
                    .ForPath(dest => dest.Endereco.EnderecoKey, opt => opt.MapFrom(src => src.EnderecoKey));

                config.CreateMap<Guid, AuthenticationOkResponseModel>()
                    .ForPath(dest => dest.Result.Token, opt => opt.MapFrom(src => src));
                config.CreateMap<Cidadao, CidadaoOkResponseModel>()
                    .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src));
                config.CreateMap<Agendamento, AgendamentoOkResponseModel>()
                    .ForPath(dest => dest.Result.Data, opt => opt.MapFrom(src => $"{src.Data.Day.ToString().PadLeft(2, '0')}/{src.Data.Month.ToString().PadLeft(2, '0')}/{src.Data.Year}"))
                    .ForPath(dest => dest.Result.Hora, opt => opt.MapFrom(src => $"{src.Hora.Hour.ToString().PadLeft(2, '0')}:{src.Hora.Minute.ToString().PadLeft(2, '0')}:{src.Hora.Second.ToString().PadLeft(2, '0')}"))
                    .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src));
            });
        }
    }
}
