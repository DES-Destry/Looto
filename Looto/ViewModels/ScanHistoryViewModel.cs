using Looto.Models.Data;
using System;

namespace Looto.ViewModels
{
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

        public void Render()
        {
            RenderCache?.Invoke(_cache.GetCache());
        }
    }
}
