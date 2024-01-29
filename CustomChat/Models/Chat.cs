namespace CustomChat.Models;

public class Chat
{
	public Chat()
	{
		Messages = new List<Message>();
		Users = new List<AppUser>();
	}

	public int Id { get; set; }
	public string Name { get; set; }
	public ChatType Type { get; set; }
	public ICollection<Message> Messages { get; set; }
	public ICollection<AppUser> Users { get; set; }
	// prop for users
}


