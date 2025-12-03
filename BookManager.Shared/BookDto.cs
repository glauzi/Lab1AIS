using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookManager.Shared
{
    /// <summary>
    /// DTO для книги, используемое между ViewModel и View.
    /// Реализует INotifyPropertyChanged для корректной работы WPF-связей.
    /// </summary>
    public class BookDto : INotifyPropertyChanged
    {
        /// <summary>
        /// Внутреннее поле для хранения значения свойства <see cref="Id"/>.
        /// Используется как источник данных для привязки и синхронизации с моделью.
        /// </summary>
        private int _id;

        /// <summary>
        /// Внутреннее поле для хранения значения свойства <see cref="Title"/>.
        /// Содержит текущее название книги.
        /// </summary>
        private string _title = string.Empty;

        /// <summary>
        /// Внутреннее поле для хранения значения свойства <see cref="Author"/>.
        /// Содержит текущее имя автора книги.
        /// </summary>
        private string _author = string.Empty;

        /// <summary>
        /// Внутреннее поле для хранения значения свойства <see cref="Genre"/>.
        /// Содержит текущий жанр книги.
        /// </summary>
        private string _genre = string.Empty;

        /// <summary>
        /// Внутреннее поле для хранения значения свойства <see cref="Year"/>.
        /// Содержит текущий год издания книги.
        /// </summary>
        private int _year;

        /// <summary>
        /// Идентификатор книги (первичный ключ).
        /// </summary>
        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        /// <summary>
        /// Название книги.
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetField(ref _title, value);
        }

        /// <summary>
        /// Автор книги.
        /// </summary>
        public string Author
        {
            get => _author;
            set => SetField(ref _author, value);
        }

        /// <summary>
        /// Жанр книги.
        /// </summary>
        public string Genre
        {
            get => _genre;
            set => SetField(ref _genre, value);
        }

        /// <summary>
        /// Год издания.
        /// </summary>
        public int Year
        {
            get => _year;
            set => SetField(ref _year, value);
        }

        /// <summary>
        /// Событие, которое вызывается при изменении свойства.
        /// На него подписывается WPF для обновления привязок.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вызывает событие PropertyChanged для указанного свойства.
        /// Имя свойства подставляется автоматически, если параметр не указан.
        /// </summary>
        /// <param name="propertyName">Имя изменившегося свойства.</param>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (propertyName is null)
                return;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Универсальный метод для установки значения поля и вызова PropertyChanged.
        /// Возвращает true, если значение действительно изменилось.
        /// </summary>
        /// <typeparam name="T">Тип поля/свойства.</typeparam>
        /// <param name="field">Ссылка на приватное поле, в котором хранится значение.</param>
        /// <param name="value">Новое значение.</param>
        /// <param name="propertyName">Имя свойства (подставляется автоматически).</param>
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value))
                return false;

            field = value;

            if (propertyName is not null)
                OnPropertyChanged(propertyName);

            return true;
        }
    }
}
