using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Queries.GetUserActiveSurveys;

public class GetUserActiveSurveyResponse
{
	public int SurveyId { get; set; }
	public string Title { get; set; } = null!;
	public string? Description { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
}



