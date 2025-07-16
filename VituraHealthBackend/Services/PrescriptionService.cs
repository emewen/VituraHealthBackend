using VituraHealthBackend.Models;

namespace VituraHealthBackend.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private ILogger<PrescriptionService> _logger;
        private ICacheService _cacheService;
        private readonly string CACHEKEY = "prescriptions";

        public PrescriptionService(ILogger<PrescriptionService> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }

        public async Task<List<Prescription>> GetPrescriptionsByPatientId(int patientId)
        {
            _logger.LogInformation(String.Format("Retrieving Prescriptions for patientId: {0}.", patientId));
            return (await _cacheService.GetFromCache<Prescription>(CACHEKEY))
                .Where(p => p.PatientId == patientId)
                .ToList();
        }

        public async Task<Prescription> CreatePrescription(Prescription prescription)
        {
            _logger.LogInformation(String.Format("Creating Prescription for patientId: {0}", prescription.PatientId));

            var prescriptions = await _cacheService.GetFromCache<Prescription>(CACHEKEY);

            int newId = prescriptions.Max(p => p.Id) + 1;

            //add item
            prescription.Id = newId;
            List<Prescription> newPrescriptionList = prescriptions.ToList();
            newPrescriptionList.Add(prescription);
            _cacheService.SetCache<Prescription>(CACHEKEY, newPrescriptionList.ToArray());

            return prescription;
        }
    }
}
