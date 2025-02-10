
using System.Globalization;
using AutoMapper;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;

namespace MusicStore.Services.Profiles
{
    public class SaleProfile : Profile
    {
        private static readonly CultureInfo cultureInfo = new CultureInfo("es-PE");

        public SaleProfile() {
            CreateMap<SaleRequestDto, Sale>()
                .ForMember(d => d.Quantify, o => o.MapFrom(x => x.TicketsQuantify));

            CreateMap<Sale, SaleResponseDto>()
                .ForMember(d => d.SaleId, o => o.MapFrom(x => x.Id))
                .ForMember(d => d.DateEvent, o => o.MapFrom(x => x.Concert.DateEvent.ToString("D", cultureInfo)))
                .ForMember(d => d.TimeEvent, o => o.MapFrom(x => x.Concert.DateEvent.ToString("T", cultureInfo)))
                .ForMember(d => d.Genre, o => o.MapFrom(x => x.Concert.Genre.Name))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(x => x.Concert.ImageUrl))
                .ForMember(d => d.Title, o => o.MapFrom(x => x.Concert.Title))
                .ForMember(d => d.FullName, o => o.MapFrom(x => x.Customer.FullName))
                .ForMember(d => d.SaleDate, o => o.MapFrom(x => x.SaleDate.ToString("dd/MM/yyyy HH:mm", cultureInfo)));
        }
    }
}
