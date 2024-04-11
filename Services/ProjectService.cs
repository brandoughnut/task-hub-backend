using Microsoft.AspNetCore.Mvc;
using task_hub_backend.Models;
using task_hub_backend.Services.Context;

namespace task_hub_backend.Services;

    public class ProjectService : ControllerBase
    {
        private readonly DataContext _context;
        public ProjectService(DataContext context)
        {
            _context = context;
        }

        public bool CreateProject(ProjectModel newProject)
        {
            _context.Add(newProject);

            return _context.SaveChanges() != 0;
        }

        public IEnumerable<ProjectModel> GetAllProjects()
        {
            return _context.ProjectInfo;
        }

        public IEnumerable<RelationModel> GetAllRelations()
        {
            return _context.RelationInfo;
        }

        public bool DeleteProject(ProjectModel projectDelete)
        {
            projectDelete.IsDeleted = true;
            _context.Update<ProjectModel>(projectDelete);
            return _context.SaveChanges() != 0;
        }

        // public bool AddUserToProjectByUserId(RelationModel addUser)
        // {
            
        // }

        public bool IsUserInProject(int UserID)
        {
            return _context.RelationInfo.SingleOrDefault(user => user.UserID == UserID) != null;
        }

    }
