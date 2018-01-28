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
            RuleFor(x => x.PostBody).NotEmpty().WithMessage("Gönderi içeriği boş geçilemez");
            RuleFor(x => x.PostBody).MinimumLength(100).WithMessage("En az 100 karakter girmelisiniz.");
            RuleFor(x => x.PostPic).NotEmpty().WithMessage("Gönderi için bir fotoğraf eklemelisiniz!");
        }
    }
}
