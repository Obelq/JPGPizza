using AutoMapper;
using JPGPizza.Models;
using JPGPizza.MVC.Dtos;

namespace JPGPizza.MVC.Utility
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductByOrderDto>()
                    .ForMember(dest => dest.NumberOfOrders,
                            y => y.ResolveUsing(src => src.OrderProducts.Count));
            });
        }
    }
}