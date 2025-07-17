using ManageLogistics_RT.Data;
using ManageLogistics_RT.Models;
using ManageLogistics_RT.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ManageLogistics_RT.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserAuthData> _userManager;
        private readonly SignInManager<UserAuthData> _signInManager;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<UserAuthData> userManager, SignInManager<UserAuthData> signInManager, ApplicationDbContext context) 
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM) 
        {
            if(!ModelState.IsValid) return View(loginVM);
            
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Неправильные учетные данные. Пожалуйста, попробуйте еще раз";
                return View(loginVM);
            }
            TempData["Error"] = "Неправильные учетные данные. Пожалуйста, попробуйте еще раз";
            return View(loginVM);
        }

        public IActionResult Register()
        {
            var response = new RegisterUserViewModel();
            return View(response);
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUserViewModel)
        {
            if(!ModelState.IsValid) return View(registerUserViewModel);

             var user = await _userManager.FindByEmailAsync(registerUserViewModel.EmailAddress);
            if(user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerUserViewModel);
            }

            var newUser = new UserAuthData()
            {
                UserName = registerUserViewModel.UserName,
                Email = registerUserViewModel.EmailAddress
            };
            var newUserRsponse  = await _userManager.CreateAsync(newUser, registerUserViewModel.Password);

            if (newUserRsponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            JsonResult jsonData = Json(new { data = new { id = newUser.Id } });
            string jsonString = JsonConvert.SerializeObject(jsonData.Value);
            var jsObject = JsonConvert.DeserializeObject<dynamic>(jsonString);
            string UserID = jsObject.data.id;

            var newAppUser = new AppUser()
            {
                UserId = UserID
            };
            _context.appUsers.Add(newAppUser);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        
        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}