using System.Collections.Generic;
using Opravilo.DataAccess.Dto.Project;

namespace Opravilo.DataAccess.Repositories
{
    public interface IProjectRepository
    {
        FullProjectDto GetProject(long projectId);
        List<ProjectDto> GetProjectsByUser(long userId);
        ProjectDto CreateProject(string name, string description, long userId);
        void RemoveProject(long projectId);
        void UpdateProject(long projectId, string name, string description);
    }
}