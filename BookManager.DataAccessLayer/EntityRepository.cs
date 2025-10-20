using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookManager.DataAccessLayer
{
    /// <summary>
    /// Реализация репозитория книг с использованием Entity Framework Core
    /// Обеспечивает объектно-ориентированную работу с базой данных
    /// </summary>
    public class EntityBookRepository : IBookRepository
    {
        private readonly BookContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр репозитория Entity Framework
        /// Создает новый контекст базы данных
        /// </summary>
        public EntityBookRepository()
        {
            _context = new BookContext();
        }

        /// <summary>
        /// Добавляет новую книгу в базу данных через Entity Framework
        /// </summary>
        /// <param name="book">Книга для добавления</param>
        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет книгу по идентификатору из базы данных через Entity Framework
        /// </summary>
        /// <param name="id">Идентификатор книги для удаления</param>
        public void Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Возвращает все книги из базы данных через Entity Framework
        /// </summary>
        /// <returns>Список всех книг</returns>
        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        /// <summary>
        /// Находит книгу по идентификатору в базе данных через Entity Framework
        /// </summary>
        /// <param name="id">Идентификатор книги для поиска</param>
        /// <returns>Найденная книга или null если не найдена</returns>
        public Book GetById(int id)
        {
            return _context.Books.Find(id);
        }

        /// <summary>
        /// Обновляет информацию о книге в базе данных через Entity Framework
        /// </summary>
        /// <param name="book">Книга с обновленными данными</param>
        public void Update(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
