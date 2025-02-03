using MachineAssetTracker.Interfaces;
using MachineAssetTracker.Models;
using MachineAssetTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MachineAssetTracker.Controllers
{
    [Route("api/asset")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;
        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        /// <summary>
        /// Get all assets
        /// </summary>
        /// <returns>List of All assets</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            return Ok(_assetService.GetAll());
        }

        /// <summary>
        /// Insert a new asset
        /// </summary>
        /// <param name="asset"></param>
        /// <returns>Returns created status</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult InsertAsset([FromBody] Asset asset)
        {
            _assetService.InsertAsset(asset);
            return Ok();
        }

        /// <summary>
        /// Update asset details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asset"></param>
        /// <returns>Updated asset object</returns>
        [HttpPatch ("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateAssetDetails(string id, [FromBody] Asset asset)
        {
            _assetService.UpdateAssetDetails(id, asset);
            return Ok();
        }

        /// <summary>
        /// Delete an asset
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Asset ID</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteAsset(string id)
        {
            _assetService.DeleteAsset(id);
            return Ok();
        }

        /// <summary>
        /// Get asset by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Asset object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAssetById(string id)
        {
            return Ok(_assetService.GetAssetById(id));
        }
    }
}
