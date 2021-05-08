using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModels;
using DataAccess.Repositories;
using DataAccess.Repositories.ListingRepositories;
using DataAccess.Repositories.LoginTrackerRepositories;
using DataAccess.Repositories.PageVisitTrackerRepositories;
using DataAccess.Repositories.SearchTrackerRepositories;
using Models;
using Models.DALListingModels;
using Services;
 

namespace UserAnalysisManager
{
    public class UserAnalysisService : IUserAnalysisService
    {
        private readonly IFriendListRepo _friendListRepo;
        private readonly IListingRepository _listingRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly ILoginTrackerRepo _loginTrackerRepo;
        private readonly IPageVisitTrackerRepo _pageVisitTrackerRepo;
        private readonly IFriendRequestListRepo _friendRequestListRepo;
        //private readonly ITraditionalListingSearchRepository _traditionalListingSearchRepository;
        private readonly ISearchTrackerRepo _searchTrackerRepo;

        public UserAnalysisService (IFriendListRepo friendListRepo, IListingRepository listingRepository,IUserAccountRepository userAccountRepository,
        ILoginTrackerRepo loginTrackerRepo, IPageVisitTrackerRepo pageVisitTrackerRepo,IFriendRequestListRepo friendRequestListRepo, /*ITraditionalListingSearchRepository
            traditionalListingSearchRepository,*/ ISearchTrackerRepo searchTrackerRepo)
        {
            _friendListRepo = friendListRepo;
            _listingRepository = listingRepository;
            _userAccountRepository = userAccountRepository;
            _loginTrackerRepo = loginTrackerRepo;
            _pageVisitTrackerRepo = pageVisitTrackerRepo;
            _friendRequestListRepo = friendRequestListRepo;
           // _traditionalListingSearchRepository = traditionalListingSearchRepository;
            _searchTrackerRepo = searchTrackerRepo;
        }

       
        //tracker..everytime someone visits a page i created this to track the info for the table 
        public async Task<int> CreateLoginTracked(BusinessLoginTrackerModel businessLoginTrackerModel)
        {
            var loginTrackerModel = ModelConverterService.ConvertTo(businessLoginTrackerModel, new DALLoginTrackerModel());
            var login = await _loginTrackerRepo.CreateLoginTracker(loginTrackerModel);

            return login;
        }

        //tracker..everytime someone visits a page i created this to track the info for the table 
        public async Task<int> CreatePageVisitTracked(BusinessPageVisitedTrackerModel businessPageVisitedTrackerModel)
        {
            var PageTrackerModel = ModelConverterService.ConvertTo(businessPageVisitedTrackerModel, new DALPageVisitTrackerModel());
            var page = await _pageVisitTrackerRepo.CreatePageVisitTracker(PageTrackerModel);

            return page;
        }


        //Counts amount of total registered accounts
        public async Task<int> GetALLUserRegistrationCount()
        {
            IEnumerable<UserAccountModel> models = await _userAccountRepository.GetAllAccounts();
            return models.Count();
        }

        //Count amount of registered accounts in a specific day 
        public async Task<int> GetALLUserRegistrationCount_Day(DateTimeOffset date)
        {
            IEnumerable<UserAccountModel> models = await _userAccountRepository.GetAllAccounts(); 
            List<UserAccountModel> dailyUsers = new List<UserAccountModel>(); //list of daily users 
            foreach(var model in models)
            {
                if(model.CreationDate == date) 
                {
                    dailyUsers.Add(model);
                }
            }
            return dailyUsers.Count();
            
        }

        //count amount of registered account "today"
        public async Task<int> GetAllUsersRegisteredToday()
        {
            return await GetALLUserRegistrationCount_Day(DateTimeOffset.UtcNow);
        }


        //count amount of registered accounts in a month. 
        public async Task<int> GetALLUserRegistrationCount_Month(int month,int year)
        {
            IEnumerable<UserAccountModel> models = await _userAccountRepository.GetAllAccounts();
            List<UserAccountModel> monthlyUsers = new List<UserAccountModel>(); //list of monthly users. 
            foreach (var model in models)
            {
              
              
                if (model.CreationDate.Year == year && model.CreationDate.Month == month)
                {
                    monthlyUsers.Add(model);
                }
            }
            return monthlyUsers.Count();

        }

        //count amount of registered accounts in a year 
        public async Task<int> GetALLUserRegistrationCount_Year(int year)
        {
            IEnumerable<UserAccountModel> models = await _userAccountRepository.GetAllAccounts();
            List<UserAccountModel> yearlyUsers = new List<UserAccountModel>(); //list of monthly users. 
            foreach (var model in models)
            {
                
               
                if (model.CreationDate.Year == year)
                {
                    yearlyUsers.Add(model);
                }
            }
            return yearlyUsers.Count();

        }

