using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Archiving
{
    public interface IArchiveService
    {
        bool ArchiveLogFiles(List<String> files);
        bool DeleteArchivedFiles(List<String> zipFiles);
        bool RecoverLogFiles(List<String> files);
    }
}
