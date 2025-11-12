using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BookManager.DataAccessLayer
{
    /// <summary>
    /// Контекст базы данных для работы с книгами через Entity Framework
    /// Представляет сессию с базой данных и позволяет работать с сущностями
    /// </summary>
    public class BookContext : DbContext
    {
        /// <summary>
        /// Набор данных для работы с книгами в базе данных
        /// Соответствует таблице Books в SQL Server
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// Настраивает подключение к базе данных SQL Server
        /// </summary>
        /// <param name="optionsBuilder">Построитель опций для настройки подключения</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(AppConfig.ConnectionString);
            }
        }
    }
}
