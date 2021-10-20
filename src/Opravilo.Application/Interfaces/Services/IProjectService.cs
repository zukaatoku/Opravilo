using System.Collections.Generic;
using Opravilo.Application.Models.Project;

namespace Opravilo.Application.Interfaces.Services
{
    public interface IProjectService
    {
        List<ProjectModel> GetProjects(long userId);
    }
}