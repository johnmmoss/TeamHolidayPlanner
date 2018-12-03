using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamHolidayPlanner.Data;
using TeamHolidayPlanner.Domain;

namespace TeamHolidayPlanner.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IGenericRepository<Employee> employeeRepository;
        private readonly IGenericRepository<Department> departmentRepository;
        private readonly IGenericRepository<EmploymentGrade> employmentGradeRepository;

        public EmployeesController( 
            IGenericRepository<Employee> employeeRepository,
            IGenericRepository<Department> departmentRepository, 
            IGenericRepository<EmploymentGrade> employmentGradeRepository)
        {
            this.employeeRepository = employeeRepository;
            this.departmentRepository = departmentRepository;
            this.employmentGradeRepository = employmentGradeRepository;
        }

        // GET: Employees
        [Authorize(Policy = "EmployeeIndex")]
        public async Task<IActionResult> Index()
        {
            var results = await employeeRepository.AllAsync(c => c.Department, c => c.EmploymentGrade);
            return View(results);
        }

        // GET: Employees/Details/5
        [Authorize(Policy = "EmployeeDetails")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await employeeRepository.FindByIdAsync(id.Value, c => c.Department, c => c.EmploymentGrade);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Policy = "EmployeeCreate")]
        public async Task<IActionResult> Create()
        {
            var user = User.Claims;

            await SetSelectLists(); 

            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EmployeeCreate")]
        public async Task<IActionResult> Create([Bind("EmployeeID,JobTitle,BirthDate,Title,FirstName,MiddleName,LastName,MaritalStatus,Gender,HireDate,EmploymentGradeID,DepartmentID,CreatedDate,ModifiedDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                await employeeRepository.CreateAsync(employee); 
                return RedirectToAction(nameof(Index));
            }

            await SetSelectLists();

            return View(employee);
        }

        // GET: Employees/Edit/5
        [Authorize(Policy = "EmployeeEdit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await employeeRepository.FindByIdAsync(id.Value, c=>c.Department, c=>c.EmploymentGrade);
            if (employee == null)
            {
                return NotFound();
            }

            await SetSelectLists(employee.DepartmentID, employee.EmploymentGradeID);

            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EmployeeEdit")]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeID,JobTitle,BirthDate,Title,FirstName,MiddleName,LastName,MaritalStatus,Gender,HireDate,EmploymentGradeID,DepartmentID,CreatedDate,ModifiedDate")] Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await employeeRepository.UpdateAsync(employee);
               
                return RedirectToAction(nameof(Index));
            }

            await SetSelectLists(employee.DepartmentID, employee.EmploymentGradeID);

            return View(employee);
        }

        // GET: Employees/Delete/5
        [Authorize(Policy = "EmployeeDelete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await employeeRepository.FindByIdAsync(id.Value, c=>c.Department, c=>c.EmploymentGrade);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: EmploymentGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EmployeeDelete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await employeeRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task SetSelectLists()
        {
            await SetSelectLists(0, 0);
        }

        private async Task SetSelectLists(int selectedDepartmentID, int selectedEmploymentGradeID)
        {
            var departments = await departmentRepository.AllAsync();
            var departmentsSelectListItems = departments.Select(x => new SelectListItem() { Text = x.Name, Value = x.DepartmentID.ToString() }).ToList();
            departmentsSelectListItems.Insert(0, new SelectListItem() { Text = "-- select --", Value = "0" });
            ViewData["DepartmentID"] = new SelectList(departmentsSelectListItems, "Value", "Text", selectedDepartmentID);

            var employmentGrades = await employmentGradeRepository.AllAsync();
            var employementGradesSelectListItems = employmentGrades.Select(x => new SelectListItem() { Text = x.Name, Value = x.EmploymentGradeID.ToString() }).ToList();
            employementGradesSelectListItems.Insert(0, new SelectListItem() { Text = "-- select --", Value = "0" });
            ViewData["EmploymentGradeID"] = new SelectList(employementGradesSelectListItems, "Value", "Text", selectedEmploymentGradeID);
        }
    }
}
