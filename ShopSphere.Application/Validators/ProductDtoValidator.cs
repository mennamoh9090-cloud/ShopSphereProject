using FluentValidation;
using ShopSphere.Application.DTOs;

namespace ShopSphere.Application.Validators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {


            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم المنتج مطلوب ")
                .Length(3, 100).WithMessage("الاسم لازم يكون بين 3 و 100 حرف");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("السعر لازم يكون أكبر من صفر");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("التصنيف مطلوب");


        }
    }
}
