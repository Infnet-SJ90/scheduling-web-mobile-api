using SchedulingWebMobileApi.Domain;
using SchedulingWebMobileApi.Models.Models.Request;
using SchedulingWebMobileApi.Models.Models.Response.Scheduling;
using SchedulingWebMobileApi.Models.Models.Response.Authentication;
using SchedulingWebMobileApi.Models.Models.Response.Citezen;
using SchedulingWebMobileApi.Models.Models.Response.Address;
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
                config.CreateMap<CitezenRequestModel, Citezen>();
                config.CreateMap<AddressRequestModel, Address>();
                config.CreateMap<SchedulingRequestModel, Scheduling>()
                    .ForPath(dest => dest.Address.AddressKey, opt => opt.MapFrom(src => src.AddressKey));

                config.CreateMap<Guid, AuthenticationOkResponseModel>()
                    .ForPath(dest => dest.Result.Token, opt => opt.MapFrom(src => src));

                config.CreateMap<Address, AddressOkResponseModel>()
                    .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src));
                config.CreateMap<Address, AddressResponseModel>();
                config.CreateMap<IList<AddressResponseModel>, AddressesOkResponseModel>()
                    .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src));

                config.CreateMap<Citezen, CitezenOkResponseModel>()
                    .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src));

                config.CreateMap<Scheduling, SchedulingResponseModel>()
                    .ForPath(dest => dest.Data, opt => opt.MapFrom(src => $"{src.Data.ToString("dd/MM/yyyy")}"))
                    .ForPath(dest => dest.Hora, opt => opt.MapFrom(src => $"{src.Hora.ToString("HH:mm")}"));
                config.CreateMap<IList<SchedulingResponseModel>, SchedulingsOkResponseModel>()
                    .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src));
                config.CreateMap<Scheduling, SchedulingOkResponseModel>()
                    .ForPath(dest => dest.Result.Address, opt => opt.MapFrom(src => src.Address))
                    .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src));
            });
        }
    }
}
