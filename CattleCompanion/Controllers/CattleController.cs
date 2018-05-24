using CattleCompanion.Core;
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

        public ViewResult Create()
        {
            var userId = User.Identity.GetUserId();
            var farms = _unitOfWork.UserFarms.GetFarms(userId);
            var viewModel = new CowFormViewModel()
            {
                Farms = farms
            };
            return View(viewModel);
        }
    }
}
