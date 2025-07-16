using VituraHealthBackend.Models;

namespace VituraHealthBackend.Services
{
    public interface IPatientService
    {
        public Task<List<Patient>> GetPatients();
    }
}
