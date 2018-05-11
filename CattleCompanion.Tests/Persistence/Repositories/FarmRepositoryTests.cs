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

        [SetUp]
        public void SetUp()
        {
            _mockFarms = new Mock<DbSet<Farm>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Farms).Returns(_mockFarms.Object);

            _repository = new FarmRepository(mockContext.Object);
        }

        [Test]
        public void GetFarm_FarmDoesntExist_FarmNotReturned()
        {
            var farm = new Farm { Id = 1 };
            var farm2 = new Farm { Id = 2 };

            _mockFarms.SetSource(new[] { farm });

            var farmFromDbSet = _repository.GetFarm(farm2.Id);

            farmFromDbSet.Should().BeNull();
        }

        [Test]
        public void GetFarm_FarmExists_FarmReturned()
        {
            var farm = new Farm { Id = 1 };

            _mockFarms.SetSource(new[] { farm });

            var farmFromDbSet = _repository.GetFarm(farm.Id);

            farmFromDbSet.Should().BeOfType<Farm>();
        }
    }
}
