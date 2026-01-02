using SurveyApp.Core.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities;

public class AnswerTemplate : Entity<int>
{
	public string Name { get; set; } = string.Empty; //Şablon adı
	public int OptionCount { get; set; } //2-4 arası
	public ICollection<AnswerOption> Options { get; set; } = new List<AnswerOption>();
}