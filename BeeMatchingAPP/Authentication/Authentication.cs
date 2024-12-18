using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BeeMatchingAPP.Models.Authentication
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("UserEmail") == null)
            {
                context.HttpContext.Session.SetString("message", "Bạn cần đăng nhập trước khi sử dụng tính năng này");
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller", "Home" },
                        {"Action","Login" }
                    }
                    );
            }
        }
    }
}