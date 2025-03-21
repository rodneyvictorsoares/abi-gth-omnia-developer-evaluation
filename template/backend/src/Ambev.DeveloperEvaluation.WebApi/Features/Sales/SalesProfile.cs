using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleItems;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    public class SalesProfile : Profile
    {
        public SalesProfile()
        {
            CreateMap<CreateSaleRequest, CreateSaleCommand>();
            CreateMap<CreateSaleItemRequest, CreateSaleItemDto>();
            CreateMap<CreateSaleResult, CreateSaleResponse>();

            CreateMap<GetSaleResult, GetSaleResponse>();
                

            CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
            CreateMap<UpdateSaleResult, UpdateSaleResponse>();

            CreateMap<CancelSaleResult, CancelSaleResponse>();

            CreateMap<DeleteSaleResult, DeleteSaleResponse>();

            CreateMap<CancelSaleItemResult, CancelSaleItemResponse>();

            // Mapping for GetSaleItems
            CreateMap<GetSaleItemsResult, GetSaleItemsResponse>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<Application.Sales.GetSaleItems.GetSaleItemDto, GetSaleItemResponse>();
        }
    }
}
