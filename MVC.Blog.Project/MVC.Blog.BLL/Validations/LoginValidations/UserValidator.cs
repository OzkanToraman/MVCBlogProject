using FluentValidation;
using MVC.Blog.DAL;
using MVC.Blog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MVC.Blog.BLL.Validations
{
    public class UserValidator:AbstractValidator<Kullanici>
    {

        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş geçilemez");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad alanı boş geçilemez");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta alanı boş geçilemez");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Hatalı giriş yaptınız");
            RuleFor(x => x.Email).MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola boş geçilemez");
            RuleFor(x => x.Password).Length(8, 12).WithMessage("Parolanız en az 8,en fazla 12 karakterden oluşmalıdır.");
            RuleFor(x => x.PasswordConfirm).NotEmpty().WithMessage("Bu alan boş geçilemez");
            RuleFor(x => x.PasswordConfirm).Equal(x => x.Password).WithMessage("Parolalar uyuşmuyor");
        }
    }
}
