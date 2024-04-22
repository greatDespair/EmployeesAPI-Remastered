using EmployeesAPI.Models.Database;
using EmployeesAPI.Domain.Abstractions;
using AutoMapper;
using EmployeesAPI.Models.Dto;
using EmployeesAPI.Domain.Exceptions;

namespace EmployeesAPI.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;
        private IPassportRepository _passportRepository;
        private IDepartmentRepository _departmentRepository;
        private IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository,
            IPassportRepository passportRepository,
            IDepartmentRepository departmentRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _passportRepository = passportRepository;
            _departmentRepository = departmentRepository;

            _mapper = mapper;
        }

        public async Task<int?> AddEmployee(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            var passport = employee.Passport;
            var department = employee.Department;

            var passportCheck = await _passportRepository.ReadByNumAsync(employeeDto.Passport.Number);
            if (passportCheck != null)
            {
                throw new AppException("User with passport '" + employeeDto.Passport.Number + "' already exists");
            }

            var passportInserted = await _passportRepository.Add(passport);

            if (passportInserted != null)
            {
                var departmentInserted = await _departmentRepository.Add(department);
                if (departmentInserted != null)
                {
                    employee.DepartmentId = departmentInserted;
                    employee.PassportId = passportInserted;

                    return await _employeeRepository.Add(employee);
                }
            }

            return 0;
        }

        public async Task DeleteEmployee(int id)
        {
            var passportId = await _employeeRepository.GetPassportId(id);
            if (passportId != null)
            {
                await _employeeRepository.Delete(id);
                await _passportRepository.Delete((int)passportId);
            }
            else
            {
                throw new KeyNotFoundException("Employee not found");
            }
        }

        public async Task<List<EmployeeDto>> GetEmployeesByCompanyId(int id)
        {
            List<EmployeeDto> employees = new List<EmployeeDto>();
            var result = await _employeeRepository.GetAllByCompanyId(id);
            foreach (var employee in result)
            {
                employees.Add(_mapper.Map<EmployeeDto>(employee));
            }
            return employees;
        }

        public async Task<List<EmployeeDto>> GetEmployeesByDepartmentName(string name)
        {
            List<EmployeeDto> employees = new List<EmployeeDto>();
            var result = await _employeeRepository.GetAllByDepartmentName(name);
            foreach (var employee in result)
            {
                employees.Add(_mapper.Map<EmployeeDto>(employee));
            }
            return employees;
        }

        public async Task UpdateEmployee(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            var checkEmployee = await _employeeRepository.ReadAsync(employee.Id);

            if(checkEmployee == null) 
            {
                throw new KeyNotFoundException("Employee not found");
            }

            await _employeeRepository.Update(employee);

            if (employee.Passport != null)
            {
                var passportId = await _employeeRepository.GetPassportId(employeeDto.Id);
                employee.Passport.Id = (int)passportId;
                await _passportRepository.Update(employee.Passport);
            }

            if (employee.Department != null)
            {
                var departmentId = await _employeeRepository.GetDepartmentId(employeeDto.Id);
                employee.Department.Id = (int)departmentId;
                await _departmentRepository.Update(employee.Department);
            }
        }
    }
}

