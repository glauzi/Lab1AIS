using BookManager.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BookManager.DataAccessLayer
{
    public class DapperBookRepository : IBookRepository
    {
        private readonly string _connectionString;

        public DapperBookRepository()
        {
            _connectionString = @"Server=GLAUZI\SQLEXPRESS;Database=BookManagerDB;Trusted_Connection=true;";
        }

        public void Add(Book book)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO Books (Title, Author, Genre, Year) 
                           VALUES (@Title, @Author, @Genre, @Year)";
                connection.Execute(sql, book);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "DELETE FROM Books WHERE Id = @Id";
                connection.Execute(sql, new { Id = id });
            }
        }

        public List<Book> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Books";
                return connection.Query<Book>(sql).ToList();
            }
        }

        public Book GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Books WHERE Id = @Id";
                return connection.QueryFirstOrDefault<Book>(sql, new { Id = id });
            }
        }

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
