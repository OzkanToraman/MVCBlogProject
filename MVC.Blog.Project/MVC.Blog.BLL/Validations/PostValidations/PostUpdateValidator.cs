using FluentValidation;
using MVC.Blog.DAL.Data;
using MVC.Blog.Repository.UOW.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Blog.BLL.Validations.PostValidations
{
    public class PostUpdateValidator:AbstractValidator<Post>
    {
        private IUnitOfWork _uow;
        public PostUpdateValidator(IUnitOfWork uow)
        {
            _uow = uow;
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bir başlık adı girmelisiniz!");
            RuleFor(x => x.PostBody).NotEmpty().WithMessage("İçerik boş geçilemez!");
            RuleFor(x => x.PostBody).MinimumLength(100).WithMessage("En az 100 karakter girmelisiniz!");
            RuleFor(x => x.Title).Must(ExistTitle).WithMessage("Böyle bir başlık önceden girilmiş!");
        }

        public bool ExistTitle(string title)
        {
            if (_uow.GetRepo<Post>()
                .Any(x => x.Title == title))
            {
                return true;
            }
            return false;
            
        }
    }
}
