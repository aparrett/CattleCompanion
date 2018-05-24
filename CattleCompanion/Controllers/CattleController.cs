using CattleCompanion.Core;
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

        //        public ViewResult Create()
        //        {
        //            return View(new cowFormViewModel());
        //        }
    }
}
