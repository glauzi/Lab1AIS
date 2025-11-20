using BookManager.Core.Services;
using BookManager.Entities;
using BookManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Presenter
{
    /// <summary>
    /// Presenter в архитектуре MVP.
    /// Подписывается на события View, вызывает бизнес-логику и обновляет View.
    /// Работает только через интерфейсы: IBookView и IBookService.
    /// </summary>
    public class BookPresenter
    {
        private readonly IBookView _view;
        private readonly IBookService _service;

        /// <summary>
        /// Создаёт новый экземпляр презентера и подписывается на события View.
        /// </summary>
        /// <param name="view">Представление (форма), реализующее IBookView.</param>
        /// <param name="service">Сервис бизнес-логики для работы с книгами.</param>
        public BookPresenter(IBookView view, IBookService service)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _service = service ?? throw new ArgumentNullException(nameof(service));

            // Подписка на события View
            _view.ViewLoaded += OnViewLoaded;
            _view.AddBookRequested += OnAddBookRequested;
            _view.UpdateBookRequested += OnUpdateBookRequested;
            _view.DeleteBookRequested += OnDeleteBookRequested;
            _view.GroupByGenreRequested += OnGroupByGenreRequested;
            _view.FindBooksByYearRequested += OnFindBooksByYearRequested;
            _view.SelectedBookChanged += OnSelectedBookChanged;
        }

        /// <summary>
        /// Обработчик события загрузки View.
        /// Загружает список всех книг и отображает их.
        /// </summary>
        private void OnViewLoaded(object? sender, EventArgs e)
        {
            try
            {
                var books = _service.GetAllBooks();
                _view.ShowBooks(books);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка при загрузке книг: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик добавления книги.
        /// Читает данные из View, валидирует, создаёт книгу через сервис и обновляет список.
        /// </summary>
        private void OnAddBookRequested(object? sender, EventArgs e)
        {
            try
            {
                // Читаем данные из View
                string title = _view.BookTitle;
                string author = _view.BookAuthor;
                string genre = _view.BookGenre;
                string yearText = _view.BookYearText;

                if (!int.TryParse(yearText, out int year))
                {
                    _view.ShowMessage("Год должен быть числом!");
                    return;
                }

                if (year < 0 || year > 2025)
                {
                    _view.ShowMessage("Год должен быть от 0 до 2025!");
                    return;
                }

                var newBook = new Book(0, title, author, genre, year);
                _service.CreateBook(newBook);

                // Обновляем список и очищаем поля
                var books = _service.GetAllBooks();
                _view.ShowBooks(books);
                _view.ClearBookInputFields();

                _view.ShowMessage("Книга успешно добавлена!");
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка при добавлении книги: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик обновления книги.
        /// </summary>
        private void OnUpdateBookRequested(object? sender, EventArgs e)
        {
            try
            {
                var selectedBook = _view.SelectedBook;
                if (selectedBook == null)
                {
                    _view.ShowMessage("Выберите книгу для обновления!");
                    return;
                }

                string title = _view.BookTitle;
                string author = _view.BookAuthor;
                string genre = _view.BookGenre;
                string yearText = _view.BookYearText;

                if (!int.TryParse(yearText, out int year))
                {
                    _view.ShowMessage("Год должен быть числом!");
                    return;
                }

                if (year < 0 || year > 2025)
                {
                    _view.ShowMessage("Год должен быть от 0 до 2025!");
                    return;
                }

                var updatedBook = new Book(selectedBook.Id, title, author, genre, year);
                _service.UpdateBook(updatedBook);

                var books = _service.GetAllBooks();
                _view.ShowBooks(books);

                _view.ShowMessage("Книга успешно обновлена!");
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка при обновлении книги: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик удаления книги.
        /// </summary>
        private void OnDeleteBookRequested(object? sender, EventArgs e)
        {
            try
            {
                var selectedBook = _view.SelectedBook;
                if (selectedBook == null)
                {
                    _view.ShowMessage("Выберите книгу для удаления!");
                    return;
                }

                // Для упрощения: удаляем без подтверждения (подтверждение можно потом добавить в IBookView как отдельный метод).
                _service.DeleteBookById(selectedBook.Id);

                var books = _service.GetAllBooks();
                _view.ShowBooks(books);
                _view.ClearBookInputFields();

                _view.ShowMessage("Книга успешно удалена!");
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка при удалении книги: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик группировки книг по жанрам.
        /// </summary>
        private void OnGroupByGenreRequested(object? sender, EventArgs e)
        {
            try
            {
                var booksByGenre = _service.GroupBooksByGenre();
                _view.ShowBooksByGenre(booksByGenre);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка при группировке: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик поиска книг, изданных после указанного года.
        /// </summary>
        private void OnFindBooksByYearRequested(object? sender, EventArgs e)
        {
            try
            {
                string yearText = _view.YearFilterText;

                if (!int.TryParse(yearText, out int year))
                {
                    _view.ShowMessage("Введите корректный год для поиска!");
                    return;
                }

                var books = _service.FindBooksPublishedAfterYear(year);
                _view.ShowBooksAfterYear(books, year);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка при поиске по году: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик смены выбранной книги.
        /// Обновляет поля ввода данными выбранной книги.
        /// </summary>
        private void OnSelectedBookChanged(object? sender, EventArgs e)
        {
            var selectedBook = _view.SelectedBook;
            if (selectedBook != null)
            {
                _view.BookTitle = selectedBook.Title;
                _view.BookAuthor = selectedBook.Author;
                _view.BookGenre = selectedBook.Genre;
                _view.BookYearText = selectedBook.Year.ToString();
            }
        }
    }
}
