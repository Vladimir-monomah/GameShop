using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
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

        public ViewResult List(string category, int page = 1)
        {
            var model = new GamesListViewModel
            {
                Games = this.repository.Games
            .Where(p => category == null || p.Category == category)
            .OrderBy(game => game.GameId)
            .Skip((page - 1) * this.pageSize)
            .Take(this.pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
        this.repository.Games.Count() :
        this.repository.Games.Where(game => game.Category == category).Count()
                },
                CurrentCategory = category
            };
            return this.View(model);
        }

        public FileContentResult GetImage(int gameId)
        {
            Game game = this.repository.Games
                .FirstOrDefault(g => g.GameId == gameId);

            if (game != null)
            {
                return this.File(game.ImageData, game.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}