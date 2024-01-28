using ManageEmployees.Entities;
using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Repositories.Implementations
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly ManageEmployeeDbContext _dbContext;

        public EmployeesRepository(ManageEmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _dbContext.Employees.ToListAsync();
                
        }

        public async Task<Employee> GetEmployeesByIdAsync(int employeesId)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employeesId);
        }

        public async Task<Employee> GetEmployeesByIdWithIncludeAsync(int employeesId)
        {
            return await _dbContext.Employees
                .Include(x => x.EmployeesDepartments)
                .FirstOrDefaultAsync(x => x.EmployeeId == employeesId);
        }

        public async Task<Employee> GetEmployeesByNameAsync(string employeesName)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.LastName == employeesName);
        }

        public async Task UpdateEmployeesAsync(Employee employeesToUpdate)
        {
            _dbContext.Employees.Update(employeesToUpdate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateEmployeesDepartmentAsync(EmployeesDepartment employeesDepartmentToUpdate)
        {
            _dbContext.EmployeesDepartments.Update(employeesDepartmentToUpdate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Employee> CreateEmployeesAsync(Employee employeesToCreate)
        {
            await _dbContext.Employees.AddAsync(employeesToCreate);
            await _dbContext.SaveChangesAsync();

            return employeesToCreate;
        }

        public async Task<Employee> DeleteEmployeesByIdAsync(int employeesID)
        {
            var employeesToDelete = await _dbContext.Employees.FindAsync(employeesID);
            _dbContext.Employees.Remove(employeesToDelete);
            await _dbContext.SaveChangesAsync();
            return employeesToDelete;
        }
        public async Task<EmployeesDepartment> CreateEmployeeDepartementAsync(EmployeesDepartment employeeDepartment)
        {
            await _dbContext.EmployeesDepartments.AddAsync(employeeDepartment);
            await _dbContext.SaveChangesAsync();
            return employeeDepartment;
        }
        public async Task<EmployeesDepartment> GetEmployeeDepartmentAsync(int employeeId, int departmentId)
        {
            return await _dbContext.EmployeesDepartments.FirstOrDefaultAsync(ed => ed.EmployeeId == employeeId && ed.DepartmentId == departmentId);
        }
        
        public async Task DeleteEmployeeFromDepartmentAsync(int employeeId, int departmentId)
        {
            var employeeDepartment = await GetEmployeeDepartmentAsync(employeeId, departmentId);

            _dbContext.EmployeesDepartments.Remove(employeeDepartment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
