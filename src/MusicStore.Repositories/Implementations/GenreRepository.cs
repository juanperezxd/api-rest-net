using Microsoft.EntityFrameworkCore;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Persistence;
using MusicStore.Repositories.Abstractions;


namespace MusicStore.Repositories;

public class GenreRepository : IGenreRepository
{

    private readonly ApplicationDbContext _context;

    public GenreRepository(ApplicationDbContext context)
    {
        this._context = context;
    }

    public async Task<List<GenreResponseDto>> GetAsync()
    {
        var items = await _context.Genres.ToListAsync();

        //Mapping
        
        var genresResponseDto = items.Select(x => new GenreResponseDto()
        {
            Id = x.Id,
            Name = x.Name,
            Status = x.Status
        }).ToList();

        return genresResponseDto;
    }

    public async Task<GenreResponseDto?> GetAsync(int id)
    {
        var item = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);

        var genreResponseDto = new GenreResponseDto();
        if (item != null)
        {
            genreResponseDto.Id = item.Id;
            genreResponseDto.Name = item.Name;
            genreResponseDto.Status = item.Status;
            return genreResponseDto;
        }
        else
        {
            throw new InvalidOperationException($"No se encontró el genero con Id {id}");
        }
    }
    public async Task<int> AddAsync(GenreRequestDto genreRequestDto)
    {
        //Mapping

        var genre = new Genre()
        {
            Name = genreRequestDto.Name,
            Status = genreRequestDto.Status,
        };

        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();

        return genre.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);

        if (item != null)
        {
            _context?.Genres.Remove(item);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException($"No se encontró el genero con Id {id}");
        }
    }

    public async Task UpdateAsync(int id, GenreRequestDto genreRequestDto)
    {

        var item = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
        if (item != null)
        {
            item.Name = genreRequestDto.Name;
            item.Status = genreRequestDto.Status;

            _context.Genres.Update(item);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException($"No se encontró el genero con Id {id}");
        }
    }

}

