using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFiltersMVC.Filters
{
    public class LoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string userName = context.HttpContext.Session.GetString("userName");

            if(string.IsNullOrEmpty(userName))
            {
                //login olmamış
                //Kullanıcı eğer yetkisi olmayan bir action/controller'ı görüntülemeye çalışırken, login sayfasına yönlendirdiğimiz esnada, geldiğimiz route'u kaybetmemiz için context nesnesinden istek alan path'i bir değişkene kayıt ediyoruz.
                var path = context.HttpContext.Request.Path;

                //domain/Account/Login/link
                context.Result = new RedirectToActionResult("Login", "Account", new { link = path });
            }
        }
    }
}
