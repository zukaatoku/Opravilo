using System.Collections.Generic;
using Opravilo.Application.Models.Project;
using Opravilo.Application.Models.Requests;

namespace Opravilo.Application.Interfaces.Services
{
    public interface IProjectService
    {
        ProjectModel GetProject(long projectId);
        List<ProjectModel> GetProjects(long userId);
        ProjectModel CreateProject(CreateProjectRequest request, long userId);
        void RemoveProject(long projectId);
        void UpdateProject(UpdateProjectRequest request);
    }
}