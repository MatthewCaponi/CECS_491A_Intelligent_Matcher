using BusinessModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Archiving
{
    public interface IFolderHandlerService
    {
        Task<List<string>> GetSubFolders(string path);
    }
}
