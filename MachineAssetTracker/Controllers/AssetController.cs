using MachineAssetTracker.Interfaces;
using MachineAssetTracker.Models;
using MachineAssetTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MachineAssetTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;
        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_assetService.GetAll());
        }
        [HttpPost]
        public IActionResult InsertAsset([FromBody] Asset asset)
        {
            _assetService.InsertAsset(asset);
            return Ok();
        }
        [HttpPatch ("{id}")]
        public IActionResult UpdateAssetDetails(string id, [FromBody] Asset asset)
        {
            _assetService.UpdateAssetDetails(id, asset);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAsset(string id)
        {
            _assetService.DeleteAsset(id);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetAssetById(string id)
        {
            return Ok(_assetService.GetAssetById(id));
        }
    }
}
