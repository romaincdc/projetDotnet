using ManageEmployees.Dtos.Employees;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Repositories.Implementations;
using ManageEmployees.Services.Contracts;

namespace ManageEmployees.Services.Implementations
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IDepartementRepository _departementRepository;
        public EmployeesService(IEmployeesRepository employeesRepository, IDepartementRepository departementRepository)
        {
            _employeesRepository = employeesRepository;
            _departementRepository = departementRepository;
        }

        /// <summary>
        /// Gets the departments.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReadEmployees>> GetEmployees()
        {
            var employees = await _employeesRepository.GetEmployeesAsync();

            List<ReadEmployees> readEmployees = new List<ReadEmployees>();

            foreach (var employee in employees)
            {
                readEmployees.Add(new ReadEmployees()
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    PhoneNumber = employee.PhoneNumber,
                    Position = employee.Position,
                    Email = employee.Email,
                    BirthdDate = employee.BirthdDate,
                });
            }

            return readEmployees;
        }

        /// <summary>
        /// Gets the department by identifier asynchronous.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'un département car il n'existe pas : {departmentId}</exception>
        public async Task<ReadEmployees> GetEmployeesByIdAsync(int employeesId)
        {
            var employee = await _employeesRepository.GetEmployeesByIdAsync(employeesId);

            if (employee is null)
                throw new Exception($"Echec de recupération des informations d'un département car il n'existe pas : {employeesId}");

            return new ReadEmployees()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                Position = employee.Position,
                Email = employee.Email,
                BirthdDate = employee.BirthdDate,
            };
        }

        /// <summary>
        /// Gets the department by name asynchronous.
        /// </summary>
        /// <param name="departmentName">Name of the department.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'un département car il n'existe pas de nom correspondant : {departmentName}</exception>
        public async Task<ReadEmployees> GetEmployeesByNameAsync(string employeeName)
        {
            var employee = await _employeesRepository.GetEmployeesByNameAsync(employeeName);

            if (employee is null)
                throw new Exception($"Echec de recupération des informations d'un département car il n'existe pas de nom correspondant : {employeeName}");

            return new ReadEmployees()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                Position = employee.Position,
                Email = employee.Email,
                BirthdDate = employee.BirthdDate,

            };
        }

        /// <summary>
        /// Updates the department asynchronous.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <param name="department">The department.</param>
        /// <exception cref="System.Exception">
        /// Echec de mise à jour d'un département : Il n'existe aucun departement avec cet identifiant : {departmentId}
        /// or
        /// Echec de mise à jour d'un département : Il existe déjà un département avec ce nom {department.Name}
        /// </exception>
        public async Task UpdateEmployeesAsync(int employeesId, UpdateEmployees employees)
        {
            var employeesGet = await _employeesRepository.GetEmployeesByIdAsync(employeesId)
                ?? throw new Exception($"Echec de mise à jour d'un département : Il n'existe aucun departement avec cet identifiant : {employeesId}");

            var employeesGetByName = await _employeesRepository.GetEmployeesByNameAsync(employees.LastName);
            if (employeesGetByName is not null && employeesId != employeesGetByName.EmployeeId)
            {
                throw new Exception($"Echec de mise à jour d'un département : Il existe déjà un département avec ce nom {employees.LastName}");
            }

            employeesGet.FirstName = employees.FirstName;
            employeesGet.LastName = employees.LastName;
            employeesGet.PhoneNumber = employees.PhoneNumber;
            employeesGet.Position = employees.Position;
            employeesGet.Email = employees.Email;
            employeesGet.BirthdDate = employees.BirthdDate;

            await _employeesRepository.UpdateEmployeesAsync(employeesGet);

        }

        /// <summary>
        /// Deletes the department by identifier.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <exception cref="System.Exception">
        /// Echec de suppression d'un département : Il n'existe aucun departement avec cet identifiant : {departmentId}
        /// or
        /// Echec de suppression car ce departement est lié à des employés
        /// </exception>
        public async Task DeleteEmployeesById(int employeesId)
        {
            var departmentGet = await _employeesRepository.GetEmployeesByIdWithIncludeAsync(employeesId)
              ?? throw new Exception($"Echec de suppression d'un département : Il n'existe aucun departement avec cet identifiant : {employeesId}");

            if (departmentGet.EmployeesDepartments.Any())
            {
                throw new Exception("Echec de suppression car ce departement est lié à des employés");
            }

            await _employeesRepository.DeleteEmployeesByIdAsync(employeesId);
        }

        /// <summary>
        /// Creates the department asynchronous.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de création d'un département : Il existe déjà un département avec ce nom {department.Name}</exception>
        public async Task<ReadEmployees> CreateEmployeesAsync(CreateEmployees employees)
        {
            var employeeGet = await _employeesRepository.GetEmployeesByNameAsync(employees.LastName);
            if (employeeGet is not null)
            {
                throw new Exception($"Echec de création d'un département : Il existe déjà un département avec ce nom {employees.LastName}");
            }

            var employeeToCreate = new Employee()
            {

                FirstName = employees.FirstName,
                LastName = employees.LastName,
                PhoneNumber = employees.PhoneNumber,
                Position = employees.Position,
                Email = employees.Email,
                BirthdDate = employees.BirthdDate
            };
    
            var employeeCreated = await _employeesRepository.CreateEmployeesAsync(employeeToCreate);

            return new ReadEmployees()
            {
                
                LastName = employeeCreated.LastName,
            };
        }

        public async Task CreateEmployeeDepartementAsync(int employeeId, int departmentId)
        {
            var employee = await _employeesRepository.GetEmployeesByIdAsync(employeeId);
            if (employee == null)
            {
                throw new Exception($"Echec d'ajout d'un département à un employé : Il n'existe aucun employé avec cet identifiant : {employeeId}");
            }

            var department = _departementRepository.GetDepartmentByIdAsync(departmentId);
            if (department == null)
            {
                throw new Exception($"Echec d'ajout d'un département à un employé : Il n'existe aucun département avec cet identifiant : {departmentId}");
            }

            var employeeDepartment = new EmployeesDepartment
            {
                EmployeeId = employeeId,
                DepartmentId = departmentId
            };

            await _employeesRepository.CreateEmployeeDepartementAsync(employeeDepartment);
        }

        public async Task DeleteEmployeeInDepartment(int employeeId, int departmentId)
        {
            var verifEmployee = await _employeesRepository.GetEmployeesByIdAsync(employeeId)
                ?? throw new Exception($"Echec de recherche de cet employée : Il n'existe aucun employée avec cet identifiant : {employeeId}");
            var verifDepartment = await _departementRepository.GetDepartmentByIdAsync(departmentId)
                ?? throw new Exception($"Echec de recherche de ce département : Il n'existe aucun département avec cet identifiant : {departmentId}");

            if (verifEmployee == null && verifDepartment == null)
            {
                throw new Exception($"Echec de suppression de l'employée {employeeId} dans le département {departmentId}");
            }

            var supprEmployee = await _employeesRepository.GetEmployeeDepartmentAsync(verifEmployee.EmployeeId, verifDepartment.DepartmentId);

            if (supprEmployee == null)
            {
                throw new Exception($"Echec de la suppression du salarié dans le département : Ce salarié {employeeId} n'est pas dans le département : {departmentId}");

            }

            await _employeesRepository.DeleteEmployeeFromDepartmentAsync(employeeId, departmentId);

        }


    }

}
