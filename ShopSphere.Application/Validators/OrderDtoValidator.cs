using FluentValidation;
using ShopSphere.Application.DTOs;

namespace ShopSphere.Application.Validators
{
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("اسم العميل مطلوب للشحن")
                .MinimumLength(3).WithMessage("الاسم لازم يكون 3 حروف على الأقل");

            RuleFor(x => x.ShippingAddress)
                .NotEmpty().WithMessage("العنوان بالكامل مطلوب للتوصيل");

            RuleFor(x => x.CustomerPhone)
                .NotEmpty().WithMessage("رقم التليفون مطلوب للتواصل");
        }
    }
}
