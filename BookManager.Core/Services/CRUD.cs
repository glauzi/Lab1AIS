using BookManager.DataAccessLayer;
using BookManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Core.Services
{
    public class CRUD
    {
        private readonly IBookRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр бизнес-логики с внедренной зависимостью репозитория
        /// </summary>
        /// <param name="repository">Реализация интерфейса IBookRepository для работы с данными</param>
        public CRUD(IBookRepository repository)
        {
            _repository = repository;
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

    }
}