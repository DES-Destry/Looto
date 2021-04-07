using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Looto.ViewModels
{
    /// <summary>
    /// Base view model class.
    /// Contains a data update event for binding data from context.<br/>
    /// Implements <see cref="INotifyPropertyChanged"/> interface.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Calls to update value in view to rerender view with new property value.</summary>
        /// <param name="prop">
        /// Property, that bind in view and was updated.<br/>
        /// Have an <see cref="CallerMemberNameAttribute"/> attribute, therefore you can ingnore this param if use this method in property setter.<br/>
        /// If this method use outside property setter this parameter mus equals string value - name of the property.
        /// </param>
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
