using Coffeeshop.Models;
using Coffeeshop.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Data
{
    public interface IProizvodPorudzbinaRepository
    {


        List<ProizvodPorudzbineDto> GetProizvodPorudzbina();
        ProizvodPorudzbine GetById(int porudzbinaId);


        void CreateProizvodPorudzbina(List<ProizvodConfirmation> proizvodi, int porudzbinaId);

        void DeleteProizvodPorudzbina(int porudzbinaId);

        List<ProizvodPorudzbineDto> GetProizvodByPorudzbina(int porudzbinaId);



        bool SaveChanges();
    }
}
