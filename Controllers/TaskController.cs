using Microsoft.AspNetCore.Mvc;
using task_hub_backend.Models;
using task_hub_backend.Services;

namespace task_hub_backend.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _data;
        public TaskController(TaskService data)
        {
            _data = data;
        }

        [HttpPost]
        [Route("CreateTask")]
        public bool CreateTask(TaskModel newTask)
        {
            return _data.CreateTask(newTask);
        }

        [HttpGet]
        [Route("GetAllTasks")]
        public IEnumerable<TaskModel> GetAllTasks()
        {
            return _data.GetAllTasks();
        }

        [HttpGet]
        [Route("GetTasksByProjectID/{projectID}")]
        public IEnumerable<TaskModel> GetTasksByProjectID(int projectID)
        {
            return _data.GetTasksByProjectID(projectID);
        }

        [HttpPut]
        [Route("EditTask")]
        public bool EditTask(TaskModel taskToUpdate)
        {
            return _data.EditTask(taskToUpdate);
        }

        [HttpGet]
        [Route("GetTasksByStatus/{status}/{projectID}")]
        public IEnumerable<TaskModel> GetTasksByStatus(string status, int projectID)
        {
            return _data.GetTasksByStatus(status, projectID);
        }

        [HttpDelete]
        [Route("DeleteTask/{taskID}")]
        public bool DeleteTask(int taskID)
        {
            return _data.DeleteTask(taskID);
        }
    }