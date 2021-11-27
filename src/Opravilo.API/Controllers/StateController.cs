using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Opravilo.API.Models.Requests;
using Opravilo.API.Models.Responses.State;
using Opravilo.Application.Interfaces.Services;
using Opravilo.Application.Models.Project;

namespace Opravilo.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/projects/{projectId:long}/states")]
    public class StateController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public StateController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpPost]
        public StateResponse CreateState(
            long projectId, 
            [FromBody] CreateStateRequest request)
        {
            var state = _projectService.CreateState(new Application.Models.Requests.CreateStateRequest()
            {
                Name = request.Name,
                ProjectId = projectId
            });

            return _mapper.Map<StateResponse>(state);
        }

        [HttpPatch("{stateId:long}")]
        public StateResponse UpdateState(
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
            return _mapper.Map<StateResponse>(state);
        }

        [HttpDelete("{stateId:long}")]
        public void RemoveState(long projectId, long stateId)
        {
            _projectService.RemoveState(stateId);
        }
    }
}