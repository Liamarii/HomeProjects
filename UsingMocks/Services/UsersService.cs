using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsingMocks.Data;
using UsingMocks.Services;

namespace UsingMocks.Models
{
    public class UsersService : IUsersService
    {
        private readonly IUsersDb _usersDb;

        public UsersService(IUsersDb usersDb)
        {
            _usersDb = usersDb;
        }

        public async Task<string> GreetUser(User user)
        {
            ArgumentNullException.ThrowIfNull(user.Forename, nameof(user.Forename));
            ArgumentNullException.ThrowIfNull(user.Surname, nameof(user.Surname));

            try
            {
                IList<User> existingUsers = await _usersDb.GetUser(user.Surname, user.Forename);
                if (existingUsers.Count > 0)
                {
                    return $"Welcome back {user.Forename} {user.Surname}";
                }
                    return $"Welcome {user.Forename} {user.Surname}";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
