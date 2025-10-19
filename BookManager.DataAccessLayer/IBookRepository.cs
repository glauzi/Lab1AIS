using BookManager.Entities;
using System.Collections.Generic;

namespace BookManager.DataAccessLayer
{
    public interface IBookRepository
    {
        void Add(Book book);
        void Delete(int id);
        List<Book> GetAll();
        Book GetById(int id);
        void Update(Book book);
    }
}
