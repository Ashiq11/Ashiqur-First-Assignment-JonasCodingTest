using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
//using Microsoft.Extensions.Logging;
using Serilog;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Controllers
{

    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public EmployeeController(IEmployeeService employeeService, IMapper mapper, ILogger logger)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _logger = logger;
        }
        // GET api/<controller>
        [HttpGet]
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            try
            {
                _logger.Information("Called method for GetAllEmployees");
                var items = await _employeeService.GetAllEmployees();
                return _mapper.Map<IEnumerable<EmployeeDto>>(items);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error occurred while getting all employees.");
                return Enumerable.Empty<EmployeeDto>(); // Return an empty list in case of an error            }
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("{employeeCode}")]
        public async Task<EmployeeDto> Get(string employeeCode)
        {
            try
            {
                _logger.Information("Called method for GetemployeeByemployeeCode");
                var item = await _employeeService.GetEmployeeByCode(employeeCode);
                if (item == null)
                {
                    _logger.Warning("employee with the given code is not found.");
                }
                return _mapper.Map<EmployeeDto>(item);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error occurred while getting employee with code: {employeeCode}");
                return new EmployeeDto(); // Return a default EmployeeDto or null if an error occurs.
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<bool> Post([FromBody]EmployeeDto employee)
        {
            try
            {
                _logger.Information("POST method called for CreateEmployee");
                var employeeInfo = _mapper.Map<EmployeeInfo>(employee);
                var item = await _employeeService.CreateEmployee(employeeInfo);
                return item;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while saving employee information.");
                _logger.Information($"Post called with employee body {JsonConvert.SerializeObject(employee)}");
                return false;
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("{employeeCode}")]
        public async Task<bool> Put(string employeeCode, [FromBody] EmployeeDto employee)
        {
            try
            {
                _logger.Information("PUT method called for Updateemployee");
                var result = await _employeeService.UpdateEmployee(employeeCode, _mapper.Map<EmployeeInfo>(employee));
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred while updating the employee");
                return false;
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("{employeeCode}")]
        public async Task<bool> Delete(string employeeCode)
        {
            try
            {
                _logger.Information("Delete method called for DeleteEmployee");
                return await _employeeService.DeleteEmployee(employeeCode);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred while Deleting the employee");
                return false;
            }
        }
    }
}