using BookManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Core.Services
{
    /// <summary>
    /// Реализует бизнес-логику для управления книгами
    /// </summary>
    public class Logic
    {
        private readonly List<Book> _books = new();
        private int _nextId = 1;

        /// <summary>
        /// Добавляет новую книгу в коллекцию
        /// </summary>
        /// <param name="book">Книга для добавления</param>
        public void CreateBook(Book book)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Возвращает все книги из коллекции
        /// </summary>
        /// <returns>Список всех книг</returns>
        public List<Book> GetAllBooks()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Находит книгу по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор книги для поиска</param>
        /// <returns>Найденная книга или null если не найдена</returns>
        public Book? GetBookById(int id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Обновляет информацию о книге
        /// </summary>
        /// <param name="bookToUpdate">Книга с обновленными данными</param>
        public void UpdateBook(Book bookToUpdate)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Удаляет книгу по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор книги для удаления</param>
        public void DeleteBookById(int id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Группирует книги по жанрам
        /// </summary>
        /// <returns>Словарь где ключ - жанр, значение - список книг этого жанра</returns>
        public Dictionary<string, List<Book>> GroupBooksByGenre()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Находит книги, изданные после указанного года
        /// </summary>
        /// <param name="year">Год для фильтрации</param>
        /// <returns>Список книг, изданных после указанного года</returns>
        public List<Book> FindBooksPublishedAfterYear(int year)
        {
            throw new System.NotImplementedException();
        }
    }
}
