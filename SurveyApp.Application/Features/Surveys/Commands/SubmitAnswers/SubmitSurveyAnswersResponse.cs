using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Features.Surveys.Commands.SubmitAnswers
{
	public class SubmitSurveyAnswersResponse
	{
		public int UserSurveyId { get; set; }
		public bool Success { get; set; }
		public string Message { get; set; } // Kaç soru cevaplandı
	}

}
