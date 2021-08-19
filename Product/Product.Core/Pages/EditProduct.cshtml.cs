using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Product.Data;

namespace Product.Core.Pages
{
    public class EditProductModel : PageModel
    {
        private readonly ProductSqlData productSqlData;
        public string PageName { get; set; }
        [BindProperty]
        public Product.Data.Product CurProduct { get; set; }

        public EditProductModel(ProductSqlData productSqlData)
        {
            this.productSqlData = productSqlData;
        }
        public IActionResult OnGet(int? Id)
        {
            if (!Id.HasValue)
            {
                CurProduct = new Data.Product();
                PageName = "Add Product";
                CurProduct.CreatedOn = DateTime.Now;
                CurProduct.ModifiedOn = DateTime.Now;
            }
            else
            {
                CurProduct = productSqlData.GetById(Id.Value);
                PageName = "Edit Product";
                

            }
            Console.WriteLine(CurProduct.Id);

            return Page();

        }
        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (CurProduct.Id > 0)
            {
                productSqlData.Update(CurProduct);
            }
            else
            {
                productSqlData.Add(CurProduct);
            }
            try
            {

                productSqlData.Commit();
            }
            catch (DbUpdateException e){
                TempData["Message"] = "Product Already Exists!";
                Console.WriteLine(e.Message);
                return RedirectToPage("/Index");

            }
            TempData["Message"] = "Product saved!";
            return RedirectToPage("/Index");
            }
        }
    }
