using System;
using System.Windows.Input;

namespace Looto.Models
{
    /// <summary>
    /// Base implementation of <see cref="ICommand"/> interface.<br/>
    /// Needs for binding commands in xaml markup.
    /// </summary>
    class BaseCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged;

        /// <summary>Create new command to binding.</summary>
        /// <param name="execute">Delegate, that will execute on command calling (for example: after button was clicked).</param>
        /// <param name="canExecute">
        /// Delegate, that install condition on execution - if it false execute Delecate will be not executed.<br/>
        /// If it null condition will be always true.
        /// </param>
        public BaseCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>Able of execution checking.</summary>
        /// <param name="parameter">Value of this gets from xaml (CommandParameter property).</param>
        /// <returns>Result of canExecute delegate - result of condition.</returns>
        public bool CanExecute(object parameter) => canExecute == null || canExecute(parameter);

        /// <summary>
        /// Execute command function.<br/>
        /// Invoke when command was called(button clicked or more).
        /// </summary>
        /// <param name="parameter">Value of this gets from xaml (CommandParameter property).</param>
        public void Execute(object parameter)
        {
            execute.Invoke(parameter);
        }
    }
}
