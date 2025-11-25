using System;
using System.Windows.Forms;
using Ninject;
using BookManager.Core;
using BookManager.Shared;
using BookManager.Presenter;
using BookManager.WinForms;

namespace BookManager.App
{
    /// <summary>
    /// Точка входа верхнеуровневого проекта приложения.
    /// Здесь реализована точка сборки (composition root):
    /// создаётся DI-контейнер, настраиваются все зависимости между слоями
    /// (View, Presenter, бизнес-логика, доступ к данным)
    /// и запускается WinForms-интерфейс.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Главный метод приложения.
        /// Отвечает за:
        /// 1) инициализацию WinForms;
        /// 2) выбор технологии доступа к данным (EF или Dapper);
        /// 3) создание и настройку DI-контейнера Ninject;
        /// 4) разрешение зависимостей View и Presenter;
        /// 5) запуск главной формы.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Инициализация визуальных стилей WinForms
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // 1. Выбор технологии доступа к данным
            var choice = MessageBox.Show(
                "Использовать Entity Framework?\n\n" +
                "Да - использовать Entity Framework\n" +
                "Нет - использовать Dapper",
                "Выбор технологии доступа к данным",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            bool useEntityFramework = (choice == DialogResult.Yes);

            // 2. Создаём DI-контейнер Ninject с конфигурационным модулем.
            // SimpleConfigModule настраивает привязки:
            // IBookRepository -> EntityBookRepository или DapperBookRepository
            // IBookService    -> BookService
            IKernel kernel = new StandardKernel(new SimpleConfigModule(useEntityFramework));

            // 3. Настраиваем привязку интерфейса представления к конкретной форме.
            kernel.Bind<IBookView>()
                  .To<Form1>()
                  .InSingletonScope();

            // 4. Настраиваем создание презентера.
            // Презентер зависит от IBookView и IBookService,
            // и контейнер сам подставит нужные реализации.
            kernel.Bind<BookPresenter>()
                  .ToSelf()
                  .InSingletonScope();

            // 5. Разрешаем зависимости: получаем Presenter и View из контейнера.
            var presenter = kernel.Get<BookPresenter>();
            var view = (Form1)kernel.Get<IBookView>();

            // 6. Запускаем цикл обработки сообщений WinForms.
            System.Windows.Forms.Application.Run(view);
        }
    }
}