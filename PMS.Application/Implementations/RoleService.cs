using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Services;
using PMS.Data.IRepositories.SystemRoles;
using PMS.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Entities.UserAggregate;

namespace PMS.Application.Implementations
{
    public class RoleService : IRoleService
    {
        private RoleManager<AppRole> _roleManager;
        private IFunctionRepository _functionRepository;
        private IPermissionRepository _permissionRepository;
        private IUnitOfWork _unitOfWork;
        public RoleService(RoleManager<AppRole> roleManager, IUnitOfWork unitOfWork,
         IFunctionRepository functionRepository, IPermissionRepository permissionRepository)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _functionRepository = functionRepository;
            _permissionRepository = permissionRepository;
        }

        public Task<bool> CheckPermission(string functionId, string action, string[] roles)
        {
            var functions = _functionRepository.FindAll();
            var permissions = _permissionRepository.FindAll();
            var query = from f in functions
                        join p in permissions on f.Id equals p.FunctionId
                        join r in _roleManager.Roles on p.RoleId equals r.Id
                        where roles.Contains(r.Name) && f.Id == functionId
                        && ((p.CanCreate && action == "Create")
                        || (p.CanUpdate && action == "Update")
                        || (p.CanDelete && action == "Delete")
                        || (p.CanRead && action == "Read"))
                        select p;
            return query.AnyAsync();
        }

        public async Task<List<AppRole>> GetAllAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public Task<List<Permission>> GetAllPermission(Guid id)
        {
            return _permissionRepository.FindAll(p => p.RoleId.Equals(id.ToString()), p => p.AppRole).ToListAsync();
        }

        public async Task<AppRole> GetById(Guid id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public async Task UpdateAsync(AppRole roleVm)
        {
            var role = await _roleManager.FindByIdAsync(roleVm.Id.ToString());
            role.Description = roleVm.Description;
            role.Name = roleVm.Name;
            await _roleManager.UpdateAsync(role);
        }
    }
}
