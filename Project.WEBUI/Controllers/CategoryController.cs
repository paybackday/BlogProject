using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.WEBUI.AuthenticationClasses;
using Project.WEBUI.Models.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.WEBUI.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository cp;
        public CategoryController()
        {
            cp = new CategoryRepository();
        }
        // GET: Category
        public ActionResult CategoryList()
        {
            CategoryVM cvm = new CategoryVM
            {
                Categories = cp.GetActives()

            };
            return View(cvm);
        }

        [AdminAuthentication]
        public ActionResult DeleteCategory(int id) {
            cp.Delete(cp.Find(id));
            return RedirectToAction("CategoryList");
        }
        [AdminAuthentication]
        public ActionResult AddCategory() {
            
            return View();
        }
        
        [HttpPost]
        public ActionResult AddCategory([Bind(Prefix = "Category")] Category item, HttpPostedFileBase resim)
        {
            item.ImagePath = ImageUploader.UploadImage("~/Pictures/", resim);
            cp.Add(item);
            return RedirectToAction("CategoryList");
        }
        [AdminAuthentication]
        public ActionResult UpdateCategory(int id) {
            CategoryVM cvm = new CategoryVM
            {
                Category = cp.Find(id)
            };
            return View(cvm);
        }
        [HttpPost]
        public ActionResult UpdateCategory([Bind(Prefix ="Category")] Category item,HttpPostedFileBase resim)
        {


            Category guncellenecek = cp.Find(item.ID);
            guncellenecek.CategoryName = item.CategoryName;
            guncellenecek.Description = item.Description;
            guncellenecek.ImagePath= ImageUploader.UploadImage("~/Pictures/", resim);

            return RedirectToAction("CategoryList");
        }
    }
}