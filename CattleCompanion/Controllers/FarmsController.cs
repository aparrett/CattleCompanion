using CattleCompanion.Core;
using CattleCompanion.Core.Models;
using CattleCompanion.Core.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CattleCompanion.Controllers
{
    [Authorize]
    public class FarmsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FarmsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("farms/create")]
        public ActionResult Create()
        {
            return View(new FarmFormViewModel());
        }

        [HttpPost]
        [Route("farms/create")]
        public ActionResult Create(FarmFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var farm = new Farm
            {
                Name = viewModel.Name,
                Url = viewModel.Url
            };

            var userFarm = new UserFarm
            {
                FarmId = farm.Id,
                UserId = userId
            };

            _unitOfWork.Farms.Add(farm);
            _unitOfWork.UserFarms.Add(userFarm);
            _unitOfWork.Complete();

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindById(userId);

            if (viewModel.IsDefault || user.DefaultFarmId == 0)
            {
                user.DefaultFarmId = farm.Id;
                userManager.Update(user);
            }

            return RedirectToAction("Details", new { url = farm.Url });
        }

        [Route("farms/edit/{id}")]
        public ActionResult EditFarm(int id)
        {
            var farm = _unitOfWork.Farms.GetFarm(id);
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindById(User.Identity.GetUserId());
            bool isDefault = user.DefaultFarmId == farm.Id;

            var viewModel = new FarmFormViewModel
            {
                Id = farm.Id,
                Name = farm.Name,
                Url = farm.Url,
                IsDefault = isDefault
            };

            return View("Edit", viewModel);
        }

        [HttpPost]
        [Route("farms/edit")]
        public ActionResult Edit(FarmFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Edit", viewModel);

            var farm = _unitOfWork.Farms.GetFarm(viewModel.Id);

            farm.Url = viewModel.Url;
            farm.Name = viewModel.Name;

            _unitOfWork.Complete();

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindById(User.Identity.GetUserId());

            if (viewModel.IsDefault || user.DefaultFarmId == 0)
            {
                user.DefaultFarmId = farm.Id;
                userManager.Update(user);
            }

            return RedirectToAction("Details", new { url = farm.Url });
        }

        [Route("farm/{url}")]
        public ActionResult Details(string url)
        {
            var farm = _unitOfWork.Farms.GetByUrl(url);
            if (farm == null)
                return HttpNotFound();

            var userFarm = _unitOfWork.UserFarms.GetUserFarm(farm.Id, User.Identity.GetUserId());
            if (userFarm == null)
            {
                return new HttpUnauthorizedResult();
            }

            return View(farm);
        }

        public ActionResult Delete(int id)
        {
            var farm = _unitOfWork.Farms.GetFarm(id);
            var userId = User.Identity.GetUserId();

            if (farm == null)
                return HttpNotFound();

            var userFarms = _unitOfWork.UserFarms.GetAllByFarmId(id);
            var authUserFarm = userFarms.SingleOrDefault(uf => uf.UserId == userId);
            var nonAuthUserFarm = userFarms.FirstOrDefault(uf => uf.UserId != userId);

            if (authUserFarm == null)
                return new HttpUnauthorizedResult();

            _unitOfWork.UserFarms.Remove(authUserFarm);

            if (nonAuthUserFarm == null)
            {
                _unitOfWork.Farms.Remove(farm);
            }

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindById(userId);

            // Set another farm as the user's default if the deleted farm was their default.
            if (user.DefaultFarmId == id)
            {
                var otherUserFarms = _unitOfWork.UserFarms.GetFarms(userId);
                if (otherUserFarms.Any())
                {
                    var newDefault = otherUserFarms.First();
                    user.DefaultFarmId = newDefault.Id;
                    userManager.Update(user);
                }
            }

            _unitOfWork.Complete();

            return RedirectToAction("All");
        }

        [Route("farms/")]
        public ActionResult All()
        {
            return View(_unitOfWork.UserFarms.GetFarms(User.Identity.GetUserId()));
        }
    }
}