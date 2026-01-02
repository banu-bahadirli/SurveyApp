using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Queries.GetById;

	public class GetByIdSurveyResponse
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public DateTime StartDate { get; set; } 
	public DateTime EndDate { get; set; } 

	public bool IsActive { get; set; }
}