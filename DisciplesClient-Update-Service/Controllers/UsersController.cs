using Disciples2ApiModels.ApiModels;
using Disciples2ClientDataBaseModels.DBModels;
using DisciplesClient_Update_Service.LogicLayer.UsersLogic.Exceptions;
using DisciplesClient_Update_Service.LogicLayer.UsersLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace DisciplesClient_Update_Service.Controllers
{
    /// <summary>
    /// The auth controller.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("user")]
    public class UsersController : ControllerBase
    {
        private readonly Logger logger;
        private readonly IUsersDataServer dataServer;
        /// <summary>
        /// The users controller ctor.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dataServer">The data server.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UsersController(Logger logger, IUsersDataServer dataServer)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dataServer = dataServer ?? throw new ArgumentNullException(nameof(dataServer));
        }

        /// <summary>
        /// Checks the user is authorized.
        /// </summary>
        /// <response code="200">Authorized.</response>
        /// <response code="401">Unauthorized.</response>
        [HttpGet("isauthorized")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthorizedInfo), 200)]
        [ProducesResponseType(401)]
        public IActionResult IsAuthorized()
        {
            try
            {
                string name = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                string[] roles = User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToArray();
                int id = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => int.Parse(x.Value)).FirstOrDefault();

                return Ok(new AuthorizedInfo()
                {
                    Id = id,
                    Name = name,
                    Roles = roles
                });
            }
            catch
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// The login request.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Successfully logined.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Something wrong!</response>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<string>> Login([Required][FromBody]LoginModel loginModel)
        {
            logger.Debug("Login request: {username}", loginModel.UserName);
            try
            {
                string token = await dataServer.Login(loginModel);
                return Ok(token);
            }
            catch(UserNotFoundException)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Unexpected exception!");
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Logouts authorized user.
        /// </summary>
        /// <response code="200">Successfully logout.</response>
        /// <response code="400">Can not find user id in token.</response>
        /// <response code="401">Unathorized.</response>
        /// <response code="500">Something wrong!</response>
        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult Logout()
        {
            logger.Debug("Logout request");
            try
            {
                if (!int.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value, out int id))
                {
                    throw new Exception("Can not parse user id!");
                }
                if (dataServer.Logout(id))
                {
                    return Ok();
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error on logouting user!");
                return BadRequest();
            }
        }
        /// <summary>
        /// The login request.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Successfully created.</response>
        /// <response code="400">User already exists.</response>
        /// <response code="401">Unathorized.</response>
        /// <response code="403">You have wrong role.</response>
        /// <response code="500">Something wrong!</response>
        [HttpPost("create")]
        [Authorize(Roles = "adm, admin, administrator")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CreateUser([Required][FromBody]CreateUserModel createUserModel)
        {
            logger.Debug("Create user request: {username}", createUserModel.UserName);
            try
            {
                await dataServer.CreateUserAsync(createUserModel);
                return Ok();
            }
            catch(UserAlreadyExistsException)
            {
                return BadRequest();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Unexpected exception!");
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Gets the user by id.
        /// </summary>
        /// <response code="200">Successfully found user.</response>
        /// <response code="401">Unathorized.</response>
        /// <response code="403">You have wrong role.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Something wrong!</response>
        [HttpGet("by-id")]
        [Authorize(Roles = "adm, admin, administrator")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<User>> GetUserById([Required][FromQuery]int id)
        {
            logger.Debug("GetUserById request: {id}", id);
            try
            {
                User result = await dataServer.GetUserByIdAsync(id);
                return Ok(result);
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Unexpected exception!");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deletes the user by id.
        /// </summary>
        /// <response code="200">Successfully delete user.</response>
        /// <response code="401">Unathorized.</response>
        /// <response code="403">You have wrong role.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Something wrong!</response>
        [HttpDelete("by-id")]
        [Authorize(Roles = "adm, admin, administrator")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteUserById([Required][FromQuery] int id)
        {
            logger.Debug("DeleteUserById request: {id}", id);
            try
            {
                await dataServer.DeleteUserById(id);
                return Ok();
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Unexpected exception!");
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Changes the user password.
        /// </summary>
        /// <param name="model">The change password model.</param>
        /// <response code="200">Successfully changed.</response>
        /// <response code="401">Unathorized.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Something wrong!</response>
        [Authorize]
        [HttpPost("change/password")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> ChangePassword([Required][FromBody] ChangePasswordModel model)
        {
            try
            {
                int userId = int.Parse(User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);
                logger.Debug("Change password: {userId}", userId);
                await dataServer.ChangePasswordAsync(userId, model);
                return Ok();
            }
            catch(UserNotFoundException) 
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error on changing password!");
                return BadRequest();
            }
        }
        /// <summary>
        /// Changes the user Email.
        /// </summary>
        /// <param name="model">The change Email model.</param>
        /// <response code="200">Successfully changed.</response>
        /// <response code="401">Unathorized.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Something wrong!</response>
        [Authorize]
        [HttpPost("change/email")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> ChangeEmail([Required][FromBody] ChangeEmailModel model)
        {
            try
            {
                int userId = int.Parse(User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);
                logger.Debug("Change Email: {userId}", userId);
                await dataServer.ChangeEmailAsync(userId, model);
                return Ok();
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on changing Email!");
                return BadRequest();
            }
        }
        /// <summary>
        /// Changes the user username.
        /// </summary>
        /// <param name="model">The change username model.</param>
        /// <response code="200">Successfully changed.</response>
        /// <response code="401">Unathorized.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Something wrong!</response>
        [Authorize]
        [HttpPost("change/username")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> ChangeUserName([Required][FromBody] ChangeUserNameModel model)
        {
            try
            {
                int userId = int.Parse(User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);
                logger.Debug("Change UserName: {userId}", userId);
                await dataServer.ChangeUserNameAsync(userId, model);
                return Ok();
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on changing UserName!");
                return BadRequest();
            }
        }
    }
}