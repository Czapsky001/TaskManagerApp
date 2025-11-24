using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto.Company;
using TaskManagerApp.Services.Companies;

namespace TaskManagerApp.Controllers
{
	/// <summary>
	/// Provides operations for managing companies, including creation,
	/// update, deletion, and retrieval of company data.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class CompanyController : ControllerBase
	{
		private readonly ILogger<CompanyController> _logger;
		private readonly ICompanyService _companyService;

		/// <summary>
		/// Initializes a new instance of the <see cref="CompanyController"/> class.
		/// </summary>
		/// <param name="logger">Logger instance.</param>
		/// <param name="companyService">Service used for company-related operations.</param>
		public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService)
		{
			_logger = logger;
			_companyService = companyService;
		}

		/// <summary>
		/// Retrieves all companies.
		/// </summary>
		/// <returns>A collection of <see cref="Company"/> objects.</returns>
		/// <remarks>Only users with the Admin role can access this endpoint.</remarks>
		[HttpGet("GetAllCompanies")]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<IEnumerable<Company>>> GetAllCompany()
		{
			var result = await _companyService.GetAllCompanies();
			return Ok(result);
		}

		/// <summary>
		/// Retrieves a specific company by its identifier.
		/// </summary>
		/// <param name="id">The company identifier.</param>
		/// <returns>A <see cref="Company"/> object if found.</returns>
		[HttpGet("GetCompanyById/{id:int}")]
		public async Task<ActionResult<Company>> GetCompanyById([FromRoute] int id)
		{
			var result = await _companyService.GetCompanyByIdAsync(id);
			return Ok(result);
		}

		/// <summary>
		/// Creates a new company.
		/// </summary>
		/// <param name="createCompanyDTO">The company creation data.</param>
		/// <returns>The created company with a reference to its location.</returns>
		[HttpPost("CreateCompany")]
		public async Task<ActionResult<bool>> CreateCompany([FromBody] CreateCompanyDTO createCompanyDTO)
		{
			var createdCompany = await _companyService.CreateCompanyAsync(createCompanyDTO);
			return CreatedAtAction(
				nameof(GetCompanyById),
				new { id = createdCompany.Id },
				createdCompany);
		}

		/// <summary>
		/// Deletes a company by its identifier.
		/// </summary>
		/// <param name="id">The company identifier.</param>
		/// <returns><c>true</c> if deleted successfully; otherwise <c>false</c>.</returns>
		[HttpDelete("DeleteCompany/{id:int}")]
		public async Task<ActionResult<bool>> DeleteCompany([FromRoute] int id)
		{
			var result = await _companyService.DeleteCompanyAsync(id);
			return Ok(result);
		}

		/// <summary>
		/// Updates an existing company.
		/// </summary>
		/// <param name="id">The identifier of the company to update.</param>
		/// <param name="updateCompanyDTO">The updated company data.</param>
		/// <returns><c>true</c> if the update was successful; otherwise <c>false</c>.</returns>
		[HttpPut("UpdateCompany/{id:int}")]
		public async Task<ActionResult<bool>> UpdateCompany(
			[FromRoute] int id,
			[FromBody] UpdateCompanyDTO updateCompanyDTO)
		{
			var result = await _companyService.UpdateCompanyAsync(id, updateCompanyDTO);
			return Ok(result);
		}
	}
}
