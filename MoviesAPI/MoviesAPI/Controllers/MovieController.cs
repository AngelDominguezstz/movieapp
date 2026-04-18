using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;
        
        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var movies = await _movieService.GetMoviesAsync();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var movie = await _movieService.GetMoviesByIdAsync(id);
                if (movie == null) return NotFound(new { message = "Movie not found." });
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movie movie)
        {
            try
            {
                var created = await _movieService.CreateMovieAsync(movie);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Movie movie)
        {
            try
            {
                if (id != movie.Id) return BadRequest(new { message = "Movie ID mismatch." });

                var updated = await _movieService.UpdateMovieAsync(movie);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _movieService.DeleteMovieAsync(id);
                if (!deleted) return NotFound(new { message = "Movie not found." });
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



    }
}
