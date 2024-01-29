using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomChat.Models;

public class AppUser : IdentityUser
{
	[StringLength(100)]
	[MaxLength(100)]
	[Required]
	public string? Name { get; set; }
}
