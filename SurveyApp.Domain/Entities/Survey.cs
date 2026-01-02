using SurveyApp.Core.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities;

public class Survey : Entity<int>
{
	public string Title { get; set; } = null!;
	public string? Description { get; set; } 
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public bool IsActive { get; set; }
	public  ICollection<SurveyQuestion> SurveyQuestions { get; set; } = new List<SurveyQuestion>();
	public  ICollection<UserSurvey> UserSurveys { get; set; } = new List<UserSurvey>();
}