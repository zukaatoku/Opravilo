using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Opravilo.API.Models.Requests;
using Opravilo.Application.Interfaces.Services;
using Opravilo.Application.Models.Project;

namespace Opravilo.API.Controllers
{
    [Authorize]
    [Route("api/projects/{projectId:long}/cards")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public CardController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost("{stateId:long}")]
        public CardModel CreateCard(
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
            return card;
        }
        
        [HttpPatch("{cardId:long}")]
        public CardModel UpdateCard(
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
            return card;
        }

        [HttpDelete("{cardId:long}")]
        public void RemoveCard(long projectId, long cardId)
        {
            _projectService.RemoveCard(cardId);
        }
    }
}