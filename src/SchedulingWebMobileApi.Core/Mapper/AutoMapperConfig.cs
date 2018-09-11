using SchedulingWebMobileApi.Domain;
using SchedulingWebMobileApi.Models.Models.Request;
using SchedulingWebMobileApi.Models.Models.Response.Agendamento;
using SchedulingWebMobileApi.Models.Models.Response.Authentication;
using SchedulingWebMobileApi.Models.Models.Response.Cidadao;
using SchedulingWebMobileApi.Models.Models.Response.Local;
using SchedulingWebMobileApi.Models.Request;
using System;
using System.Collections.Generic;

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
                config.CreateMap<LocalRequestModel, Local>();
                config.CreateMap<AgendamentoRequestModel, Agendamento>()
                    .ForPath(dest => dest.Endereco.EnderecoKey, opt => opt.MapFrom(src => src.EnderecoKey));

                config.CreateMap<Guid, AuthenticationOkResponseModel>()
                    .ForPath(dest => dest.Result.Token, opt => opt.MapFrom(src => src));

                config.CreateMap<Local, LocalOkResponseModel>()
                    .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src));
                config.CreateMap<Local, LocalResponseModel>();
                config.CreateMap<IList<LocalResponseModel>, LocaisOkResponseModel>()
                    .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src));

                config.CreateMap<Cidadao, CidadaoOkResponseModel>()
                    .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src));

                config.CreateMap<Agendamento, AgendamentoResponseModel>()
                    .ForPath(dest => dest.Data, opt => opt.MapFrom(src => $"{src.Data.ToString("dd/MM/yyyy")}"))
                    .ForPath(dest => dest.Hora, opt => opt.MapFrom(src => $"{src.Hora.ToString("HH:mm")}"));
                config.CreateMap<IList<AgendamentoResponseModel>, AgendamentosOkResponseModel>()
                    .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src));
                config.CreateMap<Agendamento, AgendamentoOkResponseModel>()
                    .ForPath(dest => dest.Result.Endereco, opt => opt.MapFrom(src => src.Endereco))
                    .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src));
            });
        }
    }
}
