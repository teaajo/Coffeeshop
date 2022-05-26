using Coffeeshop.Models;
using Coffeeshop.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Data
{
   public interface IOcenaRepository
    {

        List<Ocena> GetOcena();
        Ocena GetById(int ocenaId);


        OcenaConfirmation CreateOcena(Ocena ocenaModel);

        void DeleteOcena(int ocenaId);



        bool SaveChanges();
    }
}
