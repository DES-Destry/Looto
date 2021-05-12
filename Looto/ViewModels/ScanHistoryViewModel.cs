using Looto.Models;
using Looto.Models.Data;
using System;
using System.Windows.Input;

namespace Looto.ViewModels
{
    /// <summary>
    /// View model for <see cref="Looto.Views.ScanHistoryWindow"/> with scanning history.<br/>
    /// Extends <see cref="BaseViewModel"/> class.
    /// </summary>
    public class ScanHistoryViewModel : BaseViewModel
    {
        private readonly Cache _cache;

        /// <summary>Calls on data render request.</summary>
        public event Action<CacheData> RenderCache;

        /// <summary>Create new view model with cache.</summary>
        /// <param name="cache">Cache from file.</param>
        public ScanHistoryViewModel(Cache cache)
        {
            _cache = cache;
            Render();
        }

        /// <summary>
        /// "Clear all" button command. <br/> 
        /// Clear all cache data from file.
        /// </summary>
        public ICommand ClearAll => new BaseCommand(ClearAllCommand);

        /// <summary>Call render event.</summary>
        public void Render()
        {
            RenderCache?.Invoke(_cache.GetCache());
        }


        /// <summary>Clear all cache data from file.</summary>
        /// <param name="parameter">
        /// Basic <see cref="BaseCommand"/> parameter. <br/>
        /// Value of this gets from xaml (CommandParameter property).
        /// </param>
        private void ClearAllCommand(object parameter)
        {
            _cache.ClearAll();
            Render();
        }
    }
}
