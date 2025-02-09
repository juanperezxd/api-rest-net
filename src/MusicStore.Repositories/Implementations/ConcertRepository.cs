﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;
using MusicStore.Entities.Info;
using MusicStore.Persistence;
using MusicStore.Repositories.Abstractions;

namespace MusicStore.Repositories.Implementations
{
    public class ConcertRepository : RepositoryBase<Concert>, IConcertRepository
    {
        public ConcertRepository(ApplicationDbContext context) : base(context)
        {
            
        }
        public override async Task<ICollection<Concert>> GetAsync()
        {
            //eager loading approach
            return await context.Set<Concert>()
                .Include(x => x.Genre)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Concert?> GetAsyncById(int id)
        {
            //eager loading approach
            return await context.Set<Concert>()
                .Include(x => x.Genre)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<ConcertInfo>> GetAsync(string? title)
        {
            //eager loading approach optimizado
            //return await context.Set<Concert>()
            //    .Include(x => x.Genre)
            //    .Where(x => x.Title.Contains(title ?? string.Empty))
            //    .AsNoTracking()
            //    .Select(x => new ConcertInfo
            //    {
            //        Id = x.Id,
            //        Title = x.Title,
            //        Description = x.Description,
            //        Place = x.Place,
            //        UnitPrice = x.UnitPrice,
            //        Genre = x.Genre.Name,
            //        GenreId = x.GenreId,
            //        DateEvent = x.DateEvent.ToShortDateString(),
            //        TimeEvent = x.DateEvent.ToShortTimeString(),
            //        ImageUrl = x.ImageUrl,
            //        TicketsQuantity = x.TicketsQuantity,
            //        Finalized = x.Finalized,
            //        Status = x.Status ? "Activo" : "Inactivo"
            //    })
            //    .ToListAsync();

            //lazy loading approach
            return await context.Set<Concert>()
                .Where(x => x.Title.Contains(title ?? string.Empty))
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Select(x => new ConcertInfo
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Place = x.Place,
                    UnitPrice = x.UnitPrice,
                    Genre = x.Genre.Name,
                    GenreId = x.GenreId,
                    DateEvent = x.DateEvent.ToShortDateString(),
                    TimeEvent = x.DateEvent.ToShortTimeString(),
                    ImageUrl = x.ImageUrl,
                    TicketsQuantify = x.TicketsQuantify,
                    Finalized = x.Finalized,
                    Status = x.Status ? "Activo" : "Inactivo"
                })
                .ToListAsync();

            //var query = context.Set<ConcertInfo>().FromSqlRaw("usp_ListConcerts {0}", title ?? string.Empty);
            //return await query.ToListAsync();
        }

        public async Task FinalizeAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity is not null)
            {
                entity.Finalized = true;
                await UpdateAsync();
            }
        }
    }
}
