namespace Logging
{
    public enum EventName { UserEvent, SecurityEvent, NetworkEvent}

    public interface ILoggingEvent
    {
        EventName GetEventName();

        string GetEventInfo();

    }
}