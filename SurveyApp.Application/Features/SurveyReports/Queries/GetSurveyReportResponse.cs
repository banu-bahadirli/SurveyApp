using System.Collections.Generic;
using SurveyApp.Application.Features.SurveyReports.Dtos;

namespace SurveyApp.Application.Features.SurveyReports.Queries.GetSurveyReport
{
	public class GetSurveyReportResponse
	{
		public int SurveyId { get; set; }
		public List<UserSurveyReportDto> Users { get; set; } = new();
	}
}
