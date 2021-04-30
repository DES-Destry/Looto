using System.ComponentModel;

namespace Looto.Models.PortScanner
{
    public enum PortState
    {
        [Description("Not checked")]
        NotChecked,

        [Description("Opened")]
        Opened,

        [Description("Closed")]
        Closed,

        [Description("Filtered")]
        Filtered,

        [Description("Opened / Filtered")]
        OpenedOrFiltered,
    }
}
