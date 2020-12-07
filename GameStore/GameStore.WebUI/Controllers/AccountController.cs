using System.Web.Mvc;
using GameStore.WebUI.Infrastructure.Abstract;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;
        public AccountController(IAuthProvider auth)
        {
            this.authProvider = auth;
        }

        public ViewResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {

            if (this.ModelState.IsValid)
            {
                if (this.authProvider.Authenticate(model.UserName, model.Password))
                {
                    return this.Redirect(returnUrl ?? this.Url.Action("Index", "Admin"));
                }
                else
                {
                    this.ModelState.AddModelError("", "Неправильный логин или пароль");
                    return this.View();
                }
            }
            else
            {
                return this.View();
            }
        }
    }
}