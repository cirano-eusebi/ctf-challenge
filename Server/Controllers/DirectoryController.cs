using Microsoft.AspNetCore.Mvc;

namespace CFTChallenge.Server.Controllers;

[ApiController]
[Route("[controller]")]
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
