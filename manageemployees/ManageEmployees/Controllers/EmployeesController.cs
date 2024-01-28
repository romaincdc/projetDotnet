using ManageEmployees.Dtos.Employees;
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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeeService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeeService = employeesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReadEmployees>>> GetEmployeesAsync()
        {
            var employees  = await _employeeService.GetEmployees();
            return Ok(employees);
        }

        [HttpGet("GetEmplByName/{name}")]
        public async Task<ActionResult<ReadEmployees>> GetEmployeesByIdAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                BadRequest("Echec de recupération d'un employee : le nom du employee est invalide");

            try
            {
                var employees  = await _employeeService.GetEmployeesByNameAsync(name);
                return Ok(employees );
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("GetEmplById/{id}")]
        public async Task<ActionResult<ReadEmployees>> GetEmployeesByIdAsync(int id)
        {
            if (id < 1)
                BadRequest($"Echec de recupération d'un employee : Il n'existe pas de employee avec cet Id {id}");

            try
            {
                var employees = await _employeeService.GetEmployeesByIdAsync(id);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        // POST api/<employeesController>
        [HttpPost]
        public async Task<ActionResult<ReadEmployees>> Post([FromBody] CreateEmployees employees)
        {
            if (employees == null || string.IsNullOrWhiteSpace(employees.FirstName)
                || string.IsNullOrWhiteSpace(employees.LastName) || string.IsNullOrWhiteSpace(employees.Email) || string.IsNullOrWhiteSpace(employees.PhoneNumber) || string.IsNullOrWhiteSpace(employees.Position))
            {
                return BadRequest("Echec de création d'un employee : les informations sont null ou vides");
            }

            if (!employees.Email.Contains("@") && !employees.Email.Contains(".fr") || !employees.Email.Contains(".com"))
            {
                return BadRequest("Echec de créaction d'un employée : vous devez entrer un email valide ('@' ou '.fr' / '.com').");
            }

            if (employees.PhoneNumber.Length < 10)
            {
                return BadRequest("Echec de la création d'un employée : vous devez entrer un vrai numéro (au moins 10 chiffres)");

            }
            if (employees.BirthdDate.Year > 2008)
            {
                return BadRequest("Echec de la création d'un employée : vous devez avoir minimum 16 ans");
            }
            try
            {
                var employeesCreated = await _employeeService.CreateEmployeesAsync(employees);
                return Ok(employeesCreated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("UpdateEmpl/{id}")]
        public async Task<ActionResult> UpdateDEmployeesAsync(int id,[FromBody] UpdateEmployees  employees )
        {
            if (employees  == null || string.IsNullOrWhiteSpace(employees.FirstName)
                || string.IsNullOrWhiteSpace(employees.LastName) || string.IsNullOrWhiteSpace(employees.PhoneNumber) || string.IsNullOrWhiteSpace(employees.Position) || string.IsNullOrWhiteSpace(employees.Email))
            {
                return BadRequest("Echec de mise jour d'un employee : les informations sont null ou vides");
            }

            try
            {
                await _employeeService.UpdateEmployeesAsync(id, employees );
                return Ok(new
                {
                    Message = $"Succès de la mise à jour du employee : {id}",
                }) ;
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpDelete("DeleteEmpl/{id}")]
        public async Task<ActionResult> DeleteEmployeesByIdAsync(int id)
        {
            if (id < 1)
                BadRequest($"Echec de suppression d'un employee : Il n'existe pas de employee avec cet Id {id}");

            try
            {
                await _employeeService.DeleteEmployeesById(id);
                return Ok(new
                {
                    Message = $"Succès de la suppression du employee : {id}",
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // POST api/<employeesController>
        [HttpPut("{employeeId}/departments/{departmentId}")]
        public async Task<ActionResult> AddDepartmentToEmployee(int employeeId, int departmentId)
        {
            if (employeeId < 0)
            {
                return BadRequest("Echec de d'ajout d'un département à l'employé : le format de l'ID est invalide");
            }
            if (departmentId < 0)
            {
                return BadRequest("Echec de d'ajout d'un département à l'employé : le format de l'ID est invalide");
            }

            try
            {
                await _employeeService.CreateEmployeeDepartementAsync(employeeId, departmentId);
                return Ok("Département d'ID " + departmentId + " ajouté avec succès à l'employé d'ID " + employeeId);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpDelete("{employeeId}/departments/{departmentId}")]
        public async Task<ActionResult> DeleteEmployeeDepartmentAsync(int employeeId, int departmentId)
        {
            if (employeeId < 0)
            {
                return BadRequest("Echec de la récupération de l'employée : l'id ne dois pas être inférieur à 1");

            }
            else if (departmentId < 0)
            {
                return BadRequest("Echec de la récupération du département : l'id ne dois pas être inférieur à 1");
            }

            try
            {
                await _employeeService.DeleteEmployeeInDepartment(employeeId, departmentId);
                return Ok($"Vous avez bien supprimé l'employée {employeeId} au département {departmentId} !");

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
