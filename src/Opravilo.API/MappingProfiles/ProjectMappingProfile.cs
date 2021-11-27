using AutoMapper;
using Opravilo.API.Models.Responses.Project;
using Opravilo.Application.Models.Project;

namespace Opravilo.API.MappingProfiles
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<ProjectModel, ProjectResponse>();
            CreateMap<FullProjectModel, FullProjectResponse>();
        }
    }
}