using MoviesAPI.Models;

namespace MoviesAPI.Interfaces
{
    public interface IDirector
    {
        Task<List<Director>> GetDirectorsAsync();
    }
}
