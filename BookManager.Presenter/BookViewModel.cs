using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BookManager.Core.Services;
using BookManager.Entities;
using BookManager.Shared;

namespace BookManager.Presenter
{
    /// <summary>
    /// ViewModel для работы со списком книг.
    /// Содержит данные для привязки и команды для управления книгами.
    /// </summary>
    public class BookViewModel : ViewModelBase
    {

        private readonly IBookService _bookService;

        /// <summary>
        /// Коллекция книг, отображаемая в таблице.
        /// </summary>
        public ObservableCollection<BookDto> Books { get; } = new();


        /// <summary>
        /// Внутреннее поле для названия книги.
        /// </summary>
        private string _bookTitle = string.Empty;

        /// <summary>
        /// Внутреннее поле для автора книги.
        /// </summary>
        private string _bookAuthor = string.Empty;

        /// <summary>
        /// Внутреннее поле для жанра книги.
        /// </summary>
        private string _bookGenre = string.Empty;

        /// <summary>
        /// Внутреннее поле для года издания книги в текстовом виде.
        /// </summary>
        private string _bookYearText = string.Empty;

        /// <summary>
        /// Внутреннее поле для текста фильтра по году.
        /// </summary>
        private string _yearFilterText = string.Empty;

        /// <summary>
        /// Внутреннее поле для выбранной книги.
        /// </summary>
        private BookDto? _selectedBook;

        /// <summary>
        /// Внутреннее поле для строки статуса.
        /// </summary>
        private string _statusMessage = string.Empty;


