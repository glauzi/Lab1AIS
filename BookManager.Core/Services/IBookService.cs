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
        /// <summary>
        /// Создаёт новую книгу в хранилище.
        /// </summary>
        /// <param name="book">Доменная сущность книги, содержащая обязательные данные.</param>
        /// <remarks>
        /// ID сущности может быть сгенерирован автоматически на уровне DAL.
        /// </remarks>
        void CreateBook(Book book);

        /// <summary>
        /// Возвращает полный список всех книг из хранилища.
        /// </summary>
        /// <returns>Список доменных объектов <see cref="Book"/>.</returns>
        List<Book> GetAllBooks();

        /// <summary>
        /// Возвращает книгу по её уникальному идентификатору.
        /// </summary>
        /// <param name="id">ID книги.</param>
        /// <returns>
        /// Объект книги, если она существует;  
        /// иначе — <c>null</c>.
        /// </returns>
        Book? GetBookById(int id);

        /// <summary>
        /// Обновляет существующую книгу в хранилище.
        /// Поля сущности перезаписываются новыми значениями.
        /// </summary>
        /// <param name="bookToUpdate">Обновлённая версия объекта книги.</param>
        void UpdateBook(Book bookToUpdate);

        /// <summary>
        /// Удаляет книгу по её ID.
        /// </summary>
        /// <param name="id">ID удаляемой книги.</param>
        void DeleteBookById(int id);

        /// <summary>
        /// Группирует книги по жанрам.
        /// </summary>
        /// <returns>
        /// Словарь, где ключ — название жанра,
        /// а значение — список книг, относящихся к этому жанру.
        /// </returns>
        Dictionary<string, List<Book>> GroupBooksByGenre();

        /// <summary>
        /// Возвращает список книг, опубликованных после указанного года.
        /// </summary>
        /// <param name="year">Пороговый год для фильтрации.</param>
        /// <returns>
        /// Список доменных объектов <see cref="Book"/>,
        /// у которых значение <see cref="Book.Year"/> больше указанного.
        /// </returns>
        List<Book> FindBooksPublishedAfterYear(int year);
    }
}
