using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Users.Queries.GetList
{
	public class GetListUserResponse
	{
		public int Id { get; set; }
		public string FirstName { get; set; }	
		public string LastName { get; set; }
	}
}
