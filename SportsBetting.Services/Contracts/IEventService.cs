using SportsBetting.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsBetting.Services.Contracts
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetEventsAsync();

        Task EditEventAsync(int eventId, string eventName, double? oddsFirstTeam, double? oddsDraw, double? oddsSecondTeam, DateTime date);

        Task<Event> AddEventAsync();

        Task<Event> DeleteEventAsync(int eventId);
    }
}
