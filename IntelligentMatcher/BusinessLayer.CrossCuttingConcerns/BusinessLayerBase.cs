using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace BusinessLayer.CrossCuttingConcerns
{
    public class BusinessLayerBase
    {
        public bool ContainsNullOrEmptyParameter<T>(T model)
        {
            if (ContainsNullOrEmptyParameter(model, null))
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
                    if (exemptParameters != null)
                    {
                        if (property.Name.ToString() == exemptParameters[i])
                        {
                            continue;
                        }
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

        public bool IsNull(object model)
        {
            if (!(model is null))
            {
                return true;
            }

            return false;
        }

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

        public bool InvalidMaxLength(string property, int maxLength)
        {
            if (property.Length >= maxLength)
            {
                return false;
            }

            return true;
        }

        public bool InvalidMinLength(string property, int minLength)
        {
            if (property.Length < minLength)
            {
                return false;
            }

            return true;
        }

        public bool ContainsRequiredCharacterTypes(string property, bool digit, bool upper, bool lower, bool number)
        {
            if (digit)
            {
                if (!property.Any(char.IsDigit))
                {
                    return false;
                }
            }
            if (upper)
            {
                if (!property.Any(char.IsUpper))
                {
                    return false;
                }
            }
            if (lower)
            {
                if (!property.Any(char.IsLower))
                {
                    return false;
                }
            }
            if (number)
            {
                if (!property.Any(char.IsNumber))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
