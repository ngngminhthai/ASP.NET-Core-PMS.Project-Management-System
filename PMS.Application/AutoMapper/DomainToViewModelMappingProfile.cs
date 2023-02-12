using AutoMapper;
using PMS.Application.ViewModels;
using PMS.Data.Entities.ProjectAggregate;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Entities.ProjectAggregate;
using WebApplication1.Models;

namespace WebApplication1.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<Project, ProjectViewModel>().ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Creator.Email));

            CreateMap<ProjectUploadedFile, ProjectUploadedFileViewModel>();

            CreateMap<ProjectTask, ProjectTaskViewModel>();
        }
    }
}
