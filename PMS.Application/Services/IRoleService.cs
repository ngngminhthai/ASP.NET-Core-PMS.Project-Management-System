using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Entities.UserAggregate;

namespace PMS.Application.Services
{
    public interface IRoleService
    {
        public Task<bool> CheckPermission(string functionId, string action, string[] roles);
        public Task<List<AppRole>> GetAllAsync();

        public Task<List<Permission>> GetAllPermission(Guid id);
        Task<AppRole> GetById(Guid id);

        public Task UpdateAsync(AppRole role);

    }
}
