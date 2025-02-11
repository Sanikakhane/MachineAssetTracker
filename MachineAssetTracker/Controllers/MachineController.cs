using MachineAssetTracker.Interfaces;
using MachineAssetTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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
            if ( machines == null || machines.Count == 0 )
            {
                return NotFound("No data found");
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
            if(machineAsset == null) 
            {
                return BadRequest("Request body cannot be empty");
            }
            else if (!ModelState.IsValid) // Check if model validation fails
            {
                return BadRequest("Invalid format");
            }
            machineAsset.MachineType = machineAsset.MachineType.ToLower();
            
            return Ok(_machineService.InsertMachine(machineAsset));
        }

        /// <summary>
        /// Update machine details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="machineAsset"></param>
        /// <returns>Updated status</returns>
        [HttpPatch ("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateMachineDetails(string id,[FromBody] Machine machineAsset)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return BadRequest("Invalid Id format. Id must be a 24-character hex string.");
            }
            var machine = _machineService.GetMachineById(id);
            if (machine == null)
            {
                return NotFound("Invalid Id");
            }
            machineAsset.MachineType = machineAsset.MachineType.ToLower();
            _machineService.UpdateMachineDetails(id,machineAsset);
            return Ok("Object updated succesfully");
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
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return BadRequest("Invalid Id format. Id must be a 24-character hex string.");
            }
            var machine = _machineService.GetMachineById(id);
            if (machine == null)
            {
                return NotFound("Id not found");
            }
            _machineService.DeleteMachine(id);
            return Ok($"Object Deleted Succesfully {id}");
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
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return BadRequest("Invalid Id format. Id must be a 24-character hex string.");
            }
            var existingMachine = _machineService.GetMachineById(id);
            if (existingMachine == null) 
            {
                return NotFound("Id not found");
            }
            return Ok(_machineService.GetMachineById(id));
        }
    }
}
