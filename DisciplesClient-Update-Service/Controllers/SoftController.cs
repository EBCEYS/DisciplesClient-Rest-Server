using Disciples2ApiModels.D2ApiModels;
using DisciplesClient_Update_Service.LogicLayer.ModsLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace DisciplesClient_Update_Service.Controllers
{
    /// <summary>
    /// The soft controller.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("d2client/soft")]
    public class SoftController : ControllerBase
    {
        private readonly Logger logger;
        private readonly IModsDataServer dataServer;
        /// <summary>
        /// Initiates the soft controller.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="dataServer">The mods data server.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SoftController(Logger logger, IModsDataServer dataServer)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dataServer = dataServer ?? throw new ArgumentNullException(nameof(dataServer));
        }
        /// <summary>
        /// Gets the soft list.
        /// </summary>
        /// <response code="200">Soft list (soft.Name, soft.Version, soft.Author.UserName).</response>
        /// <response code="500">Something wrong!</response>
        [HttpGet("soft-list")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ModInfo[]), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ModInfo[]>> GetSoftList()
        {
            logger.Debug("Get soft list request.");
            try
            {
                ModInfo[] result = await dataServer.GetModsNamesAsync(true);
                return Ok(result);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error on geting soft list!");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Uploads the soft to server.
        /// </summary>
        /// <param name="softName">The soft name.</param>
        /// <param name="version">The soft version.</param>
        /// <param name="mod">The soft archive.</param>
        /// <param name="updateDateTime">The soft update time.</param>
        /// <response code="200">Uploaded successfuly.</response>
        /// <response code="400">Bad request!.</response>
        /// <response code="401">Unathorized.</response>
        /// <response code="403">Wrong user role!</response>
        /// <response code="500">Something wrong!</response>
        [HttpPost("upload")]
        [Authorize(Roles = "adm, admin, administrator, author")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UploadSoft([Required][FromQuery] string softName, [Required][FromQuery] string version, [FromQuery] DateTime? updateDateTime, [Required] IFormFile mod)
        {
            logger.Info("Upload soft: {modName} file: {fileName}", softName, mod.FileName);
            try
            {
                string authorName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                bool result = await dataServer.UploadModAsync(softName, version, authorName, mod, updateDateTime ?? null, true);
                if (result)
                    return Ok();
                return BadRequest();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on uploading mod!");
                return StatusCode(500);
            }
        }
    }
}
