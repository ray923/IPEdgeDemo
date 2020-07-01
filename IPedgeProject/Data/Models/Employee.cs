using System;

namespace IPedgeProject.Data
{
    public class Employee
    {
        public long EmployeeID { get; set; }
        public int EmployeeNumber { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateJoined { get; set; }

        public short? Extension { get; set; }

        public int? RoleID { get; set; }

        public Role EmployeeRole { get; set; }
    }
}
