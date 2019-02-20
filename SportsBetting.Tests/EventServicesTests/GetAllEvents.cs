using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsBetting.Data;
using SportsBetting.Data.Context;
using SportsBetting.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SportsBetting.Tests
{
    [TestClass]
    public class GetAllEvents
    {
        private DbContextOptions<sportsBettingDbContext> contextOptions;

        [TestMethod]
        public async Task ReturnAllEvents()
        {
            // Arrange
            contextOptions = new DbContextOptionsBuilder<sportsBettingDbContext>()
                .UseInMemoryDatabase(databaseName: "ReturnAllEvents")
                .Options;

            using (var assertContext = new sportsBettingDbContext(contextOptions))
            {
                await assertContext.Events.AddRangeAsync(
                    new Event
                    {
                        Id = 1,
                        Name = "Barca - Real",
                        OddsFirstTeam = 1.55,
                        OddsDraw = 3.00,
                        OddsSecondTeam = 2.00,
                        StartDate = DateTime.Now,
                        IsDeleted = false
                    },
                    new Event
                    {
                        Id = 3,
                        Name = "Botev Vraca - CSKA",
                        OddsFirstTeam = 1.55,
                        OddsDraw = 3.00,
                        OddsSecondTeam = 2.00,
                        StartDate = DateTime.Now,
                        IsDeleted = false
                    },
                    new Event
                    {
                        Id = 2,
                        Name = "Levski - Ludogorec",
                        OddsFirstTeam = 1.55,
                        OddsDraw = 3.00,
                        OddsSecondTeam = 2.00,
                        StartDate = DateTime.Now,
                        IsDeleted = true
                    });                   

                await assertContext.SaveChangesAsync();
            }

            using (var assertContext = new sportsBettingDbContext(contextOptions))
            {
                var eventServiceMock = new EventService(assertContext);

                // Act
                var allEvents = await eventServiceMock.GetEventsAsync();
                var events = allEvents.ToList();

                // Assert
                Assert.IsTrue(events.Count == 2);
            }
        }
    }
}
