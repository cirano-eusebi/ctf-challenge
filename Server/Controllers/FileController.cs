// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;

namespace CFTChallenge.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> DownloadFile([FromQuery] string path)
        {
            path = Path.GetFullPath(path);
            var stream = await System.IO.File.ReadAllBytesAsync(path);

            if (stream == null) return NotFound();

            return File(stream, "application/octet-stream");
        }
    }
}
