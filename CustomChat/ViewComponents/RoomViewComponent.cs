using CustomChat.Data;
using Microsoft.AspNetCore.Mvc;

namespace CustomChat.ViewComponents;

public class RoomViewComponent : ViewComponent
{
	private AppDbContext _ctx;

	public RoomViewComponent(AppDbContext ctx)
	{
		_ctx = ctx;
	}

	public IViewComponentResult Invoke()
	{
		var chats = _ctx.Chats.ToList(); 
		return View(chats);
	}
}
