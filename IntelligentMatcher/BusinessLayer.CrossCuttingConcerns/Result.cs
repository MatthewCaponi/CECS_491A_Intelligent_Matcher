﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.CrossCuttingConcerns
{
    public class Result<T>
    {
        public bool WasSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public T SuccessValue { get; set; }
        public static Result<T> Success(T successValue)
        {
            return new Result<T>
            {
                WasSuccessful = true,
                SuccessValue = successValue
            };
        }

        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T>
            {
                WasSuccessful = false,
                ErrorMessage = errorMessage
            };
        }
    }
}

