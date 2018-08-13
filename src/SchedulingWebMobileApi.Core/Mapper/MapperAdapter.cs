using SchedulingWebMobileApi.Core.Entities;
using SchedulingWebMobileApi.Models.Models.Response.Authentication;
using SchedulingWebMobileApi.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulingWebMobileApi.Core.Mapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<AuthenticationRequestModel, Authentication>();

                config.CreateMap<Guid, AuthenticationOkResponseModel>()
                    .ForMember(dest => dest.Result.Token, opt => opt.MapFrom(src => src));
            });
        }
    }
}
