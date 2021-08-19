using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Product.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ProductSqlData productSqlData;
        public IEnumerable<Product.Data.Product> Products { get; set; }
        public int PageNo { get; set; } = 1;
        public int ProductCount { get; set; }
        [TempData]
        public string Message { get; set; }
        public int PageSize { get; set; } = 10;

        public IndexModel(ILogger<IndexModel> logger,
            ProductSqlData productSqlData)
        {
            _logger = logger;
            this.productSqlData = productSqlData;
        }

        public void OnGet(int? pageNo)
        {
            if (pageNo.HasValue)
            {
                PageNo = pageNo.Value;
            }
            Products = productSqlData.GetProducts(PageSize, PageNo);
            ProductCount = productSqlData.GetCount();
        }
    }
}
