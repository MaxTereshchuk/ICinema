# Repositories

## Призначення
В цій папці зберігаються всі репозиторії. Репозиторії - це класи, які працюють безпосередньо з DbContext для додавання, видалення, оновлення та отримання даних з бази даних.

### Приклад Коду

```csharp
public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAll()
    {
        return await _context.Employees
            .Include(e => e.Library)
            .Include(e => e.Position)
            .ToListAsync();
    }
    public bool AddEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        return Save();
    }

    public bool DeleteEmployee(Employee employee)
    {
        _context.Employees.Remove(employee);
        return Save();
    }

    
}