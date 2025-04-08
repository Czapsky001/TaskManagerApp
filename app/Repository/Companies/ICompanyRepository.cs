using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto.Company;

namespace TaskManagerApp.Repository.Companies;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllCompanies();
    Task<Company> GetCompanyByIdAsync(int id);
    Task<bool> CreateCompanyAsync(Company company);
    Task<bool> UpdateCompanyAsync(Company company);
    Task<bool> DeleteCompanyAsync(Company company);

}
