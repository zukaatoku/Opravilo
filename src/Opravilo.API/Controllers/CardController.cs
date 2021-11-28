using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Opravilo.API.Models.Requests;
using Opravilo.API.Models.Responses.Card;
using Opravilo.Application.Interfaces.Services;

namespace Opravilo.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/projects/{projectId:long}")]
    public class CardController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public CardController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpPost("states/{stateId:long}/cards")]
        public CardResponse CreateCard(
            long stateId,
            long projectId,
            [FromBody] CreateCardRequest request)
        {
            var card = _projectService.CreateCard(new Application.Models.Requests.CreateCardRequest()
            {
                ProjectId = projectId,
                StateId = stateId,
                Description = request.Description,
                Name = request.Name
            });
            return _mapper.Map<CardResponse>(card);
        }
        
        [HttpPatch("cards/{cardId:long}")]
        public CardResponse UpdateCard(
            long cardId,
            long projectId,
            [FromBody] UpdateCardRequest request)
        {
            var card = _projectService.UpdateCard(new Application.Models.Requests.UpdateCardRequest()
            {
                ProjectId = projectId,
                CardId = cardId,
                Description = request.Description,
                Name = request.Name
            });
            return _mapper.Map<CardResponse>(card);
        }

        [HttpPatch("cards/{cardId:long}/state/{stateId:long}")]
        public void ChangeState(long cardId, long stateId)
        {
            _projectService.ChangeCardState(cardId, stateId);
        }

        [HttpDelete("cards/{cardId:long}")]
        public void RemoveCard(long projectId, long cardId)
        {
            _projectService.RemoveCard(cardId);
        }
    }
}