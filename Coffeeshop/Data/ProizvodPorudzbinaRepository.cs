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
    public class ProizvodPorudzbinaRepository : IProizvodPorudzbinaRepository
    {
        private readonly it70g2018Context context;
        private readonly IMapper mapper;

        public ProizvodPorudzbinaRepository(it70g2018Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }

        public void CreateProizvodPorudzbina(List<ProizvodConfirmation> proizvodi, int porudzbinaId)
        {
            foreach(ProizvodConfirmation proizvod in proizvodi)
            {
                context.ProizvodPorudzbines.Add(new ProizvodPorudzbine() {IdProizvod=proizvod.Id, IdPorudzbine=porudzbinaId });
            }

            //var createdEntity = context.ProizvodPorudzbines.Add(proizvodPorudzbine);

            SaveChanges();
           // return mapper.Map<ProizvodPorudzbineConfirmation>(createdEntity.Entity);
        }

        
        public void DeleteProizvodPorudzbina(int porudzbinaId)
        {
            var porudzbineProizvodi = GetById(porudzbinaId);
            
            context.ProizvodPorudzbines.Remove(porudzbineProizvodi);

            SaveChanges();
        }

        public ProizvodPorudzbine GetById(int porudzbinaId)
        {
            return context.ProizvodPorudzbines.FirstOrDefault(e => e.Id == porudzbinaId );
        }

        public List<ProizvodPorudzbineDto> GetProizvodPorudzbina()
        {
           
            return mapper.Map<List<ProizvodPorudzbineDto>>(context.ProizvodPorudzbines.Include(t => t.IdProizvodNavigation).ToList());
        }
        public List<ProizvodPorudzbineDto> GetProizvodByPorudzbina(int id)
        {

            return mapper.Map<List<ProizvodPorudzbineDto>>(context.ProizvodPorudzbines.Where(k => k.IdPorudzbine == id).Include(t => t.IdProizvodNavigation).ToList());
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        
    }
}
