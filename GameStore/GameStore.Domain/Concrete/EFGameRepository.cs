﻿using System.Collections.Generic;
using GameStore.Domain.Entities;
using GameStore.Domain.Abstract;

namespace GameStore.Domain.Concrete
{
    public class EFGameRepository : IGameRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Game> Games
        {
            get { return this.context.Games; }
        }

        public void SaveGame(Game game)
        {
            if (game.GameId == 0)
                this.context.Games.Add(game);
            else
            {
                Game dbEntry = this.context.Games.Find(game.GameId);
                if (dbEntry != null)
                {
                    dbEntry.Name = game.Name;
                    dbEntry.Description = game.Description;
                    dbEntry.Price = game.Price;
                    dbEntry.Category = game.Category;
                    dbEntry.ImageData = game.ImageData;
                    dbEntry.ImageMimeType = game.ImageMimeType;
                }
            }
            this.context.SaveChanges();
        }

        public Game DeleteGame(int gameId)
        {
            Game dbEntry = this.context.Games.Find(gameId);
            if (dbEntry != null)
            {
                this.context.Games.Remove(dbEntry);
                this.context.SaveChanges();
            }
            return dbEntry;
        }
    }
}