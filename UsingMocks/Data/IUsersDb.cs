using System.Collections.Generic;
using System.Threading.Tasks;
using UsingMocks.Models;

namespace UsingMocks.Data
{
    public interface IUsersDb
    {
        public Task<IList<User>> GetUser(string forename, string surname);
    }
}
