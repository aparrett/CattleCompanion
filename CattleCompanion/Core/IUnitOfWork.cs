using CattleCompanion.Core.Repositories;

namespace CattleCompanion.Core
{
    public interface IUnitOfWork
    {
        IFarmRepository Farms { get; }
        IUserFarmRepository UserFarms { get; }
        void Complete();
    }
}