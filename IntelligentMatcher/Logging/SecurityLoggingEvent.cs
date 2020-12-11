using System;
using System.Collections.Generic;
using System.Text;

namespace Logging
{
    public class SecurityLoggingEvent : ILoggingEvent
    {
        private readonly EventName _eventName;
        private readonly int _userId;
        private readonly Uri _url;

        public SecurityLoggingEvent(EventName eventName, int userId, Uri url)
        {
            _eventName = eventName;
            _userId = userId;
            _url = url;
        }
    }
}
