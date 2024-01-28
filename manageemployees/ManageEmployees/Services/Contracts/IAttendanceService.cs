using ManageEmployees.Dtos.Attendance;

namespace ManageEmployees.Services.Contracts
{
    public interface IAttendanceService
    {

        /// <summary>
        /// Gets the attendances.
        /// </summary>
        /// <returns></returns>
        Task<List<ReadAttendance>> GetAttendances();

        Task<ReadAttendance> CreateAttendanceAsync(CreateAttendance attendance);
        Task<List<ReadAttendance>> GetAttendancesByEmployeeId(int employeeId);


    }
}
