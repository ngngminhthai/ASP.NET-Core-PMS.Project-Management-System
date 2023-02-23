using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Data.Entities.ProjectAggregate
{
    public class ProjectRoleManager : RoleManager<ProjectRole>
    {
        private readonly int _projectId;

        public ProjectRoleManager(IRoleStore<ProjectRole> store, IEnumerable<IRoleValidator<ProjectRole>> roleValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, ILogger<RoleManager<ProjectRole>> logger, int projectId)
            : base(store, roleValidators, keyNormalizer, errors, logger)
        {
            _projectId = projectId;
        }

        public async Task<IdentityResult> CreateProjectRoleAsync(ProjectRole role)
        {
            role.ProjectId = _projectId;
            return await base.CreateAsync(role);
        }

        // other methods to manage project roles
    }


}
