﻿using CattleCompanion.Core;
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
            var viewModel = new CowFormViewModel
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

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult EditCow(int id)
        {
            var userId = User.Identity.GetUserId();
            var farms = _unitOfWork.UserFarms.GetFarms(userId);
            var cow = _unitOfWork.Cattle.GetCow(id);

            var viewModel = new CowFormViewModel
            {
                Farms = farms,
                FarmId = cow.FarmId,
                GivenId = cow.GivenId,
                Birthday = cow.Birthday,
                Gender = cow.Gender
            };

            return View("Edit", viewModel);
        }

        [HttpPost]
        [Route("edit")]
        public ActionResult Edit(CowFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", viewModel);
            }

            var cow = _unitOfWork.Cattle.GetCow(viewModel.Id);

            cow.Birthday = viewModel.Birthday;
            cow.FarmId = viewModel.FarmId;
            cow.GivenId = viewModel.GivenId;
            cow.Gender = viewModel.Gender;

            _unitOfWork.Complete();

            return RedirectToAction("Details", new { id = cow.Id });
        }

        [Route("{id}")]
        public ActionResult Details(int id)
        {
            var cow = _unitOfWork.Cattle.GetCowWithAll(id);

            if (cow == null)
                return HttpNotFound();

            var events = _unitOfWork.Events.GetAll();
            var cowsInFarm = _unitOfWork.Cattle.GetAllByFarm(cow.FarmId);
            var siblings = _unitOfWork.Cattle.GetSiblings(cow.Id);

            var viewModel = new CowDetailsViewModel
            {
                Cow = cow,
                Events = events,
                Siblings = siblings,
                PossibleMothers = cowsInFarm.Where(c => c.Gender == "F" && c.Birthday < cow.Birthday && !siblings.Contains(c)),
                PossibleFathers = cowsInFarm.Where(c => c.Gender == "M" && c.Birthday < cow.Birthday && !siblings.Contains(c)),
                PossibleChildren = cowsInFarm.Where(c => c.Birthday > cow.Birthday && !siblings.Contains(c) && !cow.Children.Contains(c))
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

            // Current workaround to delete all relationships before deleting cows
            // since onCascadeDelete will not work for relationships.
            _unitOfWork.Relationships.DeleteAll(cow.Id);

            _unitOfWork.Cattle.Remove(cow);
            _unitOfWork.Complete();

            return RedirectToAction("Details", "Farms", new { url });
        }
    }
}
