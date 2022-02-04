using System;
using ApiIntegrationTests.Fixture;
using Opravilo.API.Auth;
using Opravilo.DataAccess.EntityFramework;
using Opravilo.DataAccess.EntityFramework.Models;

namespace ApiIntegrationTests.Helpers;

public static class TestDbInitializer
{
    public static void PopulateDb(DataContext context, IPasswordHasher hasher)
    {
        var now = DateTime.Now;
        var hashedPwd = hasher.HashPassword(ApiFixture.DefaultPassword);
        context.Users.Add(new UserModel()
        {
            ChangedDate = now,
            CreatedDate = now,
            DisplayName = ApiFixture.DefaultUser,
            Login = ApiFixture.DefaultUser,
            PasswordHash = hashedPwd,
        });
        context.SaveChanges();
    }
}