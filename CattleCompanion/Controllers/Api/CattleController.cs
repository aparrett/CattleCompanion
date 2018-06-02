using CattleCompanion.Core;
using CattleCompanion.Core.Dtos;
using System.Web.Http;
using System.Web.Routing;

namespace CattleCompanion.Controllers.Api
{
    [Authorize]
    public class CattleController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CattleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("api/cattle/{id}")]
        public IHttpActionResult Update(int id, CowDto dto)
        {
            var cow = _unitOfWork.Cattle.GetCow(id);

            if (cow == null)
                return NotFound();

            if (dto.MotherId != null)
            {
                var mother = _unitOfWork.Cattle.GetCow((int)dto.MotherId);
                if (mother == null)
                    return NotFound();

                if (cow.Id != dto.MotherId && cow.Birthday > mother.Birthday && mother.Gender == "F")
                    cow.MotherId = dto.MotherId;
            }

            if (dto.FatherId != null)
            {
                var father = _unitOfWork.Cattle.GetCow((int)dto.FatherId);
                if (father == null)
                    return NotFound();

                if (cow.Id != dto.FatherId && cow.Birthday > father.Birthday && father.Gender == "M")
                    cow.FatherId = dto.FatherId;
            }

            _unitOfWork.Complete();

            return Ok(cow);
        }
    }
}
