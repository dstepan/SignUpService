using FluentValidation;

namespace SignUpService.Validation;

public class ValidationResultMessage
{
    public string Message { get; set; }
    public string[] Errors { get; set; }
}

public static class ValidationExceptionExtensions
{
    public static ValidationResultMessage ResultMessage(this ValidationException exception)
    {
        return new ValidationResultMessage
        {
            Message = exception.Message,
            Errors = exception.Errors.Select(e => e.ErrorMessage).ToArray()
        };
    }
}