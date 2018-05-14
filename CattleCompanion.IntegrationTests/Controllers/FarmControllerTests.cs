using CattleCompanion.Controllers;
using CattleCompanion.Core.Models;
using CattleCompanion.Core.ViewModels;
using CattleCompanion.IntegrationTests.Extensions;
using CattleCompanion.Persistence;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Linq;
using System.Web.Mvc;

namespace CattleCompanion.IntegrationTests.Controllers
{
    [TestFixture]
    class FarmControllerTests
    {
        private FarmsController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new FarmsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Create_WhenCalled_ShouldRedirectToFarmDetails()
        {
            var user = _context.Users.First();

            _controller.MockCurrentUser(user.Id, user.UserName);

            var farm = new Farm { Name = "Farm1", Url = "Farm1" };

            var result = _controller.Create(new FarmFormViewModel
            {
                Id = farm.Id,
                Name = farm.Name,
                Url = farm.Url
            }) as RedirectToRouteResult;

            Assert.That(result.RouteValues["action"], Is.EqualTo("Details"));
            Assert.That(result.RouteValues["url"], Is.EqualTo(farm.Url));
        }
    }
}
