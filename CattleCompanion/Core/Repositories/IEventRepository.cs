using System.Collections.Generic;
using CattleCompanion.Core.Models;

namespace CattleCompanion.Core.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();
    }
}