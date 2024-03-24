using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCaseToDo.Services.DataBaseService
{
    public class AppDbContext<T>(DbContextOptions<AppDbContext<T>> options) : DbContext(options) where T : class
    {
        public DbSet<T> Entities { get; set; }

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, "YourAppDatabase.db3");
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DatabasePath}");
        }

        public void EnsureDatabaseCreated()
        {
            Database.EnsureCreated();
        }
    }

}
