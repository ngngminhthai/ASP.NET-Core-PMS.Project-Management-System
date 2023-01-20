using AutoMapper;
using WebApplication1.Data.Entities;
using WebApplication1.Models;

namespace WebApplication1.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Product, ProductViewModel>();
        }
    }
}
