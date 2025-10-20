using BookManager.Entities;
using System.Collections.Generic;

namespace BookManager.DataAccessLayer
{
    /// <summary>
    /// Определяет контракт для репозитория работы с книгами
    /// Обеспечивает абстракцию над способом хранения данных
    /// </summary>
    public interface IBookRepository
    {
        /// <summary>
        /// Добавляет новую книгу в хранилище данных
        /// </summary>
        /// <param name="book">Книга для добавления</param>
        void Add(Book book);

        /// <summary>
        /// Удаляет книгу по идентификатору из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор книги для удаления</param>
        void Delete(int id);

        /// <summary>
        /// Возвращает все книги из хранилища данных
        /// </summary>
        /// <returns>Список всех книг</returns>
        List<Book> GetAll();

        /// <summary>
        /// Находит книгу по идентификатору в хранилище данных
        /// </summary>
        /// <param name="id">Идентификатор книги для поиска</param>
        /// <returns>Найденная книга или null если не найдена</returns>
        Book GetById(int id);

        /// <summary>
        /// Обновляет информацию о книге в хранилище данных
        /// </summary>
        /// <param name="book">Книга с обновленными данными</param>
        void Update(Book book);
    }
}
