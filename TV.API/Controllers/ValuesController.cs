using System.Threading.Tasks;
using TV.DAL;
using Microsoft.AspNetCore.Mvc;
using TV.SER.Interfaces;
using Microsoft.AspNetCore.Http;
using TV.API.ViewModels;

namespace TV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDbContex _context;
        private readonly ICloudStorage _storage;

        public ValuesController(ApplicationDbContex context, ICloudStorage storage)
        {
            _storage = storage;
            _context = context;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IFormFile file)
        {
            await _storage.UploadAsync(file);
            return Ok();
        }

        // POST api/values
        [HttpPost("deleteimage")]
        public async Task<IActionResult> Delete(DeleteImageViewModel model)
        {
            await _storage.DeleteImage(model.Url);
            return Ok();
        }
    }
}
