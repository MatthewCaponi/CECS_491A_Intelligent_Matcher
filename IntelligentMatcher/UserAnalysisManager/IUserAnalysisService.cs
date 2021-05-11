using BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserAnalysisManager
{
    public interface IUserAnalysisService
    {
        Task<int> CreateLoginTracked(BusinessLoginTrackerModel businessLoginTrackerModel);
        Task<int> CreatePageVisitTracked(BusinessPageVisitedTrackerModel businessPageVisitedTrackerModel);
        Task<int> GetALLUserRegistrationCount();
        Task<int> GetALLUserRegistrationCount_Day(DateTimeOffset date);
        Task<int> GetAllUsersRegisteredToday();
        Task<int> GetALLUserRegistrationCount_Month(int month, int year);
        Task<int> GetALLUserRegistrationCount_Year(int year);
        Task<IEnumerable<int>> GetLast12MonthsUserTrend();
        Task<int> GetAvgFriends();
        Task<int> FriendRequest_GivenDay(DateTimeOffset dateTimeOffset);
        Task<IEnumerable<int>> GetLast12MonthsFriendRequestTrend();
        //Task<int> ListingCreated_GivenDay(DateTimeOffset dateTimeOffset);
        //Task<int> ListingCreated_GivenMonth(int month, int year);
        //Task<int> ListingCreated_GivenYear(int year);
        Task<int> GetNumOfSuspendedUsers();
        Task<int> GetNumOfBannedUsers();
        Task<int> GetNumOfShadowBannedUsers();
        Task<int> GetNumOfSuspendedUsers_GivenDay(DateTimeOffset dateTimeOffset);
        Task<int> GetNumOfBannedUsers_GivenDay(DateTimeOffset dateTimeOffset);
        Task<int> GetNumOfShadowBannedUsers_GivenDay(DateTimeOffset dateTimeOffset);
        Task<int> GetTotalNumLogins_GivenDay(DateTimeOffset dateTimeOffset);
        Task<int> GetTotalNumLogins_GivenMonth(int month, int year);
        Task<int> GetTotalNumLogins_GivenYear(int year);
        Task<int> GetTotalListingSeaches_GivenDay(DateTimeOffset dateTimeOffset);
        Task<int> GetTotalListingSeaches_GivenMonth(int month, int year);
        Task<int> GetTotalListingSeaches_GivenMonth(int year);
    }
}
