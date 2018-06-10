using CattleCompanion.Core;
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

        [HttpGet]
        [Route("{id:int}/siblings")]
        public IHttpActionResult GetSiblings(int id)
        {
            var cow = _unitOfWork.Cattle.GetCow(id);
            if (cow == null)
                return NotFound();

            var siblings = _unitOfWork.Cattle.GetSiblings(cow);

            if (siblings == null)
                return Ok();

            return Ok(siblings);
        }

        public IHttpActionResult GetCow(int id)
        {
            var cow = _unitOfWork.Cattle.GetCow(id);
            if (cow == null)
                return NotFound();

            return Ok(cow);
        }
    }
}
