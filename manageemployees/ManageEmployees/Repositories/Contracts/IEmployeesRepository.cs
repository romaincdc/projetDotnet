using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    public interface IEmployeesRepository
    {
        Task<List<Employee>> GetEmployeesAsync();

        Task<Employee> GetEmployeesByIdAsync(int employeesId);

        Task<Employee> GetEmployeesByIdWithIncludeAsync(int employeesId);

        Task<Employee> GetEmployeesByNameAsync(string employeesName);

        Task UpdateEmployeesAsync(Employee employeesToUpdate);

        Task<Employee> CreateEmployeesAsync(Employee employeesToCreate);

        Task<Employee> DeleteEmployeesByIdAsync(int employeesId);

        Task<EmployeesDepartment> GetEmployeeDepartmentAsync(int employeeId, int departmentId);

        Task<EmployeesDepartment> CreateEmployeeDepartementAsync(EmployeesDepartment employeeDepartement);
        Task DeleteEmployeeFromDepartmentAsync(int employeeId, int departmentId);
    }
}
