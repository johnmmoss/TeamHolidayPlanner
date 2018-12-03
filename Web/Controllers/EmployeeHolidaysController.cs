using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamHolidayPlanner.Data;
using TeamHolidayPlanner.Domain;

namespace TeamHolidayPlanner.Web.Controllers
{
    public class EmployeeHolidaysController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeeHolidaysController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: EmployeeHolidays
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.EmployeeHoliday.Include(e => e.Employee).Include(e => e.HolidayPeriod);
            return View(await employeeContext.ToListAsync());
        }

        // GET: EmployeeHolidays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeHoliday = await _context.EmployeeHoliday
                .Include(e => e.Employee)
                .Include(e => e.HolidayPeriod)
                .FirstOrDefaultAsync(m => m.EmployeeHolidayId == id);
            if (employeeHoliday == null)
            {
                return NotFound();
            }

            return View(employeeHoliday);
        }

        // GET: EmployeeHolidays/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "FirstName");
            ViewData["HolidayPeriodID"] = new SelectList(_context.HolidayPeriod, "HolidayPeriodID", "Description");
            return View();
        }

        // POST: EmployeeHolidays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeHolidayId,EmployeeID,HolidayPeriodID,StartDate,EndDate,Authorised,AuthorisedDate,AuthorisedBy")] EmployeeHoliday employeeHoliday)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeHoliday);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "FirstName", employeeHoliday.EmployeeID);
            ViewData["HolidayPeriodID"] = new SelectList(_context.HolidayPeriod, "HolidayPeriodID", "Description", employeeHoliday.HolidayPeriodID);
            return View(employeeHoliday);
        }

        // GET: EmployeeHolidays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeHoliday = await _context.EmployeeHoliday.FindAsync(id);
            if (employeeHoliday == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "FirstName", employeeHoliday.EmployeeID);
            ViewData["HolidayPeriodID"] = new SelectList(_context.HolidayPeriod, "HolidayPeriodID", "Description", employeeHoliday.HolidayPeriodID);
            return View(employeeHoliday);
        }

        // POST: EmployeeHolidays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeHolidayId,EmployeeID,HolidayPeriodID,StartDate,EndDate,Authorised,AuthorisedDate,AuthorisedBy")] EmployeeHoliday employeeHoliday)
        {
            if (id != employeeHoliday.EmployeeHolidayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeHoliday);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeHolidayExists(employeeHoliday.EmployeeHolidayId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "FirstName", employeeHoliday.EmployeeID);
            ViewData["HolidayPeriodID"] = new SelectList(_context.HolidayPeriod, "HolidayPeriodID", "Description", employeeHoliday.HolidayPeriodID);
            return View(employeeHoliday);
        }

        // GET: EmployeeHolidays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeHoliday = await _context.EmployeeHoliday
                .Include(e => e.Employee)
                .Include(e => e.HolidayPeriod)
                .FirstOrDefaultAsync(m => m.EmployeeHolidayId == id);
            if (employeeHoliday == null)
            {
                return NotFound();
            }

            return View(employeeHoliday);
        }

        // POST: EmployeeHolidays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeHoliday = await _context.EmployeeHoliday.FindAsync(id);
            _context.EmployeeHoliday.Remove(employeeHoliday);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeHolidayExists(int id)
        {
            return _context.EmployeeHoliday.Any(e => e.EmployeeHolidayId == id);
        }
    }
}
