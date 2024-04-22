using EmployeesAPI.Models.Dto;

namespace EmployeesAPI.Validation
{
    public static class ValidateEmployee
    {
        private const string _errorMessageEmpty = " cannot be empty";
        public static IEnumerable<string> TryValidateToInsert(this AddedEmployeeDto item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                yield return nameof(item.Name) + _errorMessageEmpty;
            }

            if (string.IsNullOrEmpty(item.Phone))
            {
                yield return nameof(item.Phone) + _errorMessageEmpty;
            }

            if (item.CompanyID == null)
            {
                yield return nameof(item.CompanyID) + _errorMessageEmpty;
            }

            if (item.Passport == null)
            {
                yield return nameof(item.Passport) + _errorMessageEmpty;
            }
            else
            {
                if(string.IsNullOrEmpty(item.Passport.Type))
                {
                    yield return nameof(item.Passport.Type) + _errorMessageEmpty;
                }
                if (string.IsNullOrEmpty(item.Passport.Number))
                {
                    yield return nameof(item.Passport.Number) + _errorMessageEmpty;
                }
            }

            if (item.Department == null)
            {
                yield return nameof(item.Department) + _errorMessageEmpty;
            }
            else
            {
                if (string.IsNullOrEmpty(item.Department.Name))
                {
                    yield return nameof(item.Passport.Type) + _errorMessageEmpty;
                }
            }
        }
    }
}
