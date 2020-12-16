using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    public class WorkersController : Controller
    {
        private readonly IWorkerService workerService;
        public WorkersController(IWorkerService _workerService)
        {
            workerService = _workerService;  
        }

        [HttpGet("/workers")]
        public IActionResult GetWorkers()
        {
            IActionResult result = BadRequest();
            var workers = workerService.GetWorkers();

            if (workers != null)
            {
                result = Ok(workers);
            }

            return result;
        }
    }
}
