using System.Collections.Generic;

namespace SurveyApp.Application.Features.SurveyReports.Dtos
{
	public class UserSurveyReportDto
	{
		public int UserId { get; set; }
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public bool IsCompleted { get; set; }
	}

}
