﻿

using System.Linq.Expressions;
using MusicStore.Entities;

namespace MusicStore.Repositories.Abstractions
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        Task<ICollection<TEntity>> GetAsync();
        Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<ICollection<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy );

        Task<int> AddAsync(TEntity entity);
        Task<TEntity?> GetAsync(int id);
        Task UpdateAsync();
        Task DeleteAsync(int id);
    }
}
