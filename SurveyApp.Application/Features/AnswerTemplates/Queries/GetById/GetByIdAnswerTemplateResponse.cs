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
	public string Name { get; set; }  // Şablon adı
	public int OptionCount { get; set; }  // 2-4 arası
	public List<AnswerOptionDto> Options { get; set; }
}
