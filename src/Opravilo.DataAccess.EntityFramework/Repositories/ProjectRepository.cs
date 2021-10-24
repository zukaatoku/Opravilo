using System;
using System.Collections.Generic;
using System.Linq;
using Opravilo.DataAccess.Dto;
using Opravilo.DataAccess.EntityFramework.Models;
using Opravilo.DataAccess.Repositories;

namespace Opravilo.DataAccess.EntityFramework.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _context;

        public ProjectRepository(DataContext context)
        {
            _context = context;
        }

        public List<ProjectDto> GetProjectsByUser(long userId)
        {
            var projects = _context
                .Projects
                .Where(p => p.CreatorId == userId)
                .Select(c => new ProjectDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Creator = new UserDto()
                    {
                        Id = c.CreatorId,
                        DisplayName = c.Creator.DisplayName
                    }
                }).ToList();

            return projects;
        }

        public ProjectDto CreateProject(string name, string description, long userId)
        {
            var now = DateTime.Now;
            
            var project = new ProjectModel()
            {
                Name = name,
                Description = description,
                CreatedDate = now,
                ChangedDate = now,
                CreatorId = userId
            };

            _context.Projects.Add(project);
            _context.SaveChanges();

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            return new ProjectDto()
            {
                Id = project.Id,
                Description = project.Description,
                Name = project.Name,
                Creator = new UserDto()
                {
                    Id = user.Id,
                    DisplayName = user.DisplayName,
                }
            };
        }

        public void RemoveProject(long projectId)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == projectId);
            _context.Remove(project);
            _context.SaveChanges();
        }
    }
}