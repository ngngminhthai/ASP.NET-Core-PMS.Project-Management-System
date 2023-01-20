using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ApiBadRequestResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; }

        public ApiBadRequestResponse(ModelStateDictionary modelState) // chuyền vào modelState , thông thường lỗi from thì chả về
            : base(400) // mặc định chuyền vào mã lỗi của thằng cha là 400
        {
            if (modelState.IsValid)
            {
                throw new ArgumentException("ModelState must be invalid", nameof(modelState));
            }

            Errors = modelState.SelectMany(x => x.Value.Errors)  // từ modelState chúng ta lấy ra được cái Errors
                .Select(x => x.ErrorMessage).ToArray();
        }

        public ApiBadRequestResponse(IdentityResult identityResult)
           : base(400)
        {
            Errors = identityResult.Errors
                .Select(x => x.Code + " - " + x.Description).ToArray();
        }

        public ApiBadRequestResponse(string message) // hoặc chuyền thẳng vào message
           : base(400, message)
        {
        }
    }
}
