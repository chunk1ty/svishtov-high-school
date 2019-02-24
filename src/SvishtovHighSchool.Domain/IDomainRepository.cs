using System;
using System.Threading.Tasks;
using SvishtovHighSchool.Domain.Core;

namespace SvishtovHighSchool.Domain
{
    public interface IDomainRepository<T> where T : AggregateRoot, new()
    {
        Task SaveAsync(AggregateRoot aggregate, int version);

        Task<T> GetByIdAsync(Guid id);
    }
}