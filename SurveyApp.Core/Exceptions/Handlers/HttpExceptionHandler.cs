

using Microsoft.AspNetCore.Http;
using SurveyApp.Core.Exceptions.HttpProblemDetails;
using SurveyApp.Core.Exceptions.Types;
using System.Text.Json;

public class HttpExceptionHandler : ExceptionHandler
{
    private HttpResponse? _response;

    public HttpResponse Response
    {
        get => _response ?? throw new ArgumentNullException(nameof(_response));
        set => _response = value;
    }

    protected override Task HandleException(ValidationException validationException)
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        var details = new ValidationProblemDetails(validationException.Errors);
        return WriteAsJsonAsync(Response, details);
    }

	protected override Task HandleException(Exception exception)
	{
		Response.StatusCode = StatusCodes.Status500InternalServerError;
		var details = new InternalServerErrorProblemDetails(exception.Message);
		return WriteAsJsonAsync(Response, details);
	}
	private static Task WriteAsJsonAsync<T>(HttpResponse response, T value)
    {
        response.ContentType = "application/json";
        return JsonSerializer.SerializeAsync(response.Body, value);
    }
}
