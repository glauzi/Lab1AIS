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
        /// Конструктор по умолчанию для обратной совместимости
        /// Создает новый контекст БД (временное решение)
        /// </summary>
        public EntityBookRepository() : this(new BookContext())
        {
        }

        /// <summary>
        /// Основной конструктор с внедрением зависимости контекста
        /// Реализует SRP - репозиторий только использует контекст, не создает его
        /// Позволяет тестировать с mock-контекстами
        /// </summary>
        /// <param name="context">Готовый контекст базы данных</param>
        public EntityBookRepository(BookContext context)
        {
            _context = context;
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
