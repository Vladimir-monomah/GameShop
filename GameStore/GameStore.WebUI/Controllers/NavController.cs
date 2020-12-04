using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GameStore.Domain.Abstract;

namespace GameStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IGameRepository repository;

        public NavController(IGameRepository repo)
        {
            this.repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            this.ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = this.repository.Games
                .Select(game => game.Category)
                .Distinct()
                .OrderBy(x => x);

            return this.PartialView("FlexMenu", categories);
        }
    }
}