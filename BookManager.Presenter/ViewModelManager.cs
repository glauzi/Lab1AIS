using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace BookManager.Presenter
{
    /// <summary>
    /// Менеджер вью-моделей.
    /// Отвечает за работу с основными ViewModel в варианте ViewModelFirst.
    /// </summary>
    public class ViewModelManager
    {
        /// <summary>
        /// Главная ViewModel приложения.
        /// </summary>
        private readonly BookViewModel _mainViewModel;

        /// <summary>
        /// Событие, возникающее при готовности основной ViewModel.
        /// View-слой (ViewManager) подписывается на это событие.
        /// </summary>
        public event Action<BookViewModel>? MainViewModelCreated;

        /// <summary>
        /// Создаёт новый экземпляр <see cref="ViewModelManager"/>.
        /// </summary>
        /// <param name="mainViewModel">Готовая к использованию ViewModel.</param>
        public ViewModelManager(BookViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel
                             ?? throw new ArgumentNullException(nameof(mainViewModel));
        }

        /// <summary>
        /// Точка старта менеджера ViewModel.
        /// Генерирует событие о готовности основной ViewModel.
        /// </summary>
        public void Run()
        {
            MainViewModelCreated?.Invoke(_mainViewModel);
        }
    }
}
