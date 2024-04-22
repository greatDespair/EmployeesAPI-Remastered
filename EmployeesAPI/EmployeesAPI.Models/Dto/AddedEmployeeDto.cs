using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesAPI.Models.Dto
{
    public class AddedEmployeeDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        [Phone]
        public string? Phone { get; set; }
        public int? CompanyID { get; set; }
        public PassportDto? Passport { get; set; }
        public DepartmentDto? Department { get; set; }
    }
}
