// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;

namespace CTFChallenge.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RestartController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Restart()
        {
            // Fire and forget a delayed stop of the application
            _ = Task.Run(async () =>
            {
                await Task.Delay(1000);
                Environment.Exit(0);
            });
            return Ok();
        }
    }
}
