using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Entities;
using MusicStore.Entities.Info;

namespace MusicStore.Repositories.Abstractions
{
    public interface IConcertRepository : IRepositoryBase<Concert>
    {
        Task<ICollection<ConcertInfo>> GetAsync(string? title);
        Task<Concert?> GetAsyncById(int id);
        Task FinalizeAsync(int id);
    }
}
