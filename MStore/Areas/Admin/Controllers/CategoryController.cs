using ECommerceStore.BLL.Interface;
using ECommerceStore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]

    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View("Form",new Category());
        } 

        [HttpPost]
        public IActionResult Create(Category category)
        {


       //     if (ModelState.IsValid)
         //   {
                _categoryService.Create(category);
                return RedirectToAction(nameof(Index));
         //   }
           //return View(category);
        }

        public IActionResult Edit(int id)
        {
            var cat = _categoryService.GetById(id);
            return cat == null ? NotFound() : View("Form",cat);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
           // if (ModelState.IsValid)
           // {
                _categoryService.Update(category);
                return RedirectToAction(nameof(Index));
           // }
           // return View(category);
        }

        public IActionResult Delete(int id)
        {
            var cat = _categoryService.GetById(id);
            return cat == null ? NotFound() : View(cat);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _categoryService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    

    
    
    
    }
}
