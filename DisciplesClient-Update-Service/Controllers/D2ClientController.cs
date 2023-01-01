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
    /// The disciples 2 client controller.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("d2client")]
    public class D2ClientController : ControllerBase
    {
        private readonly Logger logger;
        private readonly IModsDataServer dataServer;

        /// <summary>
        /// The ctor for d2 client controller.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dataServer">The mods data server.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public D2ClientController(Logger logger, IModsDataServer dataServer)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dataServer = dataServer ?? throw new ArgumentNullException(nameof(dataServer));
        }

        /// <summary>
        /// Gets the mods list with: mod.Name, mod.Version, mod.Author.UserName
        /// </summary>
        /// <response code="200">Mod list (mod.Name, mod.Version, mod.Author.UserName).</response>
        /// <response code="500">Something wrong!</response>
        /// <returns></returns>
        [HttpGet("mods-list")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ModInfo[]), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ModInfo[]>> GetModsList()
        {
            logger.Info("GetModsList request");
            try
            {
                ModInfo[] mods = await dataServer.GetModsNamesAsync();
                return Ok(mods);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error on geting mods list!");
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Uploads the mod to server.
        /// </summary>
        /// <param name="modName">The mod name.</param>
        /// <param name="version">The mod version.</param>
        /// <param name="mod">The mod archive.</param>
        /// <param name="updateDateTime">The mod update time.</param>
        /// <response code="200">Uploaded successfuly.</response>
        /// <response code="400">Bad request!.</response>
        /// <response code="401">Unathorized.</response>
        /// <response code="403">Wrong user role!</response>
        /// <response code="500">Something wrong!</response>
        [HttpPost("mod/upload")]
        [Authorize(Roles = "adm, admin, administrator, author")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UploadMod([Required][FromQuery] string modName, [Required][FromQuery] string version, [FromQuery] DateTime? updateDateTime, [Required]IFormFile mod)
        {
            logger.Info("Upload mod: {modName} file: {fileName}", modName, mod.FileName);
            try
            {
                string authorName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                bool result = await dataServer.UploadModAsync(modName, version, authorName, mod, updateDateTime ?? null);
                if (result)
                    return Ok();
                return BadRequest();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error on uploading mod!");
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Dowloads the mod archive.
        /// </summary>
        /// <param name="modName">The mod name.</param>
        /// <response code="200">Dowloaded.</response>
        /// <response code="404">File not found.</response>
        /// <response code="500">Something wrong!</response>
        [HttpGet("mod/download")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DownloadMod([Required][FromQuery] string modName)
        {
            logger.Info("Download mod: {modName}", modName);
            try
            {
                Stream file = await dataServer.GetModFile(modName);
                if (file == null)
                {
                    return NotFound();
                }
                return File(file, "application/octet-stream");
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error on dowloading file!");
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Remove the mod file. If file name == last file name mod will be removed from data base.
        /// </summary>
        /// <param name="modName"></param>
        /// <param name="fileName"></param>
        /// <response code="200">Removed.</response>
        /// <response code="404">File not found.</response>
        /// <response code="500">Something wrong!</response>
        [HttpDelete("mod/remove")]
        [Authorize(Roles = "adm, admin, administrator")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> RemoveMod([Required][FromQuery] string modName, [Required][FromQuery] string fileName)
        {
            logger.Info("Remove mod: {modName}", modName);
            try
            {
                bool res = await dataServer.RemoveModAsync(modName, fileName);
                if (res)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error on removing mod!");
                return StatusCode(500);
            }
        }
    }
}
