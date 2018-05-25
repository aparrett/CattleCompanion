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

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Farms = new FarmRepository(context);
            UserFarms = new UserFarmRepository(context);
            Cattle = new CowRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}