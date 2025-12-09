using System;
using System.Windows;
using Ninject;
using BookManager.Core;
using BookManager.Core.Services;
using BookManager.Presenter;

namespace BookManager.Wpf
{
    /// <summary>
    /// Класс приложения WPF.
    /// Здесь находится композиционный корень: настройка DI
    /// и запуск схемы ViewModelFirst через ViewModelManager и ViewManager.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Контейнер зависимостей Ninject на время жизни приложения.
        /// </summary>
        private IKernel? _kernel;

        /// <summary>
        /// Точка входа WPF-приложения.
        /// Вызывается при старте (согласно атрибуту Startup в App.xaml).
        /// </summary>
        /// <param name="e">Аргументы запуска приложения.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _kernel = CreateKernel();

            // 1) Берём бизнес-логику из слоя Core.
            var bookService = _kernel.Get<IBookService>();

            // 2) Создаём главную ViewModel и передаём ей сервис.
            var bookViewModel = new BookViewModel(bookService);

            // 3) Создаём менеджер ViewModel, который будет управлять этой VM.
            var viewModelManager = new ViewModelManager(bookViewModel);

            // 4) Создаём менеджер View, который подписывается на события VMManager.
            var viewManager = new ViewManager(viewModelManager);

            // 5) Запускаем цепочку ViewModelFirst.
            viewManager.Run();
        }

        /// <summary>
        /// Создаёт и настраивает контейнер зависимостей Ninject.
        /// Здесь подключаем модуль конфигурации из Core (DAL + BL).
        /// </summary>
        /// <returns>Инициализированный контейнер IKernel.</returns>
        private static IKernel CreateKernel()
        {
            bool useEntityFramework = true;

            var kernel = new StandardKernel(new SimpleConfigModule(useEntityFramework));
            return kernel;
        }

        /// <summary>
        /// Освобождает ресурсы контейнера при завершении работы приложения.
        /// </summary>
        /// <param name="e">Аргументы завершения.</param>
        protected override void OnExit(ExitEventArgs e)
        {
            _kernel?.Dispose();
            base.OnExit(e);
        }
    }
}
