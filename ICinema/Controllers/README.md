# Controllers

## Призначення
В цій папці зберігаються всі контролери, але логіка роботи з контекстом винесена до Repository.

### Приклад Коду
```csharp
public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeeRepository)
    {
        _employeeRepository = employeeeRepository;
    }

    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        IEnumerable<Employee> employees = await _employeeRepository.GetAll();
        return View(employees);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(CreateEmployeeViewModel employeeVM)
    {
        if (ModelState.IsValid)
        {
            Employee employee = new Employee
            {
                Passport = employeeVM.Passport,
                FullName = employeeVM.FullName,
                // Інші поля...
            };

            _employeeRepository.AddEmployee(employee);
            return RedirectToAction("Index");
        }
        
        return View(employeeVM);
        
        
    }
}