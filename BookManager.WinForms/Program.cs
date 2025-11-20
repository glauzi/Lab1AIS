using System;
using System.Windows.Forms;
using Ninject;
using BookManager.Core;
using BookManager.Shared;
using BookManager.Presenter;

namespace BookManager.WinForms
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа в приложение.
        /// Здесь реализована точка сборки (composition root):
        /// создаётся DI-контейнер, настраиваются все зависимости,
        /// разрешаются View и Presenter, после чего запускается WinForms-интерфейс.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 1. Выбор технологии доступа к данным (EF или Dapper)
            var choice = MessageBox.Show(
                "Использовать Entity Framework?\n\n" +
                "Да - использовать Entity Framework\n" +
                "Нет - использовать Dapper",
                "Выбор технологии доступа к данным",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            bool useEntityFramework = (choice == DialogResult.Yes);

            // 2. Создаём DI-контейнер Ninject и передаём ему конфигурационный модуль
            // SimpleConfigModule настраивает привязки для IBookRepository и IBookService
            IKernel kernel = new StandardKernel(new SimpleConfigModule(useEntityFramework));

            // 3. Настраиваем привязку интерфейса представления к конкретной форме
            kernel.Bind<IBookView>()
                  .To<Form1>()
                  .InSingletonScope();

            // 4. Настраиваем создание презентера
            // Он зависит от IBookView и IBookService и будет создан контейнером автоматически
            kernel.Bind<BookPresenter>()
                  .ToSelf()
                  .InSingletonScope();

            // 5. Разрешаем зависимости: получаем Presenter и View из контейнера
            var presenter = kernel.Get<BookPresenter>();
            var view = (Form1)kernel.Get<IBookView>();

            // 6. Запускаем приложение с полученным View
            Application.Run(view);
        }
    }
}
