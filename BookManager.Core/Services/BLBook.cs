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
    public class BLBook
    {
        private readonly IBookRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр бизнес-логики с внедренной зависимостью репозитория
        /// </summary>
        /// <param name="repository">Реализация интерфейса IBookRepository для работы с данными</param>
        public BLBook(IBookRepository repository)
        {
            _repository = repository;
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