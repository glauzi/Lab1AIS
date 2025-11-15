using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using BookManager.DataAccessLayer;
using BookManager.Core.Services;

namespace BookManager.Core
{
    /// <summary>
    /// Модуль конфигурации Ninject для Dependency Injection
    /// Реализует принцип Dependency Inversion (DIP) - зависимости определяются через абстракции
    /// Обеспечивает централизованное управление зависимостями в соответствии с SOLID
    /// </summary>
    public class SimpleConfigModule : NinjectModule
    {
        private readonly bool _useEntityFramework;

        /// <summary>
        /// Создает конфигурационный модуль Ninject с выбором технологии доступа к данным
        /// </summary>
        /// <param name="useEntityFramework">
        /// true - использовать Entity Framework, 
        /// false - использовать Dapper
        /// </param>
        public SimpleConfigModule(bool useEntityFramework = true)
        {
            _useEntityFramework = useEntityFramework;
        }
        /// <summary>
        /// Настраивает привязки зависимостей для DI контейнера
        /// </summary>
        public override void Load()
        {
            Bind<string>().ToConstant(AppConfig.ConnectionString);
            Bind<Func<BookContext>>().ToMethod(ctx => () => new BookContext());
            if (_useEntityFramework)
            {
                Bind<IBookRepository>().To<EntityBookRepository>().InSingletonScope();
            }
            else
            {
                Bind<IBookRepository>().To<DapperBookRepository>().InSingletonScope();
            }
            Bind<IBookService>().To<BookService>().InSingletonScope();
        }
    }
}
