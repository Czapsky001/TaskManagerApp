using TaskManagerApp.Model;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<Company>> GetAllCompanies()
    {
        try
        {
            return await _dbContext.Companies.Include(e => e.WorkTables).Include(c => c.Employees).ToListAsync();
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return new List<Company>();
        }
    }

    public async Task<Company> GetCompanyByIdAsync(int id)
    {
        try
        {
            // FindAsync jest idealny, gdy szukamy po ID (klucz główny)
            return await _dbContext.Companies
                                   .Include(e => e.Employees)
                                   .Include(w => w.WorkTables)
                                   .FirstOrDefaultAsync(c => c.Id == id); // zamiast FindAsync, używamy FirstOrDefaultAsync z Include
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return null;
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
