using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.WEBUI.Models.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.WEBUI.Controllers
{
    public class RegisterController : Controller
    {
        AppUserRepository apRep;
        UserProfileRepository apdRep;

        public RegisterController()
        {
            apRep = new AppUserRepository();
            apdRep = new UserProfileRepository();
        }
        // GET: Register
        public ActionResult RegisterNow()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterNow(AppUserVM apvm) 
        {
            AppUser appUser = apvm.AppUser;
            UserProfile profile = apvm.Profile;

            appUser.Password = DantexCrypt.Crypt(appUser.Password);

            if (apRep.Any(x=>x.UserName==appUser.UserName))
            {
                ViewBag.ZatenVar = "Kullanici ismi daha onceden alinmis";
                return View();
            }
            else if (apRep.Any(x=>x.Email==appUser.Email))
            {
                ViewBag.ZatenVar = "Email adresi daha onceden alinmis";
                return View();
            }

            string gonderilecekMail = "Tebrikler. Hesabiniz olusturuldu. Hesabinizi aktif etmek icin lutfen baglantiya tiklayin. https://localhost:44318/Register/Activation/" + appUser.ActivationCode;

            MailSender.Send(appUser.Email, password: "emreemre123", body: gonderilecekMail, subject: "Hesap Aktivasyon", sender: "emregorentest@gmail.com");
            apRep.Add(appUser);

            if (!string.IsNullOrEmpty(profile.FirstName) || !string.IsNullOrEmpty(profile.LastName))
            {
                profile.ID = appUser.ID;
                apdRep.Add(profile);
            }
            return View("RegisterOk");




           
        }

        public ActionResult Activation(Guid id) {
            AppUser aktifEdilecek = apRep.FirstOrDefault(x => x.ActivationCode == id);
            if (aktifEdilecek != null)
            {
                aktifEdilecek.Active = true;
                apRep.Update(aktifEdilecek);

                TempData["HesapAktifmi"] = "Hesabınız aktif hale getirildi";
                return RedirectToAction("Login", "Home");
            }
            TempData["HesapAktifmi"] = "Aktif edilecek hesap bulunamadı";
            return RedirectToAction("Login", "Home");

        }
        
        public ActionResult RegisterOk() {

            return View();
        }
    }
}