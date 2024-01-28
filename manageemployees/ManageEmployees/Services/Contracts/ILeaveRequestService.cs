using ManageEmployees.Dtos.Employees;
using ManageEmployees.Dtos.LeaveRequest;

namespace ManageEmployees.Services.Contracts
{
    public interface ILeaveRequestService
    {
        Task<List<ReadLeaveRequest>> GetReadLeaveRequests();

        Task<ReadLeaveRequest> CreateLeaveRequestAsync(CreateLeaveRequest leaveRequest);
        
        Task<ReadLeaveRequest> GetLeaveRequestByIdAsync(int leaveRequestId);
        
        Task UpdateLeaveRequestAsync(int leaveRequestId, UpdateLeaveRequest newLeaveRequest);
       
        Task<List<ReadLeaveRequest>> GetLeaveRequestByEmployeeId(int employeeId);
    }
}
