using PMS.Application.ViewModels;
using System.Collections.Generic;
using WebApplication1.RequestHelpers;

namespace PMS.Application.Services
{
    public interface ITagService
    {
        void Add(TagViewModel tag);
        PagedList<TagViewModel> GetAllWithPagination(string keyword, int page, int pageSize);
        List<TagViewModel> GetAll();
        void Update(TagViewModel tag);
        //void Delete(int id);
        TagViewModel GetById(int id);
        void Save();
    }
}
