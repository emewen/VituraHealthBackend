using Microsoft.AspNetCore.Mvc;
using VituraHealthBackend.Services;

namespace VituraHealthBackend.Controllers
{
    [Route("api/patients")]
    public class PatientController : Controller
    {
        private IPatientService _patientService;
        public PatientController(IPatientService patientService) { 
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _patientService.GetPatients());
        }
    }
}
