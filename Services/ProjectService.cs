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

        public bool SaveChangesToDataBase()
        {
            return _context.SaveChanges() != 0;
        }


        public int CreateProject(ProjectModel newProject)
        {
            newProject.IsDeleted = false;
            _context.Add(newProject);
            
            RelationModel relationModel = new RelationModel();
            
            relationModel.UserID = newProject.UserID;
            relationModel.ProjectID = _context.ProjectInfo.Count() + 1;

            _context.Add(relationModel);

            SaveChangesToDataBase();

            return _context.ProjectInfo.Count();
        }

        public IEnumerable<ProjectModel> GetAllProjects()
        {
            return _context.ProjectInfo;
        }

        public IEnumerable<RelationModel> GetAllRelations()
        {
            return _context.RelationInfo;
        }

        public bool DeleteProject(int projectID)
        {
            ProjectModel projectOwner = GetProjectByID(projectID);
            IEnumerable<RelationModel> usersWithinProject = GetAllUsersWithinProject(projectID);
            bool result = false;
            if(projectOwner != null){
                projectOwner.IsDeleted = true;
                _context.Update<ProjectModel>(projectOwner);
                _context.RemoveRange(usersWithinProject);
                result = _context.SaveChanges() != 0;
            }

            return result;

        }

        public bool RemoveUserFromProjectByUserId(int userID, int projectID)
        {
            RelationModel user = _context.RelationInfo.SingleOrDefault(retrieve => retrieve.UserID == userID && retrieve.ProjectID == projectID);
            bool result = false;
            if(user != null){
                _context.Remove<RelationModel>(user);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public ProjectModel GetProjectByID(int projectID)
        {
            return _context.ProjectInfo.SingleOrDefault(project => project.ID == projectID);
        }

        public IEnumerable<RelationModel> GetAllUsersWithinProject(int projectID)
        {
            return _context.RelationInfo.Where(project => project.ProjectID == projectID);
        }

        public IEnumerable<RelationModel> GetAllProjectsUserIsIn(int userID)
        {
            return _context.RelationInfo.Where(user => user.UserID == userID);
        }

        public bool AddUserToProjectByUserId(int userID, int projectID)
        {
            RelationModel newUser = new RelationModel();
            bool result = false;

            if(!IsUserInProject(userID, projectID) && DoesUserExist(userID)){

            newUser.UserID = userID;
            newUser.ProjectID = projectID;
            
            _context.Add(newUser);

            result = _context.SaveChanges() != 0;
            }

            return result;
        }

        public bool IsUserInProject(int userID, int projectID)
        {
            return _context.RelationInfo.SingleOrDefault(check => check.UserID == userID && check.ProjectID == projectID) != null;
        }

        public bool DoesUserExist(int userID)
        {
            return _context.UserInfo.SingleOrDefault(user => user.ID == userID) != null;
        }

    }
