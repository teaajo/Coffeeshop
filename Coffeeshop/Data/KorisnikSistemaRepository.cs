using AutoMapper;
using Coffeeshop.Models;
using Coffeeshop.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Data
{
    public class KorisnikSistemaRepository : IKorisnikSistemaRepository
    {
        
        private readonly it70g2018Context context;
        private readonly IMapper mapper;

        public KorisnikSistemaRepository(it70g2018Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public KorisnikConfirmation CreateKorisnik(KorisnikSistema korisnikModel)
        {
            var createdEntity = context.KorisnikSistemas.Add(korisnikModel);
            SaveChanges();
            return mapper.Map<KorisnikConfirmation>(createdEntity.Entity);

        }

        public void DeleteKorisnik(int id)
        {
            var korisnik = GetKorisnikById(id);
            context.KorisnikSistemas.Remove(korisnik);

            SaveChanges();
        }

        public List<KorisnikDto> GetKorisnik()
        {

            return mapper.Map<List<KorisnikDto>>(context.KorisnikSistemas.ToList());
        }

        public KorisnikSistema GetKorisnikById(int id)
        {
            return context.KorisnikSistemas.FirstOrDefault(e => e.Id == id);
        }

        public List<KorisnikDto> GetKorisnikByTip(string tip)
        {
            return mapper.Map<List<KorisnikDto>>(context.KorisnikSistemas.Where(e => e.Tip == tip).ToList());
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        
    }
}
