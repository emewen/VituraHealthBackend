using Microsoft.AspNetCore.Mvc;
using VituraHealthBackend.Models;
using VituraHealthBackend.Services;

namespace VituraHealthBackend.Controllers
{
    [Route("api/prescriptions")]
    public class PrescriptionController : Controller
    {
        private IPrescriptionService _prescriptionService;
        public PrescriptionController(IPrescriptionService prescriptionService) {
            _prescriptionService = prescriptionService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrescriptionsByPatientId([FromRoute]int id)
        {
            if(id == 0) //inputs 0 and strings are converted to 0
            {
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }
            return Ok(await _prescriptionService.GetPrescriptionsByPatientId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Prescription prescription)
        {
            if (prescription == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            if (prescription.PatientId == 0) //inputs 0 and strings are converted to 0
            {
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }
            if (String.IsNullOrEmpty(prescription.DrugName) || String.IsNullOrEmpty(prescription.Dosage) || prescription.DatePrescribed == DateOnly.MinValue)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return Ok(await _prescriptionService.CreatePrescription(prescription));
        }
    }
}
