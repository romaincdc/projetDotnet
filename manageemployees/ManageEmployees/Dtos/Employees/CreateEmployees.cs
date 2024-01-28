namespace ManageEmployees.Dtos.Employees
{
    public class CreateEmployees
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime BirthdDate { get; set; }

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? Position { get; set; }
    }
}
