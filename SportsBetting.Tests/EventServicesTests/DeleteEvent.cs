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
    public class DeleteEvent
    {
        private DbContextOptions<sportsBettingDbContext> contextOptions;

        [TestMethod]
        public async Task DeleteEvent_WhenArgumentIsCorrect()
        {
            // Arrange
            contextOptions = new DbContextOptionsBuilder<sportsBettingDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteEvent_WhenArgumentIsCorrect")
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
                var deletedEvent = await eventServiceMock.DeleteEventAsync(3);

                // Assert
                Assert.IsTrue(deletedEvent.IsDeleted == true);
            }
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public async Task DeleteEvent_WhenArgumentIsIncorrect(int id)
        {

            // Arrange
            var dbContextStub = new Mock<sportsBettingDbContext>();
            var eventServiceMock = new EventService(dbContextStub.Object);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await eventServiceMock.DeleteEventAsync(id));
        }

        [TestMethod]
        [DataRow(5)]
        public async Task ThrowArgumentNullEcxeption_WhenUserEventIsNotFound(int id)
        {
            // Arrange
            contextOptions = new DbContextOptionsBuilder<sportsBettingDbContext>()
                .UseInMemoryDatabase(databaseName: "ThrowArgumentNullEcxeption_WhenUserEventIsNotFound")
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

            // Act and Assert
            using (var assertContext = new sportsBettingDbContext(contextOptions))
            {
                var eventServiceMock = new EventService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await eventServiceMock.DeleteEventAsync(id));
            }
        }

    }
}
