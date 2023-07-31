using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyInfo>> GetAllCompanies();
        Task<CompanyInfo> GetCompanyByCode(string companyCode);
        Task<bool> CreateCompany(CompanyInfo company);
        Task<bool> UpdateCompany(string companyCode, CompanyInfo company);
        Task<bool> DeleteCompany(string companyCode);
    }
}
