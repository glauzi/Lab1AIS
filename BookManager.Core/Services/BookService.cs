using BookManager.DataAccessLayer;
using BookManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Core.Services
{
    /// <summary>
    /// Реализация сервиса бизнес-логики для работы с книгами.
    /// 
    /// Данный класс реализует интерфейс <see cref="IBookService"/> и выступает
    /// фасадом, объединяющим два отдельных уровня логики:
    /// 1) CRUD-операции (<see cref="CRUD"/>)
    /// 2) бизнес-функции (<see cref="BLBook"/>)
    /// 
    /// Таким образом, BookService инкапсулирует детали реализации
    /// и предоставляет презентеру единый, удобный интерфейс.
    /// </summary>
    public class BookService : IBookService
    {
        /// <summary>
        /// Сервис, отвечающий за CRUD-операции с сущностью <see cref="Book"/>.
        /// Взаимодействует с репозиторием для создания, чтения, обновления и удаления данных.
        /// </summary>
        private readonly CRUD _managementService;

        /// <summary>
        /// Сервис, отвечающий за дополнительную бизнес-логику:
        /// группировку, фильтрацию и другие вычисления, не относящиеся к CRUD.
        /// </summary>
        private readonly BLBook _businessLogicService;

        /// <summary>
        /// Создаёт основной сервис бизнес-логики.
        /// 
        /// Получает конкретный <see cref="IBookRepository"/> из DI-контейнера
        /// и на его основе создаёт два внутренних сервиса:
        /// - CRUD: базовые операции хранения
        /// - BLBook: бизнес-логика
        /// 
        /// Презентер не работает с репозиторием напрямую — только с этим фасадом.
        /// </summary>
        /// <param name="repository">Репозиторий для доступа к данным книг.</param>
        public BookService(IBookRepository repository)
        {
            _managementService = new CRUD(repository);
            _businessLogicService = new BLBook(repository);
        }

        /// <summary>
        /// Добавляет новую книгу в хранилище.
        /// Делегирует вызов в <see cref="CRUD.CreateBook(Book)"/>.
        /// </summary>
        public void CreateBook(Book book) =>
            _managementService.CreateBook(book);

        /// <summary>
        /// Возвращает список всех книг.
        /// Делегирует вызов в <see cref="CRUD.GetAllBooks"/>.
        /// </summary>
        public List<Book> GetAllBooks() =>
            _managementService.GetAllBooks();

        /// <summary>
        /// Возвращает книгу по её ID.
        /// Делегирует вызов в <see cref="CRUD.GetBookById(int)"/>.
        /// </summary>
        public Book? GetBookById(int id) =>
            _managementService.GetBookById(id);

        /// <summary>
        /// Обновляет данные существующей книги.
        /// Делегирует вызов в <see cref="CRUD.UpdateBook(Book)"/>.
        /// </summary>
        public void UpdateBook(Book bookToUpdate) =>
            _managementService.UpdateBook(bookToUpdate);

        /// <summary>
        /// Удаляет книгу по идентификатору.
        /// Делегирует вызов в <see cref="CRUD.DeleteBookById(int)"/>.
        /// </summary>
        public void DeleteBookById(int id) =>
            _managementService.DeleteBookById(id);

        /// <summary>
        /// Группирует книги по жанрам.
        /// Делегирует вызов в <see cref="BLBook.GroupBooksByGenre"/>.
        /// </summary>
        public Dictionary<string, List<Book>> GroupBooksByGenre() =>
            _businessLogicService.GroupBooksByGenre();

        /// <summary>
        /// Возвращает книги, опубликованные после указанного года.
        /// Делегирует вызов в <see cref="BLBook.FindBooksPublishedAfterYear(int)"/>.
        /// </summary>
        public List<Book> FindBooksPublishedAfterYear(int year) =>
            _businessLogicService.FindBooksPublishedAfterYear(year);
    }
}
