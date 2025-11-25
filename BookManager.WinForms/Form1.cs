using BookManager.Entities;
using System;
using System.Collections.Generic;
using Ninject;
using BookManager.Shared;

namespace BookManager.WinForms
{
    /// <summary>
    /// Представление (View) в архитектуре MVP.
    /// Форма отвечает только за отображение данных и генерацию событий
    /// пользовательского интерфейса. Не содержит бизнес-логики.
    /// Вся логика обработки запросов пользователя реализована в BookPresenter.
    /// </summary>
    public partial class Form1 : Form, IBookView
    {
        /// <summary>
        /// Событие вызывается при загрузке формы.
        /// </summary>
        public event EventHandler? ViewLoaded;

        /// <summary>
        /// Событие вызывается, когда пользователь нажимает кнопку "Добавить".
        /// </summary>
        public event EventHandler? AddBookRequested;

        /// <summary>
        /// Событие вызывается при запросе обновления выбранной книги.
        /// </summary>
        public event EventHandler? UpdateBookRequested;

        /// <summary>
        /// Событие вызывается при удалении книги.
        /// </summary>
        public event EventHandler? DeleteBookRequested;

        /// <summary>
        /// Событие вызывается для группировки книг по жанрам.
        /// </summary>
        public event EventHandler? GroupByGenreRequested;

        /// <summary>
        /// Событие вызывается для поиска книг, опубликованных после указанного года.
        /// </summary>
        public event EventHandler? FindBooksByYearRequested;

        /// <summary>
        /// Событие вызывается при смене выделенной строки в таблице.
        /// </summary>
        public event EventHandler? SelectedBookChanged;

        /// <summary>
        /// Создаёт экземпляр формы представления.
        /// Инициализирует компоненты UI. Логики не содержит.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Заголовок книги, вводимый пользователем.
        /// </summary>
        public string BookTitle
        {
            get => textBoxTitle.Text;
            set => textBoxTitle.Text = value;
        }

        /// <summary>
        /// Автор книги, вводимый пользователем.
        /// </summary>
        public string BookAuthor
        {
            get => textBoxAuthor.Text;
            set => textBoxAuthor.Text = value;
        }

        /// <summary>
        /// Жанр книги, вводимый пользователем.
        /// </summary>
        public string BookGenre
        {
            get => textBoxGenre.Text;
            set => textBoxGenre.Text = value;
        }

        /// <summary>
        /// Год издания книги, вводимый пользователем как строка.
        /// Презентер выполняет валидацию и преобразование.
        /// </summary>
        public string BookYearText
        {
            get => textBoxYear.Text;
            set => textBoxYear.Text = value;
        }

        /// <summary>
        /// Значение фильтра года для поиска книг.
        /// </summary>
        public string YearFilterText
        {
            get => textBoxYearFilter.Text;
            set => textBoxYearFilter.Text = value;
        }

        /// <summary>
        /// Возвращает книгу, выбранную в таблице.
        /// Если строка не выбрана — возвращает null.
        /// </summary>
        public BookDto? SelectedBook
        {
            get
            {
                if (dataGridViewBooks.SelectedRows.Count > 0)
                    return dataGridViewBooks.SelectedRows[0].DataBoundItem as BookDto;

                return null;
            }
        }

        /// <summary>
        /// Отображает список книг в таблице.
        /// Презентер вызывает этот метод после получения данных из модели.
        /// </summary>
        public void ShowBooks(IList<BookDto> books)
        {
            dataGridViewBooks.DataSource = null;
            dataGridViewBooks.DataSource = books;
        }

        /// <summary>
        /// Отображает сгруппированный по жанрам список книг.
        /// Используется презентером при выполнении группировки.
        /// </summary>
        public void ShowBooksByGenre(Dictionary<string, List<BookDto >> booksByGenre)
        {
            listBoxGenres.Items.Clear();

            foreach (var genreGroup in booksByGenre)
            {
                listBoxGenres.Items.Add($"=== {genreGroup.Key} ===");
                foreach (var book in genreGroup.Value)
                    listBoxGenres.Items.Add($"  {book.Title} ({book.Year})");
                listBoxGenres.Items.Add(string.Empty);
            }
        }

