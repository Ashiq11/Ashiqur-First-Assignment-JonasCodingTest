using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
//using Microsoft.Extensions.Logging;
using Serilog;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
	    private readonly IDbWrapper<Employee> _employeeDbWrapper;
        private readonly ILogger _logger;
        public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper, ILogger logger)
	    {
            _employeeDbWrapper = employeeDbWrapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                return await _employeeDbWrapper.FindAllAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while fetching all employees.");
                throw;
            }
        }

        public async Task<Employee> GetEmployeeByCodeAsync(string employeeCode)
        {
            try
            {
                var list = await _employeeDbWrapper.FindAsync(e => e.EmployeeCode.Equals(employeeCode));
                return list?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error occurred while fetching employee with code: {employeeCode}");
                throw;
            }
        }

        public async Task<bool> SaveEmployeeAsync(Employee employee)
        {
            try
            {
                var itemRepo = (await _employeeDbWrapper.FindAsync(e =>
                   e.EmployeeCode.Equals(employee.EmployeeCode)).ConfigureAwait(false))?.FirstOrDefault();
                if (itemRepo != null)
                {
                    itemRepo.EmployeeName = employee.EmployeeName;
                    itemRepo.Occupation = employee.Occupation;
                    itemRepo.EmployeeStatus = employee.EmployeeStatus;
                    itemRepo.EmailAddress = employee.EmailAddress;
                    itemRepo.Phone = employee.Phone;
                    itemRepo.LastModified = employee.LastModified;
                    _logger.Information($"Updating an existing employee with employee code: {employee}");
                    return await _employeeDbWrapper.UpdateAsync(itemRepo);
                }
                _logger.Information($"Inserting a new employee with employee code: {employee}");
                return await _employeeDbWrapper.InsertAsync(employee);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while saving employee information.");
                throw;
            }
        }

        public async Task<bool> UpdateEmployeeAsync(string employeeCode, Employee employee)
        {
            try 
            { 
                var employeeDB = await GetEmployeeByCodeAsync(employeeCode);
                if (employeeDB == null)
                {
                    throw new System.Exception("Couldn't find employee");
                }
                employeeDB.EmployeeName = employee.EmployeeName;
                employeeDB.Occupation = employee.Occupation;
                employeeDB.EmployeeStatus = employee.EmployeeStatus;
                employeeDB.EmailAddress = employee.EmailAddress;
                employeeDB.Phone = employee.Phone;
                employeeDB.LastModified = employee.LastModified;
                _logger.Information($"Updating an existing employee with employee code: {employee}");
                return await _employeeDbWrapper.UpdateAsync(employeeDB);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while updating employee information.");
                throw;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(string employeeCode)
        {
            try
            {
                _logger.Information($"Deleting an existing employee with employee code: {employeeCode}");
                return await _employeeDbWrapper.DeleteAsync(x => x.EmployeeCode == employeeCode);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while deleting employee information.");
                throw;
            }
        }
    }
}
