using System.Linq;
using System.Web.Mvc;
using GameStore.Domain.Entities;
using GameStore.Domain.Abstract;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        public ViewResult Index(string returnUrl)
        {
            return this.View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        private IGameRepository repository;
        public CartController(IGameRepository repo)
        {
            this.repository = repo;
        }

        public RedirectToRouteResult AddToCart(int gameId, string returnUrl)
        {
            Game game = this.repository.Games
                .FirstOrDefault(g => g.GameId == gameId);

            if (game != null)
            {
                this.GetCart().AddItem(game, 1);
            }
            return this.RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int gameId, string returnUrl)
        {
            Game game = this.repository.Games
                .FirstOrDefault(g => g.GameId == gameId);

            if (game != null)
            {
                this.GetCart().RemoveLine(game);
            }
            return this.RedirectToAction("Index", new { returnUrl });
        }

        public Cart GetCart()
        {
            var cart = (Cart)this.Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                this.Session["Cart"] = cart;
            }
            return cart;
        }
    }
}