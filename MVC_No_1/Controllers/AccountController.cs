using Microsoft.AspNetCore.Mvc;
using MVC_No_1.Models;
using System.Threading.Tasks; 

public class AccountController : Controller
{
    // GET: /Account/Login
    public IActionResult Login()
    {
        return View(); 
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Username == "test" && model.Password == "123")
            {

                return RedirectToAction("Index2", "Manager"); 
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
        }

        return View(model);
    }

}