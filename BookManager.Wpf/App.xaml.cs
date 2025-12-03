using System;
using System.Windows;
using Ninject;
using BookManager.Core;          // пространство имён, где лежит SimpleConfigModule
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
        /// Вызывается при старте (согласно атрибуту Startup в App.xaml).
        /// </summary>
        /// <param name="e">Аргументы запуска приложения.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _kernel = CreateKernel();

            var bookViewModel = _kernel.Get<BookViewModel>();

            var mainWindow = new MainWindow
            {
                DataContext = bookViewModel
            };

            mainWindow.Show();
        }

        /// <summary>
        /// Создаёт и настраивает контейнер зависимостей Ninject.
        /// Здесь подключаем модуль конфигурации из Core и регистрируем ViewModel.
        /// </summary>
        /// <returns>Инициализированный контейнер IKernel.</returns>
        private static IKernel CreateKernel()
        {

            bool useEntityFramework = true;

            var kernel = new StandardKernel(new SimpleConfigModule(useEntityFramework));

            kernel.Bind<BookViewModel>().ToSelf().InSingletonScope();

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
