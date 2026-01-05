using SurveyApp.Application.Features.AnswerTemplates.Dtos;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.AnswerTemplates.Queries.GetById;

public class GetByIdAnswerTemplateResponse
{
	public string Name { get; set; }  
	public int OptionCount { get; set; }  
	public List<AnswerOptionDto> Options { get; set; }
}
