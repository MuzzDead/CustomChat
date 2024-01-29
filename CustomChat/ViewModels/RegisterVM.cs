using System.ComponentModel.DataAnnotations;

namespace CustomChat.ViewModels;

public class RegisterVM
{
	[Required]
	public string? Name { get; set; }

	[Required]
	[DataType(DataType.EmailAddress)]
	public string? Email { get; set; }

	[Required]
	[DataType(DataType.Password)]
	public string? Password { get; set; }

	[Compare("Password", ErrorMessage = "Password don`t match!")]
	[Display(Name = "Confirm Password")]
	[DataType(DataType.Password)]
	public string? ConfirmPassword { get; set; }
}
