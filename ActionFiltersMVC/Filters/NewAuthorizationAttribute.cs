using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace ActionFiltersMVC.Filters
{
    public class NewAuthorizationAttribute : ActionFilterAttribute
    {
        public string Role { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string userRole = context.HttpContext.Session.GetString("userRole").ToLower().Trim();

            bool isAuthority = userRole == Role ? true : false;

            if(!isAuthority || string.IsNullOrEmpty(userRole))
            {
                var referer = context.HttpContext.Request.GetTypedHeaders().Referer;

                string controller = "Home";
                string action = "Index";

                try
                {
                    controller = (referer.Segments.Skip(1).Take(1).SingleOrDefault() ?? "Home").Trim('/');
                    action = (referer.Segments.Skip(2).Take(1).SingleOrDefault() ?? "Index").Trim('/');
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine("Yönlendirmede hata yakalandı..!");
                }

                context.HttpContext.Session.SetString("message", "Yetkiniz Bulunmamaktadır..!");

                context.Result = new RedirectToActionResult(action, controller, null);

                /*
                 "??" yanyana iki soru işareti ifadesi kullanıldığında, sol tarafındaki bakılan nesnenin null olması durumunu kontrol eden kısa bir ifadedir.
                 */
            }
        }
    }
}
