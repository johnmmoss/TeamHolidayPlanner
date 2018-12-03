using Moq;
using TeamHolidayPlanner.Web.Controllers;
using TeamHolidayPlanner.Domain;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace TeamHolidayPlanner.Web.UnitTests
{
    public class EmploymentGradesControllerTest
    {
        Mock<IGenericRepository<EmploymentGrade>> employmentGradesRepository;
        EmploymentGradesController controller;

        [SetUp]
        public void Setup()
        {
            employmentGradesRepository = new Mock<IGenericRepository<EmploymentGrade>>();
            controller = new EmploymentGradesController(employmentGradesRepository.Object);
        }

        [Test]
        public async Task Index_GET_displays_list_of_all_employees()
        {
            employmentGradesRepository.Setup(x => x.AllAsync())
                .ReturnsAsync(GetEmploymentGrades());

            var viewResult = await controller.Index() as ViewResult;

            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as IEnumerable<EmploymentGrade>;
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

            var viewResult = await controller.Create(new EmploymentGrade()) as ViewResult;

            Assert.IsNotNull(viewResult);
        }

        [Test]
        public async Task Create_POST_on_successful_validate_creates_employmentGrade()
        {
            var redirectToActionResult = await controller.Create(new EmploymentGrade()) as RedirectToActionResult;

            employmentGradesRepository.Verify(x => x.CreateAsync(It.IsAny<EmploymentGrade>()), Times.Once);
        }

        [Test]
        public async Task Create_POST_on_successful_create_redirects_to_index()
        {
            var redirectToActionResult = await controller.Create(new EmploymentGrade()) as RedirectToActionResult;

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
            var employmentGradeId = 7;
            employmentGradesRepository.Setup(x => x.FindByIdAsync(employmentGradeId))
                .ReturnsAsync(new EmploymentGrade() { EmploymentGradeID = employmentGradeId });

            var notFoundResult = await controller.Edit(33) as NotFoundResult;

            Assert.IsNotNull(notFoundResult);
        }

        [Test]
        public async Task Edit_GET_displays_the_edit_form()
        {
            var employmentGradeId = 7;
            employmentGradesRepository.Setup(x => x.FindByIdAsync(employmentGradeId))
                .ReturnsAsync(new EmploymentGrade() { EmploymentGradeID = employmentGradeId });

            var viewResult = await controller.Edit(employmentGradeId) as ViewResult;

            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as EmploymentGrade;
            Assert.That(model.EmploymentGradeID, Is.EqualTo(employmentGradeId));
        }

        [Test]
        public async Task Edit_POST_returns_NotFound_when_provided_id_does_not_exist()
        {
            var employmentGradeId = 7;
            var updatedEmploymentGrade = new EmploymentGrade() { EmploymentGradeID = employmentGradeId, Name = "updated" };

            var notFoundResult = await controller.Edit(33, updatedEmploymentGrade) as NotFoundResult;

            Assert.IsNotNull(notFoundResult);
        }

        [Test]
        public async Task Edit_POST_on_validation_error_redisplays_update_form()
        {
            var employmentGradeId = 7;
            var updatedEmploymentGrade = new EmploymentGrade() { EmploymentGradeID = employmentGradeId, Name = "updated" };
            controller.ModelState.AddModelError("Name", "The name is required.");

            var viewResult = await controller.Edit(employmentGradeId, updatedEmploymentGrade) as ViewResult;

            Assert.IsNotNull(viewResult);
            employmentGradesRepository.Verify(x => x.UpdateAsync(It.IsAny<EmploymentGrade>()), Times.Never);
        }

        [Test]
        public async Task Edit_POST_on_successful_validation_updates_the_employmentGrade()
        {
            var employmentGradeId = 7;
            var updatedEmploymentGrade = new EmploymentGrade() { EmploymentGradeID = employmentGradeId, Name = "updated" };

            var redirectToActionResult = await controller.Edit(employmentGradeId, updatedEmploymentGrade) as RedirectToActionResult;

            employmentGradesRepository.Verify(x => x.UpdateAsync(updatedEmploymentGrade), Times.Once);
        }

        [Test]
        public async Task Edit_POST_on_successful_validation_redirects_to_index()
        {
            var employmentGradeId = 7;
            var updatedEmploymentGrade = new EmploymentGrade() { EmploymentGradeID = employmentGradeId, Name = "updated" };

            var redirectToActionResult = await controller.Edit(employmentGradeId, updatedEmploymentGrade) as RedirectToActionResult;

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
            var employmentGradeId = 7;
            employmentGradesRepository.Setup(x => x.FindByIdAsync(employmentGradeId))
                .ReturnsAsync(new EmploymentGrade() { EmploymentGradeID = employmentGradeId });

            var viewResult = await controller.Details(employmentGradeId) as ViewResult;

            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as EmploymentGrade;
            Assert.That(model.EmploymentGradeID, Is.EqualTo(employmentGradeId));
            employmentGradesRepository.Verify(x => x.FindByIdAsync(employmentGradeId), Times.Once);
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
            var employmentGradeId = 7;
            employmentGradesRepository.Setup(x => x.FindByIdAsync(employmentGradeId))
                .ReturnsAsync(new EmploymentGrade() { EmploymentGradeID = employmentGradeId });

            var viewResult = await controller.Delete(employmentGradeId) as ViewResult;

            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as EmploymentGrade;
            Assert.That(employmentGradeId, Is.EqualTo(model.EmploymentGradeID));
        }

        [Test]
        public async Task DeleteConfirmed_POST_deletes_the_employmentGrade()
        {
            var employmentGradeId = 7;

            await controller.DeleteConfirmed(employmentGradeId);

            employmentGradesRepository.Verify(x => x.DeleteAsync(employmentGradeId), Times.Once());
        }

        [Test]
        public async Task DeleteConfirmed_POST_on_successful_redirects_to_index()
        {
            var employmentGradeId = 7;

            var redirectToActionResult = await controller.DeleteConfirmed(employmentGradeId) as RedirectToActionResult;

            Assert.IsNotNull(redirectToActionResult);
            Assert.That(redirectToActionResult.ActionName, Is.EqualTo("Index"));
        }

        private IEnumerable<EmploymentGrade> GetEmploymentGrades()
        {
            return (new List<EmploymentGrade>()
            {
                new EmploymentGrade(),
                new EmploymentGrade(),
                new EmploymentGrade()
            }).AsEnumerable();
        }
    }
}
