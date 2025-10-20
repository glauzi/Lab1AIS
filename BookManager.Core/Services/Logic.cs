using BookManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.DataAccessLayer;

namespace BookManager.Core.Services
{
    /// <summary>
    /// Реализует бизнес-логику для управления книгами
    /// Работает с данными через абстракцию IBookRepository
    /// </summary>
    public class Logic
    {
        private readonly IBookRepository _repository;

        /// <summary>
        /// Инициализирует бизнес-логику с выбранной реализацией репозитория
        /// </summary>
        public Logic()
        {
            _repository = new EntityBookRepository();
            //_repository = new DapperBookRepository();
        }

        /// <summary>
        /// Добавляет новую книгу в ,базу данных
        /// </summary>
        /// <param name="book">Книга для добавления</param>
        public void CreateBook(Book book)
        {
            _repository.Add(book);
        }

        /// <summary>
        /// Возвращает все книги из базы данных
        /// </summary>
        /// <returns>Список всех книг</returns>
        public List<Book> GetAllBooks()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Находит книгу по идентификатору в базе данных
        /// </summary>
        /// <param name="id">Идентификатор книги для поиска</param>
        /// <returns>Найденная книга или null если не найдена</returns>
        public Book? GetBookById(int id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Обновляет информацию о книге в базе данных
        /// </summary>
        /// <param name="bookToUpdate">Книга с обновленными данными</param>
        public void UpdateBook(Book bookToUpdate)
        {
            _repository.Update(bookToUpdate);
        }

        /// <summary>
        /// Удаляет книгу по идентификатору в базе данных
        /// </summary>
        /// <param name="id">Идентификатор книги для удаления</param>
        public void DeleteBookById(int id)
        {
            _repository.Delete(id);
        }

        /// <summary>
        /// Группирует книги по жанрам в базе данных
        /// </summary>
        /// <returns>Словарь где ключ - жанр, значение - список книг этого жанра</returns>
        public Dictionary<string, List<Book>> GroupBooksByGenre()
        {
            var books = _repository.GetAll();
            return books.GroupBy(book => book.Genre)
                       .ToDictionary(group => group.Key, group => group.ToList());
        }

        /// <summary>
        /// Находит книги, изданные после указанного года в базе данных
        /// </summary>
        /// <param name="year">Год для фильтрации</param>
        /// <returns>Список книг, изданных после указанного года</returns>
        public List<Book> FindBooksPublishedAfterYear(int year)
        {
            var books = _repository.GetAll();
            return books.Where(book => book.Year > year).ToList();
        }
    }
}
