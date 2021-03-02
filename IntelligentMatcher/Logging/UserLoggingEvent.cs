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
        private readonly string _accountType;

        public UserLoggingEvent(EventName eventName, string ipAddress, int userId, string accountType)
        {
            _eventName = eventName;
            _ipAddress = ipAddress;
            _userId = userId;
            _accountType = accountType;
        }

        public EventName GetEventName()
        {
            return _eventName;
        }
        public string GetEventInfo()
        {
            return $"{_userId} {_ipAddress} {_accountType}";
        }

    }
}
