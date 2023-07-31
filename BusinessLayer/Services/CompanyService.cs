using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CompanyInfo>> GetAllCompanies()
        {
            var res = await _companyRepository.GetAll();
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }

        public async Task<CompanyInfo> GetCompanyByCode(string companyCode)
        {
            var result = await _companyRepository.GetByCode(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }

        public async Task<bool> CreateCompany(CompanyInfo company)
        {
            var obj = _mapper.Map<Company>(company);
            var result = await _companyRepository.SaveCompanyAsync(obj);
            return result;
        }

        public async Task<bool> UpdateCompany(string companyCode, CompanyInfo company)
        {
            company.CompanyCode = companyCode;
            var result = await _companyRepository.UpdateCompanyAsync(companyCode, _mapper.Map<Company>(company));
            return result;
        }

        public async Task<bool> DeleteCompany(string companyCode)
        {
            return await _companyRepository.DeleteCompanyAsync(companyCode);
        }
    }
}
