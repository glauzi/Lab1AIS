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
    /// Получает события от <see cref="ViewModelManager"/>
    /// и создаёт соответствующие окна WPF.
    /// </summary>
    public class ViewManager
    {
        private readonly ViewModelManager _viewModelManager;

        /// <summary>
        /// Создаёт новый экземпляр <see cref="ViewManager"/>.
        /// </summary>
        /// <param name="viewModelManager">
        /// Менеджер ViewModel, который отвечает за их создание и инициализацию.
        /// </param>
        public ViewManager(ViewModelManager viewModelManager)
        {
            _viewModelManager = viewModelManager
                                ?? throw new ArgumentNullException(nameof(viewModelManager));

            // (2) Подписываемся на событие создания основной VM.
            _viewModelManager.MainViewModelCreated += OnMainViewModelCreated;
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
        /// Обработчик события создания основной ViewModel.
        /// Здесь выбирается соответствующее окно и запускается с переданным контекстом.
        /// </summary>
        /// <param name="viewModel">Готовая к работе ViewModel.</param>
        private void OnMainViewModelCreated(BookViewModel viewModel)
        {
            // (6) Создаём нужное окно для данной VM и передаём контекст.
            // Пока у нас одна основная ViewModel -> MainWindow.
            var mainWindow = new MainWindow
            {
                DataContext = viewModel
            };

            mainWindow.Show();
        }
    }
}
