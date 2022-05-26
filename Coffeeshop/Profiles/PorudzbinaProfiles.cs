using AutoMapper;
using Coffeeshop.Models;
using Coffeeshop.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Profiles
{
    public class PorudzbinaProfiles : Profile
    {
        public PorudzbinaProfiles()
        {
            CreateMap<Porudzbine, Porudzbine>();
            CreateMap<Porudzbine, PorudzbineConfirmation>();
            CreateMap<PorudzbineConfirmation, Porudzbine>();
            CreateMap<Porudzbine, PorudzbineCreation>();
            CreateMap<PorudzbineCreation, Porudzbine>();
            CreateMap<PorudzbineCreation, PorudzbineConfirmation>();
            CreateMap<PorudzbineConfirmation, PorudzbineCreation>();
        }
    }
}
