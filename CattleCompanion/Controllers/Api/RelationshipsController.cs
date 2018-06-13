using AutoMapper;
using CattleCompanion.Core;
using CattleCompanion.Core.Dtos;
using CattleCompanion.Core.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace CattleCompanion.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/relationships")]
    public class RelationshipsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RelationshipsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Create(RelationshipDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var relationship = Mapper.Map<RelationshipDto, Relationship>(dto);

            _unitOfWork.Relationships.Add(relationship);
            _unitOfWork.Complete();

            return Created(new Uri(Request.RequestUri + relationship.Id.ToString()), dto);
        }

        [HttpDelete]
        [Route("{cow1Id}")]
        public IHttpActionResult Delete(int cow1Id, int cow2Id)
        {
            var relationship = _unitOfWork.Relationships.GetRelationship(cow1Id, cow2Id);
            if (relationship == null)
                return NotFound();

            var userCattle = _unitOfWork.Cattle.GetAllByUserId(User.Identity.GetUserId());
            if (!userCattle.Any(c => c.Id == cow1Id || c.Id == cow2Id))
                return Unauthorized();

            _unitOfWork.Relationships.Delete(relationship);
            _unitOfWork.Complete();

            return Ok("Relationship deleted successfully.");
        }
    }
}