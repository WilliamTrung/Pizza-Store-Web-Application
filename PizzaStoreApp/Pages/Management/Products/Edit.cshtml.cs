using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessService.IService;
using BusinessService.DTOs;

namespace PizzaStoreApp.Pages.Management
{
    public class EditModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;
        private readonly IProductService _productService;


        public EditModel(ISupplierService supplierService, ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _supplierService = supplierService;
            _productService = productService;
        }
        [BindProperty]
        public IFormFile? FileUpload { get; set; } = default;
        [BindProperty]
        public Product Product { get; set; } = default!;
        public string Message { get; set; } =  string.Empty;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  await _productService.GetDTOs(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product.First();
           ViewData["CategoryId"] = new SelectList(await _categoryService.GetDTOs(), "CategoryId", "CategoryName");
           ViewData["SupplierId"] = new SelectList(await _supplierService.GetDTOs(), "SupplierId", "CompanyName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string image)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            if (FileUpload != null && FileUpload.Length > 0)
            {
                var file_stored = "wwwroot" + "/" + "images" + "/" + FileUpload.FileName;
                var file_save = "images" + "/" + FileUpload.FileName;
                using (var filestream = new FileStream(file_stored, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(filestream);
                }
                Product.ProductImage = file_save;
            }
            else
            {
                Product.ProductImage = image;
            }
            try
            {
                var result = await _productService.Update(filter: p=>p.ProductId == Product.ProductId, Product);
                if(result == null)
                {
                    Message = "Cannot update produc!";
                }
            }
            catch
            {
                Message = "Cannot update produc!";
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return false;
        }
    }
}
