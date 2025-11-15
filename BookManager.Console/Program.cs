namespace BookManager.Console
{
    using BookManager.Core;
    using BookManager.Entities;
    using BookManager.Core.Services;
    using System;
    using System.Text.RegularExpressions;
    using BookManager.DataAccessLayer;
    using Ninject;

    /// <summary>
    /// Предоставляет консольный интерфейс для выполнения CRUD операций и бизнес-функций
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Главная точка входа для консольного приложения
        /// Предоставляет пользователю выбор технологии ORM для работы с данными
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("=== ВЫБОР ТЕХНОЛОГИИ ДОСТУПА К ДАННЫМ ===");
            Console.WriteLine("1 - Entity Framework");
            Console.WriteLine("2 - Dapper");
            Console.Write("Ваш выбор (1 или 2): ");

            var choiceorm = Console.ReadLine();

            // СОЗДАЕМ DI КОНТЕЙНЕР С ВЫБРАННОЙ КОНФИГУРАЦИЕЙ
            bool useEntityFramework = choiceorm != "2"; // true для EF, false для Dapper
            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule(useEntityFramework));

            // ПОЛУЧАЕМ LOGIC ЧЕРЕЗ DI КОНТЕЙНЕР
            var bookService = ninjectKernel.Get<IBookService>();

            bool exitRequested = false;

            while (!exitRequested)
            {
                Console.Clear();
                DisplayMenu();

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllBooks(bookService);
                        break;
                    case "2":
                        AddNewBook(bookService);
                        break;
                    case "3":
                        EditBook(bookService);
                        break;
                    case "4":
                        DeleteBook(bookService);
                        break;
                    case "5":
                        GroupByGenre(bookService);
                        break;
                    case "6":
                        FindBooksByYear(bookService);
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
        private static void ShowAllBooks(IBookService bookService)
        {
            Console.Clear();
            Console.WriteLine("=== ВСЕ КНИГИ ===");

            var books = bookService.GetAllBooks();

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
        private static void AddNewBook(IBookService bookService)
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
                        bookService.CreateBook(newBook);
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
        private static void EditBook(IBookService bookService)
        {
            Console.Clear();
            Console.WriteLine("=== РЕДАКТИРОВАНИЕ КНИГИ ===");

            ShowAllBooks(bookService);
            Console.Write("\nВведите ID книги для редактирования: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var existingBook = bookService.GetBookById(id);

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
                        }
                        else
                        {
                            Console.WriteLine("Год должен быть правильным!");
                        }
                    }
                    bookService.UpdateBook(existingBook);
                    Console.WriteLine("Книга успешно обновлена!");
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
        private static void DeleteBook(IBookService bookService)
        {
            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ КНИГИ ===");

            ShowAllBooks(bookService);
            Console.Write("\nВведите ID книги для удаления: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var existingBook = bookService.GetBookById(id);

                if (existingBook != null)
                {
                    Console.WriteLine($"Вы уверены, что хотите удалить: {existingBook}? (напишите 'ДА')");
                    var confirmation = Console.ReadLine();

                    if (confirmation?.ToUpper() == "ДА")
                    {
                        bookService.DeleteBookById(id);
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
        private static void GroupByGenre(IBookService bookService)
        {
            Console.Clear();
            Console.WriteLine("=== ГРУППИРОВКА ПО ЖАНРАМ ===");

            var booksByGenre = bookService.GroupBooksByGenre();

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
        private static void FindBooksByYear(IBookService bookService)
        {
            Console.Clear();
            Console.WriteLine("=== ПОИСК КНИГ ПО ГОДУ ===");

            Console.Write("Введите год: книги, изданные ПОСЛЕ этого года будут показаны: ");

            if (int.TryParse(Console.ReadLine(), out int year))
            {
                var books = bookService.FindBooksPublishedAfterYear(year);

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
