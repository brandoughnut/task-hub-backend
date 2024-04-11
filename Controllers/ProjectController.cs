using Microsoft.AspNetCore.Mvc;
using task_hub_backend.Models;
using task_hub_backend.Services;

namespace task_hub_backend.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _data;

        public ProjectController(ProjectService data)
        {
            _data = data;
        }

        [HttpPost]
        [Route("CreateProject")]
        public bool CreateProject(ProjectModel newProject)
        {
            return _data.CreateProject(newProject);
        }

        // [HttpPost]
        // [Route("AddUserToProjectByUserId")]
        // public bool AddUserToProjectByUserId(RelationModel addUser)
        // {
        //     return _data.AddUserToProjectByUserId(addUser);
        // }

        [HttpGet]
        [Route("GetAllProjects")]
        public IEnumerable<ProjectModel> GetAllProjects()
        {
            return _data.GetAllProjects();
        }

        [HttpGet]
        [Route("GetAllRelations")]
        public IEnumerable<RelationModel> GetAllRelations()
        {
            return _data.GetAllRelations();
        }

        [HttpDelete]
        [Route("DeleteProject")]
        public bool DeleteProject(ProjectModel projectDelete)
        {
            return _data.DeleteProject(projectDelete);
        }
    }
