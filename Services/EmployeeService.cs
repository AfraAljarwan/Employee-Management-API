using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Employee>> GetAllAsync()
        => _repository.GetAllAsync();

    public Task<Employee?> GetByIdAsync(int id)
        => _repository.GetByIdAsync(id);

    public Task<Employee> AddAsync(Employee employee)
        => _repository.AddAsync(employee);

    public Task UpdateAsync(Employee employee)
        => _repository.UpdateAsync(employee);

    public Task DeleteAsync(int id)
        => _repository.DeleteAsync(id);
}