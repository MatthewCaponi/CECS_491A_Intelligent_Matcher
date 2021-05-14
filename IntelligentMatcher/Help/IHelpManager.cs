using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Help
{
    public interface IHelpManager
    {
        Task<bool> SendHelpEmail(string subject, string message);
    }
}
