using System.Web.Mvc;
using GameStore.Domain.Abstract;

namespace GameStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IGameRepository repository;

        public AdminController(IGameRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult Index()
        {
            return View(this.repository.Games);
        }
    }
}