using Microsoft.EntityFrameworkCore;
using SportsBetting.Data;
using SportsBetting.Data.Context;
using SportsBetting.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsBetting.Services
{
    public class EventService : IEventService
    {
        private readonly sportsBettingDbContext context;

        public EventService(sportsBettingDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public async Task<Event> AddEventAsync()
        {
            var myDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 0);

            var newEvent = new Event
            {
                StartDate = myDate,
                IsDeleted = false,
            };

            await this.context.Events.AddAsync(newEvent);
            await this.context.SaveChangesAsync();

            return newEvent;
        }

        public async Task<Event> DeleteEventAsync(int eventId)
        {
            if (eventId <= 0)
            {
                throw new ArgumentException("Invalid Id!");
            }

            var currEvent = await this.context.Events.FirstOrDefaultAsync(e => e.Id == eventId);

            if (currEvent is null)
            {
                throw new ArgumentNullException($"Event with Id {eventId} does not exist!");
            }

            currEvent.IsDeleted = true;

            this.context.Events.Update(currEvent);
            await this.context.SaveChangesAsync();

            return currEvent;
        }

        public async Task EditEventAsync(int eventId, string eventName, double? oddsFirstTeam, double? oddsDraw, double? oddsSecondTeam, DateTime date)
        {
            if (eventId <= 0)
            {
                throw new ArgumentException("Invalid Id!");
            }

            var currEvent = await this.context.Events.FirstOrDefaultAsync(e => e.Id == eventId);

            if (currEvent is null)
            {
                throw new ArgumentNullException($"Event with Id {eventId} does not exist!");
            }

            if (string.IsNullOrEmpty(eventName) || eventName.Length < 3 || eventName.Length > 50)
            {
                throw new ArgumentException("Name must be between 3 and 50 symbols!");
            }

            if (oddsFirstTeam < 1 || oddsSecondTeam < 1 || oddsDraw < 1)
            {
                throw new ArgumentException("Each odd must be greater or equal to 1!");
            }

            currEvent.Name = eventName;
            currEvent.OddsFirstTeam = oddsFirstTeam;
            currEvent.OddsSecondTeam = oddsSecondTeam;
            currEvent.OddsDraw = oddsDraw;
            currEvent.StartDate = date;

            this.context.Events.Update(currEvent);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            return await this.context.Events
                              .Where(e => !e.IsDeleted)
                              .ToListAsync();
        }
    }
}
