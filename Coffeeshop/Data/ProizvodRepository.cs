using AutoMapper;
using Coffeeshop.Models;
using Coffeeshop.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Data
{
    public class ProizvodRepository : IProizvodRepository
    {
        
        private readonly it70g2018Context context;
        private readonly IMapper mapper;

        public ProizvodRepository(it70g2018Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public ProizvodConfirmation CreateProizvod(Proizvod proizvodModel)
        {
            var createdEntity = context.Proizvods.Add(proizvodModel);

            SaveChanges();
            createdEntity.Reference("IdTipNavigation").Load();
            return mapper.Map<ProizvodConfirmation>(createdEntity.Entity);
        }

        public void DeleteProizvod(int proizvodId)
        {
            var proizvod = GetProizvodById(proizvodId);
            context.Proizvods.Remove(proizvod);

            SaveChanges();
        }

        public List<ProizvodDto> GetProizvod()
        {
            return mapper.Map<List<ProizvodDto>>(context.Proizvods.Include(t => t.IdTipNavigation).ToList());
            //return mapper.Map<List<ProizvodDto>>(context.Proizvods.ToList());
        }

        public Proizvod GetProizvodById(int proizvodId)
        {
            return  context.Proizvods.FirstOrDefault(e => e.Id == proizvodId);
        }

        public List<Proizvod> GetProizvodByTip(string tip)
        {
            return context.Proizvods.Include(t => t.IdTipNavigation).Where(k => k.IdTipNavigation.Tip == tip).ToList();
        }
       

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        
    }
}
