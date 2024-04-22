using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesAPI.Models.Database
{
    public class EmployeeShortInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public int CompanyID { get; set; }

        public string Type { get; set; }

        public string Number { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentPhone { get; set; }
    }
}
