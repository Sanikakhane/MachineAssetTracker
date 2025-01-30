using MachineAssetTracker.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MachineAssetTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineAssetsController : ControllerBase
    {
        private readonly IMachineAssetsService _machineAssetsService;
        public MachineAssetsController(IMachineAssetsService machineAssetsService)
        {
            _machineAssetsService = machineAssetsService;
        }
        

        [HttpGet("GetAssetsByMachineType/{machineType}")]
        public IActionResult GetAssetsByMachineType(string machineType)
        {
            return Ok(_machineAssetsService.GetAssetsByMachineType(machineType));
        }
        [HttpGet("GetMachinesByAsset/{assetName}")]
        public IActionResult GetMachinesByAsset(string assetName)
        {
            return Ok(_machineAssetsService.GetMachinesByAsset(assetName));
        }
        [HttpGet("GetMachinesUsingLatestSeries")]
        public IActionResult GetMachinesUsingLatestSeries()
        {
            return Ok(_machineAssetsService.GetMachinesUsingLatestSeries());
        }
    }
}
