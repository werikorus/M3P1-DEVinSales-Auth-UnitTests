using System.Collections.Generic;
using System.Linq;
using DevInSales.Models;
using DevInSales.Context;
using System.Diagnostics.CodeAnalysis;

namespace DevInSales.Repositories
{
    [ExcludeFromCodeCoverage]
    public class UserRepository
    {
        private readonly SqlContext _context;

        public UserRepository(SqlContext context)
        {
            _context = context;
        }

        public User VerifyUser(string username, string password)
        {
            var user = _context.User.Where(x => x.UserName.ToLower() == username.ToLower()
                && x.Password == password).FirstOrDefault();

            return user;
        }

        public List<User> GetAllUsers()
        {
            var users = _context.User.ToList();
            return users;
        }
    }
}
