using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MachineAssetTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataLoaderController : ControllerBase
    {
        public DataLoaderController() { }
        [HttpGet("dataloader-error")]
        public IActionResult GetDataLoaderError()
        {
            if (string.IsNullOrEmpty(DataLoader.DataLoadError))
            {
                return Ok(new { message = "Data loaded successfully." });
            }
            return BadRequest(new { error = DataLoader.DataLoadError });
        }

    }
}
