
using Coffeeshop.Models;
using Coffeeshop.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Data
{
    public interface IKorisnikSistemaRepository
    {
        List<KorisnikDto> GetKorisnik();
        KorisnikSistema GetKorisnikById(int id);

        KorisnikConfirmation CreateKorisnik(KorisnikSistema korisnikModel);

        void DeleteKorisnik(int korisnikID);
        List<KorisnikDto> GetKorisnikByTip(string tip);



        bool SaveChanges();
        

    }
}
