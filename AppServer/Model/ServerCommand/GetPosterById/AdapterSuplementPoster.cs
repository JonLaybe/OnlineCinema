using MainServer.Model.ActionsDb;
using MainServer.Model.ActionsDb.TableDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MainServer.Model.ServerCommand.GetPosterById
{
    public class AdapterSuplementPoster : SuplementPoster
    {
        public AdapterSuplementPoster(Film film)
        {
            Id = film.Id;
            Price = film.Price;
            Year = film.Year;
            Timing = film.Timing;
            Description = film.Description;
        }
    }
}
