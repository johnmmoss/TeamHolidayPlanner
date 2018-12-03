using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamHolidayPlanner.Domain;

namespace TeamHolidayPlanner.Web.Controllers
{
    public class HolidayPeriodsController : Controller
    {
        private readonly IGenericRepository<HolidayPeriod> holidayPeriodsRepository;

        public HolidayPeriodsController(IGenericRepository<HolidayPeriod> holidayPeriodsRepository)
        {
            this.holidayPeriodsRepository = holidayPeriodsRepository;
        }

        // GET: HolidayPeriods
        [Authorize(Policy = "HolidayPeriodIndex")]
        public async Task<ActionResult> Index()
        {
            var holidayPeriods = await holidayPeriodsRepository.AllAsync();

            return View(holidayPeriods);
        }

        // GET: HolidayPeriods/Details/5
        [Authorize(Policy = "HolidayPeriodDetails")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            HolidayPeriod holidayPeriod = await holidayPeriodsRepository.FindByIdAsync(id.Value);
            if (holidayPeriod == null)
            {
                return NotFound();
            }
            return View(holidayPeriod);
        }

        // GET: HolidayPeriods/Create
        [Authorize(Policy = "HolidayPeriodCreate")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: HolidayPeriods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "HolidayPeriodCreate")]
        public async Task<IActionResult> Create([Bind("HolidayPeriodID,Description,StartDate,EndDate,CreatedDate,ModifiedDate")] HolidayPeriod holidayPeriod)
        {
            if (ModelState.IsValid)
            {
                await holidayPeriodsRepository.CreateAsync(holidayPeriod);
                return RedirectToAction("Index");
            }
            return View(holidayPeriod);
        }

        // GET: HolidayPeriods/Edit/5
        [Authorize(Policy = "HolidayPeriodEdit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            HolidayPeriod holidayPeriod = await holidayPeriodsRepository.FindByIdAsync(id.Value);
            if (holidayPeriod == null)
            {
                return NotFound();
            }
            return View(holidayPeriod);
        }

        // POST: HolidayPeriods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "HolidayPeriodEdit")]
        public async Task<IActionResult> Edit(int id, [Bind("HolidayPeriodID,Description,StartDate,EndDate,CreatedDate,ModifiedDate")] HolidayPeriod holidayPeriod)
        {
            if (id != holidayPeriod.HolidayPeriodID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await holidayPeriodsRepository.UpdateAsync(holidayPeriod);

                return RedirectToAction(nameof(Index));
            }
            return View(holidayPeriod);
        }

        // GET: HolidayPeriods/Delete/5
        [Authorize(Policy = "HolidayPeriodDelete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HolidayPeriod holidayPeriod = await holidayPeriodsRepository.FindByIdAsync(id.Value);
            if (holidayPeriod == null)
            {
                return NotFound();
            }

            return View(holidayPeriod);
        }

        // POST: HolidayPeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "HolidayPeriodDelete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await holidayPeriodsRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
