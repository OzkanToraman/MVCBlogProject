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
    public class ForgotPasswordValidator:AbstractValidator<Kullanici>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-postanızı giriniz");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Epostanızı giriniz");

        }
    }
}
