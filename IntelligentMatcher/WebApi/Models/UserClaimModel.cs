﻿namespace WebApi.Models
{
    public class UserClaimModel
    {
        public string Type { get; set; }
        public string Value { get; set; }

        public UserClaimModel(string type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}