        /// <summary>
        /// Отображает список книг, опубликованных после указанного года.
        /// Используется презентером при поиске.
        /// </summary>
        public void ShowBooksAfterYear(IList<BookDto> books, int year)
        {
            listBoxYearResults.Items.Clear();

            if (books.Count > 0)
            {
                listBoxYearResults.Items.Add($"Книги после {year} года:");
                foreach (var book in books)
                    listBoxYearResults.Items.Add($"{book.Title} — {book.Author} ({book.Genre}, {book.Year})");
            }
            else
            {
                listBoxYearResults.Items.Add("Книги не найдены");
            }
        }

        /// <summary>
        /// Отображает всплывающее сообщение пользователю.
        /// </summary>
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        /// <summary>
        /// Очищает поля ввода книги.
        /// Презентер вызывает этот метод после успешного добавления или удаления.
        /// </summary>
        public void ClearBookInputFields()
        {
            textBoxTitle.Clear();
            textBoxAuthor.Clear();
            textBoxGenre.Clear();
            textBoxYear.Clear();
        }

        /// <summary>
        /// Пытается считать и провалидировать год издания книги из поля ввода.
        /// В случае некорректного ввода показывает пользователю сообщение об ошибке.
        /// </summary>
        /// <param name="year">Считанное значение года при успешной валидации.</param>
        /// <returns>
        /// true, если год успешно считан и проходит валидацию;
        /// false, если ввод некорректен или вне допустимого диапазона.
        /// </returns>
        public bool TryGetBookYear(out int year)
        {
            string yearText = BookYearText;

            if (!int.TryParse(yearText, out year))
            {
                ShowMessage("Год должен быть числом!");
                return false;
            }

            if (year < 0 || year > 2025)
            {
                ShowMessage("Год должен быть от 0 до 2025!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Пытается считать и провалидировать год для фильтрации книг при поиске.
        /// В случае некорректного ввода показывает пользователю сообщение об ошибке.
        /// </summary>
        /// <param name="year">Считанное значение года фильтра при успешной валидации.</param>
        /// <returns>
        /// true, если год успешно считан и проходит валидацию;
        /// false, если ввод некорректен или вне допустимого диапазона.
        /// </returns>
        public bool TryGetFilterYear(out int year)
        {
            string yearText = YearFilterText;

            if (!int.TryParse(yearText, out year))
            {
                ShowMessage("Введите корректный год для поиска!");
                return false;
            }

            if (year < 0 || year > 2025)
            {
                ShowMessage("Год должен быть от 0 до 2025!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Обработчик загрузки формы.
        /// Не содержит логики — только вызывает событие ViewLoaded.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            ViewLoaded?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик смены выделенной строки в таблице.
        /// Генерирует событие SelectedBookChanged,
        /// которое обрабатывается презентером.
        /// </summary>
        private void dataGridViewBooks_SelectionChanged(object sender, EventArgs e)
        {
            SelectedBookChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Генерирует событие AddBookRequested при нажатии кнопки "Добавить".
        /// </summary>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddBookRequested?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Генерирует событие UpdateBookRequested при нажатии кнопки "Обновить".
        /// </summary>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            UpdateBookRequested?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Генерирует событие DeleteBookRequested при нажатии кнопки "Удалить".
        /// </summary>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteBookRequested?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Генерирует событие GroupByGenreRequested для выполнения группировки.
        /// </summary>
        private void buttonGroupByGenre_Click(object sender, EventArgs e)
        {
            GroupByGenreRequested?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Генерирует событие FindBooksByYearRequested для выполнения поиска.
        /// </summary>
        private void buttonFindByYear_Click(object sender, EventArgs e)
        {
            FindBooksByYearRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