        /// <summary>
        /// Название книги, вводимое пользователем.
        /// Привязано к соответствующему TextBox в правой панели.
        /// Влияет на доступность команд добавления и обновления.
        /// </summary>
        public string BookTitle
        {
            get => _bookTitle;
            set
            {
                if (SetProperty(ref _bookTitle, value))
                {
                    (AddBookCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (UpdateBookCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Автор книги, вводимый пользователем.
        /// Привязан к соответствующему TextBox.
        /// Влияет на доступность команд добавления и обновления.
        /// </summary>
        public string BookAuthor
        {
            get => _bookAuthor;
            set
            {
                if (SetProperty(ref _bookAuthor, value))
                {
                    (AddBookCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (UpdateBookCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Жанр книги, вводимый пользователем.
        /// Привязан к соответствующему TextBox.
        /// </summary>
        public string BookGenre
        {
            get => _bookGenre;
            set
            {
                if (SetProperty(ref _bookGenre, value))
                {
                    (AddBookCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (UpdateBookCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Год издания книги в текстовом виде.
        /// Используется для валидации и преобразуется в целое число
        /// при добавлении или обновлении книги.
        /// </summary>
        public string BookYearText
        {
            get => _bookYearText;
            set
            {
                if (SetProperty(ref _bookYearText, value))
                {
                    (AddBookCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (UpdateBookCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Значение фильтра по году.
        /// Если задано, используется для поиска книг,
        /// изданных после указанного года.
        /// </summary>
        public string YearFilterText
        {
            get => _yearFilterText;
            set
            {
                if (SetProperty(ref _yearFilterText, value))
                {
                    (FindBooksAfterYearCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (ResetFilterCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Текущая выбранная книга в списке.
        /// При изменении выбранной книги синхронизирует поля ввода
        /// и обновляет доступность команд обновления и удаления.
        /// </summary>
        public BookDto? SelectedBook
        {
            get => _selectedBook;
            set
            {
                if (SetProperty(ref _selectedBook, value))
                {
                    if (value != null)
                    {
                        BookTitle = value.Title;
                        BookAuthor = value.Author;
                        BookGenre = value.Genre;
                        BookYearText = value.Year.ToString();
                    }

                    (UpdateBookCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (DeleteBookCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Строка статуса, отображаемая в нижней части окна.
        /// Содержит служебные сообщения и информацию об ошибках.
        /// </summary>
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }


        /// <summary>
        /// Команда добавления новой книги.
        /// </summary>
        public ICommand AddBookCommand { get; }

        /// <summary>
        /// Команда обновления данных выбранной книги.
        /// </summary>
        public ICommand UpdateBookCommand { get; }

        /// <summary>
        /// Команда удаления выбранной книги.
        /// </summary>
        public ICommand DeleteBookCommand { get; }

        /// <summary>
        /// Команда поиска книг, изданных после указанного года.
        /// </summary>
        public ICommand FindBooksAfterYearCommand { get; }

        /// <summary>
        /// Команда группировки (упорядочивания) книг по жанру.
        /// </summary>
        public ICommand GroupByGenreCommand { get; }

        /// <summary>
        /// Команда сброса фильтра по году и возврата к полному списку книг.
        /// </summary>
        public ICommand ResetFilterCommand { get; }

        /// <summary>
        /// Создаёт новый экземпляр BookViewModel.
        /// При инициализации настраивает команды и загружает список книг.
        /// </summary>
        /// <param name="bookService">Сервис работы с книгами.</param>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается, если <paramref name="bookService"/> равен null.
        /// </exception>
        public BookViewModel(IBookService bookService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));

            AddBookCommand = new RelayCommand(
                execute: _ => AddBook(),
                canExecute: _ => CanAddBook()
            );

            UpdateBookCommand = new RelayCommand(
                execute: _ => UpdateBook(),
                canExecute: _ => CanUpdateBook()
            );

            DeleteBookCommand = new RelayCommand(
                execute: _ => DeleteBook(),
                canExecute: _ => CanDeleteBook()
            );

            FindBooksAfterYearCommand = new RelayCommand(
                execute: _ => FindBooksAfterYear(),
                canExecute: _ => CanFindBooksAfterYear()
            );

            GroupByGenreCommand = new RelayCommand(
                execute: _ => GroupBooksByGenre(),
                canExecute: _ => Books.Any()
            );

            ResetFilterCommand = new RelayCommand(
                execute: _ => ResetFilter(),
                canExecute: _ => CanResetFilter()
            );

            LoadBooks();
        }

        /// <summary>
        /// Загружает все книги из сервиса и заполняет коллекцию Books.
        /// Также выбирает первую книгу (если она есть) и обновляет строку статуса.
        /// После загрузки перечитывается доступность команд,
        /// зависящих от наличия книг.
        /// </summary>
        public void LoadBooks()
        {
            Books.Clear();

            var books = _bookService.GetAllBooks();

            foreach (var book in books)
            {
                Books.Add(ToDto(book));
            }

            SelectedBook = Books.Count > 0 ? Books[0] : null;

            StatusMessage = $"Загружено книг: {Books.Count}";

            (GroupByGenreCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (DeleteBookCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (UpdateBookCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Определяет, можно ли сбросить фильтр по году.
        /// Фильтр доступен для сброса только если поле фильтра не пустое.
        /// </summary>
        /// <returns>true, если фильтр задан; иначе false.</returns>
        private bool CanResetFilter()
        {
            return !string.IsNullOrWhiteSpace(YearFilterText);
        }

        /// <summary>
        /// Сбрасывает фильтр по году: очищает YearFilterText,
        /// повторно загружает полный список книг и обновляет строку статуса.
        /// </summary>
        private void ResetFilter()
        {
            YearFilterText = string.Empty;

            LoadBooks();

            StatusMessage = "Фильтр сброшен. Показаны все книги.";
        }

        /// <summary>
        /// Определяет, можно ли добавить новую книгу с текущими данными.
        /// Книга доступна к добавлению, если заполнены обязательные поля:
        /// название, автор и год (в текстовом виде).
        /// </summary>
        /// <returns>
        /// <c>true</c>, если данные заполнены достаточно для добавления книги;
        /// иначе <c>false</c>.
        /// </returns>
        private bool CanAddBook()
        {
            if (string.IsNullOrWhiteSpace(BookTitle))
                return false;

            if (string.IsNullOrWhiteSpace(BookAuthor))
                return false;

            if (string.IsNullOrWhiteSpace(BookYearText))
                return false;

            return true;
        }

        /// <summary>
        /// Добавляет новую книгу, используя данные, введённые пользователем
        /// в свойства <see cref="BookTitle"/>, <see cref="BookAuthor"/>,
        /// <see cref="BookGenre"/> и <see cref="BookYearText"/>.
        /// Выполняет простую проверку корректности года и при успехе
        /// обновляет список книг и строку статуса.
        /// </summary>
        private void AddBook()
        {
            if (!int.TryParse(BookYearText, out int year))
            {
                StatusMessage = "Ошибка: год должен быть целым числом.";
                return;
            }

            if (year <= 0)
            {
                StatusMessage = "Ошибка: год должен быть положительным числом.";
                return;
            }

            var newBook = new Book(
                id: 0, // идентификатор будет присвоен на уровне БД/репозитория
                title: BookTitle,
                author: BookAuthor,
                genre: BookGenre,
                year: year
            );

            try
            {
                _bookService.CreateBook(newBook);

                LoadBooks();

                BookTitle = string.Empty;
                BookAuthor = string.Empty;
                BookGenre = string.Empty;
                BookYearText = string.Empty;

                StatusMessage = "Книга успешно добавлена.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Ошибка при добавлении книги: {ex.Message}";
            }
        }

        /// <summary>
        /// Определяет, можно ли обновить текущую выбранную книгу.
        /// Обновление доступно, если выбрана книга и заполнены
        /// обязательные поля: название, автор и год.
        /// </summary>
        /// <returns>
        /// <c>true</c>, если книга выбрана и данные корректны для обновления;
        /// иначе <c>false</c>.
        /// </returns>
        private bool CanUpdateBook()
        {
            if (SelectedBook == null)
                return false;

            if (string.IsNullOrWhiteSpace(BookTitle))
                return false;

            if (string.IsNullOrWhiteSpace(BookAuthor))
                return false;

            if (string.IsNullOrWhiteSpace(BookYearText))
                return false;

            return true;
        }

        /// <summary>
        /// Обновляет данные выбранной книги на основе значений,
        /// введённых в свойства <see cref="BookTitle"/>,
        /// <see cref="BookAuthor"/>, <see cref="BookGenre"/> и
        /// <see cref="BookYearText"/>.
        /// При успешном обновлении заново загружает список книг
        /// и старается сохранить выбор той же книги.
        /// </summary>
        private void UpdateBook()
        {
            if (SelectedBook == null)
            {
                StatusMessage = "Ошибка: не выбрана книга для обновления.";
                return;
            }

            if (!int.TryParse(BookYearText, out int year))
            {
                StatusMessage = "Ошибка: год должен быть целым числом.";
                return;
            }

            if (year <= 0)
            {
                StatusMessage = "Ошибка: год должен быть положительным числом.";
                return;
            }

            var updatedBook = new Book(
                id: SelectedBook.Id,
                title: BookTitle,
                author: BookAuthor,
                genre: BookGenre,
                year: year
            );

            try
            {
                _bookService.UpdateBook(updatedBook);

                int oldId = SelectedBook.Id;

                LoadBooks();

                SelectedBook = Books.FirstOrDefault(b => b.Id == oldId);

                StatusMessage = "Книга успешно обновлена.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Ошибка при обновлении книги: {ex.Message}";
            }
        }

        /// <summary>
        /// Определяет, можно ли удалить текущую выбранную книгу.
        /// Удаление доступно только при наличии выбранной записи.
        /// </summary>
        /// <returns>
        /// <c>true</c>, если книга выбрана; иначе <c>false</c>.
        /// </returns>
        private bool CanDeleteBook()
        {
            return SelectedBook != null;
        }

        /// <summary>
        /// Удаляет выбранную книгу через сервис <see cref="_bookService"/>,
        /// обновляет список книг и строку статуса.
        /// </summary>
        private void DeleteBook()
        {
            if (SelectedBook == null)
            {
                StatusMessage = "Ошибка: не выбрана книга для удаления.";
                return;
            }

            try
            {
                int idToDelete = SelectedBook.Id;

                _bookService.DeleteBookById(idToDelete);

                LoadBooks();

                StatusMessage = $"Книга с Id={idToDelete} успешно удалена.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Ошибка при удалении книги: {ex.Message}";
            }
        }

        /// <summary>
        /// Определяет, можно ли выполнить поиск книг по году.
        /// Поиск доступен, если поле фильтра по году не пустое.
        /// </summary>
        /// <returns>
        /// <c>true</c>, если задан текст фильтра по году; иначе <c>false</c>.
        /// </returns>
        private bool CanFindBooksAfterYear()
        {
            return !string.IsNullOrWhiteSpace(YearFilterText);
        }

        /// <summary>
        /// Выполняет поиск книг, изданных после указанного в фильтре года.
        /// Заменяет содержимое коллекции <see cref="Books"/> результатами поиска
        /// и обновляет строку статуса.
        /// </summary>
        private void FindBooksAfterYear()
        {
            if (!int.TryParse(YearFilterText, out int year))
            {
                StatusMessage = "Ошибка: год для фильтра должен быть целым числом.";
                return;
            }

            if (year <= 0)
            {
                StatusMessage = "Ошибка: год для фильтра должен быть положительным числом.";
                return;
            }

            try
            {
                var allBooks = _bookService.GetAllBooks();
                var filtered = allBooks.Where(b => b.Year > year);

                Books.Clear();

                foreach (var book in filtered)
                {
                    Books.Add(ToDto(book));
                }

                SelectedBook = Books.Count > 0 ? Books[0] : null;

                StatusMessage = $"Найдено книг: {Books.Count} после {year} года.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Ошибка при поиске книг: {ex.Message}";
            }
        }

        /// <summary>
        /// Группирует (фактически упорядочивает) книги по жанру и названию.
        /// Для этого берёт список книг из сервиса, сортирует по жанру и названию
        /// и заполняет коллекцию <see cref="Books"/> отсортированными элементами.
        /// </summary>
        private void GroupBooksByGenre()
        {
            try
            {
                var allBooks = _bookService.GetAllBooks();

                var grouped = allBooks
                    .OrderBy(b => b.Genre)
                    .ThenBy(b => b.Title);

                Books.Clear();

                foreach (var book in grouped)
                {
                    Books.Add(ToDto(book));
                }

                SelectedBook = Books.Count > 0 ? Books[0] : null;

                StatusMessage = "Книги отсортированы по жанру и названию.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Ошибка при группировке книг: {ex.Message}";
            }
        }

        /// <summary>
        /// Преобразует доменную сущность <see cref="Book"/> в DTO-объект <see cref="BookDto"/>,
        /// используемый слоем представления.
        /// </summary>
        /// <param name="book">Доменная сущность книги.</param>
        /// <returns>Экземпляр <see cref="BookDto"/> с соответствующими полями.</returns>
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
        /// Преобразует DTO-объект <see cref="BookDto"/> обратно в доменную сущность <see cref="Book"/>.
        /// Используется при передаче данных в бизнес-логику или уровень данных.
        /// </summary>
        /// <param name="dto">DTO-объект книги.</param>
        /// <returns>Экземпляр <see cref="Book"/> с теми же значениями полей.</returns>
        public static Book FromDto(BookDto dto)
        {
            return new Book(
                dto.Id,
                dto.Title,
                dto.Author,
                dto.Genre,
                dto.Year
            );
        }
    }
}
