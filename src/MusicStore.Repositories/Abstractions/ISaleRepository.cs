using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Dto.Request;
using MusicStore.Entities;

namespace MusicStore.Repositories.Abstractions
{
    public interface ISaleRepository : IRepositoryBase<Sale>
    {
        Task CreateTransactionAsync();
        Task RollBackAsync();

        Task<ICollection<Sale>> GetAsync<TKey>(
        Expression<Func<Sale, bool>> predicate,
        Expression<Func<Sale, TKey>> orderBy,
        PaginationDto pagination);
    }
}
