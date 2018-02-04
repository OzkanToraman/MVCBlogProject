using FluentValidation;
using MVC.Blog.DAL.Data;
using MVC.Blog.Repository.UOW.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Blog.BLL.Validations.CategoryValidations
{
    public class CategoryAddValidator:AbstractValidator<Category>
    {
        IUnitOfWork _uow;

        public CategoryAddValidator(IUnitOfWork uow)
        {
            _uow = uow;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori alanı boş geçilemez");
            RuleFor(x => x.Name).Must(ExistinName).WithMessage("Böyle bir kategori adı mevcuttur!");
        }

        public bool ExistinName(string Name)
        {
            if (!_uow.GetRepo<Category>()
                .Any(x => x.Name == Name))
            {
                return true;
            }

            return false;
             
                
        }
    }
}
