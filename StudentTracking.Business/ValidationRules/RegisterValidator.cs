using FluentValidation;
using StudentTracking.Data.EntityFramework.Entities;

namespace StudentTracking.Business.ValidationRules
{
    public class RegisterValidator : AbstractValidator<Customer>
    {
        public RegisterValidator()
        {
            RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre kısmı boş geçilemez.");
            RuleFor(p => p.Password).Matches(@"[A-Z]+").WithMessage("Şifre en az bir büyük harf içermelidir.");
            RuleFor(p => p.Password).Matches(@"[a-z]+").WithMessage("Şifre en az bir küçük harf içermelidir.");
            RuleFor(p => p.Password).Matches(@"[0-9]+").WithMessage("Şifre en az bir rakam içermelidir.");
            RuleFor(p => p.Password).MinimumLength(8).WithMessage("Şifre uzunluğu en az 8 karakter olmalıdır.");
        }
    }
}
