using ECommerceStore.BLL;
using ECommerceStore.BLL.Interface;
using ECommerceStore.Entities;
using ECommerceStore.Entities.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using MStore.Models;
using MStore.Properties;
using Newtonsoft.Json.Linq;
using System.Drawing.Printing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public StoreController(IProductService productService,ICategoryService categoryService,IBrandService brandService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
        }

        public void CreateDropDownList()
        {
            var categories = _categoryService.GetAll();
            SelectList selectListItemsC = new SelectList(categories, "Name", "Name");
            ViewBag.Categories = selectListItemsC;

            //var brands = _brandService.GetAll();
            //SelectList selectListItemsB = new SelectList(brands, "Id", "Name");
            //ViewBag.Brands = selectListItemsB;
        }

        public IActionResult Index(int pageNumber = 1,string search = "")
        {
            var pageSize = Convert.ToInt32(ResGeneral.PageSizeForStore);

            (var productsDetails, var totalPages) = _productService.PaginateProducts(pageNumber, pageSize, search);

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.ValueSearch = search;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ProductBox",productsDetails);
            }

            return View(productsDetails);

        }

        public IActionResult Details(int ProductID,string? returnUrl)
        {         
          var produstDetails = _productService.GetDetailsById(ProductID);

           if (produstDetails == null)
              return RedirectToAction("Index");

            ViewBag.ReturnUrl = returnUrl ?? Url.Action("Index", "Store");

            return View(produstDetails);

        }





    }
}
