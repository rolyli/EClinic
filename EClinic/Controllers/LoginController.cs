using System.Text.Json;
using System.Threading.Tasks;
using EClinic.Dtos;
using EClinic.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EClinic.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly IPatientRepository patientRepository;
        private readonly IDoctorRepository doctorRepository;

        public LoginController(IPatientRepository patientRepository, IDoctorRepository doctorRepository)
        {
            this.patientRepository = patientRepository;
            this.doctorRepository = doctorRepository;
        }

        [HttpPost]
        public async Task<ActionResult> LoginPatientAsync(LoginPatientDto loginPatientDto)
        {
            var patient = await patientRepository.GetPatientAsync(loginPatientDto.Username);
            var doctor = await doctorRepository.GetDoctorAsync(loginPatientDto.Username);

            // todo: return JWT token
            if ((patient != null) && (patient.Password == loginPatientDto.Password))
            {
                // login successful
                return Content(JsonSerializer.Serialize(patient));
            }

            if ((doctor != null) && (doctor.Password == loginPatientDto.Password))
            {
                // login successful
                return Content(JsonSerializer.Serialize(doctor));
            }

            // login failure
            return Content("false");
        }

        /* can't figure out why this doesn't work so just use HTTP POST to /patient for signup
        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult> SignupPatientAsync(CreatePatientDto createPatientDto)
        {
            return await Task.Run<ActionResult>(() =>
                RedirectToAction("CreatePatientAsync", "PatientController")
            );
        }
        */
    }
}