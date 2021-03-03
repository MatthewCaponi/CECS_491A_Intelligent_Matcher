using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public static class ModelConverterService
    {
        public static Q ConvertTo<T, Q>(T convertFrom, Q convertTo)
        {
            var propList = convertFrom.GetType().GetProperties();
            foreach (var item in propList)
            {
                convertTo.GetType().GetProperty(item.Name).SetValue(item.GetValue(convertFrom, null), null);
            }

            return convertTo;
        }
    }
}
