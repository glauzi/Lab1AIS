using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Entities;
using BookManager.Shared;

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
        BookDto? SelectedBook { get; }

        /// <summary>
        /// Показать (перерисовать) полный список книг в основной таблице.
        /// </summary>
        /// <param name="books">Список книг для отображения.</param>
        void ShowBooks(IList<BookDto> books);

        /// <summary>
        /// Показать результат группировки книг по жанрам.
        /// </summary>
        /// <param name="booksByGenre">
        /// Словарь: ключ - жанр, значение - список книг этого жанра.
        /// </param>
        void ShowBooksByGenre(Dictionary<string, List<BookDto>> booksByGenre);

        /// <summary>
        /// Показать результат поиска книг, изданных после указанного года.
        /// </summary>
        /// <param name="books">Список найденных книг.</param>
        /// <param name="year">Год, по которому выполнялась фильтрация.</param>
        void ShowBooksAfterYear(IList<BookDto> books, int year);

        /// <summary>
        /// Показать пользователю информационное сообщение (ошибка, успех и т.п.).
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        void ShowMessage(string message);

        /// <summary>
        /// Очистить поля ввода данных книги.
        /// </summary>
        void ClearBookInputFields();

        /// <summary>
        /// Пытается считать и провалидировать год издания книги из UI.
        /// Возвращает true при успешной валидации, иначе false
        /// и показывает пользователю сообщение об ошибке.
        /// </summary>
        /// <param name="year">Результат успешно считанного года.</param>
        bool TryGetBookYear(out int year);

        /// <summary>
        /// Пытается считать и провалидировать год для фильтрации книг при поиске.
        /// Возвращает true при успешной валидации, иначе false
        /// и показывает пользователю сообщение об ошибке.
        /// </summary>
        /// <param name="year">Результат успешно считанного года фильтра.</param>
        bool TryGetFilterYear(out int year);
    }
}
