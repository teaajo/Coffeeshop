
using Coffeeshop.Models;
using Coffeeshop.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Data
{
   public interface ITipProizvodaRepository
    {
        List<TipDto> GetTip();
        TipProizvodum GetById(int tipId);


        TipDto CreateTip(TipProizvodum tipModel);

        void DeleteTip(int tipId);
       


        bool SaveChanges();
        

    }
}
