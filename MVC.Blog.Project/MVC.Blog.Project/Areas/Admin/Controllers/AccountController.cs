using MVC.Blog.BLL.Services.Abstract;
using MVC.Blog.BLL.Services.Concrete;
using MVC.Blog.BLL.Validations;
using MVC.Blog.DAL;
using MVC.Blog.DAL.Data;
using MVC.Blog.Repository.UOW.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC.Blog.Project.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMailMessage _mesaj;
        private readonly IEncryptor _encrypt;
        bool IsSuccess;
        string _kod;

        public AccountController(IUnitOfWork uow, IMailMessage mesaj,IEncryptor encrypt)
        {
            _uow = uow;
            _mesaj = mesaj;
            _encrypt = encrypt;
        }

        // GET: Admin/Account
        public ActionResult Dashboard()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("/Admin/Dashboard/Home/");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(Kullanici model)
        {
            var validator = new BaseValidator().Validate(model);
            if (validator.IsValid)
            {
                model.Password = _encrypt.Hash(model.Password);

                Kullanici loginUser =
                 _uow
                 .GetRepo<Kullanici>()
                 .Where(x => x.Email == model.Email && x.Password == model.Password)
                 .FirstOrDefault();

                if (loginUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                    return Redirect("/Admin/Dashboard/Home/");
                }
                else
                {
                    ViewBag.Msg = "E-posta ya da şifre hatalı!";
                    return View();
                }
            }
            else
            {
                validator.Errors.ToList().ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Register(Kullanici model)
        {
            var validator = new UserValidator().Validate(model);
            bool sorgu = _uow.GetRepo<Kullanici>()
                .Any(x => x.Email == model.Email);

            IsSuccess = false;

            if (validator.IsValid)
            {
                if (!sorgu)
                {

                    _kod = Guid.NewGuid().ToString();
                    model.Kod = _kod;
                    string url = Path.Combine("http://localhost:2815/Admin/Account/RegisterConfirm/", "?kod=" + _kod);
                    string htmlString = "<a href=" + url + ">Üyeliğimi Doğrula</a>";

                    var message = (MailMessage)_mesaj;
                    message.To = model.Email;
                    bool Ok = await message.SendMessageAsync("Üyelik Doğrulama Kodu", htmlString);

                    ModelState.Clear();

                    if (Ok)
                    {
                        IsSuccess = true;
                        ViewBag.IsSuccess = IsSuccess;
                        ViewBag.Msg = "E-posta başarıyla gönderilmiştir";
                    }
                    else
                    {
                        IsSuccess = false;
                        ViewBag.IsSuccess = false;
                        ViewBag.Msg = "E-posta gönderilirken bir hata oluştu,lütfen tekrar deneyin!";
                    }

                    string pass = _encrypt.Hash(model.Password);
                    model.Password = pass;
                    model.PasswordConfirm = pass;

                    _uow.GetRepo<Kullanici>().Add(model);
                    _uow.Commit();
                }
                else
                {
                    ViewBag.IsSuccess = IsSuccess;
                    ViewBag.Msg = "Bu e-posta adresine ait bir kullanıcı zaten mevcut!";
                }
            }
            else
            {
                validator.Errors.ToList().ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
            }
            return View();
        }

        public ActionResult RegisterConfirm(string kod)
        {
            int userId = _uow.GetRepo<Kullanici>()
                .Where(x => x.Kod == kod)
                .FirstOrDefault()
                .Id;
            if (userId != 0)
            {
                _uow.GetRepo<Kullanici>()
                .GetById(userId)
                .IsAccepted = true;
                _uow.Commit();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(Kullanici model)
        {
            var validator = new ForgotPasswordValidator().Validate(model);
            if (validator.IsValid)
            {
                int forgotPasswordUserId = _uow.GetRepo<Kullanici>()
                    .Where(x => x.Email == model.Email)
                    .FirstOrDefault()
                    .Id;

                if (forgotPasswordUserId != 0)
                {
                    _kod = Guid.NewGuid().ToString();

                    _uow.GetRepo<Kullanici>()
                        .GetById(forgotPasswordUserId)
                        .Kod = _kod;
                    _uow.Commit();

                    string url = Path.Combine("http://localhost:2815/Admin/Account/PasswordConfirm/", "?kod=" + _kod);
                    string htmlString = "<a href=" + url + ">Parolamı sıfırla</a>";

                    var message = (MailMessage)_mesaj;
                    message.To = model.Email;
                    bool Ok = message.SendMessage("Şifre Sıfırlama Talimatı", htmlString);

                    ModelState.Clear();
                    if (Ok)
                    {
                        IsSuccess = true;
                        ViewBag.IsSuccess = IsSuccess;
                        ViewBag.Msg = "E-postanıza onay linki gönderilmiştir";
                    }
                    else
                    {
                        IsSuccess = false;
                        ViewBag.IsSuccess = IsSuccess;
                        ViewBag.Msg = "E-posta gönderilirken bir hata oluştu,lütfen tekrar deneyiniz!";
                    }
                    return View();
                }
                IsSuccess = false;
                ViewBag.Msg = "Böyle bir kayıt bulunamadı!";
                return View();
            }
            else
            {
                validator.Errors.ToList().ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
                return View();
            }
        }

        public ActionResult PasswordConfirm(string kod)
        {
            Kullanici model = new Kullanici();
            Kullanici passwordConfirm = _uow.GetRepo<Kullanici>()
                .Where(x => x.Kod == kod)
                .FirstOrDefault();
            if (passwordConfirm != null)
            {
                model.Id = passwordConfirm.Id;
                model.Email = passwordConfirm.Email;
            }
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordConfirm(Kullanici model)
        {

            var validator = new PasswordConfirmValidator().Validate(model);
            if (validator.IsValid)
            {
                _uow.GetRepo<Kullanici>()
                    .GetById(model.Id)
                    .Password = model.Password;

                if (_uow.Commit() > 0)
                {
                    IsSuccess = true;
                    ViewBag.IsSuccess = IsSuccess;
                    ViewBag.Msg = "Şifre başarıyla değiştirilmiştir.";
                }
                else
                {
                    IsSuccess = false;
                    ViewBag.IsSuccess = IsSuccess;
                    ViewBag.Msg = "Şifre değiştirme işlemi başarısız,tekrar deneyiniz";
                }
                return View();
            }
            else
            {
                validator.Errors.ToList().ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
                return View();
            }
        }

        [Authorize]
        public RedirectResult LogOut()
        {
            //cookie siler
            FormsAuthentication.SignOut();

            return Redirect("/Admin/Account/Login");
        }
    }
}