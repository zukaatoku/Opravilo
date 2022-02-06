using System;
using System.Collections.Generic;
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

        var u = new UserModel()
        {
            ChangedDate = now,
            CreatedDate = now,
            DisplayName = ApiFixture.DefaultUser,
            Login = ApiFixture.DefaultUser,
            PasswordHash = hashedPwd,
        };
        context.Users.Add(u);
        context.SaveChanges();

        var p = new ProjectModel()
        {
            ChangedDate = now,
            CreatedDate = now,
            CreatorId = u.Id,
            Creator = u,
            Description = ApiFixture.DefaultProject,
            Name = ApiFixture.DefaultProject,
            States = new List<StateModel>()
            {
                new StateModel()
                {
                    Name = ApiFixture.DefaultState,
                    ChangedDate = now,
                    CreatedDate = now,
                    Cards = new List<CardModel>()
                    {
                        new CardModel()
                        {
                            ChangedDate = now,
                            CreatedDate = now,
                            Description = ApiFixture.DefaultCard,
                            Name = ApiFixture.DefaultCard
                        }
                    }
                }
            }
        };
        context.Projects.Add(p);
        context.SaveChanges();
    }
}