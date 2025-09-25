using BookManager.Core.Services;
using BookManager.Core.Models;
using System;
using System.Collections.Generic;
namespace BookManager.WinForms
{
    public partial class Form1 : Form
    {
        private Logic _logic = new Logic();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (_logic.GetAllBooks().Count == 0)
                AddSampleData();
            LoadBooksToGrid();
        }

        private void AddSampleData()
        {
            _logic.CreateBook(new Book(0, "Война и мир", "Лев Толстой", "Роман", 1869));
            _logic.CreateBook(new Book(0, "Преступление и наказание", "Федор Достоевский", "Роман", 1866));
            _logic.CreateBook(new Book(0, "Мастер и Маргарита", "Михаил Булгаков", "Фантастика", 1967));
            _logic.CreateBook(new Book(0, "1984", "Джордж Оруэлл", "Антиутопия", 1949));
            _logic.CreateBook(new Book(0, "Гарри Поттер", "Джоан Роулинг", "Фэнтези", 1997));
        }

        private void LoadBooksToGrid()
        {
            dataGridViewBooks.DataSource = _logic.GetAllBooks();
        }

        private void dataGridViewBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewBooks.SelectedRows.Count > 0)
            {
                var selectedBook = dataGridViewBooks.SelectedRows[0].DataBoundItem as Book;
                if (selectedBook != null)
                {
                    textBoxTitle.Text = selectedBook.Title;
                    textBoxAuthor.Text = selectedBook.Author;
                    textBoxGenre.Text = selectedBook.Genre;
                    textBoxYear.Text = selectedBook.Year.ToString();
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(textBoxYear.Text, out int year) && year >= 0 && year <= 2025)
                {
                    var newBook = new Book(0, textBoxTitle.Text, textBoxAuthor.Text, textBoxGenre.Text, year);
                    _logic.CreateBook(newBook);
                    LoadBooksToGrid();
                    ClearInputFields();
                    MessageBox.Show("Книга добавлена!");
                }
                else
                {
                    MessageBox.Show("Год должен быть числом от 0 до 2025!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите книгу для редактирования!");
                return;
            }

            try
            {
                var selectedBook = dataGridViewBooks.SelectedRows[0].DataBoundItem as Book;
                if (selectedBook != null && int.TryParse(textBoxYear.Text, out int year))
                {
                    var updatedBook = new Book(selectedBook.Id, textBoxTitle.Text, textBoxAuthor.Text, textBoxGenre.Text, year);
                    _logic.UpdateBook(updatedBook);
                    LoadBooksToGrid();
                    MessageBox.Show("Книга обновлена!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении: {ex.Message}");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите книгу для удаления!");
                return;
            }

            try
            {
                var selectedBook = dataGridViewBooks.SelectedRows[0].DataBoundItem as Book;
                if (selectedBook != null)
                {
                    var result = MessageBox.Show($"Удалить книгу: {selectedBook.Title}?", "Подтверждение", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        _logic.DeleteBookById(selectedBook.Id);
                        LoadBooksToGrid();
                        ClearInputFields();
                        MessageBox.Show("Книга удалена!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}");
            }
        }

        private void buttonGroupByGenre_Click(object sender, EventArgs e)
        {
            try
            {
                var booksByGenre = _logic.GroupBooksByGenre();
                listBoxGenres.Items.Clear();

                foreach (var genreGroup in booksByGenre)
                {
                    listBoxGenres.Items.Add($"=== {genreGroup.Key} ===");
                    foreach (var book in genreGroup.Value)
                        listBoxGenres.Items.Add($"  {book.Title} ({book.Year})");
                    listBoxGenres.Items.Add("");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void buttonFindByYear_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxYearFilter.Text, out int year))
            {
                try
                {
                    var books = _logic.FindBooksPublishedAfterYear(year);
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
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Введите корректный год!");
            }
        }

        private void ClearInputFields()
        {
            textBoxTitle.Clear();
            textBoxAuthor.Clear();
            textBoxGenre.Clear();
            textBoxYear.Clear();
        }
    }
}
