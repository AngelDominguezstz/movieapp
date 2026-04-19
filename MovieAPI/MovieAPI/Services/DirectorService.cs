using Microsoft.EntityFrameworkCore;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public class DirectorService: IDirector
    {
        private readonly MoviesDbContext _context;

        public DirectorService(MoviesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Director>> GetDirectorsAsync()
        {
            try
            {
                return await _context.Directors.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving directors: {ex.Message}");
            }
        }
    }
}
