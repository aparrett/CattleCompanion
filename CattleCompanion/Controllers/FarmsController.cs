using CattleCompanion.Core;
using CattleCompanion.Core.Models;
using CattleCompanion.Core.ViewModels;
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

            _unitOfWork.Farms.Add(farm);
            _unitOfWork.Complete();

            return RedirectToAction("Index", "Home");
        }
    }
}