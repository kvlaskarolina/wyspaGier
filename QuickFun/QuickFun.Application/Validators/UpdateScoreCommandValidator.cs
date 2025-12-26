using FluentValidation;
using QuickFun.Application.Commands;

namespace QuickFun.Application.Validators;

public class UpdateScoreCommandValidator : AbstractValidator<UpdateScoreCommand>
{
    public UpdateScoreCommandValidator()
    {
        RuleFor(x => x.ScoreId)
            .NotEmpty().WithMessage("Score ID is required");

        RuleFor(x => x.Points)
            .GreaterThanOrEqualTo(0).WithMessage("Points cannot be negative");

        RuleFor(x => x.Duration)
            .GreaterThan(TimeSpan.Zero).WithMessage("Duration must be greater than zero");
    }
}