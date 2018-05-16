using CattleCompanion.Core;
using CattleCompanion.Core.Models;
using CattleCompanion.Persistence.Repositories;
using CattleCompanion.Tests.Extensions;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Data.Entity;

namespace CattleCompanion.Tests.Persistence.Repositories
{
    [TestFixture]
    public class UserFarmRepositoryTests
    {
        private UserFarmRepository _repository;
        private Mock<DbSet<UserFarm>> _mockUserFarms;

        [SetUp]
        public void SetUp()
        {
            _mockUserFarms = new Mock<DbSet<UserFarm>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.UserFarms).Returns(_mockUserFarms.Object);

            _repository = new UserFarmRepository(mockContext.Object);
        }

        [Test]
        public void GetAllByFarmId_UserFarmWithOtherId_ShouldNotBeReturned()
        {
            var userFarm = new UserFarm { FarmId = 1 };

            _mockUserFarms.SetSource(new[] { userFarm });

            var userFarmFromDb = _repository.GetAllByFarmId(2);

            userFarmFromDb.Should().BeEmpty();
        }

        [Test]
        public void GetAllByFarmId_UserFarmWithId_UserFarmShouldBeReturned()
        {
            var userFarm = new UserFarm { FarmId = 1 };

            _mockUserFarms.SetSource(new[] { userFarm });

            var userFarmFromDb = _repository.GetAllByFarmId(userFarm.FarmId);

            userFarmFromDb.Should().Contain(userFarm);
        }

        [Test]
        public void GetUserFarm_DifferentFarmId_ShouldNotBeReturned()
        {
            var userFarm = new UserFarm { FarmId = 1, UserId = "1" };

            _mockUserFarms.SetSource(new[] { userFarm });

            var userFarmFromDb = _repository.GetUserFarm(2, "1");

            userFarmFromDb.Should().BeNull();
        }

        [Test]
        public void GetUserFarm_DifferentUserId_ShouldNotBeReturned()
        {
            var userFarm = new UserFarm { FarmId = 1, UserId = "1" };

            _mockUserFarms.SetSource(new[] { userFarm });

            var userFarmFromDb = _repository.GetUserFarm(1, "2");

            userFarmFromDb.Should().BeNull();
        }

        [Test]
        public void GetUserFarm_CorrectUserIdAndFarmId_ShouldBeReturned()
        {
            var userFarm = new UserFarm { FarmId = 1, UserId = "1" };

            _mockUserFarms.SetSource(new[] { userFarm });

            var userFarmFromDb = _repository.GetUserFarm(userFarm.FarmId, userFarm.UserId);

            userFarmFromDb.Should().BeOfType<UserFarm>();
        }
    }
}
