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
    public class FarmRepositoryTests
    {
        private FarmRepository _repository;
        private Mock<DbSet<Farm>> _mockFarms;
        private Mock<DbSet<UserFarm>> _mockUserFarms;

        [SetUp]
        public void SetUp()
        {
            _mockFarms = new Mock<DbSet<Farm>>();
            _mockUserFarms = new Mock<DbSet<UserFarm>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Farms).Returns(_mockFarms.Object);
            mockContext.SetupGet(c => c.UserFarms).Returns(_mockUserFarms.Object);

            _repository = new FarmRepository(mockContext.Object);
        }

        [Test]
        public void GetFarm_FarmDoesntExist_FarmNotReturned()
        {
            var farm = new Farm { Id = 1 };
            var farm2 = new Farm { Id = 2 };

            _mockFarms.SetSource(new[] { farm });

            var farmFromDb = _repository.GetFarm(farm2.Id);

            farmFromDb.Should().BeNull();
        }

        [Test]
        public void GetFarm_FarmExists_FarmReturned()
        {
            var farm = new Farm { Id = 1 };

            _mockFarms.SetSource(new[] { farm });

            var farmFromDb = _repository.GetFarm(farm.Id);

            farmFromDb.Should().BeOfType<Farm>();
        }

        [Test]
        public void GetByUrl_FarmDoesntExist_FarmNotReturned()
        {
            var farm = new Farm() { Url = "1" };
            _mockFarms.SetSource(new[] { farm });

            var farmFromDb = _repository.GetByUrl("2");

            farmFromDb.Should().BeNull();
        }

        [Test]
        public void GetByUrl_FarmExists_FarmReturned()
        {
            var farm = new Farm() { Url = "1" };
            _mockFarms.SetSource(new[] { farm });

            var farmFromDb = _repository.GetByUrl(farm.Url);

            farmFromDb.Should().BeOfType<Farm>();
        }
    }
}
