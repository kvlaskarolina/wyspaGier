using FluentValidation;
using QuickFun.Application.Commands;

namespace QuickFun.Application.Validators;

public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Game name is required")
            .MaximumLength(100).WithMessage("Game name must not exceed 100 characters");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

        RuleFor(x => x.MinPlayers)
            .GreaterThan(0).WithMessage("Minimum players must be greater than 0")
            .LessThanOrEqualTo(x => x.MaxPlayers).WithMessage("Minimum players cannot exceed maximum players");

        RuleFor(x => x.MaxPlayers)
            .GreaterThan(0).WithMessage("Maximum players must be greater than 0")
            .LessThanOrEqualTo(100).WithMessage("Maximum players cannot exceed 100");
    }
}