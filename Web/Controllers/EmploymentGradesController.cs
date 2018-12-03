using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamHolidayPlanner.Domain;

namespace TeamHolidayPlanner.Web.Controllers
{
    public class EmploymentGradesController : Controller
    {
        private readonly IGenericRepository<EmploymentGrade> repository;

        public EmploymentGradesController(IGenericRepository<EmploymentGrade> repository)
        {
            this.repository = repository;
        }

        // GET: EmploymentGrades
        [Authorize(Policy = "EmploymentGradeIndex")]
        public async Task<IActionResult> Index()
        {
            return View(await repository.AllAsync());
        }

        // GET: EmploymentGrades/Details/5
        [Authorize(Policy = "EmploymentGradeDetails")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var employmentGrade = await repository.FindByIdAsync(id.Value);
            if (employmentGrade == null)
            {
                return NotFound();
            }

            return View(employmentGrade);
        }

        // GET: EmploymentGrades/Create
        [Authorize(Policy = "EmploymentGradeCreate")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmploymentGrades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EmploymentGradeCreate")]
        public async Task<IActionResult> Create([Bind("EmploymentGradeID,Name,Grade,AnnualLeaveEntitlement")] EmploymentGrade employmentGrade)
        {
            if (ModelState.IsValid)
            {
                await repository.CreateAsync(employmentGrade);
                return RedirectToAction(nameof(Index));
            }
            return View(employmentGrade);
        }

        // GET: EmploymentGrades/Edit/5
        [Authorize(Policy = "EmploymentGradeEdit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var employmentGrade = await repository.FindByIdAsync(id.Value);
            if (employmentGrade == null)
            {
                return NotFound();
            }
            return View(employmentGrade);
        }

        // POST: EmploymentGrades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EmploymentGradeEdit")]
        public async Task<IActionResult> Edit(int id, [Bind("EmploymentGradeID,Name,Grade,AnnualLeaveEntitlement")] EmploymentGrade employmentGrade)
        {
            if (id != employmentGrade.EmploymentGradeID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await repository.UpdateAsync(employmentGrade);
               
                return RedirectToAction(nameof(Index));
            }
            return View(employmentGrade);
        }

        // GET: EmploymentGrades/Delete/5
        [Authorize(Policy = "EmploymentGradeDelete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employmentGrade = await repository.FindByIdAsync(id.Value);
            if (employmentGrade == null)
            {
                return NotFound();
            }
            return View(employmentGrade);
        }

        // POST: EmploymentGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EmploymentGradeDelete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
