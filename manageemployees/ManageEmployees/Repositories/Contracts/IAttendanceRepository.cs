using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    public interface IAttendanceRepository
    {
        Task<List<Attendance>> GetAttendancesAsync();

        Task<Attendance> CreateAttendanceByEmployeeAsync(Attendance attendance);
        Task<int> GetAttendanceByEmployeeAndDateAsync(int employeeId, DateTime date);
        Task<Attendance> GetAttendanceAsync(int attendanceId);
        Task<List<Attendance>> GetAttendancesByEmployeeId(int employeeId);

    }
}
