using PMS.Application.Services.Roles;
using PMS.Data.IRepositories.SystemRoles;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data.Entities;

namespace PMS.Application.Implementations.Roles
{
    public class FunctionService : IFunctionService
    {
        private readonly IFunctionRepository functionRepository;

        public FunctionService(IFunctionRepository functionRepository)
        {
            this.functionRepository = functionRepository;
        }

        public List<Function> GetAll()
        {
            return functionRepository.FindAll().ToList();
        }
    }
}
