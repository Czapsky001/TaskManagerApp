using AutoMapper;
using System.Threading.Tasks;
using TaskManagerApp.Model;
using TaskManagerApp.Model.Dto.Company;
using TaskManagerApp.Model.Dto.Tasks;
using TaskManagerApp.Repository.Companies;

namespace TaskManagerApp.Services.Companies;

public class CompanyService : ICompanyService
{
    private readonly ILogger<CompanyService> _logger;
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;
    public CompanyService(ILogger<CompanyService> logger, ICompanyRepository companyRepository, IMapper mapper)
    {
        _logger = logger;
        _companyRepository = companyRepository;
        _mapper = mapper;
    }
    public async Task<CompanyDTO> CreateCompanyAsync(CreateCompanyDTO createCompanyDTO)
    {
        try
        {
            var companyToAdd = _mapper.Map<Company>(createCompanyDTO);
            var result = await _companyRepository.CreateCompanyAsync(companyToAdd);
            return _mapper.Map<CompanyDTO>(companyToAdd);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return null;
        }
    }

    public async Task<bool> DeleteCompanyAsync(int id)
    {
        try
        {
            var companyToDelete = await _companyRepository.GetCompanyByIdAsync(id);
            if(companyToDelete == null)
            {
                _logger.LogInformation($"Company with id - {id} does not exist");
                return false;
            }
            var result = await _companyRepository.DeleteCompanyAsync(companyToDelete);
            return result;
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }

    public async Task<IEnumerable<CompanyDTO>> GetAllCompanies()
    {
        try
        {
            var companies = await _companyRepository.GetAllCompanies();
            return _mapper.Map<IEnumerable<CompanyDTO>>(companies);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return new List<CompanyDTO>();
        }
    }

    public async Task<Company> GetCompanyByIdAsync(int id)
    {
        try
        {
            var result = await _companyRepository.GetCompanyByIdAsync(id);
            return result;
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return null;
        }
    }

    public async Task<bool> UpdateCompanyAsync(int id, UpdateCompanyDTO updateCompanyDTO)
    {
        try
        {
            var companyFromRepo = await _companyRepository.GetCompanyByIdAsync(id);
            if(companyFromRepo == null)
            {
                _logger.LogInformation($"Company with id - {id} does not exist");
                return false;
            }
            _mapper.Map(updateCompanyDTO, companyFromRepo);
            return await _companyRepository.UpdateCompanyAsync(companyFromRepo);
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }
    }
}
