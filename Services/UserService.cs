using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using task_hub_backend.Models;
using task_hub_backend.Models.DTO;
using task_hub_backend.Services.Context;

namespace task_hub_backend.Services;

public class UserService : ControllerBase
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }

    public bool DoesUserExist(string Username)
    {
        return _context.UserInfo.SingleOrDefault(user => user.Username == Username) != null;
    }

    public bool AddUser(CreateAccountDTO UserToAdd)
    {
        bool result = false;

        if (!DoesUserExist(UserToAdd.Username))
        {
            UserModel newUser = new UserModel();

            var hashPassword = HashPassword(UserToAdd.Password);

            newUser.ID = UserToAdd.ID;
            newUser.Username = UserToAdd.Username;
            newUser.Salt = hashPassword.Salt;
            newUser.Hash = hashPassword.Hash;

            _context.Add(newUser);

            result = _context.SaveChanges() != 0;

        }

        return result;
    }

    public PasswordDTO HashPassword(string password)
    {
        PasswordDTO newHashPassword = new PasswordDTO();

        byte[] SaltByte = new byte[64];

        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

        provider.GetNonZeroBytes(SaltByte);

        string salt = Convert.ToBase64String(SaltByte);

        Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltByte, 10000);

        string hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

        newHashPassword.Salt = salt;
        newHashPassword.Hash = hash;

        return newHashPassword;
    }


    public bool VerifyUsersPassword(string? password, string? storedHash, string? storedSalt)
    {
        byte[] SaltBytes = Convert.FromBase64String(storedSalt);

        Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltBytes, 10000);

        string newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

        return newHash == storedHash;
    }

    public IActionResult Login(LoginDTO User)
    {
        IActionResult Result = Unauthorized();

        if (DoesUserExist(User.Username))
        {

            UserModel foundUser = GetUserByUsername(User.Username);

            if (VerifyUsersPassword(User.Password, foundUser.Hash, foundUser.Salt))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));

                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5000",
                    audience: "http://localhost:5000",
                    claims: new List<Claim>(), // Claims can be added here if needed
                    expires: DateTime.Now.AddMinutes(10), // Set token expiration time (e.g., 30 minutes)
                    signingCredentials: signinCredentials // Set signing credentials
                );

                // Generate JWT token as a string
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);


                Result = Ok(new { Token = tokenString });
            }

        }
        return Result;
    }

    public UserModel GetUserByUsername(string username)
    {
        return _context.UserInfo.SingleOrDefault(user => user.Username == username);
    }

    public bool UpdateUser(UserModel userToUpdate)
    {
        _context.Update<UserModel>(userToUpdate);
        return _context.SaveChanges() != 0;
    }

    public bool UpdateUsername(int id, string username)
    {
        UserModel foundUser = GetUserById(id);

        bool result = false;

        if (foundUser != null)
        {
            foundUser.Username = username;
            _context.Update<UserModel>(foundUser);
            result = _context.SaveChanges() != 0;
        }

        return result;
    }

    public UserModel GetUserById(int id)
    {
        return _context.UserInfo.SingleOrDefault(user => user.ID == id);
    }

    public bool DeleteUser(string userToDelete)
    {
        UserModel foundUser = GetUserByUsername(userToDelete);

        bool result = false;

        if (foundUser != null)
        {
            _context.Remove<UserModel>(foundUser);
            result = _context.SaveChanges() != 0;
        }

        return result;
    }
    public UserIdDTO GetUserIdDTOByUsername(string username)
    {

        UserIdDTO UserInfo = new UserIdDTO();
        //query through database to find the user 
        UserModel foundUser = _context.UserInfo.SingleOrDefault(user => user.Username == username);
        UserInfo.UserId = foundUser.ID;

        UserInfo.PublishedName = foundUser.Username;

        return UserInfo;
    }

    public bool UpdateUserInfo(int id, string firstName, string lastName, string contact, string bio, string image)
    {

        UserModel foundUser = GetUserById(id);
        bool result = false;
        if (foundUser != null)
        {
            foundUser.FirstName = firstName;
            foundUser.LastName = lastName;
            foundUser.Contact = contact;
            foundUser.Bio = bio;
            foundUser.Image = image;
            _context.Update<UserModel>(foundUser);
            result = _context.SaveChanges() != 0;
        }
        return result;
    }
    public IEnumerable<UserModel> GetProfileByUsername(string username)
    {
        return _context.UserInfo.Where(item => item.Username == username);
    }
    public UserModel GetProfileByUserID(int id)
    {
        return _context.UserInfo.SingleOrDefault(item => item.ID == id);
    }

    public bool ForgotPassword(string username, string newPassword){

        UserModel foundUser = GetUserByUsername(username);
        bool result = false;

        if (foundUser != null)
        {

            var hashPassword = HashPassword(newPassword);

            foundUser.Salt = hashPassword.Salt;
            foundUser.Hash = hashPassword.Hash;

            _context.Update<UserModel>(foundUser);

            result = _context.SaveChanges() != 0;

        }

        return result;
    }

}
