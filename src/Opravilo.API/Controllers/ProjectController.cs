using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Opravilo.API.Extensions;
using Opravilo.Application.Interfaces.Services;
using Opravilo.Application.Models.Project;
using Opravilo.Application.Models.Requests;

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
    }
}