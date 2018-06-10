using CattleCompanion.Core;
using CattleCompanion.Core.Repositories;
using CattleCompanion.Persistence.Repositories;

namespace CattleCompanion.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IFarmRepository Farms { get; private set; }
        public IUserFarmRepository UserFarms { get; private set; }
        public ICowRepository Cattle { get; private set; }
        public IEventRepository Events { get; private set; }
        public ICowEventRepository CowEvents { get; private set; }
        public IRelationshipRepository Relationships { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Farms = new FarmRepository(context);
            UserFarms = new UserFarmRepository(context);
            Cattle = new CowRepository(context);
            Events = new EventRepository(context);
            CowEvents = new CowEventRepository(context);
            Relationships = new RelationshipRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}