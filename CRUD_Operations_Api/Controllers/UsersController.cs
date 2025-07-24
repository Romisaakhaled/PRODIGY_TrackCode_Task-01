using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using CRUD_Operations_Api.Models;
namespace CRUD_Operations_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>();

        [HttpGet]
        public ActionResult<List<User>> GetAllUsers() => Ok(users);

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(Guid id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound("User not found.");
            return Ok(user);
        }

        
        [HttpPost]
        public ActionResult<User> CreateUser(User newUser)
        {
            if (!IsValidEmail(newUser.Email))
                return BadRequest("Invalid email address.");

            newUser.Id = Guid.NewGuid();
            users.Add(newUser);
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        // ✅ PUT: api/users/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateUser(Guid id, User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound("User not found.");

            if (!IsValidEmail(updatedUser.Email))
                return BadRequest("Invalid email address.");

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Age = updatedUser.Age;

            return NoContent();
        }

     
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(Guid id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound("User not found.");

            users.Remove(user);
            return NoContent();
        }

        
        private bool IsValidEmail(string email)
        {
            try
            {
                var mail = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

