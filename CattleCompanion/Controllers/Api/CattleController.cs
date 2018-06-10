using CattleCompanion.Core;
using CattleCompanion.Core.Dtos;
using System.Web.Http;
using System.Web.Routing;

namespace CattleCompanion.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/cattle")]
    public class CattleController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CattleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IHttpActionResult GetCow(int id)
        {
            var cow = _unitOfWork.Cattle.GetCow(id);
            if (cow == null)
                return NotFound();

            return Ok(cow);
        }

        [HttpGet]
        [Route("{id:int}/{relationship}")]
        public IHttpActionResult GetCowsInRelationship(int id, string relationship)
        {
            var cow = _unitOfWork.Cattle.GetCow(id);
            if (cow == null)
                return NotFound();

            if (relationship == "siblings")
            {
                var siblings = _unitOfWork.Cattle.GetSiblings(cow);
                return Ok(siblings);
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(int id, CowDto dto)
        {
            //            var cow = _unitOfWork.Cattle.GetCow(id);
            //
            //            if (cow == null)
            //                return NotFound();
            //
            //            if (dto.MotherId != null)
            //            {
            //                if (dto.MotherId == 0)
            //                {
            //                    cow.MotherId = null;
            //                }
            //                else
            //                {
            //                    var mother = _unitOfWork.Cattle.GetCow((int)dto.MotherId);
            //                    if (mother == null)
            //                        return NotFound();
            //
            //                    if (cow.Id != dto.MotherId && cow.Birthday > mother.Birthday && mother.Gender == "F")
            //                        cow.MotherId = dto.MotherId;
            //                }
            //            }
            //
            //            if (dto.FatherId != null)
            //            {
            //                if (dto.FatherId == 0)
            //                {
            //                    cow.FatherId = null;
            //                }
            //                else
            //                {
            //                    var father = _unitOfWork.Cattle.GetCow((int)dto.FatherId);
            //                    if (father == null)
            //                        return NotFound();
            //
            //                    if (cow.Id != dto.FatherId && cow.Birthday > father.Birthday && father.Gender == "M")
            //                        cow.FatherId = dto.FatherId;
            //                }
            //            }
            //
            //            if (dto.ChildId != null)
            //            {
            //                var child = _unitOfWork.Cattle.GetCow((int)dto.ChildId);
            //                if (child == null)
            //                    return NotFound();
            //
            //                if (cow.Birthday < child.Birthday)
            //                {
            //                    if (cow.Gender == "M")
            //                        child.FatherId = cow.Id;
            //                    else
            //                        child.MotherId = cow.Id;
            //
            //                    // Return child because it is technically the model being updated.
            //                    cow = child;
            //                }
            //            }
            //
            //            if (dto.ParentId != null && cow.FatherId == dto.ParentId)
            //            {
            //                cow.FatherId = null;
            //            }
            //
            //            if (dto.ParentId != null && cow.MotherId == dto.ParentId)
            //            {
            //                cow.MotherId = null;
            //            }
            //
            //            _unitOfWork.Complete();
            //
            //            return Ok(cow);
            return Ok();
        }
    }
}
