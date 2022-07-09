using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsingMocks.Models;

namespace UsingMocks.Data
{
    public class UsersDb : IUsersDb
    {
        private readonly List<User> _usersDb = new()
        {
            new User(forename: "Scooby", surname:"Doo"),
            new User(forename: "Hank", surname: "Hill"),
            new User(forename: "Bob", surname: "Dugnutt"),
            new User(forename: "Tim", surname: "Taylor"),
            new User(forename: "Bart", surname: "Simpson"),
            new User(forename: "Scrappy", surname: "Doo")
        };

        public async Task<IList<User>> GetUser(string forename, string surname)
        {
            await Task.Delay(2000);
            return _usersDb.Where(x => x.Forename == forename && x.Surname == surname).ToList();
        }
    }
}
