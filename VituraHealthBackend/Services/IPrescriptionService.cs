using VituraHealthBackend.Models;

namespace VituraHealthBackend.Services
{
    public interface IPrescriptionService
    {
        public Task<List<Prescription>> GetPrescriptionsByPatientId(int patientId);
        public Task<Prescription> CreatePrescription(Prescription prescription);
    }
}
