using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Questions.Queries.GetList.Dtos
{
	

	public class AnswerTemplateDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public List<AnswerOptionDto> Options { get; set; } = new List<AnswerOptionDto>();
	}

}
