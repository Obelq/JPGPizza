using AutoMapper;
using JPGPizza.Models;
using JPGPizza.MVC.Dtos;
using JPGPizza.MVC.ViewModels.ApplicationUser;

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

                cfg.CreateMap<EditApplicationUserViewModel, ApplicationUser>();
                cfg.CreateMap<ApplicationUser, EditApplicationUserViewModel>();
                cfg.CreateMap<ApplicationUser, DeleteApplicationUserViewModel>();
                cfg.CreateMap<ApplicationUser, ApplicationUserDto>();
            });
        }
    }
}