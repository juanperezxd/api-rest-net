﻿
using System.Data;
using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;
using MusicStore.Persistence;
using MusicStore.Repositories.Abstractions;

namespace MusicStore.Repositories.Implementations
{
    public class SaleRepository : RepositoryBase<Sale>, ISaleRepository
    {
        public SaleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task CreateTransactionAsync()
        {
            await context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        }

        public async Task RollBackAsync()
        {
            await context.Database.RollbackTransactionAsync();
        }

        public override async Task<int> AddAsync(Sale entity)
        {
            entity.SaleDate = DateTime.Now;

            var nextNumber = await context.Set<Sale>().CountAsync() + 1;
            entity.OperationNumber = $"{nextNumber:000000}";
            //add entity to context

            await context.AddAsync(entity);

            return entity.Id;
        }

        public override async Task UpdateAsync()
        {
            await context.Database.CommitTransactionAsync();
            await base.UpdateAsync();
        }

        public override async Task<Sale?> GetAsync(int id)
        {
            return await context.Set<Sale>()
                .Include(x => x.Customer)
                .Include(x => x.Concert)
                .ThenInclude(x => x.Genre)
                .Where(x => x.Id == id)
                .AsNoTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(); 
        }
    }
}
