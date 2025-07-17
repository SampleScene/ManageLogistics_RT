using ManageLogistics_RT.Data;
using ManageLogistics_RT.Helpers;
using ManageLogistics_RT.Interface;
using ManageLogistics_RT.Models;
using ManageLogistics_RT.Repository;
using ManageLogistics_RT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace ManageLogistics_RT.Controllers
{
    public class StopController : Controller
    {
        private readonly IStopRepository _stopRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StopController( IStopRepository stopRepository,
            IPhotoService photoService,
            IHttpContextAccessor httpContextAccessor) 
        {
            _stopRepository = stopRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
       
        public async Task<IActionResult> Index()
        {
            IEnumerable<Stop> stops = await _stopRepository.GetAll();
            List<StopViewModel> stopsSerr = new List<StopViewModel>();
            foreach (var item in stops)
            {
               // Address address = JsonSerializer.Deserialize<Address>(item.Address); 
                stopsSerr.Add(new StopViewModel
                {
                    Id = item.Id,
                    City = item.City,
                    Street = item.Street,
                    Home = item.Home,
                    Image = item.Image,
                   
                });
            } 
            return View(stopsSerr);
        }
 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStopViewModel stopVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(stopVM.Image);
                var stop = new Stop
                {
                    City = stopVM.City,
                    Street = stopVM.Street,
                    Home = stopVM.Home,
                    Image = result.Url.ToString()
                };
                _stopRepository.Add(stop);
                return RedirectToAction("Index", "Stop");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload error");
                return View(stopVM);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var stop = await _stopRepository.GetByIdAsync(id);
            if (stop == null) return View("Error");
            //var jsonAddress = JsonSerializer.Deserialize<Address>(stop.Address);

            var stopVM = new EditStopViewModel
            {
                City = stop.City,
                Street = stop.Street,
                Home = stop.Home,
                URL = stop.Image
            };
            
            return View(stopVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditStopViewModel stopVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit stop");
            }
            
            var currentStop = await _stopRepository.GetByIdAsyncNoTracking(id);

            if (currentStop != null)
            {
                var stop = new Stop
                {
                    Id = id,
                    City = stopVM.City,
                    Street = stopVM.Street,
                    Home = stopVM.Home,
                    //Address = JsonSerializer.Serialize(stopVM.Address).ToString(),
                    //Image = photoResult.Url.ToString()
                };

                if (stopVM.Image == null)
                {
                    stop.Image = currentStop.Image;
                }
                else
                { 
                    try
                    {
                        await _photoService.DeletePhotoAsync(currentStop.Image);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Could not delete photo");
                        return View(stopVM);
                    }
                    var photoResult = await _photoService.AddPhotoAsync(stopVM.Image);
                    stop.Image = photoResult.Url.ToString();
                }
                
                _stopRepository.Update(stop);
                return RedirectToAction("Index", "Stop");
            }
            else
            {
                return View(stopVM);
            }
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var stopDetails = await _stopRepository.GetByIdAsync(id);
            if (stopDetails == null) return View("Error");
            return View(stopDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteStop(int id)
        {
            var stopDetails = await _stopRepository.GetByIdAsync(id);
            if (stopDetails == null) return View("Error");

            _stopRepository.Delete(stopDetails);
            return RedirectToAction("Index");
        }
    }
}
