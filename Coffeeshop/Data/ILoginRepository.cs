using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Data
{
    public interface ILoginRepository
    {

        public bool UserWithCredentialsExists(string username, string password);
        public Tuple<string, string> HashPassword(string password);
    }
}
