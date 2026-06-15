using FluentValidation;
using ShopSphere.Application.DTOs;

namespace ShopSphere.Application.Validators
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم التصنيف مطلوب")
                .Length(2, 50).WithMessage("اسم التصنيف بين 2 و 50 حرف");
        }
    }
}
