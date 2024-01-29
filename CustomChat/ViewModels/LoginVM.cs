using System.ComponentModel.DataAnnotations;

namespace CustomChat.ViewModels;

public class LoginVM
{
	[Required(ErrorMessage = "User name is required.")]
	public string? Name { get; set; }

	[Required(ErrorMessage = "Password name is required.")]
	[DataType(DataType.Password)]
	public string? Password { get; set; }

	[Display(Name = "Remember Me")]
	public bool RememberMe { get; set; }
}
