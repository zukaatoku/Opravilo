using System.Collections.Generic;
using Opravilo.DataAccess.Dto;

namespace Opravilo.DataAccess.Repositories
{
    public interface IProjectRepository
    {
        List<ProjectDto> GetProjectsByUser(long userId);
    }
}