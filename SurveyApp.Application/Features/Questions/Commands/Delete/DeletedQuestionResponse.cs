

namespace SurveyApp.Application.Features.Questions.Commands.Delete
{
	public class DeletedQuestionResponse
	{
		public int Id { get; set; }
		public string Message { get; set; } = default!;
		public bool Success { get; set; }
	}
}
