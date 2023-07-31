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
    [RoutePrefix("api/Company")]
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public CompanyController(ICompanyService companyService, IMapper mapper, ILogger logger)
        {
            _companyService = companyService;
            _mapper = mapper;
            _logger = logger;
        }
        // GET api/<controller>
        [HttpGet]
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            try
            {
                _logger.Information("Called method for GetAllCompanies");
                var items = await _companyService.GetAllCompanies();
                return _mapper.Map<IEnumerable<CompanyDto>>(items);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error occurred while getting all companys.");
                return Enumerable.Empty<CompanyDto>(); // Return an empty list in case of an error            }
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("{companyCode}")]
        public async Task<CompanyDto> Get(string companyCode)
        {
            try
            {
                _logger.Information("Called method for GetCompanyByCompanyCode");
                var item = await _companyService.GetCompanyByCode(companyCode);
                if (item == null)
                {
                    _logger.Warning("company with the given code is not found.");
                }

                return _mapper.Map<CompanyDto>(item);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error occurred while getting company with code: {companyCode}");
                return new CompanyDto(); // Return a default CompanyDto or null if an error occurs.
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<bool> Post([FromBody]CompanyDto company)
        {
            try
            {
                _logger.Information("POST method called for CreateCompany");
                var companyInfo = _mapper.Map<CompanyInfo>(company);
                var item = await _companyService.CreateCompany(companyInfo);
                return item;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while saving company information.");
                _logger.Information($"Post called with company body {JsonConvert.SerializeObject(company)}");
                return false;
            }
            
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("{companyCode}")]
        public async Task<bool> Put(string companyCode, [FromBody] CompanyDto company)
        {
            try
            {
                _logger.Information("PUT method called for UpdateCompany");
                var result = await _companyService.UpdateCompany(companyCode, _mapper.Map<CompanyInfo>(company));
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred while updating the company");
                return false;
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("{companyCode}")]
        public async Task<bool> Delete(string companyCode)
        {
            try
            {
                _logger.Information("Delete method called for DeleteCompany");
                return await _companyService.DeleteCompany(companyCode);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred while Deleting the company");
                return false;
            }
        }
    }
}