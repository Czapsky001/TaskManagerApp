using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto.Company;

namespace TaskManagerApp.Services.Companies;

public interface ICompanyService
{
    Task<CompanyDTO> CreateCompanyAsync(CreateCompanyDTO createCompanyDTO);
    Task<bool> UpdateCompanyAsync(int id, UpdateCompanyDTO updateCompanyDTO);
    Task<bool> DeleteCompanyAsync(int id);
    Task<Company> GetCompanyByIdAsync(int id);
    Task<IEnumerable<CompanyDTO>> GetAllCompanies();
}
