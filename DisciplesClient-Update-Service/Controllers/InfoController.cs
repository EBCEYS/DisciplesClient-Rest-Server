using Disciples2.Info.InfoAdapter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.ComponentModel.DataAnnotations;

namespace DisciplesClient_Update_Service.Controllers
{
    /// <summary>
    /// The info controller.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class InfoController : ControllerBase
    {
        private readonly Logger logger;
        private readonly IInfoAdapter infoAdapter;
        /// <summary>
        /// Initiates the new instance of <see cref="InfoController"/>.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="infoAdapter">The info adapter.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public InfoController(Logger logger, IInfoAdapter infoAdapter)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.infoAdapter = infoAdapter ?? throw new ArgumentNullException(nameof(infoAdapter));
        }
        /// <summary>
        /// Gets the info.
        /// </summary>
        /// <response code="200">The info string.</response>
        /// <response code="503">Try again latter!</response>
        [HttpGet("info")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(503)]
        public ActionResult<string> GetInfo()
        {
            string info = infoAdapter.InfoCache;
            if (string.IsNullOrWhiteSpace(info))
            {
                logger.Error("Error on geting info! Info is null or empty!");
                return StatusCode(503);
            }
            return Ok(info);
        }
        /// <summary>
        /// Sets the new info.
        /// Requires the admin role!
        /// </summary>
        /// <param name="newInfo">The new info.</param>
        /// <response code="200">Info updated.</response>
        /// <response code="400">The new info is null or white space.</response>
        /// <response code="503">Try again latter!</response>
        [HttpPost("info")]
        [Authorize(Roles = "adm, admin, administrator")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(string), 503)]
        public async Task<ActionResult> SetInfoAsync([Required]string newInfo)
        {
            if (string.IsNullOrWhiteSpace(newInfo))
            {
                return BadRequest();
            }
            try
            {
                await infoAdapter.SetNewInfoAsync(newInfo);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on setting new info!");
                return StatusCode(503, ex.Message);
            }

        }
    }
}
