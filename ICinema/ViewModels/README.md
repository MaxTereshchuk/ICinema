# ViewModels

## Призначення
В цій папці зберігаються всі моделі, які працюють з формами.

### Приклад

```csharp
public class CreateEmployeeViewModel
{
    public int Id { get; set; }

    [Required]
    [MaxLength(16, ErrorMessage = "Passport length should not exceed 16 characters.")]
    public string Passport { get; set; }

    [Required]
    [RegularExpression(@"^[^!?]*$", ErrorMessage = "Name should not contain '!' or '?'.")]
    public string Name { get; set; }
}

Після заповнення форми, ця модель перевіряється на валідацію у контролері, тобто чи виповненні всі умови полів. Там же створюється новий екземпляр звичайної моделі з цими даними, яка в свою чергу передається до репозиторію, який вже додає її до контексту.