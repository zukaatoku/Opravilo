using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Opravilo.API.Extensions;
using Opravilo.API.Models.Requests;
using Opravilo.Application.Interfaces.Services;
using Opravilo.Application.Models.Project;
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

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public List<ProjectModel> GetProjects()
        {
            var userId = this.GetUserId();
            return _projectService.GetProjects(userId);
        }

        [HttpPost]
        public ProjectModel CreateProject(
            [FromBody] CreateProjectRequest request)
        {
            var userId = this.GetUserId();
            return _projectService.CreateProject(request, userId);
        }
        
        [HttpDelete("{id:long}")]
        public void DeleteProject(long id)
        {
            _projectService.RemoveProject(id);
        }

        [HttpPatch("{id:long}")]
        public void UpdateProject(long id, [FromBody] UpdateProjectRequest request)
        {
            _projectService.UpdateProject(new Application.Models.Requests.UpdateProjectRequest()
            {
                ProjectId = id,
                Name = request.Name,
                Description = request.Description
            });
        }
    }
}