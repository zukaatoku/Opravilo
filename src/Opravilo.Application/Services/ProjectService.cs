using System.Collections.Generic;
using System.Linq;
using Opravilo.Application.Interfaces.Services;
using Opravilo.Application.Models.Project;
using Opravilo.DataAccess.Repositories;

namespace Opravilo.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public List<ProjectModel> GetProjects(long userId)
        {
            var projects = _projectRepository.GetProjectsByUser(userId);
            return projects.Select(p => new ProjectModel()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            }).ToList();
        }
    }
}