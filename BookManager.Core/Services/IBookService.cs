using BookManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Core.Services
{
    /// <summary>
    /// Общий интерфейс для работы с книгами: CRUD-операции и бизнес-логика
    /// </summary>
    public interface IBookService
    {
        // CRUD-операции
        void CreateBook(Book book);
        List<Book> GetAllBooks();
        Book? GetBookById(int id);
        void UpdateBook(Book bookToUpdate);
        void DeleteBookById(int id);

        // Бизнес-логика
        Dictionary<string, List<Book>> GroupBooksByGenre();
        List<Book> FindBooksPublishedAfterYear(int year);
    }
}
