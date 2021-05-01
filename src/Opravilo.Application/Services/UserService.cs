using System;
using System.Collections.Generic;
using Opravilo.Application.Interfaces.Services;
using Opravilo.Application.Models.User;
using Opravilo.DataAccess.Repositories;

namespace Opravilo.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserModel FindUser(string vkId)
        {
            var user = _userRepository.FindUser(vkId);

            if (user == null)
            {
                return null;
            }

            return new UserModel()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
            };
        }

        public UserModel FindUser(string login, string password)
        {
            var user = _userRepository.FindUser(login, password);

            if (user == null)
            {
                return null;
            }

            return new UserModel()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
            };
        }

        public RegistrationResultModel RegisterUser(string login, string displayName, string passwordHash)
        {
            var canCreateUser = _userRepository.LoginAvailable(login);

            if (!canCreateUser)
            {
                return new RegistrationResultModel()
                {
                    Errors = new List<string>()
                    {
                        "Invalid credentials!"
                    }
                };
            }
            
            var user = _userRepository.AddUser(login, displayName, passwordHash);

            return new RegistrationResultModel()
            {
                CreatedUser = new UserModel()
                {
                    Id = user.Id,
                    DisplayName = user.DisplayName,
                } 
            };
        }

        public void SaveRefreshToken(long userId, string refreshToken, DateTime expirationTime)
        {
            _userRepository.SaveRefreshToken(userId, refreshToken, expirationTime);
        }

        public RefreshTokenModel FindToken(long userId)
        {
            var token = _userRepository.FindRefreshToken(userId);
            if (token == null)
            {
                return null;
            }

            return new RefreshTokenModel()
            {
                RefreshToken = token.RefreshToken,
                ExpirationDate = token.ExpirationDate
            };
        }

        public void CleanRefreshTokens(long userId)
        {
            _userRepository.CleanRefreshTokens(userId);
        }

        public UserModel CreateVkUser(string givenName, string surname, string vkId)
        {
            var user = _userRepository.AddVkUser(givenName, surname, vkId);

            return new UserModel()
            {
                Id = user.Id,
                DisplayName = user.DisplayName
            };
        }
    }
}