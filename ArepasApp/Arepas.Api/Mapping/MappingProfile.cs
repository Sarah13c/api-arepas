using Arepas.Api.Dtos;
using Arepas.Domain.Models;
using AutoMapper;

namespace Arepas.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerDto, Customer>()
                .ReverseMap()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<Product, ProductDto>()
                .ReverseMap();

            CreateMap<Order, OrderDto>()
                .ReverseMap();

            CreateMap<OrderDetail, OrderDetailDto>()
                .ReverseMap();

            CreateMap<CustomerOrder, CustomerOrderDto>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders));

            CreateMap<OrderOrderDetail, OrderOrderDetailDto>()
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.DetailProducts, opt => opt.MapFrom(src => src.DetailProducts));

            CreateMap<OrderDetailProduct, OrderDetailProductDto>()
                .ReverseMap();
        }
    }
}
