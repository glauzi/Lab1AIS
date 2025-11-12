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
    /// Использует фабрику контекстов для изоляции операций и предотвращения конфликтов отслеживания
    /// </summary>
    public class EntityBookRepository : IBookRepository
    {
        private readonly Func<BookContext> _contextFactory;

        /// <summary>
        /// Основной конструктор с внедрением фабрики контекстов
        /// Решает проблему отслеживания сущностей в WinForms
        /// </summary>
        /// <param name="contextFactory">Фабрика для создания экземпляров контекста БД</param>
        public EntityBookRepository(Func<BookContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <summary>
        /// Добавляет новую книгу в базу данных через Entity Framework
        /// </summary>
        /// <param name="book">Книга для добавления</param>
        public void Add(Book book)
        {
            using (var context = _contextFactory())
            {
                context.Books.Add(book);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Удаляет книгу по идентификатору из базы данных через Entity Framework
        /// </summary>
        /// <param name="id">Идентификатор книги для удаления</param>
        public void Delete(int id)
        {
            using (var context = _contextFactory())
            {
                var book = context.Books.Find(id);
                if (book != null)
                {
                    context.Books.Remove(book);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Возвращает все книги из базы данных через Entity Framework
        /// </summary>
        /// <returns>Список всех книг</returns>
        public List<Book> GetAll()
        {
            using (var context = _contextFactory())
            {
                return context.Books.ToList();
            }
        }

        /// <summary>
        /// Находит книгу по идентификатору в базе данных через Entity Framework
        /// </summary>
        /// <param name="id">Идентификатор книги для поиска</param>
        /// <returns>Найденная книга или null если не найдена</returns>
        public Book GetById(int id)
        {
            using (var context = _contextFactory())
            {
                return context.Books.Find(id);
            }
        }

        /// <summary>
        /// Обновляет информацию о книге в базе данных через Entity Framework
        /// </summary>
        /// <param name="book">Книга с обновленными данными</param>
        public void Update(Book book)
        {
            using (var context = _contextFactory())
            {
                var existingBook = context.Books.Find(book.Id);
                if (existingBook != null)
                {
                    context.Entry(existingBook).CurrentValues.SetValues(book);
                    context.SaveChanges();
                }
            }
        }
    }
}
