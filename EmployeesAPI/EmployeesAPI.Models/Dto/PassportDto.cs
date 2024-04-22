using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeesAPI.Models.Dto
{
    public class PassportDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Type { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Number { get; set; }
    }
}
