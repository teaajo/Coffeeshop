﻿using AutoMapper;
using Coffeeshop.Models;
using Coffeeshop.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Data
{
    public class PorudzbinaRepository : IPorudzbinaRepository
    {
        private readonly it70g2018Context context;
        private readonly IMapper mapper;
       

        public PorudzbinaRepository(it70g2018Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            

        }
        public PorudzbineConfirmation CreatePorudzbina(Porudzbine porudzbinaModel, List<ProizvodConfirmation> proizvodi)
        {
            porudzbinaModel.Iznos = 0;
            porudzbinaModel.Datum = DateTime.Now;
            porudzbinaModel.Status = "Aktivan";
 
            foreach(var a in proizvodi)
            {
                porudzbinaModel.Iznos += a.Cena;

            }
            if(porudzbinaModel.Kupon!= null)
            {
                porudzbinaModel.Iznos = porudzbinaModel.Iznos - 100;
            }

            var createdEntity = context.Porudzbines.Add(porudzbinaModel);
         
            


            SaveChanges();
            return mapper.Map<PorudzbineConfirmation>(createdEntity.Entity);
        }

        public void DeletePorudzbina(int porudzbinaId)
        {
            var porudzbine = GetById(porudzbinaId);
            context.Porudzbines.Remove(porudzbine);

            SaveChanges();
        }

        public Porudzbine GetById(int porudzbinaId)
        {
            return context.Porudzbines.FirstOrDefault(e => e.Id == porudzbinaId);
        }
        public Porudzbine GetHighId()
        {
            var a = context.Porudzbines.ToList();

            return a.Last(); 

        }

        public List<Porudzbine> GetPorudzbine()
        {
 
            return context.Porudzbines.ToList();
        }
        public List<PorudzbineConfirmation> GetPorudzbinaByKorisnikId(int Korisnikid)
        {
              return mapper.Map <List < PorudzbineConfirmation >>( context.Porudzbines.Where(e => e.IdKorisnik == Korisnikid).ToList());
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
