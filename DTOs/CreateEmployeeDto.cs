using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class CreateEmployeeDto
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Salary { get; set; }

    public DateTime HireDate { get; set; }
}