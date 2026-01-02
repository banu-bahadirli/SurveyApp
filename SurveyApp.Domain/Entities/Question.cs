using SurveyApp.Core.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities;

public class Question : Entity<int>
{
	public string Text { get; set; } = string.Empty; //soru metni
	public int AnswerTemplateId { get; set; } //Hangi şablon kullanılıyor
	public AnswerTemplate AnswerTemplate { get; set; } = null!;
}