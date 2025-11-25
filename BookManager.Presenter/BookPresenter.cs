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
        /// <summary>
        /// Представление (View), с которым работает презентер.
        /// Реализуется, например, формой WinForms.
        /// </summary>
        private readonly IBookView _view;
        /// <summary>
        /// Сервис бизнес-логики для работы с книгами.
        /// Инкапсулирует CRUD-операции и бизнес-функции.
        /// </summary>
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
        /// Обрабатывает событие загрузки представления.
        /// Загружает список всех книг из модели, преобразует в DTO и передаёт во View.
        /// </summary>
        private void OnViewLoaded(object? sender, EventArgs e)
        {
            try
            {
                var books = _service.GetAllBooks();
                var dtos = books.Select(ToDto).ToList();
                _view.ShowBooks(dtos);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка при загрузке книг: {ex.Message}");
            }
        }

        /// <summary>
        /// Обрабатывает запрос на добавление новой книги.
        /// Читает данные из View, запрашивает у View корректный год,
        /// создаёт доменную сущность Book и передаёт её в сервис.
        /// </summary>
        private void OnAddBookRequested(object? sender, EventArgs e)
        {
            try
            {
                string title = _view.BookTitle;
                string author = _view.BookAuthor;
                string genre = _view.BookGenre;

                // Валидация и получение года полностью во View
                if (!_view.TryGetBookYear(out int year))
                    return;

                var newBook = new Book(0, title, author, genre, year);
                _service.CreateBook(newBook);

                var books = _service.GetAllBooks();
                var dtos = books.Select(ToDto).ToList();
                _view.ShowBooks(dtos);
                _view.ClearBookInputFields();

                _view.ShowMessage("Книга успешно добавлена!");
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка при добавлении книги: {ex.Message}");
            }
        }

        /// <summary>
        /// Обрабатывает запрос на обновление выбранной книги.
        /// Берёт выбранный DTO из View, применяет изменённые значения полей,
        /// запрашивает корректный год у View и передаёт обновлённую сущность в сервис.
        /// </summary>
        private void OnUpdateBookRequested(object? sender, EventArgs e)
        {
            try
            {
                var selectedDto = _view.SelectedBook;
                if (selectedDto == null)
                {
                    _view.ShowMessage("Выберите книгу для обновления!");
                    return;
                }

                if (!_view.TryGetBookYear(out int year))
                    return;

                // Собираем обновлённую доменную сущность
                var updatedBook = new Book(
                    selectedDto.Id,
                    _view.BookTitle,
                    _view.BookAuthor,
                    _view.BookGenre,
                    year);

                _service.UpdateBook(updatedBook);

                var books = _service.GetAllBooks();
                var dtos = books.Select(ToDto).ToList();
                _view.ShowBooks(dtos);

                _view.ShowMessage("Книга успешно обновлена!");
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка при обновлении книги: {ex.Message}");
            }
        }

        /// <summary>
        /// Обрабатывает запрос на удаление выбранной книги.
        /// Берёт выбранный DTO из View, удаляет соответствующую сущность по Id.
        /// </summary>
        private void OnDeleteBookRequested(object? sender, EventArgs e)
        {
            try
            {
                var selectedDto = _view.SelectedBook;
                if (selectedDto == null)
                {
                    _view.ShowMessage("Выберите книгу для удаления!");
                    return;
                }

                _service.DeleteBookById(selectedDto.Id);

                var books = _service.GetAllBooks();
                var dtos = books.Select(ToDto).ToList();
                _view.ShowBooks(dtos);
                _view.ClearBookInputFields();

                _view.ShowMessage("Книга успешно удалена!");
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка при удалении книги: {ex.Message}");
            }
        }

        /// <summary>
        /// Обрабатывает запрос на группировку книг по жанрам.
        /// Получает данные из сервиса, преобразует в DTO и передаёт во View.
        /// </summary>
        private void OnGroupByGenreRequested(object? sender, EventArgs e)
        {
            try
            {
                var booksByGenre = _service.GroupBooksByGenre();

                var dtoDict = booksByGenre.ToDictionary(
                    g => g.Key,
                    g => g.Value.Select(ToDto).ToList()
                );

                _view.ShowBooksByGenre(dtoDict);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка при группировке: {ex.Message}");
            }
        }


        /// <summary>
        /// Обрабатывает запрос на поиск книг, опубликованных после указанного года.
        /// Год фильтра запрашивается и валидируется во View.
        /// </summary>
        private void OnFindBooksByYearRequested(object? sender, EventArgs e)
        {
            try
            {
                if (!_view.TryGetFilterYear(out int year))
                    return;

                var books = _service.FindBooksPublishedAfterYear(year);
                var dtos = books.Select(ToDto).ToList();
                _view.ShowBooksAfterYear(dtos, year);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Ошибка при поиске по году: {ex.Message}");
            }
        }

        /// <summary>
        /// Обрабатывает смену выбранной книги в представлении.
        /// Копирует данные выбранного DTO в поля ввода View.
        /// </summary>
        private void OnSelectedBookChanged(object? sender, EventArgs e)
        {
            var selectedDto = _view.SelectedBook;
            if (selectedDto != null)
            {
                _view.BookTitle = selectedDto.Title;
                _view.BookAuthor = selectedDto.Author;
                _view.BookGenre = selectedDto.Genre;
                _view.BookYearText = selectedDto.Year.ToString();
            }
        }

        /// <summary>
        /// Преобразует доменную сущность Book в DTO для передачи во View.
        /// </summary>
        private static BookDto ToDto(Book book) =>
            new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Year = book.Year
            };

        /// <summary>
        /// Преобразует DTO-книги из View обратно в доменную сущность Book.
        /// Может использоваться, если потребуется передавать изменения обратно в модель.
        /// </summary>
        private static Book FromDto(BookDto dto) =>
            new Book(dto.Id, dto.Title, dto.Author, dto.Genre, dto.Year);
    }
}
