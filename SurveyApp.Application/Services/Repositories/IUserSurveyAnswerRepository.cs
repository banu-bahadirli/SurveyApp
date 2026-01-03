using SurveyApp.Core.Persistance.Repositories;
using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.Repositories
{
	public interface IUserSurveyAnswerRepository : IAsyncRepository<UserSurveyAnswer, int>
	{
	}
}
