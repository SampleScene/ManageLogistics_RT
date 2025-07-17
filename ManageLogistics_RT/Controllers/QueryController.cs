using ManageLogistics_RT.Data;
using ManageLogistics_RT.Helpers;
using ManageLogistics_RT.Interface;
using ManageLogistics_RT.Models;
using ManageLogistics_RT.ViewModels.UserQuery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ManageLogistics_RT.Controllers
{
    public class QueryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QueryController(
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext context,
            IQRCodeGenerator qRCodeGenerator)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> UserToPrices()
        {
            var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var user = await _context.employees.FirstOrDefaultAsync(i => i.EmployeeId == userId);
            List<TripReceipt> allTripReciepts = new List<TripReceipt>();
            var traevlTrip = await _context.TripReceipts.FromSqlRaw($"select * FROM TripReceipts WHERE TripReceipts.PassId in (select id from passReciepts where priceID in ( SELECT id from prices where TerminalId = {user.TerminalId}))").ToListAsync();
            var ticketTrip = await _context.TripReceipts.FromSqlRaw($"select * from TripReceipts where tripId in (SELECT id from Trips where routeId in (select id from Ways WHERE terminalId = {user.TerminalId})) and Operation = 'ticket'").ToListAsync();

            allTripReciepts.AddRange(traevlTrip);
            allTripReciepts.AddRange(ticketTrip);

            int ticket = 0;
            int travelCard = 0;
             

            foreach ( var trip in allTripReciepts )
            {
                if (trip.Operation == "ticket")
                {
                    ticket++;
                }
                else
                {
                    travelCard++;
                }
            }



            var UserToPriceVM = new UserToPriceViewModel()
            {
                tripReceipts = allTripReciepts,
                ticketCount = ticket,
                travelCardCount = travelCard,
                allTrips = ticket + travelCard
            };
            
            return View(UserToPriceVM);
        }

        [HttpPost]
        public async Task<IActionResult> UserToPrices(string query)
        {
            UserToPriceViewModel userToPriceVM = new UserToPriceViewModel();
            userToPriceVM.Query = query;
            
            var userId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var user = await _context.employees.FirstOrDefaultAsync(i => i.EmployeeId == userId);
            List<TripReceipt> allTripReciepts = new List<TripReceipt>();

            if (query.Contains('-') && !query.Contains("/ticket"))
            {
                var massQuery = query.Split(' ');
                
                if (massQuery.Length > 1)
                {
                    var traevlTrip = await _context.TripReceipts.FromSqlRaw($"select * FROM TripReceipts WHERE (DATE(DateOfoperation) =  '{massQuery[0]}') and HOUR(DateOfoperation) = {massQuery[1]} and TripReceipts.PassId in (select id from passReciepts where priceID in ( SELECT id from prices where TerminalId = {user.TerminalId}))").ToListAsync();
                    var ticketTrip = await _context.TripReceipts.FromSqlRaw($"select * from TripReceipts where tripId in (SELECT id from Trips where (DATE(DateOfoperation) =  '{massQuery[0]}') and HOUR(DateOfoperation) = {massQuery[1]} and routeId in (select id from Ways WHERE terminalId = {user.TerminalId})) and Operation = 'ticket'").ToListAsync();
                    allTripReciepts.AddRange(ticketTrip);
                    allTripReciepts.AddRange(traevlTrip);
                }
                else
                {
                    var traevlTrip = await _context.TripReceipts.FromSqlRaw($"select * FROM TripReceipts WHERE (DATE(DateOfoperation) =  '{query}') and TripReceipts.PassId in (select id from passReciepts where priceID in ( SELECT id from prices where TerminalId = {user.TerminalId}))").ToListAsync();
                    var ticketTrip = await _context.TripReceipts.FromSqlRaw($"select * from TripReceipts where tripId in (SELECT id from Trips where (DATE(DateOfoperation) =  '{query}') and routeId in (select id from Ways WHERE terminalId = {user.TerminalId})) and Operation = 'ticket'").ToListAsync();
                    allTripReciepts.AddRange(traevlTrip);
                    allTripReciepts.AddRange(ticketTrip);
                }
            }
            else if (query.Contains("/ticket"))
            {
                var massQuery = query.Split(' ');

                if (massQuery.Length > 1)
                {
                    var ticketTrip = await _context.TripReceipts.FromSqlRaw($"select * from TripReceipts where tripId in (SELECT id from Trips where (DATE(DateOfoperation) =  '{massQuery[1]}') and routeId in (select id from Ways WHERE terminalId = {user.TerminalId})) and Operation = 'ticket'").ToListAsync();
                    allTripReciepts.AddRange(ticketTrip);
                }
                else
                {
                    var ticketTrip = await _context.TripReceipts.FromSqlRaw($"select * from TripReceipts where tripId in (SELECT id from Trips where (DATE(DateOfoperation) = DATE(current_timestamp())) and routeId in (select id from Ways WHERE terminalId = 1)) and Operation = 'ticket'").ToListAsync();
                    allTripReciepts.AddRange(ticketTrip);
                }
            }
            else
            {
                var traevlTrip = await _context.TripReceipts.FromSqlRaw($"select * FROM TripReceipts WHERE (PassId = {query}) and TripReceipts.PassId in (select id from passReciepts where priceID in ( SELECT id from prices where TerminalId = {user.TerminalId}))").ToListAsync();
                allTripReciepts.AddRange(traevlTrip);
            }
           
            int ticket = 0;
            int travelCard = 0;

            foreach (var trip in allTripReciepts)
            {
                if (trip.Operation == "ticket")
                {
                    ticket++;
                }
                else
                {
                    travelCard++;
                }
            }

            userToPriceVM.tripReceipts = allTripReciepts;

            return View(userToPriceVM);
        }
        
    }
}
