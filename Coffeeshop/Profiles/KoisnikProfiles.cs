using AutoMapper;
using Coffeeshop.Models;
using Coffeeshop.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Profiles
{
    public class KoisnikProfiles : Profile
    {

        public KoisnikProfiles()
        {
            
            CreateMap<KorisnikSistema, KorisnikCreation>();
            CreateMap<KorisnikSistema, KorisnikSistema>();
            CreateMap<KorisnikCreation, KorisnikSistema>();
            CreateMap<KorisnikConfirmation, KorisnikSistema>();
            CreateMap<KorisnikSistema, KorisnikConfirmation>();
            CreateMap<KorisnikSistema, KorisnikDto>();
            CreateMap<KorisnikDto, KorisnikSistema>();
            CreateMap<KorisnikDto, KorisnikConfirmation>();
            CreateMap<KorisnikConfirmation, KorisnikDto>();



        }
    }
}
