using SurveyApp.Core.Persistance.Repositories;
using SurveyApp.Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities;

public class UserSurvey : Entity<int>
{
	public int UserId { get; set; }
	public User? User { get; set; }

	public int SurveyId { get; set; }
	public Survey? Survey { get; set; }

	public bool IsCompleted { get; set; }
}