using System;
using System.Linq;
using Opravilo.DataAccess.Dto;
using Opravilo.DataAccess.EntityFramework.Models;
using Opravilo.DataAccess.Repositories;

namespace Opravilo.DataAccess.EntityFramework.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public UserDto FindUser(string login, string passwordHash)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == login && 
                                                          u.PasswordHash == passwordHash);

            if (user == null)
            {
                return null;
            }

            return new UserDto()
            {
                Login = user.Login
            };
        }

        public UserDto AddUser(string login, string passwordHash)
        {
            var now = DateTime.Now;
            var user = new UserModel()
            {
                Login = login,
                PasswordHash = passwordHash,
                ChangedDate = now,
                CreatedDate = now
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return new UserDto()
            {
                Login = login
            };
        }
    }
}