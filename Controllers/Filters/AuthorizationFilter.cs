using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Exam_System.Controllers.Filters
{
    public class AuthorizationFilter : ActionFilterAttribute
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {

                context.Result = new RedirectToActionResult("AccessError", "Account", null);


            }
        }
    }
}
