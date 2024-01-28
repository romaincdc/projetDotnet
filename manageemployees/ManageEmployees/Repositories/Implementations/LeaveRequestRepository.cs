using ManageEmployees.Entities;
using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Repositories.Implementations
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly ManageEmployeeDbContext _dbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveRequestRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public LeaveRequestRepository(ManageEmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<LeaveRequest>> GetLeaveRequestAsync()
        {
            return await _dbContext.LeaveRequests.ToListAsync();

        }
        /// <summary>
        /// Creates the leave request asynchronous.
        /// </summary>
        /// <param name="leaveRequestToCreate">The leave request to create.</param>
        /// <returns></returns>
        public async Task<LeaveRequest> CreateLeaveRequestAsync(LeaveRequest leaveRequestToCreate)
        {
            await _dbContext.LeaveRequests.AddAsync(leaveRequestToCreate);
            await _dbContext.SaveChangesAsync();
            return leaveRequestToCreate;
        }

        /// <summary>
        /// Gets the leave request asynchronous.
        /// </summary>
        /// <param name="leaveRequestId">The leave request identifier.</param>
        /// <returns></returns>
        public async Task<LeaveRequest> GetLeaveRequestAsync(int leaveRequestId)
        {
            return await _dbContext.LeaveRequests.FirstOrDefaultAsync(lr => lr.LeaveRequestId == leaveRequestId);

        }

        /// <summary>
        /// Updates the leave request status asynchronous.
        /// </summary>
        /// <param name="leaveRequest">The leave request.</param>
        public async Task UpdateLeaveRequestStatusAsync(LeaveRequest leaveRequest)
        {
            _dbContext.LeaveRequests.Update(leaveRequest);
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Gets the leave request by employee identifier.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public async Task<List<LeaveRequest>> GetLeaveRequestByEmployeeId(int employeeId)
        {
            return await _dbContext.LeaveRequests
            .Where(a => a.EmployeeId == employeeId)
            .ToListAsync();
        }

    }
}
