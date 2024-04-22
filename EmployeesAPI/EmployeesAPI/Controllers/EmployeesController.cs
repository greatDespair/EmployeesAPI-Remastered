﻿using AutoMapper;
using EmployeesAPI.Models.Database;
using Microsoft.AspNetCore.Mvc;
using EmployeesAPI.Domain.Services;
using EmployeesAPI.Models.Dto;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;
using EmployeesAPI.Domain.Abstractions;

namespace EmployeesAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с сотрудниками и их данными
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeRepository _employeeRepo;
        private IMapper _mapper;
        private EmployeeService _employeeService;
        private IPassportRepository _passportRepository;
        private IDepartmentRepository _departmentRepository;
        public EmployeesController(IEmployeeRepository employeeRepo, IPassportRepository passportRepo, IDepartmentRepository departmentRepo, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _passportRepository = passportRepo;
            _departmentRepository = departmentRepo;
            _mapper = mapper;
            _employeeService = new EmployeeService(_employeeRepo,_passportRepository, _departmentRepository, _mapper);
        }

        /// <summary>
        /// Возвращает список сотрудников по названию отдела
        /// </summary>
        /// <param name="name">Название отдела</param>
        /// <response code="200">Сотрудники успешно возвращены</response>
        /// <response code="404">Сотрудники не найдены</response>
        [SwaggerResponse((int)HttpStatusCode.OK, "List<EmployeeDto>", typeof(List<EmployeeDto>))]
        [HttpGet("GetEmployeesByDepartmentName")]
        public async Task<IActionResult> GetEmployeesByDepartmentName(string name)
        {
            var result = await _employeeService.GetEmployeesByDepartmentName(name);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Возвращает список сотрудников по id компании
        /// </summary>
        /// <param name="id">Id компании</param>
        /// <response code="200">Сотрудники успешно возвращены</response>
        /// <response code="404">Сотрудники не найдены</response>
        [SwaggerResponse((int)HttpStatusCode.OK, "List<EmployeeDto>", typeof(List<EmployeeDto>))]
        [HttpGet("GetEmployeesByCompanyId")]
        public async Task<IActionResult> GetEmployeesByCompanyId(int id)
        {
            var result = await _employeeService.GetEmployeesByCompanyId(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Добавляет нового сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        /// <response code="200">Сотрудник успешно добавлен</response>
        [SwaggerResponse((int)HttpStatusCode.OK, "Id", typeof(int))]
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeDto employee)
        {
            var result = await _employeeService.AddEmployee(employee);
            return Ok(result);
        }

        /// <summary>
        /// Удаляет информацию о сотруднике
        /// </summary>
        /// <param name="id">Id удаляемого сотрудника</param>
        /// <response code="204">Сотрудник успешно удален</response>
        /// <response code="404">Данные о сотруднике не найдены</response>
        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployee(id);
            return NoContent();
        }

        /// <summary>
        /// Обновляет информацию о сотруднике
        /// </summary>
        /// <param name="employee">Обновляемый сотрудник</param>
        /// <response code="204">Сотрудник успешно обновлен</response>
        /// <response code="404">Данные о сотруднике не найдены</response>
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeDto employee)
        {
            await _employeeService.UpdateEmployee(employee);
            return NoContent();
        }
    }
}
