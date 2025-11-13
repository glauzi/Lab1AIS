using BookManager.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BookManager.DataAccessLayer
{
    /// <summary>
    /// Реализация репозитория книг с использованием Dapper
    /// Обеспечивает высокопроизводительную работу с базой данных через SQL-запросы
    /// </summary>
    public class DapperBookRepository : IBookRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Основной конструктор с внедрением зависимости строки подключения
        /// Реализует SRP - репозиторий только использует строку подключения, не определяет ее
        /// Позволяет использовать разные базы данных и тестировать с mock-connection
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных</param>
        public DapperBookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Добавляет новую книгу в базу данных через Dapper
        /// Использует параметризованный SQL-запрос для безопасности
        /// </summary>
        /// <param name="book">Книга для добавления</param>
        public void Add(Book book)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO Books (Title, Author, Genre, Year) 
                           VALUES (@Title, @Author, @Genre, @Year)";
                connection.Execute(sql, book);
            }
        }

        /// <summary>
        /// Удаляет книгу по идентификатору из базы данных через Dapper
        /// </summary>
        /// <param name="id">Идентификатор книги для удаления</param>
        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "DELETE FROM Books WHERE Id = @Id";
                connection.Execute(sql, new { Id = id });
            }
        }

        /// <summary>
        /// Возвращает все книги из базы данных через Dapper
        /// </summary>
        /// <returns>Список всех книг</returns>
        public List<Book> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Books";
                return connection.Query<Book>(sql).ToList();
            }
        }

        /// <summary>
        /// Находит книгу по идентификатору в базе данных через Dapper
        /// </summary>
        /// <param name="id">Идентификатор книги для поиска</param>
        /// <returns>Найденная книга или null если не найдена</returns>
        public Book GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Books WHERE Id = @Id";
                return connection.QueryFirstOrDefault<Book>(sql, new { Id = id });
            }
        }

        /// <summary>
        /// Обновляет информацию о книге в базе данных через Dapper
        /// </summary>
        /// <param name="book">Книга с обновленными данными</param>
        public void Update(Book book)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE Books 
                           SET Title = @Title, Author = @Author, 
                               Genre = @Genre, Year = @Year 
                           WHERE Id = @Id";
                connection.Execute(sql, book);
            }
        }
    }
}
