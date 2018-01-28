using MVC.Blog.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.Blog.DAL.Data;
using MVC.Blog.Repository.UOW.Abstract;
using System.Web;
using System.IO;

namespace MVC.Blog.BLL.Services.Concrete
{
    public class UploadSave : IUploadSave
    {
        IUnitOfWork _uow;
        public UploadSave(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public Task<bool> SaveUploadAsync(MediaUpload model)
        {
            return Task.Run(() =>
            {
                _uow.GetRepo<MediaUpload>()
                .Add(model);

                if (_uow.Commit() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }
    }
}
