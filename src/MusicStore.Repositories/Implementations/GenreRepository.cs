using Microsoft.EntityFrameworkCore;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Persistence;
using MusicStore.Repositories.Abstractions;
using MusicStore.Repositories.Implementations;


namespace MusicStore.Repositories;

public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
{
    public GenreRepository(ApplicationDbContext context) : base(context)
    {
    }
}

