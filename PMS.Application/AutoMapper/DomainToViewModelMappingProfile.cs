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
            CreateMap<ProjectComment, ProjectCommentViewModel>().ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.UserName));
        }
    }
}
