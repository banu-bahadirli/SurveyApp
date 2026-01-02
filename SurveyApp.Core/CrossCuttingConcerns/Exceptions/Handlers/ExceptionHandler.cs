using SurveyApp.Core.CrossCuttingConcerns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.CrossCuttingConcerns.Exceptions.Handlers
{
	public abstract class ExceptionHandler
	{
		public Task HandleExceptionAsync(Exception exception)
		{
			if (exception is BusinessException businessException)
				return HandleException(businessException);

			return HandleException(exception);
		}

		protected abstract Task HandleException(BusinessException businessException);

		protected abstract Task HandleException(Exception exception);
	}
}
