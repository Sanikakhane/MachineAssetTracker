using MachineAssetTracker.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MachineAssetTracker.Controllers
{
    [Route("api/machineassets")]
    [ApiController]
    public class MachineAssetsController : ControllerBase
    {
        private readonly IMachineAssetsService _machineAssetsService;
        public MachineAssetsController(IMachineAssetsService machineAssetsService)
        {
            _machineAssetsService = machineAssetsService;
        }

        /// <summary>
        /// Get All Assets
        /// </summary>
        /// <returns>Returns a list</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            return Ok(_machineAssetsService.GetAll());
        }

        /// <summary>
        /// Get Assets By MachineType
        /// </summary>
        /// <param name="machineType"></param>
        /// <returns>List Of Asset for Provided machine type</returns>
        [HttpGet("byMachineType/{machineType}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAssetsByMachineType(string machineType)
        {
            return Ok(_machineAssetsService.GetAssetsByMachineType(machineType));
        }

        /// <summary>
        /// Get Machines By Asset
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns>List Of machines which using given Asset</returns>
        [HttpGet("byAssetName/{assetName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMachinesByAsset(string assetName)
        {
            return Ok(_machineAssetsService.GetMachinesByAsset(assetName));
        }

        /// <summary>
        /// Get Machines Using Latest Series
        /// </summary>
        /// <returns>Returns machine which are using latest assets</returns>
        [HttpGet("GetMachinesUsingLatestSeries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMachinesUsingLatestSeries()
        {
            return Ok(_machineAssetsService.GetMachinesUsingLatestSeries());
        }
    }
}
