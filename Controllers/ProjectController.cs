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
        public int CreateProject(ProjectModel newProject)
        {
            return _data.CreateProject(newProject);
        }

        [HttpPost]
        [Route("AddUserToProjectByUserId/{userID}/{projectID}")]
        public bool AddUserToProjectByUserId(int userID, int projectID)
        {
            return _data.AddUserToProjectByUserId(userID, projectID);
        }

        [HttpGet]
        [Route("GetAllProjects")]
        public IEnumerable<ProjectModel> GetAllProjects()
        {
            return _data.GetAllProjects();
        }

        [HttpGet]
        [Route("GetProjectByID/{projectID}")]
        public ProjectModel GetProjectByID(int projectID)
        {
            return _data.GetProjectByID(projectID);
        }

        [HttpGet]
        [Route("GetAllRelations")]
        public IEnumerable<RelationModel> GetAllRelations()
        {
            return _data.GetAllRelations();
        }

        [HttpGet]
        [Route("GetAllUsersWithinProject/{projectID}")]
        public IEnumerable<RelationModel> GetAllUsersWithinProject(int projectID)
        {
            return _data.GetAllUsersWithinProject(projectID);
        }

        [HttpGet]
        [Route("GetAllProjectsUserIsIn/{userID}")]
        public IEnumerable<RelationModel> GetAllProjectsUserIsIn(int userID)
        {
            return _data.GetAllProjectsUserIsIn(userID);
        }

        [HttpDelete]
        [Route("DeleteProject/{projectID}")]
        public bool DeleteProject(int projectID)
        {
            return _data.DeleteProject(projectID);
        }

        [HttpDelete]
        [Route("RemoveUserFromProjectByID/{userID}/{projectID}")]
        public bool RemoveUserFromProjectByUserId(int userID, int projectID)
        {
            return _data.RemoveUserFromProjectByUserId(userID, projectID);
        }

    }
