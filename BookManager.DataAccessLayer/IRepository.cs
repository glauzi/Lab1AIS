using BookManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.DataAccessLayer
{
    public interface IRepository<T> where T : IDomainObject
    {
        /// <summary>
        /// Добавляет новую сущность в хранилище данных
        /// </summary>
        /// <param name="entity">Сущность для добавления</param>
        void Add(T entity);

        /// <summary>
        /// Удаляет сущность по идентификатору из хранилища данных
        /// </summary>
        /// <param name="id">Идентификатор сущности для удаления</param>
        void Delete(int id);

        /// <summary>
        /// Возвращает все сущности из хранилища данных
        /// </summary>
        /// <returns>Список всех сущностей</returns>
        List<T> GetAll();

        /// <summary>
        /// Находит сущность по идентификатору в хранилище данных
        /// </summary>
        /// <param name="id">Идентификатор сущности для поиска</param>
        /// <returns>Найденная сущность или null если не найдена</returns>
        T GetById(int id);

        /// <summary>
        /// Обновляет информацию о сущности в хранилище данных
        /// </summary>
        /// <param name="entity">Сущность с обновленными данными</param>
        void Update(T entity);
    }
}
