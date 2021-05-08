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
using FriendList;
using PublicUserProfile;
using DataAccess.Repositories.ListingRepositories;
using TraditionalListings.Services;
using TraditionalListings.Managers;
using Models.DALListingModels;
using Logging;
using DataAccess.Repositories.LoginTrackerRepositories;
using DataAccess.Repositories.PageVisitTrackerRepositories;
using DataAccess.Repositories.SearchTrackerRepositories;
using UserAnalysisManager;
using TraditionalListingSearch;
using WebApi.Custom_Middleware;
using IdentityServices;
using AuthorizationResolutionSystem;
using UserAccessControlServices;
using AuthorizationPolicySystem;

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
            services.AddSingleton(Configuration);

            services.AddTransient<ILogService, LogService>();
            services.AddTransient<ITokenBuilderService, JwtTokenBuilderService>();
            services.AddTransient<IAuthorizationPolicyManager, AuthorizationPolicyManager>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IAuthorizationResolutionManager, AuthorizationResolutionManager>();
            services.AddTransient<IDataGateway, SQLServerGateway>();
            services.AddSingleton<IConnectionStringData, ConnectionStringData>();
            services.AddTransient<ILoginAttemptsRepository, LoginAttemptsRepository>();
            services.AddTransient<IUserProfileRepository, UserProfileRepository>();
            services.AddTransient<IUserAccountRepository, UserAccountRepository>();
            services.AddTransient<IFriendBlockListRepo, FriendBlockListRepo>();
            services.AddTransient<IFriendListRepo, FriendListRepo>();
            services.AddTransient<IFriendRequestListRepo, FriendRequestListRepo>();
            services.AddTransient<IPublicUserProfileRepo, PublicUserProfileRepo>();

            services.AddTransient<IListingRepository, ListingRepository>();
            services.AddTransient<ICollaborationRepository, CollaborationRepository>();
            services.AddTransient<IRelationshipRepository, RelationshipRepository>();
            services.AddTransient<IDatingRepository, DatingRepository>();
            services.AddTransient<ITeamModelRepository, TeamModelRepository>();
            services.AddTransient<ITraditionalListingSearchRepository, TraditionalListingSearchRepository>();
            services.AddTransient<ILoginTrackerRepo, LoginTrackerRepo>();
            services.AddTransient<IPageVisitTrackerRepo, PageVisitTrackerRepo>();
            services.AddTransient<ISearchTrackerRepo, SearchTrackerRepo>();



            services.AddTransient<IListingCreationService, ListingCreationService>();
            services.AddTransient<IListingDeletionService, ListingDeletionService>();
            services.AddTransient<IListingUpdationService, ListingUpdationService>();
            services.AddTransient<IListingGetterService, ListingGetterService>();

           

           


            services.AddTransient<IUserAccountCodeRepository, UserAccountCodeRepository>();

            services.AddTransient<IUserReportsRepo, UserReportsRepo>();


            services.AddTransient<IAccountVerificationRepo, AccountVerificationRepo>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IPublicUserProfileService, PublicUserProfileService>();

            services.AddTransient<ICryptographyService, CryptographyService>();
            services.AddTransient<ILoginAttemptsService, LoginAttemptsService>();
            services.AddTransient<IMessagesRepo, MessagesRepo>();
            services.AddTransient<IChannelsRepo, ChannelsRepo>();
            services.AddTransient<IUserChannelsRepo, UserChannelsRepo>();
            services.AddTransient<ITraditionalListingSearchRepository, TraditionalListingSearchRepository>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IFriendListManager, FriendListManager>();
            services.AddTransient<IPublicUserProfileManager, PublicUserProfileManager>();
            services.AddTransient<IUserInteractionService, UserInteractionService>();

            services.AddTransient<IUserProfileService, UserProfileService>();
            services.AddTransient<IUserAccountService, UserAccountService>();
            services.AddTransient<IUserAccountCodeService, UserAccountCodeService>();
            services.AddTransient<IValidationService, ValidationService>();
            services.AddTransient<IUserAccessService, UserAccessService>();

            services.AddScoped<ILoginManager, LoginManager>();
            services.AddScoped<IRegistrationManager, RegistrationManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IListingSearchManager, ListingSearchManager>();


            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IMessagingService, MessagingService>();
            services.AddScoped<IUserAnalysisService,UserAnalysisService>();

            services.AddScoped<IListingsManager, ListingsManager>();


            services.AddScoped<IMessagingService, MessagingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthorizationMiddleware();
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
