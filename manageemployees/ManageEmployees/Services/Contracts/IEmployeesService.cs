using ManageEmployees.Dtos.Employees;

namespace ManageEmployees.Services.Contracts
{
    public interface IEmployeesService
    {
        Task<ReadEmployees> CreateEmployeesAsync(CreateEmployees employees);

        /// <summary>
        /// Updates the department asynchronous.
        /// </summary>
        /// <param name="employeesId">The department identifier.</param>
        /// <param name="employees">The department.</param>
        /// <exception cref="System.Exception">
        /// Echec de mise à jour d'un département : Il n'existe aucun departement avec cet identifiant : {departmentId}
        /// or
        /// Echec de mise à jour d'un département : Il existe déjà un département avec ce nom {department.Name}
        /// </exception>
        Task UpdateEmployeesAsync(int employeesId, UpdateEmployees employees);

        /// <summary>
        /// Gets the departments.
        /// </summary>
        /// <returns></returns>
        Task<List<ReadEmployees>> GetEmployees();

        /// <summary>
        /// Gets the department by identifier asynchronous.
        /// </summary>
        /// <param name="employeesId">The department identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'un département car il n'existe pas : {departmentId}</exception>
        Task<ReadEmployees> GetEmployeesByIdAsync(int employeesId);

        /// <summary>
        /// Gets the department by name asynchronous.
        /// </summary>
        /// <param name="employeesName">Name of the department.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'un département car il n'existe pas de nom correspondant : {departmentName}</exception>
        Task<ReadEmployees> GetEmployeesByNameAsync(string employeesName);

        /// <summary>
        /// Deletes the department by identifier.
        /// </summary>
        /// <param name="employeesId">The department identifier.</param>
        /// <exception cref="System.Exception">
        /// Echec de suppression d'un département : Il n'existe aucun departement avec cet identifiant : {departmentId}
        /// or
        /// Echec de suppression car ce departement est lié à des employés
        /// </exception>
        Task DeleteEmployeesById(int employeesId);


        Task CreateEmployeeDepartementAsync(int EmployeeId, int DepartementId);
        Task DeleteEmployeeInDepartment(int employeeId, int departmentId);
    }
}
