using System;
using System.Collections.Generic;
using System.Linq;
using Opravilo.DataAccess.Dto;
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
                    Description = c.Description
                }).ToList();

            return projects;
        }
    }
}