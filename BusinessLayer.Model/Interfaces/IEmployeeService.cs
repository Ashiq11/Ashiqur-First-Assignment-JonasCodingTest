using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeInfo>> GetAllEmployees();
        Task<EmployeeInfo> GetEmployeeByCode(string employeeCode);
        Task<bool> CreateEmployee(EmployeeInfo employee);
        Task<bool> UpdateEmployee(string employeeCode, EmployeeInfo employee);
        Task<bool> DeleteEmployee(string employeeCode);
    }
}
