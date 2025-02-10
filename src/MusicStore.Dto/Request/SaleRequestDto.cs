
namespace MusicStore.Dto.Request
{
    public record SaleRequestDto(int ConcertId, short TicketsQuantify, string Email, string FullName);
}
