using Looto.Models;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Looto.ViewModels
{
    /// <summary>
    /// View model for window with occured error information.<br/>
    /// Extends <see cref="BaseViewModel"/> class.
    /// </summary>
    class ErrorOccuredViewModel : BaseViewModel
    {
        #region Fields for binding
        private Exception _occuredError;

        /// <summary>Occured error to show.</summary>
        /// <value>The <see cref="OccuredError"/> property gets/sets the value of the <see cref="Exception"/> field, <see cref="_occuredError"/>.</value>
        public Exception OccuredError
        {
            get => _occuredError;
            set
            {
                _occuredError = value;
                OnPropertyChanged();
            }
        }
        /// <summary>Name of the error exception</summary>
        /// <value>The <see cref="NameOfException"/> property gets the value of the <see cref="Exception.GetType()"/> field, <see cref="_occuredError"/>.</value>
        public string NameOfException => _occuredError?.GetType().Name;

        /// <summary>
        /// Send bug report button command. <br/>
        /// Open github page with app issues.
        /// </summary>
        public ICommand OpenGithub => new BaseCommand(OpenGithubCommand);
        #endregion

        /// <summary>Create new view model without error.</summary>
        public ErrorOccuredViewModel() { }
        /// <summary>Create new view model with error.</summary>
        /// <param name="error">Occured error.</param>
        public ErrorOccuredViewModel(Exception error)
        {
            OccuredError = error;
        }

        /// <summary>Open github issues of app page.</summary>
        /// <param name="parameter">
        /// Basic <see cref="BaseCommand"/> parameter. <br/>
        /// Value of this gets from xaml (CommandParameter property).
        /// </param>
        private void OpenGithubCommand(object parameter)
        {
            Process.Start("https://github.com/DES-Destry/Looto/issues/new?assignees=DES-Destry&labels=bug&template=bug_report.md&title=Looto+have+a+bug%21");
        }
    }
}
