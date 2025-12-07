using System;
using System.Windows;
using Ninject;
using BookManager.Core;
using BookManager.Core.Services;          // пространство имён, где лежит SimpleConfigModule
using BookManager.Presenter;     // BookViewModel

namespace BookManager.Wpf
{
    /// <summary>
    /// Логика взаимодействия для приложения WPF.
    /// Здесь находится композиционный корень: настройка DI и запуск главного окна.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Контейнер зависимостей Ninject, доступный на время жизни приложения.
        /// </summary>
        private IKernel? _kernel;

        /// <summary>
        /// Точка входа WPF-приложения.
        /// Вариант ViewModelFirst: запуск через ViewManager и ViewModelManager.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _kernel = CreateKernel();

            // Берём сервис из Core — ниже лежащий слой.
            var bookService = _kernel.Get<IBookService>();

            // Создаём менеджер ViewModel (Presenter-слой).
            var viewModelManager = new ViewModelManager(bookService);

            // Создаём менеджер View (WPF-слой) и передаём ему VMManager.
            var viewManager = new ViewManager(viewModelManager);

            // Запускаем цепочку ViewModelFirst.
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

            // ВАЖНО: здесь НЕ биндируем BookViewModel и тем более ViewModelManager.
            // Контейнер знает только про DAL/BL (репозитории, сервисы и т.п.).

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
