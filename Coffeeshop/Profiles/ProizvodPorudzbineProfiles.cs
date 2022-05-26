using AutoMapper;
using Coffeeshop.Models;
using Coffeeshop.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Profiles
{
    public class ProizvodPorudzbineProfiles :Profile
    {

        public ProizvodPorudzbineProfiles()
        {
            CreateMap<ProizvodPorudzbine, ProizvodPorudzbine>();
            CreateMap<ProizvodPorudzbine, ProizvodPorudzbineConfirmation>();
            CreateMap<ProizvodPorudzbineConfirmation, ProizvodPorudzbine>();
            CreateMap<ProizvodPorudzbineConfirmation, ProizvodPorudzbineCreation>();
            CreateMap<ProizvodPorudzbineCreation, ProizvodPorudzbineConfirmation>();
            CreateMap<ProizvodPorudzbine, ProizvodPorudzbineCreation>();
            CreateMap<ProizvodPorudzbineCreation, ProizvodPorudzbine>();
            CreateMap<ProizvodPorudzbine, ProizvodPorudzbineDto>()
                .ForMember(
                dest => dest.Proizvod,
                opt => opt.MapFrom(src => $"{src.IdProizvodNavigation.Naziv}"));
            CreateMap<ProizvodPorudzbineDto, ProizvodPorudzbine>();
                



        }
    }
}
