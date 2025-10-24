using BookManager.Entities;
using System.Collections.Generic;

namespace BookManager.DataAccessLayer
{
    /// <summary>
    /// Определяет контракт для репозитория работы с книгами
    /// Обеспечивает абстракцию над способом хранения данных
    /// </summary>
    public interface IBookRepository : IRepository<Book>
    {

    }
}
