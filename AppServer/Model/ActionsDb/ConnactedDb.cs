using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainServer.Model.ActionsDb
{
    public static class ConnactedDb
    {
        public static ModelDb ContextModelDb { get; private set; } // Context
        public static int CountFilms { get; private set; } // Количество фильмов
        public static int CountGenre { get; private set; } // Количество жанров

        static ConnactedDb()
        {
            CountFilms = 0;
            CountGenre = 0;
        }

        public static void Connact()
        {
            ContextModelDb = new ModelDb();
            CountFilms = ContextModelDb.Films.Count();
            CountGenre = ContextModelDb.Genres.Count();
        }
    }
}
