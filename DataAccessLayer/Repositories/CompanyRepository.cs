using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
//using Microsoft.Extensions.Logging;
using Serilog;

namespace DataAccessLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
	    private readonly IDbWrapper<Company> _companyDbWrapper;
        private readonly ILogger _logger;
        public CompanyRepository(IDbWrapper<Company> companyDbWrapper, ILogger logger)
	    {
		    _companyDbWrapper = companyDbWrapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await _companyDbWrapper.FindAllAsync();
        }

        public async Task<Company> GetByCode(string companyCode)
        {
            var list = await _companyDbWrapper.FindAsync(t => t.CompanyCode.Equals(companyCode));

            return list?.FirstOrDefault();
        }

        public async Task<bool> SaveCompanyAsync(Company company)
        {
            var itemRepo = _companyDbWrapper.Find(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode))?.FirstOrDefault();
            if (itemRepo !=null)
            {
                itemRepo.CompanyName = company.CompanyName;
                itemRepo.AddressLine1 = company.AddressLine1;
                itemRepo.AddressLine2 = company.AddressLine2;
                itemRepo.AddressLine3 = company.AddressLine3;
                itemRepo.Country = company.Country;
                itemRepo.EquipmentCompanyCode = company.EquipmentCompanyCode;
                itemRepo.FaxNumber = company.FaxNumber;
                itemRepo.PhoneNumber = company.PhoneNumber;
                itemRepo.PostalZipCode = company.PostalZipCode;
                itemRepo.LastModified = company.LastModified;
                _logger.Information($"Updating an existing company with company code: {company}");
                return await _companyDbWrapper.UpdateAsync(itemRepo);
            }
            _logger.Information($"Inserting a new company with company code: {company}");
            return await _companyDbWrapper.InsertAsync(company);
        }

        public async Task<bool> UpdateCompanyAsync(string companyCode, Company company)
        {
            var companyDB = await GetByCode(companyCode);
            if(companyDB == null)
            {
                throw new System.Exception("Couldn't find company");
            }
            companyDB.CompanyCode = companyCode;
            companyDB.CompanyName = company.CompanyName;
            companyDB.AddressLine1 = company.AddressLine1;
            companyDB.AddressLine2 = company.AddressLine2;
            companyDB.AddressLine3 = company.AddressLine3;
            companyDB.Country = company.Country;
            companyDB.EquipmentCompanyCode = company.EquipmentCompanyCode;
            companyDB.FaxNumber = company.FaxNumber;
            companyDB.PhoneNumber = company.PhoneNumber;
            companyDB.PostalZipCode = company.PostalZipCode;
            companyDB.LastModified = company.LastModified;

            _logger.Information($"Updating an existing company with company code: {companyCode}");
            return await _companyDbWrapper.UpdateAsync(companyDB);
        }

        public async Task<bool> DeleteCompanyAsync(string companyCode)
        {
            _logger.Information($"Deleting an existing company with company code: {companyCode}");
            return await _companyDbWrapper.DeleteAsync(x => x.CompanyCode == companyCode);
        }
    }
}
