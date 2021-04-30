using System;
using System.ComponentModel;
using System.Linq;

namespace Looto.Models.Utils
{
    /// <summary>Extensions for enums and their members.</summary>
    public static class EnumExtensions
    {
        /// <summary>Get string value from <see cref="DescriptionAttribute"/> of <see cref="" langword="enum"/> member.</summary>
        /// <typeparam name="T">Any <see cref="" langword="enum"/> type.</typeparam>
        /// <param name="enumValue">Any <see cref="" langword="enum"/> member.</param>
        /// <returns>Stringify <see cref="" langword="enum"/> member value.</returns>
        public static string EnumToString<T>(this T enumValue) where T : Enum
        {
            var enumType = typeof(T);
            var memberInfos = enumType.GetMember(enumValue.ToString());
            var enumValueMemberInfo = memberInfos.FirstOrDefault(memeberInfo => memeberInfo.DeclaringType == enumType);
            var valueAttributes = enumValueMemberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            var description = (valueAttributes[0] as DescriptionAttribute).Description;

            return description ?? enumValue.ToString();
        }
    }
}