        //Past 12 month trendline ....for graphs 
        public async Task<IEnumerable<int>> GetLast12MonthsUserTrend()
        {
            List<int> trend = new List<int>();
            DateTimeOffset date = DateTimeOffset.UtcNow;
            for (int i=0; i <= 12; i++)
            {
                trend.Add(await GetALLUserRegistrationCount_Month(date.Month, date.Year)); //adds monthly viewer count to the list. 
                date = date.AddMonths(-1); //-1 so it goes back from the month when i access this 
            }
            return trend;
        }


        //get avg number of friends our users have.. 
        public async Task<int> GetAvgFriends()
        {
            IEnumerable<UserAccountModel> models = await _userAccountRepository.GetAllAccounts();
            List<int> friendcount = new List<int>();
            foreach(var model in models)
            {
                IEnumerable<FriendsListJunctionModel> friendsListJunctionModel = await _friendListRepo.GetAllUserFriends(model.Id);
                friendcount.Add(friendsListJunctionModel.Count());

            }
            int total = 0;
            foreach(int i in friendcount){
                total += i;
            }
            int avg = total / friendcount.Count();
            return avg;
        }


        //number of requests sent in a given day 
        public async Task<int> FriendRequest_GivenDay(DateTimeOffset dateTimeOffset)
        {
            IEnumerable<FriendsListJunctionModel> models = await _friendRequestListRepo.GetAllFriendRequests();
            List<FriendsListJunctionModel> friendcount = new List<FriendsListJunctionModel>();
            foreach (var model in models)
            {
                if(model.Date == dateTimeOffset) //only count the requests in the day. 
                {
                    friendcount.Add(model);
                }

            }
            return friendcount.Count();
        }


        //trendline..refelects change in the number of daily friend rqs
        public async Task<IEnumerable<int>> GetLast12MonthsFriendRequestTrend()
        {
            List<int> trend = new List<int>();
            DateTimeOffset date = DateTimeOffset.UtcNow;
            for (int i = 0; i <= 365; i++) //last 365 days 
            {
                trend.Add(await FriendRequest_GivenDay(date)); //adds daily friend requests  count to the list. 
               
                date = date.AddDays(-1); //-1 so it goes back from the day i access this 
            }
            return trend;
        }

        //number of listings created in a given day 
        //public async Task<int> ListingCreated_GivenDay(DateTimeOffset dateTimeOffset)
        //{
        //    IEnumerable<DALListingModel> models = await _traditionalListingSearchRepository.GetAllListings();
        //    List<DALListingModel> listingcount = new List<DALListingModel>();

        //    foreach (var model in models)
        //    {
        //        if (model.CreationDate == dateTimeOffset)
        //        {
        //            listingcount.Add(model);
        //        }
        //    }
        //    return listingcount.Count();
        //}

        ////number of listings created in a given month 
        //public async Task<int> ListingCreated_GivenMonth(int month, int year)
        //{
        //    IEnumerable<DALListingModel> models = await _traditionalListingSearchRepository.GetAllListings();
        //    List<DALListingModel> listingcount = new List<DALListingModel>(); //list of monthly listings created
        //    foreach (var model in models)
        //    {
              
        //        if (model.CreationDate.Year == year && model.CreationDate.Month == month)
        //        {
        //            listingcount.Add(model);
        //        }
        //    }
        //    return listingcount.Count();

        //}

        ////number of listings created in a given year 
        //public async Task<int> ListingCreated_GivenYear(int year)
        //{
        //    IEnumerable<DALListingModel> models = await _traditionalListingSearchRepository.GetAllListings();
        //    List<DALListingModel> listingcount = new List<DALListingModel>(); //list of yearly listings created 
        //    foreach (var model in models)
        //    {

        //        if (model.CreationDate.Year == year)
        //        {
        //            listingcount.Add(model);
        //        }
        //    }
        //    return listingcount.Count();

        //}

        //Get total number of Suspended users... 
        public async Task<int> GetNumOfSuspendedUsers()
        {
            IEnumerable<UserAccountModel> models = await _userAccountRepository.GetAllAccounts();
            List<UserAccountModel> userAccountModels = new List<UserAccountModel>();
            foreach (var model in models)
            {
                if (model.AccountStatus == "Suspended"){
                    userAccountModels.Add(model);
                }
            }
            return userAccountModels.Count();
        }

        //Get total number of Banned Users.. 
        public async Task<int> GetNumOfBannedUsers()
        {
            IEnumerable<UserAccountModel> models = await _userAccountRepository.GetAllAccounts();
            List<UserAccountModel> userAccountModels = new List<UserAccountModel>();
            foreach (var model in models)
            {
                if(model.AccountStatus == "Banned")
                {
                    userAccountModels.Add(model);
                }
            }
            return userAccountModels.Count();
        }

        //Total number of shadow banned users.. 
        public async Task<int> GetNumOfShadowBannedUsers()
        {
            IEnumerable<UserAccountModel> models = await _userAccountRepository.GetAllAccounts();
            List<UserAccountModel> userAccountModels = new List<UserAccountModel>();
            foreach (var model in models)
            {
                
                if (model.AccountStatus == "Shadow-Banned")
                {
                    userAccountModels.Add(model);

                }
            }
            return userAccountModels.Count();

        }

