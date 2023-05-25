using Emedicine.BAL.UserBased;
using Emedicine.DAL.model;


using Microsoft.AspNetCore.Mvc;
namespace Emedicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager md;
        public UserController(IUserManager _md) 
        {
            md= _md;
        }
        [HttpGet]
        [Route("AllUser")]
        public Task<IEnumerable<User>> GetAllUser()
        {
            try
            {
                return md.GetAllUser();

            }
            catch (Exception ex)
            {
                IEnumerable<User> users = new List<User>();
                return (Task<IEnumerable<User>>)users;
            }
        }
        [HttpGet]
        [Route("OnlineUser")]
        public Task<IEnumerable<User>> GetOnlineUser()
        {
            try
            {
                return md.GetOnlineUser();

            }catch(Exception ex)
            {
                IEnumerable<User> users = new List<User>() ;
                return (Task<IEnumerable<User>>)users;
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id) {
            try
            {
                var user=await md.GetUser(id);
                if(user==null) return BadRequest("user id not exists");
                return Ok(user);
            }
            catch (Exception es)
            {
                return BadRequest("something went erong");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            try
            {
                if (user == null)
                    return BadRequest();
                if (await md.AddUser(user))
                {
                    return StatusCode(
                       StatusCodes.Status200OK,
                       "User added successfully");
                } 
                else return StatusCode(StatusCodes.Status406NotAcceptable, "User already exists");
            }
            catch(Exception es)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Something went wrong");
            }
            
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id,[FromBody] User user)
        {
            try
            {
                User oldUser=await md.GetUser(id);
                if (oldUser == null) return NotFound();
                oldUser.FirstName = user.FirstName;
                oldUser.LastName = user.LastName;
                oldUser.Email = user.Email;
                oldUser.Password= user.Password;
                oldUser.Status= user.Status;
                oldUser.Address = user.Address;
                oldUser.Type = user.Type;
                md.UpdateUser(oldUser);
                return Ok("User updated successfully");
            }catch( Exception es)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error updating user");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                User oldUser = await md.GetUser(id);
                if(oldUser==null) return NotFound();
                md.DeleteUser(oldUser);
                return Ok("User removed successfully");
            }
            catch (Exception es)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error updating user");
            }

        }

    }
}
