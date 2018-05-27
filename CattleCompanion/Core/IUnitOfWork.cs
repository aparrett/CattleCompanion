using CattleCompanion.Core.Repositories;

namespace CattleCompanion.Core
{
    public interface IUnitOfWork
    {
        IFarmRepository Farms { get; }
        IUserFarmRepository UserFarms { get; }
        ICowRepository Cattle { get; }
        IEventRepository Events { get; }
        ICowEventRepository CowEvents { get; }
        void Complete();
    }
}