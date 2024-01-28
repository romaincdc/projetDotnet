using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Repositories.Implementations;
using ManageEmployees.Services.Contracts;

namespace ManageEmployees.Services.Implementations
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        
        private readonly IEmployeesRepository _employeesRepository;

        public AttendanceService( IAttendanceRepository attendanceRepository, IEmployeesRepository employeesRepository)
        {
            _attendanceRepository = attendanceRepository;
            _employeesRepository = employeesRepository;
        }

        /// <summary>
        /// Gets the departments.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReadAttendance>> GetAttendances()
        {
            var attendances = await _attendanceRepository.GetAttendancesAsync();

            List<ReadAttendance> readAttendance = new List<ReadAttendance>();

            foreach (var attendance in attendances)
            {
                readAttendance.Add(new ReadAttendance()
                {
                    EmployeeId = attendance.EmployeeId,
                    StartDate = attendance.StartDate,
                    EndDate = attendance.EndDate
                });
            }

            return readAttendance;
        }


        /// <summary>
        /// Creates the department asynchronous.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de création d'un département : Il existe déjà un département avec ce nom {department.Name}</exception>
        public async Task<ReadAttendance> CreateAttendanceAsync(CreateAttendance attendance)
        {
            var attendanceCreate = new Attendance()
            {
                EmployeeId = attendance.EmployeeId,
                EndDate = attendance.EndDate,
                StartDate = attendance.StartDate,
            };

            var attendanceCreated = await _attendanceRepository.CreateAttendanceByEmployeeAsync(attendanceCreate);

            if (await _attendanceRepository.GetAttendanceByEmployeeAndDateAsync(attendanceCreated.EmployeeId, DateTime.Today) > 4)
            {
                throw new Exception($"Echec de Création d'une présence  : Vous n'avez pas le droit de faire plus de 4 présences par jours !");
            }

            return new ReadAttendance()
            {
                
                EmployeeId = attendanceCreated.EmployeeId,
                EndDate = attendanceCreated.EndDate,
                StartDate = (DateTime)attendanceCreated.StartDate,
            };

        }
        public async Task<List<ReadAttendance>> GetAttendancesByEmployeeId(int employeeId)
        {
            var attendances = await _attendanceRepository.GetAttendancesByEmployeeId(employeeId);

            if (attendances == null)
                throw new Exception($"Echec de recupération des informations d'une présence car il n'existe pas de présence avec l'id de cet employée: {employeeId}");

            List<ReadAttendance> readAttendance = new List<ReadAttendance>();

            foreach (var attendance in attendances)
            {
                readAttendance.Add(new ReadAttendance()
                {
                    
                    EmployeeId = attendance.EmployeeId,
                    EndDate = attendance.EndDate,
                    StartDate = (DateTime)attendance.StartDate,
                });
            }

            return readAttendance;

        }
    }
}
