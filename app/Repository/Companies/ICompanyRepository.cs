using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto.Company;

namespace TaskManagerApp.Repository.Companies;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllCompaniesAsync();
    Task<Company> GetByIdAsync(int id);
    Task<bool> CreateCompanyAsync(Company company);
    Task<bool> UpdateCompanyAsync(Company company);
    Task<bool> DeleteCompanyAsync(Company company);

}
