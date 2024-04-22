using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeesAPI.Models.Dto
{
    public class DepartmentDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Name { get; set; }
        [Phone]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Phone { get; set; }
    }
}
