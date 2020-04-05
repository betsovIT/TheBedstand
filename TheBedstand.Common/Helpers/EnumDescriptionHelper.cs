namespace TheBedstand.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public static class EnumDescriptionHelper
    {
        public static string GetEnumValueDescription<T>(T value)
            where T : Enum
        {
            Type enumType = typeof(T);

            MemberInfo memberInfo = enumType.GetMember(value.ToString()).Single();

            var descriptionAttribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();

            return descriptionAttribute.Description;
        }
    }
}
