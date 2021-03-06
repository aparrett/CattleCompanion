﻿using AutoMapper;
using CattleCompanion.Core;
using CattleCompanion.Core.Dtos;
using CattleCompanion.Core.Models;
using System;
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
            if (dto.Date.Year < 1900)
                return BadRequest("Please enter a valid date.");

            if (dto.EventId == 0)
                return BadRequest("Please select an event.");

            var cowEvent = Mapper.Map<CowEventDto, CowEvent>(dto);

            _unitOfWork.CowEvents.Add(cowEvent);
            _unitOfWork.Complete();

            return Created(new Uri(Request.RequestUri + cowEvent.Id.ToString()), dto);
        }
    }
}