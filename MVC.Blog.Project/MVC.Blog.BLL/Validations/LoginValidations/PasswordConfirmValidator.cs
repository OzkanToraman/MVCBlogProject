using FluentValidation;
using MVC.Blog.DAL;
using MVC.Blog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Blog.BLL.Validations
{
    public class PasswordConfirmValidator:AbstractValidator<Kullanici> 
    {
        public PasswordConfirmValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola boş geçilemez");
            RuleFor(x => x.Password).Matches(@"^[a-zA-Z0-9]*$").WithMessage("Parolanız en az bir büyük harf ve bir sayı içermelidir.");
            RuleFor(x => x.PasswordConfirm).Equal(x => x.Password).WithMessage("Parolalar uyuşmuyor");
        }
    }
}
