using System.ComponentModel.DataAnnotations;

namespace EmployeesAPI.Models.Database
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public int CompanyID { get; set; }

        public int? PassportId { get; set; }

        public int? DepartmentId { get; set; }

        public Passport Passport { get; set; }

        public Department Department { get; set; }
    }
}
