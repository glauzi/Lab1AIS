using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Shared
{
    /// <summary>
    /// DTO-объект для передачи данных о книге между Presenter и View.
    /// View не знает о доменной сущности Book, работает только с этой оболочкой.
    /// </summary>
    public class BookDto
    {
        /// <summary>
        /// Идентификатор книги.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название книги.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Автор книги.
        /// </summary>
        public string Author { get; set; } = string.Empty;

        /// <summary>
        /// Жанр книги.
        /// </summary>
        public string Genre { get; set; } = string.Empty;

        /// <summary>
        /// Год издания книги.
        /// </summary>
        public int Year { get; set; }
    }
}
