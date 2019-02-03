using Moq;
using TeamHolidayPlanner.Domain;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TeamHolidayPlanner.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace TeamHolidayPlanner.Web.UnitTests
{
    public class DepartmentsControllerTest
    {
        DepartmentsController controller;

        Mock<ILogger<DepartmentsController>> logger;
        Mock<IGenericRepository<Department>> departmentsRepository;
        Mock<IAuthorizationService> authorizationService;
        Mock<IModelEntityBuilder> modelEntityBuilder;

        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILogger<DepartmentsController>>();
            authorizationService = new Mock<IAuthorizationService>();
            departmentsRepository = new Mock<IGenericRepository<Department>>();
            modelEntityBuilder = new Mock<IModelEntityBuilder>();

            controller = new DepartmentsController(
                logger.Object,
                authorizationService.Object,
                departmentsRepository.Object,
                modelEntityBuilder.Object);
        }

        [Test]
        public async Task Index_GET_displays_list_of_all_employees()
        {
            departmentsRepository.Setup(x => x.AllAsync())
                .ReturnsAsync(GetDepartments());

            var viewResult = await controller.Index() as ViewResult;

            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as IEnumerable<Department>;
            Assert.That(model.Count, Is.EqualTo(3));
        }

        [Test]
        public void Create_GET_displays_the_create_form()
        {
            var viewResult = controller.Create() as ViewResult;

            Assert.IsNotNull(viewResult);
        }

        [Test]
        public async Task Create_POST_on_validation_error_redisplays_create_form()
        {
            controller.ModelState.AddModelError("Name", "The name is required");

            var viewResult = await controller.Create(new Department()) as ViewResult;

            Assert.IsNotNull(viewResult);
        }

        [Test]
        public async Task Create_POST_on_successful_validate_creates_department()
        {
            var redirectToActionResult = await controller.Create(new Department()) as RedirectToActionResult;

            departmentsRepository.Verify(x => x.CreateAsync(It.IsAny<Department>()), Times.Once);
        }

        [Test]
        public async Task Create_POST_on_successful_create_redirects_to_index()
        {
            var redirectToActionResult = await controller.Create(new Department()) as RedirectToActionResult;

            Assert.IsNotNull(redirectToActionResult);
            Assert.That(redirectToActionResult.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task Edit_GET_returns_BadRequest_when_null_id()
        {
            var badRequestResult = await controller.Edit(null) as BadRequestResult;

            Assert.IsNotNull(badRequestResult);
        }

        [Test]
        public async Task Edit_GET_returns_NotFound_when_provided_id_does_not_exist()
        {
            var departmentId = 7;
            departmentsRepository.Setup(x => x.FindByIdAsync(departmentId))
                .ReturnsAsync(new Department() { DepartmentID = departmentId });

            var notFoundResult = await controller.Edit(33) as NotFoundResult;

            Assert.IsNotNull(notFoundResult);
        }

        [Test]
        public async Task Edit_GET_displays_the_edit_form()
        {
            var departmentId = 7;
            departmentsRepository.Setup(x => x.FindByIdAsync(departmentId))
                .ReturnsAsync(new Department() { DepartmentID = departmentId });

            var viewResult = await controller.Edit(departmentId) as ViewResult;

            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as Department;
            Assert.That(model.DepartmentID, Is.EqualTo(departmentId));
        }

        [Test]
        public async Task Edit_POST_returns_NotFound_when_provided_id_does_not_exist()
        {
            var departmentId = 7;
            var updatedDepartment = new Department() { DepartmentID = departmentId, GroupName = "updated" };

            var notFoundResult = await controller.Edit(33, updatedDepartment) as NotFoundResult;

            Assert.IsNotNull(notFoundResult);
        }

        [Test]
        public async Task Edit_POST_on_validation_error_redisplays_update_form()
        {
            var departmentId = 7;
            var updatedDepartment = new Department() { DepartmentID = departmentId, GroupName = "updated" };
            controller.ModelState.AddModelError("Name", "The name is required.");

            var viewResult = await controller.Edit(departmentId, updatedDepartment) as ViewResult;

            Assert.IsNotNull(viewResult);
            departmentsRepository.Verify(x => x.UpdateAsync(It.IsAny<Department>()), Times.Never);
        }

        [Test]
        public async Task Edit_POST_on_successful_validation_updates_the_department()
        {
            var departmentId = 7;
            var updatedDepartment = new Department() { DepartmentID = departmentId, GroupName = "updated" };

            var redirectToActionResult = await controller.Edit(departmentId, updatedDepartment) as RedirectToActionResult;

            departmentsRepository.Verify(x => x.UpdateAsync(updatedDepartment), Times.Once);
        }

        [Test]
        public async Task Edit_POST_on_successful_validation_redirects_to_index()
        {
            var departmentId = 7;
            var updatedDepartment = new Department() { DepartmentID = departmentId, GroupName = "updated" };

            var redirectToActionResult = await controller.Edit(departmentId, updatedDepartment) as RedirectToActionResult;

            Assert.IsNotNull(redirectToActionResult);
            Assert.That(redirectToActionResult.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task Details_GET_returns_BadRequest_when_provided_id_is_null()
        {
            var badRequestResult = await controller.Details(null) as BadRequestResult;

            Assert.IsNotNull(badRequestResult);
        }

        [Test]
        public async Task Details_GET_returns_NotFound_when_provided_id_does_not_exist()
        {
            var notFoundResult = await controller.Details(33) as NotFoundResult;

            Assert.IsNotNull(notFoundResult);
        }

        [Test]
        public async Task Details_GET_when_valid_id_provided_displays_the_details_view()
        {
            var departmentId = 7;
            departmentsRepository.Setup(x => x.FindByIdAsync(departmentId))
                .ReturnsAsync(new Department() { DepartmentID = departmentId });

            var viewResult = await controller.Details(departmentId) as ViewResult;

            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as Department;
            Assert.That(model.DepartmentID, Is.EqualTo(departmentId));
            departmentsRepository.Verify(x => x.FindByIdAsync(departmentId), Times.Once);
        }

        [Test]
        public async Task Delete_GET_returns_NotFound_when_provided_id_is_null()
        {
            var notFoundResult = await controller.Delete(null) as NotFoundResult;

            Assert.IsNotNull(notFoundResult);
        }

        [Test]
        public async Task Delete_GET_returns_NotFound_when_provided_id_does_not_exist()
        {
            var notFoundResult = await controller.Delete(33) as NotFoundResult;

            Assert.IsNotNull(notFoundResult);
        }

        [Test]
        public async Task Delete_GET_displays_the_delete_form()
        {
            var departmentId = 7;
            departmentsRepository.Setup(x => x.FindByIdAsync(departmentId))
                .ReturnsAsync(new Department() { DepartmentID = departmentId });

            var viewResult = await controller.Delete(departmentId) as ViewResult;

            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as Department;
            Assert.That(departmentId, Is.EqualTo(model.DepartmentID));
        }

        [Test]
        public async Task DeleteConfirmed_POST_deletes_the_department()
        {
            var departmentId = 7;

            await controller.DeleteConfirmed(departmentId);

            departmentsRepository.Verify(x => x.DeleteAsync(departmentId), Times.Once());
        }

        [Test]
        public async Task DeleteConfirmed_POST_on_successful_redirects_to_index()
        {
            var departmentId = 7;

            var redirectToActionResult = await controller.DeleteConfirmed(departmentId) as RedirectToActionResult;

            Assert.IsNotNull(redirectToActionResult);
            Assert.That(redirectToActionResult.ActionName, Is.EqualTo("Index"));
        }

        private IEnumerable<Department> GetDepartments()
        {
            return (new List<Department>()
            {
                new Department(),
                new Department(),
                new Department()
            }).AsEnumerable();
        }
    }
}
