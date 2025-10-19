using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookManager.DataAccessLayer
{
    public class EntityBookRepository : IBookRepository
    {
        private readonly BookContext _context;

        public EntityBookRepository()
        {
            _context = new BookContext();
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public Book GetById(int id)
        {
            return _context.Books.Find(id);
        }

        public void Update(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
