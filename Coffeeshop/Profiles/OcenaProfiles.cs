using AutoMapper;
using Coffeeshop.Models;
using Coffeeshop.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Profiles
{
    public class OcenaProfiles : Profile
    {
        public OcenaProfiles()
        {
            CreateMap<Ocena, Ocena>();
            CreateMap<Ocena, OcenaCreation>();
            CreateMap<OcenaCreation, Ocena>();
            CreateMap<OcenaConfirmation, OcenaCreation>();
            CreateMap<OcenaCreation, OcenaConfirmation>();
            CreateMap<OcenaConfirmation, Ocena>();
            CreateMap<Ocena, OcenaConfirmation>();
            CreateMap<OcenaConfirmation, OcenaConfirmation>();
        }
    }
}
