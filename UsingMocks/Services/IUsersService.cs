using System.Threading.Tasks;
using UsingMocks.Models;

namespace UsingMocks.Services
{
    public interface IUsersService
    {
        public Task<string> GreetUser(User user);
    }
}
