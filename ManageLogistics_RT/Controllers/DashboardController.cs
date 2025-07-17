using CloudinaryDotNet.Actions;
using ManageLogistics_RT.Data;
using ManageLogistics_RT.Helpers;
using ManageLogistics_RT.Interface;
using ManageLogistics_RT.Models;
using ManageLogistics_RT.Repository;
using ManageLogistics_RT.ViewModels;
using ManageLogistics_RT.ViewModels.UserDashboard;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QRCoder;
using System.Security.Claims;

namespace ManageLogistics_RT.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUserDashboardRepository _userDashboardRepository;
        private readonly IPhotoService _photoService;
        private readonly ApplicationDbContext _context;
        private readonly IQRCodeGenerator _qRCodeGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public DashboardController(IUserDashboardRepository userDashboardRepository,
            IHttpContextAccessor httpContextAccessor,
            IPhotoService photoService,
            ApplicationDbContext context,
            IQRCodeGenerator qRCodeGenerator) 
        {
            _userDashboardRepository = userDashboardRepository;
            _photoService = photoService;
            _context = context;
            _qRCodeGenerator = qRCodeGenerator;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> EmployeePreview()
        {
            var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var employee = await _context.employees.Include(t => t.Terminal).FirstOrDefaultAsync(i => i.EmployeeId == userId);
            return View(employee);
        }
        
        public async Task<IActionResult> UserQR()
        {
            var userId =  _httpContextAccessor.HttpContext?.User.GetUserId();
            if (string.IsNullOrEmpty(userId)) return BadRequest();

            byte[] QRCodeAsBytes = _qRCodeGenerator.GenerateQRCode(userId);
            string QRCodeAsImageBase64 = $"data:image/png;base64,{Convert.ToBase64String(QRCodeAsBytes)}";

            GenerateQRCodeViewModel model = new GenerateQRCodeViewModel();

            model.QRCodeImageUrl = QRCodeAsImageBase64;

            return View(model);
        }
        
        public async Task<IActionResult> UserPrices()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            
            //TODO: include
            var userPassReciepts = await _context.passReciepts.Where(i => i.AppUserId == curUserId ).ToListAsync();
            
            var priceList = new List<Price>();
            
            foreach (var userPassRecipt in userPassReciepts)
            {
                var price = await _context.prices.AsNoTracking().FirstOrDefaultAsync(i => i.Id == userPassRecipt.PriceId);

                var tripRecieptsCount = _context.TripReceipts.Count(p => p.PassId == userPassRecipt.Id);

                if (price.Time != null)
                {
                    double hours = Convert.ToDouble(price.Time);
                    var timeEnd = userPassRecipt.TimeStump.Value.AddHours(hours);
                    if (timeEnd > DateTime.Now)
                    {
                        price.Time =  Convert.ToInt32((timeEnd - DateTime.Now).TotalHours);
                    }
                    else
                    {
                        price.Time = -1;
                    }
                }
                else
                {
                    int? number = price.Number - tripRecieptsCount;
                    price.Number = number;
                }
                //int? number = price.Number - tripRecieptsCount;
                
                //price.Number = number;

                priceList.Add(price);
            }
            
            //var userNumberPrice = await _context.prices.FromSqlRaw($"SELECT prices.Id, prices.Title, prices.TerminalId, prices.Type, prices.Fare, prices.Time, FullPricesCount.Poezdki as Number from prices, (SELECT passReciepts.Id, passReciepts.AppUserId, passReciepts.PriceId, passReciepts.TimeStump, PassCount.Poezdki from passReciepts, (SELECT Test.Id as PassId, COUNT(TripReceipts.Id) as Poezdki FROM (SELECT passReciepts.Id FROM passReciepts, prices WHERE AppUserId = '6fde53cc-2f54-4d0b-b452-03be6449eef7' and passReciepts.PriceId = prices.Id and prices.Time is null) as Test LEFT OUTER JOIN TripReceipts ON Test.Id = TripReceipts.PassId GROUP by Test.Id) as PassCount where passReciepts.Id = PassCount.PassId) as FullPricesCount where FullPricesCount.PriceId = prices.Id").ToListAsync();

            var dashboardViewModel = new UserDashboardViewModel()
            {
                userPrices = priceList
            };
           
            return View(dashboardViewModel);
        }
        
        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var user = await _userDashboardRepository.GetUserById(curUserId);
            if (user == null) return View("Error");
            var editUserViewModel = new EditUserProfileViewModel()
            {
                Id = user.UserId,
                URL = user.Image,
                UserAuthData = user.UserAuthData,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Balance = user.Balance
                
            };
            
            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserProfileViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ошибка редкатирования профиля");
                return View("EditUserProfile", editVM);
            }

            var userNoTrac = await _userDashboardRepository.GetByIdNoTracking(editVM.Id);
            if (userNoTrac == null) return View("Error");

            
            if (userNoTrac != null)
            {
                AppUser user = new AppUser() 
                {
                    UserId = editVM.Id,
                    //Image = user.Image,
                    //UserAuthData = editVM.UserAuthData,
                    FirstName = editVM.FirstName,
                    LastName = editVM.LastName,
                    Birthday = editVM.Birthday,
                    Balance = editVM.Balance
                };

                if (editVM.Image == null)
                {
                    user.Image = userNoTrac.Image;
                }
                else
                {
                    if (userNoTrac.Image != null)
                    {
                        try
                        {
                            await _photoService.DeletePhotoAsync(userNoTrac.Image);
                        }
                        catch
                        {
                            ModelState.AddModelError("", "Could not delete photo");
                            return View(editVM);
                        }
                    }
                    var photoResult = await _photoService.AddPhotoToUserProfileAsync(editVM.Image);
                    user.Image = photoResult.Url.ToString();
                }
                _userDashboardRepository.Update(user);
                return RedirectToAction("EditUserProfile", "Dashboard");
            }
            else
            {
                return View(editVM);
            }
        }

        public IActionResult ListQuery()
        {
            return View();
        }

    }
}