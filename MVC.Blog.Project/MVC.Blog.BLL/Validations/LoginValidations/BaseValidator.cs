using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.Blog.DAL.Data;
using MVC.Blog.DAL;

namespace MVC.Blog.BLL.Validations
{
    public class BaseValidator:AbstractValidator<Kullanici>
    {
        public BaseValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta alanı boş geçilemez!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola boş geçilemez!");
        }
    }
}
