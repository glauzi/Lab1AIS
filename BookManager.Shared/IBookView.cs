using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Entities;

namespace BookManager.Shared
{
    /// <summary>
    /// Интерфейс представления (View) для работы с книгами в архитектуре MVP.
    /// Его реализует форма (WinForms), а с ним работает Presenter.
    /// </summary>
    public interface IBookView
    {

        /// <summary>
        /// Событие, возникающее при загрузке представления (например, при загрузке формы).
        /// Presenter должен на него подписаться и загрузить данные.
        /// </summary>
        event EventHandler ViewLoaded;

        /// <summary>
        /// Пользователь запросил добавление новой книги (нажал кнопку "Добавить").
        /// </summary>
        event EventHandler AddBookRequested;

        /// <summary>
        /// Пользователь запросил обновление данных книги (нажал "Обновить").
        /// </summary>
        event EventHandler UpdateBookRequested;

        /// <summary>
        /// Пользователь запросил удаление книги (нажал "Удалить").
        /// </summary>
        event EventHandler DeleteBookRequested;

        /// <summary>
        /// Пользователь запросил группировку книг по жанрам.
        /// </summary>
        event EventHandler GroupByGenreRequested;

        /// <summary>
        /// Пользователь запросил поиск книг по году (книги новее указанного года).
        /// </summary>
        event EventHandler FindBooksByYearRequested;

        /// <summary>
        /// Пользователь сменил выбранную книгу (например, выделил другую строку в таблице).
        /// </summary>
        event EventHandler SelectedBookChanged;


        /// <summary>
        /// Название книги, введённое пользователем.
        /// </summary>
        string BookTitle { get; set; }

        /// <summary>
        /// Автор книги, введённый пользователем.
        /// </summary>
        string BookAuthor { get; set; }

        /// <summary>
        /// Жанр книги, введённый пользователем.
        /// </summary>
        string BookGenre { get; set; }

        /// <summary>
        /// Год книги в виде текста (Presenter сам будет парсить и валидировать).
        /// </summary>
        string BookYearText { get; set; }

        /// <summary>
        /// Текст для фильтрации по году (поиск книг новее этого года).
        /// </summary>
        string YearFilterText { get; set; }

        /// <summary>
        /// Текущая выбранная книга (строка в таблице).
        /// Может быть null, если ничего не выбрано.
        /// </summary>
        Book? SelectedBook { get; }

        /// <summary>
        /// Показать (перерисовать) полный список книг в основной таблице.
        /// </summary>
        /// <param name="books">Список книг для отображения.</param>
        void ShowBooks(IList<Book> books);

        /// <summary>
        /// Показать результат группировки книг по жанрам.
        /// </summary>
        /// <param name="booksByGenre">
        /// Словарь: ключ - жанр, значение - список книг этого жанра.
        /// </param>
        void ShowBooksByGenre(Dictionary<string, List<Book>> booksByGenre);

        /// <summary>
        /// Показать результат поиска книг, изданных после указанного года.
        /// </summary>
        /// <param name="books">Список найденных книг.</param>
        /// <param name="year">Год, по которому выполнялась фильтрация.</param>
        void ShowBooksAfterYear(IList<Book> books, int year);

        /// <summary>
        /// Показать пользователю информационное сообщение (ошибка, успех и т.п.).
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        void ShowMessage(string message);

        /// <summary>
        /// Очистить поля ввода данных книги.
        /// </summary>
        void ClearBookInputFields();
    }
}
