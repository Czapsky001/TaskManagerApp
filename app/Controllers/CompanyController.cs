using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto.Company;
using TaskManagerApp.Services.Companies;

namespace TaskManagerApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ILogger<CompanyController> _logger;
    private readonly ICompanyService _companyService;
    public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService)
    {
        _logger = logger;
        _companyService = companyService;
    }
    [HttpGet("GetAllCompanies"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<Company>>> GetAllCompany()
    {
        try
        {
            var result = await _companyService.GetAllCompanies();
            return Ok(result);
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest();
        }
    }
    [HttpGet("GetCompanyById/{id}")]
    public async Task<ActionResult<Company>> GetCompanyById(int id)
    {
        try
        {
            var result = await _companyService.GetCompanyByIdAsync(id);
            return Ok(result);
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest();
        }
    }
    [HttpPost("CreateCompany")]
    public async Task<ActionResult<bool>> CreateCompany(CreateCompanyDTO createCompanyDTO)
    {
        try
        {
            var createdCompany = await _companyService.CreateCompanyAsync(createCompanyDTO);
            return CreatedAtAction(nameof(GetCompanyById), new {id = createdCompany.Id}, createdCompany);
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest();
        }
    }

    [HttpDelete("DeleteCompany/{id}")]
    public async Task<ActionResult<bool>> DeleteCompany(int id)
    {
        try
        {
            var result = await _companyService.DeleteCompanyAsync(id);
            return Ok(result);
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest();
        }
    }
    [HttpPut("UpdateCompany/{id}")]
    public async Task<ActionResult<bool>> UpdateCompany(int id, [FromBody]UpdateCompanyDTO updateCompanyDTO)
    {
        try
        {
            var result = await _companyService.UpdateCompanyAsync(id, updateCompanyDTO);
            return Ok(result);
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return BadRequest();
        }
    }
}
