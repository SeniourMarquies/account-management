using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nop.gg.Entities
{
	[Table("Users")]
	public class User
	{
		[Key]
		public Guid Id { get; set; }
		// A property representing the unique identifier for the user.

		[StringLength(50)]
		public string? FullName { get; set; }
		// A property representing the full name of the user, limited to a maximum of 50 characters.

		[Required]
		[StringLength(30)]
		public string Username { get; set; }
		// A property representing the username of the user, limited to a maximum of 30 characters. It is required.

		[Required]
		[StringLength(100)]
		public string Password { get; set; }
		// A property representing the password of the user, limited to a maximum of 100 characters. It is required.

		public bool Locked { get; set; } = false;
		// A property representing whether the user account is locked. It defaults to "false."

		public DateTime CreatedAt { get; set; } = DateTime.Now;
		// A property representing the date and time when the user account was created. It defaults to the current date and time.

		[Required]
		[StringLength(50)]
		public string Role { get; set; } = "user";
		// A property representing the role of the user. It is limited to a maximum of 50 characters and defaults to "user."

		// Additional properties and methods for the User class can be defined here as needed.
	}
}
