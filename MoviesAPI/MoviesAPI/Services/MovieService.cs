using Microsoft.EntityFrameworkCore;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;
using System;

namespace MoviesAPI.Services
{
    public class MovieService: IMovie
    {
        private readonly MoviesDbContext _context;

        public MovieService(MoviesDbContext context)
        {
            _context = context;
        }
        public async Task<List<Movie>> GetMoviesAsync()
        {
            try
            {
                return await _context.Movies
                                     .Include(m => m.FkdirectorNavigation)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving movies: {ex.Message}");
            }
        }

        public async Task<Movie?> GetMoviesByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid movie ID.");

                return await _context.Movies
                                     .Include(m => m.FkdirectorNavigation)
                                     .FirstOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving movie by ID: {ex.Message}");
            }
        }

        public async Task<Movie> CreateMovieAsync(Movie movie)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(movie.Name))
                    throw new ArgumentException("Movie name is required.");

                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
                return movie;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating movie: {ex.Message}");
            }
        }

        public async Task<Movie?> UpdateMovieAsync(Movie movie)
        {
            try
            {
                if (movie.Id <= 0)
                    throw new ArgumentException("Invalid movie ID.");
                //movie.FkdirectorNavigation = null;
                _context.Entry(movie).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return movie;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating movie: {ex.Message}");
            }
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid movie ID.");

                var movie = await _context.Movies.FindAsync(id);
                if (movie == null)
                    throw new KeyNotFoundException("Movie not found.");

                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting movie: {ex.Message}");
            }
        }
    }
}
