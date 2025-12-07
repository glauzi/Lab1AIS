using BookManager.Core.Services;
using BookManager.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Wpf
{
    /// <summary>
    /// Менеджер представлений (View).
    /// В варианте ViewModelFirst сам создаёт менеджер ViewModel,
    /// подписывается на его события и по типу ViewModel выбирает соответствующее окно.
    /// </summary>
    public class ViewManager
    {
        private readonly ViewModelManager _viewModelManager;

        /// <summary>
        /// Сопоставление типов ViewModel и фабрик окон (View),
        /// позволяющее по типу VM создать нужное окно.
        /// </summary>
        private readonly Dictionary<Type, Func<BaseWindow>> _viewFactories =
            new Dictionary<Type, Func<BaseWindow>>();

        /// <summary>
        /// Создаёт новый экземпляр <see cref="ViewManager"/>.
        /// </summary>
        /// <param name="bookService">
        /// Сервис работы с книгами из слоя Core.
        /// На его основе будет создан менеджер ViewModel.
        /// </param>
        public ViewManager(IBookService bookService)
        {
            if (bookService == null)
                throw new ArgumentNullException(nameof(bookService));

            // (2) Здесь ViewManager сам создаёт VMManager
            _viewModelManager = new ViewModelManager(bookService);

            // Подписываемся на событие готовности основной ViewModel
            _viewModelManager.MainViewModelCreated += OnMainViewModelCreated;

            // Регистрируем соответствия ViewModel -> View
            ConfigureViewFactories();
        }

        /// <summary>
        /// Точка входа для запуска менеджера представлений.
        /// Делегирует запуск менеджеру ViewModel.
        /// </summary>
        public void Run()
        {
            // (1) Запуск цепочки: дальше управление перейдёт к ViewModelManager.
            _viewModelManager.Run();
        }

        /// <summary>
        /// Настраивает сопоставления между ViewModel и View.
        /// При необходимости здесь можно зарегистрировать несколько пар.
        /// </summary>
        private void ConfigureViewFactories()
        {
            // В нашей лабораторной одна главная VM: BookViewModel - MainWindow
            _viewFactories[typeof(BookViewModel)] = () => new MainWindow();
        }

        /// <summary>
        /// Обработчик события создания основной ViewModel.
        /// Здесь по типу ViewModel выбирается соответствующее окно,
        /// создаётся экземпляр и запускается с переданным контекстом.
        /// </summary>
        /// <param name="viewModel">Готовая к работе ViewModel.</param>
        private void OnMainViewModelCreated(BookViewModel viewModel)
        {
            var vmType = viewModel.GetType();

            if (!_viewFactories.TryGetValue(vmType, out var viewFactory))
            {
                var fallbackWindow = new MainWindow
                {
                    DataContext = viewModel
                };
                fallbackWindow.Show();
                return;
            }

            // Создаём окно через фабрику
            BaseWindow window = viewFactory();

            // Передаём контекст ViewModel
            window.DataContext = viewModel;

            // Показываем окно
            window.Show();
        }
    }
}
