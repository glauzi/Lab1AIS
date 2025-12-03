using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Presenter
{
    /// <summary>
    /// Базовый класс для всех ViewModel в приложении.
    /// Реализует INotifyPropertyChanged и удобный SetProperty.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вызывает событие PropertyChanged для указанного свойства.
        /// [CallerMemberName] подставляет имя свойства автоматически.
        /// </summary>
        /// <param name="propertyName">Имя свойства</param>
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
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
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
