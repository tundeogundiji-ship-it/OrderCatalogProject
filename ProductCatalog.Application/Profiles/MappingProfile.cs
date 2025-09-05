using AutoMapper;
using ProductCatalog.Application.Dtos.Authentication;
using ProductCatalog.Application.Dtos.Orders;
using ProductCatalog.Application.Dtos.Products;
using ProductCatalog.Dormain;


namespace ProductCatalog.Application.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //users
            CreateMap<GetUsersDto,User>().ReverseMap(); 
            CreateMap<RegisterUserDto,User>().ReverseMap();
            //product
            CreateMap<Product,CreateProductDto>().ReverseMap();
            CreateMap<Product,UpdateProductDto>().ReverseMap();
            CreateMap<Product,GetProductDto>().ReverseMap();

            //order
            CreateMap<Order,CreateOrderDto>().ReverseMap();
            CreateMap<Order,GetOrderDto>().ReverseMap();
            CreateMap<OrderItem,GetOrderItemDto>().ReverseMap();
            CreateMap<OrderItem,OrderItemDto>().ReverseMap();

        }
    }
}
