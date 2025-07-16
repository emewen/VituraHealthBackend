using Microsoft.Extensions.Logging;
using Moq;
using VituraHealthBackend.Models;
using VituraHealthBackend.Services;

namespace VituraHealthBackendTests
{
    public class PatientServiceTests
    {
        private Mock<ILogger<PatientService>> mockLogger = new Mock<ILogger<PatientService>>();
        private Mock<ICacheService> mockCache = new Mock<ICacheService>();

        private PatientService patientService;


        [SetUp]
        public void Setup()
        {
            List<Patient> patients = new List<Patient>();
            Patient patient = new Patient() {
                Id = 1,
                FullName = "TestName",
                DateOfBirth = new DateOnly(2025, 1, 11)
            };
            patients.Add(patient);
            mockCache.Setup(m => m.GetFromCache<Patient>("patients")).Returns(Task.FromResult(patients));
            patientService = new PatientService(mockLogger.Object, mockCache.Object);
        }

        [Test]
        public void GetPatients_RunSuccessfully()
        {
            var returnedValue = patientService.GetPatients().Result;

            Assert.IsNotNull(returnedValue);
            Assert.IsTrue(returnedValue.Count() == 1);
            Assert.That(returnedValue[0].Id, Is.EqualTo(1));
            Assert.That(returnedValue[0].FullName, Is.EqualTo("TestName"));
            Assert.That(returnedValue[0].DateOfBirth.Year, Is.EqualTo(2025));
            mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals("Retrieving patient data.", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Test]
        public void GetPatients_Exception()
        {
            var exception = new Exception("it broke!");
            mockCache.Setup(m => m.GetFromCache<Patient>("patients")).Throws(exception);

            var ex = Assert.ThrowsAsync<Exception>(() => patientService.GetPatients());
            Assert.That(ex.Message, Is.EqualTo("it broke!"));
        }
    }
}