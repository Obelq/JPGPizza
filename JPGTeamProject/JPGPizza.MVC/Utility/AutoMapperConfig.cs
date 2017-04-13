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
                cfg.CreateMap<Product, TopSellingProductDto>()
                    .ForMember(dest => dest.TotalOrders,
                            y => y.ResolveUsing(src => src.OrderProducts.Count));
            });
        }
    }
}