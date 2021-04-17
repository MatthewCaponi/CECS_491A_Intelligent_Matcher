using DataAccess;
using DataAccess.Repositories;
using IntelligentMatcher.Services;
using IntelligentMatcher.UserManagement;
using Login;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Security;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Services;
using Messaging;
using Registration;
using Registration.Services;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();

            services.AddControllers();
            
            services.AddTransient<IDataGateway, SQLServerGateway>();
            services.AddSingleton<IConnectionStringData, ConnectionStringData>();
            services.AddTransient<ILoginAttemptsRepository, LoginAttemptsRepository>();
            services.AddTransient<IUserProfileRepository, UserProfileRepository>();
            services.AddTransient<IUserAccountRepository, UserAccountRepository>();

            services.AddTransient<IUserAccountCodeRepository, UserAccountCodeRepository>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<ICryptographyService, CryptographyService>();
            services.AddTransient<ILoginAttemptsService, LoginAttemptsService>();

            services.AddTransient<IMessagesRepo, MessagesRepo>();
            services.AddTransient<IChannelsRepo, ChannelsRepo>();
            services.AddTransient<IUserChannelsRepo, UserChannelsRepo>();

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserProfileService, UserProfileService>();
            services.AddTransient<IUserAccountService, UserAccountService>();
            services.AddTransient<IUserAccountCodeService, UserAccountCodeService>();
            services.AddTransient<IValidationService, ValidationService>();
            services.AddTransient<IUserAccessService, UserAccessService>();

            services.AddScoped<ILoginManager, LoginManager>();
            services.AddScoped<IRegistrationManager, RegistrationManager>();
            services.AddScoped<IUserManager, UserManager>();

            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IMessagingService, MessagingService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //app.UseCors(x => x.WithOrigin("http://localhost:3000").AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
