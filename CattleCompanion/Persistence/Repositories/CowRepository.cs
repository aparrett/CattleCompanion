﻿using CattleCompanion.Core.Models;
using CattleCompanion.Core.Repositories;

namespace CattleCompanion.Persistence.Repositories
{
    public class CowRepository : ICowRepository
    {
        private ApplicationDbContext _context;

        public CowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Cow cow)
        {
            _context.Cattle.Add(cow);
        }
    }
}