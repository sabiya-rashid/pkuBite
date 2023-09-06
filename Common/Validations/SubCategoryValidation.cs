using System;
using FluentValidation;
using pkuBite.Common.DTO;

namespace Common.Validations
{
	public class SubCategoryValidation : AbstractValidator<subCategoryDTO>
	{
		public SubCategoryValidation()
		{
			RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("Name can't be empty");
			RuleFor(c => c.CategoryId).NotEmpty().NotNull().WithMessage("Category Id can't be null");
		}
	}
}

