using System.ComponentModel.DataAnnotations;

namespace nop.gg.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Username is required.")]
		// Specifies that the "Username" field is required, and if not provided, the error message "Username is required." will be displayed.

		[StringLength(30, ErrorMessage = "Username can be max 30 chars")]
		// Specifies that the "Username" field must have a maximum length of 30 characters, and if it exceeds this limit, the error message "Username can be max 30 chars" will be displayed.
		public string Username { get; set; }
		// A property that represents the username entered during login.

		[Required(ErrorMessage = "Password is required")]
		// Specifies that the "Password" field is required, and if not provided, the error message "Password is required" will be displayed.

		[MinLength(6, ErrorMessage = "Password can be min 6 chars")]
		// Specifies that the "Password" field must have a minimum length of 6 characters, and if it is shorter than this, the error message "Password can be min 6 chars" will be displayed.

		[MaxLength(16, ErrorMessage = "Password can be max 16 chars")]
		// Specifies that the "Password" field must have a maximum length of 16 characters, and if it exceeds this limit, the error message "Password can be max 16 chars" will be displayed.
		public string Password { get; set; }
		// A property that represents the password entered during login.
	}
}
