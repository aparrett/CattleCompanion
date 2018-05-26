using CattleCompanion.Core;
using CattleCompanion.Core.Models;
using CattleCompanion.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace CattleCompanion.Controllers
{
    [Authorize]
    public class CattleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CattleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ViewResult Create(int? farmId = null)
        {
            var userId = User.Identity.GetUserId();
            var farms = _unitOfWork.UserFarms.GetFarms(userId);
            var viewModel = new CowFormViewModel()
            {
                Farms = farms
            };

            if (farmId != null)
                viewModel.FarmId = (int)farmId;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CowFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }

            var cow = new Cow
            {
                Birthday = viewModel.Birthday,
                FarmId = viewModel.FarmId,
                GivenId = viewModel.GivenId,
                Gender = viewModel.Gender
            };

            _unitOfWork.Cattle.Add(cow);
            _unitOfWork.Complete();

            return RedirectToAction("Details", new { id = cow.Id });
        }

        public ActionResult Details(int id)
        {
            var cow = _unitOfWork.Cattle.GetCow(id);

            if (cow == null)
                return HttpNotFound();

            var events = _unitOfWork.Events.GetAll();

            var viewModel = new CowDetailsViewModel
            {
                Cow = cow,
                Events = events
            };

            return View(viewModel);
        }
    }
}
