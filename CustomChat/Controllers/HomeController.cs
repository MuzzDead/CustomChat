using CustomChat.Data;
using CustomChat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CustomChat.Controllers;

//[Authorize]
public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private AppDbContext _ctx;

	public HomeController(ILogger<HomeController> logger, AppDbContext ctx)
	{
		_logger = logger;
		_ctx = ctx;
	}


	public IActionResult Index()
	{
		return View();
	}

	[HttpGet("{id}")]
	public IActionResult Chat(int id)
	{
		var chat = _ctx.Chats
			.Include(x=>x.Messages)
			.FirstOrDefault(c => c.Id == id);
		return View(chat);
	}


	[HttpPost]
	public async Task<IActionResult> CreateMessage(int ChatId, string message)
	{
		var Message = new Message
		{
			ChatId = ChatId,
			Text = message,
			Name = "Default",
			Timestamp = DateTime.Now
		};

		_ctx.Messages.Add(Message);
		await _ctx.SaveChangesAsync();
		return RedirectToAction("ChatMes", new {id = ChatId});
	}

	[HttpPost]
	public async Task<IActionResult> CreateRoom(string name)
	{
		_ctx.Chats.Add(new Chat
		{
			Name = name,
			Type = ChatType.Room
		});

		await _ctx.SaveChangesAsync();
		return RedirectToAction("Chat");
	}

	[Authorize]
	public IActionResult Chat()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}