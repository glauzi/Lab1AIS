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
    /// Реализует интерфейс IBookService, объединяя CRUD и бизнес-логику
    /// </summary>
    public class BookService : IBookService
    {
        private readonly CRUD _managementService;
        private readonly BLBook _businessLogicService;

        public BookService(IBookRepository repository)
        {
            // Оба внутренних сервиса получают один и тот же репозиторий
            _managementService = new CRUD(repository);
            _businessLogicService = new BLBook(repository);
        }

        // CRUD-методы делегируют в _managementService
        public void CreateBook(Book book) => _managementService.CreateBook(book);
        public List<Book> GetAllBooks() => _managementService.GetAllBooks();
        public Book? GetBookById(int id) => _managementService.GetBookById(id);
        public void UpdateBook(Book bookToUpdate) => _managementService.UpdateBook(bookToUpdate);
        public void DeleteBookById(int id) => _managementService.DeleteBookById(id);

        // Бизнес-методы делегируют в _businessLogicService
        public Dictionary<string, List<Book>> GroupBooksByGenre() => _businessLogicService.GroupBooksByGenre();
        public List<Book> FindBooksPublishedAfterYear(int year) => _businessLogicService.FindBooksPublishedAfterYear(year);
    }
}
