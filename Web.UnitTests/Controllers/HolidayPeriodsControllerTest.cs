using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamHolidayPlanner.Domain;
using TeamHolidayPlanner.Web.Controllers;

namespace TeamHolidayPlanner.Web.UnitTests
{
    public class HolidayPeriodsControllerTest
    {
        Mock<IGenericRepository<HolidayPeriod>> holidayPeriodsRepository;
        HolidayPeriodsController controller;

        [SetUp]
        public void Setup()
        {
            holidayPeriodsRepository = new Mock<IGenericRepository<HolidayPeriod>>();
            controller = new HolidayPeriodsController(holidayPeriodsRepository.Object);
        }

        [Test]
        public async Task Index_GET_displays_list_of_all_employees()
        {
            holidayPeriodsRepository.Setup(x => x.AllAsync())
                .ReturnsAsync(GetHolidayPeriods());

            var viewResult = await controller.Index() as ViewResult;

            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as IEnumerable<HolidayPeriod>;
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

            var viewResult = await controller.Create(new HolidayPeriod()) as ViewResult;

            Assert.IsNotNull(viewResult);
        }

        [Test]
        public async Task Create_POST_on_successful_validate_creates_holidayPeriod()
        {
            var redirectToActionResult = await controller.Create(new HolidayPeriod()) as RedirectToActionResult;

            holidayPeriodsRepository.Verify(x => x.CreateAsync(It.IsAny<HolidayPeriod>()), Times.Once);
        }

        [Test]
        public async Task Create_POST_on_successful_create_redirects_to_index()
        {
            var redirectToActionResult = await controller.Create(new HolidayPeriod()) as RedirectToActionResult;

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
            var holidayPeriodId = 7;
            holidayPeriodsRepository.Setup(x => x.FindByIdAsync(holidayPeriodId))
                .ReturnsAsync(new HolidayPeriod() { HolidayPeriodID = holidayPeriodId });

            var notFoundResult = await controller.Edit(33) as NotFoundResult;

            Assert.IsNotNull(notFoundResult);
        }

        [Test]
        public async Task Edit_GET_displays_the_edit_form()
        {
            var holidayPeriodId = 7;
            holidayPeriodsRepository.Setup(x => x.FindByIdAsync(holidayPeriodId))
                .ReturnsAsync(new HolidayPeriod() { HolidayPeriodID = holidayPeriodId });

            var viewResult = await controller.Edit(holidayPeriodId) as ViewResult;

            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as HolidayPeriod;
            Assert.That(model.HolidayPeriodID, Is.EqualTo(holidayPeriodId));
        }

        [Test]
        public async Task Edit_POST_returns_NotFound_when_provided_id_does_not_exist()
        {
            var holidayPeriodId = 7;
            var updatedHolidayPeriod = new HolidayPeriod() { HolidayPeriodID = holidayPeriodId, Description = "xxx" };

            var notFoundResult = await controller.Edit(33, updatedHolidayPeriod) as NotFoundResult;

            Assert.IsNotNull(notFoundResult);
        }

        [Test]
        public async Task Edit_POST_on_validation_error_redisplays_update_form()
        {
            var holidayPeriodId = 7;
            var updatedHolidayPeriod = new HolidayPeriod() { HolidayPeriodID = holidayPeriodId, Description = "XXX" };
            controller.ModelState.AddModelError("Name", "The name is required.");

            var viewResult = await controller.Edit(holidayPeriodId, updatedHolidayPeriod) as ViewResult;

            Assert.IsNotNull(viewResult);
            holidayPeriodsRepository.Verify(x => x.UpdateAsync(It.IsAny<HolidayPeriod>()), Times.Never);
        }

        [Test]
        public async Task Edit_POST_on_successful_validation_updates_the_holidayPeriod()
        {
            var holidayPeriodId = 7;
            var updatedHolidayPeriod = new HolidayPeriod() { HolidayPeriodID = holidayPeriodId, Description = "XXX" };

            var redirectToActionResult = await controller.Edit(holidayPeriodId, updatedHolidayPeriod) as RedirectToActionResult;

            holidayPeriodsRepository.Verify(x => x.UpdateAsync(updatedHolidayPeriod), Times.Once);
        }

        [Test]
        public async Task Edit_POST_on_successful_validation_redirects_to_index()
        {
            var holidayPeriodId = 7;
            var updatedHolidayPeriod = new HolidayPeriod() { HolidayPeriodID = holidayPeriodId, Description = "XXX" };

            var redirectToActionResult = await controller.Edit(holidayPeriodId, updatedHolidayPeriod) as RedirectToActionResult;

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
            var holidayPeriodId = 7;
            holidayPeriodsRepository.Setup(x => x.FindByIdAsync(holidayPeriodId))
                .ReturnsAsync(new HolidayPeriod() { HolidayPeriodID = holidayPeriodId });

            var viewResult = await controller.Details(holidayPeriodId) as ViewResult;

            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as HolidayPeriod;
            Assert.That(model.HolidayPeriodID, Is.EqualTo(holidayPeriodId));
            holidayPeriodsRepository.Verify(x => x.FindByIdAsync(holidayPeriodId), Times.Once);
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
            var holidayPeriodId = 7;
            holidayPeriodsRepository.Setup(x => x.FindByIdAsync(holidayPeriodId))
                .ReturnsAsync(new HolidayPeriod() { HolidayPeriodID = holidayPeriodId });

            var viewResult = await controller.Delete(holidayPeriodId) as ViewResult;

            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as HolidayPeriod;
            Assert.That(holidayPeriodId, Is.EqualTo(model.HolidayPeriodID));
        }

        [Test]
        public async Task DeleteConfirmed_POST_deletes_the_holidayPeriod()
        {
            var holidayPeriodId = 7;

            await controller.DeleteConfirmed(holidayPeriodId);

            holidayPeriodsRepository.Verify(x => x.DeleteAsync(holidayPeriodId), Times.Once());
        }

        [Test]
        public async Task DeleteConfirmed_POST_on_successful_redirects_to_index()
        {
            var holidayPeriodId = 7;

            var redirectToActionResult = await controller.DeleteConfirmed(holidayPeriodId) as RedirectToActionResult;

            Assert.IsNotNull(redirectToActionResult);
            Assert.That(redirectToActionResult.ActionName, Is.EqualTo("Index"));
        }

        private IEnumerable<HolidayPeriod> GetHolidayPeriods()
        {
            return (new List<HolidayPeriod>()
            {
                new HolidayPeriod(),
                new HolidayPeriod(),
                new HolidayPeriod()
            }).AsEnumerable();
        }
    }
}
