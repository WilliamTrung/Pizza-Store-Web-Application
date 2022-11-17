using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessService.IService;
using BusinessService.DTOs;
using BusinessService.Service;
using System.IO;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using PizzaStoreApp.Filter;

namespace PizzaStoreApp.Pages.Management
{
    [Authorized("0")]
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;
        

        public CreateModel(
            IProductService productService, 
            ICategoryService categoryService, 
            ISupplierService supplierService)
        {
            _productService = productService;
            _supplierService = supplierService;
            _categoryService = categoryService;
            Product = new Product();
        }

        public async Task<IActionResult> OnGetAsync()
        {
        ViewData["CategoryId"] = new SelectList(await _categoryService.GetDTOs(), "CategoryId", "CategoryName");
        ViewData["SupplierId"] = new SelectList(await _supplierService.GetDTOs(), "SupplierId", "Address");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public IFormFile? FileUpload { get; set; } 

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            if(FileUpload.Length > 0)
            {
                var file_stored = "wwwroot" + "/" + "images" + "/" +  FileUpload.FileName;
                var file_save = "images" + "/" + FileUpload.FileName;
                using (var filestream = new FileStream(file_stored, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(filestream);
                }
                Product.ProductImage = file_save;
            }
            await _productService.Create(Product);

            return RedirectToPage("./Index");
        }
    }
}
