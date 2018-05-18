using CattleCompanion.Core;
using CattleCompanion.Core.Models;
using CattleCompanion.Core.ViewModels;
using Microsoft.AspNet.Identity;
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

        public ActionResult Create()
        {
            var viewModel = new FarmFormViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(FarmFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }

            var farm = new Farm
            {
                Name = viewModel.Name,
                Url = viewModel.Url
            };

            var userFarm = new UserFarm
            {
                FarmId = farm.Id,
                UserId = User.Identity.GetUserId()
            };

            _unitOfWork.Farms.Add(farm);
            _unitOfWork.UserFarms.Add(userFarm);

            _unitOfWork.Complete();

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
    }
}