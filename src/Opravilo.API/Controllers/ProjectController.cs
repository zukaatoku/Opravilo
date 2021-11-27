using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Opravilo.API.Extensions;
using Opravilo.API.Models.Responses.Project;
using Opravilo.Application.Interfaces.Services;
using Opravilo.Application.Models.Requests;
using UpdateProjectRequest = Opravilo.API.Models.Requests.UpdateProjectRequest;

namespace Opravilo.API.Controllers
{
    [Authorize]
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet]
        public List<ProjectResponse> GetProjects()
        {
            var userId = this.GetUserId();
            return _projectService.GetProjects(userId).Select(_mapper.Map<ProjectResponse>).ToList();
        }

        [HttpGet("{projectId:long}")]
        public FullProjectResponse GetProject(long projectId)
        {
            // todo: check rights
            return _mapper.Map<FullProjectResponse>(_projectService.GetProject(projectId));
        }

        [HttpPost]
        public ProjectResponse CreateProject(
            [FromBody] CreateProjectRequest request)
        {
            var userId = this.GetUserId();
            return _mapper.Map<ProjectResponse>(_projectService.CreateProject(request, userId));
        }
        
        [HttpDelete("{id:long}")]
        public void DeleteProject(long id)
        {
            _projectService.RemoveProject(id);
        }

        [HttpPatch("{id:long}")]
        public void UpdateProject(long id, [FromBody] UpdateProjectRequest request)
        {
            // todo: return
            _projectService.UpdateProject(new Application.Models.Requests.UpdateProjectRequest()
            {
                ProjectId = id,
                Name = request.Name,
                Description = request.Description
            });
        }
    }
}