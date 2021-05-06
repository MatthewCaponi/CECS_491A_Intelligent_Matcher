using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DataAccessLayer.CrossCuttingConcerns
{
    public class DataAccessBase
    {
        public bool InvalidLength<T>(T model, int maxLength)
        {
            foreach (PropertyInfo property in model.GetType().GetProperties())
            {
                if (property.GetType() == typeof(string))
                {
                    if (property.GetValue(model, null).ToString().Length >= maxLength)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool InvalidLength(string property, int maxLength)
        {
            if (property.Length >= maxLength)
            {
                return false;
            }

            return true;
        }

        public bool IsNull(object model)
        {
            if (!(model is null))
            {
                return true;
            }

            return false;
        }

        public bool ContainsNullOrEmptyParameter<T>(T model, List<string> exemptParameters)
        {
            int i = 0;
            foreach (PropertyInfo property in model.GetType().GetProperties())
            {
                if (Nullable.GetUnderlyingType(property.GetType()) == null)
                {
                    if (property.Name.ToString() == exemptParameters[i])
                    {
                        continue;
                    }

                    if (property.GetValue(model, null) == null)
                    {
                        return false;
                    }
                }

                if (property.GetType() == typeof(string))
                {
                    if (string.IsNullOrEmpty((string)property.GetValue(model, null)))
                    {
                        return false;
                    }
                }

                ++i;
            }

            return true;
        }
    }
}
