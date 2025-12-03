using System;
using System.Windows.Input;

namespace BookManager.Presenter
{
    /// <summary>
    /// Универсальная реализация команды для паттерна MVVM.
    /// Оборачивает делегаты execute/canExecute и реализует интерфейс ICommand.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Делегат, содержащий основной код команды (что выполнить).
        /// </summary>
        private readonly Action<object?> _execute;

        /// <summary>
        /// Делегат, определяющий, доступна ли команда в данный момент.
        /// Если null, команда считается всегда доступной.
        /// </summary>
        private readonly Func<object?, bool>? _canExecute;

        /// <summary>
        /// Создаёт новую команду.
        /// </summary>
        /// <param name="execute">
        /// Действие, выполняемое при вызове команды (метод Execute).
        /// Не должно быть null.
        /// </param>
        /// <param name="canExecute">
        /// Функция, определяющая, можно ли сейчас выполнить команду (метод CanExecute).
        /// Если не задана, команда всегда возвращает true в CanExecute.
        /// </param>
        /// <exception cref="ArgumentNullException">Если execute == null.</exception>
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Определяет, может ли команда выполниться в текущий момент.
        /// WPF вызывает этот метод, чтобы решить, нужно ли отключить/включить связанные элементы управления.
        /// </summary>
        /// <param name="parameter">
        /// Параметр команды, передаваемый из привязки (CommandParameter).
        /// Может быть null, если параметр не используется.
        /// </param>
        /// <returns>true, если команду можно выполнить; иначе false.</returns>
        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        /// <summary>
        /// Выполняет команду.
        /// Вызывается WPF при активации команды (например, при нажатии на кнопку).
        /// </summary>
        /// <param name="parameter">
        /// Параметр команды, передаваемый из привязки (CommandParameter).
        /// Может быть null, если команда не требует параметров.
        /// </param>
        public void Execute(object? parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// Событие, которое должен вызывать разработчик,
        /// когда условия выполнения команды (CanExecute) изменились.
        /// WPF подписывается на это событие и заново вызывает CanExecute.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Явно уведомляет всех подписчиков о том,
        /// что результат CanExecute мог измениться.
        /// Обычно вызывается из ViewModel при изменении свойств,
        /// влияющих на доступность команды.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
