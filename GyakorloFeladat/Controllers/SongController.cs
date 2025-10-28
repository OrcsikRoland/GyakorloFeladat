using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace GyakorloFeladat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongController : ControllerBase
    {
        private readonly IRepository<Song> _repository;

        public SongController(IRepository<Song> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<List<Song>> GetAll()
        {
            var existingSongs = await _repository.GetAll().ToListAsync();

            if (!existingSongs.Any())
            {
                var defaultSongs = new List<Song>
        {
            new Song("Queen", "Bohemian Rhapsody", 555, 12),
            new Song("The Beatles", "Hey Jude", 711, 12),
            new Song("Led Zeppelin", "Stairway to Heaven", 802, 12),
            new Song("Michael Jackson", "Billie Jean", 454, 42),
            new Song("Nirvana", "Smells Like Teen Spirit", 501, 2)
        };

                foreach (var song in defaultSongs)
                {
                    await _repository.Create(song);
                }
            }

            return await _repository.GetAll().ToListAsync();
        }
    }
}