        //suspended users on a given day 
        public async Task<int> GetNumOfSuspendedUsers_GivenDay(DateTimeOffset dateTimeOffset)
        {
            IEnumerable<UserAccountModel> models = await _userAccountRepository.GetAllAccounts();
            List<UserAccountModel> userAccountModels = new List<UserAccountModel>();
            foreach (var model in models)
            {
                if (model.AccountStatus == "Suspended") //if a user account status is suspended enter this 
                {
                    if(model.UpdationDate == dateTimeOffset)// if the user status is suspended, figure out if the update to the account model was made that day? 
                    {
                        userAccountModels.Add(model);
                    }
                }
            }
            return userAccountModels.Count();
        }

        //banned users on a given day
        public async Task<int> GetNumOfBannedUsers_GivenDay(DateTimeOffset dateTimeOffset)
        {
            IEnumerable<UserAccountModel> models = await _userAccountRepository.GetAllAccounts();
            List<UserAccountModel> userAccountModels = new List<UserAccountModel>();
            foreach (var model in models)
            {
                if (model.AccountStatus == "Banned") //if a user account status is banned enter this 
                {
                    if (model.UpdationDate == dateTimeOffset)// if the user status is banned, figure out if the update to the account model was made that day? 
                    {
                        userAccountModels.Add(model);
                    }
                }
            }
            return userAccountModels.Count();
        }

        // Shadow-Banned users on a given day 

        public async Task<int> GetNumOfShadowBannedUsers_GivenDay(DateTimeOffset dateTimeOffset)
        {
            IEnumerable<UserAccountModel> models = await _userAccountRepository.GetAllAccounts();
            List<UserAccountModel> userAccountModels = new List<UserAccountModel>();
            foreach (var model in models)
            {
                if (model.AccountStatus == "Shadow-Banned") //if a user account status is shadow banned enter this 
                {
                    if (model.UpdationDate == dateTimeOffset)// if the user status is shadow banned, figure out if the update to the account model was made that day? 
                    {
                        userAccountModels.Add(model);
                    }
                }
            }
            return userAccountModels.Count();
        }

        public async Task<int> GetTotalNumLogins_GivenDay(DateTimeOffset dateTimeOffset)
        {
            IEnumerable<DALLoginTrackerModel> models = await _loginTrackerRepo.GetAllLogins();
            List<DALLoginTrackerModel> logins = new List<DALLoginTrackerModel>();
            foreach(var model in models)
            {
                if(model.LoginTime == dateTimeOffset)
                {
                    logins.Add(model);
                }
            }
            return logins.Count();
        }

        public async Task<int> GetTotalNumLogins_GivenMonth(int month, int year)
        {
            IEnumerable<DALLoginTrackerModel> models = await _loginTrackerRepo.GetAllLogins();
            List<DALLoginTrackerModel> logins = new List<DALLoginTrackerModel>();
            foreach (var model in models)
            {

                if (model.LoginTime.Year == year && model.LoginTime.Month == month)
                {
                    logins.Add(model);
                }
            }
            return logins.Count();
        }

        public async Task<int> GetTotalNumLogins_GivenYear(int year)
        {
            IEnumerable<DALLoginTrackerModel> models = await _loginTrackerRepo.GetAllLogins();
            List<DALLoginTrackerModel> logins = new List<DALLoginTrackerModel>();
            foreach (var model in models)
            {
                if (model.LoginTime.Year == year)
                {
                    logins.Add(model);
                }
            }
            return logins.Count();

        }

        public async Task<int> GetTotalListingSeaches_GivenDay(DateTimeOffset dateTimeOffset)
        {
            IEnumerable<DALSearchTrackerModel> models = await _searchTrackerRepo.GetAllSearches();
            List<DALSearchTrackerModel> logins = new List<DALSearchTrackerModel>();
            foreach (var model in models)
            {
                if (model.SearchTime== dateTimeOffset)
                {
                    logins.Add(model);
                }
            }
            return logins.Count();
        }

        public async Task<int> GetTotalListingSeaches_GivenMonth(int month, int year)
        {
            IEnumerable<DALSearchTrackerModel> models = await _searchTrackerRepo.GetAllSearches();
            List<DALSearchTrackerModel> logins = new List<DALSearchTrackerModel>();
            foreach (var model in models)
            {

                if (model.SearchTime.Year == year && model.SearchTime.Month == month)
                {
                    logins.Add(model);
                }
            }
            return logins.Count();
        }

        public async Task<int> GetTotalListingSeaches_GivenMonth(int year)
        {
            IEnumerable<DALSearchTrackerModel> models = await _searchTrackerRepo.GetAllSearches();
            List<DALSearchTrackerModel> logins = new List<DALSearchTrackerModel>();
            foreach (var model in models)
            {

                if (model.SearchTime.Year == year)
                {
                    logins.Add(model);
                }
            }
            return logins.Count();
        }




    }

}
