using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Data.Entities;

namespace PMS.Application.Implementations
{
    public class UserService
    {
        private readonly ManageAppDbContext context;

        public UserService(ManageAppDbContext context)
        {
            this.context = context;
        }

        public List<ManageUser> GetAllUsers()
        {
            var users = context.Users.ToList();
            return users;
        }
    }
}
