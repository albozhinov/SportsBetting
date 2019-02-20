using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsBetting.Services.Contracts;
using SportsBetting.Web.Models;

namespace SportsBetting.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        }

        [HttpGet]
        public async Task<IActionResult> Preview()
        {
            var allEvents = await this.eventService.GetEventsAsync();

            var eventsViewModel = new AllEventViewModel() { PageMode = "Preview" };

            eventsViewModel.Events = allEvents.Select(e => new EventViewModel()
            {
                Name = e.Name,
                OddsFirstTeam = e.OddsFirstTeam,
                OddsSecondTeam = e.OddsSecondTeam,
                OddsDraw = e.OddsDraw,
                StartDate = e.StartDate
            });

            return View(eventsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var allEvents = await this.eventService.GetEventsAsync();

            var eventsViewModel = new AllEventViewModel() { PageMode = "Edit"};

            eventsViewModel.Events = allEvents.Select(e => new EventViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                OddsFirstTeam = e.OddsFirstTeam,
                OddsSecondTeam = e.OddsSecondTeam,
                OddsDraw = e.OddsDraw,
                StartDate = e.StartDate
            });

            return View(eventsViewModel);
        }

        [HttpGet]
        public IActionResult EditEvent(EventViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await this.eventService.DeleteEventAsync(Id);

                return RedirectToAction("Edit");
            }
            catch (Exception)
            {
                // exception will be save on file

                return RedirectToAction("Edit");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add()
        {
            await this.eventService.AddEventAsync();

            return RedirectToAction("Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(EventViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit");
            }

            try
            {
                await this.eventService.EditEventAsync(viewModel.Id, viewModel.Name, viewModel.OddsFirstTeam, viewModel.OddsDraw, viewModel.OddsSecondTeam, viewModel.StartDate);

                return RedirectToAction("Edit");
            }
            catch (Exception)
            {
                // exception will be save on file

                return RedirectToAction("Edit");
            }
        }
    }
}