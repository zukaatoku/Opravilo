using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
                Id = user.Id,
                DisplayName = user.DisplayName,
            };
        }

        public UserDto FindUser(string vkId)
        {
            var user = _context.Users.FirstOrDefault(u => u.VkUserId == vkId);

            if (user == null)
            {
                return null;
            }

            return new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
            };
        }

        public UserDto AddUser(string login, string displayName, string passwordHash)
        {
            var user = CreateUser(login, displayName, passwordHash);

            _context.Users.Add(user);
            _context.SaveChanges();

            return new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
            };
        }

        public UserDto AddVkUser(string firstName, string surname, string vkId)
        {
            var user = CreateUser(Guid.NewGuid().ToString(), $"{firstName} {surname}", null);

            user.VkUserId = vkId;
            _context.Users.Add(user);
            _context.SaveChanges();
            
            return new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
            };
        }

        private UserModel CreateUser(string login, string displayName, string passwordHash)
        {
            var now = DateTime.Now;
            var user = new UserModel()
            {
                Login = login,
                PasswordHash = passwordHash,
                DisplayName = displayName,
                ChangedDate = now,
                CreatedDate = now
            };
            return user;
        }

        public bool LoginAvailable(string login)
        {
            return !_context.Users
                .Any(u => u.Login == login);
        }
    }
}