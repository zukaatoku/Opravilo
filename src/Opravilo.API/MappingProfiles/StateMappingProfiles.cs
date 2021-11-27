using AutoMapper;
using Opravilo.API.Models.Responses.State;
using Opravilo.Application.Models.Project;

namespace Opravilo.API.MappingProfiles
{
    public class StateMappingProfiles : Profile
    {
        public StateMappingProfiles()
        {
            CreateMap<StateModel, StateResponse>();
            CreateMap<FullStateModel, FullStateResponse>();
        }
    }
}