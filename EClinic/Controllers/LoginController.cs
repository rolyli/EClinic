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

        public LoginController(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        [HttpPost]
        public async Task<ActionResult> LoginPatientAsync(LoginPatientDto loginPatientDto)
        {
            // todo: return JWT token instead...
            var patient = await patientRepository.GetPatientAsync(loginPatientDto.Username);

            if ((patient != null) && (patient.Password == loginPatientDto.Password))
            {
                // login successful
                return Content("true");
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