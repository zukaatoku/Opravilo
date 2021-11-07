using System.Collections.Generic;
using Opravilo.Application.Models.Project;
using Opravilo.Application.Models.Requests;

namespace Opravilo.Application.Interfaces.Services
{
    public interface IProjectService
    {
        FullProjectModel GetProject(long projectId);
        List<ProjectModel> GetProjects(long userId);
        ProjectModel CreateProject(CreateProjectRequest request, long userId);
        void RemoveProject(long projectId);
        void UpdateProject(UpdateProjectRequest request);
        StateModel CreateState(CreateStateRequest request);
        StateModel UpdateState(UpdateStateRequest request);
        void RemoveState(long stateId);
    }
}