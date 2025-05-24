using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; 
using System.Linq; 

namespace MVC_No_1.Controllers
{
    public class BoardController : Controller
    {
        public IActionResult Chart()
        {
           
            var salesData = new List<object>
            {
                new { Month = "Tháng 1", Sales = 150 },
                new { Month = "Tháng 2", Sales = 200 },
                new { Month = "Tháng 3", Sales = 180 },
                new { Month = "Tháng 4", Sales = 220 },
                new { Month = "Tháng 5", Sales = 250 },
                new { Month = "Tháng 6", Sales = 230 }
            };

            var productCategories = new List<object>
            {
                new { Category = "Điện tử", Value = 40 },
                new { Category = "Thời trang", Value = 25 },
                new { Category = "Gia dụng", Value = 20 },
                new { Category = "Khác", Value = 15 }
            };

            ViewData["SalesData"] = salesData;
            ViewData["ProductCategories"] = productCategories;

            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}