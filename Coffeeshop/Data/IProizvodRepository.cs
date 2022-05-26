
using Coffeeshop.Models;
using Coffeeshop.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Data
{
   public interface IProizvodRepository
    {
        
        List<ProizvodDto> GetProizvod();

        Proizvod GetProizvodById(int proizvodId);

        ProizvodConfirmation CreateProizvod(Proizvod proizvodModel);



        void DeleteProizvod(int proizvodId);
        List<Proizvod> GetProizvodByTip(string tip);

     

        bool SaveChanges();
        
    }
}
