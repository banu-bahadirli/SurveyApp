using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Core.Exceptions.Types;

public class ValidationException : Exception
{
	public IEnumerable<ValidationExceptionModel> Errors { get; }

	public ValidationException(IEnumerable<ValidationExceptionModel> errors) : base("Validation error(s)")
	{
		Errors = errors;
	}
}

public class ValidationExceptionModel
{
	public string Property { get; set; }
	public IEnumerable<string> Errors { get; set; }

	public ValidationExceptionModel(string property, IEnumerable<string> errors)
	{
		Property = property;
		Errors = errors;
	}
}