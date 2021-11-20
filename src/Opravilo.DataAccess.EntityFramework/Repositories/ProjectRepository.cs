using System;
using System.Collections.Generic;
using System.Linq;
using Opravilo.DataAccess.Dto;
using Opravilo.DataAccess.Dto.Project;
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

        public FullProjectDto GetProject(long projectId)
        {
            var project = _context
                .Projects
                .Where(p => p.Id == projectId)
                .Select(p => new FullProjectDto()
                {
                    Description = p.Description,
                    Id = p.Id,
                    Name = p.Name,
                    Creator = new UserDto()
                    { 
                        Id = p.Creator.Id, 
                        DisplayName = p.Creator.DisplayName,
                    },
                    States = p.States.Select(s => new FullStateDto()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Cards = s.Cards.Select(c => new CardDto()
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Description = c.Description
                        }).ToList()
                    }).ToList()
                })
                .FirstOrDefault();
            return project;
        }

        public List<ProjectDto> GetProjectsByUser(long userId)
        {
            var projects = _context
                .Projects
                .Where(p => p.CreatorId == userId)
                .OrderByDescending(p => p.ChangedDate)
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

        public void UpdateProject(long projectId, string name, string description)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == projectId);
            project.Name = name;
            project.Description = description;
            project.ChangedDate = DateTime.Now;
            _context.Update(project);
            _context.SaveChanges();
        }
    }
}