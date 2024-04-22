using System.ComponentModel.DataAnnotations;


namespace EmployeesAPI.Models.Database
{
    public class Department
    {
        public int Id { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentPhone { get; set; }
    }
}
