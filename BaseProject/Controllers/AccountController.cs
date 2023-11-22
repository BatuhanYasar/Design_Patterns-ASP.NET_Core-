using BaseProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }



        public IActionResult Login()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Login(string email,string password) 
        {
        
            var hasUser = await _userManager.FindByEmailAsync(email);


            if(hasUser == null)  // Eğer boş ise ana sayfaya dönüyoruz.
            {
               return View();
            }


            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, password,true,false);


            if(!signInResult.Succeeded) // Eğer giriş başarılı değilse ana ekrana geri dön.
            {
                return View();
            }

            // nameof --> kullanılmasının sebebi Index adı değişirse uygulamamız patlasın.

            // "Home": Bu bölüm, yönlendirilecek olan controller'ın adını belirtir.

            // HomeController.Index ifadesi, "HomeController" adlı denetleyici sınıfının "Index" adlı eylemini ifade eder.

            // Belirtilen eyleme ve denetleyiciye gitmek için bir HTTP yönlendirmesi oluşturur.

            return RedirectToAction(nameof(HomeController.Index),"Home");

        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
