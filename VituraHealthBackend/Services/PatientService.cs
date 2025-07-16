using VituraHealthBackend.Models;

namespace VituraHealthBackend.Services
{
    public class PatientService : IPatientService
    {
        private ILogger<PatientService> _logger;
        private ICacheService _cacheService; 
        private readonly string CACHEKEY = "patients";

        public PatientService(ILogger<PatientService> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }

        public async Task<List<Patient>> GetPatients()
        {
            _logger.LogInformation("Retrieving patient data.");
            return await _cacheService.GetFromCache<Patient>(CACHEKEY);
        }
    }
}
