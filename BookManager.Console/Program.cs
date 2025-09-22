namespace BookManager.Console
{
    using BookManager.Core;
    using System;
    internal class Program
    {
        static void Main(string[] args)
        {
            var testService = new TestServices();
            Console.WriteLine(testService.GetTestMessage());
            Console.ReadLine();
        }
    }
}
