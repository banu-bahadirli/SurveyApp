using SurveyApp.Core.Exceptions.Types;

public abstract class ExceptionHandler
{
    public Task HandleExceptionAsync(Exception exception)
    {

        if (exception is ValidationException validationException)
            return HandleException(validationException);

        return HandleException(exception);
    }
    protected abstract Task HandleException(ValidationException validationException);
    protected abstract Task HandleException(Exception exception);
} 