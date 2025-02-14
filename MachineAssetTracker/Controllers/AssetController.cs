using MachineAssetTracker.Interfaces;
using MachineAssetTracker.Models;
using MachineAssetTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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
            var assets = _assetService.GetAll();
            if( assets == null || assets.Count == 0 )
            {
                return NotFound("No data found");
            }
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
            if (asset == null)
            {
                return BadRequest("Request body cannot be empty");
            }
            else if (asset.Series == null || !asset.Series.Any()) 
            {
                return BadRequest("At least one series must be provided." );
            }
            else if (!ModelState.IsValid) // Check if model validation fails
            {
                return BadRequest("Invalid format");
            }
            asset.AssetName = asset.AssetName.ToLower();
            return Ok(_assetService.InsertAsset(asset));
        }

        /// <summary>
        /// Update asset details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asset"></param>
        /// <returns>Updated status</returns>
        [HttpPatch ("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateAssetDetails(string id, [FromBody] Asset asset)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return BadRequest("Invalid Id format. Id must be a 24-character hex string.");
            }
            var assetData = _assetService.GetAssetById(id);
            if (assetData == null)
            {
                return NotFound("Invalid Id");
            }
            asset.AssetName = asset.AssetName.ToLower();
            _assetService.UpdateAssetDetails(id, asset);
            return Ok("Object updated succesfully");
        }

        /// <summary>
        /// Delete an asset
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Asset ID</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteAsset(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return BadRequest("Invalid Id format. Id must be a 24-character hex string.");
            }
            var assetData = _assetService.GetAssetById(id);
            if (assetData == null)
            {
                return NotFound("Id not found");
            }
            _assetService.DeleteAsset(id);
            return Ok($"Object deleted succesfully  {id}");
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
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return BadRequest("Invalid Id format. Id must be a 24-character hex string.");
            }
            var exisingAsset = _assetService.GetAssetById(id);
            if (exisingAsset == null)
            {
                return NotFound("Id not found");
            }
            return Ok(_assetService.GetAssetById(id));
        }
    }
}
