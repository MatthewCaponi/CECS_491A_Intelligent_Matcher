﻿using System;

namespace Cross_Cutting_Concerns
{
    public static class ModelConverterService
    {
        
        /* T is the type of the model that needs to be converted
            * Q is the type of the model you want it to be converted to
            * source is the object that needs to be converted
            * destination is the object you want to store the converted model
            * destination is then returned
            */
        public static Q ConvertTo<T, Q>(T source, Q destination)
        {
            var sourceProperties = source.GetType().GetProperties();
            var destinationProperties = destination.GetType().GetProperties();

            foreach (var sourceProp in sourceProperties)
            {
                foreach (var destinationProp in destinationProperties)
                {
                    if (sourceProp.Name == destinationProp.Name)
                    {
                        destinationProp.SetValue(destination, sourceProp.GetValue(source));
                    }
                }
            }

            return destination;
        }
    }
}
