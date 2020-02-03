using FluentValidation;

namespace Application.Validators
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var opts = ruleBuilder.NotEmpty()
                .MinimumLength(6).WithMessage("Password must be at least 6 characters")
                .Matches("[A-Z]").WithMessage("Password must contain one uppercase")
                .Matches("[a-z]").WithMessage("Password must contain one lowercase")
                .Matches("[0-9]").WithMessage("Password must contain one number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain one symbol")
                ;

            return opts;
        }
    }
}
