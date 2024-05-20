using manage_users.src.models;
using manage_users.src.models.requests;
using manage_users.src.repository;
using manage_users.src.util;
using Microsoft.AspNetCore.Mvc;

namespace manage_users.src.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        IRequestValidator _validator;
        IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository, IRequestValidator requestValidator)
        {
            _validator = requestValidator;
            _usersRepository = usersRepository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_validator.ValidateGetUser(id))
            {
                try
                {
                    User user = await _usersRepository.GetUser(id);
                    return Ok(user);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
            else
            {
                return BadRequest("User ID is required.");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserList), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserList>> GetUsers()
        {
            if (_validator.ValidateGetUsers())
            {
                try
                {
                    UserList userList = await _usersRepository.GetUsers();
                    return Ok(userList);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CreateTask(CreateUser createUserRequest)
        {
            if (_validator.ValidateCreateUser(createUserRequest))
            {
                try
                {
                    _usersRepository.CreateUser(createUserRequest);
                    return Ok("User Created");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
            else
            {
                return BadRequest("CreateUser is required.");
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateUser(UpdateUser updateUserRequest)
        {
            if (_validator.ValidateUpdateUser(updateUserRequest))
            {
                try
                {
                    _usersRepository.UpdateUser(updateUserRequest);
                    return Ok("User Updated");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
            else
            {
                return BadRequest("UpdateUser is required.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteUser(int id, int updateUserId)
        {
            if (_validator.ValidateDeleteUser(id, updateUserId))
            {
                try
                {
                    _usersRepository.DeleteUser(id, updateUserId);
                    return Ok("User Deleted");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
            else
            {
                return BadRequest("id and updateUserId params are required.");
            }
        }
    }
}