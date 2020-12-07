﻿using System.Linq;
using System.Web.Mvc;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;

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
            return this.View(this.repository.Games);
        }

        public ViewResult Edit(int gameId)
        {
            Game game = this.repository.Games
                .FirstOrDefault(g => g.GameId == gameId);
            return this.View(game);
        }

        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult Edit(Game game)
        {
            if (this.ModelState.IsValid)
            {
                this.repository.SaveGame(game);
                this.TempData["message"] = string.Format("Изменения в игре \"{0}\" были сохранены", game.Name);
                return this.RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return this.View(game);
            }
        }
    }
}