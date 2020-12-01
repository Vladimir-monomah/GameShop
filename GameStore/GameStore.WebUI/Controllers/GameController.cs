using GameStore.Domain.Abstract;
using GameStore.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class GameController : Controller
    {
        private IGameRepository repository;
        public int pageSize = 4;

        public GameController(IGameRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult List(int page=1)
        {
            GamesListViewModel model = new GamesListViewModel
            {
                Games = this.repository.Games
                    .OrderBy(game => game.GameId)
                    .Skip((page - 1) * this.pageSize)
                    .Take(this.pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = this.repository.Games.Count()
                }
            };
            return this.View(model);
        }
    }
}