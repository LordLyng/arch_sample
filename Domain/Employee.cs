namespace Domain;

public class Employee
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateOnly BirthDate { get; set; }
    public int Age => DateTime.Now.Year - BirthDate.Year;
}
