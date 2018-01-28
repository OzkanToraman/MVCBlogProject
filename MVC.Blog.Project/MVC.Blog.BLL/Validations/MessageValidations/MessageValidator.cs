using FluentValidation;
using MVC.Blog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Blog.BLL.Validations.MessageValidations
{
    public class MessageValidator:AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Konu başlığı boş olamaz!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta adresiniz boş olamaz!");
            RuleFor(x => x.Email).MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Yanlış giriş yaptınız!");
            RuleFor(x => x.MessageBody).NotEmpty().WithMessage("Konu içeriği boş olamaz!");
        }
    }
}
