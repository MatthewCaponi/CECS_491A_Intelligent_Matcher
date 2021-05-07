using System;
using System.Threading.Tasks;

namespace Archiving
{
    public interface IArchiveManager
    {
        Task<bool> ArchiveLogFiles(DateTimeOffset startTime, DateTimeOffset endTime);
        Task<bool> ArchiveLogFilesByCategory(DateTimeOffset startTime, DateTimeOffset endTime, string category);
        Task<bool> DeleteArchivedFiles(DateTimeOffset startTime, DateTimeOffset endTime);
        Task<bool> RecoverLogFiles(DateTimeOffset startTime, DateTimeOffset endTime);
    }
}
