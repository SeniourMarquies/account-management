namespace nop.gg.Models
{
	public class ErrorViewModel
	{
		public string? RequestId { get; set; }
		// A property that holds a request identifier, which can be used to track and identify specific requests.

		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
		// A computed property that returns true if the RequestId is not empty, enabling conditional rendering in views.
	}
}
