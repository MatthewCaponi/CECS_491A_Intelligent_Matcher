using System;
using System.Collections.Generic;
using System.Text;

namespace Logging
{
    public class NetworkLoggingEvent : ILoggingEvent
    {
        private readonly EventName _eventName;
        private readonly int _userId;
        private readonly string _ipAddress;
        private readonly string _pageRequest;
        private readonly Uri _urlReferrer;
        private readonly string _userAgent;

        public NetworkLoggingEvent(EventName eventName, int userId, string ipAddress, string pageRequest, Uri urlReferrer, string userAgent)
        {
            _eventName = eventName;
            _userId = userId;
            _ipAddress = ipAddress;
            _pageRequest = pageRequest;
            _urlReferrer = urlReferrer;
            _userAgent = userAgent;
        }

        public EventName GetEventName()
        {
            return _eventName;
        }
    }
}
