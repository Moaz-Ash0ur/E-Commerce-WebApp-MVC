using ECommerceStore.BLL.Interface;
using ECommerceStore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MStore.Properties;
using MStore.Utlities;
using System.Drawing.Printing;

namespace MStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public IBrandService _brandService;

       
        public ProductController(IProductService productService, 
            ICategoryService categoryService , 
            IBrandService brandService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
        }


        public void CreateDropDownList()
        {
            var categories = _categoryService.GetAll();
            SelectList selectListItemsC  = new SelectList(categories,"Id","Name");
            ViewBag.Categories = selectListItemsC;

            var brands = _brandService.GetAll();
            SelectList selectListItemsB = new SelectList(brands, "Id", "Name");
            ViewBag.Brands = selectListItemsB;
        }

        public IActionResult Index(int pageNumber = 1, string? search = "")
        {
            var pageSize = Convert.ToInt32(ResGeneral.PageSize);

            (var productsDetails,var totalPages) = _productService.PaginateProducts(pageNumber,pageSize,search);

            ViewBag.PageNumber = pageNumber;//sent it to know the active page
            ViewBag.TotalPages = totalPages;//to show the number of pages depand pageSize
            ViewBag.ValueSearch = search;//to save the value on paginate

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ProductTablePartial", productsDetails);
            }

            return View(productsDetails);
        }

        public IActionResult Create()
        {
            CreateDropDownList();
            return View("Form",new Product());
        }

        [HttpPost]
        public IActionResult Create(Product product,IFormFile? ImageFile)
        {
            if (ImageFile != null)
            {
                product.ImageName = UploadImage.SaveImage(ImageFile,product.ImageName);
            }
        
            _productService.Create(product);
            CreateDropDownList();
            return RedirectToAction(nameof(Index));
           
        }

        public IActionResult Edit(int ProductID)
        {
            var product = _productService.GetById(ProductID);
            if (product == null)
                return NotFound();

            CreateDropDownList();
            return View("Form",product);
        }

        [HttpPost]
        public IActionResult Edit(Product product, IFormFile? ImageFile)
        {
            if (ImageFile != null)
            {
                product.ImageName = UploadImage.SaveImage(ImageFile, product.ImageName);

            }

            CreateDropDownList();
           _productService.Update(product);
           return RedirectToAction(nameof(Index));
           

        }

        public IActionResult Delete(int ProductID)
        {
            _productService.Delete(ProductID);
            return RedirectToAction(nameof(Index));
        
        }


  





    }
}
