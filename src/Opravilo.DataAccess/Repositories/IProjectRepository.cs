using System.Collections.Generic;
using Opravilo.DataAccess.Dto;

namespace Opravilo.DataAccess.Repositories
{
    public interface IProjectRepository
    {
        List<ProjectDto> GetProjectsByUser(long userId);
        ProjectDto CreateProject(string name, string description, long userId);
        void RemoveProject(long projectId);
        void UpdateProject(long projectId, string name, string description);
    }
}