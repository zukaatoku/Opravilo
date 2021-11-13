using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Opravilo.API.Models.Requests;
using Opravilo.Application.Interfaces.Services;
using Opravilo.Application.Models.Project;

namespace Opravilo.API.Controllers
{
    [Authorize]
    [Route("api/projects/{projectId:long}/states")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public StateController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public StateModel CreateState(
            long projectId, 
            [FromBody] CreateStateRequest request)
        {
            var state = _projectService.CreateState(new Application.Models.Requests.CreateStateRequest()
            {
                Name = request.Name,
                ProjectId = projectId
            });

            return state;
        }

        [HttpPatch("{stateId:long}")]
        public StateModel UpdateState(
            long projectId,
            long stateId,
            [FromBody] UpdateStateRequest request)
        {
            var state = _projectService.UpdateState(new Application.Models.Requests.UpdateStateRequest()
            {
                StateId = stateId,
                Name = request.Name,
                ProjectId = projectId
            });
            return state;
        }

        [HttpDelete("{stateId:long}")]
        public void RemoveState(long projectId, long stateId)
        {
            _projectService.RemoveState(stateId);
        }
    }
}