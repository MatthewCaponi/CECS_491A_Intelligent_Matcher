using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Logging
{
    public class UserLoggingEvent : ILoggingEvent
    {
        private readonly EventName _eventName;
        private readonly string _ipAddress;
        private readonly int _userId;
        private readonly UserProfileModel.AccountType _accountType;

        public UserLoggingEvent(EventName eventName, string ipAddress, int userId, UserProfileModel.AccountType accountType)
        {
            _eventName = eventName;
            _ipAddress = ipAddress;
            _userId = userId;
            _accountType = accountType;
        }
    }
}
