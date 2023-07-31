using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployees()
        {
            var res = await _employeeRepository.GetAllEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
        }

        public async Task<EmployeeInfo> GetEmployeeByCode(string employeeCode)
        {
            var result = await _employeeRepository.GetEmployeeByCodeAsync(employeeCode);
            return _mapper.Map<EmployeeInfo>(result);
        }

        public async Task<bool> CreateEmployee(EmployeeInfo employee)
        {
            var obj = _mapper.Map<Employee>(employee);
            var result = await _employeeRepository.SaveEmployeeAsync(obj);
            return result;
        }

        public async Task<bool> UpdateEmployee(string employeeCode, EmployeeInfo employee)
        {
            employee.EmployeeCode = employeeCode;
            var result = await _employeeRepository.UpdateEmployeeAsync(employeeCode, _mapper.Map<Employee>(employee));
            return result;
        }

        public async Task<bool> DeleteEmployee(string employeeCode)
        {
            return await _employeeRepository.DeleteEmployeeAsync(employeeCode);
        }
    }
}
