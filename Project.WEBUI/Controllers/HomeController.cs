using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.WEBUI.Controllers
{
    public class HomeController : Controller
    {
        AppUserRepository apRep;
        public HomeController()
        {
            apRep = new AppUserRepository();
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login() {

            return View();
        
        }

        [HttpPost]
        public ActionResult Login([Bind(Prefix = "AppUser")] AppUser item) {
            AppUser yakalanan = apRep.FirstOrDefault(x => x.UserName == item.UserName);
            string decrypted = DantexCrypt.DeCrypt(yakalanan.Password);
            if (item.Password==decrypted && yakalanan!=null&&yakalanan.Role==ENTITIES.Enum.UserRole.Admin)
            {
                if (!yakalanan.Active)
                {
                    return AktifKontrol();
                }
                Session["admin"] = yakalanan;
                return RedirectToAction("CategoryList", "Category");
            }
            else if (yakalanan.Role==ENTITIES.Enum.UserRole.Member)
            {
                if (!yakalanan.Active)
                {
                    return AktifKontrol();
                }
                Session["member"] = yakalanan;
                return RedirectToAction("CategoryList", "Category");
            }
            ViewBag.KullaniciYok = "Kullanici Bulunamadi";
            return View();
        
        }


        private ActionResult AktifKontrol() {
            ViewBag.AktifDegil = "Lutfen hesabinizi aktif hale getiriniz. Bununu icinde mailinizi kontrol ediniz.";
            return View("Login");
        }
    }
}