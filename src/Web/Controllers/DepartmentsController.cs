using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TeamHolidayPlanner.Domain;
using TeamHolidayPlanner.Web.Models;

namespace TeamHolidayPlanner.Web.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IAuthorizationService authorizationService;
        private readonly IModelEntityBuilder modelEntityBuilder;
        private readonly ILogger<DepartmentsController> logger;
        private readonly IGenericRepository<Department> departmentsRepository;

        public DepartmentsController(
            ILogger<DepartmentsController> logger,
            IAuthorizationService authorizationService,
            IGenericRepository<Department> departmentsRepository,
            IModelEntityBuilder modelEntityBuilder)
        {
            this.logger = logger;
            this.departmentsRepository = departmentsRepository;
            this.authorizationService = authorizationService;
            this.modelEntityBuilder = modelEntityBuilder;
        }

        public async Task<ActionResult> Index()
        {
            logger.LogDebug("Executing display departments list page");

            var authorized = await authorizationService.AuthorizeAsync(User, "DepartmentIndex");

            if (!authorized.Succeeded)
            {
                logger.LogInformation("Unauthorized access attempt of departments list page");

                return Challenge();
            }

            logger.LogDebug("Loading all departments from database");

            var entities = await departmentsRepository.AllAsync();

            var model = modelEntityBuilder.BuildModelList<DepartmentModel, Department>(entities);

            logger.LogInformation("Executed display departments list page");

            return View(model);
        }

        // GET: Departments/Details/5
        [Authorize(Policy = "DepartmentDetails")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Department department = await departmentsRepository.FindByIdAsync(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        [Authorize(Policy = "DepartmentCreate")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DepartmentCreate")]
        public async Task<IActionResult> Create([Bind("DepartmentID,Name,GroupName,CreatedDate,ModifiedDate")] Department department)
        {
            if (ModelState.IsValid)
            {
                await departmentsRepository.CreateAsync(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        [Authorize(Policy = "DepartmentEdit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Department department = await departmentsRepository.FindByIdAsync(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DepartmentEdit")]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentID,Name,GroupName,CreatedDate,ModifiedDate")] Department department)
        {
            if (id != department.DepartmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await departmentsRepository.UpdateAsync(department);
               
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        [Authorize(Policy = "DepartmentDelete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Department department = await departmentsRepository.FindByIdAsync(id.Value);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DepartmentDelete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await departmentsRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
