using CustomChat.Models;
using CustomChat.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CustomChat.Controllers;

public class Account : Controller
{
	private readonly SignInManager<AppUser> signInManager;
	private readonly UserManager<AppUser> userManager;

	public Account(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
	{
		this.signInManager = signInManager;
		this.userManager = userManager;
	}

	[HttpGet]
	public IActionResult Login()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Login(LoginVM model)
	{
		if(ModelState.IsValid)
		{
			//login
			var result = await signInManager.PasswordSignInAsync(model.Name!, model.Password!, model.RememberMe!, false);
			if(result.Succeeded)
			{
				return RedirectToAction("Chat", "Home");
			}

			ModelState.AddModelError("", "Invalid login attempt");
			return View(model);
		}
		return View(model);
	}

	public IActionResult Register()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Register(RegisterVM model)
	{
		if (ModelState.IsValid)
		{
			AppUser user = new AppUser()
			{
				UserName = model.Name,
				Name = model.Name
			};

			user.Email = model.Email;

			var result = await userManager.CreateAsync(user, model.Password!);
			if (result.Succeeded)
			{
				await signInManager.SignInAsync(user, false);
				return RedirectToAction("Chat", "Home");
			}
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error.Description);
			}
		}
		return View(model);
	}

	public async Task<IActionResult> Logout()
	{
		await signInManager.SignOutAsync();
		return RedirectToAction("Chat", "Home");
	}
}
