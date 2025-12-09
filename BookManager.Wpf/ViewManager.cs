using System;
using System.Collections.Generic;
using BookManager.Presenter;

namespace BookManager.Wpf
{
    /// <summary>
    /// Менеджер представлений (View).
    /// Подписывается на события ViewModelManager и
    /// управляет окнами WPF.
    /// Работает только с ViewModel и View, без зависимостей от Core.
    /// </summary>
    public class ViewManager
    {
        private readonly ViewModelManager _viewModelManager;

        /// <summary>
        /// Сопоставление ViewModel и соответствующих им окон (View).
        /// Позволяет по экземпляру VM найти и управлять её View.
        /// </summary>
        private readonly Dictionary<ViewModelBase, BaseWindow> _views =
            new Dictionary<ViewModelBase, BaseWindow>();

        /// <summary>
        /// Создаёт новый экземпляр <see cref="ViewManager"/>.
        /// </summary>
        /// <param name="viewModelManager">
        /// Менеджер ViewModel, который создаёт и инициализирует VM.
        /// </param>
        public ViewManager(ViewModelManager viewModelManager)
        {
            _viewModelManager = viewModelManager
                                ?? throw new ArgumentNullException(nameof(viewModelManager));

            // Подписываемся на событие создания основной ViewModel
            _viewModelManager.MainViewModelCreated += OnMainViewModelCreated;
        }

        /// <summary>
        /// Точка входа для запуска менеджера представлений.
        /// Делегирует запуск менеджеру ViewModel.
        /// </summary>
        public void Run()
        {
            _viewModelManager.Run();
        }

        /// <summary>
        /// Обработчик события создания основной ViewModel.
        /// Здесь создаётся и отображается окно, соответствующее данной VM,
        /// и регистрируется связь ViewModel -> View.
        /// </summary>
        /// <param name="viewModel">Готовая к работе ViewModel.</param>
        private void OnMainViewModelCreated(BookViewModel viewModel)
        {
            // Создаём окно для этой ViewModel
            var mainWindow = new MainWindow
            {
                DataContext = viewModel
            };

            // Регистрируем связь VM - View
            _views[viewModel] = mainWindow;

            // Показываем окно пользователю
            mainWindow.Show();
        }
    }
}
