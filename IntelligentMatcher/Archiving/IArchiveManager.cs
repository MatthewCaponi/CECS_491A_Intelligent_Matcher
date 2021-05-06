using System;

namespace Archiving
{
    public interface IArchiveManager
    {
        bool ArchiveLogFiles(DateTimeOffset startTime, DateTimeOffset endTime);
        bool DeleteArchivedFiles(DateTimeOffset startTime, DateTimeOffset endTime);
        bool RecoverLogFiles(DateTimeOffset startTime, DateTimeOffset endTime);
    }
}
