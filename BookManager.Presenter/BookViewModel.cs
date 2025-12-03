using System;
using System.Collections.ObjectModel;
using BookManager.Core.Services;  // IBookService, BookService
using BookManager.Entities;       // Book
using BookManager.Shared;         // BookDto

namespace BookManager.Presenter
{
    /// <summary>
    /// ViewModel для работы со списком книг в WPF-приложении.
    /// Отвечает за загрузку данных из бизнес-логики и представление их в виде DTO для привязки.
    /// </summary>
    public class BookViewModel : ViewModelBase
    {
        /// <summary>
        /// Сервис для работы с книгами (фасад над бизнес-логикой и репозиториями).
        /// Предоставляется через DI.
        /// </summary>
        private readonly IBookService _bookService;

        /// <summary>
        /// Коллекция книг, отображаемая в интерфейсе.
        /// ObservableCollection используется для автоматического обновления UI
        /// при добавлении/удалении элементов.
        /// </summary>
        public ObservableCollection<BookDto> Books { get; } = new();

        private BookDto? _selectedBook;

        /// <summary>
        /// Текущая выбранная книга в списке.
        /// Используется для привязки к выбранной строке и редактированию полей.
        /// </summary>
        public BookDto? SelectedBook
        {
            get => _selectedBook;
            set => SetProperty(ref _selectedBook, value);
        }

        private string _statusMessage = string.Empty;

        /// <summary>
        /// Строка статуса/сообщения для пользователя.
        /// Например, количество загруженных книг или текст ошибки.
        /// </summary>
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        /// <summary>
        /// Создаёт новый экземпляр BookViewModel.
        /// </summary>
        /// <param name="bookService">
        /// Сервис для работы с книгами. Должен быть предоставлен из DI-контейнера.
        /// </param>
        /// <exception cref="ArgumentNullException">Если bookService равен null.</exception>
        public BookViewModel(IBookService bookService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            LoadBooks();
        }

        /// <summary>
        /// Загружает все книги из бизнес-логики и заполняет коллекцию Books.
        /// Вызывается при инициализации ViewModel и может вызываться повторно для обновления списка.
        /// </summary>
        public void LoadBooks()
        {
            Books.Clear();

            var books = _bookService.GetAllBooks();

            foreach (var book in books)
            {
                Books.Add(ToDto(book));
            }

            StatusMessage = $"Загружено книг: {Books.Count}";
        }

        /// <summary>
        /// Преобразует доменную сущность Book в DTO для слоя представления.
        /// </summary>
        /// <param name="book">Доменная сущность книги.</param>
        /// <returns>Экземпляр BookDto с теми же данными.</returns>
        private static BookDto ToDto(Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Year = book.Year
            };
        }

        /// <summary>
        /// Преобразует DTO обратно в доменную сущность Book.
        /// Используется при сохранении/обновлении данных.
        /// </summary>
        /// <param name="dto">DTO книги.</param>
        /// <returns>Экземпляр Book с теми же данными.</returns>
        public static Book FromDto(BookDto dto)
        {
            return new Book(dto.Id, dto.Title, dto.Author, dto.Genre, dto.Year);
        }
    }
}
