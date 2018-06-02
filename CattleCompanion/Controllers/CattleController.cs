using CattleCompanion.Core;
using CattleCompanion.Core.Models;
using CattleCompanion.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace CattleCompanion.Controllers
{
    [Authorize]
    [RoutePrefix("cattle")]
    public class CattleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CattleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("create")]
        public ViewResult Create(int id)
        {
            var userId = User.Identity.GetUserId();
            var farms = _unitOfWork.UserFarms.GetFarms(userId);
            var viewModel = new CowFormViewModel()
            {
                Farms = farms,
                FarmId = id
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("create")]
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

        [Route("details/{id}")]
        public ActionResult Details(int id)
        {
            var cow = _unitOfWork.Cattle.GetCow(id);

            if (cow == null)
                return HttpNotFound();

            var events = _unitOfWork.Events.GetAll();
            var cowsInFarm = _unitOfWork.Cattle.GetAllByFarm(cow.FarmId);

            var viewModel = new CowDetailsViewModel
            {
                Cow = cow,
                Events = events,
                Children = _unitOfWork.Cattle.GetChildren(cow),
                Siblings = _unitOfWork.Cattle.GetSiblings(cow),
                MotherOptions = cowsInFarm.Where(c => c.Gender == "F" && c.Birthday < cow.Birthday),
                FatherOptions = cowsInFarm.Where(c => c.Gender == "M" && c.Birthday < cow.Birthday),
                ChildOptions = cowsInFarm.Where(c => c.Birthday > cow.Birthday)
            };

            return View(viewModel);
        }

        public ActionResult Delete(int id)
        {
            var cow = _unitOfWork.Cattle.GetCow(id);
            var url = cow.Farm.Url;

            if (cow == null)
                return HttpNotFound();

            var userFarm = _unitOfWork.UserFarms.GetUserFarm(cow.FarmId, User.Identity.GetUserId());
            if (userFarm == null)
                return new HttpUnauthorizedResult();

            var children = _unitOfWork.Cattle.GetChildren(cow);
            foreach (Cow child in children)
            {
                if (child.MotherId == cow.Id)
                    child.DeleteMother();
                else
                    child.DeleteFather();
            }

            _unitOfWork.Cattle.Remove(cow);
            _unitOfWork.Complete();

            return RedirectToAction("Details", "Farms", new { url });
        }
    }
}
