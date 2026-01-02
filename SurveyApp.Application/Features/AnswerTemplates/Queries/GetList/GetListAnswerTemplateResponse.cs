using SurveyApp.Application.Features.AnswerTemplates.Dtos;
using System.Collections.Generic;

namespace SurveyApp.Application.Features.AnswerTemplates.Queries.GetList
{
	public class GetListAnswerTemplateResponse
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public List<AnswerOptionDto> Options { get; set; } = new List<AnswerOptionDto>();
	}
}
