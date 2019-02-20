using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsBetting.Data;
using SportsBetting.Data.Context;
using SportsBetting.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsBetting.Tests.EventServicesTests
{
    [TestClass]
    public class AddEvent
    {
        private DbContextOptions<sportsBettingDbContext> contextOptions;

        [TestMethod]
        public async Task AddEventAsync()
        {
            // Arrange
            contextOptions = new DbContextOptionsBuilder<sportsBettingDbContext>()
                .UseInMemoryDatabase(databaseName: "AddEvent")
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
                    });

                await assertContext.SaveChangesAsync();
            }

            using (var assertContext = new sportsBettingDbContext(contextOptions))
            {
                var eventServiceMock = new EventService(assertContext);

                // Act
                var addedEvent = await eventServiceMock.AddEventAsync();

                // Assert                
                Assert.IsTrue(addedEvent.Id != 1);
                Assert.IsTrue(addedEvent.Id != 3);               
            }
        }

    }
}
