using MachineAssetTracker.Interfaces;
using MachineAssetTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MachineAssetTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineService;
        public MachineController(IMachineService machineService)
        {
            _machineService = machineService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_machineService.GetAll());
        }
        [HttpPost]
        public IActionResult InsertMachine([FromBody] Machine machineAsset)
        {
            _machineService.InsertMachine(machineAsset);
            return Ok();
        }
        [HttpPatch ("{id}")]
        public IActionResult UpdateMachineDetails(string id,[FromBody] Machine machineAsset)
        {
            _machineService.UpdateMachineDetails(id,machineAsset);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteMachine(string id)
        {
            _machineService.DeleteMachine(id);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetMachineById(string id)
        {
            return Ok(_machineService.GetMachineById(id));
        }
    }
}
