using BookManager.Entities;
using System;
using System.Collections.Generic;
using Ninject;
using BookManager.Shared;

namespace BookManager.WinForms
{
    /// <summary>
    /// Главная форма приложения для управления книгами
    /// Предоставляет графический интерфейс для выполнения CRUD операций и бизнес-функций
    /// </summary>
    public partial class Form1 : Form, IBookView
    {
        public event EventHandler? ViewLoaded;
        public event EventHandler? AddBookRequested;
        public event EventHandler? UpdateBookRequested;
        public event EventHandler? DeleteBookRequested;
        public event EventHandler? GroupByGenreRequested;
        public event EventHandler? FindBooksByYearRequested;
        public event EventHandler? SelectedBookChanged;

        public Form1()
        {
            InitializeComponent();
        }
        public string BookTitle
        {
            get => textBoxTitle.Text;
            set => textBoxTitle.Text = value;
        }

        public string BookAuthor
        {
            get => textBoxAuthor.Text;
            set => textBoxAuthor.Text = value;
        }

        public string BookGenre
        {
            get => textBoxGenre.Text;
            set => textBoxGenre.Text = value;
        }

        public string BookYearText
        {
            get => textBoxYear.Text;
            set => textBoxYear.Text = value;
        }

        public string YearFilterText
        {
            get => textBoxYearFilter.Text;
            set => textBoxYearFilter.Text = value;
        }

        public Book? SelectedBook
        {
            get
            {
                if (dataGridViewBooks.SelectedRows.Count > 0)
                    return dataGridViewBooks.SelectedRows[0].DataBoundItem as Book;

                return null;
            }
        }

        public void ShowBooks(IList<Book> books)
        {
            dataGridViewBooks.DataSource = null;
            dataGridViewBooks.DataSource = books;
        }

        public void ShowBooksByGenre(Dictionary<string, List<Book>> booksByGenre)
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

        public void ShowBooksAfterYear(IList<Book> books, int year)
        {
            listBoxYearResults.Items.Clear();

            if (books.Count > 0)
            {
                listBoxYearResults.Items.Add($"Книги после {year} года:");
                foreach (var book in books)
                    listBoxYearResults.Items.Add(book.ToString());
            }
            else
            {
                listBoxYearResults.Items.Add("Книги не найдены");
            }
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void ClearBookInputFields()
        {
            textBoxTitle.Clear();
            textBoxAuthor.Clear();
            textBoxGenre.Clear();
            textBoxYear.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ViewLoaded?.Invoke(this, EventArgs.Empty);
        }

        private void dataGridViewBooks_SelectionChanged(object sender, EventArgs e)
        {
            SelectedBookChanged?.Invoke(this, EventArgs.Empty);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddBookRequested?.Invoke(this, EventArgs.Empty);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            UpdateBookRequested?.Invoke(this, EventArgs.Empty);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteBookRequested?.Invoke(this, EventArgs.Empty);
        }

        private void buttonGroupByGenre_Click(object sender, EventArgs e)
        {
            GroupByGenreRequested?.Invoke(this, EventArgs.Empty);
        }

        private void buttonFindByYear_Click(object sender, EventArgs e)
        {
            FindBooksByYearRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
