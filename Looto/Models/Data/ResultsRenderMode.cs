using System.ComponentModel;

namespace Looto.Models.Data
{
    /// <summary>Mode of rendering in the <see cref="Looto.Views.ResultsWindow"/>.</summary>
    public enum ResultsRenderMode
    {
        /// <summary>
        /// Render <see cref="Looto.Components.PortInfo"/> components in the <see cref="Looto.Views.ResultsWindow"/>. <br/>
        /// Many components take up a lot of memory in RAM.
        /// </summary>
        [Description("Full")]
        Full,

        /// <summary>
        /// Show resulsts in the <see cref="Looto.Views.ResultsWindow"/> with text. <br/>
        /// Not memory intensive.
        /// </summary>
        [Description("As text")]
        AsText,

        /// <summary>
        /// Not show results anyway. <br/>
        /// Needs only if user not want see a result, and want only to download result as file.
        /// </summary>
        [Description("Not render")]
        NotRender,
    }
}
