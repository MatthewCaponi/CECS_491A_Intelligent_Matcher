using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using WebApi.Models;
namespace WebApi
{
    public class EmailOptionsManager
    {

        public IConfigurationRoot Configuration { get; set; }


        public EmailOptionsModel GetEmailOptions()
        {

            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            Configuration = builder.Build();

            EmailOptionsModel emailOptions = new EmailOptionsModel();

            emailOptions.Sender =  Configuration.GetSection("Email:Sender").Value;
            emailOptions.MessageStream = Configuration.GetSection("Email:MessageStream").Value;
            emailOptions.Subject = Configuration.GetSection("Email:Subject").Value;
            emailOptions.TextBody = Configuration.GetSection("Email:TextBody").Value;
            emailOptions.Tag = Configuration.GetSection("Email:Tag").Value;
            emailOptions.TrackOpens = bool.Parse(Configuration.GetSection("Email:TrackOpens").Value);


            return emailOptions;

        }
    }
}
