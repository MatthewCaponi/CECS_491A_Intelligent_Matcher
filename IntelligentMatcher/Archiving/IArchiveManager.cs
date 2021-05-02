using System;

namespace Archiving
{
    public interface IArchiveManager
    {
        bool ArchiveLogFiles(DateTimeOffset startTime, DateTimeOffset endTime);
    }
}
