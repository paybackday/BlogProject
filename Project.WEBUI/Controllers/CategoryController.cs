using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
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

        public ActionResult DeleteCategory(int id) {
            cp.Delete(cp.Find(id));
            return RedirectToAction("CategoryList");
        }

        
    }
}