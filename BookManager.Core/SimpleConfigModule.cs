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

        public override void Load()
        {
            // Динамически выбираем реализацию
            if (_useEntityFramework)
            {
                Bind<IBookRepository>().To<EntityBookRepository>().InSingletonScope();
            }
            else
            {
                Bind<IBookRepository>().To<DapperBookRepository>().InSingletonScope();
            }

            Bind<Logic>().ToSelf();
        }
    }
}
