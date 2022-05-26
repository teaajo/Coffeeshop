using AutoMapper;
using Coffeeshop.Models;
using Coffeeshop.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Profiles
{
    public class TipProizvodaProfiles : Profile
    {
        public TipProizvodaProfiles()
        {
            
            CreateMap<TipProizvodum, TipProizvodum>();
            CreateMap<TipProizvodum, TipDto>();
            CreateMap<TipDto, TipProizvodum>();
            CreateMap<TipDto, TipDto>();
        }
    }
}
