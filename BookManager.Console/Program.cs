namespace BookManager.Console
{
    using BookManager.Core;
    using BookManager.Core.Models;
    using BookManager.Core.Services;
    using System;
    using System.Text.RegularExpressions;

    internal class Program
    {
        private static Logic _logic = new Logic();
        static void Main(string[] args)
        {
            AddSampleData();

            bool exitRequested = false;

            while (!exitRequested)
            {
                Console.Clear();
                DisplayMenu();

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllBooks();
                        break;
                    case "2":
                        AddNewBook();
                        break;
                    case "3":
                        EditBook();
                        break;
                    case "4":
                        DeleteBook();
                        break;
                    case "5":
                        GroupByGenre();
                        break;
                    case "6":
                        FindBooksByYear();
                        break;
                    case "0":
                        exitRequested = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор! Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
            Console.WriteLine("До встречи в следующий раз!");
        }
        /// <summary>
        /// Добавляем изначальные данные
        /// </summary>
        private static void AddSampleData()
        {
            _logic.CreateBook(new Book(0, "Война и мир", "Лев Толстой", "Роман", 1869));
            _logic.CreateBook(new Book(0, "Преступление и наказание", "Федор Достоевский", "Роман", 1866));
            _logic.CreateBook(new Book(0, "Мастер и Маргарита", "Михаил Булгаков", "Фантастика", 1967));
            _logic.CreateBook(new Book(0, "1984", "Джордж Оруэлл", "Антиутопия", 1949));
        }
        /// <summary>
        /// Вывод меню
        /// </summary>
        private static void DisplayMenu()
        {
            Console.WriteLine("=== КНИЖНЫЙ КАТАЛОГ ===");
            Console.WriteLine("1. Показать все книги");
            Console.WriteLine("2. Добавить новую книгу");
            Console.WriteLine("3. Редактировать книгу");
            Console.WriteLine("4. Удалить книгу");
            Console.WriteLine("5. Группировать по жанрам");
            Console.WriteLine("6. Найти книги по году");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");
        }

        /// <summary>
        /// Вывод всех книг
        /// </summary>
        private static void ShowAllBooks()
        {
            Console.Clear();
            Console.WriteLine("=== ВСЕ КНИГИ ===");

            var books = _logic.GetAllBooks();

            if (books.Count == 0)
            {
                Console.WriteLine("Книги не найдены.");
            }
            else
            {
                foreach (var book in books)
                {
                    Console.WriteLine(book);
                }
            }

            WaitForPress();
        }
        /// <summary>
        /// Добавление книги
        /// </summary>
        private static void AddNewBook()
        {
            Console.Clear();
            Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОЙ КНИГИ ===");

            try
            {
                Console.Write("Введите название: ");
                var title = Console.ReadLine();

                Console.Write("Введите автора: ");
                var author = Console.ReadLine();

                Console.Write("Введите жанр: ");
                var genre = Console.ReadLine();

                Console.Write("Введите год издания: ");
                var yearInput = Console.ReadLine();

                if (int.TryParse(yearInput, out int year))
                {
                    if (year >= 0 && year <= 2025)
                    {
                        var newBook = new Book(0, title, author, genre, year);
                        _logic.CreateBook(newBook);
                        Console.WriteLine("Книга успешно добавлена!");
                    }
                    else
                    {
                        Console.WriteLine("Введите правильный год!");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: год должен быть числом!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении книги: {ex.Message}");
            }

            WaitForPress();
        }
        /// <summary>
        /// Редактирование книги
        /// </summary>
        private static void EditBook()
        {
            Console.Clear();
            Console.WriteLine("=== РЕДАКТИРОВАНИЕ КНИГИ ===");

            ShowAllBooks();
            Console.Write("\nВведите ID книги для редактирования: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var existingBook = _logic.GetBookById(id);

                if (existingBook != null)
                {
                    Console.WriteLine($"Редактирование: {existingBook}");

                    Console.Write("Новое название (Enter - оставить текущее): ");
                    var title = Console.ReadLine();
                    if (!string.IsNullOrEmpty(title))
                        existingBook.Title = title;

                    Console.Write("Новый автор (Enter - оставить текущего): ");
                    var author = Console.ReadLine();
                    if (!string.IsNullOrEmpty(author))
                        existingBook.Author = author;

                    Console.Write("Новый жанр (Enter - оставить текущий): ");
                    var genre = Console.ReadLine();
                    if (!string.IsNullOrEmpty(genre))
                        existingBook.Genre = genre;

                    Console.Write("Новый год (Enter - оставить текущий): ");
                    var yearInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(yearInput) && int.TryParse(yearInput, out int year))
                    {
                        if (year >= 0 && year <= 2025)
                        {
                            existingBook.Year = year;
                            _logic.UpdateBook(existingBook);
                            Console.WriteLine("Книга успешно обновлена!");
                        }
                        else
                        {
                            Console.WriteLine("Год должен быть правильным!");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Книга с указанным ID не найдена!");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: ID должен быть числом!");
            }

            WaitForPress();
        }
        /// <summary>
        /// Удаление книги
        /// </summary>
        private static void DeleteBook()
        {
            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ КНИГИ ===");

            ShowAllBooks();
            Console.Write("\nВведите ID книги для удаления: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var existingBook = _logic.GetBookById(id);

                if (existingBook != null)
                {
                    Console.WriteLine($"Вы уверены, что хотите удалить: {existingBook}? (напишите 'ДА')");
                    var confirmation = Console.ReadLine();

                    if (confirmation?.ToUpper() == "ДА")
                    {
                        _logic.DeleteBookById(id);
                        Console.WriteLine("Книга успешно удалена!");
                    }
                    else
                    {
                        Console.WriteLine("Удаление отменено.");
                    }
                }
                else
                {
                    Console.WriteLine("Книга с указанным ID не найдена!");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: ID должен быть числом!");
            }

            WaitForPress();
        }
        /// <summary>
        /// Группировка книг по жанрам
        /// </summary>
        private static void GroupByGenre()
        {
            Console.Clear();
            Console.WriteLine("=== ГРУППИРОВКА ПО ЖАНРАМ ===");

            var booksByGenre = _logic.GroupBooksByGenre();

            if (booksByGenre.Count == 0)
            {
                Console.WriteLine("Книги не найдены.");
            }
            else
            {
                foreach (var genreGroup in booksByGenre)
                {
                    Console.WriteLine($"\n--- {genreGroup.Key} ---");
                    foreach (var book in genreGroup.Value)
                    {
                        Console.WriteLine($"  {book.Title} ({book.Year})");
                    }
                }
            }

            WaitForPress();
        }
        /// <summary>
        /// Поиск книг по году
        /// </summary>
        private static void FindBooksByYear()
        {
            Console.Clear();
            Console.WriteLine("=== ПОИСК КНИГ ПО ГОДУ ===");

            Console.Write("Введите год: книги, изданные ПОСЛЕ этого года будут показаны: ");

            if (int.TryParse(Console.ReadLine(), out int year))
            {
                var books = _logic.FindBooksPublishedAfterYear(year);

                if (books.Count == 0)
                {
                    Console.WriteLine($"Книги, изданные после {year} года, не найдены.");
                }
                else
                {
                    Console.WriteLine($"\nКниги, изданные после {year} года:");
                    foreach (var book in books)
                    {
                        Console.WriteLine(book);
                    }
                }
            }
            else
            {
                Console.WriteLine("Ошибка: год должен быть числом!");
            }

            WaitForPress();
        }
        /// <summary>
        /// Ожидает нажатие любой клавиши
        /// </summary>
        private static void WaitForPress()
        {
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}
