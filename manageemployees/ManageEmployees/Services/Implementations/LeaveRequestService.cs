using ManageEmployees.Dtos.Employees;
using ManageEmployees.Dtos.LeaveRequest;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Repositories.Implementations;
using ManageEmployees.Services.Contracts;

namespace ManageEmployees.Services.Implementations
{
    public class LeaveRequesteService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        public LeaveRequesteService(ILeaveRequestRepository leaveRequest)
        {
            _leaveRequestRepository = leaveRequest;
           
        }
        public async Task<List<ReadLeaveRequest>> GetReadLeaveRequests()
        {
            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestAsync();

            List<ReadLeaveRequest> readLeaveRequests = new List<ReadLeaveRequest>();

            foreach (var leaveRequest in leaveRequests)
            {
                readLeaveRequests.Add(new ReadLeaveRequest()
                {
                    Id = leaveRequest.LeaveRequestId,
                    EmployeeId = leaveRequest.EmployeeId,
                    RequestDate = leaveRequest.RequestDate,
                    StartDate = leaveRequest.StartDate,
                    EndDate = leaveRequest.EndDate,
                    LeaveRequestStatusId = leaveRequest.LeaveRequestStatusId
                    
                });
            }

            return readLeaveRequests;
        }
        public async Task<ReadLeaveRequest> CreateLeaveRequestAsync(CreateLeaveRequest leaveRequest)
        {
            var leaveRequesteCreated = new LeaveRequest()
            {
                
                EmployeeId = leaveRequest.EmployeeId,
                RequestDate = leaveRequest.RequestDate,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                LeaveRequestStatusId = leaveRequest.LeaveRequestStatusId,
            };

            var leaveRequestedCreated = await _leaveRequestRepository.CreateLeaveRequestAsync(leaveRequesteCreated);

            return new ReadLeaveRequest()
            {
                Id = leaveRequestedCreated.LeaveRequestId,
                EmployeeId = leaveRequestedCreated.EmployeeId,
                RequestDate = leaveRequestedCreated.RequestDate,
                StartDate = leaveRequestedCreated.StartDate,
                EndDate = leaveRequestedCreated.EndDate,
                LeaveRequestStatusId = leaveRequestedCreated.LeaveRequestStatusId,

            };
        }

       
        public async Task<ReadLeaveRequest> GetLeaveRequestByIdAsync(int leaveRequestId)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestAsync(leaveRequestId);

            if (leaveRequest == null)
                throw new Exception($"Echec de recupération de la demande de congé elle n'existe pas : {leaveRequestId}");

            return new ReadLeaveRequest()
            {
                Id = leaveRequest.LeaveRequestId,
                EmployeeId = leaveRequest.EmployeeId,
                RequestDate = leaveRequest.RequestDate,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                LeaveRequestStatusId = leaveRequest.LeaveRequestStatusId,
            };
        }

       
        public async Task UpdateLeaveRequestAsync(int leaveRequestId, UpdateLeaveRequest newLeaveRequest)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestAsync(leaveRequestId);

            if (leaveRequest == null)
            {
                throw new Exception($"Echec de mise à jour du congé : Il n'existe aucun congé avec cet identifiant : {leaveRequestId}");

            }

            if (newLeaveRequest.LeaveRequestStatusId == 1)
            {
                throw new Exception($"Echec de mise à jour du congé : Vous devez ou accepter (2) ou refuser (3) le congé !");
            }
            else if (newLeaveRequest.LeaveRequestStatusId > 3)
            {
                throw new Exception($"Echec de mise à jour du congé : le status ne dois pas être supérieur à 3 !");
            }

            leaveRequest.LeaveRequestStatusId = newLeaveRequest.LeaveRequestStatusId;

            await _leaveRequestRepository.UpdateLeaveRequestStatusAsync(leaveRequest);

        }


      
        public async Task<List<ReadLeaveRequest>> GetLeaveRequestByEmployeeId(int employeeId)
        {
            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestByEmployeeId(employeeId);

            if (leaveRequests == null)
                throw new Exception($"Echec de recupération des informations des congés car il n'existe pas de congés avec l'id de cet employée: {employeeId}");

            List<ReadLeaveRequest> readLeaveRequest = new List<ReadLeaveRequest>();

            foreach (var leaveRequest in leaveRequests)
                readLeaveRequest.Add(new ReadLeaveRequest()
                {
                    Id = leaveRequest.LeaveRequestId,
                    EmployeeId = leaveRequest.EmployeeId,
                    RequestDate = leaveRequest.RequestDate,
                    StartDate = leaveRequest.StartDate,
                    EndDate = leaveRequest.EndDate,
                    LeaveRequestStatusId = leaveRequest.LeaveRequestStatusId,
                });
            return readLeaveRequest;
        }
    }
}
