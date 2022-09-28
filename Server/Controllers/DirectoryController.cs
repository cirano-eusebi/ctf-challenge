// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;

namespace CTFChallenge.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class DirectoryController : ControllerBase
{
    private readonly ILogger<DirectoryController> _logger;

    public DirectoryController(ILogger<DirectoryController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public Shared.Directory Get([FromQuery] string path)
    {
        return new Shared.Directory(Path.GetFullPath(path));
    }
}
