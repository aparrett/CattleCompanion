﻿using CattleCompanion.Core;
using CattleCompanion.Core.Models;
using CattleCompanion.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CattleCompanion.Persistence.Repositories
{
    public class CowRepository : ICowRepository
    {
        private readonly IApplicationDbContext _context;

        public CowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Cow cow)
        {
            _context.Cattle.Add(cow);
        }

        public Cow GetCow(int id)
        {
            return _context.Cattle
                .Include(c => c.Farm)
                .SingleOrDefault(c => c.Id == id);
        }

        public Cow GetCowWithAll(int id)
        {
            return _context.Cattle
                .Include(c => c.Farm)
                .Include(c => c.CowEvents)
                .Include(c => c.ParentRelationships)
                .Include(c => c.ChildrenRelationships)
                .SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Cow> GetSiblings(int id)
        {
            var parents = _context.Relationships
                .Where(r => r.Cow2Id == id)
                .Select(r => r.Cow1Id)
                .ToList();

            var siblings = _context.Relationships
                .Where(r => parents.Contains(r.Cow1Id) && r.Cow2Id != id)
                .Select(r => r.Cow2)
                .Include(c => c.ParentRelationships)
                .Include(c => c.ChildrenRelationships)
                .OrderBy(c => c.GivenId)
                .ToList();

            return siblings.GroupBy(c => c.Id).Select(g => g.First()).ToList();
        }

        public IEnumerable<Cow> GetAllByFarm(int id)
        {
            return _context.Cattle
                .Where(c => c.FarmId == id)
                .OrderBy(c => c.GivenId)
                .ToList();
        }

        public IEnumerable<Cow> GetAllByUserId(string userId)
        {
            var farmIds = _context.UserFarms.Where(uf => uf.UserId == userId).Select(uf => uf.FarmId).ToList();
            return _context.Cattle.Where(c => farmIds.Contains(c.FarmId)).ToList();
        }

        public void Remove(Cow cow)
        {
            _context.Cattle.Remove(cow);
        }
    }
}