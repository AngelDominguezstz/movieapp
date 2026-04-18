using MoviesAPI.Models;

namespace MoviesAPI.Interfaces
{
    public interface IMovie
    {
        Task<List<Movie>> GetMoviesAsync();
        Task<Movie?> GetMoviesByIdAsync(int id);
        Task<Movie> CreateMovieAsync(Movie movie);
        Task<Movie?> UpdateMovieAsync(Movie movie);
        Task<bool> DeleteMovieAsync(int id);
    }
}
