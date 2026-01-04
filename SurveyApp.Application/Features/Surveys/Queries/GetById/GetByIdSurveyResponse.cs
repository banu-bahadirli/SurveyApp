using System;
using System.Collections.Generic;

namespace SurveyApp.Application.Features.Surveys.Queries.GetById;

public class GetByIdSurveyResponse
{
	public int Id { get; set; }
	public string Title { get; set; } = null!;
	public string? Description { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public bool IsActive { get; set; }
	public List<int> UserIds { get; set; } = new();
	public List<int> QuestionIds { get; set; } = new();
}
