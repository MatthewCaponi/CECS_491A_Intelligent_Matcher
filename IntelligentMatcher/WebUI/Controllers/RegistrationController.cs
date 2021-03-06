using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logging;
using Registration;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegistrationManager _registrationManager;
        private ILogServiceFactory _logFactory;
        private ILogService _logService;
        public RegistrationController(Registration.IRegistrationManager registrationManager, ILogServiceFactory logFactory)
        {
            _registrationManager = registrationManager;
            _logFactory = logFactory;

            _logFactory.AddTarget(TargetType.Text);
            _logService = _logFactory.CreateLogService<UserHomeController>(); // place holder
        }
        public IActionResult Index()
        {
            return View(); // place holder
        }
    }
}
