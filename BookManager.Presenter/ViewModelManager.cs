using BookManager.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Presenter
{
    /// <summary>
    /// Менеджер вью-моделей.
    /// Отвечает за создание и инициализацию ViewModel
    /// в варианте ViewModelFirst.
    /// </summary>
    public class ViewModelManager
    {
        private readonly IBookService _bookService;

        /// <summary>
        /// Событие, возникающее при создании и готовности
        /// основной ViewModel приложения.
        /// </summary>
        public event Action<BookViewModel>? MainViewModelCreated;

        /// <summary>
        /// Создаёт новый экземпляр <see cref="ViewModelManager"/>.
        /// </summary>
        /// <param name="bookService">Сервис работы с книгами.</param>
        public ViewModelManager(IBookService bookService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        /// <summary>
        /// Точка старта менеджера ViewModel.
        /// Создаёт основную ViewModel, подготавливает её к работе
        /// и посылает событие для View-слоя.
        /// </summary>
        public void Run()
        {
            // 3) Создаём главную VM. В конструкторе BookViewModel
            // уже вызывается LoadBooks(), т.е. подготовка к работе идёт внутри.
            var mainViewModel = new BookViewModel(_bookService);

            // 4–5) Посылаем событие о готовности VM
            // (ViewManager в WPF позже подпишется на это событие).
            MainViewModelCreated?.Invoke(mainViewModel);
        }
    }
}
