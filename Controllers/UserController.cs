using Microsoft.AspNetCore.Mvc;
using task_hub_backend.Models;
using task_hub_backend.Models.DTO;
using task_hub_backend.Services;

namespace task_hub_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _data;

    public UserController(UserService data)
    {
        _data = data;
    }

    [HttpPost]
    [Route("Login")]
    public IActionResult Login([FromBody] LoginDTO User)
    {
        return _data.Login(User);
    }

    [HttpPost]
    [Route("AddUser")]
    public bool AddUser(CreateAccountDTO UserToAdd)
    {
        return _data.AddUser(UserToAdd);
    }

    [HttpPut]
    [Route("UpdateUser")]
    public bool UpdateUser(UserModel userToUpdate)
    {
        return _data.UpdateUser(userToUpdate);
    }

    [HttpPut]
    [Route("UpdateUser/{id}/{username}")]
    public bool UpdateUsername(int id, string username)
    {
        return _data.UpdateUsername(id, username);
    }

    [HttpDelete]
    [Route("DeleteUser/{userToDelete}")]
    public bool DeleteUser(string userToDelete)
    {
        return _data.DeleteUser(userToDelete);
    }
    [HttpGet]
    [Route("GetUserByUsername/{username}")]
    public UserIdDTO GetUserByUsername(string username)
    {
        return _data.GetUserIdDTOByUsername(username);
    }

    [HttpPut]
    [Route("UpdateUserInfo")]
    public IActionResult UpdateUserInfo(UserModel updateUser)
    {

        return _data.UpdateUserInfo(updateUser);
    }

    [HttpGet]
    [Route("GetProfileByUsername/{username}")]
    public IEnumerable<UserModel> GetProfileByUsername(string username)
    {
        return _data.GetProfileByUsername(username);
    }
    [HttpGet]
    [Route("GetProfileByUserID/{id}")]
    public UserModel GetProfileByUserID(int id)
    {
        return _data.GetProfileByUserID(id);
    }

    [HttpPut]
    [Route("ForgotPassword/{username}/{newPassword}")]
    public bool ForgotPassword(string username, string newPassword){
        return _data.ForgotPassword(username, newPassword);
    }

}
