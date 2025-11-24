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
            await _dbContext.Companies.AddAsync(company);
            await _dbContext.SaveChangesAsync();
            return true;
    }
    public async Task<bool> DeleteCompanyAsync(Company company)
    {
            _dbContext.Companies.Remove(company);
            await _dbContext.SaveChangesAsync();
            return true;
    }

    public async Task<IEnumerable<Company>> GetAllCompanies()
    {
            return await _dbContext.Companies.Include(e => e.WorkTables).Include(c => c.Employees).ToListAsync();
    }

    public async Task<Company> GetCompanyByIdAsync(int id)
    {
            return await _dbContext.Companies
                                   .Include(e => e.Employees)
                                   .Include(w => w.WorkTables)
                                   .FirstOrDefaultAsync(c => c.Id == id);
    }


    public async Task<bool> UpdateCompanyAsync(Company company)
    {
            _dbContext.Companies.Update(company);
            await _dbContext.SaveChangesAsync();
            return true;
    }
}
