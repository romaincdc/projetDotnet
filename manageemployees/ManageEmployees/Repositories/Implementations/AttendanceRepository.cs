using ManageEmployees.Entities;
using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Repositories.Implementations
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ManageEmployeeDbContext _dbContext;

        public AttendanceRepository(ManageEmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Attendance>> GetAttendancesAsync()
        {
            return await _dbContext.Attendances.ToListAsync();
        }

        public async Task<Attendance> CreateAttendanceByEmployeeAsync(Attendance attendance)
        {
            await _dbContext.Attendances.AddAsync(attendance);
            await _dbContext.SaveChangesAsync();
            return attendance;
        }

        public async Task<Attendance> GetAttendanceAsync(int attendanceId)
        {
            return await _dbContext.Attendances.FirstOrDefaultAsync(a => a.AttendanceId == attendanceId);

        }

        public async Task<int> GetAttendanceByEmployeeAndDateAsync(int employeeId, DateTime date)
        {
            return await _dbContext.Attendances
                .Where(a => a.EmployeeId == employeeId && a.EndDate == date)
                .CountAsync();
        }

        public async Task<List<Attendance>> GetAttendancesByEmployeeId(int employeeId)
        {
            return await _dbContext.Attendances
            .Where(a => a.EmployeeId == employeeId)
            .ToListAsync();
        }

    }
}
