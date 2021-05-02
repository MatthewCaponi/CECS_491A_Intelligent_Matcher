using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Archiving
{
    public interface IArchiveService
    {
        bool ArchiveLogFiles(List<String> files);
    }
}
