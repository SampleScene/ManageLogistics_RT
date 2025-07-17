using ManageLogistics_RT.Data;
using ManageLogistics_RT.Interface;
using ManageLogistics_RT.Models;
using ManageLogistics_RT.ViewModels;
using ManageLogistics_RT.ViewModels.PriceVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManageLogistics_RT.Controllers
{
    public class PriceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPriceRepository _priceRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PriceController( IPriceRepository priceRepository, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _priceRepository = priceRepository;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Price> prices = await _priceRepository.GetAll();
            return View(prices);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Price price)
        {
            if (User.Identity.IsAuthenticated && !User.IsInRole("user"))
            {
                if (!ModelState.IsValid) return View(price);

                var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
                var user = await  _context.employees.FirstOrDefaultAsync(i => i.EmployeeId == curUserId);
                price.TerminalId = user.TerminalId;
                _priceRepository.Add(price);
                return RedirectToAction("Index", "Price");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var price = await _priceRepository.GetByIdAsync(id);
            if (price == null) return View("Error");
            var priceVM = new EditPriceViewModel
            {
                Title = price.Title,
                TerminalId = price.TerminalId,
                Type = price.Type,
                Fare = price.Fare,
                Time = price.Time,
                Number = price.Number
            };
            return View(priceVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditPriceViewModel priceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", priceVM);
            }
            var userPrice = await _priceRepository.GetByIdAsyncNoTracking(id);
            if (userPrice != null)
            {
                var price = new Price
                {
                    Id = id,
                    Title = priceVM.Title,
                    TerminalId = priceVM.TerminalId,
                    Type = priceVM.Type,
                    Fare = priceVM.Fare,
                    Time = priceVM.Time,
                    Number = priceVM.Number
                };

                _priceRepository.Update(price);

                return RedirectToAction("Index", "Price");
            }
            else
            {
                return View(priceVM);
            }
        }
        
        public async Task<IActionResult> PriceList(PriceListViewModel priceListViewModel)
        {

            var terminals = await _context.terminals.ToListAsync();
            var prices = await _context.prices.ToListAsync();

            var priceListVM = new PriceListViewModel()
            {
                terminals = terminals,
                prices = prices,
                errorMessage = priceListViewModel.errorMessage
            };
            return View(priceListVM);
        }

        public async Task<IActionResult> Buy(int id)
        {
            var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
            
            var user = await _context.appUsers.AsNoTracking().FirstOrDefaultAsync(i => i.UserId == userId);
            if (user != null)
            {
                var price = await _context.prices.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

                if (user.Balance >= price.Fare)
                {
                    var userupdae = new AppUser()
                    {
                        UserId = user.UserId,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Image = user.Image,
                        Birthday = user.Birthday,
                        Balance = user.Balance - price.Fare,
                    };

                    _context.appUsers.Update(userupdae);
                    _context.SaveChanges();

                    var passReceipt = new PassReciept()
                    {
                        AppUserId = userId,
                        PriceId = id,
                        TimeStump = DateTime.Now,
                    };
                    _context.passReciepts.Add(passReceipt);
                    _context.SaveChanges();
                    return RedirectToAction("UserPrices", "Dashboard");
                }
                else
                {
                    var priceListVM = new PriceListViewModel()
                    {
                        errorMessage = "Недостаточно средств не счете"
                    };
                    return RedirectToAction("PriceList", "Price", priceListVM);
                }
            }
            var price_ListVM = new PriceListViewModel()
            {
                errorMessage = "Пользователь не найден"
            };
            return RedirectToAction("PriceList", "Price", price_ListVM);
            
        }
    }
}