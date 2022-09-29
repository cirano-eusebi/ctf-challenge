// See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

namespace CTFChallenge.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private static readonly string FLag1of3 = "FLAG 07F2745C-4B7A-48D7-A449-104887961395";


        private string FlagPath { get; init; }

        public FileController(IConfiguration configuration)
        {
            var relativeFlagPath = configuration.GetValue<string>("FlagPath");
            FlagPath = Path.GetFullPath(relativeFlagPath);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadFile([FromQuery] string path, [FromQuery] string? dllPath)
        {
            dllPath = Path.GetFullPath(dllPath ?? "./CTFChallenge.Security.dll");
            path = Path.GetFullPath(path);


            // Checks file existence and not being a directory
            if (!System.IO.File.Exists(path)) return NotFound();

            // Don't return the flag
            if (!AllowDownload(path, dllPath)) return NotFound();

            // Allow to donwload enything else
            var stream = await System.IO.File.ReadAllBytesAsync(path);

            if (stream == null) return NotFound();

            return File(stream, "application/octet-stream");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromQuery] string path, [FromForm] IFormFile file)
        {
            path = Path.GetFullPath(path);
            var filePath = Path.Combine(path, file.FileName);

            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return new CreatedResult(filePath, null);
        }

        private bool AllowDownload(string path, string dllPath)
        {
            var difference = string.Compare(path, FlagPath, StringComparison.OrdinalIgnoreCase);

            // Dynamically load the dll to allow it to change in runtime
            var dll = NativeLibrary.Load(dllPath);

            var method = NativeLibrary.GetExport(dll, nameof(Add));
            var @delegate = Marshal.GetDelegateForFunctionPointer<Add>(method);
            var result = @delegate(difference, 0);

            // Release the handle
            NativeLibrary.Free(dll);

            return result != 0;
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Auto)]
        delegate int Add(int a, int b);

    }

}
