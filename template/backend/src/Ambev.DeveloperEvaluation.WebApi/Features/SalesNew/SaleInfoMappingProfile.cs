using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.SalesNew;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesNew
{
    /// <summary>
    /// AutoMapper profile for mapping the application GetSaleResult to the new SaleInfoResponse.
    /// </summary>
    public class SaleInfoMappingProfile : Profile
    {
        public SaleInfoMappingProfile()
        {
            CreateMap<GetSaleResult, SaleInfoResponse>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<GetSaleItemDto, SaleItemInfoResponse>();
                
        }
    }
}
