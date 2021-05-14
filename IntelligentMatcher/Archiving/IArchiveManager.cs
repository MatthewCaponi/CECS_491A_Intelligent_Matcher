using BusinessModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Archiving
{
    public interface IArchiveManager
    {
        Task<Result<bool>> ArchiveLogFiles(DateTimeOffset startTime, DateTimeOffset endTime);
        Task<Result<bool>> ArchiveLogFilesByCategory(DateTimeOffset startTime, DateTimeOffset endTime, string category);
        Task<Result<bool>> DeleteArchivedFiles(DateTimeOffset startTime, DateTimeOffset endTime);
        Task<Result<bool>> RecoverLogFiles(DateTimeOffset startTime, DateTimeOffset endTime);
        Task<Result<List<string>>> GetCategories();
    }
}
