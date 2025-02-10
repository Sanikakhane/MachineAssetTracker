using MachineAssetTracker.Interfaces;
using MachineAssetTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MachineAssetTracker.Controllers
{
    [Route("api/machines")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineService;
        public MachineController(IMachineService machineService)
        {
            _machineService = machineService;
        }

        /// <summary>
        /// Get all machines
        /// </summary>
        /// <returns>Returns a list of machines</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            var machines = _machineService.GetAll();
            if (machines.Count == 0 || machines == null)
            {
                return NotFound();
            }
            return Ok(_machineService.GetAll());
        }

        /// <summary>
        /// Insert a new machine
        /// </summary>
        /// <param name="machineAsset"></param>
        /// <returns>Returns Created Status</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult InsertMachine([FromBody] Machine machineAsset)
        {
            _machineService.InsertMachine(machineAsset);
            return Ok();
        }

        /// <summary>
        /// Update machine details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="machineAsset"></param>
        /// <returns>Updated Machine object</returns>
        [HttpPatch ("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateMachineDetails(string id,[FromBody] Machine machineAsset)
        {
            var machine = _machineService.GetMachineById(id);
            if (machine == null)
            {
                return NotFound();
            }
            _machineService.UpdateMachineDetails(id,machineAsset);
            return Ok();
        }

        /// <summary>
        /// Delete a machine
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Machine ID</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteMachine(string id)
        {
            var machine = _machineService.GetMachineById(id);
            if (machine == null)
            {
                return NotFound();
            }
            _machineService.DeleteMachine(id);
            return Ok();
        }

        /// <summary>
        /// Get machine by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the machine with given ID</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMachineById(string id)
        {
            return Ok(_machineService.GetMachineById(id));
        }
    }
}
