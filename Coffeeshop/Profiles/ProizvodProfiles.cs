using AutoMapper;
using Coffeeshop.Models;
using Coffeeshop.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Profiles
{
    public class ProizvodProfiles : Profile
    {

        public ProizvodProfiles()
        {
            CreateMap<Proizvod, Proizvod>();

            CreateMap<ProizvodConfirmation, Proizvod>();
            CreateMap<Proizvod, ProizvodConfirmation>();
            CreateMap<ProizvodCreation, ProizvodConfirmation>();
            CreateMap<ProizvodConfirmation, ProizvodCreation>();
            CreateMap<ProizvodCreation, Proizvod>();
            CreateMap<Proizvod, ProizvodCreation>();
            CreateMap<ProizvodDto, Proizvod>();
            CreateMap<ProizvodDto, ProizvodDto>();
            CreateMap<ProizvodDto, ProizvodConfirmation>();
            CreateMap<ProizvodConfirmation, ProizvodDto>();
            CreateMap<ProizvodDto, ProizvodCreation>();
            CreateMap<ProizvodCreation, ProizvodDto>();
            CreateMap<Proizvod, ProizvodDto>()
                 .ForMember(
              dest => dest.Tip,
              opt => opt.MapFrom(src => $"{src.IdTipNavigation.Tip}"));




        }
    }
}
