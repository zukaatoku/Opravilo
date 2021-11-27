using AutoMapper;
using Opravilo.API.Models.Responses.Card;
using Opravilo.Application.Models.Project;

namespace Opravilo.API.MappingProfiles
{
    public class CardMappingProfiles : Profile
    {
        public CardMappingProfiles()
        {
            CreateMap<CardModel, CardResponse>();
        }
    }
}