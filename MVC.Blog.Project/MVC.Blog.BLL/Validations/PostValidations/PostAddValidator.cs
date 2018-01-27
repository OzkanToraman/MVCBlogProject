using FluentValidation;
using MVC.Blog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Blog.BLL.Validations.PostValidations
{
    public class PostAddValidator:AbstractValidator<Post>
    {
        public PostAddValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bir yazı başlığı giriniz.");
        }
    }
}
