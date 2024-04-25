using Microsoft.AspNetCore.Mvc;
using task_hub_backend.Models;
using task_hub_backend.Services.Context;

namespace task_hub_backend.Services;

    public class TaskService : ControllerBase
    {
        private readonly DataContext _context;
        public TaskService(DataContext context)
        {
            _context = context;
        }

        public bool CreateTask(TaskModel newTask)
        {
            _context.Add(newTask);

            return _context.SaveChanges() != 0;
        }

        public IEnumerable<TaskModel> GetAllTasks()
        {
            return _context.TaskInfo;
        }

        public IEnumerable<TaskModel> GetTasksByProjectID(int projectID)
        {
            return _context.TaskInfo.Where(project => project.ProjectID == projectID);
        }

        public bool EditTask(TaskModel taskToUpdate)
        {
            _context.Update<TaskModel>(taskToUpdate);
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<TaskModel> GetTasksByStatus(string status, int projectID)
        {
            return _context.TaskInfo.Where(retrieve => retrieve.Status == status && retrieve.ProjectID == projectID);
        }

        public bool DeleteTask(int taskID)
        {
            TaskModel task = _context.TaskInfo.SingleOrDefault(task => task.ID == taskID);
            bool result = false;
            if(task != null){
                _context.Remove<TaskModel>(task);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }
    }
