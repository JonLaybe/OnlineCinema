using MainServer.Model.ActionsDb.TableDb;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainServer.Model.ActionsDb
{
    public class ModelDb : DbContext
    {
        public DbSet<Film> Films { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=GLEB-PC\SQLEXPRESS;Database=FilmsDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
