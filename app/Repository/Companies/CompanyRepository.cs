using System.Data.Entity;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto.Company;

namespace TaskManagerApp.Repository.Companies;

public class CompanyRepository : ICompanyRepository
{
    private readonly ILogger<CompanyRepository> _logger;
    private readonly DatabaseContext _dbContext;

    public CompanyRepository(ILogger<CompanyRepository> logger, DatabaseContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    public async Task<bool> CreateCompanyAsync(Company company)
    {
        try
        {
            await _dbContext.Companies.AddAsync(company);
            await _dbContext.SaveChangesAsync();
            return true;
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }
    public async Task<bool> DeleteCompanyAsync(Company company)
    {
        try
        {
            _dbContext.Companies.Remove(company);
            await _dbContext.SaveChangesAsync();
            return true;
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }

    public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
    {
        try
        {
            return await _dbContext.Companies.ToListAsync();
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return new List<Company>();
        }
    }

    public async Task<Company> GetByIdAsync(int id)
    {
        try
        {
            return await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw;
        }
    }

    public async Task<bool> UpdateCompanyAsync(Company company)
    {
        try
        {
            _dbContext.Companies.Update(company);
            await _dbContext.SaveChangesAsync();
            return true;
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }
}
