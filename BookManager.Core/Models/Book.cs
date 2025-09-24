using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Core.Models
{
    /// <summary>
    /// Представляет книгу в библиотечной системеа
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Идентификатор книги
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название книги
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Автор книги
        /// </summary>
        public string Author { get; set; } = string.Empty;
        /// <summary>
        /// Жанр книги
        /// </summary>
        public string Genre { get; set; } = string.Empty;
        /// <summary>
        /// Год выпуска книги
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Book() { }
        /// <summary>
        /// Создание нового экземпляра книги
        /// </summary>
        /// <param name="id">Айди</param>
        /// <param name="title">Название</param>
        /// <param name="author">Автор</param>
        /// <param name="genre">Жанр</param>
        /// <param name="year">Год</param>
        public Book(int id, string title, string author, string genre, int year)
        {
            Id = id;
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
        }
        /// <summary>
        /// Возвращает строковое представление книги
        /// </summary>
        /// <returns>Строка с информацией о книге</returns>
        public override string ToString()
        {
            return $"{Id}: {Title}. Автор: {Author} ({Genre}, {Year}г.)";
        }
    }
}
