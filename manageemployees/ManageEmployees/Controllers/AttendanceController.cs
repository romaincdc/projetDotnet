using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Entities;
using ManageEmployees.Services.Contracts;
using ManageEmployees.Services.Implementations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
        [HttpGet]
        public async Task<ActionResult<List<ReadAttendance>>> GetAttendanceAsync()
        {
            var attendances = await _attendanceService.GetAttendances();
            return Ok(attendances);
        }

        // POST api/<AttendanceController>
        [HttpPost]
        public async Task<ActionResult<CreateAttendance>> AddAttendance([FromBody] CreateAttendance attendance)
        {


            if (attendance == null || attendance.EmployeeId == 0 || default(DateTime) == attendance.EndDate || default(DateTime) == attendance.StartDate)
            {
                return BadRequest("Echec de création d'une présence : les informations sont null ou vides");
            }
            else if (attendance.EmployeeId < 0)
                return BadRequest("Echec de création d'une présence : l'id doit être supérieur à 0");

            try
            {
                var attendanceCreated = await _attendanceService.CreateAttendanceAsync(attendance);
                return Ok(attendanceCreated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpGet("/attendances/{idEmployee}")]
        public async Task<ActionResult> GetAttendanceByEmployeeId(int idEmployee)
        {
            if (idEmployee < 0)
            {
                return BadRequest("Echec de la recherche d'une présence : l'id doit être supérieur à 0");
            }

            try
            {
                return Ok(await _attendanceService.GetAttendancesByEmployeeId(idEmployee));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

    }
}
