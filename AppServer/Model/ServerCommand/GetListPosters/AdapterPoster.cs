using MainServer.Model.ActionsDb.TableDb;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainServer.Model.ServerCommand.GetListPosters
{
    public class AdapterPoster : Poster
    {
        public AdapterPoster(Film film)
        {
            Id = film.Id;
            Name = film.Name;
            Genres = new List<string>();
            ConvertImageToByte(film.Photo);
            ConvertGenres(film.Genres);
        }

        private void ConvertImageToByte(string path)
        {
            Photo = File.ReadAllBytes(path);
        }

        private void ConvertGenres(List<Genre> genres)
        {
            if (genres != null)
                foreach (var item in genres)
                    Genres.Add(item.Name.ToString());
            else
                Genres = new List<string>();
        }
    }
}
