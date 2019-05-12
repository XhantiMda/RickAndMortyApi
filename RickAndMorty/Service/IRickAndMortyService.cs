using RickAndMorty.Models;
using System.Threading.Tasks;

namespace RickAndMorty.Service
{
    public interface IRickAndMortyService
    {
        Task<Character> GetCharacterByIdAsync(int id);
        Task<Character> GetRandomCharacterAsync();
    }
}
