using ManageEmployees.Dtos.LeaveRequest;
using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    public interface ILeaveRequestRepository
    {
        Task<List<LeaveRequest>> GetLeaveRequestAsync();
        Task<LeaveRequest> CreateLeaveRequestAsync(LeaveRequest leaveRequestToCreate);

        Task<LeaveRequest> GetLeaveRequestAsync(int leaveRequestId);

        Task UpdateLeaveRequestStatusAsync(LeaveRequest leaveRequest);
 
        Task<List<LeaveRequest>> GetLeaveRequestByEmployeeId(int employeeId);

    }
}
