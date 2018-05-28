using CattleCompanion.Core;
using CattleCompanion.Core.Dtos;
using CattleCompanion.Core.Models;
using System.Web.Http;

namespace CattleCompanion.Controllers.Api
{
    [Authorize]
    public class CowEventsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CowEventsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult CreateCowEvent(CowEventDto dto)
        {
            var cowEvent = new CowEvent
            {
                CowId = dto.CowId,
                EventId = dto.EventId,
                Date = dto.Date,
                Description = dto.Description
            };

            _unitOfWork.CowEvents.Add(cowEvent);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}