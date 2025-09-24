namespace BookManager.Console
{
    using BookManager.Core;
    using BookManager.Core.Models;
    using BookManager.Core.Services;
    using System;
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создаем экземпляр Logic для тестирования
            var logic = new Logic();

            // Тестируем создание книг
            logic.CreateBook(new Book(0, "Война и мир", "Лев Толстой", "Роман", 1869));
            logic.CreateBook(new Book(0, "Преступление и наказание", "Федор Достоевский", "Роман", 1866));
            logic.CreateBook(new Book(0, "Мастер и Маргарита", "Михаил Булгаков", "Фантастика", 1967));
            logic.CreateBook(new Book(0, "1984", "Джордж Оруэлл", "Антиутопия", 1949));

            Console.WriteLine("=== Все книги ===");
            var allBooks = logic.GetAllBooks();
            foreach (var book in allBooks)
            {
                Console.WriteLine(book);
            }

            Console.WriteLine("\n=== Поиск книги по ID ===");
            var foundBook = logic.GetBookById(2);
            Console.WriteLine(foundBook != null ? foundBook.ToString() : "Книга не найдена");

            Console.WriteLine("\n=== Группировка по жанрам ===");
            var booksByGenre = logic.GroupBooksByGenre();
            foreach (var genre in booksByGenre)
            {
                Console.WriteLine($"\n{genre.Key}:");
                foreach (var book in genre.Value)
                {
                    Console.WriteLine($"  {book.Title}");
                }
            }

            Console.WriteLine("\n=== Книги после 1900 года ===");
            var modernBooks = logic.FindBooksPublishedAfterYear(1900);
            foreach (var book in modernBooks)
            {
                Console.WriteLine(book);
            }

            Console.WriteLine("\n=== Тест обновления ===");
            var bookToUpdate = new Book(1, "Война и мир ТОМ 1", "Лев Толстой", "Классика", 1869);
            logic.UpdateBook(bookToUpdate);
            Console.WriteLine("После обновления: " + logic.GetBookById(1));

            Console.WriteLine("\n=== Тест удаления ===");
            logic.DeleteBookById(3);
            Console.WriteLine("После удаления книги с ID=3:");
            foreach (var book in logic.GetAllBooks())
            {
                Console.WriteLine(book);
            }

            Console.ReadLine();
        }
    }
}
