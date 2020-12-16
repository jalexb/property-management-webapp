using Capstone.Entities;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    public interface IWorkerService
    {
        List<WorkerInformation>  GetWorkers();
    }
}