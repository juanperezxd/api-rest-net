
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;

namespace MusicStore.Services.Abstractions
{
    public interface ISaleService
    {
        Task<BaseResponseGeneric<int>> AddAsync(string email, SaleRequestDto request);

        Task<BaseResponseGeneric<SaleResponseDto>> GetAsync(int id);
    }
}
