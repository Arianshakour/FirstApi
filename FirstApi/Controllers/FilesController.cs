using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace FirstApi.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _file;

        public FilesController(FileExtensionContentTypeProvider file)
        {
            _file = file;
        }

        [HttpGet("{fileId}")]
        public ActionResult getFile(int fileId)
        {
            string pathoffile = "6.rar";
            if (!System.IO.File.Exists(pathoffile))
            {
                return NotFound();
            }
            var bytes = System.IO.File.ReadAllBytes(pathoffile);
            if(!_file.TryGetContentType(pathoffile, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return File(bytes, contentType,Path.GetFileName(pathoffile));
        }
    }
}
