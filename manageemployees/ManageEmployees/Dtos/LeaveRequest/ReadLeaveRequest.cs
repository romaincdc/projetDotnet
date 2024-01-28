﻿namespace ManageEmployees.Dtos.LeaveRequest
{
    public class ReadLeaveRequest
    {
       
        public int Id { get; set; }
       
        public int EmployeeId { get; set; }
        
        public DateTime RequestDate { get; set; }
       
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public int LeaveRequestStatusId { get; set; } = 1;

    }
}
