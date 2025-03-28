using FluentValidation.Results;

namespace DataDrivenApi.Exceptions;

public class ValidationException : Exception
{
    public ValidationException() : base("Validation errors occurred")
    {
        Errors = new List<string>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .Select(e => $"{e.PropertyName}: {e.ErrorMessage}")
            .ToList();
    }

    public List<string> Errors { get; }
}