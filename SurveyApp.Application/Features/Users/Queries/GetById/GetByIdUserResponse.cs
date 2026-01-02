using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Users.Queries.GetById;

public class GetByIdUserResponse
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
}