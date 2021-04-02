using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ILoginAttemptsService
    {
        Task<bool> AddIpAddress(string ipAddress);
        Task<bool> CheckLoginCounterByIpAddress(string ipAddress);
        Task<bool> IncrementLoginCounterByIpAddress(string ipAddress);
        Task<bool> ResetLoginCounterByIpAddress(string ipAddress);
        Task<bool> SetSuspensionEndTimeByIpAddress(string ipAddress);
    }
}
