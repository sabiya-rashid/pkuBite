using FluentValidation;
using pkuBite.Common.DTO;

namespace Common.Validations
{
	public class LoginValidation : AbstractValidator<LoginDTO>
	{
		public LoginValidation()
		{
			RuleFor(c => c.Email).EmailAddress().WithMessage("Enter a Valid email Address");
			RuleFor(c => c.Password).NotNull().NotEmpty().WithMessage("Password is required");
		}
	}
}

