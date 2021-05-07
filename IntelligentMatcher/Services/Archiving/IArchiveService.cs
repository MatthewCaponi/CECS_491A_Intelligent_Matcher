using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Archiving
{
    public interface IArchiveService
    {
        Task<bool> ArchiveLogFiles(List<String> files);
        Task<bool> DeleteArchivedFiles(List<String> zipFiles);
        Task<bool> RecoverLogFiles(List<String> files);
    }
}
