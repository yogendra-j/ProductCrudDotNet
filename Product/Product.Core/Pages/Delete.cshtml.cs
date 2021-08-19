using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Product.Data;

namespace Product.Core.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ProductSqlData productSqlData;

        public Data.Product CurProduct { get; set; }

        public DeleteModel(ProductSqlData productSqlData)
        {
            this.productSqlData = productSqlData;
        }
        public void OnGet(int Id)
        {
            CurProduct = productSqlData.GetById(Id);
        }
        public IActionResult OnPost(int Id)
        {
            var CurProduct = productSqlData.Delete(Id);
            productSqlData.Commit();

            if (CurProduct == null)
            {
                return RedirectToPage("./NotFound");
            }

            TempData["Message"] = $"{CurProduct.Name} deleted";
            return RedirectToPage("./Index");
        }
    }
}
