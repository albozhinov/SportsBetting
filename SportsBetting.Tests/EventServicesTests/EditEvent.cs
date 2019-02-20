using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
    public class EditEvent
    {
        private DbContextOptions<sportsBettingDbContext> contextOptions;

        [TestMethod]
        [DataRow(1, "Botev Vraca-Dunav Ruse", 2, 5, 3)]
        public async Task EditEventAsync_WhemParametersAreCorrect(int eventId, string eventName, double oddsFirstTeam, double oddsDraw, double oddsSecondTeam)
        {
            // Arrange
            contextOptions = new DbContextOptionsBuilder<sportsBettingDbContext>()
                .UseInMemoryDatabase(databaseName: "EditEventAsync_WhemParametersAreCorrect")
                .Options;

            using (var assertContext = new sportsBettingDbContext(contextOptions))
            {
                await assertContext.Events.AddAsync(
                    new Event
                    {
                        Id = 1,
                        Name = "Barca - Real",
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
                await eventServiceMock.EditEventAsync(eventId, eventName, oddsFirstTeam, oddsDraw, oddsSecondTeam, DateTime.Now);

                var editedEvent = await assertContext.Events.FirstOrDefaultAsync(e => e.Id == eventId);

                // Assert                
                Assert.IsTrue(editedEvent.Id == eventId);
                Assert.IsTrue(editedEvent.Name == eventName);
                Assert.IsTrue(editedEvent.OddsFirstTeam == oddsFirstTeam);
                Assert.IsTrue(editedEvent.OddsDraw == oddsDraw);
                Assert.IsTrue(editedEvent.OddsSecondTeam == oddsSecondTeam);
            }
        }



        [TestMethod]
        [DataRow(0, "Botev Vraca-Dunav Ruse", 2, 5, 3)]
        [DataRow(-1, "Botev Vraca-Dunav Ruse", 2, 5, 3)]
        public async Task ThrowArgumentException_WhenArgumentsAreIncorrect(int eventId, string eventName, double oddsFirstTeam, double oddsDraw, double oddsSecondTeam)
        {
            // Arrange
            var dbContextStub = new Mock<sportsBettingDbContext>();
            var eventServiceMock = new EventService(dbContextStub.Object);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await eventServiceMock.EditEventAsync(eventId, eventName, oddsFirstTeam, oddsDraw, oddsSecondTeam, DateTime.Now));
        }   
    }
}
