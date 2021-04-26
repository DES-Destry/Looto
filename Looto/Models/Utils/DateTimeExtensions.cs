using System;

namespace Looto.Models.Utils
{
    /// <summary>Extensions for <see cref="DateTime"/> objects.</summary>
    public static class DateTimeExtensions
    {
        /// <summary>Generate string of time passed.</summary>
        /// <param name="date">Date from.</param>
        /// <returns>String of time passed.</returns>
        public static string GetTimeString(this DateTime date)
        {
            TimeSpan time = DateTime.Now - date;

            if (time.Days > 0)
                return $"{time.Days} days ago.";
            if (time.Hours > 0)
                return $"{time.Hours} hours ago.";
            if (time.Minutes > 0)
                return $"{time.Minutes} minutes ago.";
            if (time.Seconds > 0)
                return $"{time.Seconds} seconds ago.";

            return "Right now.";
        }
    }
}
