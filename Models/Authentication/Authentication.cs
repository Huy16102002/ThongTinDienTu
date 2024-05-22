using System.Security.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BTLG06WNC;

public class Authentication : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context){
        if(context.HttpContext.Session.GetString("User") == null){
            context.Result = new RedirectToRouteResult(new RouteValueDictionary{
                { "Controller", "Account" },
                { "Action", "Login" }
            });
        }
    }
}
