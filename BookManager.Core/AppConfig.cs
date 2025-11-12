using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Core
{
    public static class AppConfig
    {
        /// <summary>
        /// Строка подключения к базе данных
        /// Используется как Entity Framework, так и Dapper
        /// </summary>
        public static string ConnectionString { get; } =
            @"Server=GLAUZI\SQLEXPRESS;Database=BookManagerDB;Trusted_Connection=true;TrustServerCertificate=true;";
    }
}
