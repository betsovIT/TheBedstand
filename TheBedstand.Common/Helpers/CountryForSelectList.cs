namespace TheBedstand.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class CountryForSelectList<T>
        where T : Enum
    {
        public static IEnumerable<CountryEnumListItem<T>> GetValuesForSelectList()
        {
            var resultList = new List<CountryEnumListItem<T>>();

            foreach (var value in Enum.GetValues(typeof(T)).Cast<T>())
            {
                resultList.Add(new CountryEnumListItem<T>
                {
                    Text = EnumDescriptionHelper.GetEnumValueDescription<T>(value),
                    Value = value,
                });
            }

            return resultList;
        }
    }

    public class CountryEnumListItem<T>
        where T : Enum
    {
        public string Text { get; set; }

        public T Value { get; set; }
    }
}
