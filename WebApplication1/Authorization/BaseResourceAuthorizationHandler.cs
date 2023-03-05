using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Services;
using RBAC.Application.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace RBAC.Authorization
{
    public class BaseResourceAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Payload>
    {


        private readonly IRoleService _roleService;
        private readonly IProjectRoleService projectRoleService;
        private readonly IProjectRole_UserService projectRole_UserService;
        private readonly ManageAppDbContext dbcontext;

        public BaseResourceAuthorizationHandler(IRoleService roleService, IProjectRoleService projectRoleService, IProjectRole_UserService projectRole_UserService, ManageAppDbContext context)
        {
            _roleService = roleService;
            this.projectRoleService = projectRoleService;
            this.projectRole_UserService = projectRole_UserService;
            this.dbcontext = context;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Payload payload)
        {
            if (payload.Resource == "system")
            {
                await CheckSystemPermission(context, requirement, payload);
            }
            else if (payload.Resource == "project")
            {
                //await CheckSystemPermission(context, requirement, payload);
                await CheckProjectPermission(context, requirement, payload);
            }
            else if (payload.Resource == "conversation")
            {
                await CheckConversationPermission(context, requirement, payload);
            }
        }

        private Task CheckConversationPermission(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Payload payload)
        {
            // Get the user ID from the authorization context.
            var email = ((ClaimsIdentity)context.User.Identity).Claims.FirstOrDefault(x => x.Type == "Email").ToString().Split(":")[1].Trim();
            var userId = dbcontext.Users.FirstOrDefault(u => u.Email == email).Id;

            // Check if the user is a member of the conversation.
            var conversation = dbcontext.Conversations
                .Include(c => c.ConversationUser)
                .FirstOrDefault(c => c.Id == payload.ConversationRequirement.ConversationId);
            if (conversation == null)
            {
                return Task.CompletedTask;
            }
            var userIsMember = conversation.ConversationUser.Any(m => m.UserId == userId);

            if (userIsMember)
            {
                // If the user is a member, they can view the conversation.
                if (payload.ConversationRequirement.Action == "View")
                {
                    context.Succeed(requirement);
                }
                // If the user is an admin, they can delete the conversation.
                else if (payload.ConversationRequirement.Action == "Delete" && conversation.AdminId == userId)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }

        private async Task CheckProjectPermission(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Payload payload)
        {
            //get all roles of the current user based on project above
            var email = ((ClaimsIdentity)context.User.Identity).Claims.FirstOrDefault(x => x.Type == "Email").ToString().Split(":")[1].Trim();
            var roles = projectRole_UserService.GetAllProjectRoleUser(email, payload.ProjectRequirement.ProjectId).Select(u => u.ProjectRole.Name).ToArray();
            //var roles = projectRole_UserService
            var result = await projectRoleService.CheckPermission(payload.ProjectRequirement.Resource, payload.ProjectRequirement.Action, roles, payload.ProjectRequirement.ProjectId);
            //find current access project based on id
            if (result)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            //get permissions current user can do with the current resource of the project
        }

        public async Task CheckSystemPermission(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Payload payload)
        {
            var roles = ((ClaimsIdentity)context.User.Identity).Claims.FirstOrDefault(x => x.Type == "Roles");
            if (roles != null)
            {
                var listRole = roles.Value.Split(";");
                var hasPermission = await _roleService.CheckPermission(payload.Resource, requirement.Name, listRole);
                if (hasPermission)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }

            }
            else
            {
                context.Fail();
            }
        }
    }
}
