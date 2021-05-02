using System.ComponentModel;

namespace Looto.Models.Data
{
    /// <summary>Mode of sorting in the <see cref="Looto.Views.ResultsWindow"/>.</summary>
    public enum ResultsSortingMode
    {
        /// <summary>Sorts results in the <see cref="Looto.Views.ResultsWindow"/> by port value.</summary>
        [Description("By port value")]
        ByPortValue,

        /// <summary>Sorts results in the <see cref="Looto.Views.ResultsWindow"/> by port protocol.</summary>
        [Description("By port protocol")]
        ByPortProtocol,

        /// <summary>Sorts results in the <see cref="Looto.Views.ResultsWindow"/> by port state.</summary>
        [Description("By port state")]
        ByPortState,
    }
}
