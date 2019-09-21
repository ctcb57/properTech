using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using properTech.Data;
using properTech.Models;

namespace properTech.Controllers
{
    public class ReportViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportViewController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Bar()
        {
            var units = _context.Unit;
            var first = new List<ReportView>();
            foreach (Unit unit in units)
            first.Add(new ReportView
            {
                DimensionOne = unit.UnitNumber.ToString(),
                Quantity = unit.SquareFootage
            });
            return View(first);
        }
        public IActionResult MonthlyRevenue()
        {
            var revenues = _context.MonthlyRevenue;
            var allRevenues = new List<ReportView>();
            foreach (MonthlyRevenue revenue in revenues)
                allRevenues.Add(new ReportView
                {
                    DimensionOne = revenue.Month,
                    Quantity = revenue.Revenue
                });
            return View(allRevenues);
        }

        public IActionResult QuarterlyEarnings()
        {
            var earnings = _context.QuarterlyEarnings;
            var allQuarters = new List<ReportView>();
            foreach (QuarterlyEarnings earning in earnings)
                allQuarters.Add(new ReportView
                {
                    DimensionOne = earning.Quarter,
                    Quantity = earning.Earnings
                });
            return View(allQuarters);
        }

        public IActionResult OccupancyPercent()
        {
            var percentages = _context.OccupancyPercent;
            var occupancies = new List<ReportView>();
            foreach (OccupancyPercent percent in percentages)
                occupancies.Add(new ReportView
                {
                    DimensionOne = percent.Month,
                    Quantity = percent.OccupancyPercentage
                });
            return View(occupancies);
        }
        public IActionResult TechEfficiency()
        {
            var techs = _context.MaintenanceTech;
            var efficiency = new List<ReportView>();
            foreach (MaintenanceTech tech in techs)
                efficiency.Add(new ReportView
                {
                    DimensionOne = tech.FirstName + tech.LastName,
                    Quantity = tech.AvgTimeSpan.Days
                }) ;
            return View(efficiency);
        }

        public IActionResult ProjectedVacancies()
        {
            var nowVacancies = _context.Unit.Count() - _context.Resident.Where(r => r.LeaseStart < DateTime.Now && r.LeaseEnd > DateTime.Now).Count();
            var thirtyDayVacancies = _context.Unit.Count() - _context.Resident.Where(r => r.LeaseStart < DateTime.Now.AddDays(30) && r.LeaseEnd > DateTime.Now.AddDays(30)).Count();
            var sixtyDayVacancies = _context.Unit.Count() - _context.Resident.Where(r => r.LeaseStart < DateTime.Now.AddDays(60) && r.LeaseEnd > DateTime.Now.AddDays(60)).Count();
            var ninetyDayVacancies = _context.Unit.Count() - _context.Resident.Where(r => r.LeaseStart < DateTime.Now.AddDays(90) && r.LeaseEnd > DateTime.Now.AddDays(90)).Count();
            var vacancies = new List<ReportView>();
            vacancies.Add(new ReportView
            {
                DimensionOne = "Current Vacancies",
                Quantity = nowVacancies
            });
            vacancies.Add(new ReportView
            {
                DimensionOne = "30 Day Vacancies",
                Quantity = thirtyDayVacancies
            });
            vacancies.Add(new ReportView
            {
                DimensionOne = "60 Day Vacancies",
                Quantity = sixtyDayVacancies
            });
            vacancies.Add(new ReportView
            {
                DimensionOne = "90 Day Vacancies",
                Quantity = ninetyDayVacancies
            });
            return View(vacancies);
        }
        }
    }