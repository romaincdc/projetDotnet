using ManageEmployees.Dtos.Employees;
using ManageEmployees.Dtos.LeaveRequest;
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
    public class LeaveRequestController : ControllerBase
    {
    
        private readonly ILeaveRequestService _leaveRequestService;
      
        public LeaveRequestController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }


        [HttpPost]
        public async Task<ActionResult<CreateLeaveRequest>> AddLeaveRequestAsync([FromBody] CreateLeaveRequest leaveRequest)
        {
            if (leaveRequest == null || leaveRequest.EmployeeId == 0 || default(DateTime) == leaveRequest.RequestDate || default(DateTime) == leaveRequest.StartDate
                || default(DateTime) == leaveRequest.EndDate)
            {
                return BadRequest("Echec de création d'un congé : les informations sont null ou vides");
            }
            else if (leaveRequest.EmployeeId < 0)
                return BadRequest("Echec de création d'une congé  : l'id de l'employée doit être supérieur à 0");
          
            try
            {
                var leaveRequestCreated = await _leaveRequestService.CreateLeaveRequestAsync(leaveRequest);
                return Ok(leaveRequestCreated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLeaveRequestAsync(int id, [FromBody] UpdateLeaveRequest leaveRequest)
        {
            if (leaveRequest == null)
            {
                return BadRequest("Echec de créaction d'un congé : les informations sont null ou vides");
            }
            else if (id < 1)
                return BadRequest("Echec de création d'une congé  : l'id du congé doit être supérieur à 1");

            try
            {
                await _leaveRequestService.UpdateLeaveRequestAsync(id, leaveRequest);
                return Ok("La mise à jour du status à bien été pris en compte");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{leaveRequestId}")]
        public async Task<ActionResult<ReadLeaveRequest>> GetLeaveRequestAsyncById(int leaveRequestId)
        {
            if (leaveRequestId < 1)
            {
                return BadRequest("Echec de la récupération du congé : l'id ne dois pas être inférieur à 1");
            }
            try
            {
                return Ok(await _leaveRequestService.GetLeaveRequestByIdAsync(leaveRequestId));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpGet("/leaveRequests/{idEmployee}")]
        public async Task<ActionResult> GetLeaveRequestByEmployeeId(int idEmployee)
        {
            if (idEmployee < 0)
            {
                return BadRequest("Echec de la recherche des congés : l'id doit être supérieur à 0");
            }

            try
            {
                return Ok(await _leaveRequestService.GetLeaveRequestByEmployeeId(idEmployee));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpGet]
        public async Task<ActionResult<List<ReadLeaveRequest>>> GetLeaveRequest()
        {
            var leaveRequest = await _leaveRequestService.GetReadLeaveRequests();
            return Ok(leaveRequest);
        }

    }
}
