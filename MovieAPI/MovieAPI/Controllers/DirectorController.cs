using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {

        private readonly DirectorService _directorService;

        public DirectorController(DirectorService directorService)
        {
            _directorService = directorService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var directors = await _directorService.GetDirectorsAsync();
                return Ok(directors);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
