using AutoMapper;
using WebApplication1.Data.Entities;
using WebApplication1.Models;

namespace WebApplication1.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductViewModel, Product>();
        }
    }
}
